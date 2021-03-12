using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A6 RID: 4262
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncomingUserNameEmpty : LocalizedException
	{
		// Token: 0x0600B239 RID: 45625 RVA: 0x00299849 File Offset: 0x00297A49
		public IncomingUserNameEmpty() : base(Strings.IncomingUserNameEmpty)
		{
		}

		// Token: 0x0600B23A RID: 45626 RVA: 0x00299856 File Offset: 0x00297A56
		public IncomingUserNameEmpty(Exception innerException) : base(Strings.IncomingUserNameEmpty, innerException)
		{
		}

		// Token: 0x0600B23B RID: 45627 RVA: 0x00299864 File Offset: 0x00297A64
		protected IncomingUserNameEmpty(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B23C RID: 45628 RVA: 0x0029986E File Offset: 0x00297A6E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
