using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02001214 RID: 4628
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17003A9A RID: 15002
		// (get) Token: 0x0600BA85 RID: 47749 RVA: 0x002A84CA File Offset: 0x002A66CA
		// (set) Token: 0x0600BA86 RID: 47750 RVA: 0x002A84D2 File Offset: 0x002A66D2
		public QueueViewerStrings.IDs FailureDescriptionId
		{
			get
			{
				return this.failureDescriptionId;
			}
			set
			{
				base.FailureDescription = QueueViewerStrings.GetLocalizedString(value);
				this.failureDescriptionId = value;
			}
		}

		// Token: 0x04006503 RID: 25859
		private QueueViewerStrings.IDs failureDescriptionId;
	}
}
