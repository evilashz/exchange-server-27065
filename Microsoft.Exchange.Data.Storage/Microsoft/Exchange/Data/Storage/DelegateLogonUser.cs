using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200028A RID: 650
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DelegateLogonUser
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x0007DA63 File Offset: 0x0007BC63
		internal DelegateLogonUser(IADOrgPerson adOrgPerson)
		{
			this.adOrgPerson = adOrgPerson;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0007DA72 File Offset: 0x0007BC72
		internal DelegateLogonUser(string legacyDn)
		{
			this.legacyDn = legacyDn;
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x0007DA81 File Offset: 0x0007BC81
		internal string LegacyDn
		{
			get
			{
				if (this.adOrgPerson != null)
				{
					return this.adOrgPerson.LegacyExchangeDN;
				}
				return this.legacyDn;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0007DA9D File Offset: 0x0007BC9D
		internal IADOrgPerson ADOrgPerson
		{
			get
			{
				return this.adOrgPerson;
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0007DAA5 File Offset: 0x0007BCA5
		public override string ToString()
		{
			return this.LegacyDn;
		}

		// Token: 0x040012E3 RID: 4835
		private readonly string legacyDn;

		// Token: 0x040012E4 RID: 4836
		private readonly IADOrgPerson adOrgPerson;
	}
}
