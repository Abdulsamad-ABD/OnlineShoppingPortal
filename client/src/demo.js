var todos = [];
function addTodo(title) {
    var newtodo = {
        id: todos.length + 1,
        title: title,
        completed: false
    };
    todos.push(newtodo);
    return newtodo;
}
function toggleTodo(id) {
    var todo = todos.find(function (todo) { return todo.id === id; });
    if (todo) {
        todo.completed = !todo.completed;
    }
}
addTodo("Build API");
addTodo("Publish API");
toggleTodo(2);
console.log(todos);
