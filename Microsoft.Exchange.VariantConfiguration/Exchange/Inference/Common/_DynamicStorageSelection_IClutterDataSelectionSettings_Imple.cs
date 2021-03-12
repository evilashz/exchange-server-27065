using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000033 RID: 51
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IClutterDataSelectionSettings_Implementation_ : IClutterDataSelectionSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003CB4 File Offset: 0x00001EB4
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003CBC File Offset: 0x00001EBC
		_DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003CC4 File Offset: 0x00001EC4
		void IDataAccessorBackedObject<_DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public int MaxFolderCount
		{
			get
			{
				if (this.dataAccessor._MaxFolderCount_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxFolderCount_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxFolderCount_MaterializedValue_;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003D12 File Offset: 0x00001F12
		public int BatchSizeForTrainedModel
		{
			get
			{
				if (this.dataAccessor._BatchSizeForTrainedModel_ValueProvider_ != null)
				{
					return this.dataAccessor._BatchSizeForTrainedModel_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BatchSizeForTrainedModel_MaterializedValue_;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003D43 File Offset: 0x00001F43
		public int BatchSizeForDefaultModel
		{
			get
			{
				if (this.dataAccessor._BatchSizeForDefaultModel_ValueProvider_ != null)
				{
					return this.dataAccessor._BatchSizeForDefaultModel_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BatchSizeForDefaultModel_MaterializedValue_;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003D74 File Offset: 0x00001F74
		public int MaxInboxFolderProportion
		{
			get
			{
				if (this.dataAccessor._MaxInboxFolderProportion_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxInboxFolderProportion_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxInboxFolderProportion_MaterializedValue_;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public int MaxDeletedFolderProportion
		{
			get
			{
				if (this.dataAccessor._MaxDeletedFolderProportion_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxDeletedFolderProportion_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxDeletedFolderProportion_MaterializedValue_;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003DD6 File Offset: 0x00001FD6
		public int MaxOtherFolderProportion
		{
			get
			{
				if (this.dataAccessor._MaxOtherFolderProportion_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxOtherFolderProportion_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxOtherFolderProportion_MaterializedValue_;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003E07 File Offset: 0x00002007
		public int MinRespondActionShare
		{
			get
			{
				if (this.dataAccessor._MinRespondActionShare_ValueProvider_ != null)
				{
					return this.dataAccessor._MinRespondActionShare_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinRespondActionShare_MaterializedValue_;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003E38 File Offset: 0x00002038
		public int MinIgnoreActionShare
		{
			get
			{
				if (this.dataAccessor._MinIgnoreActionShare_ValueProvider_ != null)
				{
					return this.dataAccessor._MinIgnoreActionShare_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinIgnoreActionShare_MaterializedValue_;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003E69 File Offset: 0x00002069
		public int MaxIgnoreActionShare
		{
			get
			{
				if (this.dataAccessor._MaxIgnoreActionShare_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxIgnoreActionShare_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxIgnoreActionShare_MaterializedValue_;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003E9A File Offset: 0x0000209A
		public int NumberOfMonthsToIncludeInRetrospectiveTraining
		{
			get
			{
				if (this.dataAccessor._NumberOfMonthsToIncludeInRetrospectiveTraining_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfMonthsToIncludeInRetrospectiveTraining_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfMonthsToIncludeInRetrospectiveTraining_MaterializedValue_;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003ECB File Offset: 0x000020CB
		public int NumberOfDaysToSkipFromCurrentForTraining
		{
			get
			{
				if (this.dataAccessor._NumberOfDaysToSkipFromCurrentForTraining_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfDaysToSkipFromCurrentForTraining_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfDaysToSkipFromCurrentForTraining_MaterializedValue_;
			}
		}

		// Token: 0x040000B2 RID: 178
		private _DynamicStorageSelection_IClutterDataSelectionSettings_DataAccessor_ dataAccessor;

		// Token: 0x040000B3 RID: 179
		private VariantContextSnapshot context;
	}
}
