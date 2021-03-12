using System;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F7 RID: 247
	internal class ProbeOrganizationInfo : ADObject
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001DED6 File Offset: 0x0001C0D6
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		public ADObjectId ProbeOrganizationId
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

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001DEF6 File Offset: 0x0001C0F6
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0001DF08 File Offset: 0x0001C108
		public string FeatureTag
		{
			get
			{
				return this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.FeatureTagProperty] as string;
			}
			set
			{
				this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.FeatureTagProperty] = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001DF16 File Offset: 0x0001C116
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0001DF28 File Offset: 0x0001C128
		public string Region
		{
			get
			{
				return this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.RegionProperty] as string;
			}
			set
			{
				this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.RegionProperty] = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0001DF36 File Offset: 0x0001C136
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0001DF48 File Offset: 0x0001C148
		public SecureString LoginPassword
		{
			get
			{
				return this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.LoginPasswordProperty] as SecureString;
			}
			set
			{
				this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.LoginPasswordProperty] = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001DF56 File Offset: 0x0001C156
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0001DF68 File Offset: 0x0001C168
		public CustomerType CustomerType
		{
			get
			{
				return (CustomerType)this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.CustomerTypeProperty];
			}
			set
			{
				this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.CustomerTypeProperty] = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001DF7B File Offset: 0x0001C17B
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001DF8D File Offset: 0x0001C18D
		public string OrganizationName
		{
			get
			{
				return this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.OrganizationNameProperty] as string;
			}
			set
			{
				this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.OrganizationNameProperty] = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001DF9B File Offset: 0x0001C19B
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0001DFAD File Offset: 0x0001C1AD
		public string InitializationScript
		{
			get
			{
				return this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.InitializationScriptProperty] as string;
			}
			set
			{
				this[ProbeOrganizationInfo.ProbeOrganizationInfoSchema.InitializationScriptProperty] = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001DFBB File Offset: 0x0001C1BB
		internal override ADObjectSchema Schema
		{
			get
			{
				return ProbeOrganizationInfo.schema;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0001DFC2 File Offset: 0x0001C1C2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ProbeOrganizationInfo.mostDerivedClass;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001DFC9 File Offset: 0x0001C1C9
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public static ProbeEnvironment FindEnvironment(string tenantName)
		{
			if (Regex.IsMatch(tenantName, "@.*msol-test.com", RegexOptions.IgnoreCase))
			{
				return ProbeEnvironment.Test;
			}
			if (Regex.IsMatch(tenantName, "@.*ccsctp.net", RegexOptions.IgnoreCase))
			{
				return ProbeEnvironment.Dogfood;
			}
			if (Regex.IsMatch(tenantName, "@.*onmicrosoft.com", RegexOptions.IgnoreCase))
			{
				return ProbeEnvironment.Production;
			}
			throw new ArgumentException(string.Format("Tenant name {0} has an invalid suffix.", tenantName));
		}

		// Token: 0x0400051A RID: 1306
		private const string TestEnvironmentSuffix = "@.*msol-test.com";

		// Token: 0x0400051B RID: 1307
		private const string DogfoodEnvironmentSuffix = "@.*ccsctp.net";

		// Token: 0x0400051C RID: 1308
		private const string ProductionEnvironmentSuffix = "@.*onmicrosoft.com";

		// Token: 0x0400051D RID: 1309
		private static readonly string mostDerivedClass = "ProbeOrganizationInfo";

		// Token: 0x0400051E RID: 1310
		private static readonly ProbeOrganizationInfo.ProbeOrganizationInfoSchema schema = ObjectSchema.GetInstance<ProbeOrganizationInfo.ProbeOrganizationInfoSchema>();

		// Token: 0x020000F8 RID: 248
		internal class ProbeOrganizationInfoSchema : ADObjectSchema
		{
			// Token: 0x0400051F RID: 1311
			internal static readonly HygienePropertyDefinition FeatureTagProperty = new HygienePropertyDefinition("FeatureTag", typeof(string));

			// Token: 0x04000520 RID: 1312
			internal static readonly HygienePropertyDefinition RegionProperty = new HygienePropertyDefinition("Region", typeof(string));

			// Token: 0x04000521 RID: 1313
			internal static readonly HygienePropertyDefinition LoginPasswordProperty = new HygienePropertyDefinition("LoginPassword", typeof(SecureString));

			// Token: 0x04000522 RID: 1314
			internal static readonly HygienePropertyDefinition CustomerTypeProperty = new HygienePropertyDefinition("CustomerType", typeof(CustomerType));

			// Token: 0x04000523 RID: 1315
			internal static readonly HygienePropertyDefinition OrganizationNameProperty = new HygienePropertyDefinition("OrganizationName", typeof(string));

			// Token: 0x04000524 RID: 1316
			internal static readonly HygienePropertyDefinition InitializationScriptProperty = new HygienePropertyDefinition("InitializationScript", typeof(string));
		}
	}
}
