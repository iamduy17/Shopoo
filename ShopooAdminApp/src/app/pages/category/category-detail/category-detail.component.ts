import { Component, Inject, OnInit } from '@angular/core';
import { CategoryModel } from 'src/app/models/category/response/category';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';
import { UpdateCategoryRequestModel } from 'src/app/models/category/request/update-category-request';
import { ConfirmDeleteCategoryComponent } from '../confirm-delete-category/confirm-delete-category.component';

@Component({
  selector: 'app-category-detail',
  templateUrl: './category-detail.component.html',
  styleUrls: ['./category-detail.component.css']
})
export class CategoryDetailComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: CategoryModel,
    private _alert: AlertService,
    private _categoryService: CategoryService,
    private _dialog: MatDialog,
    private _dialogRefDetail: MatDialogRef<CategoryDetailComponent>,
    private _dialogRefConfirmDelete: MatDialogRef<ConfirmDeleteCategoryComponent>
  ) { }

  updateModel!: CategoryModel;

  get isUpdateDisable(): boolean {
    return !this.updateModel.name || JSON.stringify(this.updateModel) == JSON.stringify(this.data);
  }

  ngOnInit(): void {
    this.updateModel = JSON.parse(JSON.stringify(this.data));
  }

  updateCategory(): void {
    const req = this.isValidData(this.updateModel);
    if(!req.valid) return;

    const request = new UpdateCategoryRequestModel(req.data.id, req.data);
    this._categoryService.updateCategory(request).subscribe((res) => {
      switch(res.returnCode) {
        case "success":
          this._alert.showAlert("Updating category successfully!");
          this._dialogRefDetail.close(true);
          break;
        default:
          this._alert.showError();
          break;
      }
    });
  }

  isValidData(req: CategoryModel): { data: CategoryModel, valid: boolean } {
    const request = {...req};

    if(!req.name) {
      this._alert.showAlert("Category Name is empty!");
      return { data: request, valid: false};
    }
    
    return { data: request, valid: true };
  }

  confirmDeleteCategory(): void {
    this._dialogRefConfirmDelete = this._dialog.open(ConfirmDeleteCategoryComponent, {
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
