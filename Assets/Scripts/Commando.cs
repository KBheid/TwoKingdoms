using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commando : MonoBehaviour
{
    public delegate void SendCommand(Command command);
    public static event SendCommand OnSendCommand;
    public static void RaiseSendCommandEvent(Command command) { OnSendCommand?.Invoke(command); }

    public delegate void Undo();
    public static event Undo OnUndo;
    public static void RaiseUndoEvent() { OnUndo?.Invoke(); }

    public delegate void Redo();
    public static event Redo OnRedo;
    public static void RaiseRedoEvent() { OnRedo?.Invoke(); }

    private List<Command> commands;
    private int _lastPlayerCommandInd;

    // Enable for debugging
    private bool _logDebug = false;
    
    // Start is called before the first frame update
    void Start()
    {
        commands = new List<Command>();
        OnSendCommand += RegisterCommand;
        OnUndo += UndoUntilAction;
        OnRedo += RedoUntilAction;
    }

    private void RegisterCommand(Command command)
	{
        if (_logDebug)
        {
            print("ADD");
            printlist();
        }

        Cleanup();
        commands.Add(command);
        _lastPlayerCommandInd = commands.Count - 1;

        if (_logDebug)
            printlist();
    }

    private void UndoUntilAction()
    {
        if (_logDebug)
        {
            print("UNDO");
            printlist();
        }

        if (commands.Count == 0)
            return;

        Command cmd;
        // Undo all actions until player command or last command
        if (_lastPlayerCommandInd > 0)
        {
            cmd = commands[_lastPlayerCommandInd];
            while (!cmd.isPlayerAction && _lastPlayerCommandInd > 0)
            {
                cmd.Undo();
                _lastPlayerCommandInd--;
                cmd = commands[_lastPlayerCommandInd];
            }
        }

        // Undo player command/last command
        if (_lastPlayerCommandInd >= 0)
        {
            cmd = commands[_lastPlayerCommandInd];
            cmd.Undo();
            _lastPlayerCommandInd--;
        }


        if (_logDebug)
        {
            print("END UNDO");
            printlist();
        }
    }

    private void RedoUntilAction()
    {

        if (_logDebug)
        {
            print("REDO");
            printlist();
        }

        if (commands.Count == 0)
            return;

        Command cmd;

        // Undo actions until the next is a player command or the last command
        if (_lastPlayerCommandInd < commands.Count-1) {
            cmd = commands[_lastPlayerCommandInd+1];
            while (!cmd.isPlayerAction && _lastPlayerCommandInd < commands.Count - 1)
            {
                _lastPlayerCommandInd++;
                cmd = commands[_lastPlayerCommandInd];
                cmd.Do(cmd.isPlayerAction);
            }
        }

        // Undo player/last command
        if (_lastPlayerCommandInd <= commands.Count - 2)
        {
            _lastPlayerCommandInd++;
            cmd = commands[_lastPlayerCommandInd];
            cmd.Do(cmd.isPlayerAction);
        }


        if (_logDebug)
        {
            print("END REDO");
            printlist();
        }
    }

	private void Cleanup()
	{
        if (_logDebug)
            print("cleanup: " + _lastPlayerCommandInd + " / " + (commands.Count - 1));

        if (_lastPlayerCommandInd < 0 && commands.Count > 0)
            commands.Clear();

        if (_lastPlayerCommandInd <= commands.Count-2 && _lastPlayerCommandInd >= 0)
            commands.RemoveRange(_lastPlayerCommandInd + 1, commands.Count - 1 - _lastPlayerCommandInd);
    }

    void printlist()
	{
        print("========");
        foreach (Command c in commands)
		{
            if (commands.IndexOf(c) == _lastPlayerCommandInd)
			{
                print("<" + c + "> - " + c.isPlayerAction);
			}
            else
			{
                print(c + " - " + c.isPlayerAction);
			}
        }
        print("========");
    }
}
