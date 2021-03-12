using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Mobility
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000101CD File Offset: 0x0000E3CD
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x000101D5 File Offset: 0x0000E3D5
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

		// Token: 0x04000157 RID: 343
		private Strings.IDs failureDescriptionId;
	}
}
