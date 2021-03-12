using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200013D RID: 317
	public sealed class RecipientOrOrganizationIdParameter
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00024169 File Offset: 0x00022369
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00024171 File Offset: 0x00022371
		public SyncObjectId ResolvedSyncObjectId { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0002417A File Offset: 0x0002237A
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00024182 File Offset: 0x00022382
		public string ResolvedServiceInstanceId { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0002418B File Offset: 0x0002238B
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00024193 File Offset: 0x00022393
		public RecipientIdParameter RecipientParameter { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002419C File Offset: 0x0002239C
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x000241A4 File Offset: 0x000223A4
		public OrganizationIdParameter OrganizationParameter { get; set; }

		// Token: 0x06000B50 RID: 2896 RVA: 0x000241AD File Offset: 0x000223AD
		public RecipientOrOrganizationIdParameter(string identity)
		{
			this.RecipientParameter = new RecipientIdParameter(identity);
			this.OrganizationParameter = new OrganizationIdParameter(identity);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000241CD File Offset: 0x000223CD
		public RecipientOrOrganizationIdParameter(ADObjectId adIdentity)
		{
			this.RecipientParameter = new RecipientIdParameter(adIdentity);
			this.OrganizationParameter = new OrganizationIdParameter(adIdentity);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000241ED File Offset: 0x000223ED
		public RecipientOrOrganizationIdParameter(INamedIdentity namedIdentity)
		{
			this.RecipientParameter = new RecipientIdParameter(namedIdentity);
			this.OrganizationParameter = new OrganizationIdParameter(namedIdentity);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002420D File Offset: 0x0002240D
		public RecipientOrOrganizationIdParameter(ADObject recipient)
		{
			this.RecipientParameter = new RecipientIdParameter(recipient);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00024221 File Offset: 0x00022421
		public RecipientOrOrganizationIdParameter(Microsoft.Exchange.Data.Directory.Management.User user)
		{
			this.RecipientParameter = new UserIdParameter(user);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00024235 File Offset: 0x00022435
		public RecipientOrOrganizationIdParameter(MailUser mailUser)
		{
			this.RecipientParameter = new MailUserIdParameter(mailUser);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00024249 File Offset: 0x00022449
		public RecipientOrOrganizationIdParameter(MailboxEntry storeMailboxEntry)
		{
			this.RecipientParameter = new MailboxIdParameter(storeMailboxEntry);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002425D File Offset: 0x0002245D
		public RecipientOrOrganizationIdParameter(MailboxId storeMailboxId)
		{
			this.RecipientParameter = new MailboxIdParameter(storeMailboxId);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00024271 File Offset: 0x00022471
		public RecipientOrOrganizationIdParameter(Mailbox mailbox)
		{
			this.RecipientParameter = new MailboxIdParameter(mailbox);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00024285 File Offset: 0x00022485
		public RecipientOrOrganizationIdParameter(Microsoft.Exchange.Data.Directory.Management.Contact contact)
		{
			this.RecipientParameter = new ContactIdParameter(contact);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00024299 File Offset: 0x00022499
		public RecipientOrOrganizationIdParameter(MailContact mailContact)
		{
			this.RecipientParameter = new MailContactIdParameter(mailContact);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000242AD File Offset: 0x000224AD
		public RecipientOrOrganizationIdParameter(WindowsGroup group)
		{
			this.RecipientParameter = new GroupIdParameter(group);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000242C1 File Offset: 0x000224C1
		public RecipientOrOrganizationIdParameter(DistributionGroup group)
		{
			this.RecipientParameter = new GroupIdParameter(group);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000242D5 File Offset: 0x000224D5
		public RecipientOrOrganizationIdParameter(TenantOrganizationPresentationObject tenant)
		{
			this.ResolvedSyncObjectId = new SyncObjectId(tenant.ExternalDirectoryOrganizationId, tenant.ExternalDirectoryOrganizationId, DirectoryObjectClass.Company);
			this.ResolvedServiceInstanceId = tenant.DirSyncServiceInstance;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00024301 File Offset: 0x00022501
		public RecipientOrOrganizationIdParameter(OrganizationId organizationId)
		{
			this.OrganizationParameter = new OrganizationIdParameter(organizationId);
		}
	}
}
