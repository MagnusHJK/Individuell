import { Component, OnInit } from '@angular/core';
import { FAQ } from './FAQ';
import { Kategori } from './Kategori';
import { HttpClient } from '@angular/common/http';

@Component({
  templateUrl: './faq.component.html'
})
export class FAQComponent {
  public alleFAQs: Array<FAQ>;
  public kategoriFAQs: Array<string>;
  public tilbakemeldingGitt: Array<boolean>;
  public sporsmalToggle: Array<boolean>;
  public kategoriToggle: Array<boolean>;
  public variabel: boolean;

  constructor(private _http: HttpClient) {
  }

  ngOnInit() {
    this.hentAlleFAQs();
  }

  //Hvis det blir stemt på om svaret var behjelpelig eller ikke
  endreTilbakemelding(faq: FAQ, hjelp: boolean) {
    this.tilbakemeldingGitt[faq.id] = true;

    var endretFAQ = new FAQ;
    endretFAQ = faq;

    //Hvis det var til hjelp
    if (hjelp) {
      endretFAQ.oppstemmer++;
    } else {
      endretFAQ.nedstemmer++;
    }

    this._http.put("api/FAQ/", endretFAQ, { responseType: 'text' })
      .subscribe(
        retur => {
          console.log("endret");
        }
      );
  }

  hentAlleFAQs() {
    this._http.get<FAQ[]>("api/FAQ/")
      .subscribe(data => {
        this.alleFAQs = data;
        this.lagFAQKategori()
      },
        error => alert(error),
    );
  }

  lagKategoriToggle(antall: number){
    var midlertidig: boolean[] = new Array();

    for (var i = 0; i <= antall; i++) {
      midlertidig[i] = false;
    }
    this.kategoriToggle = midlertidig;

  }

  //Array som holder på om en tilbakemeldig skal være positiv eller negativ
  lagSporsmalArray(antall: number) {
    var midlertidig: boolean[] = new Array(antall);

    for (var i = 0; i <= antall; i++) {
      midlertidig[i] = false;
    }
    this.tilbakemeldingGitt = Object.assign([], midlertidig);
    this.sporsmalToggle = Object.assign([], midlertidig);
  }

  //Legger alle kategorier som har FAQs til seg i egen liste
  lagFAQKategori() {
    this.kategoriFAQs = Array.from(new Set(this.alleFAQs.map((item: any) => item.kategori.navn)));
    this.lagKategoriToggle(this.kategoriFAQs.length);
    this.lagSporsmalArray(this.alleFAQs.length);
  }
}
