using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000322 RID: 802
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00073A54 File Offset: 0x00071C54
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x00073A5C File Offset: 0x00071C5C
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

		// Token: 0x0400113E RID: 4414
		private Strings.IDs failureDescriptionId;
	}
}
