import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FAQComponent } from './faq/faq.component';
import { HendvendelseComponent } from './hendvendelse/hendvendelse.component';
import { AdminComponent } from './admin/admin.component';

const appRoots: Routes = [
  { path: 'faq', component: FAQComponent },
  { path: 'hendvendelse', component: HendvendelseComponent },
  { path: 'admin', component: AdminComponent },
  { path: '', redirectTo: 'faq', pathMatch: 'full' }
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})

export class AppRoutingModule {
}
