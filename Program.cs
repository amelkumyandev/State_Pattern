using System;

public interface IEditorState
{
    void Type(TextEditor editor, string words);
    void Select(TextEditor editor, int start, int end);
}

// Concrete State: Editing
public class EditingState : IEditorState
{
    public void Type(TextEditor editor, string words)
    {
        editor.Content += words;
        Console.WriteLine("Editing: " + editor.Content);
    }

    public void Select(TextEditor editor, int start, int end)
    {
        editor.SetState(new SelectingState());
        editor.Select(start, end);
    }
}

// Concrete State: Selecting
public class SelectingState : IEditorState
{
    public void Type(TextEditor editor, string words)
    {
        Console.WriteLine("Cannot type in selection mode");
    }

    public void Select(TextEditor editor, int start, int end)
    {
        Console.WriteLine($"Text selected from {start} to {end}");
    }
}

// Context
public class TextEditor
{
    private IEditorState _state;
    public string Content { get; set; }

    public TextEditor()
    {
        _state = new EditingState();
    }

    public void SetState(IEditorState state)
    {
        _state = state;
    }

    public void Type(string words)
    {
        _state.Type(this, words);
    }

    public void Select(int start, int end)
    {
        _state.Select(this, start, end);
    }
}

// Usage
public class Program
{
    public static void Main()
    {
        TextEditor editor = new TextEditor();
        editor.Type("Hello, ");
        editor.Type("world!");
        editor.Select(0, 5);
        editor.Type("This should not appear.");
    }
}
