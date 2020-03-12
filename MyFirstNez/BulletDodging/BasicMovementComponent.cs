using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class BasicMovementComponent : Component, IUpdatable
    {
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        Mover _mover;
        float _moveSpeed = 200f;

        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;

        public override void OnAddedToEntity()
        {
            _mover = Entity.AddComponent(new Mover());
            SetupInput();
        }

        void SetupInput()
        {
            // horizontal input from dpad, left stick or keyboard left/right
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));

            // vertical input from dpad, left stick or keyboard up/down
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));
        }
        void IUpdatable.Update()
        {
            // handle movement
            var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);

            if (moveDir != Vector2.Zero)
            {
                var movement = moveDir * _moveSpeed * Time.DeltaTime;

                _mover.CalculateMovement(ref movement, out var res);
                _subpixelV2.Update(ref movement);
                _mover.ApplyMovement(movement);
            }
        }
    }
}
