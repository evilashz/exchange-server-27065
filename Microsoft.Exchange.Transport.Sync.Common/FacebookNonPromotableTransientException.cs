using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200004B RID: 75
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FacebookNonPromotableTransientException : NonPromotableTransientException
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00006188 File Offset: 0x00004388
		public FacebookNonPromotableTransientException() : base(Strings.FacebookNonPromotableTransientException)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006195 File Offset: 0x00004395
		public FacebookNonPromotableTransientException(Exception innerException) : base(Strings.FacebookNonPromotableTransientException, innerException)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000061A3 File Offset: 0x000043A3
		protected FacebookNonPromotableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000061AD File Offset: 0x000043AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
