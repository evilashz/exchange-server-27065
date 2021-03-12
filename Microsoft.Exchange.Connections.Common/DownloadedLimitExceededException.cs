using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200003B RID: 59
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DownloadedLimitExceededException : NonPromotableTransientException
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00003AD7 File Offset: 0x00001CD7
		public DownloadedLimitExceededException() : base(CXStrings.DownloadedLimitExceededError)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003AE9 File Offset: 0x00001CE9
		public DownloadedLimitExceededException(Exception innerException) : base(CXStrings.DownloadedLimitExceededError, innerException)
		{
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003AFC File Offset: 0x00001CFC
		protected DownloadedLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003B06 File Offset: 0x00001D06
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
