using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000014 RID: 20
	internal sealed class UserUpdateProvisioningAgent : ProvisioningAgent
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00007864 File Offset: 0x00005A64
		public UserUpdateProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.ProvisioningType != ProvisioningType.UserUpdate)
			{
				throw new ArgumentException("data needs to be of UserUpdateProvisioningData type.");
			}
			if (((UserUpdateProvisioningData)data).IsEmpty())
			{
				throw new ArgumentException("data cannot be empty.");
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000789C File Offset: 0x00005A9C
		protected override Error CreateRecipient()
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17748, (long)this.GetHashCode(), "CreateRecipient");
			string identity = ((UserUpdateProvisioningData)base.ProvisioningData).Identity;
			ExTraceGlobals.WorkerTracer.TraceInformation(17776, (long)this.GetHashCode(), "invoke set-mailbox");
			PSCommand pscommand = new PSCommand().AddCommand("set-mailbox");
			if (base.PopulateParamsToPSCommand(pscommand, UserUpdateProvisioningAgent.setMailboxParameterMap, base.ProvisioningData.Parameters))
			{
				pscommand.AddParameter("Identity", identity);
				Error error;
				base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.Runspace, out error, new ProvisioningAgent.ErrorMessageOperation(Strings.FailedToUpdateProperty), null);
				if (error != null)
				{
					return error;
				}
			}
			ExTraceGlobals.WorkerTracer.TraceInformation(17780, (long)this.GetHashCode(), "invoke set-user");
			pscommand = new PSCommand().AddCommand("set-user");
			if (base.PopulateParamsToPSCommand(pscommand, UserUpdateProvisioningAgent.setUserParameterMap, base.ProvisioningData.Parameters))
			{
				pscommand.AddParameter("Identity", identity);
				Error error2;
				base.SafeRunPSCommand<ADUser>(pscommand, base.AgentContext.Runspace, out error2, new ProvisioningAgent.ErrorMessageOperation(Strings.FailedToUpdateProperty), null);
				if (error2 != null)
				{
					return error2;
				}
			}
			return null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000079D8 File Offset: 0x00005BD8
		protected override void IncrementPerfCounterForAttempt()
		{
			base.IncrementPerfCounterForAttempt();
			BulkUserProvisioningCounters.NumberOfUpdateUserAttempted.Increment();
			BulkUserProvisioningCounters.RateOfUpdateUserAttempted.Increment();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000079F6 File Offset: 0x00005BF6
		protected override void IncrementPerfCounterForFailure()
		{
			base.IncrementPerfCounterForFailure();
			BulkUserProvisioningCounters.NumberOfUpdateUserFailed.Increment();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007A09 File Offset: 0x00005C09
		protected override void IncrementPerfCounterForCompletion()
		{
			base.IncrementPerfCounterForCompletion();
			BulkUserProvisioningCounters.NumberOfUpdateUserCreated.Increment();
			BulkUserProvisioningCounters.RateOfUpdateUserCreated.Increment();
		}

		// Token: 0x0400007C RID: 124
		private static readonly string[][] setMailboxParameterMap = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.GrantSendOnBehalfTo.Name,
				string.Empty
			}
		};

		// Token: 0x0400007D RID: 125
		private static readonly string[][] setUserParameterMap = new string[][]
		{
			new string[]
			{
				ADOrgPersonSchema.Manager.Name,
				string.Empty
			}
		};
	}
}
