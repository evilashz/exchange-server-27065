using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public sealed class EumProxyAddressTemplate : ProxyAddressTemplate
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x00011896 File Offset: 0x0000FA96
		public EumProxyAddressTemplate(string addressTemplate, bool isPrimaryAddress) : base(ProxyAddressPrefix.UM, addressTemplate, isPrimaryAddress)
		{
			if (!EumProxyAddressTemplate.IsValidEumAddressTemplate(addressTemplate))
			{
				throw new ArgumentOutOfRangeException(DataStrings.InvalidEumAddressTemplateFormat(addressTemplate), null);
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000118C0 File Offset: 0x0000FAC0
		public static bool IsValidEumAddressTemplate(string eumAddressTemplate)
		{
			if (eumAddressTemplate == null)
			{
				throw new ArgumentNullException("eumAddressTemplate");
			}
			int num = eumAddressTemplate.IndexOf("phone-context=");
			bool result = false;
			if (num != -1)
			{
				string value = eumAddressTemplate.Substring(num + "phone-context=".Length + 1);
				result = !string.IsNullOrEmpty(value);
			}
			return result;
		}
	}
}
