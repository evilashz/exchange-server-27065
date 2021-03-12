using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000CC RID: 204
	[Serializable]
	internal sealed class SmtpProxyAddressPrefix : ProxyAddressPrefix
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x00012D9A File Offset: 0x00010F9A
		internal SmtpProxyAddressPrefix() : base("SMTP")
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00012DA7 File Offset: 0x00010FA7
		public override ProxyAddress GetProxyAddress(string address, bool isPrimaryAddress)
		{
			if (address.IndexOf('@') == -1)
			{
				return new InvalidProxyAddress(null, ProxyAddressPrefix.Smtp, address, isPrimaryAddress, new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidSmtpAddress(address), null));
			}
			return new SmtpProxyAddress(address, isPrimaryAddress);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00012DDA File Offset: 0x00010FDA
		public override ProxyAddressTemplate GetProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress)
		{
			return new SmtpProxyAddressTemplate(addressTemplate, isPrimaryAddress);
		}
	}
}
