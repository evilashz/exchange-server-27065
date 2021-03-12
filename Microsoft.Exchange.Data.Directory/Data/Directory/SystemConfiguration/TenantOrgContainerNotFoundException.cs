using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A93 RID: 2707
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantOrgContainerNotFoundException : TenantContainerNotFoundException
	{
		// Token: 0x06007FAE RID: 32686 RVA: 0x001A451D File Offset: 0x001A271D
		public TenantOrgContainerNotFoundException(string orgId) : base(DirectoryStrings.TenantOrgContainerNotFoundException(orgId))
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x001A4532 File Offset: 0x001A2732
		public TenantOrgContainerNotFoundException(string orgId, Exception innerException) : base(DirectoryStrings.TenantOrgContainerNotFoundException(orgId), innerException)
		{
			this.orgId = orgId;
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x001A4548 File Offset: 0x001A2748
		protected TenantOrgContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (string)info.GetValue("orgId", typeof(string));
		}

		// Token: 0x06007FB1 RID: 32689 RVA: 0x001A4572 File Offset: 0x001A2772
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x17002EB9 RID: 11961
		// (get) Token: 0x06007FB2 RID: 32690 RVA: 0x001A458D File Offset: 0x001A278D
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x04005593 RID: 21907
		private readonly string orgId;
	}
}
