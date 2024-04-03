import { AfterViewInit, Component } from '@angular/core';
import { LoadingService } from './services/loading.service';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { AlertService } from './services/alert.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {
  title = 'ShopooAdminApp';
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(public loadingService: LoadingService, private alertService: AlertService,
    private snackBar: MatSnackBar) {
      this.alertService.onAlert.subscribe(this.openSnackBar.bind(this));
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

  openSnackBar(request: { message: string, timeout: number }): void {
    this.snackBar.open(request.message, '', {
      duration: request.timeout,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      panelClass: ['test-snackbar']
    });
  }
}
