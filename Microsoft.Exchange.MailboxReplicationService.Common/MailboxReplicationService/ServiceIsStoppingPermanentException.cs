using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002EC RID: 748
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceIsStoppingPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600245E RID: 9310 RVA: 0x0004FF79 File Offset: 0x0004E179
		public ServiceIsStoppingPermanentException() : base(MrsStrings.ServiceIsStopping)
		{
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0004FF86 File Offset: 0x0004E186
		public ServiceIsStoppingPermanentException(Exception innerException) : base(MrsStrings.ServiceIsStopping, innerException)
		{
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0004FF94 File Offset: 0x0004E194
		protected ServiceIsStoppingPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0004FF9E File Offset: 0x0004E19E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
