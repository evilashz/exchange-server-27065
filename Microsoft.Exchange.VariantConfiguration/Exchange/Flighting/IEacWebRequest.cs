using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x0200004D RID: 77
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IEacWebRequest : ISettings
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060001A6 RID: 422
		string Request { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060001A7 RID: 423
		bool Enabled { get; }
	}
}
