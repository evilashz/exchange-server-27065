using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000065 RID: 101
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDriverFatalErrorException : MobilePermanentException
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		public MobileDriverFatalErrorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000CD03 File Offset: 0x0000AF03
		public MobileDriverFatalErrorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		protected MobileDriverFatalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000CD17 File Offset: 0x0000AF17
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
