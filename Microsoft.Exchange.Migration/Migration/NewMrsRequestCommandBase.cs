using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000148 RID: 328
	internal class NewMrsRequestCommandBase : MrsAccessorCommand
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x00045738 File Offset: 0x00043938
		protected NewMrsRequestCommandBase(string cmdletName, ICollection<Type> ignoredExceptions) : base(cmdletName, ignoredExceptions, null)
		{
		}

		// Token: 0x170004F0 RID: 1264
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x00045743 File Offset: 0x00043943
		public bool SuspendWhenReadyToComplete
		{
			set
			{
				base.AddParameter("SuspendWhenReadyToComplete", value);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x00045756 File Offset: 0x00043956
		public TimeSpan IncrementalSyncInterval
		{
			set
			{
				base.AddParameter("IncrementalSyncInterval", value);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x0004576C File Offset: 0x0004396C
		public Unlimited<int> BadItemLimit
		{
			set
			{
				base.AddParameter("BadItemLimit", value);
				Unlimited<int> value2 = new Unlimited<int>(TestIntegration.Instance.LargeDataLossThreshold);
				if (value >= value2)
				{
					this.AcceptLargeDataLoss = true;
				}
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x000457AC File Offset: 0x000439AC
		public Unlimited<int> LargeItemLimit
		{
			set
			{
				base.AddParameter("LargeItemLimit", value);
				Unlimited<int> value2 = new Unlimited<int>(TestIntegration.Instance.LargeDataLossThreshold);
				if (value >= value2)
				{
					this.AcceptLargeDataLoss = true;
				}
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x000457EB File Offset: 0x000439EB
		public bool AcceptLargeDataLoss
		{
			set
			{
				if (value && !this.hasAcceptedLargeDataLoss)
				{
					base.AddParameter("AcceptLargeDataLoss");
					this.hasAcceptedLargeDataLoss = true;
				}
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x0004580A File Offset: 0x00043A0A
		public string BatchName
		{
			set
			{
				base.AddParameter("BatchName", value);
			}
		}

		// Token: 0x040005D4 RID: 1492
		internal const string AcceptLargeDataLossParameter = "AcceptLargeDataLoss";

		// Token: 0x040005D5 RID: 1493
		private bool hasAcceptedLargeDataLoss;
	}
}
