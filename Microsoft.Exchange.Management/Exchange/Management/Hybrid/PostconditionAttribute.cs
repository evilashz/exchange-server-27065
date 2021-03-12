using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001235 RID: 4661
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17003B3D RID: 15165
		// (get) Token: 0x0600BBDD RID: 48093 RVA: 0x002AB84B File Offset: 0x002A9A4B
		// (set) Token: 0x0600BBDE RID: 48094 RVA: 0x002AB853 File Offset: 0x002A9A53
		public HybridStrings.IDs FailureDescriptionId
		{
			get
			{
				return this.failureDescriptionId;
			}
			set
			{
				base.FailureDescription = HybridStrings.GetLocalizedString(value);
				this.failureDescriptionId = value;
			}
		}

		// Token: 0x04006603 RID: 26115
		private HybridStrings.IDs failureDescriptionId;
	}
}
