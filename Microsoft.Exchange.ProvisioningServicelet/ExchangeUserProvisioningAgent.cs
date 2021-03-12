using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExchangeUserProvisioningAgent : UserProvisioningAgent
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000705E File Offset: 0x0000525E
		public ExchangeUserProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.Component != ProvisioningComponent.ExchangeMigration)
			{
				throw new ArgumentException("data needs to be for ExchangeMigration.");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000707C File Offset: 0x0000527C
		protected override string[][] NewMailboxParameterMap
		{
			get
			{
				return ExchangeUserProvisioningAgent.newMailboxParameterMap;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00007083 File Offset: 0x00005283
		protected override string[][] GetMailboxParameterMap
		{
			get
			{
				return ExchangeUserProvisioningAgent.getMailboxParameterMap;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000708A File Offset: 0x0000528A
		protected override string[][] SetMailboxParameterMap
		{
			get
			{
				return ExchangeUserProvisioningAgent.setMailboxParameterMap;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00007091 File Offset: 0x00005291
		protected override string[][] SetMailboxParameterMapForPreexistingMailbox
		{
			get
			{
				return ExchangeUserProvisioningAgent.setMailboxParameterMapForPreexistingMailbox;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00007098 File Offset: 0x00005298
		protected override string[][] ImportRecipientDataPropertyParameterMapForDCAdmin
		{
			get
			{
				return ExchangeUserProvisioningAgent.importRecipientDataPropertyParameterMapForDCAdmin;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000709F File Offset: 0x0000529F
		protected override string[][] SetUserParameterMap
		{
			get
			{
				return ExchangeUserProvisioningAgent.setUserParameterMap;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000070A8 File Offset: 0x000052A8
		protected override Error Provision()
		{
			UserProvisioningData userProvisioningData = (UserProvisioningData)base.ProvisioningData;
			if (userProvisioningData.Action == ProvisioningAction.UpdateExisting)
			{
				Mailbox mailbox;
				Error error = base.GetMailbox(out mailbox);
				if (error != null)
				{
					return error;
				}
				this.mailboxData = new MailboxData(mailbox.ExchangeGuid, new Fqdn(mailbox.OriginatingServer), mailbox.LegacyExchangeDN, mailbox.Id, mailbox.ExchangeObjectId);
				this.mailboxData.Update(mailbox.Identity.ToString(), mailbox.OrganizationId);
				error = base.SetMailboxForPreexistingMailbox(mailbox);
				if (error != null)
				{
					return error;
				}
			}
			else
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2196122941U);
				Mailbox mailbox2;
				Error error2 = base.NewMailbox(out mailbox2);
				if (error2 != null && error2.Exception is WLCDManagedMemberExistsException && userProvisioningData.Action == ProvisioningAction.CreateNewOrUpdateExisting && base.GetMailbox(out mailbox2) == null)
				{
					error2 = null;
				}
				if (error2 != null)
				{
					return error2;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3940953405U);
				this.mailboxData = new MailboxData(mailbox2.ExchangeGuid, new Fqdn(mailbox2.OriginatingServer), mailbox2.LegacyExchangeDN, mailbox2.Id, mailbox2.ExchangeObjectId);
				this.mailboxData.Update(mailbox2.Identity.ToString(), mailbox2.OrganizationId);
				error2 = base.SetMailbox(mailbox2);
				if (error2 != null)
				{
					return error2;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2464558397U);
			}
			return null;
		}

		// Token: 0x04000074 RID: 116
		private static readonly string[][] newMailboxParameterMap = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.DisplayName.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.FirstName.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Initials.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.LastName.Name,
				string.Empty
			},
			new string[]
			{
				ADObjectSchema.Name.Name,
				string.Empty
			},
			new string[]
			{
				ADRecipientSchema.WindowsLiveID.Name,
				string.Empty
			},
			new string[]
			{
				ADUserSchema.ResetPasswordOnNextLogon.Name,
				string.Empty
			},
			new string[]
			{
				"Organization",
				string.Empty
			},
			new string[]
			{
				"MicrosoftOnlineServicesID",
				string.Empty
			}
		};

		// Token: 0x04000075 RID: 117
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

		// Token: 0x04000076 RID: 118
		private static readonly string[][] setMailboxParameterMap = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.EmailAddresses.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Languages.Name,
				string.Empty
			},
			new string[]
			{
				ADRecipientSchema.ResourceCapacity.Name,
				string.Empty
			},
			new string[]
			{
				ADRecipientSchema.ResourceType.Name,
				"Type"
			}
		};

		// Token: 0x04000077 RID: 119
		private static readonly string[][] setMailboxParameterMapForPreexistingMailbox = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.EmailAddresses.Name,
				string.Empty
			}
		};

		// Token: 0x04000078 RID: 120
		private static readonly string[][] setUserParameterMap = new string[][]
		{
			new string[]
			{
				ADOrgPersonSchema.City.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Company.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.CountryOrRegion.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Department.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Fax.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.HomePhone.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.MobilePhone.Name,
				string.Empty
			},
			new string[]
			{
				ADRecipientSchema.Notes.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Office.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Phone.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.PostalCode.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.StateOrProvince.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.StreetAddress.Name,
				string.Empty
			},
			new string[]
			{
				ADOrgPersonSchema.Title.Name,
				string.Empty
			},
			new string[]
			{
				ADRecipientSchema.WebPage.Name,
				string.Empty
			}
		};

		// Token: 0x04000079 RID: 121
		private static readonly string[][] importRecipientDataPropertyParameterMapForDCAdmin = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.UMSpokenName.Name,
				"FileData"
			}
		};
	}
}
