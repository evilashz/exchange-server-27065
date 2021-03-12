using System;
using System.Globalization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public sealed class RestrictionRow : IConfigurable
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000CF16 File Offset: 0x0000B116
		ObjectId IConfigurable.Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000CF19 File Offset: 0x0000B119
		ValidationError[] IConfigurable.Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000CF21 File Offset: 0x0000B121
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CF24 File Offset: 0x0000B124
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CF28 File Offset: 0x0000B128
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			if (!(source is RestrictionRow))
			{
				throw new NotImplementedException(string.Format("Cannot copy changes from type {0}.", source.GetType()));
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CF55 File Offset: 0x0000B155
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CF57 File Offset: 0x0000B157
		public RestrictionRow()
		{
			this.scopeFolderEntryId = null;
			this.displayName = null;
			this.cultureInfo = null;
			this.contentCount = 0L;
			this.contentUnread = 0L;
			this.viewAccessTime = null;
			this.restriction = null;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CF97 File Offset: 0x0000B197
		public RestrictionRow(MapiEntryId scopeFolderEntryId)
		{
			this.scopeFolderEntryId = scopeFolderEntryId;
			this.displayName = null;
			this.cultureInfo = null;
			this.contentCount = 0L;
			this.contentUnread = 0L;
			this.viewAccessTime = null;
			this.restriction = null;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000CFDF File Offset: 0x0000B1DF
		public MapiEntryId ScopeFolderEntryId
		{
			get
			{
				return this.scopeFolderEntryId;
			}
			internal set
			{
				this.scopeFolderEntryId = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			internal set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000CFF9 File Offset: 0x0000B1F9
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D001 File Offset: 0x0000B201
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
			internal set
			{
				this.cultureInfo = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D00A File Offset: 0x0000B20A
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000D012 File Offset: 0x0000B212
		public long ContentCount
		{
			get
			{
				return this.contentCount;
			}
			internal set
			{
				this.contentCount = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D01B File Offset: 0x0000B21B
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000D023 File Offset: 0x0000B223
		public long ContentUnread
		{
			get
			{
				return this.contentUnread;
			}
			internal set
			{
				this.contentUnread = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D02C File Offset: 0x0000B22C
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000D034 File Offset: 0x0000B234
		public DateTime? ViewAccessTime
		{
			get
			{
				return this.viewAccessTime;
			}
			internal set
			{
				this.viewAccessTime = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000D03D File Offset: 0x0000B23D
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000D045 File Offset: 0x0000B245
		public MapiEntryId MapiEntryID
		{
			get
			{
				return this.restrictionEntryId;
			}
			internal set
			{
				this.restrictionEntryId = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000D050 File Offset: 0x0000B250
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000D073 File Offset: 0x0000B273
		public string Restriction
		{
			get
			{
				string text = this.restriction;
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					text = SuppressingPiiData.Redact(text);
				}
				return text;
			}
			internal set
			{
				this.restriction = value;
			}
		}

		// Token: 0x040000F5 RID: 245
		private MapiEntryId scopeFolderEntryId;

		// Token: 0x040000F6 RID: 246
		private string displayName;

		// Token: 0x040000F7 RID: 247
		private CultureInfo cultureInfo;

		// Token: 0x040000F8 RID: 248
		private long contentCount;

		// Token: 0x040000F9 RID: 249
		private long contentUnread;

		// Token: 0x040000FA RID: 250
		private DateTime? viewAccessTime;

		// Token: 0x040000FB RID: 251
		private MapiEntryId restrictionEntryId;

		// Token: 0x040000FC RID: 252
		private string restriction;
	}
}
