using System;
using System.CodeDom.Compiler;

namespace Microsoft.Exchange.Flighting
{
	// Token: 0x02000059 RID: 89
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IFlight
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060001D0 RID: 464
		string Name { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060001D1 RID: 465
		bool Enabled { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060001D2 RID: 466
		string Rotate { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060001D3 RID: 467
		string Ramp { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060001D4 RID: 468
		string RampSeed { get; }
	}
}
