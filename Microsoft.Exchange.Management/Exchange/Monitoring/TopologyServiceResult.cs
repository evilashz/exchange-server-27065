using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class TopologyServiceResult
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000044B8 File Offset: 0x000026B8
		public TopologyServiceResult()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000044C0 File Offset: 0x000026C0
		public TopologyServiceResult(TopologyServiceResultEnum result)
		{
			this.result = result;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000044CF File Offset: 0x000026CF
		public TopologyServiceResultEnum Value
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000044D8 File Offset: 0x000026D8
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.result)
			{
			case TopologyServiceResultEnum.Undefined:
				text = Strings.TopologyServiceResultUndefined;
				break;
			case TopologyServiceResultEnum.Success:
				text = Strings.TopologyServiceResultSuccess;
				break;
			case TopologyServiceResultEnum.Failure:
				text = Strings.TopologyServiceResultFailure;
				break;
			}
			return text;
		}

		// Token: 0x04000060 RID: 96
		private TopologyServiceResultEnum result;
	}
}
