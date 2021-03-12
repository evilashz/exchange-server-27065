using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000577 RID: 1399
	[Serializable]
	public class MonitoringServiceBasicCmdletResult
	{
		// Token: 0x0600314C RID: 12620 RVA: 0x000C8C2F File Offset: 0x000C6E2F
		public MonitoringServiceBasicCmdletResult()
		{
			this.result = MonitoringServiceBasicCmdletResultEnum.Undefined;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000C8C3E File Offset: 0x000C6E3E
		public MonitoringServiceBasicCmdletResult(MonitoringServiceBasicCmdletResultEnum result)
		{
			this.result = result;
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000C8C4D File Offset: 0x000C6E4D
		public MonitoringServiceBasicCmdletResultEnum Value
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000C8C58 File Offset: 0x000C6E58
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.result)
			{
			case MonitoringServiceBasicCmdletResultEnum.Undefined:
				text = "Undefined";
				break;
			case MonitoringServiceBasicCmdletResultEnum.Success:
				text = "Success";
				break;
			case MonitoringServiceBasicCmdletResultEnum.Failure:
				text = "Failure";
				break;
			}
			return text;
		}

		// Token: 0x040022ED RID: 8941
		private MonitoringServiceBasicCmdletResultEnum result;
	}
}
