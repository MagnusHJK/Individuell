import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Kategori } from '../../faq/Kategori';
import { FAQ } from '../../faq/FAQ';

@Component({
  templateUrl: 'faqModal.html'
})
export class FAQModal {
  public skjema: FormGroup;

  public FAQ: FAQ;
  public nyFAQ: FAQ;
  public nyFAQSannhet: boolean;
  public slettFAQSannhet: boolean;

  public alleKategorier: Array<Kategori>;
  public tittel: string;

  public nyKategoriSannhet: boolean;

  public tilbakemeldingOK: boolean;
  public tilbakemelding: string;



  validering = {
    kategori: [
      null, Validators.compose([Validators.required])
    ],
    nyKategori: [
      { value: '', disabled: true }, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ. \-]{3,20}")])
    ],
    sporsmal: [
      null, Validators.compose([Validators.required, Validators.pattern("^.{10,500}$")])
    ],
    svar: [
      null, Validators.compose([Validators.required, Validators.pattern("^.{10,500}$")])
    ]
  }


  constructor(public modal: NgbActiveModal, private _http: HttpClient, private fb: FormBuilder) {
    this.skjema = fb.group(this.validering);
  }


  ngOnInit() {
    this.tilbakemeldingOK = false;

    //Hvis det skal slettes en FAQ
    if (this.slettFAQSannhet) {
      this.skjema.controls['sporsmal'].disable();
      this.skjema.controls['svar'].disable();
    }

    //Hvis det ikke skal lages ny FAQ
    if (!this.nyFAQSannhet) {
      this.skjema.controls['kategori'].disable();
      this.skjema.controls['nyKategori'].disable();

      this.skjema.controls['svar'].setValue(this.FAQ.svar);
      this.skjema.controls['sporsmal'].setValue(this.FAQ.sporsmal);
    }

    //Hvis det ikke sendes inn en FAQ, altså det skal opprettes FAQ, så settes string verdier som skal vises til intet
    if (this.FAQ == null) {
      this.skjema.controls['svar'].setValue("");
      this.skjema.controls['sporsmal'].setValue("");
    }
  }

  //Utifra om bruker vil ha ny eller gammle kategori for ny FAQ
  //Vi må derfor se hvilket felt vi skal validere
  kategoriValideringToggle() {
    if (this.nyKategoriSannhet) {
      this.skjema.controls['nyKategori'].enable();
      this.skjema.controls['kategori'].disable();
    }
    else {
      this.skjema.controls['kategori'].enable();
      this.skjema.controls['nyKategori'].disable();
    }
  }

  //Oppretter selve FAQ objektet, lager også kategori hvis den er ny.
  //Sendes videre til lagFAQ som oppretter på backend
  opprettFAQ() {
    this.nyFAQ = new FAQ();

    this.nyFAQ.nedstemmer = 0;
    this.nyFAQ.oppstemmer = 0;
    this.nyFAQ.sporsmal = this.skjema.value.sporsmal;
    this.nyFAQ.svar = this.skjema.value.svar;

    //Hvis ny kategori til FAQ, så må denne lages og returneres først
    if (this.nyKategoriSannhet) {
      this.tilbakemelding = "Jobber...";
      this.tilbakemeldingOK = true;

      var nyKategori = new Kategori();
      nyKategori.navn = this.skjema.value.nyKategori;

      this._http.post<Kategori>("/api/Kategori/", nyKategori)
        .subscribe(data => {
          if (data != null) {

            //Lager FAQ med ny kategori
            this.nyFAQ.kategori = data;
            this.lagFAQ();
          }
        },
          error => { console.log(error), this.tilbakemelding = error;}
        );

    }
    //Ingen ny kategori, bruker eksisterende
    else {
      this.nyFAQ.kategori = this.skjema.value.kategori;
      this.lagFAQ();
    }
  }

  //Lag FAQ på backend
  lagFAQ() {
    this.tilbakemelding = "Jobber...";
    this.tilbakemeldingOK = true;

    this._http.post("/api/FAQ/", this.nyFAQ, { responseType: 'text' })
      .subscribe(retur => {
        if (retur == "FAQ ble opprettet") {
          this.tilbakemeldingOK = true;
          this.tilbakemelding = "FAQ ble opprettet";
        }
      },
        error => console.log(error),
      );
  }

  //Endre eksisterende FAQ
  endreFAQ() {
    this.tilbakemelding = "Jobber...";
    this.tilbakemeldingOK = true;

    this.FAQ.sporsmal = this.skjema.value.sporsmal;
    this.FAQ.svar = this.skjema.value.svar;

    this._http.put("/api/FAQ", this.FAQ, { responseType: 'text' })
      .subscribe(retur => {
        if (retur == "FAQ ble endret") {
          this.tilbakemeldingOK = true;
          this.tilbakemelding = "FAQ ble endret";
        }
      },
        error => console.log(error),
      );

  }

  slettFAQ() {
    this._http.delete<FAQ>("/api/FAQ/" + this.FAQ.id, { responseType: 'text' as 'json' })
      .subscribe(retur => {
        this.tilbakemeldingOK = true;
        this.tilbakemelding = "FAQ ble slettet";
      },
        error => console.log(error),
      );
  }
}
