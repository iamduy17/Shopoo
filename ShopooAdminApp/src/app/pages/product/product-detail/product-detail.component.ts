import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';
import { UpdateProductRequestModel } from 'src/app/models/product/request/update-product-request';
import { ProductModel } from 'src/app/models/product/response/product';
import { ProductService } from 'src/app/services/product.service';
import { ConfirmDeleteProductComponent } from '../confirm-delete-product/confirm-delete-product.component';
import { SelectItemList } from 'src/app/models/common/select-item-list';
import { ImageUploadComponent } from 'src/app/components/image-upload/image-upload.component';
import { ImageFileResult } from 'src/app/models/common/image-file-result';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  updateModel!: ProductModel;
  listCategory!: SelectItemList[];
  imageFile!: File | undefined;

  get isUpdateDisable(): boolean {
    return !this.updateModel.name || !this.updateModel.price || !this.updateModel.categoryId || !this.updateModel.imageURL
    || JSON.stringify(this.updateModel) == JSON.stringify(this.data);
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ProductModel,
    private _alert: AlertService,
    private _categoryService: CategoryService,
    private _productService: ProductService,
    private _dialog: MatDialog,
    private _dialogRefDetail: MatDialogRef<ProductDetailComponent>,
    private _dialogRefImageUpload: MatDialogRef<ImageUploadComponent>,
    private _dialogRefConfirmDelete: MatDialogRef<ConfirmDeleteProductComponent>
  ) { }

  ngOnInit(): void {
    this.updateModel = JSON.parse(JSON.stringify(this.data));
    setTimeout(() => {
      this.getCategoryList();
    });
  }

  getCategoryList(): void {
    this._categoryService.getCategoryList().subscribe(res => {
      switch(res.returnCode) {
        case "success":
          if(res?.data?.categories.length > 0) {
            this.listCategory = res.data.categories.map((item, index) => new SelectItemList(item.name ?? "", index, item.id));
          }
          break;
        default:
          this._alert.showAlert("Categories are not found!");
          break;    
      }
    })
  }

  openImageUploader(): void {
    this._dialogRefImageUpload = this._dialog.open(ImageUploadComponent, {
      panelClass: ['light-theme'],
      autoFocus: true
    });

    this._dialogRefImageUpload.afterClosed().subscribe((result: ImageFileResult) => {
      if(result.result) {
        this.updateModel.imageURL = result.fileResult.url as string;
        this.imageFile = result.fileResult.file;
      }
    })
  }

  updateProduct(): void {
    const req = this.isValidData(this.updateModel);
    if(!req.valid) return;

    const request = new UpdateProductRequestModel(req.data.id, req.data);
    this._productService.updateProduct(request).subscribe((res) => {
      switch(res.returnCode) {
        case "success":
          this._alert.showAlert("Updating product successfully!");
          this._dialogRefDetail.close(true);
          break;
        default:
          this._alert.showError();
          break;
      }
    });
  }

  isValidData(req: ProductModel): { valid: boolean, data: ProductModel } {
    const request = {...req};

    if(!request.name) {
      this._alert.showAlert("Product Name is required!");
      return { data: request, valid: false};
    }

    if(!request.price || request.price == 0) {
      this._alert.showAlert("Price is required!");
      return { data: request, valid: false};
    }

    if(!request.categoryId) {
      this._alert.showAlert("Category is required!");
      return { data: request, valid: false};
    }

    if(!request.imageURL) {
      this._alert.showAlert("Image is required!");
      return { data: request, valid: false};
    }

    return { data: request, valid: true };
  }

  confirmDeleteProduct(): void {
    this._dialogRefConfirmDelete = this._dialog.open(ConfirmDeleteProductComponent, {
      data: this.data,
      panelClass: ['light-theme'],
      autoFocus: true
    });

    this._dialogRefConfirmDelete.afterClosed().subscribe((res: boolean) => {
      if(res) {
        this._dialogRefDetail.close(true);
      }
    });
  }
}
