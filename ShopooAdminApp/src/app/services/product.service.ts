import { Injectable } from '@angular/core';
import { DataAccessService } from './data-access.service';
import * as global from "../globals";
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/reponse-model';
import { GetProductListModel } from '../models/product/response/get-product-list';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(
    private _dao: DataAccessService
  ) { }

  private url = `${global.API_URL}/api/Product`;

  getProductList(): Observable<ResponseModel<GetProductListModel>> {
    return this._dao.get(this.url);
  }
}
