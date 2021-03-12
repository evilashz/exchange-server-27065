using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E1 RID: 225
	internal class FfoTraceTenant : ConfigurablePropertyBag
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001B552 File Offset: 0x00019752
		public override ObjectId Identity
		{
			get
			{
				return this.TenantId;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0001B55A File Offset: 0x0001975A
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0001B56C File Offset: 0x0001976C
		public ADObjectId TenantId
		{
			get
			{
				return this[FfoTraceTenant.TenantIdProp] as ADObjectId;
			}
			set
			{
				this[FfoTraceTenant.TenantIdProp] = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001B57A File Offset: 0x0001977A
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0001B58C File Offset: 0x0001978C
		public string DatabaseName
		{
			get
			{
				return this[FfoTraceTenant.DatabaseNameProp] as string;
			}
			set
			{
				this[FfoTraceTenant.DatabaseNameProp] = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001B59A File Offset: 0x0001979A
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0001B5AC File Offset: 0x000197AC
		public DateTime ChangedDatetime
		{
			get
			{
				return (DateTime)this[FfoTraceTenant.ChangedDatetimeProp];
			}
			set
			{
				this[FfoTraceTenant.ChangedDatetimeProp] = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001B5BF File Offset: 0x000197BF
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0001B5D1 File Offset: 0x000197D1
		public DateTime CurrentDatetime
		{
			get
			{
				return (DateTime)this[FfoTraceTenant.CurrentDatetimeProp];
			}
			set
			{
				this[FfoTraceTenant.CurrentDatetimeProp] = value;
			}
		}

		// Token: 0x04000493 RID: 1171
		public static readonly ADPropertyDefinition TenantIdProp = ADObjectSchema.OrganizationalUnitRoot;

		// Token: 0x04000494 RID: 1172
		public static readonly HygienePropertyDefinition DatabaseNameProp = new HygienePropertyDefinition("DatabaseName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000495 RID: 1173
		public static readonly HygienePropertyDefinition ChangedDatetimeProp = new HygienePropertyDefinition("ChangedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000496 RID: 1174
		public static readonly HygienePropertyDefinition CurrentDatetimeProp = new HygienePropertyDefinition("CurrentDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
