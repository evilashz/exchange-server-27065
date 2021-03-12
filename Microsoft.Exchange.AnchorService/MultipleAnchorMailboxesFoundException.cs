using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000042 RID: 66
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultipleAnchorMailboxesFoundException : AnchorPermanentException
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000957F File Offset: 0x0000777F
		public MultipleAnchorMailboxesFoundException() : base(Strings.MultipleAnchorMailboxesFound)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000958C File Offset: 0x0000778C
		public MultipleAnchorMailboxesFoundException(Exception innerException) : base(Strings.MultipleAnchorMailboxesFound, innerException)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000959A File Offset: 0x0000779A
		protected MultipleAnchorMailboxesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000095A4 File Offset: 0x000077A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
