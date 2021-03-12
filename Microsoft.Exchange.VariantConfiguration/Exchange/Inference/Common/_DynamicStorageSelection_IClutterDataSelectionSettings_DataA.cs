using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000032 RID: 50
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_ : VariantObjectDataAccessorBase<IClutterDataSelectionSettings, _DynamicStorageSelection_IClutterDataSelectionSettings_Implementation_, _DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_>
	{
		// Token: 0x0400009B RID: 155
		internal string _Name_MaterializedValue_;

		// Token: 0x0400009C RID: 156
		internal int _MaxFolderCount_MaterializedValue_;

		// Token: 0x0400009D RID: 157
		internal ValueProvider<int> _MaxFolderCount_ValueProvider_;

		// Token: 0x0400009E RID: 158
		internal int _BatchSizeForTrainedModel_MaterializedValue_;

		// Token: 0x0400009F RID: 159
		internal ValueProvider<int> _BatchSizeForTrainedModel_ValueProvider_;

		// Token: 0x040000A0 RID: 160
		internal int _BatchSizeForDefaultModel_MaterializedValue_;

		// Token: 0x040000A1 RID: 161
		internal ValueProvider<int> _BatchSizeForDefaultModel_ValueProvider_;

		// Token: 0x040000A2 RID: 162
		internal int _MaxInboxFolderProportion_MaterializedValue_;

		// Token: 0x040000A3 RID: 163
		internal ValueProvider<int> _MaxInboxFolderProportion_ValueProvider_;

		// Token: 0x040000A4 RID: 164
		internal int _MaxDeletedFolderProportion_MaterializedValue_;

		// Token: 0x040000A5 RID: 165
		internal ValueProvider<int> _MaxDeletedFolderProportion_ValueProvider_;

		// Token: 0x040000A6 RID: 166
		internal int _MaxOtherFolderProportion_MaterializedValue_;

		// Token: 0x040000A7 RID: 167
		internal ValueProvider<int> _MaxOtherFolderProportion_ValueProvider_;

		// Token: 0x040000A8 RID: 168
		internal int _MinRespondActionShare_MaterializedValue_;

		// Token: 0x040000A9 RID: 169
		internal ValueProvider<int> _MinRespondActionShare_ValueProvider_;

		// Token: 0x040000AA RID: 170
		internal int _MinIgnoreActionShare_MaterializedValue_;

		// Token: 0x040000AB RID: 171
		internal ValueProvider<int> _MinIgnoreActionShare_ValueProvider_;

		// Token: 0x040000AC RID: 172
		internal int _MaxIgnoreActionShare_MaterializedValue_;

		// Token: 0x040000AD RID: 173
		internal ValueProvider<int> _MaxIgnoreActionShare_ValueProvider_;

		// Token: 0x040000AE RID: 174
		internal int _NumberOfMonthsToIncludeInRetrospectiveTraining_MaterializedValue_;

		// Token: 0x040000AF RID: 175
		internal ValueProvider<int> _NumberOfMonthsToIncludeInRetrospectiveTraining_ValueProvider_;

		// Token: 0x040000B0 RID: 176
		internal int _NumberOfDaysToSkipFromCurrentForTraining_MaterializedValue_;

		// Token: 0x040000B1 RID: 177
		internal ValueProvider<int> _NumberOfDaysToSkipFromCurrentForTraining_ValueProvider_;
	}
}
