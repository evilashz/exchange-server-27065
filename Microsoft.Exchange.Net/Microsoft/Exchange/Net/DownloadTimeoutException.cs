using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D4 RID: 212
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DownloadTimeoutException : TransientException
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00014036 File Offset: 0x00012236
		public DownloadTimeoutException() : base(HttpStrings.DownloadTimeoutException)
		{
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00014043 File Offset: 0x00012243
		public DownloadTimeoutException(Exception innerException) : base(HttpStrings.DownloadTimeoutException, innerException)
		{
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00014051 File Offset: 0x00012251
		protected DownloadTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001405B File Offset: 0x0001225B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
