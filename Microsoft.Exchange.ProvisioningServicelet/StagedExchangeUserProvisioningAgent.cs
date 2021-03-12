using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class StagedExchangeUserProvisioningAgent : UserProvisioningAgent
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000076FD File Offset: 0x000058FD
		public StagedExchangeUserProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.Component != ProvisioningComponent.StagedExchangeMigration)
			{
				throw new ArgumentException("data needs to be for StagedExchangeMigration.");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000771B File Offset: 0x0000591B
		protected override string[][] GetMailboxParameterMap
		{
			get
			{
				return StagedExchangeUserProvisioningAgent.getMailboxParameterMap;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00007722 File Offset: 0x00005922
		protected override string[][] SetMailboxParameterMapForDCAdmin
		{
			get
			{
				return StagedExchangeUserProvisioningAgent.setMailboxParameterMapForDCAdmin;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000772C File Offset: 0x0000592C
		protected override Error Provision()
		{
			UserProvisioningData userProvisioningData = (UserProvisioningData)base.ProvisioningData;
			Mailbox mailbox;
			Error error = base.ConvertMailUserToMailbox(out mailbox);
			if (error != null && error.Exception is ManagementObjectNotFoundException)
			{
				base.GetMailbox(out mailbox);
				error = ((mailbox != null) ? null : error);
			}
			if (error != null)
			{
				return error;
			}
			this.mailboxData = new MailboxData(mailbox.ExchangeGuid, new Fqdn(mailbox.OriginatingServer), mailbox.LegacyExchangeDN, mailbox.Id, mailbox.ExchangeObjectId);
			this.mailboxData.Update(mailbox.Identity.ToString(), mailbox.OrganizationId);
			return null;
		}

		// Token: 0x0400007A RID: 122
		private static readonly string[][] getMailboxParameterMap = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.WindowsLiveID.Name,
				"Identity"
			},
			new string[]
			{
				"Organization",
				string.Empty
			},
			new string[]
			{
				"MicrosoftOnlineServicesID",
				"Identity"
			}
		};

		// Token: 0x0400007B RID: 123
		private static readonly string[][] setMailboxParameterMapForDCAdmin = new string[][]
		{
			new string[]
			{
				ADUserSchema.ResetPasswordOnNextLogon.Name,
				string.Empty
			}
		};
	}
}
