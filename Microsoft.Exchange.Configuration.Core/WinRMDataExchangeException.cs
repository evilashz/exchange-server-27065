using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000030 RID: 48
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WinRMDataExchangeException : LocalizedException
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00007421 File Offset: 0x00005621
		public WinRMDataExchangeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000742A File Offset: 0x0000562A
		public WinRMDataExchangeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007434 File Offset: 0x00005634
		protected WinRMDataExchangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000743E File Offset: 0x0000563E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
