using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000049 RID: 73
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IDocumentFeederSettings : ISettings
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000196 RID: 406
		TimeSpan BatchTimeout { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000197 RID: 407
		TimeSpan LostCallbackTimeout { get; }
	}
}
