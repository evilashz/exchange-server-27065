using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000042 RID: 66
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3LeaveOnServerNotPossibleException : LocalizedException
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00005EA1 File Offset: 0x000040A1
		public Pop3LeaveOnServerNotPossibleException() : base(Strings.Pop3LeaveOnServerNotPossibleException)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005EAE File Offset: 0x000040AE
		public Pop3LeaveOnServerNotPossibleException(Exception innerException) : base(Strings.Pop3LeaveOnServerNotPossibleException, innerException)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005EBC File Offset: 0x000040BC
		protected Pop3LeaveOnServerNotPossibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005EC6 File Offset: 0x000040C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
