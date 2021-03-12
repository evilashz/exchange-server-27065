using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ServerNotFoundException : MailboxLoadBalanceTransientException
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002673 File Offset: 0x00000873
		public ServerNotFoundException(string guid) : base(MigrationWorkflowServiceStrings.ErrorServerNotFound(guid))
		{
			this.guid = guid;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002688 File Offset: 0x00000888
		public ServerNotFoundException(string guid, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorServerNotFound(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000269E File Offset: 0x0000089E
		protected ServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (string)info.GetValue("guid", typeof(string));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026C8 File Offset: 0x000008C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026E3 File Offset: 0x000008E3
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly string guid;
	}
}
