using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE7 RID: 2791
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidResourceThresholdException : LocalizedException
	{
		// Token: 0x06008135 RID: 33077 RVA: 0x001A644D File Offset: 0x001A464D
		public InvalidResourceThresholdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x001A6456 File Offset: 0x001A4656
		public InvalidResourceThresholdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x001A6460 File Offset: 0x001A4660
		protected InvalidResourceThresholdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x001A646A File Offset: 0x001A466A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
