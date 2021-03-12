using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	public sealed class InvalidProxyAddress : ProxyAddress, IInvalidProxy
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x0001190D File Offset: 0x0000FB0D
		public InvalidProxyAddress(string proxyAddressString, ProxyAddressPrefix prefix, string address, bool isPrimaryAddress, ArgumentOutOfRangeException parseException) : base(prefix, address, isPrimaryAddress, true)
		{
			if (parseException == null)
			{
				throw new ArgumentNullException("parseException", "An invalid proxy address must contain the exception that makes it invalid.");
			}
			base.RawProxyAddressBaseString = proxyAddressString;
			this.parseException = parseException;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001193D File Offset: 0x0000FB3D
		public ArgumentOutOfRangeException ParseException
		{
			get
			{
				return this.parseException;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00011945 File Offset: 0x0000FB45
		public override string ToString()
		{
			return base.RawProxyAddressBaseString;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001194D File Offset: 0x0000FB4D
		public override ProxyAddressBase ToPrimary()
		{
			if (base.IsPrimaryAddress)
			{
				return this;
			}
			return new InvalidProxyAddress(null, base.Prefix, base.AddressString, true, this.ParseException);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00011972 File Offset: 0x0000FB72
		public override ProxyAddressBase ToSecondary()
		{
			if (base.IsPrimaryAddress)
			{
				return new InvalidProxyAddress(null, base.Prefix, base.AddressString, false, this.ParseException);
			}
			return this;
		}

		// Token: 0x04000305 RID: 773
		private ArgumentOutOfRangeException parseException;
	}
}
