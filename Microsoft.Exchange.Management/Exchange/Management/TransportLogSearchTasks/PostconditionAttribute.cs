using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200120F RID: 4623
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17003A6C RID: 14956
		// (get) Token: 0x0600BA33 RID: 47667 RVA: 0x002A7571 File Offset: 0x002A5771
		// (set) Token: 0x0600BA34 RID: 47668 RVA: 0x002A7579 File Offset: 0x002A5779
		public Strings.IDs FailureDescriptionId
		{
			get
			{
				return this.failureDescriptionId;
			}
			set
			{
				base.FailureDescription = Strings.GetLocalizedString(value);
				this.failureDescriptionId = value;
			}
		}

		// Token: 0x040064B2 RID: 25778
		private Strings.IDs failureDescriptionId;
	}
}
