using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000059 RID: 89
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileServiceUserException : MobileServicePermanentException
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000CB26 File Offset: 0x0000AD26
		public MobileServiceUserException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000CB2F File Offset: 0x0000AD2F
		public MobileServiceUserException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000CB39 File Offset: 0x0000AD39
		protected MobileServiceUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CB43 File Offset: 0x0000AD43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
