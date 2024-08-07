interface IEditorState {
    type(editor: TextEditor, words: string): void;
    select(editor: TextEditor, start: number, end: number): void;
}

// Concrete State: Editing
class EditingState implements IEditorState {
    type(editor: TextEditor, words: string): void {
        editor.content += words;
        console.log("Editing: " + editor.content);
    }

    select(editor: TextEditor, start: number, end: number): void {
        editor.setState(new SelectingState());
        editor.select(start, end);
    }
}

class SelectingState implements IEditorState {
    type(editor: TextEditor, words: string): void {
        console.log("Cannot type in selection mode");
    }

    select(editor: TextEditor, start: number, end: number): void {
        console.log(`Text selected from ${start} to ${end}`);
    }
}

class TextEditor {
    private state: IEditorState;
    public content: string;

    constructor() {
        this.state = new EditingState();
        this.content = "";
    }

    setState(state: IEditorState): void {
        this.state = state;
    }

    type(words: string): void {
        this.state.type(this, words);
    }

    select(start: number, end: number): void {
        this.state.select(this, start, end);
    }
}

const editor = new TextEditor();
editor.type("Hello, ");
editor.type("world!");
editor.select(0, 5);
editor.type("This should not appear.");
