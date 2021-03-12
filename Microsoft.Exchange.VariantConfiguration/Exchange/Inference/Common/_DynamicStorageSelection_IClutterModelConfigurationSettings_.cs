using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000036 RID: 54
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_ : VariantObjectDataAccessorBase<IClutterModelConfigurationSettings, _DynamicStorageSelection_IClutterModelConfigurationSettings_Implementation_, _DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_>
	{
		// Token: 0x040000C0 RID: 192
		internal string _Name_MaterializedValue_;

		// Token: 0x040000C1 RID: 193
		internal int _MaxModelVersion_MaterializedValue_;

		// Token: 0x040000C2 RID: 194
		internal ValueProvider<int> _MaxModelVersion_ValueProvider_;

		// Token: 0x040000C3 RID: 195
		internal int _MinModelVersion_MaterializedValue_;

		// Token: 0x040000C4 RID: 196
		internal ValueProvider<int> _MinModelVersion_ValueProvider_;

		// Token: 0x040000C5 RID: 197
		internal int _NumberOfVersionCrumbsToRecord_MaterializedValue_;

		// Token: 0x040000C6 RID: 198
		internal ValueProvider<int> _NumberOfVersionCrumbsToRecord_ValueProvider_;

		// Token: 0x040000C7 RID: 199
		internal bool _AllowTrainingOnMutipleModelVersions_MaterializedValue_;

		// Token: 0x040000C8 RID: 200
		internal ValueProvider<bool> _AllowTrainingOnMutipleModelVersions_ValueProvider_;

		// Token: 0x040000C9 RID: 201
		internal int _NumberOfModelVersionToTrain_MaterializedValue_;

		// Token: 0x040000CA RID: 202
		internal ValueProvider<int> _NumberOfModelVersionToTrain_ValueProvider_;

		// Token: 0x040000CB RID: 203
		internal IList<int> _BlockedModelVersions_MaterializedValue_;

		// Token: 0x040000CC RID: 204
		internal ValueProvider<IList<int>> _BlockedModelVersions_ValueProvider_;

		// Token: 0x040000CD RID: 205
		internal IList<int> _ClassificationModelVersions_MaterializedValue_;

		// Token: 0x040000CE RID: 206
		internal ValueProvider<IList<int>> _ClassificationModelVersions_ValueProvider_;

		// Token: 0x040000CF RID: 207
		internal IList<int> _DeprecatedModelVersions_MaterializedValue_;

		// Token: 0x040000D0 RID: 208
		internal ValueProvider<IList<int>> _DeprecatedModelVersions_ValueProvider_;

		// Token: 0x040000D1 RID: 209
		internal double _ProbabilityBehaviourSwitchPerWeek_MaterializedValue_;

		// Token: 0x040000D2 RID: 210
		internal ValueProvider<double> _ProbabilityBehaviourSwitchPerWeek_ValueProvider_;

		// Token: 0x040000D3 RID: 211
		internal double _SymmetricNoise_MaterializedValue_;

		// Token: 0x040000D4 RID: 212
		internal ValueProvider<double> _SymmetricNoise_ValueProvider_;
	}
}
