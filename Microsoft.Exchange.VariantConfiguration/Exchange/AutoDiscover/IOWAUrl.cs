using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.AutoDiscover
{
	// Token: 0x02000095 RID: 149
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IOWAUrl : ISettings
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600039B RID: 923
		string OwaInternalAuthMethods { get; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600039C RID: 924
		string OwaExternalAuthMethods { get; }
	}
}
