using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009DB RID: 2523
	internal class OrgAndObjectId : IEquatable<OrgAndObjectId>
	{
		// Token: 0x060074CB RID: 29899 RVA: 0x001811BC File Offset: 0x0017F3BC
		private OrgAndObjectId()
		{
		}

		// Token: 0x060074CC RID: 29900 RVA: 0x001811C4 File Offset: 0x0017F3C4
		public OrgAndObjectId(OrganizationId orgId, ADObjectId objectId)
		{
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			this.OrganizationId = orgId;
			this.Id = objectId;
			this.idString = string.Format("Org: {0}, Id: {1}", orgId, (objectId == null) ? "<NULL>" : objectId.DistinguishedName);
		}

		// Token: 0x17002998 RID: 10648
		// (get) Token: 0x060074CD RID: 29901 RVA: 0x0018121A File Offset: 0x0017F41A
		// (set) Token: 0x060074CE RID: 29902 RVA: 0x00181222 File Offset: 0x0017F422
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17002999 RID: 10649
		// (get) Token: 0x060074CF RID: 29903 RVA: 0x0018122B File Offset: 0x0017F42B
		// (set) Token: 0x060074D0 RID: 29904 RVA: 0x00181233 File Offset: 0x0017F433
		public ADObjectId Id { get; private set; }

		// Token: 0x060074D1 RID: 29905 RVA: 0x0018123C File Offset: 0x0017F43C
		public override string ToString()
		{
			return this.idString;
		}

		// Token: 0x060074D2 RID: 29906 RVA: 0x00181244 File Offset: 0x0017F444
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OrgAndObjectId);
		}

		// Token: 0x060074D3 RID: 29907 RVA: 0x00181254 File Offset: 0x0017F454
		public bool Equals(OrgAndObjectId other)
		{
			if (other == null || this.OrganizationId == null || other.OrganizationId == null)
			{
				return false;
			}
			if (this.OrganizationId.Equals(other.OrganizationId))
			{
				if (this.Id == null)
				{
					if (other.Id == null)
					{
						return true;
					}
				}
				else if (this.Id.Equals(other.Id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060074D4 RID: 29908 RVA: 0x001812BD File Offset: 0x0017F4BD
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x04004B3B RID: 19259
		private readonly string idString;
	}
}
