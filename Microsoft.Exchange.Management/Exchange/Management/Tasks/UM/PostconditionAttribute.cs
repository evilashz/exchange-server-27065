using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011A8 RID: 4520
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17003A13 RID: 14867
		// (get) Token: 0x0600B839 RID: 47161 RVA: 0x002A44F7 File Offset: 0x002A26F7
		// (set) Token: 0x0600B83A RID: 47162 RVA: 0x002A44FF File Offset: 0x002A26FF
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

		// Token: 0x0400642E RID: 25646
		private Strings.IDs failureDescriptionId;
	}
}
