using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000DA RID: 218
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DownloadPermanentException : LocalizedException
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x00014274 File Offset: 0x00012474
		public DownloadPermanentException() : base(HttpStrings.DownloadPermanentException)
		{
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00014281 File Offset: 0x00012481
		public DownloadPermanentException(Exception innerException) : base(HttpStrings.DownloadPermanentException, innerException)
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001428F File Offset: 0x0001248F
		protected DownloadPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00014299 File Offset: 0x00012499
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
