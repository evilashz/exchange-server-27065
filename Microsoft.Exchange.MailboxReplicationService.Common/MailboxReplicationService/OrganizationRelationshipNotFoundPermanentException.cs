using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002DA RID: 730
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationRelationshipNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023FF RID: 9215 RVA: 0x0004F52E File Offset: 0x0004D72E
		public OrganizationRelationshipNotFoundPermanentException(string domain, string orgId) : base(MrsStrings.OrganizationRelationshipNotFound(domain, orgId))
		{
			this.domain = domain;
			this.orgId = orgId;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0004F54B File Offset: 0x0004D74B
		public OrganizationRelationshipNotFoundPermanentException(string domain, string orgId, Exception innerException) : base(MrsStrings.OrganizationRelationshipNotFound(domain, orgId), innerException)
		{
			this.domain = domain;
			this.orgId = orgId;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0004F56C File Offset: 0x0004D76C
		protected OrganizationRelationshipNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.orgId = (string)info.GetValue("orgId", typeof(string));
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0004F5C1 File Offset: 0x0004D7C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domain", this.domain);
			info.AddValue("orgId", this.orgId);
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x0004F5ED File Offset: 0x0004D7ED
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0004F5F5 File Offset: 0x0004D7F5
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x04000FE0 RID: 4064
		private readonly string domain;

		// Token: 0x04000FE1 RID: 4065
		private readonly string orgId;
	}
}
