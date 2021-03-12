using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200001D RID: 29
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000C3A7 File Offset: 0x0000A5A7
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000C3AF File Offset: 0x0000A5AF
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

		// Token: 0x04000101 RID: 257
		private Strings.IDs failureDescriptionId;
	}
}
