using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E6 RID: 742
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AbstractUserPrincipal : IUserPrincipal, IExchangePrincipal
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0008664C File Offset: 0x0008484C
		public virtual ADObjectId ObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x00086653 File Offset: 0x00084853
		public virtual string UserPrincipalName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x0008665A File Offset: 0x0008485A
		public virtual string LegacyDn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x00086661 File Offset: 0x00084861
		public virtual string Alias
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00086668 File Offset: 0x00084868
		public virtual ADObjectId DefaultPublicFolderMailbox
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x0008666F File Offset: 0x0008486F
		public virtual SecurityIdentifier Sid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00086676 File Offset: 0x00084876
		public virtual SecurityIdentifier MasterAccountSid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0008667D File Offset: 0x0008487D
		public virtual IEnumerable<SecurityIdentifier> SidHistory
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x00086684 File Offset: 0x00084884
		public virtual IEnumerable<ADObjectId> Delegates
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0008668B File Offset: 0x0008488B
		public virtual IEnumerable<CultureInfo> PreferredCultures
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x00086692 File Offset: 0x00084892
		public virtual RecipientType RecipientType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x00086699 File Offset: 0x00084899
		public virtual RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x000866A0 File Offset: 0x000848A0
		public virtual bool? IsResource
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x000866A7 File Offset: 0x000848A7
		public virtual ModernGroupObjectType ModernGroupType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x000866AE File Offset: 0x000848AE
		public virtual IEnumerable<SecurityIdentifier> PublicToGroupSids
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x000866B5 File Offset: 0x000848B5
		public virtual string ExternalDirectoryObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x000866BC File Offset: 0x000848BC
		public virtual IEnumerable<Guid> AggregatedMailboxGuids
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x000866C3 File Offset: 0x000848C3
		public virtual ReleaseTrack? ReleaseTrack
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x000866CA File Offset: 0x000848CA
		public virtual IMailboxInfo MailboxInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x000866D1 File Offset: 0x000848D1
		public virtual IEnumerable<IMailboxInfo> AllMailboxes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x000866D8 File Offset: 0x000848D8
		public virtual bool IsCrossSiteAccessAllowed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x000866DF File Offset: 0x000848DF
		public virtual bool IsMailboxInfoRequired
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000866E6 File Offset: 0x000848E6
		public virtual string PrincipalId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x000866ED File Offset: 0x000848ED
		public virtual SmtpAddress WindowsLiveId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x000866F4 File Offset: 0x000848F4
		public virtual NetID NetId
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
