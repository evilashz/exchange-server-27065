using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedCommunicationException : ThirdPartyReplicationException
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000308B File Offset: 0x0000128B
		public FailedCommunicationException(string reason) : base(ThirdPartyReplication.FailedCommunication(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030A5 File Offset: 0x000012A5
		public FailedCommunicationException(string reason, Exception innerException) : base(ThirdPartyReplication.FailedCommunication(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030C0 File Offset: 0x000012C0
		protected FailedCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000030EA File Offset: 0x000012EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003105 File Offset: 0x00001305
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000020 RID: 32
		private readonly string reason;
	}
}
