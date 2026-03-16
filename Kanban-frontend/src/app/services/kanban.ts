import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Board , Column , Task , MoveTaskRequest } from '../models/board.models';

@Injectable({
  providedIn: 'root',
})

export class Kanban {
  private http = inject(HttpClient);
  private apiUrl = 'https://localhost:7107/api';


  // Board Service Methods  

  // Get All Boards
  getBoards() {
  return this.http.get<Board[]>(`${this.apiUrl}/Boards`)
  }

  // Create a new Board
  postBoard(board: Board) {
    return this.http.post<Board>(`${this.apiUrl}/Boards`, board)
  }

  // Get a board by ID
  getBoard(id: number) {
    return this.http.get<Board>(`${this.apiUrl}/Boards/${id}`)
  }

  // Edit a Board
  putBoard(board: Board, id: number) {
    return this.http.put<Board>(`${this.apiUrl}/Boards/${id}`, board)
  }

  // Delete a board
  deleteBoard(id: number) {
    return this.http.delete<Board>(`${this.apiUrl}/Boards/${id}`)
  }


  // Column Service Methods





}




