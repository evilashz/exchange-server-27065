using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB3 RID: 2739
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LicensingRpcResults : RpcParameters
	{
		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x060063F5 RID: 25589 RVA: 0x001A7978 File Offset: 0x001A5B78
		public OverallRpcResult OverallRpcResult
		{
			get
			{
				if (this.overallRpcResult == null)
				{
					this.overallRpcResult = (base.GetParameterValue("OverallRpcResult") as OverallRpcResult);
					if (this.overallRpcResult == null)
					{
						throw new ArgumentNullException("OverallRpcResult");
					}
				}
				return this.overallRpcResult;
			}
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x001A79B1 File Offset: 0x001A5BB1
		public LicensingRpcResults(byte[] data) : base(data)
		{
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x001A79BA File Offset: 0x001A5BBA
		public LicensingRpcResults(OverallRpcResult overallRpcResult)
		{
			if (overallRpcResult == null)
			{
				throw new ArgumentNullException("overallRpcResult");
			}
			base.SetParameterValue("OverallRpcResult", overallRpcResult);
		}

		// Token: 0x040038AF RID: 14511
		private const string OverallRpcResultParameterName = "OverallRpcResult";

		// Token: 0x040038B0 RID: 14512
		private OverallRpcResult overallRpcResult;
	}
}
