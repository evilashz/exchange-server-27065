using System;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F4 RID: 244
	internal class Role : ConfigurablePropertyBag
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0001DA25 File Offset: 0x0001BC25
		public Role()
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001DA2D File Offset: 0x0001BC2D
		internal Role(string roleGroupName, RoleSet roleGroupSet, string roleName, RoleSet roleSet, string roleEntryName, string roleEntryParameter, RoleEntryType roleEntryType) : this(roleName, roleSet, roleEntryName, roleEntryParameter, roleEntryType)
		{
			if (string.IsNullOrEmpty(roleGroupName))
			{
				throw new ArgumentException("roleGroupName");
			}
			this.RoleGroupName = roleGroupName;
			this.RoleGroupSet = roleGroupSet;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001DA60 File Offset: 0x0001BC60
		internal Role(string roleName, RoleSet roleSet, string roleEntryName, string roleEntryParameter, RoleEntryType roleEntryType)
		{
			if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentException("roleName");
			}
			if (string.IsNullOrEmpty(roleEntryName))
			{
				throw new ArgumentException("roleEntryName");
			}
			this.RoleName = roleName;
			this.RoleSet = roleSet;
			this.RoleEntryName = roleEntryName;
			this.RoleEntryType = roleEntryType;
			this.RoleEntryParameter = roleEntryParameter;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0001DABE File Offset: 0x0001BCBE
		public Guid RoleGroupId
		{
			get
			{
				return (Guid)this[Role.RoleGroupIdDef];
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0001DAE2 File Offset: 0x0001BCE2
		public string RoleGroupName
		{
			get
			{
				return this[Role.RoleGroupNameDef] as string;
			}
			internal set
			{
				this[Role.RoleGroupNameDef] = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0001DB02 File Offset: 0x0001BD02
		public RoleSet RoleGroupSet
		{
			get
			{
				return (RoleSet)this[Role.RoleGroupSetDef];
			}
			internal set
			{
				this[Role.RoleGroupSetDef] = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001DB15 File Offset: 0x0001BD15
		public Guid RoleId
		{
			get
			{
				return (Guid)this[Role.RoleIdDef];
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001DB27 File Offset: 0x0001BD27
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x0001DB39 File Offset: 0x0001BD39
		public string RoleName
		{
			get
			{
				return this[Role.RoleNameDef] as string;
			}
			internal set
			{
				this[Role.RoleNameDef] = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001DB47 File Offset: 0x0001BD47
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0001DB59 File Offset: 0x0001BD59
		public RoleSet RoleSet
		{
			get
			{
				return (RoleSet)this[Role.RoleSetDef];
			}
			internal set
			{
				this[Role.RoleSetDef] = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0001DB7E File Offset: 0x0001BD7E
		public string RoleEntryName
		{
			get
			{
				return this[Role.RoleEntryNameDef] as string;
			}
			internal set
			{
				this[Role.RoleEntryNameDef] = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0001DB9E File Offset: 0x0001BD9E
		public string RoleEntryParameter
		{
			get
			{
				return this[Role.RoleEntryParametersDef] as string;
			}
			internal set
			{
				this[Role.RoleEntryParametersDef] = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0001DBBE File Offset: 0x0001BDBE
		public RoleEntryType RoleEntryType
		{
			get
			{
				return (RoleEntryType)this[Role.RoleEntryTypeDef];
			}
			internal set
			{
				this[Role.RoleEntryTypeDef] = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001DBDC File Offset: 0x0001BDDC
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0001DC3A File Offset: 0x0001BE3A
		public string[] RoleEntryFeatures
		{
			get
			{
				string text = this[Role.RoleEntryFeaturesDef] as string;
				if (text != null)
				{
					return (from x in text.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries)
					select x.Trim()).ToArray<string>();
				}
				return null;
			}
			internal set
			{
				if (value != null)
				{
					this[Role.RoleEntryFeaturesDef] = string.Join(", ", value);
					return;
				}
				this[Role.RoleEntryFeaturesDef] = null;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public override ObjectId Identity
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.RoleGroupName))
				{
					return new ConfigObjectId(this.RoleName + "_" + this.RoleEntryName);
				}
				return new ConfigObjectId(string.Concat(new string[]
				{
					this.RoleGroupName,
					"_",
					this.RoleName,
					"_",
					this.RoleEntryName
				}));
			}
		}

		// Token: 0x0400050A RID: 1290
		internal static readonly HygienePropertyDefinition RoleGroupIdDef = new HygienePropertyDefinition("roleGroupId", typeof(Guid));

		// Token: 0x0400050B RID: 1291
		internal static readonly HygienePropertyDefinition RoleGroupNameDef = new HygienePropertyDefinition("roleGroupName", typeof(string));

		// Token: 0x0400050C RID: 1292
		internal static readonly HygienePropertyDefinition RoleGroupSetDef = new HygienePropertyDefinition("roleGroupSet", typeof(RoleSet));

		// Token: 0x0400050D RID: 1293
		internal static readonly HygienePropertyDefinition RoleIdDef = new HygienePropertyDefinition("roleId", typeof(Guid));

		// Token: 0x0400050E RID: 1294
		internal static readonly HygienePropertyDefinition RoleNameDef = new HygienePropertyDefinition("roleName", typeof(string));

		// Token: 0x0400050F RID: 1295
		internal static readonly HygienePropertyDefinition RoleSetDef = new HygienePropertyDefinition("roleSet", typeof(RoleSet));

		// Token: 0x04000510 RID: 1296
		internal static readonly HygienePropertyDefinition RoleEntryTypeDef = new HygienePropertyDefinition("roleEntryType", typeof(RoleEntryType));

		// Token: 0x04000511 RID: 1297
		internal static readonly HygienePropertyDefinition RoleEntryNameDef = new HygienePropertyDefinition("roleEntryName", typeof(string));

		// Token: 0x04000512 RID: 1298
		internal static readonly HygienePropertyDefinition RoleEntryParametersDef = new HygienePropertyDefinition("roleEntryParameters", typeof(string));

		// Token: 0x04000513 RID: 1299
		internal static readonly HygienePropertyDefinition RoleEntryFeaturesDef = new HygienePropertyDefinition("roleEntryFeatures", typeof(string));
	}
}
