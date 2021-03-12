using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x0200048B RID: 1163
	[Cmdlet("Get", "MailPublicFolder", DefaultParameterSetName = "Identity")]
	public sealed class GetMailPublicFolder : GetRecipientBase<MailPublicFolderIdParameter, ADPublicFolder>
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x000A2C34 File Offset: 0x000A0E34
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x000A2C4B File Offset: 0x000A0E4B
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["OrganizationalUnit"];
			}
			set
			{
				base.Fields["OrganizationalUnit"] = value;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000A2C5E File Offset: 0x000A0E5E
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetMailPublicFolder.SortPropertiesArray;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000A2C65 File Offset: 0x000A0E65
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return GetMailPublicFolder.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000A2C6C File Offset: 0x000A0E6C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailPublicFolder.FromDataObject((ADPublicFolder)dataObject);
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000A2C79 File Offset: 0x000A0E79
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ADPublicFolderSchema>();
			}
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000A2C80 File Offset: 0x000A0E80
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (MapiTaskHelper.IsDatacenter)
			{
				OrganizationIdParameter organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(base.Organization, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				return MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			}
			return base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000A2CF5 File Offset: 0x000A0EF5
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return false;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000A2CF8 File Offset: 0x000A0EF8
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x04001E42 RID: 7746
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.PublicFolder
		};

		// Token: 0x04001E43 RID: 7747
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ADRecipientSchema.Alias,
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName
		};
	}
}
