import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Hendvendelse } from './Hendvendelse';
import { Kategori } from '../FAQ/Kategori';

@Component({
  templateUrl: './hendvendelse.component.html'
})

export class HendvendelseComponent {
  public skjema: FormGroup;
  public hendvedelseSendt: boolean;
  public alleKategorier: Array<Kategori>;

  emailRegex = RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

  validering = {
    email: [
      null, Validators.compose([Validators.required, Validators.pattern(this.emailRegex)])
    ],
    kategori: [
      null, Validators.compose([Validators.required])
    ],
    melding: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZæøåÆØÅ. \-]{10,2000}$")])
    ]
  }


  constructor(private _http: HttpClient, private fb: FormBuilder) {
    this.skjema = fb.group(this.validering);
  }

  ngOnInit() {
    this.hendvedelseSendt = false;
    this.hentKategorier();
  }

  onSubmit() {
    this.lagHendvendelse();
  }

  hentKategorier() {
    this._http.get<Kategori[]>("api/Kategori/")
      .subscribe(data => {
        this.alleKategorier = data;
      },
        error => console.log(error),
      );
  }

  lagHendvendelse() {
    const hendvendelse = new Hendvendelse();

    hendvendelse.email = this.skjema.value.email;
    hendvendelse.kategori = this.skjema.value.kategori;
    hendvendelse.melding = this.skjema.value.melding;
    hendvendelse.status = false;

    this._http.post("api/Hendvendelse/Hendvendelse", hendvendelse, { responseType: 'text' })
      .subscribe(retur => {
        this.hendvendelseSendt();
      },
        error => console.log(error),
    );
  };

  //Resetter felter
  hendvendelseSendt() {
    this.hendvedelseSendt = true;
   
    this.skjema.patchValue({ email: "" });
    this.skjema.patchValue({ kategori: "" });
    this.skjema.patchValue({ melding: "" });
  }
}
