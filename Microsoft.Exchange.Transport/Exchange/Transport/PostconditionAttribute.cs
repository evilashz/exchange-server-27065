using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200057E RID: 1406
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x0011A631 File Offset: 0x00118831
		// (set) Token: 0x06004130 RID: 16688 RVA: 0x0011A639 File Offset: 0x00118839
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

		// Token: 0x0400253A RID: 9530
		private Strings.IDs failureDescriptionId;
	}
}
