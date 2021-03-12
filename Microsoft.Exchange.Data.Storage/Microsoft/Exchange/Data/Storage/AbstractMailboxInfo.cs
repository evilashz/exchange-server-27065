using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002D7 RID: 727
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AbstractMailboxInfo : IMailboxInfo
	{
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x00086352 File Offset: 0x00084552
		public virtual string DisplayName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00086359 File Offset: 0x00084559
		public virtual SmtpAddress PrimarySmtpAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00086360 File Offset: 0x00084560
		public virtual ProxyAddress ExternalEmailAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00086367 File Offset: 0x00084567
		public virtual IEnumerable<ProxyAddress> EmailAddresses
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0008636E File Offset: 0x0008456E
		public virtual OrganizationId OrganizationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00086375 File Offset: 0x00084575
		public virtual Guid MailboxGuid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0008637C File Offset: 0x0008457C
		public virtual ADObjectId MailboxDatabase
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00086383 File Offset: 0x00084583
		public virtual DateTime? WhenMailboxCreated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0008638A File Offset: 0x0008458A
		public virtual string ArchiveName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00086391 File Offset: 0x00084591
		public virtual bool IsArchive
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00086398 File Offset: 0x00084598
		public virtual bool IsAggregated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0008639F File Offset: 0x0008459F
		public virtual ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x000863A6 File Offset: 0x000845A6
		public virtual ArchiveState ArchiveState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000863AD File Offset: 0x000845AD
		public virtual SmtpAddress? RemoteIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000863B4 File Offset: 0x000845B4
		public virtual bool IsRemote
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x000863BB File Offset: 0x000845BB
		public virtual IMailboxLocation Location
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000863C2 File Offset: 0x000845C2
		public virtual IMailboxConfiguration Configuration
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x000863C9 File Offset: 0x000845C9
		public virtual MailboxLocationType MailboxType
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
