using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HolidayCalendars
{
	// Token: 0x0200005D RID: 93
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IHostSettings : ISettings
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060001E7 RID: 487
		string Endpoint { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060001E8 RID: 488
		int Timeout { get; }
	}
}
