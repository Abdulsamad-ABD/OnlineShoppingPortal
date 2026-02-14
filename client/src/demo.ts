type Todo = {
    id: number
    title: string
    completed: boolean
}

let todos : Todo[] = [];

function addTodo(title: string) : Todo {
    const newtodo: Todo = {
        id:todos.length+1,
        title,
        completed:false
    }
    todos.push(newtodo);
    return newtodo;
}

function toggleTodo(id: number): void{
    const todo = todos.find(todo => todo.id === id );
    if(todo){
        todo.completed = !todo.completed
    }
}

addTodo("Build API")
addTodo("Publish API")
toggleTodo(2)

console.log(todos);