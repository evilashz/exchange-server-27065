using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search
{
	// Token: 0x020000AC RID: 172
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface ITransportFlowSettings : ISettings
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000414 RID: 1044
		bool SkipTokenInfoGeneration { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000415 RID: 1045
		bool SkipMdmGeneration { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000416 RID: 1046
		bool UseMdmFlow { get; }
	}
}
