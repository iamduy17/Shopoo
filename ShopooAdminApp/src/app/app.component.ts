import { AfterViewInit, Component } from '@angular/core';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  title = 'ShopooAdminApp';

  constructor(public loadingService: LoadingService) {

  }

  ngAfterViewInit(): void {
    this.loadingService.isShowLoadingSub.subscribe((response: boolean) => {
      if (response) {
        setTimeout(() => {
          const loader = document.getElementById('loader');
          if (loader) {
            loader.style.opacity = '1';
          }
        }, 500);
      }
      else {
        document.getElementsByTagName('body')[0].classList.remove('loading-process');
      }
    });
  }
}
