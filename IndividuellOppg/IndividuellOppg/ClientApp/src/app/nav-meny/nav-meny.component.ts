import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-meny',
  templateUrl: './nav-meny.component.html'
})

export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toogle() {
    this.isExpanded = !this.isExpanded;
  }
}
