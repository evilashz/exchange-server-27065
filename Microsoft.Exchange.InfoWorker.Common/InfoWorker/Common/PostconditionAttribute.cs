using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000323 RID: 803
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x00073A79 File Offset: 0x00071C79
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x00073A81 File Offset: 0x00071C81
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

		// Token: 0x0400113F RID: 4415
		private Strings.IDs failureDescriptionId;
	}
}
