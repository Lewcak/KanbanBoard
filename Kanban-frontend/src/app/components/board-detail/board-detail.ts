import { Component, inject, OnInit } from '@angular/core';
import { Board } from '../../models/board.models';
import { ActivatedRoute } from '@angular/router';
import { Kanban } from '../../services/kanban';
import { Task } from '../../models/board.models';
import { Column } from '../../models/board.models';

@Component({
  selector: 'app-board-detail',
  imports: [],
  templateUrl: './board-detail.html',
  styleUrl: './board-detail.css',
})
export class BoardDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private kanbanService = inject(Kanban);

  boardId: number = 0;
  board: Board | null = null

  addingColumn: Boolean = false;
  editingColumnId: number | null = null;

  addTaskToColumnId: number | null = null;
  editingTaskId: number | null = null;

  ngOnInit(): void {
    this.boardId = Number(this.route.snapshot.paramMap.get('id'));

    this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
  }

  // Tasks  

  createTask(name: string) {
    
    const newTask: Task = {
      id: 0,
      title: name,
      description: null,
      order: 0,
      columnId: this.addTaskToColumnId!
    }

    this.kanbanService.postTask(newTask).subscribe(() => {
      this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
      this.addTaskToColumnId = null;
    })
  }

  deleteTask(id: number) {
    this.kanbanService.deleteTask(id).subscribe(() => {
      this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
    })
  }

  editTask(task: Task, newTitle: string ) {

    const  editedTask: Task = {
      id: task.id,
      title: newTitle, 
      description: task.description,
      order: task.order,
      columnId: task.columnId
    }

    this.kanbanService.putTask(editedTask, task.id).subscribe(() => {
      this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
      this.editingTaskId = null
    })
  }
  

  // Columns 

  createColumn(name: string) {
    const newColumn: Column = {
      id: 0,
      name: name,
      order: 0,
      tasks: [],
      boardId: this.boardId
    }

    this.kanbanService.postColumn(newColumn).subscribe(() => {
      this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
      this.addingColumn = false;
    })
  }

  editColumn(column: Column, newName: string) {
    const editedColumn: Column = {
      id: column.id,
      name: newName,
      order: column.order,
      tasks: column.tasks,
      boardId: column.boardId
    }

    this.kanbanService.putColumn(editedColumn, column.id).subscribe(() => {
      this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
      this.editingColumnId = null
    })
  }

  deleteColumn(id: number) {
    this.kanbanService.deleteColumn(id).subscribe(() => {
      this.kanbanService.getBoard(this.boardId).subscribe(data => this.board = data);
    })
  }

}
