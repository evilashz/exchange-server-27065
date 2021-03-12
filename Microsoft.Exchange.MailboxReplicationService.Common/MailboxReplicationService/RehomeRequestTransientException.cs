using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000326 RID: 806
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RehomeRequestTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002572 RID: 9586 RVA: 0x0005175D File Offset: 0x0004F95D
		public RehomeRequestTransientException() : base(MrsStrings.RehomeRequestFailure)
		{
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x0005176A File Offset: 0x0004F96A
		public RehomeRequestTransientException(Exception innerException) : base(MrsStrings.RehomeRequestFailure, innerException)
		{
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00051778 File Offset: 0x0004F978
		protected RehomeRequestTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00051782 File Offset: 0x0004F982
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
