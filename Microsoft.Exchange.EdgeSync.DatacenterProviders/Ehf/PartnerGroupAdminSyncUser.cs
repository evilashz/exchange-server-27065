using System;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000015 RID: 21
	internal class PartnerGroupAdminSyncUser : AdminSyncUser
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00006C3B File Offset: 0x00004E3B
		public PartnerGroupAdminSyncUser(string dn, Guid objectGuid, Guid partnerGroupGuid) : base(dn, objectGuid)
		{
			if (partnerGroupGuid == Guid.Empty)
			{
				throw new ArgumentException("PartnerGroupGuid cannot be emtpy");
			}
			this.partnerGroupGuid = partnerGroupGuid;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00006C64 File Offset: 0x00004E64
		public Guid PartnerGroupGuid
		{
			get
			{
				return this.partnerGroupGuid;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006C6C File Offset: 0x00004E6C
		public override string ToString()
		{
			return this.partnerGroupGuid.ToString();
		}

		// Token: 0x0400004B RID: 75
		private Guid partnerGroupGuid;
	}
}
