using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200036C RID: 876
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToFetchMimeStreamException : MailboxReplicationTransientException
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x00053C30 File Offset: 0x00051E30
		public UnableToFetchMimeStreamException(string identity) : base(MrsStrings.UnableToFetchMimeStream(identity))
		{
			this.identity = identity;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x00053C45 File Offset: 0x00051E45
		public UnableToFetchMimeStreamException(string identity, Exception innerException) : base(MrsStrings.UnableToFetchMimeStream(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x00053C5B File Offset: 0x00051E5B
		protected UnableToFetchMimeStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00053C85 File Offset: 0x00051E85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00053CA0 File Offset: 0x00051EA0
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04001072 RID: 4210
		private readonly string identity;
	}
}
