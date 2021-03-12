using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001AA RID: 426
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUMAutoAttendantException : LocalizedException
	{
		// Token: 0x06000E7C RID: 3708 RVA: 0x00035164 File Offset: 0x00033364
		public InvalidUMAutoAttendantException() : base(Strings.InvalidUMAutoAttendantException)
		{
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00035171 File Offset: 0x00033371
		public InvalidUMAutoAttendantException(Exception innerException) : base(Strings.InvalidUMAutoAttendantException, innerException)
		{
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003517F File Offset: 0x0003337F
		protected InvalidUMAutoAttendantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00035189 File Offset: 0x00033389
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
