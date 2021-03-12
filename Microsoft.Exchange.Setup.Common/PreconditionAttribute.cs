using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000165C6 File Offset: 0x000147C6
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x000165CE File Offset: 0x000147CE
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

		// Token: 0x040002F9 RID: 761
		private Strings.IDs failureDescriptionId;
	}
}
