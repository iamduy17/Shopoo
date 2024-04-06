import { Injectable } from '@angular/core';
import { DataAccessService } from './data-access.service';
import * as global from "../globals";
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/reponse-model';
import { GetProductListModel } from '../models/product/response/get-product-list';
import { AddProductRequestModel } from '../models/product/request/add-product-request';
import { AddProductResponseModel } from '../models/product/response/add-product-response';
import { UpdateProductRequestModel } from '../models/product/request/update-product-request';
import { UpdateProductResponseModel } from '../models/product/response/update-product-response';

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

  addProduct(request: AddProductRequestModel): Observable<ResponseModel<AddProductResponseModel>> {
    return this._dao.post(this.url, request);
  }

  updateProduct(request: UpdateProductRequestModel): Observable<ResponseModel<UpdateProductResponseModel>> {
    return this._dao.put(`${this.url}/${request.id}`, request.product);
  }

  deleteProduct(id: string): Observable<ResponseModel<UpdateProductResponseModel>> {
    return this._dao.delete(`${this.url}/${id}`);
  }
}
