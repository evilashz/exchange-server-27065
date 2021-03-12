using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02001240 RID: 4672
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17003B77 RID: 15223
		// (get) Token: 0x0600BC47 RID: 48199 RVA: 0x002AC77D File Offset: 0x002AA97D
		// (set) Token: 0x0600BC48 RID: 48200 RVA: 0x002AC785 File Offset: 0x002AA985
		public UpdatableHelpStrings.IDs FailureDescriptionId
		{
			get
			{
				return this.failureDescriptionId;
			}
			set
			{
				base.FailureDescription = UpdatableHelpStrings.GetLocalizedString(value);
				this.failureDescriptionId = value;
			}
		}

		// Token: 0x04006654 RID: 26196
		private UpdatableHelpStrings.IDs failureDescriptionId;
	}
}
