using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Exchange.Hygiene.Migrate1415.AlexToDal
{
	// Token: 0x020000EB RID: 235
	internal class MigrationCookie : ConfigurablePropertyBag
	{
		// Token: 0x0600092F RID: 2351 RVA: 0x0001CE1D File Offset: 0x0001B01D
		public MigrationCookie()
		{
			this.ID = Guid.NewGuid();
			this.Cookie = MigrationCookie.MinimumCookieValue;
			this.DirectionId = MailDirection.Inbound;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001CE42 File Offset: 0x0001B042
		internal MigrationCookie(ADObjectId tenantId, ObjectId configId, string name)
		{
			this.ID = Guid.NewGuid();
			this.ConfigurationId = configId;
			this.Name = name;
			this.Cookie = MigrationCookie.MinimumCookieValue;
			this.DirectionId = MailDirection.Inbound;
			this.TenantId = tenantId;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001CE7C File Offset: 0x0001B07C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ID.ToString());
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0001CEA2 File Offset: 0x0001B0A2
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		internal string Name
		{
			get
			{
				return (string)this[MigrationCookie.CookieNameProperty];
			}
			set
			{
				this[MigrationCookie.CookieNameProperty] = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0001CEC2 File Offset: 0x0001B0C2
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		internal byte[] Cookie
		{
			get
			{
				return (byte[])this[MigrationCookie.CookieValueProperty];
			}
			set
			{
				this[MigrationCookie.CookieValueProperty] = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x0001CEE2 File Offset: 0x0001B0E2
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		internal ObjectId ConfigurationId
		{
			get
			{
				return (ObjectId)this[MigrationCookie.ConfigurationIdProp];
			}
			set
			{
				this[MigrationCookie.ConfigurationIdProp] = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001CF02 File Offset: 0x0001B102
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001CF14 File Offset: 0x0001B114
		internal Guid ID
		{
			get
			{
				return (Guid)this[MigrationCookie.IDProperty];
			}
			set
			{
				this[MigrationCookie.IDProperty] = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001CF27 File Offset: 0x0001B127
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0001CF39 File Offset: 0x0001B139
		internal MailDirection DirectionId
		{
			get
			{
				return (MailDirection)this[MigrationCookie.DirectionIdProp];
			}
			set
			{
				this[MigrationCookie.DirectionIdProp] = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001CF4C File Offset: 0x0001B14C
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0001CF5E File Offset: 0x0001B15E
		internal ADObjectId TenantId
		{
			get
			{
				return this[ADObjectSchema.OrganizationalUnitRoot] as ADObjectId;
			}
			set
			{
				this[ADObjectSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001CF6C File Offset: 0x0001B16C
		// Note: this type is marked as 'beforefieldinit'.
		static MigrationCookie()
		{
			byte[] minimumCookieValue = new byte[8];
			MigrationCookie.MinimumCookieValue = minimumCookieValue;
			MigrationCookie.CookieNameProperty = new HygienePropertyDefinition("Name", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
			MigrationCookie.CookieValueProperty = new HygienePropertyDefinition("cookieValue", typeof(byte[]), MigrationCookie.MinimumCookieValue, ADPropertyDefinitionFlags.PersistDefaultValue);
			MigrationCookie.ConfigurationIdProp = new HygienePropertyDefinition("configId", typeof(ADObjectId));
			MigrationCookie.IDProperty = new HygienePropertyDefinition("ID", typeof(Guid));
			MigrationCookie.DirectionIdProp = new HygienePropertyDefinition("directionId", typeof(MailDirection), MailDirection.Inbound, ADPropertyDefinitionFlags.PersistDefaultValue);
		}

		// Token: 0x040004CA RID: 1226
		internal static readonly byte[] MinimumCookieValue;

		// Token: 0x040004CB RID: 1227
		internal static readonly HygienePropertyDefinition CookieNameProperty;

		// Token: 0x040004CC RID: 1228
		internal static readonly HygienePropertyDefinition CookieValueProperty;

		// Token: 0x040004CD RID: 1229
		internal static readonly HygienePropertyDefinition ConfigurationIdProp;

		// Token: 0x040004CE RID: 1230
		internal static readonly HygienePropertyDefinition IDProperty;

		// Token: 0x040004CF RID: 1231
		internal static readonly HygienePropertyDefinition DirectionIdProp;
	}
}
