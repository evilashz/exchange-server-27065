using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020B RID: 523
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MobileRecoRequestCannotBeHandledException : LocalizedException
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x0003968C File Offset: 0x0003788C
		public MobileRecoRequestCannotBeHandledException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00039695 File Offset: 0x00037895
		public MobileRecoRequestCannotBeHandledException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003969F File Offset: 0x0003789F
		protected MobileRecoRequestCannotBeHandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000396A9 File Offset: 0x000378A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
