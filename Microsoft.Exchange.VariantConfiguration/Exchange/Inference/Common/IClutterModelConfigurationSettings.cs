using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000035 RID: 53
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IClutterModelConfigurationSettings : ISettings
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600010D RID: 269
		int MaxModelVersion { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600010E RID: 270
		int MinModelVersion { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600010F RID: 271
		int NumberOfVersionCrumbsToRecord { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000110 RID: 272
		bool AllowTrainingOnMutipleModelVersions { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000111 RID: 273
		int NumberOfModelVersionToTrain { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000112 RID: 274
		IList<int> BlockedModelVersions { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000113 RID: 275
		IList<int> ClassificationModelVersions { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000114 RID: 276
		IList<int> DeprecatedModelVersions { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000115 RID: 277
		double ProbabilityBehaviourSwitchPerWeek { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000116 RID: 278
		double SymmetricNoise { get; }
	}
}
