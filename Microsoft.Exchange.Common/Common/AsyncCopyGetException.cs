using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000021 RID: 33
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AsyncCopyGetException : TransientException
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003FE6 File Offset: 0x000021E6
		public AsyncCopyGetException() : base(CommonStrings.AsyncCopyGetException)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003FF3 File Offset: 0x000021F3
		public AsyncCopyGetException(Exception innerException) : base(CommonStrings.AsyncCopyGetException, innerException)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004001 File Offset: 0x00002201
		protected AsyncCopyGetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000400B File Offset: 0x0000220B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
