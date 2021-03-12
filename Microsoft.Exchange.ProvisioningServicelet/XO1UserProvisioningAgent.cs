using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class XO1UserProvisioningAgent : ProvisioningAgent
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00007A8F File Offset: 0x00005C8F
		public XO1UserProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.Component != ProvisioningComponent.XO1)
			{
				throw new ArgumentException("data needs to be for XO1.");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00007AAD File Offset: 0x00005CAD
		public override IMailboxData MailboxData
		{
			get
			{
				return this.mailboxData;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007AB8 File Offset: 0x00005CB8
		protected override Error CreateRecipient()
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17748, (long)this.GetHashCode(), "CreateRecipient");
			XO1UserProvisioningData xo1UserProvisioningData = (XO1UserProvisioningData)base.ProvisioningData;
			PSCommand command = new PSCommand().AddCommand("new-consumermailbox");
			if (!base.PopulateParamsToPSCommand(command, XO1UserProvisioningAgent.newConsumerMailboxParameterMap, xo1UserProvisioningData.Parameters))
			{
				throw new InvalidOperationException("No parameters were mapped for New-ConsumerMailbox.");
			}
			ExTraceGlobals.WorkerTracer.TraceInformation(17752, (long)this.GetHashCode(), "invoke new-ConsumerMailbox");
			Error error;
			ConsumerMailbox consumerMailbox = base.SafeRunPSCommand<ConsumerMailbox>(command, base.AgentContext.Runspace, out error, null, null);
			if ((error != null && error.Exception is WLCDManagedMemberExistsException) || consumerMailbox == null)
			{
				command = new PSCommand().AddCommand("get-consumermailbox");
				if (base.PopulateParamsToPSCommand(command, XO1UserProvisioningAgent.getConsumerMailboxParameterMap, xo1UserProvisioningData.Parameters))
				{
					Error error2;
					consumerMailbox = base.SafeRunPSCommand<ConsumerMailbox>(command, base.AgentContext.Runspace, out error2, null, null);
				}
			}
			if (consumerMailbox == null)
			{
				if (error == null)
				{
					error = new Error(new InvalidDataException("no ConsumerMailbox created or found, but no error either!"));
				}
				return error;
			}
			this.mailboxData = new ConsumerMailboxData(consumerMailbox.ExchangeGuid.Value, consumerMailbox.Database.ObjectGuid);
			this.mailboxData.Update(consumerMailbox.Identity.ToString(), OrganizationId.ForestWideOrgId);
			return null;
		}

		// Token: 0x0400007E RID: 126
		private static readonly string[][] newConsumerMailboxParameterMap = new string[][]
		{
			new string[]
			{
				"Identity",
				"WindowsLiveID"
			},
			new string[]
			{
				"FirstName",
				string.Empty
			},
			new string[]
			{
				"LastName",
				string.Empty
			},
			new string[]
			{
				"TimeZone",
				string.Empty
			},
			new string[]
			{
				"LocaleId",
				string.Empty
			},
			new string[]
			{
				"EmailAddresses",
				string.Empty
			},
			new string[]
			{
				"Database",
				string.Empty
			},
			new string[]
			{
				"MakeExoSecondary",
				string.Empty
			}
		};

		// Token: 0x0400007F RID: 127
		private static readonly string[][] getConsumerMailboxParameterMap = new string[][]
		{
			new string[]
			{
				"Identity",
				string.Empty
			}
		};

		// Token: 0x04000080 RID: 128
		private IMailboxData mailboxData;
	}
}
