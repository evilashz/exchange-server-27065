using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004FCB File Offset: 0x000031CB
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004FD3 File Offset: 0x000031D3
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

		// Token: 0x04000085 RID: 133
		private Strings.IDs failureDescriptionId;
	}
}
