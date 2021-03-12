using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x0200000A RID: 10
	internal sealed class MeuUpdateProvisioningAgent : ProvisioningAgent
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00004524 File Offset: 0x00002724
		public MeuUpdateProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.ProvisioningType != ProvisioningType.MailEnabledUserUpdate)
			{
				throw new ArgumentException("data needs to be of MailEnabledUserUpdateProvisioningData type.");
			}
			if (((MailEnabledUserUpdateProvisioningData)data).IsEmpty())
			{
				throw new ArgumentException("data cannot be empty.");
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000455C File Offset: 0x0000275C
		protected override Error CreateRecipient()
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17748, (long)this.GetHashCode(), "CreateRecipient");
			string identity = ((MailEnabledUserUpdateProvisioningData)base.ProvisioningData).Identity;
			PSCommand pscommand = new PSCommand().AddCommand("Set-MailUser");
			if (base.PopulateParamsToPSCommand(pscommand, MeuUpdateProvisioningAgent.setMailUserParameterMap, base.ProvisioningData.Parameters))
			{
				ExTraceGlobals.WorkerTracer.TraceInformation(17776, (long)this.GetHashCode(), "invoke set-mailuser");
				pscommand.AddParameter("Identity", identity);
				Error error;
				base.SafeRunPSCommand<MailUser>(pscommand, base.AgentContext.Runspace, out error, new ProvisioningAgent.ErrorMessageOperation(Strings.FailedToUpdateProperty), null);
				if (error != null)
				{
					error.Message = Strings.FailedToUpdateProperty(error.Exception.Message);
					return error;
				}
			}
			pscommand = new PSCommand().AddCommand("set-user");
			if (base.PopulateParamsToPSCommand(pscommand, MeuUpdateProvisioningAgent.setUserParameterMap, base.ProvisioningData.Parameters))
			{
				ExTraceGlobals.WorkerTracer.TraceInformation(17780, (long)this.GetHashCode(), "invoke set-user");
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

		// Token: 0x06000054 RID: 84 RVA: 0x000046B3 File Offset: 0x000028B3
		protected override void IncrementPerfCounterForAttempt()
		{
			base.IncrementPerfCounterForAttempt();
			BulkUserProvisioningCounters.NumberOfUpdateContactsAttempted.Increment();
			BulkUserProvisioningCounters.RateOfUpdateContactsAttempted.Increment();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000046D1 File Offset: 0x000028D1
		protected override void IncrementPerfCounterForFailure()
		{
			base.IncrementPerfCounterForFailure();
			BulkUserProvisioningCounters.NumberOfUpdateContactsFailed.Increment();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000046E4 File Offset: 0x000028E4
		protected override void IncrementPerfCounterForCompletion()
		{
			base.IncrementPerfCounterForCompletion();
			BulkUserProvisioningCounters.NumberOfUpdateContactsCreated.Increment();
			BulkUserProvisioningCounters.RateOfUpdateContactsCreated.Increment();
		}

		// Token: 0x04000020 RID: 32
		private static readonly string[][] setMailUserParameterMap = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.GrantSendOnBehalfTo.Name,
				string.Empty
			}
		};

		// Token: 0x04000021 RID: 33
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
