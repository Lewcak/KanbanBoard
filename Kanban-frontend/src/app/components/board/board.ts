import { Component, OnInit, inject } from '@angular/core';
import { Column } from '../column/column';
import { Kanban } from '../../services/kanban';
import { Board } from '../../models/board.models';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-board',
  imports: [Column, RouterLink],
  templateUrl: './board.html',
  styleUrl: './board.css',
})
export class BoardComponent implements OnInit {
  private kanbanService = inject(Kanban);
  boards: Board[] = [];
  editingBoardId: number | null = null;

  ngOnInit(): void {
    this.kanbanService.getBoards().subscribe(data => this.boards = data);
  }

  createBoard(name: string) {
    const newBoard: Board = {
      id: 0,
      name: name,
      columns: [],
    }
    this.kanbanService.postBoard(newBoard).subscribe(() => {
      this.kanbanService.getBoards().subscribe(data => this.boards = data);
    });
  }

  deleteBoard(id: number) {
    this.kanbanService.deleteBoard(id).subscribe(() => {
      this.kanbanService.getBoards().subscribe(data => this.boards = data);
    });
  }

  saveBoard(id: number, name: string) {
    const editedBoard: Board = {
      id: id,
      name: name,
      columns: []
    }
    this.kanbanService.putBoard(editedBoard, id).subscribe(() => {
      this.kanbanService.getBoards().subscribe(data => this.boards = data);
      this.editingBoardId = null;
    });
  }
  
}

