import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProductModel } from 'src/app/models/product/response/product';
import { AlertService } from 'src/app/services/alert.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-confirm-delete-product',
  templateUrl: './confirm-delete-product.component.html',
  styleUrls: ['./confirm-delete-product.component.css']
})
export class ConfirmDeleteProductComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ProductModel,
    private _alert: AlertService,
    private _productService: ProductService,
    private _dialogRef: MatDialogRef<ConfirmDeleteProductComponent>
  ) { }

  ngOnInit(): void {
  }

  deleteProduct(): void {
    this._productService.deleteProduct(this.data.id).subscribe(res => {
      switch(res.returnCode) {
        case "success":
          this._alert.showAlert(`Delete product ${this.data.name} successfully!`);
          this._dialogRef.close(true);
          break;
        default:
          this._alert.showError();
          break;
      }
    });
  }

}
