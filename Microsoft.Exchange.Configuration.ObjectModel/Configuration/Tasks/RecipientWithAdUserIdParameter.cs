using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	public class RecipientWithAdUserIdParameter<TRecipientIdParameter> : RecipientIdParameter where TRecipientIdParameter : RecipientIdParameter, new()
	{
		// Token: 0x06000D98 RID: 3480 RVA: 0x00028C68 File Offset: 0x00026E68
		static RecipientWithAdUserIdParameter()
		{
			TRecipientIdParameter trecipientIdParameter = Activator.CreateInstance<TRecipientIdParameter>();
			RecipientWithAdUserIdParameter<TRecipientIdParameter>.AllowedRecipientTypes = new RecipientType[trecipientIdParameter.RecipientTypes.Length + 1];
			trecipientIdParameter.RecipientTypes.CopyTo(RecipientWithAdUserIdParameter<TRecipientIdParameter>.AllowedRecipientTypes, 0);
			RecipientWithAdUserIdParameter<TRecipientIdParameter>.AllowedRecipientTypes[RecipientWithAdUserIdParameter<TRecipientIdParameter>.AllowedRecipientTypes.Length - 1] = RecipientType.User;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00028CBE File Offset: 0x00026EBE
		public RecipientWithAdUserIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00028CC7 File Offset: 0x00026EC7
		public RecipientWithAdUserIdParameter()
		{
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00028CCF File Offset: 0x00026ECF
		public RecipientWithAdUserIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00028CD8 File Offset: 0x00026ED8
		public RecipientWithAdUserIdParameter(ADPresentationObject recipient) : base(recipient)
		{
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00028CE1 File Offset: 0x00026EE1
		public RecipientWithAdUserIdParameter(ReducedRecipient recipient) : base(recipient.Id)
		{
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00028CEF File Offset: 0x00026EEF
		public RecipientWithAdUserIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00028CF8 File Offset: 0x00026EF8
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return RecipientWithAdUserIdParameter<TRecipientIdParameter>.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00028CFF File Offset: 0x00026EFF
		public new static RecipientWithAdUserIdParameter<TRecipientIdParameter> Parse(string identity)
		{
			return new RecipientWithAdUserIdParameter<TRecipientIdParameter>(identity);
		}

		// Token: 0x040002FA RID: 762
		internal new static readonly RecipientType[] AllowedRecipientTypes;
	}
}
