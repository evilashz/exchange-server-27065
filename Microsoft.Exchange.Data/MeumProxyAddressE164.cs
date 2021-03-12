using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	public sealed class MeumProxyAddressE164 : MeumProxyAddress
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00011A2F File Offset: 0x0000FC2F
		public MeumProxyAddressE164(string address, bool primaryAddress) : base(address, primaryAddress)
		{
			if (!MeumProxyAddressE164.ValidateAddress(address))
			{
				throw new ArgumentOutOfRangeException(DataStrings.ExceptionInvalidMeumAddress(address ?? "<null>"), null);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00011A5C File Offset: 0x0000FC5C
		internal static MeumProxyAddressE164 CreateFromE164(string phoneNumber, bool primaryAddress)
		{
			if (!MeumProxyAddressE164.ValidateE164Number(phoneNumber))
			{
				throw new ArgumentOutOfRangeException("phoneNumber", phoneNumber, "Invalid E164 number");
			}
			SmtpAddress smtpAddress = new SmtpAddress(phoneNumber.Substring(1), "um.exchangelabs.com");
			if (!smtpAddress.IsValidAddress)
			{
				throw new ArgumentOutOfRangeException("phoneNumber", phoneNumber, string.Format("Invalid SMTP address - {0}", smtpAddress.ToString()));
			}
			return new MeumProxyAddressE164(smtpAddress.ToString(), primaryAddress);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		internal static bool ValidateAddress(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return false;
			}
			SmtpAddress smtpAddress = new SmtpAddress(address);
			return smtpAddress.IsValidAddress && MeumProxyAddressE164.IsNumber(smtpAddress.Local) && string.Equals(smtpAddress.Domain, "um.exchangelabs.com", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00011B21 File Offset: 0x0000FD21
		internal static bool ValidateE164Number(string phoneNumber)
		{
			return !string.IsNullOrEmpty(phoneNumber) && phoneNumber[0] == '+' && phoneNumber.Length > 1 && MeumProxyAddressE164.IsNumber(phoneNumber.Substring(1));
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00011B50 File Offset: 0x0000FD50
		private static bool IsNumber(string phoneNumber)
		{
			if (string.IsNullOrEmpty(phoneNumber))
			{
				return false;
			}
			for (int i = 0; i < phoneNumber.Length; i++)
			{
				if (!char.IsDigit(phoneNumber[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000307 RID: 775
		private const string Domain = "um.exchangelabs.com";
	}
}
