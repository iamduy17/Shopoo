import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  constructor() { }
  private loadingIds: string[] = [];
  public isShowLoadingSub = new BehaviorSubject<boolean>(false);

  get isShowLoading(): boolean {
    return this.loadingIds.length > 0;
  }

  private getLoadingId(): string {
    return Math.random().toString(36).substring(10);
  }

  pushLoading(): string {
    const id = this.getLoadingId();
    this.loadingIds.push(id);
    this.isShowLoadingSub.next(true);
    return id;
  }

  popLoading(id: string): void {
    this.loadingIds = this.loadingIds.filter(u => u !== id);
    this.isShowLoadingSub.next(false);
  }
}
