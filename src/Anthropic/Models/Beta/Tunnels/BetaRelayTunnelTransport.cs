using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;

namespace Anthropic.Models.Beta.Tunnels;

/// <summary>
/// The tunnel is connected through Anthropic's relay. In the create response `token`
/// is the tunnel's relay token, shown that once (only a hash is kept, so reveal_token
/// refuses a relay tunnel and rotate_token issues a new one); reads never carry it.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<BetaRelayTunnelTransport, BetaRelayTunnelTransportFromRaw>)
)]
public sealed record class BetaRelayTunnelTransport : JsonModel
{
    public JsonElement Type
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("type");
        }
        init { this._rawData.Set("type", value); }
    }

    /// <summary>
    /// The tunnel's relay token. Present only in the create response, which issues
    /// it; absent on every read. Store it: Anthropic keeps only a hash, reveal_token
    /// refuses a relay tunnel, and rotate_token is the only way to obtain a new one.
    /// </summary>
    public BetaTunnelToken? Token
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<BetaTunnelToken>("token");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("token", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        if (!JsonElement.DeepEquals(this.Type, JsonSerializer.SerializeToElement("relay")))
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
        this.Token?.Validate();
    }

    public BetaRelayTunnelTransport()
    {
        this.Type = JsonSerializer.SerializeToElement("relay");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public BetaRelayTunnelTransport(BetaRelayTunnelTransport betaRelayTunnelTransport)
        : base(betaRelayTunnelTransport) { }
#pragma warning restore CS8618

    public BetaRelayTunnelTransport(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Type = JsonSerializer.SerializeToElement("relay");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaRelayTunnelTransport(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="BetaRelayTunnelTransportFromRaw.FromRawUnchecked"/>
    public static BetaRelayTunnelTransport FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaRelayTunnelTransportFromRaw : IFromRawJson<BetaRelayTunnelTransport>
{
    /// <inheritdoc/>
    public BetaRelayTunnelTransport FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaRelayTunnelTransport.FromRawUnchecked(rawData);
}
