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
	// Token: 0x02000489 RID: 1161
	[Cmdlet("Disable", "MailPublicFolder", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableMailPublicFolder : RemoveRecipientObjectTask<MailPublicFolderIdParameter, ADPublicFolder>
	{
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000A2162 File Offset: 0x000A0362
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x000A216A File Offset: 0x000A036A
		private new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.IgnoreDefaultScope;
			}
			set
			{
				base.IgnoreDefaultScope = value;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000A2173 File Offset: 0x000A0373
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableMailPublicFolder(this.Identity.ToString());
			}
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000A2188 File Offset: 0x000A0388
		protected override IConfigDataProvider CreateSession()
		{
			OrganizationIdParameter organization = null;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(null, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			return base.CreateSession();
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000A21F3 File Offset: 0x000A03F3
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return false;
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000A21F8 File Offset: 0x000A03F8
		protected override void InternalProcessRecord()
		{
			try
			{
				using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "Disable-MailPublicFolder", Guid.Empty))
				{
					if (!string.IsNullOrWhiteSpace(base.DataObject.EntryId))
					{
						StoreObjectId storeObjectId = StoreObjectId.FromHexEntryId(base.DataObject.EntryId);
						PublicFolder publicFolder = (PublicFolder)publicFolderDataProvider.Read<PublicFolder>(new PublicFolderId(storeObjectId));
						publicFolder.MailEnabled = false;
						publicFolder.ProxyGuid = Guid.Empty.ToByteArray();
						publicFolderDataProvider.Save(publicFolder);
					}
				}
			}
			catch (AccessDeniedException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.Authorization, this.Identity);
			}
			catch (ObjectNotFoundException ex)
			{
				this.WriteWarning(Strings.FailedToLocatePublicFolder(this.Identity.ToString(), ex.ToString()));
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000A22E4 File Offset: 0x000A04E4
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}
	}
}
