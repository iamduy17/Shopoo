import { Directive, EventEmitter, HostBinding, HostListener, Output } from '@angular/core';
import { ImageFile } from '../models/common/image-file';

enum DropColor {
  Default = '#C6E4F1', // Default color
  Over = '#ACADAD', // Color to be used once the file is "over" the drop box
}

@Directive({
  selector: '[appImageUploader]'
})
export class ImageUploaderDirective {

  @Output() dropFile: EventEmitter<ImageFile> = new EventEmitter<ImageFile>();
  @HostBinding('style.background') backgroundColor = DropColor.Default;

  constructor() { }

  @HostListener('dragover', ['$event']) public dragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.backgroundColor = DropColor.Default;
  }

  @HostListener('dragleave', ['$event']) public dragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    this.backgroundColor = DropColor.Over;
  }

  @HostListener('drop', ['$event']) public drop(event: DragEvent) {
    event.preventDefault();
    event.preventDefault();
    this.backgroundColor = DropColor.Default;

    let file = event.dataTransfer?.files[0];
    if(file) {
      const blob = file.slice(0, file.size, file.type);

      const reader = new FileReader();
      reader.readAsDataURL(blob);
      reader.onload = () => {
        let resultFile = { file, url: reader.result };
        this.dropFile.emit(resultFile);
      };
    }
  }

}
