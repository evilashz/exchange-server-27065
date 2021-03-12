using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000047 RID: 71
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3TransientLoginDelayedAuthErrorException : LocalizedException
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000602C File Offset: 0x0000422C
		public Pop3TransientLoginDelayedAuthErrorException() : base(Strings.Pop3TransientLoginDelayedAuthErrorException)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006039 File Offset: 0x00004239
		public Pop3TransientLoginDelayedAuthErrorException(Exception innerException) : base(Strings.Pop3TransientLoginDelayedAuthErrorException, innerException)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006047 File Offset: 0x00004247
		protected Pop3TransientLoginDelayedAuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006051 File Offset: 0x00004251
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
