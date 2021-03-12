using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C5 RID: 197
	internal static class MeumProxyAddressFactory
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00011B89 File Offset: 0x0000FD89
		public static MeumProxyAddress CreateFromAddressString(string address, bool primaryAddress)
		{
			if (MeumProxyAddressE164.ValidateAddress(address))
			{
				return new MeumProxyAddressE164(address, primaryAddress);
			}
			if (MeumProxyAddressGateway.ValidateAddress(address))
			{
				return new MeumProxyAddressGateway(address, primaryAddress);
			}
			throw new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidMeumAddress(address ?? "<null>"), null);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00011BC5 File Offset: 0x0000FDC5
		public static MeumProxyAddress CreateFromE164(string phoneNumber, bool primaryAddress)
		{
			return MeumProxyAddressE164.CreateFromE164(phoneNumber, primaryAddress);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00011BCE File Offset: 0x0000FDCE
		public static MeumProxyAddress CreateFromGatewayGuid(Guid gatewayObjectGuid, bool primaryAddress)
		{
			return MeumProxyAddressGateway.CreateFromGuid(gatewayObjectGuid, primaryAddress);
		}
	}
}
