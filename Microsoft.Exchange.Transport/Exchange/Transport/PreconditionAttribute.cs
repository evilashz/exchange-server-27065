using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200057D RID: 1405
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x0011A60C File Offset: 0x0011880C
		// (set) Token: 0x0600412D RID: 16685 RVA: 0x0011A614 File Offset: 0x00118814
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

		// Token: 0x04002539 RID: 9529
		private Strings.IDs failureDescriptionId;
	}
}
