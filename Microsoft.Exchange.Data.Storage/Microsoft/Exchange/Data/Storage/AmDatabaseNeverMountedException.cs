using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D7 RID: 215
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDatabaseNeverMountedException : AmServerException
	{
		// Token: 0x060012B0 RID: 4784 RVA: 0x00068118 File Offset: 0x00066318
		public AmDatabaseNeverMountedException() : base(ServerStrings.AmDatabaseNeverMountedException)
		{
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0006812A File Offset: 0x0006632A
		public AmDatabaseNeverMountedException(Exception innerException) : base(ServerStrings.AmDatabaseNeverMountedException, innerException)
		{
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0006813D File Offset: 0x0006633D
		protected AmDatabaseNeverMountedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00068147 File Offset: 0x00066347
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
