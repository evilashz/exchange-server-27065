using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class QuarantineMessageType
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x0000558C File Offset: 0x0000378C
		public QuarantineMessageType()
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005594 File Offset: 0x00003794
		public QuarantineMessageType(QuarantineMessageTypeEnum value)
		{
			this.value = value;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000055A3 File Offset: 0x000037A3
		public QuarantineMessageTypeEnum Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000055AC File Offset: 0x000037AC
		public override string ToString()
		{
			string result = string.Empty;
			switch (this.value)
			{
			case QuarantineMessageTypeEnum.Spam:
				result = CoreStrings.QuarantineSpam;
				break;
			case QuarantineMessageTypeEnum.TransportRule:
				result = CoreStrings.QuarantineTransportRule;
				break;
			}
			return result;
		}

		// Token: 0x04000055 RID: 85
		private QuarantineMessageTypeEnum value;
	}
}
