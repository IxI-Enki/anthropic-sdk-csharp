using System.Text.Json;
using Anthropic.Core;
using Anthropic.Exceptions;
using Anthropic.Models.Beta.Messages;

namespace Anthropic.Tests.Models.Beta.Messages;

public class BetaThinkingConfigParamTest : TestBase
{
    [Fact]
    public void EnabledValidationWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigEnabled()
        {
            BudgetTokens = 1024,
            BlockBinding = new()
            {
                PrefixMismatchBehavior = BetaThinkingPrefixMismatchBehavior.Error,
            },
            Display = BetaThinkingConfigEnabledDisplay.Summarized,
        };
        value.Validate();
    }

    [Fact]
    public void DisabledValidationWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigDisabled();
        value.Validate();
    }

    [Fact]
    public void BetweenToolsValidationWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigParamBetweenTools();
        value.Validate();
    }

    [Fact]
    public void AdaptiveValidationWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigAdaptive()
        {
            BlockBinding = new()
            {
                PrefixMismatchBehavior = BetaThinkingPrefixMismatchBehavior.Error,
            },
            Display = Display.Summarized,
        };
        value.Validate();
    }

    [Fact]
    public void EnabledSerializationRoundtripWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigEnabled()
        {
            BudgetTokens = 1024,
            BlockBinding = new()
            {
                PrefixMismatchBehavior = BetaThinkingPrefixMismatchBehavior.Error,
            },
            Display = BetaThinkingConfigEnabledDisplay.Summarized,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void DisabledSerializationRoundtripWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigDisabled();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void BetweenToolsSerializationRoundtripWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigParamBetweenTools();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void AdaptiveSerializationRoundtripWorks()
    {
        BetaThinkingConfigParam value = new BetaThinkingConfigAdaptive()
        {
            BlockBinding = new()
            {
                PrefixMismatchBehavior = BetaThinkingPrefixMismatchBehavior.Error,
            },
            Display = Display.Summarized,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParam>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void UnknownVariantCommonProperties_Works()
    {
        BetaThinkingConfigParam value = new(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "type": "enabled",
                  "block_binding": {
                    "prefix_mismatch_behavior": "error"
                  }
                }
                """
            )
        );
        Assert.Throws<AnthropicInvalidDataException>(() => value.Validate());

        JsonElement expectedType = JsonSerializer.SerializeToElement("enabled");
        BetaThinkingBlockBinding expectedBlockBinding = new()
        {
            PrefixMismatchBehavior = BetaThinkingPrefixMismatchBehavior.Error,
        };

        Assert.NotNull(value.Type);
        Assert.True(JsonElement.DeepEquals(expectedType, value.Type.Value));
        Assert.Equal(expectedBlockBinding, value.BlockBinding);

        BetaThinkingConfigParam emptyValue = new(JsonSerializer.Deserialize<JsonElement>("{}"));

        Assert.Null(emptyValue.Type);
        Assert.Null(emptyValue.BlockBinding);

        BetaThinkingConfigParam mismatchedValue = new(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "block_binding": [
                    "invalid"
                  ]
                }
                """
            )
        );

        Assert.Null(mismatchedValue.BlockBinding);
    }
}

public class BetaThinkingConfigParamBetweenToolsTest : TestBase
{
    [Fact]
    public void DefaultValidation_Works()
    {
        var constant = new BetaThinkingConfigParamBetweenTools();
        constant.Validate();
    }

    [Fact]
    public void ValidConstantValidation_Works()
    {
        var constant = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
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
        var constant = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        Assert.Throws<AnthropicInvalidDataException>(() => constant.Validate());
    }

    [Fact]
    public void DefaultRoundtrip_Works()
    {
        var constant = new BetaThinkingConfigParamBetweenTools();
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void ValidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
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
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void InvalidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BetaThinkingConfigParamBetweenTools>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }
}
