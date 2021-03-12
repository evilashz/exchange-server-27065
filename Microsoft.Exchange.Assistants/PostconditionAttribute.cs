using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003DBB File Offset: 0x00001FBB
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003DC3 File Offset: 0x00001FC3
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

		// Token: 0x0400009C RID: 156
		private Strings.IDs failureDescriptionId;
	}
}
