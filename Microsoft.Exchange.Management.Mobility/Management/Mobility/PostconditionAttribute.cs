using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Mobility
{
	// Token: 0x02000060 RID: 96
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000101F2 File Offset: 0x0000E3F2
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x000101FA File Offset: 0x0000E3FA
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

		// Token: 0x04000158 RID: 344
		private Strings.IDs failureDescriptionId;
	}
}
