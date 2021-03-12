using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public sealed class CustomProxyAddress : ProxyAddress
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001115B File Offset: 0x0000F35B
		public CustomProxyAddress(CustomProxyAddressPrefix prefix, string address, bool isPrimaryAddress) : base(prefix, address, isPrimaryAddress)
		{
			if (string.IsNullOrEmpty(prefix.PrimaryPrefix))
			{
				throw new ArgumentOutOfRangeException(DataStrings.ExceptionEmptyPrefix(address), null);
			}
		}
	}
}
