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

using Microsoft.Xna.Framework;

namespace BadEcho.Game;

/// <summary>
/// Defines a single source and management point for input provided by the user.
/// </summary>
public interface IInputHandler
{
    /// <summary>
    /// Gets the last captured position of the mouse by the handler.
    /// </summary>
    Point MousePosition { get; }


}