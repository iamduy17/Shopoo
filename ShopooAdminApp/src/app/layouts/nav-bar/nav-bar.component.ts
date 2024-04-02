import { Component, OnInit } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';

interface NavMenu {
  text?: string; logo?: string; url?: string;
}

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(breakpointObserver: BreakpointObserver) {
    breakpointObserver.observe('(min-width: 1367px)').subscribe((u) => this.showMenu = u.matches);
  }

  showMenu = true;

  navData: NavMenu[] = [
    { text: 'Category', logo: 'list', url: '/category' },
    { text: 'Product', logo: 'library_books', url: '/product' },
  ];

  ngOnInit(): void {
  }

}
