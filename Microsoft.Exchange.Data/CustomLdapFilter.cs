using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal class CustomLdapFilter : QueryFilter
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000725F File Offset: 0x0000545F
		public CustomLdapFilter(string ldapFilter)
		{
			if (string.IsNullOrEmpty(ldapFilter))
			{
				throw new ArgumentNullException("ldapFilter");
			}
			this.ldapFilter = ldapFilter;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007281 File Offset: 0x00005481
		public string LdapFilter
		{
			get
			{
				return this.ldapFilter;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007289 File Offset: 0x00005489
		public override void ToString(StringBuilder sb)
		{
			sb.Append("([LDAP](");
			sb.Append(this.ldapFilter);
			sb.Append("))");
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000072B0 File Offset: 0x000054B0
		public override bool Equals(object obj)
		{
			CustomLdapFilter customLdapFilter = obj as CustomLdapFilter;
			return customLdapFilter != null && this.LdapFilter.Equals(customLdapFilter.LdapFilter, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000072DB File Offset: 0x000054DB
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.LdapFilter.GetHashCode();
		}

		// Token: 0x04000091 RID: 145
		private readonly string ldapFilter;
	}
}
