using System;
using System.Text;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009C7 RID: 2503
	internal sealed class LicenseIdentity
	{
		// Token: 0x06003693 RID: 13971 RVA: 0x0008B1BE File Offset: 0x000893BE
		public LicenseIdentity(string email, string[] proxyAddresses)
		{
			this.Email = email;
			this.ProxyAddresses = proxyAddresses;
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x0008B1D4 File Offset: 0x000893D4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Email);
			if (this.ProxyAddresses != null && this.ProxyAddresses.Length > 0)
			{
				stringBuilder.Append(",");
				for (int i = 0; i < this.ProxyAddresses.Length; i++)
				{
					stringBuilder.Append(this.ProxyAddresses[i]);
					if (i != this.ProxyAddresses.Length - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x0008B254 File Offset: 0x00089454
		public static string ToString(LicenseIdentity[] licenseIdentities)
		{
			if (licenseIdentities == null)
			{
				throw new ArgumentNullException("licenseIdentities");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = Math.Min(licenseIdentities.Length, 10);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(licenseIdentities[i].ToString());
				if (i != num - 1)
				{
					stringBuilder.Append(" | ");
				}
			}
			if (num < licenseIdentities.Length)
			{
				stringBuilder.Append(" ... (" + licenseIdentities.Length + ")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002EBD RID: 11965
		private const int MaxLicenseIdentitiesToTrace = 10;

		// Token: 0x04002EBE RID: 11966
		public readonly string Email;

		// Token: 0x04002EBF RID: 11967
		public readonly string[] ProxyAddresses;
	}
}
