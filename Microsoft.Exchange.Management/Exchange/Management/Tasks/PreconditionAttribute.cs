using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DD1 RID: 3537
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x170035DB RID: 13787
		// (get) Token: 0x0600A407 RID: 41991 RVA: 0x00283924 File Offset: 0x00281B24
		// (set) Token: 0x0600A408 RID: 41992 RVA: 0x0028392C File Offset: 0x00281B2C
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

		// Token: 0x04005F41 RID: 24385
		private Strings.IDs failureDescriptionId;
	}
}
