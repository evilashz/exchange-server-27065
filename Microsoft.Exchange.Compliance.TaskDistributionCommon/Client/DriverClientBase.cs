using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Client
{
	// Token: 0x02000005 RID: 5
	public abstract class DriverClientBase : IMessageSender
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000022E8 File Offset: 0x000004E8
		public virtual async Task<bool[]> SendMessageAsync(IEnumerable<ComplianceMessage> messages)
		{
			List<Task<ComplianceMessage>> tasks = new List<Task<ComplianceMessage>>();
			List<bool> sentMessages = new List<bool>();
			foreach (ComplianceMessage message in messages)
			{
				tasks.Add(this.GetResponseAsync(message));
			}
			foreach (Task<ComplianceMessage> t in tasks)
			{
				try
				{
					await t;
					sentMessages.Add(true);
				}
				catch (Exception)
				{
					sentMessages.Add(false);
				}
			}
			return sentMessages.ToArray();
		}

		// Token: 0x06000003 RID: 3
		public abstract Task<ComplianceMessage> GetResponseAsync(ComplianceMessage message);
	}
}
