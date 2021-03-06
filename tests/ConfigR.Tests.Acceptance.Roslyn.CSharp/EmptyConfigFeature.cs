﻿// <copyright file="EmptyConfigFeature.cs" company="ConfigR contributors">
//  Copyright (c) ConfigR contributors. (configr.net@gmail.com)
// </copyright>

namespace ConfigR.Tests.Acceptance
{
    using System;
    using ConfigR.Tests.Acceptance.Roslyn.CSharp.Support;
    using FluentAssertions;
    using Xbehave;
    using Xunit;

    public static class EmptyConfigFeature
    {
        [Scenario]
        [Example(null)]
        [Example("")]
        [Example(" ")]
        [Example("\n")]
        [Example("//")]
        [Example("// a comment")]
        public static void EmptyConfig(string code, Exception exception)
        {
            "Given a config file which contains no executable code"
                .f(c => ConfigFile.Create(code).Using(c));

            "When I load the config"
                .f(async () => exception = await Record.ExceptionAsync(async () => await new Config().UseRoslynCSharpLoader().Load()));

            "Then no exception is thrown"
                .f(() => exception.Should().BeNull());
        }
    }
}
