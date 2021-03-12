using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000019 RID: 25
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ObjectCannotBeMovedException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002EB1 File Offset: 0x000010B1
		public ObjectCannotBeMovedException(string objectType, string objectIdentity) : base(MigrationWorkflowServiceStrings.ErrorObjectCannotBeMoved(objectType, objectIdentity))
		{
			this.objectType = objectType;
			this.objectIdentity = objectIdentity;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002ECE File Offset: 0x000010CE
		public ObjectCannotBeMovedException(string objectType, string objectIdentity, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorObjectCannotBeMoved(objectType, objectIdentity), innerException)
		{
			this.objectType = objectType;
			this.objectIdentity = objectIdentity;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002EEC File Offset: 0x000010EC
		protected ObjectCannotBeMovedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectType = (string)info.GetValue("objectType", typeof(string));
			this.objectIdentity = (string)info.GetValue("objectIdentity", typeof(string));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002F41 File Offset: 0x00001141
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectType", this.objectType);
			info.AddValue("objectIdentity", this.objectIdentity);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002F6D File Offset: 0x0000116D
		public string ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002F75 File Offset: 0x00001175
		public string ObjectIdentity
		{
			get
			{
				return this.objectIdentity;
			}
		}

		// Token: 0x04000032 RID: 50
		private readonly string objectType;

		// Token: 0x04000033 RID: 51
		private readonly string objectIdentity;
	}
}
