import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, pipe, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class DataAccessService {

  constructor(
    private http: HttpClient,
    private loading: LoadingService,
    private router: Router,
    private _dialog: MatDialog,
    private _alert: AlertService
  ) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };


  get<T>(url: string, showLoading = true): Observable<T> {
    const id = showLoading ? this.loading.pushLoading() : '';
    return this.http.get<T>(url)
      .pipe(
        tap(() => this.loading.popLoading(id)),
        map((r) => this.processResult<T>(r)),
        catchError((error) => this.handleError(error, id))
      );
  }

  post<T>(url: string, requestData: any, showLoading = true): Observable<T> {
    const id = showLoading ? this.loading.pushLoading() : '';
    return this.http.post<T>(url, requestData, this.httpOptions)
      .pipe(
        tap(() => this.loading.popLoading(id)),
        map((r) => this.processResult<T>(r)),
        catchError((error) => this.handleError(error, id))
      );
  }

  processResult<T>(result: any): T {
    if(result?.returnCode == "account.not.permission") {
      throw new HttpErrorResponse({ status: 403 });
    }
    return result;
  }

  handleError(error: HttpErrorResponse, id: string): Observable<never> {
    this.loading.popLoading(id);
    if (error.error instanceof ErrorEvent) {
    } else {
      this._alert.showAlert("Có lỗi trong quá trình xử lý!");
    }
    return throwError(error);
  }
}
