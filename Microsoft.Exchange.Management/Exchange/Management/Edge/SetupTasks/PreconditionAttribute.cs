using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200121A RID: 4634
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17003AD5 RID: 15061
		// (get) Token: 0x0600BAE2 RID: 47842 RVA: 0x002A961B File Offset: 0x002A781B
		// (set) Token: 0x0600BAE3 RID: 47843 RVA: 0x002A9623 File Offset: 0x002A7823
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

		// Token: 0x0400655D RID: 25949
		private Strings.IDs failureDescriptionId;
	}
}
