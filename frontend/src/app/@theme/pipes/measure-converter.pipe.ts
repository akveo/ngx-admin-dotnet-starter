import {Pipe, PipeTransform} from '@angular/core';

@Pipe({ name: 'ngxMeasureConverter' })
export class MeasureConverterPipe implements PipeTransform {

  transform(input: number | string): string {
    if (typeof input === 'string') {
      return input;
    }

    if (input === 0) {
      return '0 Byte';
    }

    const sizes: string[] = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    const degree: number = Math.floor(Math.log(input) / Math.log(1024));

    return (input / Math.pow(1024, degree)).toFixed(2) + ' ' + sizes[degree];
  }
}
