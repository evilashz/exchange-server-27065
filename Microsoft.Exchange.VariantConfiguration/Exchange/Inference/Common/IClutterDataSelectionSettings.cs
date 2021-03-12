using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000031 RID: 49
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IClutterDataSelectionSettings : ISettings
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000E2 RID: 226
		int MaxFolderCount { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000E3 RID: 227
		int BatchSizeForTrainedModel { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000E4 RID: 228
		int BatchSizeForDefaultModel { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000E5 RID: 229
		int MaxInboxFolderProportion { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060000E6 RID: 230
		int MaxDeletedFolderProportion { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060000E7 RID: 231
		int MaxOtherFolderProportion { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060000E8 RID: 232
		int MinRespondActionShare { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000E9 RID: 233
		int MinIgnoreActionShare { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000EA RID: 234
		int MaxIgnoreActionShare { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000EB RID: 235
		int NumberOfMonthsToIncludeInRetrospectiveTraining { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000EC RID: 236
		int NumberOfDaysToSkipFromCurrentForTraining { get; }
	}
}
