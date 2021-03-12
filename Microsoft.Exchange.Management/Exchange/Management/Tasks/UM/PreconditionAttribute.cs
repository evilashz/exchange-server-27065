using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011A7 RID: 4519
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17003A12 RID: 14866
		// (get) Token: 0x0600B836 RID: 47158 RVA: 0x002A44D2 File Offset: 0x002A26D2
		// (set) Token: 0x0600B837 RID: 47159 RVA: 0x002A44DA File Offset: 0x002A26DA
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

		// Token: 0x0400642D RID: 25645
		private Strings.IDs failureDescriptionId;
	}
}
