import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { FAQComponent } from './faq/faq.component';
import { HendvendelseComponent } from './hendvendelse/hendvendelse.component';
import { AdminComponent } from './admin/admin.component';
import { NavMenuComponent } from './nav-meny/nav-meny.component';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FAQModal } from './admin/modal/FAQModal';
import { HendvendelseModal } from './admin/modal/hendvendelseModal';

import { AppComponent } from './app.component';
import { Kategori } from './faq/Kategori';
import { KategoriModal } from './admin/modal/kategoriModal';
import { collapse } from './collapse';



@NgModule({
  declarations: [
    AppComponent,
    FAQComponent,
    HendvendelseComponent,
    AdminComponent,
    NavMenuComponent,
    FAQModal,
    HendvendelseModal,
    KategoriModal,
    collapse
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [FAQModal, HendvendelseModal, KategoriModal]
})
export class AppModule { }
