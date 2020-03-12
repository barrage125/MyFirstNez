using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstNez
{
    class Player : Component, IUpdatable
    {
        SpriteAnimator _animator;

        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        Mover _mover;
        float _moveSpeed = 100f;
        FollowCamera _camera;

        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;
        VirtualButton _kickInput;
        VirtualButton _crouchInput;

        enum animation
        {
            Walk,
            Damage,
            Crouch,
            Transition,
            Idle,
            Kick
        }

        public override void OnAddedToEntity()
        {
            // load up our character texture.
            var texture = Entity.Scene.Content.Load<Texture2D>("Textures/DinoSprites - tard");
            var sprites = Sprite.SpritesFromAtlas(texture, 24, 24);

            //Entity.AddComponent<SpriteRenderer>(new SpriteRenderer(texture));
            Entity.SetLocalScale(new Vector2(2.0f, 2.0f));
            
            _mover = Entity.AddComponent(new Mover());
            _camera = Entity.AddComponent(new FollowCamera());
            _animator = Entity.AddComponent<SpriteAnimator>();
            _animator.SetRenderLayer(-20);

            _animator.AddAnimation("Walk", new[]
            {
                sprites[4],
                sprites[5],
                sprites[6],
                sprites[7],
                sprites[8],
                sprites[9]
            });
            _animator.AddAnimation("Damage", new[]
            {
                sprites[14],
                sprites[15],
                sprites[16]
            });
            _animator.AddAnimation("Crouch", new[]
            {
                sprites[18],
                sprites[19],
                sprites[20],
                sprites[21],
                sprites[22],
                sprites[23]
            });
            _animator.AddAnimation("Transition", new[]
            {
                sprites[17]
            });
            _animator.AddAnimation("Idle", new[]
            {
                sprites[0],
                sprites[1],
                sprites[2],
                sprites[3],
            });
            _animator.AddAnimation("Kick", new[]
            {
                sprites[10],
                sprites[11],
                sprites[12],
                sprites[13]
            });
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

            // kick input from gamepad A press, or keyboard space
            _kickInput = new VirtualButton();
            _kickInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Space));
            _kickInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.A));

            // crouch input from gamepad B press, or keyboard shift
            _crouchInput = new VirtualButton();
            _crouchInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.LeftShift));
            _crouchInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.B));
        }
        void IUpdatable.Update()
        {
            if (Entity.GetComponent<CircleCollider>() != null) {
                var collider = Entity.GetComponent<CircleCollider>();
                //if (collider.CollidesWithAny()
            }
            // handle movement and animations
            var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            var animation = "Idle";
            if (moveDir.X < 0)
            {
                if (_crouchInput)
                    if (!_animator.CurrentAnimationName.Equals("Transition") && !_animator.CurrentAnimationName.Equals("Crouch"))
                        animation = "Transition";
                    else
                        animation = "Crouch";
                else
                    animation = "Walk";
                _animator.FlipX = true;
            }
            else if (moveDir.X > 0)
            {
                if (_crouchInput)
                    if (!_animator.CurrentAnimationName.Equals("Transition") && !_animator.CurrentAnimationName.Equals("Crouch"))
                        animation = "Transition";
                    else
                        animation = "Crouch";
                else
                    animation = "Walk";
                _animator.FlipX = false;
            }

            if (_kickInput)
            {
                moveDir = Vector2.Zero;
                animation = "Kick";
                if (!_animator.IsAnimationActive(animation))
                    _animator.Play(animation);
                else
                    _animator.UnPause();
            }

            else if (moveDir != Vector2.Zero)
            {
                if (!_animator.IsAnimationActive(animation))
                    _animator.Play(animation);
                else
                    _animator.UnPause();

                if (_crouchInput)
                    moveDir *= 3;
                var movement = moveDir * _moveSpeed * Time.DeltaTime;

                _mover.CalculateMovement(ref movement, out var res);
                _subpixelV2.Update(ref movement);
                _mover.ApplyMovement(movement);
            }
            else
            {
                if (!_animator.IsAnimationActive(animation))
                    _animator.Play(animation);
                else
                    _animator.UnPause();
            }
        }
    }
}
