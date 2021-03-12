using System;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000016 RID: 22
	internal sealed class EhfCompanyIdentity
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006C7F File Offset: 0x00004E7F
		public int EhfCompanyId
		{
			get
			{
				return this.ehfCompanyId;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006C87 File Offset: 0x00004E87
		public Guid EhfCompanyGuid
		{
			get
			{
				return this.ehfCompanyGuid;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006C8F File Offset: 0x00004E8F
		public EhfCompanyIdentity(int companyId, Guid companyGuid)
		{
			if (companyGuid == Guid.Empty)
			{
				throw new ArgumentException("CompanyGuid cannot be empty", "companyGuid");
			}
			this.ehfCompanyId = companyId;
			this.ehfCompanyGuid = companyGuid;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006CC4 File Offset: 0x00004EC4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			EhfCompanyIdentity ehfCompanyIdentity = obj as EhfCompanyIdentity;
			return ehfCompanyIdentity != null && this.ehfCompanyGuid.Equals(ehfCompanyIdentity.ehfCompanyGuid);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006CF3 File Offset: 0x00004EF3
		public override int GetHashCode()
		{
			return this.ehfCompanyGuid.GetHashCode();
		}

		// Token: 0x0400004C RID: 76
		private int ehfCompanyId;

		// Token: 0x0400004D RID: 77
		private Guid ehfCompanyGuid;
	}
}
