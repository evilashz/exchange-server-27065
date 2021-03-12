using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02001215 RID: 4629
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17003A9B RID: 15003
		// (get) Token: 0x0600BA88 RID: 47752 RVA: 0x002A84EF File Offset: 0x002A66EF
		// (set) Token: 0x0600BA89 RID: 47753 RVA: 0x002A84F7 File Offset: 0x002A66F7
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

		// Token: 0x04006504 RID: 25860
		private QueueViewerStrings.IDs failureDescriptionId;
	}
}
