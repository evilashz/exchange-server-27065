using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x0200048F RID: 1167
	[Cmdlet("Get", "PublicFolderStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderStatistics : GetTenantADObjectWithIdentityTaskBase<PublicFolderIdParameter, PublicFolderStatistics>
	{
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000A3C60 File Offset: 0x000A1E60
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x000A3C68 File Offset: 0x000A1E68
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override PublicFolderIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x000A3C71 File Offset: 0x000A1E71
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x000A3C88 File Offset: 0x000A1E88
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["MailboxInformation"];
			}
			set
			{
				base.Fields["MailboxInformation"] = value;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000A3C9B File Offset: 0x000A1E9B
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x000A3CB2 File Offset: 0x000A1EB2
		[Parameter]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x000A3CC5 File Offset: 0x000A1EC5
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x000A3CCD File Offset: 0x000A1ECD
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000A3CD6 File Offset: 0x000A1ED6
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return new Unlimited<uint>(10000U);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000A3CE2 File Offset: 0x000A1EE2
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity != null)
				{
					return this.Identity.PublicFolderId;
				}
				return null;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000A3CF9 File Offset: 0x000A1EF9
		protected override bool DeepSearch
		{
			get
			{
				return this.Identity == null;
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000A3D04 File Offset: 0x000A1F04
		protected sealed override IConfigDataProvider CreateSession()
		{
			OrganizationIdParameter organization = null;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(this.Organization, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			if (this.publicFolderStatisticsDataProvider == null || base.CurrentOrganizationId != this.publicFolderStatisticsDataProvider.CurrentOrganizationId)
			{
				if (this.publicFolderStatisticsDataProvider != null)
				{
					this.publicFolderStatisticsDataProvider.Dispose();
					this.publicFolderStatisticsDataProvider = null;
				}
				Guid mailboxGuid = Guid.Empty;
				if (base.Fields.IsModified("MailboxInformation"))
				{
					ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), ExchangeErrorCategory.Client);
					if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
					{
						base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, aduser);
					}
					mailboxGuid = aduser.ExchangeGuid;
				}
				try
				{
					this.publicFolderStatisticsDataProvider = new PublicFolderStatisticsDataProvider(this.ConfigurationSession, "Get-PublicFolderStatistics", mailboxGuid);
				}
				catch (AccessDeniedException exception)
				{
					base.WriteError(exception, ErrorCategory.PermissionDenied, this.Identity);
				}
			}
			return this.publicFolderStatisticsDataProvider;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000A3E80 File Offset: 0x000A2080
		protected override void InternalProcessRecord()
		{
			if (this.Identity != null)
			{
				base.GetDataObject<PublicFolder>(this.Identity, this.publicFolderStatisticsDataProvider.PublicFolderDataProvider, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(this.Identity.ToString())));
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000A3EDE File Offset: 0x000A20DE
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000A3EF1 File Offset: 0x000A20F1
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.publicFolderStatisticsDataProvider != null)
			{
				this.publicFolderStatisticsDataProvider.Dispose();
				this.publicFolderStatisticsDataProvider = null;
			}
		}

		// Token: 0x04001E54 RID: 7764
		private const string MailboxInformation = "MailboxInformation";

		// Token: 0x04001E55 RID: 7765
		private PublicFolderStatisticsDataProvider publicFolderStatisticsDataProvider;
	}
}
