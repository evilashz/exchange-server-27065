using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB8 RID: 2744
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcquireUseLicensesRpcResults : LicensingRpcResults
	{
		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06006406 RID: 25606 RVA: 0x001A7D57 File Offset: 0x001A5F57
		public UseLicenseRpcResult[] UseLicenseRpcResults
		{
			get
			{
				if (this.useLicenseRpcResults == null)
				{
					this.useLicenseRpcResults = (base.GetParameterValue("UseLicenseRpcResult") as UseLicenseRpcResult[]);
				}
				return this.useLicenseRpcResults;
			}
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x001A7D7D File Offset: 0x001A5F7D
		public AcquireUseLicensesRpcResults(byte[] data) : base(data)
		{
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x001A7D88 File Offset: 0x001A5F88
		public AcquireUseLicensesRpcResults(OverallRpcResult overallRpcResult, UseLicenseResult[] originalResults) : base(overallRpcResult)
		{
			if (overallRpcResult == null)
			{
				throw new ArgumentNullException("overallRpcResult");
			}
			if (originalResults == null)
			{
				throw new ArgumentNullException("originalResults");
			}
			UseLicenseRpcResult[] array = new UseLicenseRpcResult[originalResults.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new UseLicenseRpcResult(originalResults[i]);
			}
			base.SetParameterValue("UseLicenseRpcResult", array);
		}

		// Token: 0x040038BD RID: 14525
		private const string UseLicenseRpcResultParameterName = "UseLicenseRpcResult";

		// Token: 0x040038BE RID: 14526
		private UseLicenseRpcResult[] useLicenseRpcResults;
	}
}
