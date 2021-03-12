using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A9 RID: 681
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotImplementedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002309 RID: 8969 RVA: 0x0004DCF5 File Offset: 0x0004BEF5
		public NotImplementedPermanentException(string methodName) : base(MrsStrings.NotImplemented(methodName))
		{
			this.methodName = methodName;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0004DD0A File Offset: 0x0004BF0A
		public NotImplementedPermanentException(string methodName, Exception innerException) : base(MrsStrings.NotImplemented(methodName), innerException)
		{
			this.methodName = methodName;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0004DD20 File Offset: 0x0004BF20
		protected NotImplementedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.methodName = (string)info.GetValue("methodName", typeof(string));
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0004DD4A File Offset: 0x0004BF4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("methodName", this.methodName);
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x0004DD65 File Offset: 0x0004BF65
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x04000FAE RID: 4014
		private readonly string methodName;
	}
}
