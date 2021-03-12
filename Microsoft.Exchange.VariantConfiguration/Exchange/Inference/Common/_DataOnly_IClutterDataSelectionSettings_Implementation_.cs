using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000034 RID: 52
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IClutterDataSelectionSettings_Implementation_ : IClutterDataSelectionSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003F04 File Offset: 0x00002104
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003F07 File Offset: 0x00002107
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003F0A File Offset: 0x0000210A
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003F12 File Offset: 0x00002112
		public int MaxFolderCount
		{
			get
			{
				return this._MaxFolderCount_MaterializedValue_;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003F1A File Offset: 0x0000211A
		public int BatchSizeForTrainedModel
		{
			get
			{
				return this._BatchSizeForTrainedModel_MaterializedValue_;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003F22 File Offset: 0x00002122
		public int BatchSizeForDefaultModel
		{
			get
			{
				return this._BatchSizeForDefaultModel_MaterializedValue_;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003F2A File Offset: 0x0000212A
		public int MaxInboxFolderProportion
		{
			get
			{
				return this._MaxInboxFolderProportion_MaterializedValue_;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00003F32 File Offset: 0x00002132
		public int MaxDeletedFolderProportion
		{
			get
			{
				return this._MaxDeletedFolderProportion_MaterializedValue_;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003F3A File Offset: 0x0000213A
		public int MaxOtherFolderProportion
		{
			get
			{
				return this._MaxOtherFolderProportion_MaterializedValue_;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003F42 File Offset: 0x00002142
		public int MinRespondActionShare
		{
			get
			{
				return this._MinRespondActionShare_MaterializedValue_;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003F4A File Offset: 0x0000214A
		public int MinIgnoreActionShare
		{
			get
			{
				return this._MinIgnoreActionShare_MaterializedValue_;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003F52 File Offset: 0x00002152
		public int MaxIgnoreActionShare
		{
			get
			{
				return this._MaxIgnoreActionShare_MaterializedValue_;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003F5A File Offset: 0x0000215A
		public int NumberOfMonthsToIncludeInRetrospectiveTraining
		{
			get
			{
				return this._NumberOfMonthsToIncludeInRetrospectiveTraining_MaterializedValue_;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003F62 File Offset: 0x00002162
		public int NumberOfDaysToSkipFromCurrentForTraining
		{
			get
			{
				return this._NumberOfDaysToSkipFromCurrentForTraining_MaterializedValue_;
			}
		}

		// Token: 0x040000B4 RID: 180
		internal string _Name_MaterializedValue_;

		// Token: 0x040000B5 RID: 181
		internal int _MaxFolderCount_MaterializedValue_;

		// Token: 0x040000B6 RID: 182
		internal int _BatchSizeForTrainedModel_MaterializedValue_;

		// Token: 0x040000B7 RID: 183
		internal int _BatchSizeForDefaultModel_MaterializedValue_;

		// Token: 0x040000B8 RID: 184
		internal int _MaxInboxFolderProportion_MaterializedValue_;

		// Token: 0x040000B9 RID: 185
		internal int _MaxDeletedFolderProportion_MaterializedValue_;

		// Token: 0x040000BA RID: 186
		internal int _MaxOtherFolderProportion_MaterializedValue_;

		// Token: 0x040000BB RID: 187
		internal int _MinRespondActionShare_MaterializedValue_;

		// Token: 0x040000BC RID: 188
		internal int _MinIgnoreActionShare_MaterializedValue_;

		// Token: 0x040000BD RID: 189
		internal int _MaxIgnoreActionShare_MaterializedValue_;

		// Token: 0x040000BE RID: 190
		internal int _NumberOfMonthsToIncludeInRetrospectiveTraining_MaterializedValue_;

		// Token: 0x040000BF RID: 191
		internal int _NumberOfDaysToSkipFromCurrentForTraining_MaterializedValue_;
	}
}
