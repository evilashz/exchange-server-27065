using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200005E RID: 94
	internal sealed class ElcUserFolderInformation : ElcUserInformation
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00014CEF File Offset: 0x00012EEF
		internal ElcUserFolderInformation(MailboxSession session, List<ELCFolder> allAdFolders) : base(session)
		{
			this.allAdFolders = allAdFolders;
			this.userAdFolders = null;
			this.mailboxFolders = null;
			this.elcRootId = null;
			this.elcRootName = null;
			this.totalMailboxElcFolders = 0;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00014D22 File Offset: 0x00012F22
		internal List<ELCFolder> AllAdFolders
		{
			get
			{
				return this.allAdFolders;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00014D2A File Offset: 0x00012F2A
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00014D32 File Offset: 0x00012F32
		internal List<AdFolderData> UserAdFolders
		{
			get
			{
				return this.userAdFolders;
			}
			set
			{
				this.userAdFolders = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00014D3B File Offset: 0x00012F3B
		internal List<MailboxFolderData> MailboxFolders
		{
			get
			{
				return this.mailboxFolders;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00014D43 File Offset: 0x00012F43
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00014D4B File Offset: 0x00012F4B
		internal StoreObjectId ElcRootId
		{
			get
			{
				return this.elcRootId;
			}
			set
			{
				this.elcRootId = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00014D54 File Offset: 0x00012F54
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00014D5C File Offset: 0x00012F5C
		internal string ElcRootName
		{
			get
			{
				return this.elcRootName;
			}
			set
			{
				this.elcRootName = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00014D65 File Offset: 0x00012F65
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00014D6D File Offset: 0x00012F6D
		internal MailboxFolderData ElcRootFolderData
		{
			get
			{
				return this.elcRootFolderData;
			}
			set
			{
				this.elcRootFolderData = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00014D76 File Offset: 0x00012F76
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00014D7E File Offset: 0x00012F7E
		internal string ElcRootHomePageUrl
		{
			get
			{
				return this.elcRootHomePageUrl;
			}
			set
			{
				this.elcRootHomePageUrl = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00014D87 File Offset: 0x00012F87
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00014D8F File Offset: 0x00012F8F
		internal int TotalMailboxElcFolders
		{
			get
			{
				return this.totalMailboxElcFolders;
			}
			set
			{
				this.totalMailboxElcFolders = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00014D98 File Offset: 0x00012F98
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00014DA0 File Offset: 0x00012FA0
		internal string CurrentFolderName
		{
			get
			{
				return this.currentFolderName;
			}
			set
			{
				this.currentFolderName = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00014DA9 File Offset: 0x00012FA9
		internal VersionedId CurrentFolderId
		{
			get
			{
				return this.currentFolderId;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00014DB4 File Offset: 0x00012FB4
		internal void Build()
		{
			ElcUserInformation.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Looking up ELC folder information for mailbox.", new object[]
			{
				TraceContext.Get()
			});
			this.userAdFolders = AdFolderReader.GetUserElcFolders(base.MailboxSession, base.ADUser, this.allAdFolders, false, false);
			if (this.userAdFolders != null && this.userAdFolders.Count > 0)
			{
				ElcUserInformation.Tracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: has {1} elc folders in the AD.", TraceContext.Get(), this.userAdFolders.Count);
			}
			else
			{
				ElcUserInformation.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: has no elc folders.", new object[]
				{
					TraceContext.Get()
				});
			}
			ProvisionedFolderReader.GetProvisionedFoldersFromMailbox(base.MailboxSession, false, out this.elcRootFolderData, out this.mailboxFolders);
			if (this.mailboxFolders != null && this.mailboxFolders.Count > 0)
			{
				ElcUserInformation.Tracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: Contains {1} elc folders in its mailbox.", TraceContext.Get(), this.mailboxFolders.Count);
			}
			else
			{
				ElcUserInformation.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Has no elc folders in its mailbox.", new object[]
				{
					TraceContext.Get()
				});
			}
			ProvisionedFolderReader.GetElcRootFolderInfo(base.MailboxSession, out this.elcRootId, out this.elcRootName, out this.elcRootHomePageUrl);
			ElcUserInformation.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Completed ELC information for mailbox from AD and Store.", 27287, TraceContext.Get());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00014F24 File Offset: 0x00013124
		internal bool NeedsElcEnforcement()
		{
			if (this.userAdFolders != null && this.userAdFolders.Count > 0)
			{
				return true;
			}
			if (this.mailboxFolders != null && this.mailboxFolders.Count > 0)
			{
				return true;
			}
			if (this.elcRootId != null)
			{
				return true;
			}
			ElcUserInformation.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Doesn't have ELC folders in AD or store no ELC processing needed", 31511, TraceContext.Get());
			return false;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00014F8C File Offset: 0x0001318C
		internal void SetCurrentFolder(string currentFolderName, VersionedId currentFolderId)
		{
			this.currentFolderName = currentFolderName;
			this.currentFolderId = currentFolderId;
		}

		// Token: 0x040002BC RID: 700
		private List<ELCFolder> allAdFolders;

		// Token: 0x040002BD RID: 701
		private List<AdFolderData> userAdFolders;

		// Token: 0x040002BE RID: 702
		private List<MailboxFolderData> mailboxFolders;

		// Token: 0x040002BF RID: 703
		private MailboxFolderData elcRootFolderData;

		// Token: 0x040002C0 RID: 704
		private int totalMailboxElcFolders;

		// Token: 0x040002C1 RID: 705
		private StoreObjectId elcRootId;

		// Token: 0x040002C2 RID: 706
		private string elcRootName;

		// Token: 0x040002C3 RID: 707
		private string elcRootHomePageUrl;

		// Token: 0x040002C4 RID: 708
		private string currentFolderName;

		// Token: 0x040002C5 RID: 709
		private VersionedId currentFolderId;
	}
}
