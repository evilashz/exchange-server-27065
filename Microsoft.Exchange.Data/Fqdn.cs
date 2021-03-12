using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public class Fqdn : SmtpDomain
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x00023076 File Offset: 0x00021276
		public Fqdn(string fqdn) : base(fqdn)
		{
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002307F File Offset: 0x0002127F
		public new static Fqdn Parse(string fqdn)
		{
			return new Fqdn(fqdn);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00023087 File Offset: 0x00021287
		public static bool TryParse(string fqdn, out Fqdn obj)
		{
			if (Fqdn.IsValidFqdn(fqdn))
			{
				obj = new Fqdn(fqdn);
				return true;
			}
			obj = null;
			return false;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002309F File Offset: 0x0002129F
		public static bool IsValidFqdn(string fqdn)
		{
			return SmtpAddress.IsValidDomain(fqdn);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000230A7 File Offset: 0x000212A7
		public static implicit operator string(Fqdn fqdn)
		{
			if (fqdn != null)
			{
				return fqdn.Domain;
			}
			return null;
		}
	}
}
