using System;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000013 RID: 19
	internal class AdminSyncUser
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00006BDC File Offset: 0x00004DDC
		public AdminSyncUser(string distinguishedName, Guid objectGuid)
		{
			if (objectGuid == Guid.Empty)
			{
				throw new ArgumentException("objectGuid cannot be empty");
			}
			this.userGuid = objectGuid;
			this.distinguishedName = distinguishedName;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006C0A File Offset: 0x00004E0A
		public Guid UserGuid
		{
			get
			{
				return this.userGuid;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006C12 File Offset: 0x00004E12
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x04000048 RID: 72
		private Guid userGuid;

		// Token: 0x04000049 RID: 73
		private string distinguishedName;
	}
}
