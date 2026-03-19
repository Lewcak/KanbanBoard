import { Routes } from '@angular/router';
import { BoardComponent } from './components/board/board';
import { BoardDetail } from './components/board-detail/board-detail';

export const routes: Routes = [
    { path: 'boards', component: BoardComponent},
    { path: 'boards/:id', component: BoardDetail },
    { path: '', redirectTo: 'boards', pathMatch: 'full' }
];
