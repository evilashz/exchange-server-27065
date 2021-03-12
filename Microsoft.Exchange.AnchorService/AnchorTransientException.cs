using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000040 RID: 64
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AnchorTransientException : TransientException
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00009529 File Offset: 0x00007729
		public AnchorTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009532 File Offset: 0x00007732
		public AnchorTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000953C File Offset: 0x0000773C
		protected AnchorTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009546 File Offset: 0x00007746
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
