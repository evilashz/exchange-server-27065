using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020E RID: 526
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MobileRecoDispatcherStoppingException : MobileRecoRequestCannotBeHandledException
	{
		// Token: 0x06001100 RID: 4352 RVA: 0x000397B0 File Offset: 0x000379B0
		public MobileRecoDispatcherStoppingException() : base(Strings.MobileRecoDispatcherStopping)
		{
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000397BD File Offset: 0x000379BD
		public MobileRecoDispatcherStoppingException(Exception innerException) : base(Strings.MobileRecoDispatcherStopping, innerException)
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000397CB File Offset: 0x000379CB
		protected MobileRecoDispatcherStoppingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000397D5 File Offset: 0x000379D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
