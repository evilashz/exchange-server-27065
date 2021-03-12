using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA3 RID: 3747
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveStartAfterEarlierThanCompleteAfterException : RecipientTaskException
	{
		// Token: 0x0600A806 RID: 43014 RVA: 0x002895C1 File Offset: 0x002877C1
		public MoveStartAfterEarlierThanCompleteAfterException() : base(Strings.ErrorStartAfterEarlierThanCompleteAfter)
		{
		}

		// Token: 0x0600A807 RID: 43015 RVA: 0x002895CE File Offset: 0x002877CE
		public MoveStartAfterEarlierThanCompleteAfterException(Exception innerException) : base(Strings.ErrorStartAfterEarlierThanCompleteAfter, innerException)
		{
		}

		// Token: 0x0600A808 RID: 43016 RVA: 0x002895DC File Offset: 0x002877DC
		protected MoveStartAfterEarlierThanCompleteAfterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A809 RID: 43017 RVA: 0x002895E6 File Offset: 0x002877E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
