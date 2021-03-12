using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C4 RID: 196
	internal class ADSpamScanSettingSchema : ADObjectSchema
	{
		// Token: 0x040003F1 RID: 1009
		public static readonly HygienePropertyDefinition ConfigurationIdProp = new HygienePropertyDefinition("configId", typeof(ADObjectId));

		// Token: 0x040003F2 RID: 1010
		public static readonly HygienePropertyDefinition ActionTypeIdProp = new HygienePropertyDefinition("actionTypeId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F3 RID: 1011
		public static readonly HygienePropertyDefinition ParameterProp = new HygienePropertyDefinition("parameter", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F4 RID: 1012
		public static readonly HygienePropertyDefinition CsfmImageProp = new HygienePropertyDefinition("csfmImage", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F5 RID: 1013
		public static readonly HygienePropertyDefinition CsfmEmptyProp = new HygienePropertyDefinition("csfmEmpty", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F6 RID: 1014
		public static readonly HygienePropertyDefinition CsfmScriptProp = new HygienePropertyDefinition("csfmScript", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F7 RID: 1015
		public static readonly HygienePropertyDefinition CsfmIframeProp = new HygienePropertyDefinition("csfmIframe", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F8 RID: 1016
		public static readonly HygienePropertyDefinition CsfmObjectProp = new HygienePropertyDefinition("csfmObject", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003F9 RID: 1017
		public static readonly HygienePropertyDefinition CsfmEmbedProp = new HygienePropertyDefinition("csfmEmbed", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003FA RID: 1018
		public static readonly HygienePropertyDefinition CsfmWebBugsProp = new HygienePropertyDefinition("csfmWebBugs", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003FB RID: 1019
		public static readonly HygienePropertyDefinition CsfmFormProp = new HygienePropertyDefinition("csfmForm", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003FC RID: 1020
		public static readonly HygienePropertyDefinition CsfmWordListProp = new HygienePropertyDefinition("csfmWordList", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003FD RID: 1021
		public static readonly HygienePropertyDefinition CsfmUrlNumericIPProp = new HygienePropertyDefinition("csfmUrlNumericIP", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003FE RID: 1022
		public static readonly HygienePropertyDefinition CsfmUrlRedirectProp = new HygienePropertyDefinition("csfmUrlRedirect", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040003FF RID: 1023
		public static readonly HygienePropertyDefinition CsfmWebsiteProp = new HygienePropertyDefinition("csfmWebsite", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000400 RID: 1024
		public static readonly HygienePropertyDefinition CsfmSpfFailProp = new HygienePropertyDefinition("csfmSpfFail", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000401 RID: 1025
		public static readonly HygienePropertyDefinition CsfmSpfFromFailProp = new HygienePropertyDefinition("csfmSpfFromFail", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000402 RID: 1026
		public static readonly HygienePropertyDefinition CsfmNdrBackScatterProp = new HygienePropertyDefinition("csfmNdrBackscatter", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000403 RID: 1027
		public static readonly HygienePropertyDefinition FlagsProp = new HygienePropertyDefinition("flags", typeof(SpamScanFlags), SpamScanFlags.AllowUserOptOut, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000404 RID: 1028
		public static readonly HygienePropertyDefinition CsfmTestBccAddressProp = new HygienePropertyDefinition("csfmTestBccAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
