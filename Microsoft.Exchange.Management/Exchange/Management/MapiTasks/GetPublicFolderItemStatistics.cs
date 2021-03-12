using System;
using System.Collections.Generic;
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
	// Token: 0x0200048D RID: 1165
	[Cmdlet("Get", "PublicFolderItemStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderItemStatistics : GetTenantADObjectWithIdentityTaskBase<PublicFolderIdParameter, PublicFolderItemStatistics>
	{
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000A34E3 File Offset: 0x000A16E3
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x000A34FA File Offset: 0x000A16FA
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override PublicFolderIdParameter Identity
		{
			get
			{
				return (PublicFolderIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000A350D File Offset: 0x000A170D
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x000A3524 File Offset: 0x000A1724
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
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

		// Token: 0x06002948 RID: 10568 RVA: 0x000A3538 File Offset: 0x000A1738
		protected sealed override IConfigDataProvider CreateSession()
		{
			OrganizationIdParameter organization = null;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(null, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			if (this.publicFolderDataProvider == null || base.CurrentOrganizationId != this.publicFolderDataProvider.CurrentOrganizationId)
			{
				if (this.publicFolderDataProvider != null)
				{
					this.publicFolderDataProvider.Dispose();
					this.publicFolderDataProvider = null;
					this.mailboxGuid = Guid.Empty;
				}
				if (this.mailboxGuid == Guid.Empty && base.Fields.IsModified("MailboxInformation"))
				{
					ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), ExchangeErrorCategory.Client);
					if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
					{
						base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, aduser);
					}
					this.mailboxGuid = aduser.ExchangeGuid;
				}
				try
				{
					this.publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "Get-PublicFolderItemStatistics", Guid.Empty);
				}
				catch (AccessDeniedException exception)
				{
					base.WriteError(exception, ErrorCategory.PermissionDenied, this.Identity);
				}
			}
			return this.publicFolderDataProvider;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000A36D0 File Offset: 0x000A18D0
		protected override void InternalProcessRecord()
		{
			PublicFolder publicFolder = (PublicFolder)base.GetDataObject<PublicFolder>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(this.Identity.ToString())));
			this.WriteResult<PublicFolderItemStatistics>(this.FindItems(publicFolder));
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000A372D File Offset: 0x000A192D
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000A3740 File Offset: 0x000A1940
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.publicFolderDataProvider != null)
			{
				this.publicFolderDataProvider.Dispose();
				this.publicFolderDataProvider = null;
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000A3B18 File Offset: 0x000A1D18
		private IEnumerable<PublicFolderItemStatistics> FindItems(PublicFolder publicFolder)
		{
			PropertyDefinition[] xsoProperties = PublicFolderItemStatistics.StaticSchema.AllDependentXsoProperties;
			PublicFolderSession publicFolderContentSession = null;
			if (this.mailboxGuid == Guid.Empty)
			{
				publicFolderContentSession = this.publicFolderDataProvider.PublicFolderSessionCache.GetPublicFolderSession(publicFolder.InternalFolderIdentity.ObjectId);
			}
			else
			{
				publicFolderContentSession = this.publicFolderDataProvider.PublicFolderSessionCache.GetPublicFolderSession(this.mailboxGuid);
			}
			using (Folder xsoFolder = Folder.Bind(publicFolderContentSession, publicFolder.InternalFolderIdentity))
			{
				using (QueryResult itemQueryResult = xsoFolder.ItemQuery(ItemQueryType.None, null, GetPublicFolderItemStatistics.sortByProperties, xsoProperties))
				{
					for (;;)
					{
						object[][] rowResults = itemQueryResult.GetRows(100);
						if (rowResults == null || rowResults.Length <= 0)
						{
							break;
						}
						foreach (object[] row in rowResults)
						{
							PublicFolderItemStatistics itemStatistics = new PublicFolderItemStatistics();
							itemStatistics.LoadDataFromXsoRows(publicFolderContentSession.MailboxPrincipal.ObjectId, row, xsoProperties);
							if (base.NeedSuppressingPiiData)
							{
								itemStatistics.PublicFolderName = SuppressingPiiData.Redact(this.Identity.ToString());
							}
							else
							{
								itemStatistics.PublicFolderName = this.Identity.ToString();
							}
							yield return itemStatistics;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x04001E4E RID: 7758
		private const string MailboxInformation = "MailboxInformation";

		// Token: 0x04001E4F RID: 7759
		private static readonly SortBy[] sortByProperties = new SortBy[]
		{
			new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
		};

		// Token: 0x04001E50 RID: 7760
		private PublicFolderDataProvider publicFolderDataProvider;

		// Token: 0x04001E51 RID: 7761
		private Guid mailboxGuid = Guid.Empty;
	}
}
