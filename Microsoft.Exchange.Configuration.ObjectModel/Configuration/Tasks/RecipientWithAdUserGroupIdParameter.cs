using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	public class RecipientWithAdUserGroupIdParameter<TRecipientIdParameter> : RecipientIdParameter where TRecipientIdParameter : RecipientIdParameter, new()
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00028BB8 File Offset: 0x00026DB8
		static RecipientWithAdUserGroupIdParameter()
		{
			TRecipientIdParameter trecipientIdParameter = Activator.CreateInstance<TRecipientIdParameter>();
			RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes = new RecipientType[trecipientIdParameter.RecipientTypes.Length + 2];
			trecipientIdParameter.RecipientTypes.CopyTo(RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes, 0);
			RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes[RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes.Length - 2] = RecipientType.Group;
			RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes[RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes.Length - 1] = RecipientType.User;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00028C1E File Offset: 0x00026E1E
		public RecipientWithAdUserGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00028C27 File Offset: 0x00026E27
		public RecipientWithAdUserGroupIdParameter()
		{
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00028C2F File Offset: 0x00026E2F
		public RecipientWithAdUserGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00028C38 File Offset: 0x00026E38
		public RecipientWithAdUserGroupIdParameter(ADPresentationObject recipient) : base(recipient)
		{
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00028C41 File Offset: 0x00026E41
		public RecipientWithAdUserGroupIdParameter(ReducedRecipient recipient) : base(recipient.Id)
		{
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00028C4F File Offset: 0x00026E4F
		public RecipientWithAdUserGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00028C58 File Offset: 0x00026E58
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00028C5F File Offset: 0x00026E5F
		public new static RecipientWithAdUserGroupIdParameter<TRecipientIdParameter> Parse(string identity)
		{
			return new RecipientWithAdUserGroupIdParameter<TRecipientIdParameter>(identity);
		}

		// Token: 0x040002F9 RID: 761
		internal new static readonly RecipientType[] AllowedRecipientTypes;
	}
}
