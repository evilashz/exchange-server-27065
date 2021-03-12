using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000335 RID: 821
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImplicitSplitPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060025C5 RID: 9669 RVA: 0x0005214C File Offset: 0x0005034C
		public ImplicitSplitPermanentException() : base(MrsStrings.ErrorImplicitSplit)
		{
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00052159 File Offset: 0x00050359
		public ImplicitSplitPermanentException(Exception innerException) : base(MrsStrings.ErrorImplicitSplit, innerException)
		{
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x00052167 File Offset: 0x00050367
		protected ImplicitSplitPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x00052171 File Offset: 0x00050371
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
