using System;
using System.Composition;
using System.Net.Http;
using Thermite.Providers.Http;

namespace Thermite.Providers
{
    /// <summary>
    /// A provider factory for retrieving audio files from HTTP(S) locations.
    /// </summary>
    [Export(typeof(IAudioProviderFactory))]
    public sealed class HttpProviderFactory : IAudioProviderFactory
    {
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Creates a new instance of the <see cref="HttpProviderFactory"/>
        /// type.
        /// </summary>
        /// <param name="clientFactory">
        /// The <see cref="IHttpClientFactory"/> used for creating instances of
        /// <see cref="HttpClient"/>.
        /// </param>
        public HttpProviderFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <inheritdoc/>
        public bool IsSupported(Uri location)
        {
            return location.Scheme == "http"
                || location.Scheme == "https";
        }

        /// <inheritdoc/>
        public IAudioProvider GetProvider(Uri location)
            => new HttpAudioProvider(_clientFactory, location);
    }
}
