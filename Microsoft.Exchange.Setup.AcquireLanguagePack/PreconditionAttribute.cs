using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004FA6 File Offset: 0x000031A6
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004FAE File Offset: 0x000031AE
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

		// Token: 0x04000084 RID: 132
		private Strings.IDs failureDescriptionId;
	}
}
