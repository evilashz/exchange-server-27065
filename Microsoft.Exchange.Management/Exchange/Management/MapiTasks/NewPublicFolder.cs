using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000490 RID: 1168
	[Cmdlet("New", "PublicFolder", SupportsShouldProcess = true)]
	public sealed class NewPublicFolder : NewTenantADTaskBase<PublicFolder>
	{
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x000A3F1F File Offset: 0x000A211F
		// (set) Token: 0x06002966 RID: 10598 RVA: 0x000A3F36 File Offset: 0x000A2136
		[Parameter]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000A3F49 File Offset: 0x000A2149
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x000A3F60 File Offset: 0x000A2160
		[Parameter]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x000A3F73 File Offset: 0x000A2173
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x000A3F8A File Offset: 0x000A218A
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x000A3F9D File Offset: 0x000A219D
		// (set) Token: 0x0600296C RID: 10604 RVA: 0x000A3FB4 File Offset: 0x000A21B4
		[Parameter]
		public PublicFolderIdParameter Path
		{
			get
			{
				return (PublicFolderIdParameter)base.Fields["Path"];
			}
			set
			{
				base.Fields["Path"] = value;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x000A3FC7 File Offset: 0x000A21C7
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x000A3FDE File Offset: 0x000A21DE
		[Parameter]
		public CultureInfo EformsLocaleId
		{
			get
			{
				return (CultureInfo)base.Fields[PublicFolderSchema.EformsLocaleId];
			}
			set
			{
				base.Fields[PublicFolderSchema.EformsLocaleId] = value;
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000A3FF1 File Offset: 0x000A21F1
		protected override OrganizationId ResolveCurrentOrganization()
		{
			return MapiTaskHelper.ResolveTargetOrganization(base.DomainController, this.Organization, base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000A4018 File Offset: 0x000A2218
		protected override IConfigDataProvider CreateSession()
		{
			if (this.publicFolderDataProvider == null || base.CurrentOrganizationId != this.publicFolderDataProvider.CurrentOrganizationId)
			{
				if (this.publicFolderDataProvider != null)
				{
					this.publicFolderDataProvider.Dispose();
					this.publicFolderDataProvider = null;
				}
				try
				{
					this.publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, "New-PublicFolder", Guid.Empty);
				}
				catch (AccessDeniedException exception)
				{
					base.WriteError(exception, ErrorCategory.PermissionDenied, this.Name);
				}
			}
			return this.publicFolderDataProvider;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000A40A4 File Offset: 0x000A22A4
		protected override void InternalBeginProcessing()
		{
			if (MapiTaskHelper.IsDatacenter)
			{
				this.Organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(this.Organization, null, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000A40E4 File Offset: 0x000A22E4
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (NotSupportedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidType, this.Name);
			}
			catch (InvalidOperationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, this.Name);
			}
			catch (ObjectExistedException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidData, this.Name);
			}
			catch (LocalizedException exception4)
			{
				base.WriteError(exception4, ErrorCategory.NotSpecified, this.Name);
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000A4174 File Offset: 0x000A2374
		protected override IConfigurable PrepareDataObject()
		{
			if (this.Path == null)
			{
				this.Path = new PublicFolderIdParameter(new PublicFolderId(MapiFolderPath.IpmSubtreeRoot));
			}
			PublicFolder publicFolder = null;
			try
			{
				publicFolder = (PublicFolder)base.GetDataObject<PublicFolder>(this.Path, base.DataSession, null, new LocalizedString?(Strings.ErrorPublicFolderNotFound(this.Path.ToString())), new LocalizedString?(Strings.ErrorPublicFolderNotUnique(this.Path.ToString())));
			}
			catch (NotSupportedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidType, this.Name);
			}
			if (publicFolder.ExtendedFolderFlags != null && ((publicFolder.ExtendedFolderFlags.Value & ExtendedFolderFlags.ExclusivelyBound) != (ExtendedFolderFlags)0 || (publicFolder.ExtendedFolderFlags.Value & ExtendedFolderFlags.RemoteHierarchy) != (ExtendedFolderFlags)0))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorInvalidParentExtendedFolderFlags(this.Name, this.Path.ToString(), string.Join(",", new string[]
				{
					ExtendedFolderFlags.ExclusivelyBound.ToString(),
					ExtendedFolderFlags.RemoteHierarchy.ToString()
				}))), ErrorCategory.InvalidOperation, this.Name);
			}
			PublicFolder publicFolder2 = (PublicFolder)base.PrepareDataObject();
			publicFolder2.MailboxOwnerId = this.publicFolderDataProvider.PublicFolderSession.MailboxPrincipal.ObjectId;
			publicFolder2[MailboxFolderSchema.InternalParentFolderIdentity] = publicFolder.InternalFolderIdentity.ObjectId;
			publicFolder2.FolderPath = MapiFolderPath.GenerateFolderPath(publicFolder.FolderPath, this.Name);
			publicFolder2.Name = this.Name;
			publicFolder2.PerUserReadStateEnabled = publicFolder.PerUserReadStateEnabled;
			publicFolder2.EformsLocaleId = this.EformsLocaleId;
			if (this.Mailbox != null)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, base.SessionSettings, 237, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\PublicFolder\\NewPublicFolder.cs");
				ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.Mailbox.ToString())));
				if (aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorMailboxNotFound(this.Mailbox.ToString())), ExchangeErrorCategory.Client, this.Mailbox);
				}
				publicFolder2.ContentMailboxGuid = aduser.ExchangeGuid;
			}
			publicFolder2.SetDefaultFolderType(DefaultFolderType.None);
			if (string.IsNullOrEmpty(publicFolder2.FolderClass))
			{
				if (DefaultFolderType.Root == publicFolder.DefaultFolderType || string.IsNullOrEmpty(publicFolder.FolderClass))
				{
					publicFolder2.FolderClass = "IPF.Note";
				}
				else
				{
					publicFolder2.FolderClass = publicFolder.FolderClass;
				}
			}
			return publicFolder2;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x000A4434 File Offset: 0x000A2634
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000A4447 File Offset: 0x000A2647
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.publicFolderDataProvider != null)
			{
				this.publicFolderDataProvider.Dispose();
				this.publicFolderDataProvider = null;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000A446D File Offset: 0x000A266D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPublicFolder(this.Path.ToString(), this.Name);
			}
		}

		// Token: 0x04001E56 RID: 7766
		private const string ParameterName = "Name";

		// Token: 0x04001E57 RID: 7767
		private const string ParameterPath = "Path";

		// Token: 0x04001E58 RID: 7768
		private const string ParameterOrganization = "Organization";

		// Token: 0x04001E59 RID: 7769
		private const string ParameterMailbox = "Mailbox";

		// Token: 0x04001E5A RID: 7770
		private PublicFolderDataProvider publicFolderDataProvider;
	}
}
