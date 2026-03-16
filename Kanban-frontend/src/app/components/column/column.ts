import { Component } from '@angular/core';
import { Task } from '../task/task';

@Component({
  selector: 'app-column',
  imports: [Task],
  templateUrl: './column.html',
  styleUrl: './column.css',
})
export class Column {}
