
export interface Board {
    id: number;
    name: string;
    columns: Column[];
}

export interface Column {
    id: number;
    name: string;
    order: number;
    tasks: Task[];
    boardId: number;
}

export interface Task {
    id: number;
    title: string;
    description: string | null;
    columnId: number;
}

export interface MoveTaskRequest {
    taskId: number;
    newColumnId: number;
    newOrderIndex: number;
}

