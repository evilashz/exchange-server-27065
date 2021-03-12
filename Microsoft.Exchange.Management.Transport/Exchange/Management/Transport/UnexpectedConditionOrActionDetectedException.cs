using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000177 RID: 375
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedConditionOrActionDetectedException : LocalizedException
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x000361BC File Offset: 0x000343BC
		public UnexpectedConditionOrActionDetectedException() : base(Strings.UnexpectedConditionOrActionDetected)
		{
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000361C9 File Offset: 0x000343C9
		public UnexpectedConditionOrActionDetectedException(Exception innerException) : base(Strings.UnexpectedConditionOrActionDetected, innerException)
		{
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000361D7 File Offset: 0x000343D7
		protected UnexpectedConditionOrActionDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000361E1 File Offset: 0x000343E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
