using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000CC RID: 204
	internal class ContactExtendedPropertiesEmailAddress : ConfigurablePropertyBag
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000156E1 File Offset: 0x000138E1
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x000156F3 File Offset: 0x000138F3
		public string ContactEmailAddress
		{
			get
			{
				return (string)this[ContactExtendedPropertiesEmailAddress.ContactEmailAddressProp];
			}
			set
			{
				this[ContactExtendedPropertiesEmailAddress.ContactEmailAddressProp] = value.ToLower();
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00015706 File Offset: 0x00013906
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00015718 File Offset: 0x00013918
		public Guid TenantId
		{
			get
			{
				return (Guid)this[ContactExtendedPropertiesEmailAddress.TenantIdProp];
			}
			set
			{
				this[ContactExtendedPropertiesEmailAddress.TenantIdProp] = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001572B File Offset: 0x0001392B
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ToString());
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00015738 File Offset: 0x00013938
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DalHelper.ObjectStateProp];
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001574A File Offset: 0x0001394A
		public override string ToString()
		{
			return this.ContactEmailAddress.ToString();
		}

		// Token: 0x04000427 RID: 1063
		public static readonly HygienePropertyDefinition ContactEmailAddressProp = new HygienePropertyDefinition("EmailAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000428 RID: 1064
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("id_TenantId", typeof(Guid));

		// Token: 0x04000429 RID: 1065
		public static readonly HygienePropertyDefinition ContactIdProp = new HygienePropertyDefinition("id_ContactId", typeof(Guid));
	}
}
