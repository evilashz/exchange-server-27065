using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ABContactSchema : ABObjectSchema
	{
		// Token: 0x0400000B RID: 11
		public static readonly ABPropertyDefinition BusinessPhoneNumber = new ABPropertyDefinition("BusinessPhoneNumber", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400000C RID: 12
		public static readonly ABPropertyDefinition CompanyName = new ABPropertyDefinition("CompanyName", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400000D RID: 13
		public static readonly ABPropertyDefinition DepartmentName = new ABPropertyDefinition("DepartmentName", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400000E RID: 14
		public static readonly ABPropertyDefinition BusinessFaxNumber = new ABPropertyDefinition("BusinessFax", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400000F RID: 15
		public static readonly ABPropertyDefinition GivenName = new ABPropertyDefinition("GivenName", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000010 RID: 16
		public static readonly ABPropertyDefinition HomePhoneNumber = new ABPropertyDefinition("HomePhoneNumber", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000011 RID: 17
		public static readonly ABPropertyDefinition Initials = new ABPropertyDefinition("Initials", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000012 RID: 18
		public static readonly ABPropertyDefinition MobilePhoneNumber = new ABPropertyDefinition("MobilePhoneNumber", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000013 RID: 19
		public static readonly ABPropertyDefinition OfficeLocation = new ABPropertyDefinition("OfficeLocation", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000014 RID: 20
		public static readonly ABPropertyDefinition Surname = new ABPropertyDefinition("Surname", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000015 RID: 21
		public static readonly ABPropertyDefinition WebPage = new ABPropertyDefinition("WebPage", typeof(Uri), PropertyDefinitionFlags.ReadOnly, null);

		// Token: 0x04000016 RID: 22
		public static readonly ABPropertyDefinition Title = new ABPropertyDefinition("Title", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000017 RID: 23
		public static readonly ABPropertyDefinition WorkAddressPostOfficeBox = new ABPropertyDefinition("WorkAddressPostOfficeBox", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000018 RID: 24
		public static readonly ABPropertyDefinition WorkAddressStreet = new ABPropertyDefinition("WorkAddressStreet", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x04000019 RID: 25
		public static readonly ABPropertyDefinition WorkAddressCity = new ABPropertyDefinition("WorkAddressCity", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400001A RID: 26
		public static readonly ABPropertyDefinition WorkAddressState = new ABPropertyDefinition("WorkAddressState", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400001B RID: 27
		public static readonly ABPropertyDefinition WorkAddressPostalCode = new ABPropertyDefinition("WorkAddressPostalCode", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400001C RID: 28
		public static readonly ABPropertyDefinition WorkAddressCountry = new ABPropertyDefinition("WorkAddressCountry", typeof(string), PropertyDefinitionFlags.ReadOnly, string.Empty);

		// Token: 0x0400001D RID: 29
		public static readonly ABPropertyDefinition Picture = new ABPropertyDefinition("ThumbnailPhoto", typeof(byte[]), PropertyDefinitionFlags.ReadOnly, null);
	}
}
