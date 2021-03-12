using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002A1 RID: 673
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetCompletedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022E3 RID: 8931 RVA: 0x0004D9C9 File Offset: 0x0004BBC9
		public CannotSetCompletedPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0004D9D2 File Offset: 0x0004BBD2
		public CannotSetCompletedPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0004D9DC File Offset: 0x0004BBDC
		protected CannotSetCompletedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0004D9E6 File Offset: 0x0004BBE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
