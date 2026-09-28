using System.Text.Json;
using Anthropic.Core;
using Anthropic.Exceptions;
using Anthropic.Models.Beta.Tunnels;

namespace Anthropic.Tests.Models.Beta.Tunnels;

public class BetaTunnelTransportTest : TestBase
{
    [Fact]
    public void CloudflareValidationWorks()
    {
        BetaTunnelTransport value = new BetaCloudflareTunnelTransport();
        value.Validate();
    }

    [Fact]
    public void RelayValidationWorks()
    {
        BetaTunnelTransport value = new BetaRelayTunnelTransport()
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };
        value.Validate();
    }

    [Fact]
    public void CloudflareSerializationRoundtripWorks()
    {
        BetaTunnelTransport value = new BetaCloudflareTunnelTransport();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaTunnelTransport>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void RelaySerializationRoundtripWorks()
    {
        BetaTunnelTransport value = new BetaRelayTunnelTransport()
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaTunnelTransport>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void UnknownVariantCommonProperties_Works()
    {
        BetaTunnelTransport value = new(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "type": "cloudflare"
                }
                """
            )
        );
        Assert.Throws<AnthropicInvalidDataException>(() => value.Validate());

        JsonElement expectedType = JsonSerializer.SerializeToElement("cloudflare");

        Assert.True(JsonElement.DeepEquals(expectedType, value.Type));

        BetaTunnelTransport emptyValue = new(JsonSerializer.Deserialize<JsonElement>("{}"));

        Assert.Throws<AnthropicInvalidDataException>(() => emptyValue.Type);
    }
}
