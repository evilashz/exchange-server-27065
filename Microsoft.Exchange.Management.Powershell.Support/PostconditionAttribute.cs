using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000052 RID: 82
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00011325 File Offset: 0x0000F525
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0001132D File Offset: 0x0000F52D
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

		// Token: 0x040001C3 RID: 451
		private Strings.IDs failureDescriptionId;
	}
}
