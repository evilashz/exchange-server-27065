using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200001E RID: 30
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotAuthorizedException : ThirdPartyReplicationException
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003593 File Offset: 0x00001793
		public NotAuthorizedException() : base(ThirdPartyReplication.NotAuthorizedError)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035A5 File Offset: 0x000017A5
		public NotAuthorizedException(Exception innerException) : base(ThirdPartyReplication.NotAuthorizedError, innerException)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035B8 File Offset: 0x000017B8
		protected NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035C2 File Offset: 0x000017C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
