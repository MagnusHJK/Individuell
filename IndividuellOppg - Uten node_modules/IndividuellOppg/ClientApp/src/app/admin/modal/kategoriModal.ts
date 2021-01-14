import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http';
import { Hendvendelse } from '../../hendvendelse/Hendvendelse';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  templateUrl: 'kategoriModal.html'
})
export class KategoriModal {
  public hendvendelse: Hendvendelse;


  constructor(public modal: NgbActiveModal, private _http: HttpClient) {
  }

  ngOnInit() {
  }
}
