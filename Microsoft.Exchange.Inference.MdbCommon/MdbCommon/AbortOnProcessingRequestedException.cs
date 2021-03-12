using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000029 RID: 41
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AbortOnProcessingRequestedException : OperationFailedException
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000057DF File Offset: 0x000039DF
		public AbortOnProcessingRequestedException() : base(Strings.AbortOnProcessingRequested)
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000057EC File Offset: 0x000039EC
		public AbortOnProcessingRequestedException(Exception innerException) : base(Strings.AbortOnProcessingRequested, innerException)
		{
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000057FA File Offset: 0x000039FA
		protected AbortOnProcessingRequestedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005804 File Offset: 0x00003A04
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
