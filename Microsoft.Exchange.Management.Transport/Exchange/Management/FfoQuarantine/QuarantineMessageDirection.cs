using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class QuarantineMessageDirection
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000055EF File Offset: 0x000037EF
		public QuarantineMessageDirection()
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000055F7 File Offset: 0x000037F7
		public QuarantineMessageDirection(QuarantineMessageDirectionEnum value)
		{
			this.value = value;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005606 File Offset: 0x00003806
		public QuarantineMessageDirectionEnum Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005610 File Offset: 0x00003810
		public override string ToString()
		{
			string result = string.Empty;
			switch (this.value)
			{
			case QuarantineMessageDirectionEnum.Inbound:
				result = CoreStrings.QuarantineInbound;
				break;
			case QuarantineMessageDirectionEnum.Outbound:
				result = CoreStrings.QuarantineOutbound;
				break;
			}
			return result;
		}

		// Token: 0x04000056 RID: 86
		private QuarantineMessageDirectionEnum value;
	}
}
