using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000492 RID: 1170
	[Cmdlet("Remove", "PublicFolder", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePublicFolder : RemoveTenantADTaskBase<PublicFolderIdParameter, PublicFolder>
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x000A4A55 File Offset: 0x000A2C55
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemovePublicFolder(this.Identity.ToString());
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000A4A68 File Offset: 0x000A2C68
		private IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					ADSessionSettings sessionSettings;
					if (MapiTaskHelper.IsDatacenter)
					{
						sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
					}
					else
					{
						sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
					}
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 75, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\PublicFolder\\RemovePublicFolder.cs");
					tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
					this.recipientSession = tenantOrRootOrgRecipientSession;
				}
				return this.recipientSession;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x000A4AD9 File Offset: 0x000A2CD9
		// (set) Token: 0x06002991 RID: 10641 RVA: 0x000A4AFF File Offset: 0x000A2CFF
		[Parameter]
		public SwitchParameter Recurse
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recurse"] ?? false);
			}
			set
			{
				base.Fields["Recurse"] = value;
			}
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000A4B18 File Offset: 0x000A2D18
		protected override void InternalValidate()
		{
			try
			{
				base.InternalValidate();
			}
			catch (NotSupportedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidType, this.Identity);
			}
			if (base.DataObject.InternalFolderIdentity.ObjectId.Equals(this.publicFolderDataProvider.PublicFolderSession.GetIpmSubtreeFolderId()))
			{
				base.WriteError(new InvalidOperationException(ServerStrings.ExceptionFolderIsRootFolder), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (base.DataObject.HasSubfolders.Value && !this.Recurse)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorFailToDeleteDueToSubFolders(this.Identity.ToString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (base.DataObject.MailEnabled)
			{
				ADObjectId identity = new ADObjectId(base.DataObject.ProxyGuid);
				if (!(this.RecipientSession.Read<ADPublicFolder>(identity) is ADPublicFolder))
				{
					base.WriteVerbose(Strings.ErrorMailPublicFolderNotFoundInDirectory(base.DataObject.Identity.ToString()));
					base.DataObject.MailEnabled = false;
					base.DataObject.ProxyGuid = null;
					this.publicFolderDataProvider.Save(base.DataObject);
					return;
				}
				base.WriteError(new InvalidOperationException(Strings.ErrorFolderIsMailEnabled), ErrorCategory.InvalidOperation, this.Identity);
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000A4C74 File Offset: 0x000A2E74
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (NotSupportedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidType, this.Identity);
			}
			catch (InvalidOperationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, this.Identity);
			}
			catch (StoragePermanentException exception3)
			{
				base.WriteError(exception3, ErrorCategory.DeviceError, this.Identity);
			}
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000A4CE8 File Offset: 0x000A2EE8
		protected override IConfigDataProvider CreateSession()
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
				}
				try
				{
					this.publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "Remove-PublicFolder", Guid.Empty);
				}
				catch (AccessDeniedException exception)
				{
					base.WriteError(exception, ErrorCategory.PermissionDenied, this.Identity);
				}
			}
			return this.publicFolderDataProvider;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000A4DCC File Offset: 0x000A2FCC
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000A4DDF File Offset: 0x000A2FDF
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.publicFolderDataProvider != null)
			{
				this.publicFolderDataProvider.Dispose();
				this.publicFolderDataProvider = null;
			}
		}

		// Token: 0x04001E65 RID: 7781
		private const string ParameterRecurse = "Recurse";

		// Token: 0x04001E66 RID: 7782
		private PublicFolderDataProvider publicFolderDataProvider;

		// Token: 0x04001E67 RID: 7783
		private IRecipientSession recipientSession;
	}
}
