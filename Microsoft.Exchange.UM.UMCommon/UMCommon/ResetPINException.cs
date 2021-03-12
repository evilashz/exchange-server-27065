using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000197 RID: 407
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ResetPINException : LocalizedException
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x00034C39 File Offset: 0x00032E39
		public ResetPINException() : base(Strings.ResetPINException)
		{
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00034C46 File Offset: 0x00032E46
		public ResetPINException(Exception innerException) : base(Strings.ResetPINException, innerException)
		{
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00034C54 File Offset: 0x00032E54
		protected ResetPINException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00034C5E File Offset: 0x00032E5E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
