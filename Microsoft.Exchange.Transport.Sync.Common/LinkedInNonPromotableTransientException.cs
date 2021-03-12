using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200004C RID: 76
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LinkedInNonPromotableTransientException : NonPromotableTransientException
	{
		// Token: 0x06000209 RID: 521 RVA: 0x000061B7 File Offset: 0x000043B7
		public LinkedInNonPromotableTransientException() : base(Strings.LinkedInNonPromotableTransientException)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000061C4 File Offset: 0x000043C4
		public LinkedInNonPromotableTransientException(Exception innerException) : base(Strings.LinkedInNonPromotableTransientException, innerException)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000061D2 File Offset: 0x000043D2
		protected LinkedInNonPromotableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000061DC File Offset: 0x000043DC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
