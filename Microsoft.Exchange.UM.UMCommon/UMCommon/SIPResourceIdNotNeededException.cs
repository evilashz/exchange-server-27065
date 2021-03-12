using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001C3 RID: 451
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SIPResourceIdNotNeededException : LocalizedException
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00035D15 File Offset: 0x00033F15
		public SIPResourceIdNotNeededException() : base(Strings.ExceptionSipResourceIdNotNeeded)
		{
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00035D22 File Offset: 0x00033F22
		public SIPResourceIdNotNeededException(Exception innerException) : base(Strings.ExceptionSipResourceIdNotNeeded, innerException)
		{
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00035D30 File Offset: 0x00033F30
		protected SIPResourceIdNotNeededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00035D3A File Offset: 0x00033F3A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
