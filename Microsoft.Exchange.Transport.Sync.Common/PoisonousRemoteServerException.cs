using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200005E RID: 94
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PoisonousRemoteServerException : LocalizedException
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00006B5A File Offset: 0x00004D5A
		public PoisonousRemoteServerException() : base(Strings.PoisonousRemoteServerException)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00006B67 File Offset: 0x00004D67
		public PoisonousRemoteServerException(Exception innerException) : base(Strings.PoisonousRemoteServerException, innerException)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006B75 File Offset: 0x00004D75
		protected PoisonousRemoteServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00006B7F File Offset: 0x00004D7F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
