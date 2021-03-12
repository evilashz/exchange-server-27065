using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000626 RID: 1574
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemAuditInfo
	{
		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x001110EC File Offset: 0x0010F2EC
		// (set) Token: 0x060040E3 RID: 16611 RVA: 0x001110F4 File Offset: 0x0010F2F4
		public StoreObjectId Id { get; internal set; }

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x001110FD File Offset: 0x0010F2FD
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x00111105 File Offset: 0x0010F305
		public StoreObjectId ParentFolderId { get; private set; }

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x0011110E File Offset: 0x0010F30E
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x00111116 File Offset: 0x0010F316
		public string ParentFolderPathName { get; internal set; }

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x0011111F File Offset: 0x0010F31F
		// (set) Token: 0x060040E9 RID: 16617 RVA: 0x00111127 File Offset: 0x0010F327
		public string Subject { get; private set; }

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x00111130 File Offset: 0x0010F330
		// (set) Token: 0x060040EB RID: 16619 RVA: 0x00111138 File Offset: 0x0010F338
		public Participant From { get; private set; }

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x00111141 File Offset: 0x0010F341
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x00111149 File Offset: 0x0010F349
		public bool IsAssociated { get; private set; }

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x00111152 File Offset: 0x0010F352
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x0011115A File Offset: 0x0010F35A
		public List<string> DirtyProperties { get; private set; }

		// Token: 0x060040F0 RID: 16624 RVA: 0x00111163 File Offset: 0x0010F363
		public ItemAuditInfo(StoreObjectId itemId, StoreObjectId parentFolderId, string parentFolderPathName, string subject) : this(itemId, parentFolderId, parentFolderPathName, subject, null)
		{
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x00111171 File Offset: 0x0010F371
		public ItemAuditInfo(StoreObjectId itemId, StoreObjectId parentFolderId, string parentFolderPathName, string subject, Participant from) : this(itemId, parentFolderId, parentFolderPathName, subject, from, false, null)
		{
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x00111182 File Offset: 0x0010F382
		public ItemAuditInfo(StoreObjectId itemId, StoreObjectId parentFolderId, string parentFolderPathName, string subject, Participant from, bool isAssociated, List<string> dirtyProperties)
		{
			this.Id = itemId;
			this.ParentFolderId = parentFolderId;
			this.ParentFolderPathName = parentFolderPathName;
			this.Subject = subject;
			this.From = from;
			this.IsAssociated = isAssociated;
			this.DirtyProperties = dirtyProperties;
		}
	}
}
