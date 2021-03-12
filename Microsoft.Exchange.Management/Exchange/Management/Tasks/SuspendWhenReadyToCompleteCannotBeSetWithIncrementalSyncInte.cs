using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA5 RID: 3749
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException : RecipientTaskException
	{
		// Token: 0x0600A80E RID: 43022 RVA: 0x0028961F File Offset: 0x0028781F
		public SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException() : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncInterval)
		{
		}

		// Token: 0x0600A80F RID: 43023 RVA: 0x0028962C File Offset: 0x0028782C
		public SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException(Exception innerException) : base(Strings.ErrorSuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncInterval, innerException)
		{
		}

		// Token: 0x0600A810 RID: 43024 RVA: 0x0028963A File Offset: 0x0028783A
		protected SuspendWhenReadyToCompleteCannotBeSetWithIncrementalSyncIntervalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A811 RID: 43025 RVA: 0x00289644 File Offset: 0x00287844
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
