using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000007 RID: 7
	internal sealed class GroupProvisioningAgent : ProvisioningAgent
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000369D File Offset: 0x0000189D
		public GroupProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.ProvisioningType != ProvisioningType.Group)
			{
				throw new ArgumentException("data needs to be of GroupProvisioningData type.");
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000036BC File Offset: 0x000018BC
		protected override Error CreateRecipient()
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17748, (long)this.GetHashCode(), "CreateRecipient");
			ExTraceGlobals.WorkerTracer.TraceInformation(17752, (long)this.GetHashCode(), "invoke new-distributiongroup");
			PSCommand pscommand = new PSCommand().AddCommand("New-DistributionGroup");
			if (!base.PopulateParamsToPSCommand(pscommand, GroupProvisioningAgent.newDgParameterMap, base.ProvisioningData.Parameters))
			{
				return new Error(new InvalidOperationException("Developer error: No parameters were mapped for cmdlet."));
			}
			if (base.ProvisioningData.Parameters.ContainsKey("Organization") && !base.ProvisioningData.Parameters.ContainsKey(ADGroupSchema.ManagedBy.Name))
			{
				string value = "admin@" + base.ProvisioningData.Parameters["Organization"];
				pscommand.AddParameter(ADGroupSchema.ManagedBy.Name, value);
			}
			Error error;
			DistributionGroup distributionGroup = base.SafeRunPSCommand<DistributionGroup>(pscommand, base.AgentContext.Runspace, out error, null, new uint?(4037422397U));
			if (error != null)
			{
				if (error.Exception is InvalidOperationException)
				{
					return error;
				}
				pscommand = new PSCommand().AddCommand("Get-DistributionGroup");
				if (base.PopulateParamsToPSCommand(pscommand, GroupProvisioningAgent.getDgParameterMap, base.ProvisioningData.Parameters))
				{
					Error error2;
					distributionGroup = base.SafeRunPSCommand<DistributionGroup>(pscommand, base.AgentContext.Runspace, out error2, null, null);
				}
			}
			if (distributionGroup == null)
			{
				if (error == null)
				{
					error = new Error(new InvalidDataException("no group created or found, but no error either!"));
				}
				return error;
			}
			this.UpdateProxyAddressesParameter(distributionGroup);
			ExTraceGlobals.WorkerTracer.TraceInformation(17776, (long)this.GetHashCode(), "invoke set-distributiongroup");
			pscommand = new PSCommand().AddCommand("Set-DistributionGroup");
			pscommand.AddParameter("Identity", distributionGroup.Identity);
			pscommand.AddParameter("BypassSecurityGroupManagerCheck");
			if (base.PopulateParamsToPSCommand(pscommand, GroupProvisioningAgent.setDgParameterMap, base.ProvisioningData.Parameters))
			{
				base.SafeRunPSCommand<DistributionGroup>(pscommand, base.AgentContext.Runspace, out error, null, new uint?(2158374205U));
				if (error != null)
				{
					return error;
				}
			}
			pscommand = new PSCommand().AddCommand("Update-DistributionGroupMember");
			pscommand.AddParameter("Identity", distributionGroup.Identity);
			pscommand.AddParameter("BypassSecurityGroupManagerCheck");
			if (base.PopulateParamsToPSCommand(pscommand, GroupProvisioningAgent.updateDgMemberParameterMap, base.ProvisioningData.Parameters))
			{
				pscommand.AddParameter("Confirm", false);
				base.SafeRunPSCommand<DistributionGroup>(pscommand, base.AgentContext.Runspace, out error, null, null);
				if (error != null)
				{
					return error;
				}
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2426809661U);
			return null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000394C File Offset: 0x00001B4C
		protected override void UpdateProxyAddressesParameter(MailEnabledRecipient recipient)
		{
			base.UpdateProxyAddressesParameter(recipient);
			base.RemoveSmtpProxyAddressesWithExternalDomain();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000395B File Offset: 0x00001B5B
		protected override void IncrementPerfCounterForAttempt()
		{
			base.IncrementPerfCounterForAttempt();
			BulkUserProvisioningCounters.NumberOfGroupsAttempted.Increment();
			BulkUserProvisioningCounters.RateOfGroupsAttempted.Increment();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003979 File Offset: 0x00001B79
		protected override void IncrementPerfCounterForFailure()
		{
			base.IncrementPerfCounterForFailure();
			BulkUserProvisioningCounters.NumberOfGroupsFailed.Increment();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000398C File Offset: 0x00001B8C
		protected override void IncrementPerfCounterForCompletion()
		{
			base.IncrementPerfCounterForCompletion();
			BulkUserProvisioningCounters.NumberOfGroupsCreated.Increment();
			BulkUserProvisioningCounters.RateOfGroupsCreated.Increment();
		}

		// Token: 0x04000017 RID: 23
		private static readonly string[][] newDgParameterMap = new string[][]
		{
			new string[]
			{
				ADObjectSchema.Name.Name,
				string.Empty
			},
			new string[]
			{
				"Organization",
				string.Empty
			}
		};

		// Token: 0x04000018 RID: 24
		private static readonly string[][] getDgParameterMap = new string[][]
		{
			new string[]
			{
				ADObjectSchema.Name.Name,
				"Identity"
			},
			new string[]
			{
				"Organization",
				string.Empty
			}
		};

		// Token: 0x04000019 RID: 25
		private static readonly string[][] setDgParameterMap = new string[][]
		{
			new string[]
			{
				ADRecipientSchema.EmailAddresses.Name,
				string.Empty
			},
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

		// Token: 0x0400001A RID: 26
		private static readonly string[][] updateDgMemberParameterMap = new string[][]
		{
			new string[]
			{
				ADGroupSchema.Members.Name,
				string.Empty
			}
		};
	}
}
