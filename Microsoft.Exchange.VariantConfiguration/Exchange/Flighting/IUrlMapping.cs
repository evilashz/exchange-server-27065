using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x020000B1 RID: 177
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IUrlMapping : ISettings
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000429 RID: 1065
		string Url { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600042A RID: 1066
		string RemapTo { get; }
	}
}
