using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DD2 RID: 3538
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x170035DC RID: 13788
		// (get) Token: 0x0600A40A RID: 41994 RVA: 0x00283949 File Offset: 0x00281B49
		// (set) Token: 0x0600A40B RID: 41995 RVA: 0x00283951 File Offset: 0x00281B51
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

		// Token: 0x04005F42 RID: 24386
		private Strings.IDs failureDescriptionId;
	}
}
