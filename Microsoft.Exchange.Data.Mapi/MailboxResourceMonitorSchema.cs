using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000027 RID: 39
	internal sealed class MailboxResourceMonitorSchema : MapiObjectSchema
	{
		// Token: 0x040000DD RID: 221
		public static readonly MapiPropertyDefinition DigestCategory = new MapiPropertyDefinition("DigestCategory", typeof(string), PropTag.DigestCategory, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 256)
		});

		// Token: 0x040000DE RID: 222
		public static readonly MapiPropertyDefinition SampleId = new MapiPropertyDefinition("SampleId", typeof(uint?), PropTag.SampleId, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000DF RID: 223
		public static readonly MapiPropertyDefinition SampleTime = new MapiPropertyDefinition("SampleTime", typeof(DateTime?), PropTag.SampleTime, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E0 RID: 224
		public static readonly MapiPropertyDefinition DisplayName = MapiPropertyDefinitions.DisplayName;

		// Token: 0x040000E1 RID: 225
		public static readonly MapiPropertyDefinition MailboxGuid = MapiPropertyDefinitions.MailboxGuid;

		// Token: 0x040000E2 RID: 226
		public static readonly MapiPropertyDefinition TimeInServer = new MapiPropertyDefinition("TimeInServer", typeof(uint?), PropTag.TimeInServer, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E3 RID: 227
		public static readonly MapiPropertyDefinition TimeInCPU = new MapiPropertyDefinition("TimeInCPU", typeof(uint?), PropTag.TimeInCPU, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E4 RID: 228
		public static readonly MapiPropertyDefinition ROPCount = new MapiPropertyDefinition("ROPCount", typeof(uint?), PropTag.ROPCount, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E5 RID: 229
		public static readonly MapiPropertyDefinition PageRead = new MapiPropertyDefinition("PageRead", typeof(uint?), PropTag.PageRead, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E6 RID: 230
		public static readonly MapiPropertyDefinition PagePreread = new MapiPropertyDefinition("PagePreread", typeof(uint?), PropTag.PagePreread, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E7 RID: 231
		public static readonly MapiPropertyDefinition LogRecordCount = new MapiPropertyDefinition("LogRecordCount", typeof(uint?), PropTag.LogRecordCount, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E8 RID: 232
		public static readonly MapiPropertyDefinition LogRecordBytes = new MapiPropertyDefinition("LogRecordBytes", typeof(uint?), PropTag.LogRecordBytes, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000E9 RID: 233
		public static readonly MapiPropertyDefinition LdapReads = new MapiPropertyDefinition("LdapReads", typeof(uint?), PropTag.LdapReads, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000EA RID: 234
		public static readonly MapiPropertyDefinition LdapSearches = new MapiPropertyDefinition("LdapSearches", typeof(uint?), PropTag.LdapSearches, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000EB RID: 235
		public static readonly MapiPropertyDefinition IsQuarantined = new MapiPropertyDefinition("IsQuarantined", typeof(bool?), PropTag.MailboxQuarantined, MapiPropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
