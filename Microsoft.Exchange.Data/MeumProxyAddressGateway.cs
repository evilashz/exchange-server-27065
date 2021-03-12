using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public sealed class MeumProxyAddressGateway : MeumProxyAddress
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00011BD7 File Offset: 0x0000FDD7
		public MeumProxyAddressGateway(string address, bool primaryAddress) : base(address, primaryAddress)
		{
			if (!MeumProxyAddressGateway.ValidateAddress(address))
			{
				throw new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidMeumAddress(address ?? "<null>"), null);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00011C04 File Offset: 0x0000FE04
		internal static MeumProxyAddressGateway CreateFromGuid(Guid gatewayObjectGuid, bool primaryAddress)
		{
			SmtpAddress smtpAddress = new SmtpAddress(gatewayObjectGuid.ToString("D").ToLowerInvariant(), "umgateway.exchangelabs.com");
			if (!smtpAddress.IsValidAddress)
			{
				throw new ArgumentOutOfRangeException("gatewayObjectGuid", gatewayObjectGuid, string.Format("Invalid SMTP address - {0}", smtpAddress.ToString()));
			}
			return new MeumProxyAddressGateway(smtpAddress.ToString(), primaryAddress);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011C74 File Offset: 0x0000FE74
		internal static bool ValidateAddress(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return false;
			}
			SmtpAddress smtpAddress = new SmtpAddress(address);
			if (!smtpAddress.IsValidAddress)
			{
				return false;
			}
			Guid empty = Guid.Empty;
			return GuidHelper.TryParseGuid(smtpAddress.Local, out empty) && string.Equals(smtpAddress.Domain, "umgateway.exchangelabs.com", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000308 RID: 776
		private const string Domain = "umgateway.exchangelabs.com";
	}
}
