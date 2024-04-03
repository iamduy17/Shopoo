import { Injectable } from '@angular/core';
import { DataAccessService } from './data-access.service';
import * as global from "../globals";
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/reponse-model';
import { GetCategoryResponseModel } from '../models/category/response/get-category-list';
import { AddCategoryRequestModel } from '../models/category/request/add-category-request';
import { AddCategoryResponseModel } from '../models/category/response/add-category-response';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private _dao: DataAccessService
  ) { }

  private url = `${global.API_URL}/api/Category`;

  getCategoryList(): Observable<ResponseModel<GetCategoryResponseModel>> {
    return this._dao.get(this.url);
  }

  addCategory(request: AddCategoryRequestModel): Observable<ResponseModel<AddCategoryResponseModel>> {
    return this._dao.post(this.url, request);
  }
}
