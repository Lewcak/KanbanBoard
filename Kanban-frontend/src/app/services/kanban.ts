import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Board } from '../models/board.models';

@Injectable({
  providedIn: 'root',
})

export class Kanban {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7107/api';

  getBoards() {
  return this.http.get<Board[]>(`${this.apiUrl}/boards`)
  }

  postBoard(board: Board) {
    return this.http.post<Board>(`${this.apiUrl}/boards`, board)
  }

  getBoardId() {
    return this.http.get<Board>(`${this.apiUrl}/boards/{id}`)
  }

}




