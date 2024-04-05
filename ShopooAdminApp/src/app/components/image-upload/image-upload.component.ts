import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ImageFile } from 'src/app/models/common/image-file';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent implements OnInit {

  file!: ImageFile; 

  constructor(
    private _dialogRef: MatDialogRef<ImageUploadComponent>
  ) { }

  ngOnInit(): void {
    this.file = { file: undefined, url: "" };
  }

  onDropFile(file: ImageFile): void {
    this.file = {...file};
  }

  removeFile(): void {
    this.file = { file: undefined, url: "" };
  }

  processFile(imageInput: any) { 
    const fileResult: File = imageInput.files[0];

    if(fileResult) {
      const blob = fileResult.slice(0, fileResult.size, fileResult.type);

      const reader = new FileReader();
      reader.readAsDataURL(blob);
      reader.onload = () => {
        let resultFile = { file: fileResult, url: reader.result };
        this.file = resultFile;
      };
    }
  }

  confirmImage(): void {
    this._dialogRef.close({ result: true, fileResult: this.file });
  }

}
