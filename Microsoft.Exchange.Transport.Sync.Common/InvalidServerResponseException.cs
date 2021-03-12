using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000025 RID: 37
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidServerResponseException : LocalizedException
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00005543 File Offset: 0x00003743
		public InvalidServerResponseException() : base(Strings.InvalidServerResponseException)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005550 File Offset: 0x00003750
		public InvalidServerResponseException(Exception innerException) : base(Strings.InvalidServerResponseException, innerException)
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000555E File Offset: 0x0000375E
		protected InvalidServerResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005568 File Offset: 0x00003768
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
