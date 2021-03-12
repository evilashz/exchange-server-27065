using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000217 RID: 535
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoSpeechDetectedException : LocalizedException
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x00039BBE File Offset: 0x00037DBE
		public NoSpeechDetectedException() : base(Strings.NoSpeechDetectedException)
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00039BCB File Offset: 0x00037DCB
		public NoSpeechDetectedException(Exception innerException) : base(Strings.NoSpeechDetectedException, innerException)
		{
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00039BD9 File Offset: 0x00037DD9
		protected NoSpeechDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00039BE3 File Offset: 0x00037DE3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
