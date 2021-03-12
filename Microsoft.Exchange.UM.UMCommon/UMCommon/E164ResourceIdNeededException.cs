using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001BF RID: 447
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E164ResourceIdNeededException : LocalizedException
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00035C59 File Offset: 0x00033E59
		public E164ResourceIdNeededException() : base(Strings.ExceptionE164ResourceIdNeeded)
		{
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00035C66 File Offset: 0x00033E66
		public E164ResourceIdNeededException(Exception innerException) : base(Strings.ExceptionE164ResourceIdNeeded, innerException)
		{
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00035C74 File Offset: 0x00033E74
		protected E164ResourceIdNeededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00035C7E File Offset: 0x00033E7E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
