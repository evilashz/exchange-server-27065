using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000315 RID: 789
	[Cmdlet("Get", "MailboxComplianceConfiguration", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxComplianceConfiguration : GetRecipientBase<MailboxIdParameter, ADUser>
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x00075670 File Offset: 0x00073870
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return RecipientConstants.GetMailboxOrSyncMailbox_AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00075677 File Offset: 0x00073877
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetMailboxComplianceConfiguration.SortPropertiesArray;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0007567E File Offset: 0x0007387E
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return new MailboxSchema();
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00075688 File Offset: 0x00073888
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			if (((ADUser)dataObject).ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				base.WriteError(new InvalidOperationException(Strings.NotSupportedForPre14Mailbox(ExchangeObjectVersion.Exchange2010.ToString(), this.Identity.ToString(), ((ADUser)dataObject).ExchangeVersion.ToString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			IConfigurable result;
			using (MailboxSession mailboxSession = ELCTaskHelper.OpenMailboxSession((ADUser)dataObject, "Client=Management;Action=Get-MailboxComplianceConfiguration", new Task.TaskErrorLoggingDelegate(base.WriteError)))
			{
				if (mailboxSession == null)
				{
					base.WriteError(new TaskException(Strings.ErrorNonExchangeUserError(this.Identity.ToString())), ErrorCategory.NotSpecified, null);
				}
				result = new MailboxComplianceConfiguration(mailboxSession)
				{
					Identity = dataObject.Identity,
					OrganizationId = ((ADUser)dataObject).OrganizationId
				};
			}
			return result;
		}

		// Token: 0x04000B8B RID: 2955
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			MailEnabledRecipientSchema.DisplayName,
			MailEnabledRecipientSchema.Alias,
			MailboxSchema.Database,
			MailboxSchema.ServerLegacyDN
		};
	}
}
