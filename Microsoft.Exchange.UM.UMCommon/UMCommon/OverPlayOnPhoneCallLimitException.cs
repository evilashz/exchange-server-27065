using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001CE RID: 462
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OverPlayOnPhoneCallLimitException : LocalizedException
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0003611E File Offset: 0x0003431E
		public OverPlayOnPhoneCallLimitException() : base(Strings.OverPlayOnPhoneCallLimitException)
		{
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003612B File Offset: 0x0003432B
		public OverPlayOnPhoneCallLimitException(Exception innerException) : base(Strings.OverPlayOnPhoneCallLimitException, innerException)
		{
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00036139 File Offset: 0x00034339
		protected OverPlayOnPhoneCallLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00036143 File Offset: 0x00034343
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
