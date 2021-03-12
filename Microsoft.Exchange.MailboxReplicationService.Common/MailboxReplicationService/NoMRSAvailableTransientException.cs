using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002DF RID: 735
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoMRSAvailableTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x0004F8D5 File Offset: 0x0004DAD5
		public NoMRSAvailableTransientException() : base(MrsStrings.NoMRSAvailable)
		{
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0004F8E2 File Offset: 0x0004DAE2
		public NoMRSAvailableTransientException(Exception innerException) : base(MrsStrings.NoMRSAvailable, innerException)
		{
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0004F8F0 File Offset: 0x0004DAF0
		protected NoMRSAvailableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0004F8FA File Offset: 0x0004DAFA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
