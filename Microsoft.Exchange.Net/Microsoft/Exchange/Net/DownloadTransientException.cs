using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000DB RID: 219
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DownloadTransientException : TransientException
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x000142A3 File Offset: 0x000124A3
		public DownloadTransientException() : base(HttpStrings.DownloadTransientException)
		{
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000142B0 File Offset: 0x000124B0
		public DownloadTransientException(Exception innerException) : base(HttpStrings.DownloadTransientException, innerException)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000142BE File Offset: 0x000124BE
		protected DownloadTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000142C8 File Offset: 0x000124C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
