using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A63 RID: 2659
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ABTimeoutException : ABTransientException
	{
		// Token: 0x06007EE9 RID: 32489 RVA: 0x001A3C0A File Offset: 0x001A1E0A
		public ABTimeoutException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x001A3C13 File Offset: 0x001A1E13
		public ABTimeoutException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EEB RID: 32491 RVA: 0x001A3C1D File Offset: 0x001A1E1D
		protected ABTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x001A3C27 File Offset: 0x001A1E27
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
