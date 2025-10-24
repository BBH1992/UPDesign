public class CommandInfo
{
    public CommandType commandType;
    public TransformInfo transformInfo;
}
public enum CommandType
{
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown,
    TurnLeft,
    TurnRight,
    Bigger,
    Smaller,
    AngularCorrection,
    Copy,
    Delete,
    Translation,
    Rotate,
    Scaling,
    BrushSizeBigger,
    BrushSizeSmaller,
    SelectEraser,
    CancelSelectEraser,
    Undo,
    Redo
}