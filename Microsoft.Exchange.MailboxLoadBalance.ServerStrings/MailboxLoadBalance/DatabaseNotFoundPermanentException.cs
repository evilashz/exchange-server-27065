using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseNotFoundPermanentException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002763 File Offset: 0x00000963
		public DatabaseNotFoundPermanentException(string guid) : base(MigrationWorkflowServiceStrings.ErrorDatabaseNotFound(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002778 File Offset: 0x00000978
		public DatabaseNotFoundPermanentException(string guid, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorDatabaseNotFound(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000278E File Offset: 0x0000098E
		protected DatabaseNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (string)info.GetValue("guid", typeof(string));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027B8 File Offset: 0x000009B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027D3 File Offset: 0x000009D3
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000021 RID: 33
		private readonly string guid;
	}
}
