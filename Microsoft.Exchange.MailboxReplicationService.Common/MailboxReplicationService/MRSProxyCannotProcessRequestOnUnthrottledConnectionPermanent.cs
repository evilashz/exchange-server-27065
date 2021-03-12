using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E6 RID: 742
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSProxyCannotProcessRequestOnUnthrottledConnectionPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002441 RID: 9281 RVA: 0x0004FCD5 File Offset: 0x0004DED5
		public MRSProxyCannotProcessRequestOnUnthrottledConnectionPermanentException() : base(MrsStrings.MRSProxyConnectionNotThrottledError)
		{
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x0004FCE2 File Offset: 0x0004DEE2
		public MRSProxyCannotProcessRequestOnUnthrottledConnectionPermanentException(Exception innerException) : base(MrsStrings.MRSProxyConnectionNotThrottledError, innerException)
		{
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x0004FCF0 File Offset: 0x0004DEF0
		protected MRSProxyCannotProcessRequestOnUnthrottledConnectionPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x0004FCFA File Offset: 0x0004DEFA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
