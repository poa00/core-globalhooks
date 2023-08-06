﻿//-----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2023 Bad Echo LLC. All rights reserved.
//
//      Bad Echo Technologies are licensed under the
//      GNU Affero General Public License v3.0.
//
//      See accompanying file LICENSE.md or a copy at:
//      https://www.gnu.org/licenses/agpl-3.0.html
// </copyright>
//-----------------------------------------------------------------------

using BadEcho.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BadEcho.Game.States;

/// <summary>
/// Provides a game state that hosts a rendering surface for drawing user interface elements.
/// </summary>
public sealed class ScreenState : GameState
{
    private readonly UserInterface _userInterface;
    private readonly GraphicsDevice _device;
    private readonly Screen _screen;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenState"/> class.
    /// </summary>
    /// <param name="userInterface">The user interface the state will display.</param>
    /// <param name="device">The graphics device that will power the rendering surface.</param>
    public ScreenState(UserInterface userInterface, GraphicsDevice device)
    {
        Require.NotNull(userInterface, nameof(userInterface));
        Require.NotNull(device, nameof(device));

        _userInterface = userInterface;
        _device = device;
        _screen = new Screen(device);
        ActivationTime = TimeSpan.FromSeconds(1.5);
    }

    /// <inheritdoc/>
    public override void Update(GameUpdateTime time)
    {
        _screen.Update();

        int contentX = _device.Viewport.Width - _screen.Content.LayoutBounds.X + _screen.Origin.X;
        int originX = contentX - (int) (Math.Pow(ActivationPercentage, 3) * contentX);

        _screen.Origin = new Point(originX, 0);

        base.Update(time);
    }

    /// <inheritdoc />
    protected override void DrawCore(SpriteBatch spriteBatch) 
        
        => _screen.Draw(spriteBatch);

    /// <inheritdoc />
    protected override void LoadContent(ContentManager contentManager)
    {
        _userInterface.Attach(_screen, contentManager);
    }
}
