using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Configuration.Common.LocStrings
{
	// Token: 0x0200029F RID: 671
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0005C073 File Offset: 0x0005A273
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0005C07B File Offset: 0x0005A27B
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

		// Token: 0x0400097B RID: 2427
		private Strings.IDs failureDescriptionId;
	}
}
