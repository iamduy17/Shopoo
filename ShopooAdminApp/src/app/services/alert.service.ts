import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor() { }
  onAlert: EventEmitter<{message: string, timeout: number}> = new EventEmitter();

  showAlert(message: string, timeout: number = 3000): void {
    this.onAlert.emit({message, timeout});
  }

  showNotFound(): void{
    this.showAlert("Không tìm thấy dữ liệu!");
  }

  showError(): void{
    this.showAlert("Có lỗi trong quá trình xử lý!");
  }
}
