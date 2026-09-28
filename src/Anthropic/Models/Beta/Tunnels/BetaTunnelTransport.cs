using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;

namespace Anthropic.Models.Beta.Tunnels;

/// <summary>
/// How traffic reaches a tunnel: `{"type": "cloudflare"}` or `{"type": "relay"}`.
/// In the create response a `relay` tunnel's transport also carries its relay `token`;
/// reads never carry a token.
/// </summary>
[JsonConverter(typeof(BetaTunnelTransportConverter))]
public record class BetaTunnelTransport : ModelBase
{
    public object? Value { get; } = null;

    JsonElement? _element = null;

    public JsonElement Json
    {
        get
        {
            return this._element ??= JsonSerializer.SerializeToElement(
                this.Value,
                ModelBase.SerializerOptions
            );
        }
    }

    public JsonElement Type
    {
        get
        {
            return this.Value switch
            {
                BetaCloudflareTunnelTransport x => x.Type,
                BetaRelayTunnelTransport x => x.Type,
                _ => WrappedJsonSerializer.GetNotNullStructProperty<JsonElement>(this.Json, "type"),
            };
        }
    }

    public BetaTunnelTransport(BetaCloudflareTunnelTransport value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public BetaTunnelTransport(BetaRelayTunnelTransport value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public BetaTunnelTransport(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="BetaCloudflareTunnelTransport"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCloudflare(out var value)) {
    ///     // `value` is of type `BetaCloudflareTunnelTransport`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCloudflare([NotNullWhen(true)] out BetaCloudflareTunnelTransport? value)
    {
        value = this.Value as BetaCloudflareTunnelTransport;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="BetaRelayTunnelTransport"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickRelay(out var value)) {
    ///     // `value` is of type `BetaRelayTunnelTransport`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickRelay([NotNullWhen(true)] out BetaRelayTunnelTransport? value)
    {
        value = this.Value as BetaRelayTunnelTransport;
        return value != null;
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Match"/>
    /// if you need your function parameters to return something.</para>
    ///
    /// <exception cref="AnthropicInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// instance.Switch(
    ///     (BetaCloudflareTunnelTransport value) =&gt; {...},
    ///     (BetaRelayTunnelTransport value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<BetaCloudflareTunnelTransport> cloudflare,
        Action<BetaRelayTunnelTransport> relay
    )
    {
        switch (this.Value)
        {
            case BetaCloudflareTunnelTransport value:
                cloudflare(value);
                break;
            case BetaRelayTunnelTransport value:
                relay(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of BetaTunnelTransport"
                );
        }
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with and
    /// returns its result.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Switch"/>
    /// if you don't need your function parameters to return a value.</para>
    ///
    /// <exception cref="AnthropicInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// var result = instance.Match(
    ///     (BetaCloudflareTunnelTransport value) =&gt; {...},
    ///     (BetaRelayTunnelTransport value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<BetaCloudflareTunnelTransport, T> cloudflare,
        Func<BetaRelayTunnelTransport, T> relay
    )
    {
        return this.Value switch
        {
            BetaCloudflareTunnelTransport value => cloudflare(value),
            BetaRelayTunnelTransport value => relay(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaTunnelTransport"
            ),
        };
    }

    public static implicit operator BetaTunnelTransport(BetaCloudflareTunnelTransport value) =>
        new(value);

    public static implicit operator BetaTunnelTransport(BetaRelayTunnelTransport value) =>
        new(value);

    /// <summary>
    /// Validates that the instance was constructed with a known variant and that this variant is valid
    /// (based on its own <c>Validate</c> method).
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="AnthropicInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public override void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaTunnelTransport"
            );
        }
        this.Switch((cloudflare) => cloudflare.Validate(), (relay) => relay.Validate());
    }

    public virtual bool Equals(BetaTunnelTransport? other) =>
        other != null
        && this.VariantIndex() == other.VariantIndex()
        && JsonElement.DeepEquals(this.Json, other.Json);

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(this.Json),
            ModelBase.ToStringSerializerOptions
        );

    int VariantIndex()
    {
        return this.Value switch
        {
            BetaCloudflareTunnelTransport _ => 0,
            BetaRelayTunnelTransport _ => 1,
            _ => -1,
        };
    }
}

sealed class BetaTunnelTransportConverter : JsonConverter<BetaTunnelTransport>
{
    public override BetaTunnelTransport? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        string? type;
        try
        {
            type = element.GetProperty("type").GetString();
        }
        catch
        {
            type = null;
        }

        switch (type)
        {
            case "cloudflare":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaCloudflareTunnelTransport>(
                        element,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new(deserialized, element);
                    }
                }
                catch (JsonException)
                {
                    // ignore
                }

                return new(element);
            }
            case "relay":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaRelayTunnelTransport>(
                        element,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new(deserialized, element);
                    }
                }
                catch (JsonException)
                {
                    // ignore
                }

                return new(element);
            }
            default:
            {
                return new BetaTunnelTransport(element);
            }
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        BetaTunnelTransport value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
