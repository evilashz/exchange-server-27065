using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A60 RID: 2656
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADInvalidHandleCookieException : ADTransientException
	{
		// Token: 0x06007EDD RID: 32477 RVA: 0x001A3B95 File Offset: 0x001A1D95
		public ADInvalidHandleCookieException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x001A3B9E File Offset: 0x001A1D9E
		public ADInvalidHandleCookieException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x001A3BA8 File Offset: 0x001A1DA8
		protected ADInvalidHandleCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x001A3BB2 File Offset: 0x001A1DB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
