using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;

namespace RPGM.UI
{
    /// <summary>
    /// Sends user input to the correct control systems.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        public float stepSize = 10.5f;
        GameModel model = Schedule.GetModel<GameModel>();

        public enum State
        {
            CharacterControl,
            DialogControl,
            Pause
        }

        State state;

        public void ChangeState(State state) => this.state = state;

        void Update()
        {
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControl();
                    break;
                case State.DialogControl:
                    DialogControl();
                    break;
            }
        }

        void DialogControl()
        {
            model.player.nextMoveCommand = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                model.dialog.FocusButton(-1);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                model.dialog.FocusButton(+1);
            if (Input.GetKeyDown(KeyCode.Space))
                model.dialog.SelectActiveButton();
        }

        void CharacterControl()
        {
            Vector3 dir = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
                dir += Vector3.up;
            if (Input.GetKey(KeyCode.DownArrow))
                dir += Vector3.down;
            if (Input.GetKey(KeyCode.LeftArrow))
                dir += Vector3.left;
            if (Input.GetKey(KeyCode.RightArrow))
                dir += Vector3.right;
                
            dir.Normalize();
            model.player.nextMoveCommand = dir * stepSize;
        }
    }
}