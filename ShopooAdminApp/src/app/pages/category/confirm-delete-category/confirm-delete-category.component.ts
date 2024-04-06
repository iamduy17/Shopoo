import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoryModel } from 'src/app/models/category/response/category';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-confirm-delete-category',
  templateUrl: './confirm-delete-category.component.html',
  styleUrls: ['./confirm-delete-category.component.css']
})
export class ConfirmDeleteCategoryComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: CategoryModel,
    private _alert: AlertService,
    private _categoryService: CategoryService,
    private _dialogRef: MatDialogRef<ConfirmDeleteCategoryComponent>
  ) { }

  ngOnInit(): void {
  }

  deleteCategory(): void {
    this._categoryService.deleteCategory(this.data.id).subscribe(res => {
      switch(res.returnCode) {
        case "success":
          this._alert.showAlert(`Delete category ${this.data.name} successfully!`);
          this._dialogRef.close(true);
          break;
        default:
          this._alert.showError();
          break;
      }
    });
  }

}
