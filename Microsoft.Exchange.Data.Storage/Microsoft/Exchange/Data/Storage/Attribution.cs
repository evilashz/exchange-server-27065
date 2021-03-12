using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200048A RID: 1162
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class Attribution
	{
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000D1B05 File Offset: 0x000CFD05
		// (set) Token: 0x0600338D RID: 13197 RVA: 0x000D1B0D File Offset: 0x000CFD0D
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x0600338E RID: 13198 RVA: 0x000D1B16 File Offset: 0x000CFD16
		// (set) Token: 0x0600338F RID: 13199 RVA: 0x000D1B1E File Offset: 0x000CFD1E
		public AttributionSourceId SourceId
		{
			get
			{
				return this.sourceId;
			}
			set
			{
				this.sourceId = value;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06003390 RID: 13200 RVA: 0x000D1B27 File Offset: 0x000CFD27
		// (set) Token: 0x06003391 RID: 13201 RVA: 0x000D1B2F File Offset: 0x000CFD2F
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000D1B38 File Offset: 0x000CFD38
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000D1B40 File Offset: 0x000CFD40
		public bool IsWritable
		{
			get
			{
				return this.isWritable;
			}
			set
			{
				this.isWritable = value;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06003394 RID: 13204 RVA: 0x000D1B49 File Offset: 0x000CFD49
		// (set) Token: 0x06003395 RID: 13205 RVA: 0x000D1B51 File Offset: 0x000CFD51
		public bool IsQuickContact
		{
			get
			{
				return this.isQuickContact;
			}
			set
			{
				this.isQuickContact = value;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000D1B5A File Offset: 0x000CFD5A
		// (set) Token: 0x06003397 RID: 13207 RVA: 0x000D1B62 File Offset: 0x000CFD62
		public StoreObjectId FolderId { get; set; }

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000D1B6B File Offset: 0x000CFD6B
		// (set) Token: 0x06003399 RID: 13209 RVA: 0x000D1B73 File Offset: 0x000CFD73
		public bool IsHidden { get; set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x000D1B7C File Offset: 0x000CFD7C
		// (set) Token: 0x0600339B RID: 13211 RVA: 0x000D1B84 File Offset: 0x000CFD84
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
			set
			{
				this.folderName = value;
			}
		}

		// Token: 0x04001BCD RID: 7117
		private string id;

		// Token: 0x04001BCE RID: 7118
		private AttributionSourceId sourceId;

		// Token: 0x04001BCF RID: 7119
		private string displayName;

		// Token: 0x04001BD0 RID: 7120
		private bool isWritable;

		// Token: 0x04001BD1 RID: 7121
		private bool isQuickContact;

		// Token: 0x04001BD2 RID: 7122
		private string folderName;
	}
}
