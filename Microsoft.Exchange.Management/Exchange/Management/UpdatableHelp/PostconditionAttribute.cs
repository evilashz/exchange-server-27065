using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02001241 RID: 4673
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PostconditionAttribute : BasicPostconditionAttribute
	{
		// Token: 0x17003B78 RID: 15224
		// (get) Token: 0x0600BC4A RID: 48202 RVA: 0x002AC7A2 File Offset: 0x002AA9A2
		// (set) Token: 0x0600BC4B RID: 48203 RVA: 0x002AC7AA File Offset: 0x002AA9AA
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

		// Token: 0x04006655 RID: 26197
		private UpdatableHelpStrings.IDs failureDescriptionId;
	}
}
