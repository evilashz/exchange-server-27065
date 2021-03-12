using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000CC7 RID: 3271
	[Cmdlet("Get", "PublicFolderClientPermission", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderClientPermission : GetMailboxFolderPermission
	{
		// Token: 0x17002734 RID: 10036
		// (get) Token: 0x06007E0E RID: 32270 RVA: 0x002037BC File Offset: 0x002019BC
		// (set) Token: 0x06007E0F RID: 32271 RVA: 0x002037D3 File Offset: 0x002019D3
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public new PublicFolderIdParameter Identity
		{
			get
			{
				return (PublicFolderIdParameter)base.Fields["PublicFolderId"];
			}
			set
			{
				base.Fields["PublicFolderId"] = value;
			}
		}

		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x06007E10 RID: 32272 RVA: 0x002037E6 File Offset: 0x002019E6
		// (set) Token: 0x06007E11 RID: 32273 RVA: 0x002037FD File Offset: 0x002019FD
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x06007E12 RID: 32274 RVA: 0x00203810 File Offset: 0x00201A10
		protected override ObjectId ResolvedObjectId
		{
			get
			{
				return this.Identity.PublicFolderId;
			}
		}

		// Token: 0x06007E13 RID: 32275 RVA: 0x00203820 File Offset: 0x00201A20
		protected override ADUser PrepareMailboxUser()
		{
			OrganizationIdParameter organization = null;
			ADUser aduser = null;
			Guid publicFolderMailboxGuid = Guid.Empty;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(null, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			if (base.Fields.IsModified("Mailbox"))
			{
				aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.Mailbox.ToString())), ExchangeErrorCategory.Client);
				if (aduser == null || aduser.RecipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
				{
					base.WriteError(new ObjectNotFoundException(Strings.PublicFolderMailboxNotFound), ExchangeErrorCategory.Client, aduser);
				}
				publicFolderMailboxGuid = aduser.ExchangeGuid;
			}
			base.Identity = PublicFolderPermissionTaskHelper.GetMailboxFolderIdParameterForPublicFolder(this.ConfigurationSession, this.Identity, publicFolderMailboxGuid, aduser, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.PrepareMailboxUser();
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x00203949 File Offset: 0x00201B49
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || base.IsKnownException(exception);
		}

		// Token: 0x04003E18 RID: 15896
		private const string MailboxFieldName = "Mailbox";
	}
}
