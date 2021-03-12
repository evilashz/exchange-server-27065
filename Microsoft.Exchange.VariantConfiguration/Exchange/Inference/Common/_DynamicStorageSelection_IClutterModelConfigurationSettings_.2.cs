using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000037 RID: 55
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IClutterModelConfigurationSettings_Implementation_ : IClutterModelConfigurationSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003F7A File Offset: 0x0000217A
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003F82 File Offset: 0x00002182
		_DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003F8A File Offset: 0x0000218A
		void IDataAccessorBackedObject<_DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003F9A File Offset: 0x0000219A
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003FA7 File Offset: 0x000021A7
		public int MaxModelVersion
		{
			get
			{
				if (this.dataAccessor._MaxModelVersion_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxModelVersion_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxModelVersion_MaterializedValue_;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003FD8 File Offset: 0x000021D8
		public int MinModelVersion
		{
			get
			{
				if (this.dataAccessor._MinModelVersion_ValueProvider_ != null)
				{
					return this.dataAccessor._MinModelVersion_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinModelVersion_MaterializedValue_;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004009 File Offset: 0x00002209
		public int NumberOfVersionCrumbsToRecord
		{
			get
			{
				if (this.dataAccessor._NumberOfVersionCrumbsToRecord_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfVersionCrumbsToRecord_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfVersionCrumbsToRecord_MaterializedValue_;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000403A File Offset: 0x0000223A
		public bool AllowTrainingOnMutipleModelVersions
		{
			get
			{
				if (this.dataAccessor._AllowTrainingOnMutipleModelVersions_ValueProvider_ != null)
				{
					return this.dataAccessor._AllowTrainingOnMutipleModelVersions_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._AllowTrainingOnMutipleModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000406B File Offset: 0x0000226B
		public int NumberOfModelVersionToTrain
		{
			get
			{
				if (this.dataAccessor._NumberOfModelVersionToTrain_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfModelVersionToTrain_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfModelVersionToTrain_MaterializedValue_;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000409C File Offset: 0x0000229C
		public IList<int> BlockedModelVersions
		{
			get
			{
				if (this.dataAccessor._BlockedModelVersions_ValueProvider_ != null)
				{
					return this.dataAccessor._BlockedModelVersions_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BlockedModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000040CD File Offset: 0x000022CD
		public IList<int> ClassificationModelVersions
		{
			get
			{
				if (this.dataAccessor._ClassificationModelVersions_ValueProvider_ != null)
				{
					return this.dataAccessor._ClassificationModelVersions_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ClassificationModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000040FE File Offset: 0x000022FE
		public IList<int> DeprecatedModelVersions
		{
			get
			{
				if (this.dataAccessor._DeprecatedModelVersions_ValueProvider_ != null)
				{
					return this.dataAccessor._DeprecatedModelVersions_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DeprecatedModelVersions_MaterializedValue_;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000412F File Offset: 0x0000232F
		public double ProbabilityBehaviourSwitchPerWeek
		{
			get
			{
				if (this.dataAccessor._ProbabilityBehaviourSwitchPerWeek_ValueProvider_ != null)
				{
					return this.dataAccessor._ProbabilityBehaviourSwitchPerWeek_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ProbabilityBehaviourSwitchPerWeek_MaterializedValue_;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004160 File Offset: 0x00002360
		public double SymmetricNoise
		{
			get
			{
				if (this.dataAccessor._SymmetricNoise_ValueProvider_ != null)
				{
					return this.dataAccessor._SymmetricNoise_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SymmetricNoise_MaterializedValue_;
			}
		}

		// Token: 0x040000D5 RID: 213
		private _DynamicStorageSelection_IClutterModelConfigurationSettings_DataAccessor_ dataAccessor;

		// Token: 0x040000D6 RID: 214
		private VariantContextSnapshot context;
	}
}
