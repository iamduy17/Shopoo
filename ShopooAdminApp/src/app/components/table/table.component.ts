import { DataSource } from '@angular/cdk/table';
import { Component, OnInit, Input, AfterViewInit, ViewChild, ComponentRef, ContentChildren, QueryList, AfterContentInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatColumnDef, MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent implements OnInit, AfterViewInit, AfterContentInit {


  constructor(breakpointObserver: BreakpointObserver) {
    breakpointObserver.observe('(min-width: 1367px)').subscribe((u) => this.canSticky = u.matches);
  }

  Header: TableTemplate[] = [];
  listColumn: TableTemplate[] = [];
  columnsToDisplay: string[] = [];
  tableWidth = 0;
  canSticky = false;

  @Input() set Template(value: TableTemplate[] | undefined) {
    if (value) {
      this.Header = [...value];
      this.setColumn(false);
    }
  }
  @Input() set Data(value: unknown[]) {
    this.paginator?.firstPage();
    setTimeout(() => {
      this.data.data = value;
    }, 50);
  }

  @Input() isShowPaginator: boolean = true;

  data = new MatTableDataSource();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable, { static: true }) table!: MatTable<any>;
  @ContentChildren(MatColumnDef) columnDefs!: QueryList<MatColumnDef>;

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    window.setTimeout(() => {
      if (this.isShowPaginator) {
        this.initPageSize();
      }
    });
  }

  ngAfterContentInit(): void {
    this.columnDefs.forEach(columnDef => this.table.addColumnDef(columnDef));
  }

  initPageSize(): void {
    this.data.paginator = this.paginator;
    this.data.paginator._intl.itemsPerPageLabel = '';
    this.data.paginator._intl.nextPageLabel = 'Tiếp';
    this.data.paginator._intl.previousPageLabel = 'Trước';
    this.data.paginator._intl.firstPageLabel = 'Đầu';
    this.data.paginator._intl.lastPageLabel = 'Cuối';
    this.data.paginator._intl.getRangeLabel = dutchRangeLabel;
    this.data.sort = this.sort;
  }

  formatValue(formatFunc: any, data: any): any {
    return formatFunc && data ? formatFunc(data) : data;
  }

  setColumn(reset?: boolean): void {
    this.tableWidth = 0;
    this.columnsToDisplay = [];
    this.Header.forEach((column) => {
      this.columnsToDisplay.push(column.property);
      this.tableWidth += column.length;
    });
  }

  cellClick(col: TableTemplate, row: any) {
    if (col.onClick) {
      col.onClick(col, row)
    }
  }
  cellStyle(col: TableTemplate, row: any) {
    if (col.onStyle) {
      return col.onStyle(col, row);
    }
    return "";
  }

}

export enum Align {
  left = 0,
  right = 1,
  center = 2
}

export interface TableTemplate {
  headerText: string;
  property: string;
  length: number;
  align?: Align;
  format?: any;
  onClick?: (c: TableTemplate, u: any) => void;
  onStyle?: (c: TableTemplate, u: any) => string;
}

const dutchRangeLabel = (page: number, pageSize: number, length: number) => {
  if (length === 0 || pageSize === 0) { return `0 / ${length}`; }

  length = Math.max(length, 0);

  const startIndex = page * pageSize;

  // If the start index exceeds the list length, do not try and fix the end index to the end.
  const endIndex = startIndex < length ?
    Math.min(startIndex + pageSize, length) :
    startIndex + pageSize;

  return `${startIndex + 1} - ${endIndex} / ${length}`;
}

