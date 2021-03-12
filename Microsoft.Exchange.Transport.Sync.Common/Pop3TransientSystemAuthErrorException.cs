using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000048 RID: 72
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3TransientSystemAuthErrorException : LocalizedException
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000605B File Offset: 0x0000425B
		public Pop3TransientSystemAuthErrorException() : base(Strings.Pop3TransientSystemAuthErrorException)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006068 File Offset: 0x00004268
		public Pop3TransientSystemAuthErrorException(Exception innerException) : base(Strings.Pop3TransientSystemAuthErrorException, innerException)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006076 File Offset: 0x00004276
		protected Pop3TransientSystemAuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006080 File Offset: 0x00004280
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
