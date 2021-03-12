using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200012E RID: 302
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxWrongTypeException : MailboxNotFoundException
	{
		// Token: 0x0600147C RID: 5244 RVA: 0x0006AF1B File Offset: 0x0006911B
		public MailboxWrongTypeException(string externalDirectoryObjectId, string type) : base(ServerStrings.InvalidMailboxType(externalDirectoryObjectId, type))
		{
			this.externalDirectoryObjectId = externalDirectoryObjectId;
			this.type = type;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0006AF38 File Offset: 0x00069138
		public MailboxWrongTypeException(string externalDirectoryObjectId, string type, Exception innerException) : base(ServerStrings.InvalidMailboxType(externalDirectoryObjectId, type), innerException)
		{
			this.externalDirectoryObjectId = externalDirectoryObjectId;
			this.type = type;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0006AF58 File Offset: 0x00069158
		protected MailboxWrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.externalDirectoryObjectId = (string)info.GetValue("externalDirectoryObjectId", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0006AFAD File Offset: 0x000691AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("externalDirectoryObjectId", this.externalDirectoryObjectId);
			info.AddValue("type", this.type);
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0006AFD9 File Offset: 0x000691D9
		public string ExternalDirectoryObjectId
		{
			get
			{
				return this.externalDirectoryObjectId;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0006AFE1 File Offset: 0x000691E1
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040009C2 RID: 2498
		private readonly string externalDirectoryObjectId;

		// Token: 0x040009C3 RID: 2499
		private readonly string type;
	}
}
