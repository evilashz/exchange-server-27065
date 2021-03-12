using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D4 RID: 724
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadADTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060023E1 RID: 9185 RVA: 0x0004F22E File Offset: 0x0004D42E
		public UnableToReadADTransientException() : base(MrsStrings.UnableToReadAD)
		{
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0004F23B File Offset: 0x0004D43B
		public UnableToReadADTransientException(Exception innerException) : base(MrsStrings.UnableToReadAD, innerException)
		{
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0004F249 File Offset: 0x0004D449
		protected UnableToReadADTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x0004F253 File Offset: 0x0004D453
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
