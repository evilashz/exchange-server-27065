using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal sealed class PreconditionAttribute : BasicPreconditionAttribute
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003D96 File Offset: 0x00001F96
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003D9E File Offset: 0x00001F9E
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

		// Token: 0x0400009B RID: 155
		private Strings.IDs failureDescriptionId;
	}
}
