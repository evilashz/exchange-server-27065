using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200022F RID: 559
	[DataContract]
	public abstract class GroupFilter : RecipientFilter
	{
		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x0007D05C File Offset: 0x0007B25C
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup
				};
			}
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0007D08C File Offset: 0x0007B28C
		protected override void UpdateFilterProperty()
		{
			base.UpdateFilterProperty();
			string text = (string)base["Filter"];
			string text2 = null;
			if (!string.IsNullOrEmpty(this.AdditionalFilterText))
			{
				text2 = this.AdditionalFilterText;
			}
			if (!string.IsNullOrEmpty(text))
			{
				text2 = string.Format("({0}) -and {1}", text, text2);
			}
			base["Filter"] = text2;
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x060027C3 RID: 10179
		protected abstract string AdditionalFilterText { get; }
	}
}
