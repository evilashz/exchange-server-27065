using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DagNotFoundException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000027DB File Offset: 0x000009DB
		public DagNotFoundException(string guid) : base(MigrationWorkflowServiceStrings.ErrorDagNotFound(guid))
		{
			this.guid = guid;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027F0 File Offset: 0x000009F0
		public DagNotFoundException(string guid, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorDagNotFound(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002806 File Offset: 0x00000A06
		protected DagNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (string)info.GetValue("guid", typeof(string));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002830 File Offset: 0x00000A30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000284B File Offset: 0x00000A4B
		public string Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04000022 RID: 34
		private readonly string guid;
	}
}
