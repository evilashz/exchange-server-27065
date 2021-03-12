using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Configuration.Common.LocStrings
{
	// Token: 0x0200029E RID: 670
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0005C04E File Offset: 0x0005A24E
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x0005C056 File Offset: 0x0005A256
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

		// Token: 0x0400097A RID: 2426
		private Strings.IDs failureDescriptionId;
	}
}
