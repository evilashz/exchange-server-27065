using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000041 RID: 65
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AnchorMailboxNotFoundException : AnchorPermanentException
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00009550 File Offset: 0x00007750
		public AnchorMailboxNotFoundException() : base(Strings.AnchorMailboxNotFound)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000955D File Offset: 0x0000775D
		public AnchorMailboxNotFoundException(Exception innerException) : base(Strings.AnchorMailboxNotFound, innerException)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000956B File Offset: 0x0000776B
		protected AnchorMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009575 File Offset: 0x00007775
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
