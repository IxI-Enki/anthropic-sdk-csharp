using System.Text.Json;
using Anthropic.Core;
using Anthropic.Models.Beta.Tunnels;

namespace Anthropic.Tests.Models.Beta.Tunnels;

public class BetaRelayTunnelTransportTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };

        JsonElement expectedType = JsonSerializer.SerializeToElement("relay");
        BetaTunnelToken expectedToken = new() { ID = "id", TunnelToken = "tunnel_token" };

        Assert.True(JsonElement.DeepEquals(expectedType, model.Type));
        Assert.Equal(expectedToken, model.Token);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaRelayTunnelTransport>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaRelayTunnelTransport>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        JsonElement expectedType = JsonSerializer.SerializeToElement("relay");
        BetaTunnelToken expectedToken = new() { ID = "id", TunnelToken = "tunnel_token" };

        Assert.True(JsonElement.DeepEquals(expectedType, deserialized.Type));
        Assert.Equal(expectedToken, deserialized.Token);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new BetaRelayTunnelTransport { };

        Assert.Null(model.Token);
        Assert.False(model.RawData.ContainsKey("token"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new BetaRelayTunnelTransport { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            // Null should be interpreted as omitted for these properties
            Token = null,
        };

        Assert.Null(model.Token);
        Assert.False(model.RawData.ContainsKey("token"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            // Null should be interpreted as omitted for these properties
            Token = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new BetaRelayTunnelTransport
        {
            Token = new() { ID = "id", TunnelToken = "tunnel_token" },
        };

        BetaRelayTunnelTransport copied = new(model);

        Assert.Equal(model, copied);
    }
}
