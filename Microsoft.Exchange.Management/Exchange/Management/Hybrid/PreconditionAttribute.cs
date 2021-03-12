using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001234 RID: 4660
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17003B3C RID: 15164
		// (get) Token: 0x0600BBDA RID: 48090 RVA: 0x002AB826 File Offset: 0x002A9A26
		// (set) Token: 0x0600BBDB RID: 48091 RVA: 0x002AB82E File Offset: 0x002A9A2E
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

		// Token: 0x04006602 RID: 26114
		private HybridStrings.IDs failureDescriptionId;
	}
}
