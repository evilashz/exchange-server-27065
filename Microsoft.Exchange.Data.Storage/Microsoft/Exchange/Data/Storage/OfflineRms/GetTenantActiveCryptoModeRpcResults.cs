using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB6 RID: 2742
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetTenantActiveCryptoModeRpcResults : LicensingRpcResults
	{
		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x060063FE RID: 25598 RVA: 0x001A7B28 File Offset: 0x001A5D28
		public ActiveCryptoModeRpcResult[] ActiveCryptoModeRpcResults
		{
			get
			{
				if (this.activeCryptoModeRpcResult == null)
				{
					this.activeCryptoModeRpcResult = (base.GetParameterValue("GetTenantActiveCryptoModeRpcResult") as ActiveCryptoModeRpcResult[]);
				}
				return this.activeCryptoModeRpcResult;
			}
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x001A7B4E File Offset: 0x001A5D4E
		public GetTenantActiveCryptoModeRpcResults(byte[] data) : base(data)
		{
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x001A7B58 File Offset: 0x001A5D58
		public GetTenantActiveCryptoModeRpcResults(OverallRpcResult overallRpcResult, ActiveCryptoModeResult[] originalResults) : base(overallRpcResult)
		{
			if (overallRpcResult == null)
			{
				throw new ArgumentNullException("overallRpcResult");
			}
			if (originalResults == null)
			{
				throw new ArgumentNullException("originalResults");
			}
			ActiveCryptoModeRpcResult[] array = new ActiveCryptoModeRpcResult[originalResults.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ActiveCryptoModeRpcResult(originalResults[i]);
			}
			base.SetParameterValue("GetTenantActiveCryptoModeRpcResult", array);
		}

		// Token: 0x040038B5 RID: 14517
		private const string GetTenantActiveCryptoModeResultParameterName = "GetTenantActiveCryptoModeRpcResult";

		// Token: 0x040038B6 RID: 14518
		private ActiveCryptoModeRpcResult[] activeCryptoModeRpcResult;
	}
}
