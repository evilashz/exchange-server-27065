using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000DC RID: 220
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbMountNotAllowedDueToLossException : AmServerException
	{
		// Token: 0x060012C8 RID: 4808 RVA: 0x00068366 File Offset: 0x00066566
		public AmDbMountNotAllowedDueToLossException() : base(ServerStrings.AmDbMountNotAllowedDueToLossException)
		{
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00068378 File Offset: 0x00066578
		public AmDbMountNotAllowedDueToLossException(Exception innerException) : base(ServerStrings.AmDbMountNotAllowedDueToLossException, innerException)
		{
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0006838B File Offset: 0x0006658B
		protected AmDbMountNotAllowedDueToLossException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00068395 File Offset: 0x00066595
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
