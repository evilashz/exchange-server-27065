using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000165EB File Offset: 0x000147EB
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x000165F3 File Offset: 0x000147F3
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

		// Token: 0x040002FA RID: 762
		private Strings.IDs failureDescriptionId;
	}
}
