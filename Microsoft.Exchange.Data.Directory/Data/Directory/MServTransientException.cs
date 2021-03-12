using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B13 RID: 2835
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MServTransientException : DataSourceTransientException
	{
		// Token: 0x0600821E RID: 33310 RVA: 0x001A7D61 File Offset: 0x001A5F61
		public MServTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600821F RID: 33311 RVA: 0x001A7D6A File Offset: 0x001A5F6A
		public MServTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008220 RID: 33312 RVA: 0x001A7D74 File Offset: 0x001A5F74
		protected MServTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008221 RID: 33313 RVA: 0x001A7D7E File Offset: 0x001A5F7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
