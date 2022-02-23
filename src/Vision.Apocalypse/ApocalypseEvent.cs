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

namespace BadEcho.Vision.Apocalypse;

/// <summary>
/// Provides a base description for an event generated by the Apocalypse system in an Omnified game.
/// </summary>
/// <remarks>
/// <para>
/// An Apocalypse event describes an action undertaken by the Apocalypse system in response to damage being done either to the
/// player, or to an enemy by the player. Different types of events exist for each of the various random effects the Apocalypse
/// system can apply to entities receiving damage.
/// </para>
/// <para>
/// All of the core Apocalypse events have a timestamp and are convertible into a recognizable effect message. These messages,
/// displayed to the player to describe what the hell is happening in their game, as well show all the other auxiliary pieces of
/// data specific to the event, are fleshed out in the particular event type that corresponds to the applied random effect.
/// </para>
/// </remarks>
public abstract class ApocalypseEvent
{
    private readonly Lazy<WeightedRandom<Func<Stream>>> _soundMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApocalypseEvent"/> class.
    /// </summary>
    protected ApocalypseEvent() =>
        _soundMap = new Lazy<WeightedRandom<Func<Stream>>>(InitializeSoundMap, LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets or sets the index of this event, which serves as a reference to its location among other Apocalypse events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The reason why we sort Apocalypse events by an independently managed index, as opposed to the events' timestamps, is because
    /// it was discovered that Lua, which the technology used to conduct high-level operations within injected Omnified code (including the
    /// creation of Apocalypse message files), lacks built-in support for capturing the current time with millisecond resolution.
    /// </para>
    /// <para>
    /// Given that it is often the case that multiple Apocalypse events can occur within the same second, relying on a timestamp that
    /// is only precise up to the second would easily result in events being ordered inaccurately.
    /// </para>
    /// </remarks>
    public int Index
    { get; set; }

    /// <summary>
    /// Gets the date and time at which this Apocalypse event occurred.
    /// </summary>
    public DateTime Timestamp  
    { get; init; }

    /// <summary>
    /// Gets the raw data for the sound effect to play, if one is to be played, announcing the event's occurrence.
    /// </summary>
    public IEnumerable<byte> EffectSound
    {
        get
        {
            Func<Stream>? soundAccessor = SoundMap.Next();

            if (soundAccessor == null)
                return Enumerable.Empty<byte>();

            using (var sound = soundAccessor())
            {
                return sound.ToArray();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating if the event's <see cref="EffectSound"/> is exempt from having its playback interrupted upon 
    /// another event (with a sound effect) occurring.
    /// </summary>
    public virtual bool IsEffectSoundUninterruptible
        => false;

    private WeightedRandom<Func<Stream>> SoundMap
        => _soundMap.Value;

    /// <summary>
    /// Creates weighted random value mappings for sound effects that might play upon the event occurring.
    /// </summary>
    /// <returns>
    /// A <see cref="WeightedRandom{T}"/> instance mapping each sound effect for the event to the weighted probability that said
    /// sound effect might occur.
    /// </returns>
    /// <remarks>By default, an Apocalypse event has no sound effect played announcing its occurrence.</remarks>
    protected virtual WeightedRandom<Func<Stream>> InitializeSoundMap()
        => new();
}