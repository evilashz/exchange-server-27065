using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000115 RID: 277
	internal class UserExtendedPropertiesEmailAddress : ConfigurablePropertyBag
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x000213E7 File Offset: 0x0001F5E7
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x000213F9 File Offset: 0x0001F5F9
		public string UserEmailAddress
		{
			get
			{
				return (string)this[UserExtendedPropertiesEmailAddress.UserEmailAddressProp];
			}
			set
			{
				this[UserExtendedPropertiesEmailAddress.UserEmailAddressProp] = value.ToLower();
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002140C File Offset: 0x0001F60C
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0002141E File Offset: 0x0001F61E
		public Guid TenantId
		{
			get
			{
				return (Guid)this[UserExtendedPropertiesEmailAddress.TenantIdProp];
			}
			set
			{
				this[UserExtendedPropertiesEmailAddress.TenantIdProp] = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00021431 File Offset: 0x0001F631
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ToString());
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002143E File Offset: 0x0001F63E
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DalHelper.ObjectStateProp];
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00021450 File Offset: 0x0001F650
		public override string ToString()
		{
			return this.UserEmailAddress.ToString();
		}

		// Token: 0x04000579 RID: 1401
		public static readonly HygienePropertyDefinition UserEmailAddressProp = new HygienePropertyDefinition("EmailAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400057A RID: 1402
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("id_TenantId", typeof(Guid));
	}
}
