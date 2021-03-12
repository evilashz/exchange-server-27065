using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A0 RID: 672
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ResourceReservationException : MailboxReplicationTransientException
	{
		// Token: 0x060022DF RID: 8927 RVA: 0x0004D9A2 File Offset: 0x0004BBA2
		public ResourceReservationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0004D9AB File Offset: 0x0004BBAB
		public ResourceReservationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0004D9B5 File Offset: 0x0004BBB5
		protected ResourceReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0004D9BF File Offset: 0x0004BBBF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
