using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000061 RID: 97
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverTransientException : MobileTransientException
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000CC5E File Offset: 0x0000AE5E
		public MobileDriverTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000CC67 File Offset: 0x0000AE67
		public MobileDriverTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000CC71 File Offset: 0x0000AE71
		protected MobileDriverTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000CC7B File Offset: 0x0000AE7B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
