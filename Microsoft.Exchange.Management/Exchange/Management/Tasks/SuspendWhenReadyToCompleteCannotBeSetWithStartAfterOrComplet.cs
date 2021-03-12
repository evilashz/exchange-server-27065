using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA4 RID: 3748
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfterException : RecipientTaskException
	{
		// Token: 0x0600A80A RID: 43018 RVA: 0x002895F0 File Offset: 0x002877F0
		public SuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfterException() : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfter)
		{
		}

		// Token: 0x0600A80B RID: 43019 RVA: 0x002895FD File Offset: 0x002877FD
		public SuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfterException(Exception innerException) : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfter, innerException)
		{
		}

		// Token: 0x0600A80C RID: 43020 RVA: 0x0028960B File Offset: 0x0028780B
		protected SuspendWhenReadyToCompleteCannotBeSetWithStartAfterOrCompleteAfterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A80D RID: 43021 RVA: 0x00289615 File Offset: 0x00287815
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
