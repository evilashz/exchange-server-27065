using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000728 RID: 1832
	[Serializable]
	public abstract class MailEnabledOrgPerson : MailEnabledRecipient
	{
		// Token: 0x060056D3 RID: 22227 RVA: 0x001386E7 File Offset: 0x001368E7
		protected MailEnabledOrgPerson()
		{
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x001386EF File Offset: 0x001368EF
		protected MailEnabledOrgPerson(ADObject dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x060056D5 RID: 22229 RVA: 0x001386F8 File Offset: 0x001368F8
		public MultiValuedProperty<string> Extensions
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledOrgPersonSchema.Extensions];
			}
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x060056D6 RID: 22230 RVA: 0x0013870A File Offset: 0x0013690A
		public bool HasPicture
		{
			get
			{
				return (bool)this[MailEnabledOrgPersonSchema.HasPicture];
			}
		}

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x0013871C File Offset: 0x0013691C
		public bool HasSpokenName
		{
			get
			{
				return (bool)this[MailEnabledOrgPersonSchema.HasSpokenName];
			}
		}
	}
}
