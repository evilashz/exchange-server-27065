using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200001C RID: 28
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000C382 File Offset: 0x0000A582
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000C38A File Offset: 0x0000A58A
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

		// Token: 0x04000100 RID: 256
		private Strings.IDs failureDescriptionId;
	}
}
