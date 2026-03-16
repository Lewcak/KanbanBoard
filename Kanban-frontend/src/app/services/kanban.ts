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

  // Create a column 
  postColumn(column: Column) {
    return this.http.post<Column>(`${this.apiUrl}/Columns`, column)
  }

  // Get column by ID
  getColumn(id: number) {
    return this.http.get<Column>(`${this.apiUrl}/Columns/${id}`)
  }

  // Edit Column by ID
  putColumn(column: Column, id: number) {
    return this.http.put<Column>(`${this.apiUrl}/Columns/${id}`, column)
  }

  // Delete Column by ID
  deleteColumn(id: number) {
    return this.http.delete<Column>(`${this.apiUrl}/Columns/${id}`)
  }


  // Task Service Methods

  

}




