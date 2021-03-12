using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000029 RID: 41
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingServerResponseException : TransientException
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00005648 File Offset: 0x00003848
		public MissingServerResponseException() : base(Strings.MissingServerResponseException)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005655 File Offset: 0x00003855
		public MissingServerResponseException(Exception innerException) : base(Strings.MissingServerResponseException, innerException)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005663 File Offset: 0x00003863
		protected MissingServerResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000566D File Offset: 0x0000386D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
