using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000123 RID: 291
	internal class DomainSchema : ADObjectSchema
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00021ACA File Offset: 0x0001FCCA
		public static ObjectId GetObjectId(Guid guid)
		{
			return new ConfigObjectId(guid.ToString());
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00021ADE File Offset: 0x0001FCDE
		public static object GetNullIfGuidEmpty(Guid guid)
		{
			if (guid != System.Guid.Empty)
			{
				return guid;
			}
			return null;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00021AF5 File Offset: 0x0001FCF5
		public static Guid GetGuidEmptyIfNull(object value)
		{
			if (value != null)
			{
				return (Guid)value;
			}
			return System.Guid.Empty;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00021B06 File Offset: 0x0001FD06
		public static object GetNullIfStringEmpty(string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x04000598 RID: 1432
		public static readonly HygienePropertyDefinition Identifier = new HygienePropertyDefinition("Identifier", typeof(Guid));

		// Token: 0x04000599 RID: 1433
		public static readonly HygienePropertyDefinition DomainTargetEnvironmentId = new HygienePropertyDefinition("DomainTargetEnvironmentId", typeof(Guid));

		// Token: 0x0400059A RID: 1434
		public static readonly HygienePropertyDefinition TargetServiceId = new HygienePropertyDefinition("TargetServiceId", typeof(Guid));

		// Token: 0x0400059B RID: 1435
		public static readonly HygienePropertyDefinition PropertiesAsId = new HygienePropertyDefinition("PropertiesAsIdTable", typeof(IDictionary<int, IDictionary<int, string>>));

		// Token: 0x0400059C RID: 1436
		public static readonly HygienePropertyDefinition DomainKey = new HygienePropertyDefinition("DomainKey", typeof(string));

		// Token: 0x0400059D RID: 1437
		public static readonly HygienePropertyDefinition UpdateDomainKey = new HygienePropertyDefinition("UpdateDomainKey", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400059E RID: 1438
		public static readonly HygienePropertyDefinition DomainKeys = new HygienePropertyDefinition("DomainKeys", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x0400059F RID: 1439
		public static readonly HygienePropertyDefinition TenantTargetEnvironmentId = new HygienePropertyDefinition("TenantTargetEnvironmentId", typeof(Guid));

		// Token: 0x040005A0 RID: 1440
		public static readonly HygienePropertyDefinition TenantId = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x040005A1 RID: 1441
		public static readonly HygienePropertyDefinition TenantIds = new HygienePropertyDefinition("OrganizationalUnitRoots", typeof(Guid), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040005A2 RID: 1442
		public static readonly HygienePropertyDefinition ZoneId = new HygienePropertyDefinition("ZoneId", typeof(Guid));

		// Token: 0x040005A3 RID: 1443
		public static readonly HygienePropertyDefinition ResourceRecordId = new HygienePropertyDefinition("ResourceRecordId", typeof(Guid));

		// Token: 0x040005A4 RID: 1444
		public static readonly HygienePropertyDefinition ResourceRecordTypeId = new HygienePropertyDefinition("ResourceRecordTypeId", typeof(Guid));

		// Token: 0x040005A5 RID: 1445
		public static readonly HygienePropertyDefinition DomainName = new HygienePropertyDefinition("DomainName", typeof(string));

		// Token: 0x040005A6 RID: 1446
		public static readonly HygienePropertyDefinition DomainNames = new HygienePropertyDefinition("DomainNames", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040005A7 RID: 1447
		public static readonly HygienePropertyDefinition NameServer = new HygienePropertyDefinition("NameServer", typeof(string));

		// Token: 0x040005A8 RID: 1448
		public static readonly HygienePropertyDefinition IpAddress = new HygienePropertyDefinition("IpAddress", typeof(string));

		// Token: 0x040005A9 RID: 1449
		public static readonly HygienePropertyDefinition PrimaryNameServer = new HygienePropertyDefinition("PrimaryNameServer", typeof(string));

		// Token: 0x040005AA RID: 1450
		public static readonly HygienePropertyDefinition ResponsibleMailServer = new HygienePropertyDefinition("ResponsibleMailServer", typeof(string));

		// Token: 0x040005AB RID: 1451
		public static readonly HygienePropertyDefinition Refresh = new HygienePropertyDefinition("Refresh", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005AC RID: 1452
		public static readonly HygienePropertyDefinition Retry = new HygienePropertyDefinition("Retry", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005AD RID: 1453
		public static readonly HygienePropertyDefinition Expire = new HygienePropertyDefinition("Expire", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005AE RID: 1454
		public static readonly HygienePropertyDefinition Serial = new HygienePropertyDefinition("Serial", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005AF RID: 1455
		public static readonly HygienePropertyDefinition DefaultTtl = new HygienePropertyDefinition("DefaultTtl", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005B0 RID: 1456
		public static readonly HygienePropertyDefinition UpdatedDomains = new HygienePropertyDefinition("UpdatedDomains", typeof(IEnumerable<string>));

		// Token: 0x040005B1 RID: 1457
		public static readonly HygienePropertyDefinition DomainKeyFlags = new HygienePropertyDefinition("DomainKeyFlags", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005B2 RID: 1458
		public static readonly HygienePropertyDefinition UserTargetEnvironmentId = new HygienePropertyDefinition("UserTargetEnvironmentId", typeof(Guid));

		// Token: 0x040005B3 RID: 1459
		public static readonly HygienePropertyDefinition UserKey = new HygienePropertyDefinition("UserKey", typeof(string));

		// Token: 0x040005B4 RID: 1460
		public static readonly HygienePropertyDefinition MSAUserName = new HygienePropertyDefinition("MSAUserName", typeof(string));

		// Token: 0x040005B5 RID: 1461
		public static readonly HygienePropertyDefinition ObjectStateProp = DalHelper.ObjectStateProp;

		// Token: 0x040005B6 RID: 1462
		public static readonly HygienePropertyDefinition IsTracerTokenProp = DalHelper.IsTracerTokenProp;

		// Token: 0x040005B7 RID: 1463
		public static readonly HygienePropertyDefinition WhenChangedProp = DalHelper.WhenChangedProp;

		// Token: 0x040005B8 RID: 1464
		public static readonly HygienePropertyDefinition CreatedDatetime = new HygienePropertyDefinition("CreatedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005B9 RID: 1465
		public static readonly HygienePropertyDefinition ChangedDatetime = new HygienePropertyDefinition("ChangedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005BA RID: 1466
		public static readonly HygienePropertyDefinition DeletedDatetime = new HygienePropertyDefinition("DeletedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005BB RID: 1467
		public static readonly HygienePropertyDefinition PropertyChangedDatetime = new HygienePropertyDefinition("PropertyChangedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005BC RID: 1468
		public static readonly HygienePropertyDefinition PropertyCreatedDatetime = new HygienePropertyDefinition("PropertyCreatedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005BD RID: 1469
		public static readonly HygienePropertyDefinition PropertyDeletedDatetime = new HygienePropertyDefinition("PropertyDeletedDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005BE RID: 1470
		public static readonly HygienePropertyDefinition PropertyId = new HygienePropertyDefinition("PropertyId", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005BF RID: 1471
		public static readonly HygienePropertyDefinition EntityId = new HygienePropertyDefinition("EntityId", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040005C0 RID: 1472
		public static readonly HygienePropertyDefinition PropertyValue = new HygienePropertyDefinition("PropertyValue", typeof(string));
	}
}
