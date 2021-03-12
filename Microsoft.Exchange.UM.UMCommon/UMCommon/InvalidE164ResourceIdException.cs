using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001C0 RID: 448
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidE164ResourceIdException : LocalizedException
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x00035C88 File Offset: 0x00033E88
		public InvalidE164ResourceIdException() : base(Strings.ExceptionInvalidE164ResourceId)
		{
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00035C95 File Offset: 0x00033E95
		public InvalidE164ResourceIdException(Exception innerException) : base(Strings.ExceptionInvalidE164ResourceId, innerException)
		{
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00035CA3 File Offset: 0x00033EA3
		protected InvalidE164ResourceIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00035CAD File Offset: 0x00033EAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
