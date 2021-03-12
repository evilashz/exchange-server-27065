using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000014 RID: 20
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EntityIsNonMovableException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public EntityIsNonMovableException(string orgId, string userId) : base(MigrationWorkflowServiceStrings.ErrorEntityNotMovable(orgId, userId))
		{
			this.orgId = orgId;
			this.userId = userId;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BC2 File Offset: 0x00000DC2
		public EntityIsNonMovableException(string orgId, string userId, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorEntityNotMovable(orgId, userId), innerException)
		{
			this.orgId = orgId;
			this.userId = userId;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BE0 File Offset: 0x00000DE0
		protected EntityIsNonMovableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgId = (string)info.GetValue("orgId", typeof(string));
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C35 File Offset: 0x00000E35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgId", this.orgId);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C61 File Offset: 0x00000E61
		public string OrgId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002C69 File Offset: 0x00000E69
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x0400002B RID: 43
		private readonly string orgId;

		// Token: 0x0400002C RID: 44
		private readonly string userId;
	}
}
