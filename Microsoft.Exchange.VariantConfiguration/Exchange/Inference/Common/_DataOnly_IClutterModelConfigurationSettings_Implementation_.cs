using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000038 RID: 56
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IClutterModelConfigurationSettings_Implementation_ : IClutterModelConfigurationSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004199 File Offset: 0x00002399
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000419C File Offset: 0x0000239C
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000419F File Offset: 0x0000239F
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000041A7 File Offset: 0x000023A7
		public int MaxModelVersion
		{
			get
			{
				return this._MaxModelVersion_MaterializedValue_;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000041AF File Offset: 0x000023AF
		public int MinModelVersion
		{
			get
			{
				return this._MinModelVersion_MaterializedValue_;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000041B7 File Offset: 0x000023B7
		public int NumberOfVersionCrumbsToRecord
		{
			get
			{
				return this._NumberOfVersionCrumbsToRecord_MaterializedValue_;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000041BF File Offset: 0x000023BF
		public bool AllowTrainingOnMutipleModelVersions
		{
			get
			{
				return this._AllowTrainingOnMutipleModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000041C7 File Offset: 0x000023C7
		public int NumberOfModelVersionToTrain
		{
			get
			{
				return this._NumberOfModelVersionToTrain_MaterializedValue_;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000041CF File Offset: 0x000023CF
		public IList<int> BlockedModelVersions
		{
			get
			{
				return this._BlockedModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000041D7 File Offset: 0x000023D7
		public IList<int> ClassificationModelVersions
		{
			get
			{
				return this._ClassificationModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000041DF File Offset: 0x000023DF
		public IList<int> DeprecatedModelVersions
		{
			get
			{
				return this._DeprecatedModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000041E7 File Offset: 0x000023E7
		public double ProbabilityBehaviourSwitchPerWeek
		{
			get
			{
				return this._ProbabilityBehaviourSwitchPerWeek_MaterializedValue_;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000041EF File Offset: 0x000023EF
		public double SymmetricNoise
		{
			get
			{
				return this._SymmetricNoise_MaterializedValue_;
			}
		}

		// Token: 0x040000D7 RID: 215
		internal string _Name_MaterializedValue_;

		// Token: 0x040000D8 RID: 216
		internal int _MaxModelVersion_MaterializedValue_;

		// Token: 0x040000D9 RID: 217
		internal int _MinModelVersion_MaterializedValue_;

		// Token: 0x040000DA RID: 218
		internal int _NumberOfVersionCrumbsToRecord_MaterializedValue_;

		// Token: 0x040000DB RID: 219
		internal bool _AllowTrainingOnMutipleModelVersions_MaterializedValue_;

		// Token: 0x040000DC RID: 220
		internal int _NumberOfModelVersionToTrain_MaterializedValue_;

		// Token: 0x040000DD RID: 221
		internal IList<int> _BlockedModelVersions_MaterializedValue_;

		// Token: 0x040000DE RID: 222
		internal IList<int> _ClassificationModelVersions_MaterializedValue_;

		// Token: 0x040000DF RID: 223
		internal IList<int> _DeprecatedModelVersions_MaterializedValue_;

		// Token: 0x040000E0 RID: 224
		internal double _ProbabilityBehaviourSwitchPerWeek_MaterializedValue_;

		// Token: 0x040000E1 RID: 225
		internal double _SymmetricNoise_MaterializedValue_;
	}
}
