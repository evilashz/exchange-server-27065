using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x00011300 File Offset: 0x0000F500
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00011308 File Offset: 0x0000F508
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

		// Token: 0x040001C2 RID: 450
		private Strings.IDs failureDescriptionId;
	}
}
