using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200121B RID: 4635
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17003AD6 RID: 15062
		// (get) Token: 0x0600BAE5 RID: 47845 RVA: 0x002A9640 File Offset: 0x002A7840
		// (set) Token: 0x0600BAE6 RID: 47846 RVA: 0x002A9648 File Offset: 0x002A7848
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

		// Token: 0x0400655E RID: 25950
		private Strings.IDs failureDescriptionId;
	}
}
