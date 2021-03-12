using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200008A RID: 138
	internal class AzureHubPayload
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F8B2 File Offset: 0x0000DAB2
		public AzureHubPayload(AzureSasKey[] sasKeys)
		{
			ArgumentValidator.ThrowIfNull("sasKeys", sasKeys);
			ArgumentValidator.ThrowIfOutOfRange<int>("sasKeys.Length", sasKeys.Length, 0, int.MaxValue);
			this.SasKeys = sasKeys;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000F8DF File Offset: 0x0000DADF
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000F8E7 File Offset: 0x0000DAE7
		public AzureSasKey[] SasKeys { get; private set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = this.SasKeys.ToNullableString(null);
			}
			return this.toString;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000F914 File Offset: 0x0000DB14
		internal void WriteAzureHubPayload(AzureHubPayloadWriter apw)
		{
			ArgumentValidator.ThrowIfNull("apw", apw);
			foreach (AzureSasKey sasKey in this.SasKeys)
			{
				apw.AddAuthorizationRule(sasKey);
			}
		}

		// Token: 0x04000251 RID: 593
		private string toString;
	}
}
