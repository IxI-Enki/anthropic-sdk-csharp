using System.Text.Json;
using Anthropic.Core;
using Anthropic.Exceptions;
using Anthropic.Models.Messages;

namespace Anthropic.Tests.Models.Messages;

public class ThinkingConfigParamTest : TestBase
{
    [Fact]
    public void EnabledValidationWorks()
    {
        ThinkingConfigParam value = new ThinkingConfigEnabled()
        {
            BudgetTokens = 1024,
            Display = ThinkingConfigEnabledDisplay.Summarized,
        };
        value.Validate();
    }

    [Fact]
    public void DisabledValidationWorks()
    {
        ThinkingConfigParam value = new ThinkingConfigDisabled();
        value.Validate();
    }

    [Fact]
    public void BetweenToolsValidationWorks()
    {
        ThinkingConfigParam value = new BetweenTools();
        value.Validate();
    }

    [Fact]
    public void AdaptiveValidationWorks()
    {
        ThinkingConfigParam value = new ThinkingConfigAdaptive() { Display = Display.Summarized };
        value.Validate();
    }

    [Fact]
    public void EnabledSerializationRoundtripWorks()
    {
        ThinkingConfigParam value = new ThinkingConfigEnabled()
        {
            BudgetTokens = 1024,
            Display = ThinkingConfigEnabledDisplay.Summarized,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void DisabledSerializationRoundtripWorks()
    {
        ThinkingConfigParam value = new ThinkingConfigDisabled();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void BetweenToolsSerializationRoundtripWorks()
    {
        ThinkingConfigParam value = new BetweenTools();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void AdaptiveSerializationRoundtripWorks()
    {
        ThinkingConfigParam value = new ThinkingConfigAdaptive() { Display = Display.Summarized };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void UnknownVariantCommonProperties_Works()
    {
        ThinkingConfigParam value = new(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "type": "enabled"
                }
                """
            )
        );
        Assert.Throws<AnthropicInvalidDataException>(() => value.Validate());

        JsonElement expectedType = JsonSerializer.SerializeToElement("enabled");

        Assert.NotNull(value.Type);
        Assert.True(JsonElement.DeepEquals(expectedType, value.Type.Value));

        ThinkingConfigParam emptyValue = new(JsonSerializer.Deserialize<JsonElement>("{}"));

        Assert.Null(emptyValue.Type);
    }
}

public class BetweenToolsTest : TestBase
{
    [Fact]
    public void DefaultValidation_Works()
    {
        var constant = new BetweenTools();
        constant.Validate();
    }

    [Fact]
    public void ValidConstantValidation_Works()
    {
        var constant = JsonSerializer.Deserialize<BetweenTools>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "type": "between_tools"
                }
                """
            ),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        constant.Validate();
    }

    [Fact]
    public void InvalidConstantValidationThrows_Works()
    {
        var constant = JsonSerializer.Deserialize<BetweenTools>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        Assert.Throws<AnthropicInvalidDataException>(() => constant.Validate());
    }

    [Fact]
    public void DefaultRoundtrip_Works()
    {
        var constant = new BetweenTools();
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetweenTools>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void ValidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<BetweenTools>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "type": "between_tools"
                }
                """
            ),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetweenTools>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void InvalidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<BetweenTools>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetweenTools>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }
}
