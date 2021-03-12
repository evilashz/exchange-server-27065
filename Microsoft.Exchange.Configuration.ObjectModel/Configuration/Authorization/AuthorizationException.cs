using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D3 RID: 723
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthorizationException : LocalizedException
	{
		// Token: 0x06001989 RID: 6537 RVA: 0x0005D4D1 File Offset: 0x0005B6D1
		public AuthorizationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005D4DA File Offset: 0x0005B6DA
		public AuthorizationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005D4E4 File Offset: 0x0005B6E4
		protected AuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005D4EE File Offset: 0x0005B6EE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
