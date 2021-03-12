using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000071 RID: 113
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface ILanguageDetection : ISettings
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000291 RID: 657
		bool EnableLanguageDetectionLogging { get; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000292 RID: 658
		int SamplingFrequency { get; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000293 RID: 659
		bool EnableLanguageSelection { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000294 RID: 660
		string RegionDefaultLanguage { get; }
	}
}
