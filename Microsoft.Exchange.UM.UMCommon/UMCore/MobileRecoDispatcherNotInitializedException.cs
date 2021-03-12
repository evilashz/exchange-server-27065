using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200020D RID: 525
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MobileRecoDispatcherNotInitializedException : MobileRecoRequestCannotBeHandledException
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x00039781 File Offset: 0x00037981
		public MobileRecoDispatcherNotInitializedException() : base(Strings.MobileRecoDispatcherNotInitialized)
		{
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003978E File Offset: 0x0003798E
		public MobileRecoDispatcherNotInitializedException(Exception innerException) : base(Strings.MobileRecoDispatcherNotInitialized, innerException)
		{
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0003979C File Offset: 0x0003799C
		protected MobileRecoDispatcherNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000397A6 File Offset: 0x000379A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
