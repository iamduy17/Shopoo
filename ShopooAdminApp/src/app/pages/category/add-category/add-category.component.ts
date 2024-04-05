import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AddCategoryRequestModel } from 'src/app/models/category/request/add-category-request';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  constructor(
    private _alert: AlertService,
    private _categoryService: CategoryService,
    private _dialogRef: MatDialogRef<AddCategoryComponent>
  ) { }

  addModel!: AddCategoryRequestModel;

  get isAddDisable(): boolean {
    return !this.addModel.name;
  }

  ngOnInit(): void {
    this.addModel = new AddCategoryRequestModel();
  }

  addCategory(): void {
    const req = this.isValidData(this.addModel);
    if(!req.valid) return;

    this._categoryService.addCategory(req.data).subscribe(res => {
      switch(res.returnCode) {
        case "success":
          this._alert.showAlert("Adding new category successfully");
          this._dialogRef.close(true);
          break;
        default:
          this._alert.showError();
          break;
      }
    })

  }

  isValidData(req: AddCategoryRequestModel): { valid: boolean, data: AddCategoryRequestModel } {
    const request = {...req};

    if(!request.name) {
      this._alert.showAlert("Category Name is required!");

      return { data: request, valid: false};
    }

    return { data: request, valid: true };
  }
}
