using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000140 RID: 320
	internal class CommonReportingSchema
	{
		// Token: 0x04000634 RID: 1588
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000635 RID: 1589
		internal static readonly HygienePropertyDefinition DataSourceProperty = new HygienePropertyDefinition("DataSource", typeof(string), "EXO", ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000636 RID: 1590
		internal static readonly HygienePropertyDefinition TrafficTypeProperty = new HygienePropertyDefinition("TrafficType", typeof(string), string.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000637 RID: 1591
		internal static readonly HygienePropertyDefinition DateKeyProperty = new HygienePropertyDefinition("DateKey", typeof(int), 0, ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000638 RID: 1592
		internal static readonly HygienePropertyDefinition HourKeyProperty = new HygienePropertyDefinition("HourKey", typeof(short), 0, ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000639 RID: 1593
		internal static readonly HygienePropertyDefinition TenantDomainProperty = new HygienePropertyDefinition("TenantDomain", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400063A RID: 1594
		internal static readonly HygienePropertyDefinition MessageCountProperty = new HygienePropertyDefinition("MessageCount", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400063B RID: 1595
		internal static readonly HygienePropertyDefinition RecipientCountProperty = new HygienePropertyDefinition("RecipientCount", typeof(long), 0L, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400063C RID: 1596
		internal static readonly HygienePropertyDefinition DomainHashKeyProp = new HygienePropertyDefinition("DomainHashKey", typeof(byte[]));

		// Token: 0x0400063D RID: 1597
		internal static readonly HygienePropertyDefinition StartDateKeyProperty = new HygienePropertyDefinition("StartDateKey", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400063E RID: 1598
		internal static readonly HygienePropertyDefinition StartHourKeyProperty = new HygienePropertyDefinition("StartHourKey", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400063F RID: 1599
		internal static readonly HygienePropertyDefinition EndDateKeyProperty = new HygienePropertyDefinition("EndDateKey", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000640 RID: 1600
		internal static readonly HygienePropertyDefinition EndHourKeyProperty = new HygienePropertyDefinition("EndHourKey", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
