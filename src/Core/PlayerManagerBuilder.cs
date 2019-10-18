using System.Collections.Immutable;

using static Thermite.Utilities.ThrowHelpers;

namespace Thermite.Core
{
    /// <summary>
    /// A builder for creating instances of <see cref="PlayerManager"/>.
    /// </summary>
    public class PlayerManagerBuilder
    {
        private readonly ulong _userId;
        private readonly ImmutableList<IAudioProviderFactory>.Builder
            _providers;
        private readonly ImmutableList<ITrackSource>.Builder _sources;
        private readonly ImmutableList<IAudioTranscoderFactory>.Builder
            _transcoders;

        private uint _socketCount;

        /// <summary>
        /// Creates a new instance of the <see cref="PlayerManagerBuilder"/>
        /// type.
        /// </summary>
        /// <param name="userId">
        /// The user ID to perform all connections as.
        /// </param>
        public PlayerManagerBuilder(ulong userId)
        {
            _userId = userId;
            _providers = ImmutableList.CreateBuilder<IAudioProviderFactory>();
            _sources = ImmutableList.CreateBuilder<ITrackSource>();
            _transcoders = ImmutableList
                .CreateBuilder<IAudioTranscoderFactory>();

            _socketCount = 20;
        }

        /// <summary>
        /// Overrides the default UDP socket count used for transmitting audio.
        /// </summary>
        /// <param name="socketCount">The number of UDP sockets to use.</param>
        /// <returns><code>this</code></returns>
        public PlayerManagerBuilder WithSocketCount(uint socketCount)
        {
            if (socketCount == 0)
                ThrowArgumentOutOfRangeException(nameof(socketCount));

            _socketCount = socketCount;

            return this;
        }

        /// <summary>
        /// Adds an <see cref="IAudioProviderFactory"/> used for providing
        /// audio data.
        /// </summary>
        /// <param name="provider">The provider to add.</param>
        /// <returns><code>this</code></returns>
        public PlayerManagerBuilder AddProvider(IAudioProviderFactory provider)
        {
            _providers.Add(provider);

            return this;
        }

        /// <summary>
        /// Adds an <see cref="ITrackSource"/> used for providing track
        /// information.
        /// </summary>
        /// <param name="source">The track source to add.</param>
        /// <returns><code>this</code></returns>
        public PlayerManagerBuilder AddSource(ITrackSource source)
        {
            _sources.Add(source);

            return this;
        }

        /// <summary>
        /// Adds an <see cref="IAudioTranscoderFactory"/> used for transcoding
        /// audio data to Thermite-compatible Opus packets.
        /// </summary>
        /// <param name="transcoder">The transcoder to add.</param>
        /// <returns><code>this</code></returns>
        public PlayerManagerBuilder AddTranscoder(
            IAudioTranscoderFactory transcoder)
        {
            _transcoders.Add(transcoder);

            return this;
        }

        /// <summary>
        /// Builds a <see cref="PlayerManager"/> with the configured
        /// properties.
        /// </summary>
        /// <returns>A configured <see cref="PlayerManager"/>.</returns>
        public PlayerManager Build()
        {
            return new PlayerManager(_userId, _socketCount,
                _providers.ToImmutable(),
                _sources.ToImmutable(),
                _transcoders.ToImmutable());
        }
    }
}