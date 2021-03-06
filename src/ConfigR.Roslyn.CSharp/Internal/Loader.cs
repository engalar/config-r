﻿// <copyright file="Loader.cs" company="ConfigR contributors">
//  Copyright (c) ConfigR contributors. (configr.net@gmail.com)
// </copyright>

namespace ConfigR.Roslyn.CSharp.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ConfigR.Roslyn.CSharp.Logging;
    using ConfigR.Sdk;
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;
    using Microsoft.CodeAnalysis.Scripting.Hosting;

    public class Loader : ILoader
    {
        private static readonly ILog log = LogProvider.GetCurrentClassLogger();

        private readonly string scriptPath;
        private readonly ScriptOptions options;
        private readonly InteractiveAssemblyLoader assemblyLoader;

        [CLSCompliant(false)]
        public Loader(string scriptPath, ScriptOptions options, InteractiveAssemblyLoader assemblyLoader)
        {
            this.scriptPath = scriptPath;
            this.options = options;
            this.assemblyLoader = assemblyLoader;
        }

        public async Task<DynamicDictionary> Load(DynamicDictionary config)
        {
            log.InfoFormat("Running script '{0}'...", this.scriptPath);

            await CSharpScript.Create(
                    File.ReadAllText(this.scriptPath), this.options, typeof(ScriptGlobals), this.assemblyLoader)
                .RunAsync(new ScriptGlobals(config));

            return config;
        }
    }
}
