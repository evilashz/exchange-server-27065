using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000EB RID: 235
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TlsProtocolFailureException : LocalizedException
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00015FF7 File Offset: 0x000141F7
		public TlsProtocolFailureException() : base(NetException.TlsProtocolFailureException)
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00016004 File Offset: 0x00014204
		public TlsProtocolFailureException(Exception innerException) : base(NetException.TlsProtocolFailureException, innerException)
		{
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00016012 File Offset: 0x00014212
		protected TlsProtocolFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001601C File Offset: 0x0001421C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
