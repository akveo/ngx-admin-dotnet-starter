/*
 * Copyright (c) Akveo 2019. All Rights Reserved.
 * Licensed under the Single Application / Multi Application License.
 * See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the 'docs' folder for license information on type of purchased license.
 */

import { Component, Input } from '@angular/core';
import { NbSortDirection, NbSortRequest, NbTreeGridDataSource, NbTreeGridDataSourceBuilder } from '@nebular/theme';
import {MeasureConverterPipe} from '../../../@theme/pipes';

interface TreeNode<T> {
  data: T;
  children?: TreeNode<T>[];
  expanded?: boolean;
}

interface FSEntry {
  name: string;
  size: number;
  kind: string;
  items?: number;
}

@Component({
  selector: 'ngx-tree-grid',
  templateUrl: './tree-grid.component.html',
  styleUrls: ['./tree-grid.component.scss'],
})
export class TreeGridComponent {
  customColumn = 'name';
  defaultColumns = [ 'size', 'kind', 'items' ];
  allColumns = [ this.customColumn, ...this.defaultColumns ];

  dataSource: NbTreeGridDataSource<FSEntry>;

  sortColumn: string;
  sortDirection: NbSortDirection = NbSortDirection.NONE;

  constructor(private dataSourceBuilder: NbTreeGridDataSourceBuilder<FSEntry>,
              public measureConverterPipe: MeasureConverterPipe) {
    this.dataSource = this.dataSourceBuilder.create(this.data);
  }

  updateSort(sortRequest: NbSortRequest): void {
    this.sortColumn = sortRequest.column;
    this.sortDirection = sortRequest.direction;
  }

  getSortDirection(column: string): NbSortDirection {
    if (this.sortColumn === column) {
      return this.sortDirection;
    }
    return NbSortDirection.NONE;
  }

  private data: TreeNode<FSEntry>[] = [
    {
      data: { name: 'Projects', size: 1800000, items: 4, kind: 'dir' },
      children: [
        { data: { name: 'project-1.doc', kind: 'doc', size: 240000 } },
        { data: { name: 'project-2.doc', kind: 'doc', size: 290000 } },
        { data: { name: 'project-3', kind: 'txt', size: 466000 } },
        { data: { name: 'project-4.docx', kind: 'docx', size: 900000 } },
      ],
    },
    {
      data: { name: 'Reports', kind: 'dir', size: 400000, items: 2 },
      children: [
        { data: { name: 'Report 1', kind: 'doc', size: 100000 } },
        { data: { name: 'Report 2', kind: 'doc', size: 300000 } },
      ],
    },
    {
      data: { name: 'Other', kind: 'dir', size: 109000000, items: 2 },
      children: [
        { data: { name: 'backup.bkp', kind: 'bkp', size: 107000000 } },
        { data: { name: 'secret-note.txt', kind: 'txt', size: 2000000 } },
      ],
    },
  ];

  getCellValue(columnValue: string | number, columnName: string): string | number {
    if (columnName === 'size') {
      return this.measureConverterPipe.transform(columnValue);
    }
    return columnValue || '-';
  }

  getShowOn(index: number) {
    const minWithForMultipleColumns = 400;
    const nextColumnStep = 100;
    return minWithForMultipleColumns + (nextColumnStep * index);
  }
}

@Component({
  selector: 'ngx-fs-icon',
  template: `
    <nb-tree-grid-row-toggle [expanded]="expanded" *ngIf="isDir(); else fileIcon">
    </nb-tree-grid-row-toggle>
    <ng-template #fileIcon>
      <nb-icon icon="file-text-outline"></nb-icon>
    </ng-template>
  `,
})
export class FsIconComponent {
  @Input() kind: string;
  @Input() expanded: boolean;

  isDir(): boolean {
    return this.kind === 'dir';
  }
}
