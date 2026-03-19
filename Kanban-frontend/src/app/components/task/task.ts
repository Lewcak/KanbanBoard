import { Component } from '@angular/core';
import { BoardComponent } from '../board/board';

@Component({
  selector: 'app-task',
  imports: [],
  templateUrl: './task.html',
  styleUrl: './task.css',
})
export class Task {
  boards: BoardComponent[] = [];
}
