using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D5 RID: 213
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DownloadCanceledException : TransientException
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x00014065 File Offset: 0x00012265
		public DownloadCanceledException() : base(HttpStrings.DownloadCanceledException)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00014072 File Offset: 0x00012272
		public DownloadCanceledException(Exception innerException) : base(HttpStrings.DownloadCanceledException, innerException)
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00014080 File Offset: 0x00012280
		protected DownloadCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001408A File Offset: 0x0001228A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
