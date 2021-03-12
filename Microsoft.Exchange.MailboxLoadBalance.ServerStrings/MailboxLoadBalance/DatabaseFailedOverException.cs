using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseFailedOverException : MailboxLoadBalanceTransientException
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000026EB File Offset: 0x000008EB
		public DatabaseFailedOverException(string guid) : base(MigrationWorkflowServiceStrings.ErrorDatabaseFailedOver(guid))
		{
			this.guid = guid;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002700 File Offset: 0x00000900
		public DatabaseFailedOverException(string guid, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorDatabaseFailedOver(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002716 File Offset: 0x00000916
		protected DatabaseFailedOverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (string)info.GetValue("guid", typeof(string));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002740 File Offset: 0x00000940
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000275B File Offset: 0x0000095B
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000020 RID: 32
		private readonly string guid;
	}
}
