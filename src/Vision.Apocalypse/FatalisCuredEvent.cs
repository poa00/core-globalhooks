﻿//-----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2022 Bad Echo LLC. All rights reserved.
//
//		Bad Echo Technologies are licensed under a
//		Creative Commons Attribution-NonCommercial 4.0 International License.
//
//		See accompanying file LICENSE.md or a copy at:
//		http://creativecommons.org/licenses/by-nc/4.0/
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using BadEcho.Extensions;
using BadEcho.Vision.Apocalypse.Properties;

namespace BadEcho.Vision.Apocalypse;

/// <summary>
/// Provides a description for the event generated by the Apocalypse system upon the player becoming cured
/// of a Fatalis debuff they were previously afflicted with in an Omnified game.
/// </summary>
/// <remarks>
/// This Apocalypse event does not trigger when a player or enemy is hit, but rather when the Fatalis timer
/// expires.
/// </remarks>
public sealed class FatalisCuredEvent : ApocalypseEvent
{
    /// <summary>
    /// Gets the number of times that the player died due their Fatalis affliction.
    /// </summary>
    public int Deaths
    { get; init; }

    /// <summary>
    /// Gets the number of minutes that player was afflicted with Fatalis.
    /// </summary>
    public int MinutesAfflicted
    { get; init; }

    /// <inheritdoc/>
    public override string ToString()
        => EffectMessages.FatalisCured.CulturedFormat(MinutesAfflicted, Deaths);

    /// <inheritdoc/>
    protected override WeightedRandom<Func<Stream>> InitializeSoundMap()
    {
        var soundMap =  base.InitializeSoundMap();

        soundMap.AddWeight(() => EffectSounds.FatalisCured, 1);

        return soundMap;
    }
}