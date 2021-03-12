using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200120E RID: 4622
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17003A6B RID: 14955
		// (get) Token: 0x0600BA30 RID: 47664 RVA: 0x002A754C File Offset: 0x002A574C
		// (set) Token: 0x0600BA31 RID: 47665 RVA: 0x002A7554 File Offset: 0x002A5754
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

		// Token: 0x040064B1 RID: 25777
		private Strings.IDs failureDescriptionId;
	}
}
