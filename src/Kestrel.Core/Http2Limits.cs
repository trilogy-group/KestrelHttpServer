// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2;

namespace Microsoft.AspNetCore.Server.Kestrel.Core
{
    /// <summary>
    /// Limits only applicable to HTTP/2 connections.
    /// </summary>
    public class Http2Limits
    {
        private int _maxStreamsPerConnection = 100;
        private int _headerTableSize = (int)Http2PeerSettings.DefaultHeaderTableSize;
        private int _maxFrameSize = (int)Http2PeerSettings.DefaultMaxFrameSize;
        private int _initialWindowSize = 1024 * 96; // Larger than the default 64kb

        /// <summary>
        /// Limits the number of concurrent request streams per HTTP/2 connection. Excess streams will be refused.
        /// <para>
        /// Defaults to 100
        /// </para>
        /// </summary>
        public int MaxStreamsPerConnection
        {
            get => _maxStreamsPerConnection;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, CoreStrings.GreaterThanZeroRequired);
                }

                _maxStreamsPerConnection = value;
            }
        }

        /// <summary>
        /// Limits the size of the header compression table, in octets, the HPACK decoder on the server can use.
        /// <para>
        /// Value must be greater than 0, defaults to 4096
        /// </para>
        /// </summary>
        public int HeaderTableSize
        {
            get => _headerTableSize;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, CoreStrings.GreaterThanZeroRequired);
                }

                _headerTableSize = value;
            }
        }

        /// <summary>
        /// Indicates the size of the largest frame payload that is allowed to be received, in octets. The size must be between 2^14 and 2^24-1.
        /// <para>
        /// Value must be between 2^14 and 2^24, defaults to 2^14 (16,384)
        /// </para>
        /// </summary>
        public int MaxFrameSize
        {
            get => _maxFrameSize;
            set
            {
                if (value < Http2PeerSettings.MinAllowedMaxFrameSize || value > Http2PeerSettings.MaxAllowedMaxFrameSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, CoreStrings.FormatArgumentOutOfRange(Http2PeerSettings.MinAllowedMaxFrameSize, Http2PeerSettings.MaxAllowedMaxFrameSize));
                }

                _maxFrameSize = value;
            }
        }

        /// <summary>
        /// Indicates how much request body data the server is willing to receive and buffer at a time per stream and per connection.
        /// <para>
        /// Value must be greater than or equal to 65,535 and less than 2^31, defaults to 96 kb.
        /// </para>
        /// </summary>
        public int InitialWindowSize
        {
            get => _initialWindowSize;
            set
            {
                if (value < Http2PeerSettings.DefaultInitialWindowSize || value > Http2PeerSettings.MaxWindowSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        CoreStrings.FormatArgumentOutOfRange(Http2PeerSettings.DefaultInitialWindowSize, Http2PeerSettings.MaxWindowSize));
                }

                _initialWindowSize = value;
            }
        }
    }
}
