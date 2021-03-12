using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RecipientNotFoundException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002853 File Offset: 0x00000A53
		public RecipientNotFoundException(string userId) : base(MigrationWorkflowServiceStrings.ErrorRecipientNotFound(userId))
		{
			this.userId = userId;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002868 File Offset: 0x00000A68
		public RecipientNotFoundException(string userId, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorRecipientNotFound(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000287E File Offset: 0x00000A7E
		protected RecipientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028A8 File Offset: 0x00000AA8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000028C3 File Offset: 0x00000AC3
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04000023 RID: 35
		private readonly string userId;
	}
}
