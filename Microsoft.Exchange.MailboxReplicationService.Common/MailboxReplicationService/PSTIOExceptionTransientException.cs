using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000349 RID: 841
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PSTIOExceptionTransientException : MailboxReplicationPermanentException
	{
		// Token: 0x06002624 RID: 9764 RVA: 0x00052990 File Offset: 0x00050B90
		public PSTIOExceptionTransientException() : base(MrsStrings.PSTIOException)
		{
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0005299D File Offset: 0x00050B9D
		public PSTIOExceptionTransientException(Exception innerException) : base(MrsStrings.PSTIOException, innerException)
		{
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000529AB File Offset: 0x00050BAB
		protected PSTIOExceptionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000529B5 File Offset: 0x00050BB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
