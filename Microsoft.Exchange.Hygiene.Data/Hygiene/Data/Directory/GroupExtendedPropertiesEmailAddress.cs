using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E3 RID: 227
	internal class GroupExtendedPropertiesEmailAddress : ConfigurablePropertyBag
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001C8F8 File Offset: 0x0001AAF8
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001C90A File Offset: 0x0001AB0A
		public string GroupEmailAddress
		{
			get
			{
				return (string)this[GroupExtendedPropertiesEmailAddress.GroupEmailAddressProp];
			}
			set
			{
				this[GroupExtendedPropertiesEmailAddress.GroupEmailAddressProp] = value.ToLower();
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001C91D File Offset: 0x0001AB1D
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0001C92F File Offset: 0x0001AB2F
		public Guid TenantId
		{
			get
			{
				return (Guid)this[GroupExtendedPropertiesEmailAddress.TenantIdProp];
			}
			set
			{
				this[GroupExtendedPropertiesEmailAddress.TenantIdProp] = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001C942 File Offset: 0x0001AB42
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0001C954 File Offset: 0x0001AB54
		public Guid GroupId
		{
			get
			{
				return (Guid)this[GroupExtendedPropertiesEmailAddress.GroupIdProp];
			}
			set
			{
				this[GroupExtendedPropertiesEmailAddress.GroupIdProp] = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001C967 File Offset: 0x0001AB67
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ToString());
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001C974 File Offset: 0x0001AB74
		public override ObjectState ObjectState
		{
			get
			{
				return (ObjectState)this[DalHelper.ObjectStateProp];
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001C986 File Offset: 0x0001AB86
		public override string ToString()
		{
			return this.GroupEmailAddress.ToString();
		}

		// Token: 0x040004A1 RID: 1185
		public static readonly HygienePropertyDefinition GroupEmailAddressProp = new HygienePropertyDefinition("EmailAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004A2 RID: 1186
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("id_TenantId", typeof(Guid));

		// Token: 0x040004A3 RID: 1187
		public static readonly HygienePropertyDefinition GroupIdProp = new HygienePropertyDefinition("id_GroupId", typeof(Guid));
	}
}
