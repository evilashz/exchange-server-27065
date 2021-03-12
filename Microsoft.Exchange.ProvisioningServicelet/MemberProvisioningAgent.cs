using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000008 RID: 8
	internal sealed class MemberProvisioningAgent : ProvisioningAgent
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003B03 File Offset: 0x00001D03
		public MemberProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.ProvisioningType != ProvisioningType.GroupMember)
			{
				throw new ArgumentException("data needs to be of MemberProvisioningData type.");
			}
			if (((MemberProvisioningData)data).IsEmpty())
			{
				throw new ArgumentException("data cannot be empty.");
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003B3C File Offset: 0x00001D3C
		protected override Error CreateRecipient()
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17748, (long)this.GetHashCode(), "CreateRecipient");
			string identity = ((MemberProvisioningData)base.ProvisioningData).Identity;
			ExTraceGlobals.WorkerTracer.TraceInformation(17752, (long)this.GetHashCode(), "invoke add-distributiongroupmember");
			this.TryAddDistributionGroupMembers(identity);
			ExTraceGlobals.WorkerTracer.TraceInformation(17752, (long)this.GetHashCode(), "invoke set-distributiongroup");
			PSCommand pscommand = new PSCommand().AddCommand("Set-DistributionGroup");
			pscommand.AddParameter("BypassSecurityGroupManagerCheck");
			if (base.PopulateParamsToPSCommand(pscommand, MemberProvisioningAgent.setDgParameterMap, base.ProvisioningData.Parameters))
			{
				pscommand.AddParameter("Identity", identity);
				Error error;
				base.SafeRunPSCommand<object>(pscommand, base.AgentContext.Runspace, out error, new ProvisioningAgent.ErrorMessageOperation(Strings.FailedToUpdateProperty), new uint?(2695245117U));
				if (error != null)
				{
					return error;
				}
			}
			return null;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003C25 File Offset: 0x00001E25
		protected override void IncrementPerfCounterForAttempt()
		{
			base.IncrementPerfCounterForAttempt();
			BulkUserProvisioningCounters.NumberOfMembersAttempted.Increment();
			BulkUserProvisioningCounters.RateOfMembersAttempted.Increment();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003C43 File Offset: 0x00001E43
		protected override void IncrementPerfCounterForFailure()
		{
			base.IncrementPerfCounterForFailure();
			BulkUserProvisioningCounters.NumberOfMembersFailed.Increment();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003C56 File Offset: 0x00001E56
		protected override void IncrementPerfCounterForCompletion()
		{
			base.IncrementPerfCounterForCompletion();
			BulkUserProvisioningCounters.NumberOfMembersCreated.Increment();
			BulkUserProvisioningCounters.RateOfMembersCreated.Increment();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003C74 File Offset: 0x00001E74
		private void TryAddDistributionGroupMembers(string identity)
		{
			MemberProvisioningData memberProvisioningData = base.ProvisioningData as MemberProvisioningData;
			if (memberProvisioningData.Members == null || memberProvisioningData.Members.Length == 0)
			{
				return;
			}
			foreach (string value in memberProvisioningData.Members)
			{
				if (!string.IsNullOrEmpty(value))
				{
					PSCommand pscommand = new PSCommand().AddCommand("Add-DistributionGroupMember");
					pscommand.AddParameter("Identity", identity);
					pscommand.AddParameter("Member", value);
					pscommand.AddParameter("BypassSecurityGroupManagerCheck");
					Error error;
					base.SafeRunPSCommand<object>(pscommand, base.AgentContext.Runspace, out error, new ProvisioningAgent.ErrorMessageOperation(Strings.FailedToUpdateDistributionGroupMember), new uint?(3500551485U));
					if (error == null)
					{
						base.GroupMemberProvisioned++;
					}
					else if (error.Exception is MemberAlreadyExistsException)
					{
						base.GroupMemberProvisioned++;
					}
					else
					{
						base.GroupMemberSkipped++;
					}
				}
			}
		}

		// Token: 0x0400001B RID: 27
		private static readonly string[][] setDgParameterMap = new string[][]
		{
			new string[]
			{
				ADGroupSchema.ManagedBy.Name,
				string.Empty
			},
			new string[]
			{
				ADRecipientSchema.GrantSendOnBehalfTo.Name,
				string.Empty
			}
		};
	}
}
