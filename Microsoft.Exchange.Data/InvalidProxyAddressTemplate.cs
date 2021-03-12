using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	public sealed class InvalidProxyAddressTemplate : ProxyAddressTemplate, IInvalidProxy
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00011997 File Offset: 0x0000FB97
		public InvalidProxyAddressTemplate(string proxyAddressTemplateString, ProxyAddressPrefix prefix, string addressTemplate, bool isPrimaryAddress, ArgumentOutOfRangeException parseException) : base(prefix, addressTemplate, isPrimaryAddress)
		{
			if (parseException == null)
			{
				throw new ArgumentNullException("parseException", "An invalid proxy address template must contain the exception that makes it invalid.");
			}
			base.RawProxyAddressBaseString = proxyAddressTemplateString;
			this.parseException = parseException;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000119C6 File Offset: 0x0000FBC6
		public ArgumentOutOfRangeException ParseException
		{
			get
			{
				return this.parseException;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000119CE File Offset: 0x0000FBCE
		public override string ToString()
		{
			return base.RawProxyAddressBaseString;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000119D6 File Offset: 0x0000FBD6
		public override ProxyAddressBase ToPrimary()
		{
			if (base.IsPrimaryAddress)
			{
				return this;
			}
			return new InvalidProxyAddressTemplate(null, base.Prefix, base.AddressTemplateString, true, this.ParseException);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000119FB File Offset: 0x0000FBFB
		public override ProxyAddressBase ToSecondary()
		{
			if (base.IsPrimaryAddress)
			{
				return new InvalidProxyAddressTemplate(null, base.Prefix, base.AddressTemplateString, false, this.ParseException);
			}
			return this;
		}

		// Token: 0x04000306 RID: 774
		private ArgumentOutOfRangeException parseException;
	}
}
