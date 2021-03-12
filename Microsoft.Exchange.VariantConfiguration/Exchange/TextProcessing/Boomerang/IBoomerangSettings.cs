using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.TextProcessing.Boomerang
{
	// Token: 0x0200001D RID: 29
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IBoomerangSettings : ISettings
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000086 RID: 134
		bool Enabled { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000087 RID: 135
		string MasterKeyRegistryPath { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000088 RID: 136
		string MasterKeyRegistryKeyName { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000089 RID: 137
		uint NumberOfValidIntervalsInDays { get; }
	}
}
