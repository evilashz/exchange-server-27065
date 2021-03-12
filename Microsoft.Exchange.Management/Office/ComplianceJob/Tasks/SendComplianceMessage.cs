using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Client;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Resolver;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x0200014A RID: 330
	[OutputType(new Type[]
	{
		typeof(bool)
	})]
	[Cmdlet("Send", "ComplianceMessage")]
	public sealed class SendComplianceMessage : Microsoft.Exchange.Configuration.Tasks.Task
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00036FBA File Offset: 0x000351BA
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00036FCC File Offset: 0x000351CC
		[Parameter(Mandatory = true)]
		public byte[] SerializedComplianceMessage
		{
			get
			{
				return ComplianceSerializer.Serialize<ComplianceMessage>(ComplianceMessage.Description, this.message);
			}
			set
			{
				this.message = ComplianceSerializer.DeSerialize<ComplianceMessage>(ComplianceMessage.Description, value);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00036FDF File Offset: 0x000351DF
		public SendComplianceMessage()
		{
			this.message = new ComplianceMessage();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00036FF4 File Offset: 0x000351F4
		protected override void InternalProcessRecord()
		{
			if (this.message != null && this.message.WorkDefinitionType == WorkDefinitionType.Test && this.message.MessageId.Equals("RpsProxyClientTestMessage", StringComparison.InvariantCultureIgnoreCase))
			{
				base.WriteObject(true);
				return;
			}
			this.message.TenantId = base.CurrentOrganizationId.GetBytes(Encoding.UTF8);
			WorkloadClientBase workloadClientBase = new IntraExchangeWorkloadClient();
			ActiveDirectoryTargetResolver activeDirectoryTargetResolver = new ActiveDirectoryTargetResolver();
			IEnumerable<ComplianceMessage> sources = new List<ComplianceMessage>
			{
				this.message
			};
			IEnumerable<ComplianceMessage> messages = activeDirectoryTargetResolver.Resolve(sources);
			Task<bool[]> task = workloadClientBase.SendMessageAsync(messages);
			task.Wait();
			bool flag = task.Result[0];
			base.WriteObject(flag);
		}

		// Token: 0x040005B2 RID: 1458
		private const string RpsProxyClientTestMessage = "RpsProxyClientTestMessage";

		// Token: 0x040005B3 RID: 1459
		private ComplianceMessage message;
	}
}
