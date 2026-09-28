using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;

namespace Anthropic.Models.Beta.Tunnels;

/// <summary>
/// The tunnel is connected through the Cloudflare connector. Its connector token
/// is fetched with reveal_token. `type` is transitional: it reads `relay` for every
/// tunnel once the Cloudflare transport is retired.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<BetaCloudflareTunnelTransport, BetaCloudflareTunnelTransportFromRaw>)
)]
public sealed record class BetaCloudflareTunnelTransport : JsonModel
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

    /// <inheritdoc/>
    public override void Validate()
    {
        if (!JsonElement.DeepEquals(this.Type, JsonSerializer.SerializeToElement("cloudflare")))
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
    }

    public BetaCloudflareTunnelTransport()
    {
        this.Type = JsonSerializer.SerializeToElement("cloudflare");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public BetaCloudflareTunnelTransport(
        BetaCloudflareTunnelTransport betaCloudflareTunnelTransport
    )
        : base(betaCloudflareTunnelTransport) { }
#pragma warning restore CS8618

    public BetaCloudflareTunnelTransport(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Type = JsonSerializer.SerializeToElement("cloudflare");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaCloudflareTunnelTransport(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="BetaCloudflareTunnelTransportFromRaw.FromRawUnchecked"/>
    public static BetaCloudflareTunnelTransport FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaCloudflareTunnelTransportFromRaw : IFromRawJson<BetaCloudflareTunnelTransport>
{
    /// <inheritdoc/>
    public BetaCloudflareTunnelTransport FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaCloudflareTunnelTransport.FromRawUnchecked(rawData);
}
