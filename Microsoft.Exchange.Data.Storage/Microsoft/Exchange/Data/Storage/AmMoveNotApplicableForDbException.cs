using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000DB RID: 219
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmMoveNotApplicableForDbException : AmServerException
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x0006832D File Offset: 0x0006652D
		public AmMoveNotApplicableForDbException() : base(ServerStrings.AmMoveNotApplicableForDbException)
		{
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0006833F File Offset: 0x0006653F
		public AmMoveNotApplicableForDbException(Exception innerException) : base(ServerStrings.AmMoveNotApplicableForDbException, innerException)
		{
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00068352 File Offset: 0x00066552
		protected AmMoveNotApplicableForDbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0006835C File Offset: 0x0006655C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
