import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Hendvendelse } from '../hendvendelse/Hendvendelse';
import { Kategori } from '../faq/Kategori';
import { FAQ } from '../faq/FAQ';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { HendvendelseModal } from './modal/hendvendelseModal';
import { FAQModal } from './modal/FAQModal';

@Component({
  templateUrl: './admin.component.html'
})
export class AdminComponent {
  //Array
  public alleHendvendelser: Array<Hendvendelse>;
  public alleKategorier: Array<Kategori>
  public alleFAQs: Array<FAQ>;

  //Objekt som holdes på nå
  public valgtKategori: string;
  public hendvendelse: Hendvendelse;

  constructor(private _http: HttpClient, private modalSerivce: NgbModal) {
  }

  ngOnInit() {
    this.hentAlleHendvendelser();
    this.hentKategorier();
    this.hentAlleFAQs();
    this.valgtKategori = "";
  }


  hentAlleHendvendelser() {
    this._http.get<Hendvendelse[]>("api/Hendvendelse/")
      .subscribe(data => {
        this.alleHendvendelser = data;
      },
        error => console.log(error)
      );
  }

  hentAlleFAQs() {
    this._http.get<FAQ[]>("api/FAQ/")
      .subscribe(data => {
        this.alleFAQs = data;
      },
        error => console.log(error)
      );
  }

  hentKategorier() {
    this._http.get<Kategori[]>("api/Kategori/")
      .subscribe(data => {
        this.alleKategorier = data;
      },
        error => console.log(error),
      );
  }

  redigerFAQModal(FAQ: FAQ) {
    var modalRef = this.modalSerivce.open(FAQModal);
    modalRef.componentInstance.FAQ = FAQ;
    modalRef.componentInstance.nyFAQSannhet = false;
    modalRef.componentInstance.nyKategoriSannhet = false;
    modalRef.componentInstance.alleKategorier = this.alleKategorier;
    modalRef.componentInstance.tittel = "Rediger FAQ";
  }

  lagFAQModal() {
    const modalRef = this.modalSerivce.open(FAQModal);
    modalRef.componentInstance.nyFAQSannhet = true;
    modalRef.componentInstance.nyKategoriSannhet = false;
    modalRef.componentInstance.alleKategorier = this.alleKategorier;
    modalRef.componentInstance.tittel = "Lag FAQ";

    modalRef.result.then(retur => {
      this.hentAlleFAQs();
      this.hentKategorier();
    });

  }

  slettFAQModal(FAQ: FAQ) {
    var modalRef = this.modalSerivce.open(FAQModal);
    modalRef.componentInstance.FAQ = FAQ;
    modalRef.componentInstance.slettFAQSannhet = true;
    modalRef.componentInstance.alleKategorier = this.alleKategorier;
    modalRef.componentInstance.tittel = "Slett FAQ";

    modalRef.result.then(retur => {
      this.hentAlleFAQs();
    });
  }

  //Åpner popup og lar deg svare på en hendvendelse
  svarHendvendelse(hendvendelse: Hendvendelse) {
    this.hendvendelse = hendvendelse;
    const modalRef = this.modalSerivce.open(HendvendelseModal);
    modalRef.componentInstance.hendvendelse = hendvendelse;
    modalRef.componentInstance.tittel = "Svar på kundehendvendelse";
    modalRef.componentInstance.svarHendvendelseOK = true;
  }


  //Hvis hendvendelsen er behandlet trenger vi ikke å bekrefte sletting
  //Hvis ikke åpnes modal
  sletteSjekk(hendvendelse) {
    //Direkte slett
    if (hendvendelse.status) {
      this.slettHendvendelse(hendvendelse);
    }
    //Bekreftelse
    else {
      this.slettHendvendelseModal(hendvendelse);
    }
  }


  slettHendvendelseModal(hendvendelse: Hendvendelse) {
    this.hendvendelse = hendvendelse;
    const modalRef = this.modalSerivce.open(HendvendelseModal);
    modalRef.componentInstance.hendvendelse = hendvendelse;
    modalRef.componentInstance.tittel = "Sikker på at vil slette hendvendelsen?";
    modalRef.componentInstance.svarHendvendelseOK = false;

    //Hvis modal lukkes med slett
    modalRef.result.then(retur => {
      if (retur === "Slett") {
        this.slettHendvendelse(hendvendelse);
      }
    });
  }

  //Sletter hendvendelse uten bruk av modal
  slettHendvendelse(hendvendelse) {
    const url = "api/Hendvendelse/" + hendvendelse.id;

    this._http.delete<Hendvendelse>(url, { responseType: 'text' as 'json' })
      .subscribe(retur => {
        this.hentAlleHendvendelser()
        error => console.log(error)
      });
  }
}
