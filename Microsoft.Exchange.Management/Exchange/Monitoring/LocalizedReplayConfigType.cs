using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000561 RID: 1377
	public class LocalizedReplayConfigType
	{
		// Token: 0x060030E7 RID: 12519 RVA: 0x000C649C File Offset: 0x000C469C
		internal LocalizedReplayConfigType(ReplayConfigType configtype)
		{
			this.configType = configtype;
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000C64AB File Offset: 0x000C46AB
		internal ReplayConfigType ConfigType
		{
			get
			{
				return this.configType;
			}
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000C64B4 File Offset: 0x000C46B4
		public override string ToString()
		{
			string result = string.Empty;
			switch (this.configType)
			{
			case ReplayConfigType.RemoteCopyTarget:
			case ReplayConfigType.RemoteCopySource:
				result = Strings.RemoteContinuousReplication;
				break;
			case ReplayConfigType.SingleCopySource:
				result = Strings.SingleCopyDatabase;
				break;
			}
			return result;
		}

		// Token: 0x040022B3 RID: 8883
		private ReplayConfigType configType;
	}
}
