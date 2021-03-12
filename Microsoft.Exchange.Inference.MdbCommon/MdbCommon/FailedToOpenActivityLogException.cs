using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000027 RID: 39
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToOpenActivityLogException : OperationFailedException
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00005781 File Offset: 0x00003981
		public FailedToOpenActivityLogException() : base(Strings.FailedToOpenActivityLog)
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000578E File Offset: 0x0000398E
		public FailedToOpenActivityLogException(Exception innerException) : base(Strings.FailedToOpenActivityLog, innerException)
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000579C File Offset: 0x0000399C
		protected FailedToOpenActivityLogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000057A6 File Offset: 0x000039A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
