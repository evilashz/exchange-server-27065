using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000053 RID: 83
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3TransientLoginDelayedAuthErrorException : LocalizedException
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00004750 File Offset: 0x00002950
		public Pop3TransientLoginDelayedAuthErrorException() : base(CXStrings.Pop3TransientLoginDelayedAuthErrorMsg)
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000475D File Offset: 0x0000295D
		public Pop3TransientLoginDelayedAuthErrorException(Exception innerException) : base(CXStrings.Pop3TransientLoginDelayedAuthErrorMsg, innerException)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000476B File Offset: 0x0000296B
		protected Pop3TransientLoginDelayedAuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004775 File Offset: 0x00002975
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
