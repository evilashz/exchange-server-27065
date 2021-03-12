using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public class UMSmartHost : SmartHost
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x000321D1 File Offset: 0x000303D1
		public UMSmartHost(string address) : base(address)
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x000321DA File Offset: 0x000303DA
		public string DisplayedAddress
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x000321E2 File Offset: 0x000303E2
		public new static UMSmartHost Parse(string address)
		{
			return new UMSmartHost(address);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000321EC File Offset: 0x000303EC
		public static bool IsValidAddress(string address)
		{
			SmartHost smartHost;
			return !address.EndsWith(".") && SmartHost.TryParse(address, out smartHost);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00032215 File Offset: 0x00030415
		public override string ToString()
		{
			if (base.IsIPAddress)
			{
				return this.IpAddressToString();
			}
			return base.Domain.ToString();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00032231 File Offset: 0x00030431
		private string IpAddressToString()
		{
			return base.Address.ToString();
		}
	}
}
