using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200007B RID: 123
	public class TraceComponentInfo
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000915A File Offset: 0x0000735A
		public TraceComponentInfo(string prettyName, Guid componentGuid, TraceTagInfo[] tagInfoList)
		{
			this.prettyName = prettyName;
			this.componentGuid = componentGuid;
			this.tagInfoList = tagInfoList;
			this.faultInjectionComponentConfig = new FaultInjectionComponentConfig();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00009182 File Offset: 0x00007382
		public string PrettyName
		{
			get
			{
				return this.prettyName;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000918A File Offset: 0x0000738A
		public Guid ComponentGuid
		{
			get
			{
				return this.componentGuid;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00009192 File Offset: 0x00007392
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000919A File Offset: 0x0000739A
		public TraceTagInfo[] TagInfoList
		{
			get
			{
				return this.tagInfoList;
			}
			set
			{
				this.tagInfoList = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000091A3 File Offset: 0x000073A3
		internal FaultInjectionComponentConfig FaultInjectionComponentConfig
		{
			get
			{
				return this.faultInjectionComponentConfig;
			}
		}

		// Token: 0x04000282 RID: 642
		private string prettyName;

		// Token: 0x04000283 RID: 643
		private Guid componentGuid;

		// Token: 0x04000284 RID: 644
		private TraceTagInfo[] tagInfoList;

		// Token: 0x04000285 RID: 645
		private FaultInjectionComponentConfig faultInjectionComponentConfig;
	}
}
