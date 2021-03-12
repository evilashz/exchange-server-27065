using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000013 RID: 19
	internal sealed class Pop3RequestStat : Pop3Request
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003B38 File Offset: 0x00001D38
		public Pop3RequestStat(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_STAT_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_STAT_Failures;
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003B6B File Offset: 0x00001D6B
		public override bool NeedsStoreConnection
		{
			get
			{
				return base.Factory.ExactRFC822SizeEnabled && !base.Factory.HasAllMessagesBeenRetrieved;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003B8C File Offset: 0x00001D8C
		public override ProtocolResponse Process()
		{
			long num = 0L;
			int num2 = 0;
			for (int i = 0; i < base.Factory.Messages.Count; i++)
			{
				if (!base.Factory.Messages[i].IsDeleted)
				{
					num2++;
					num += base.Factory.Messages[i].GetSize();
				}
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(num2);
				base.Session.LightLogSession.TotalSize = new long?(num);
			}
			return new Pop3Response(Pop3Response.Type.ok, string.Format("{0} {1}\r\n", num2, num));
		}
	}
}
