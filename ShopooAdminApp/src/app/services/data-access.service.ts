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

  put<T>(url: string, requestData: any, showLoading = true): Observable<T> {
    const id = showLoading ? this.loading.pushLoading() : '';
    return this.http.put<T>(url, requestData, this.httpOptions)
      .pipe(
        tap(() => this.loading.popLoading(id)),
        map((r) => this.processResult<T>(r)),
        catchError((error) => this.handleError(error, id))
      );
  }

  delete<T>(url: string, showLoading = true): Observable<T> {
    const id = showLoading ? this.loading.pushLoading() : '';

    return this.http.delete<T>(url, this.httpOptions)
      .pipe(
        tap(() => this.loading.popLoading(id)),
        map((r) => this.processResult<T>(r)),
        catchError((error) => this.handleError(error, id))
      );
  }

  processResult<T>(result: any): T {
    if(result?.returnCode == "not.found") {
      throw new HttpErrorResponse({ status: 404 });
    }
    return result;
  }

  handleError(error: HttpErrorResponse, id: string): Observable<never> {
    this.loading.popLoading(id);
    if (error.error instanceof ErrorEvent) {
    } else {
      if(error.status == 404) {
        this._alert.showAlert("Not found!");
      } else {
        this._alert.showAlert("There are something wrong in the system. Please retry later!");
      }
      
    }
    return throwError(error);
  }
}
