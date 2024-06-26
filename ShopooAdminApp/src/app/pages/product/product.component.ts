import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ImageUploadComponent } from 'src/app/components/image-upload/image-upload.component';
import { TableTemplate } from 'src/app/components/table/table.component';
import { ImageFileResult } from 'src/app/models/common/image-file-result';
import { ProductModel } from 'src/app/models/product/response/product';
import { AlertService } from 'src/app/services/alert.service';
import { ProductService } from 'src/app/services/product.service';
import { AddProductComponent } from './add-product/add-product.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [
    {
      provide: MatDialogRef,
      useValue: {}
    }
  ]
})
export class ProductComponent implements OnInit {

  constructor(
    private _alert: AlertService,
    private _productService: ProductService,
    private _dialog: MatDialog,
    private _dialogRefDetail: MatDialogRef<ProductDetailComponent>,
    private _dialogRefAdd: MatDialogRef<AddProductComponent>
  ) { }

  listHeader?: TableTemplate[];
  listData: ProductModel[] = [];

  ngOnInit(): void {
    this.setHeaderTable();

    setTimeout(() => {
      this.getProductList();
    })
  }

  setHeaderTable(): void {
    this.listHeader = [
      { headerText: 'No', property: 'no', length: 30 },
      { headerText: 'Product Name', property: 'name', length: 150, onClick: this.showDetail.bind(this) },
      { headerText: 'Description', property: 'description', length: 150  },
      { headerText: 'Price', property: 'price', length: 100  },
      { headerText: 'Created Date', property: 'createdDate', length: 150  },
      { headerText: 'Updated Date', property: 'updatedDate', length: 150  },
      { headerText: 'Category Name', property: 'categoryName', length: 150  },
    ];
  }

  getProductList(): void {
    this._productService.getProductList().subscribe(res => {
      switch(res.returnCode) {
        case "success":
          if(res?.data?.products.length > 0) {
            this.listData = res.data.products.map((item, index) => ({...item, no: index + 1, categoryName: item.category?.name}));
          } else {
            this._alert.showNotFound();
          }
          break;
        default:
          this._alert.showError();
          break;    
      }
    });
  }

  addProduct(): void {
    this._dialogRefAdd = this._dialog.open(AddProductComponent, {
      panelClass: ['light-theme'],
      autoFocus: true
    });

    this._dialogRefAdd.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.getProductList();
      }
    });
  }

  showDetail(column: any, data: ProductModel): void {
    this._dialogRefDetail = this._dialog.open(ProductDetailComponent, {
      data: data,
      panelClass: ['light-theme'],
      autoFocus: true
    });

    this._dialogRefDetail.afterClosed().subscribe((result: boolean) => {
      if(result) {
        this.getProductList();
      }
    });
  }

}
