using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleRecipientFoundException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000028CB File Offset: 0x00000ACB
		public MultipleRecipientFoundException(string userId) : base(MigrationWorkflowServiceStrings.ErrorMultipleRecipientFound(userId))
		{
			this.userId = userId;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028E0 File Offset: 0x00000AE0
		public MultipleRecipientFoundException(string userId, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorMultipleRecipientFound(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028F6 File Offset: 0x00000AF6
		protected MultipleRecipientFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002920 File Offset: 0x00000B20
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000293B File Offset: 0x00000B3B
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04000024 RID: 36
		private readonly string userId;
	}
}
