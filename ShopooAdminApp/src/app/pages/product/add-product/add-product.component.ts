import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ImageUploadComponent } from 'src/app/components/image-upload/image-upload.component';
import { ImageFileResult } from 'src/app/models/common/image-file-result';
import { SelectItemList } from 'src/app/models/common/select-item-list';
import { AddProductRequestModel } from 'src/app/models/product/request/add-product-request';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  imageFile!: File | undefined;
  addModel!: AddProductRequestModel;
  listCategory!: SelectItemList[];

  get isAddDisable(): boolean {
    return !this.addModel.name || !this.addModel.price || !this.addModel.categoryId || !this.addModel.imageURL;
  }

  constructor(
    private _alert: AlertService,
    private _categoryService: CategoryService,
    private _productService: ProductService,
    private _dialog: MatDialog,
    private _dialogRefAdd: MatDialogRef<AddProductComponent>,
    private _dialogRefImageUpload: MatDialogRef<ImageUploadComponent>
  ) { }

  ngOnInit(): void {
    this.addModel = new AddProductRequestModel();
    setTimeout(() => {
      this.getCategoryList();
    })
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
        console.log(result.fileResult.url);
        this.addModel.imageURL = result.fileResult.url as string;
        this.imageFile = result.fileResult.file;
      }
    })
  }

  addProduct(): void {
    const req = this.isValidData(this.addModel);
    if(!req.valid) return;

    this._productService.addProduct(req.data).subscribe(res => {
      switch(res.returnCode) {
        case "success":
          this._alert.showAlert("Adding new product successfully");
          this._dialogRefAdd.close(true);
          break;
        default:
          this._alert.showError();
          break;
      }
    });
  }

  isValidData(req: AddProductRequestModel): { valid: boolean, data: AddProductRequestModel } {
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

}
