import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http';
import { Hendvendelse } from '../../hendvendelse/Hendvendelse';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Mail } from '../Mail';

@Component({
  templateUrl: 'hendvendelseModal.html'
})
export class HendvendelseModal {
  public hendvendelse: Hendvendelse;
  public tittel: string;
  public svarHendvendelseOK: boolean;

  public emailTittel: string;
  public skjema: FormGroup;
  public emailSendt: boolean;

  validering = {
    emailTittel: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{3,200}")])
    ],
    //Ingen regex fordi vi vil ha muligheten til å sende html svar
    emailSvar: [
      null, Validators.compose([Validators.required, Validators.minLength(10), Validators.maxLength(2000)])
    ]
  }


  constructor(public modal: NgbActiveModal, private _http: HttpClient, private fb: FormBuilder) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.fyllInnInfo();
  }

  //Fyller inn en standard email subject
  fyllInnInfo() {
    this.skjema.controls['emailTittel'].setValue("NORWAY - Svar på din hendvendelse angående " + this.hendvendelse.kategori.navn);
  }

  svarHendvendelse() {
    var mail = new Mail();

    mail.tilAddresse = this.hendvendelse.email;
    mail.tittel = this.skjema.value.emailTittel;
    mail.body = this.skjema.value.emailSvar;

    this._http.post("/api/Hendvendelse/Mail", mail, { responseType: 'text' })
      .subscribe(retur => {
        if (retur == "Mail sendt") {
          this.emailSendt = true;
          this.mailVellykket();
        }
      },
        error => console.log(error),
      );
  }

  mailVellykket() {
    this.emailSendt = true;
    var endretHendvendelse = this.hendvendelse;
    endretHendvendelse.status = true;

    this._http.put("api/Hendvendelse/", endretHendvendelse, { responseType: 'text' })
      .subscribe(retur => {
        if (retur == "Hendvendelse endret") {
          //hvis alt går bra
        }
      });
  }
}
