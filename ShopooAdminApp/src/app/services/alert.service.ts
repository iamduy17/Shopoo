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
    this.showAlert("No data found!");
  }

  showError(): void{
    this.showAlert("There are something wrong in the system. Please retry later!");
  }
}
