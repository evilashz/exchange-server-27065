using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020001C0 RID: 448
	internal class DeviceCommonSchema
	{
		// Token: 0x04000907 RID: 2311
		public static HygienePropertyDefinition OrganizationalUnitRootProperty = new HygienePropertyDefinition("id_OrganizationalUnitRoot", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000908 RID: 2312
		public static HygienePropertyDefinition DeviceIdProperty = new HygienePropertyDefinition("id_DeviceId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000909 RID: 2313
		public static HygienePropertyDefinition EASIdProperty = new HygienePropertyDefinition("nvc_EASId", typeof(string), null, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x0400090A RID: 2314
		public static HygienePropertyDefinition IntuneIdProperty = new HygienePropertyDefinition("id_IntuneId", typeof(Guid?), null, ADPropertyDefinitionFlags.None);

		// Token: 0x0400090B RID: 2315
		public static HygienePropertyDefinition UserProperty = new HygienePropertyDefinition("nvc_User", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400090C RID: 2316
		public static HygienePropertyDefinition DeviceNameProperty = new HygienePropertyDefinition("nvc_DeviceName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400090D RID: 2317
		public static HygienePropertyDefinition DeviceModelProperty = new HygienePropertyDefinition("nvc_DeviceModel", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400090E RID: 2318
		public static HygienePropertyDefinition DeviceTypeProperty = new HygienePropertyDefinition("nvc_DeviceType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400090F RID: 2319
		public static HygienePropertyDefinition FirstSyncTimeProperty = new HygienePropertyDefinition("dt_FirstSyncTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000910 RID: 2320
		public static HygienePropertyDefinition LastSyncTimeProperty = new HygienePropertyDefinition("dt_LastSyncTime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000911 RID: 2321
		public static HygienePropertyDefinition IMEIProperty = new HygienePropertyDefinition("nvc_IMEI", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000912 RID: 2322
		public static HygienePropertyDefinition PhoneNumberProperty = new HygienePropertyDefinition("nvc_PhoneNumber", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000913 RID: 2323
		public static HygienePropertyDefinition MobileNetworkProperty = new HygienePropertyDefinition("nvc_MobileNetwork", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000914 RID: 2324
		public static HygienePropertyDefinition EASVersionProperty = new HygienePropertyDefinition("nvc_EASVersion", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000915 RID: 2325
		public static HygienePropertyDefinition UserAgentProperty = new HygienePropertyDefinition("nvc_UserAgent", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000916 RID: 2326
		public static HygienePropertyDefinition DeviceLanguageProperty = new HygienePropertyDefinition("nvc_DeviceLanguage", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000917 RID: 2327
		public static HygienePropertyDefinition DeletedTimeProperty = new HygienePropertyDefinition("dt_DeletedTime", typeof(DateTime?), null, ADPropertyDefinitionFlags.None);

		// Token: 0x04000918 RID: 2328
		public static HygienePropertyDefinition ActivityIdProperty = new HygienePropertyDefinition("id_ActivityId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.Mandatory);

		// Token: 0x04000919 RID: 2329
		public static HygienePropertyDefinition TimeStampProperty = new HygienePropertyDefinition("dt_TimeStamp", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400091A RID: 2330
		public static HygienePropertyDefinition DateKeyProperty = new HygienePropertyDefinition("i_DateKey", typeof(int), 19700101, ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400091B RID: 2331
		public static HygienePropertyDefinition PlatformProperty = new HygienePropertyDefinition("nvc_Platform", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400091C RID: 2332
		public static HygienePropertyDefinition AccessStateProperty = new HygienePropertyDefinition("i_AccessState", typeof(int?), null, ADPropertyDefinitionFlags.None);

		// Token: 0x0400091D RID: 2333
		public static HygienePropertyDefinition AccessStateReasonProperty = new HygienePropertyDefinition("i_AccessStateReason", typeof(int?), null, ADPropertyDefinitionFlags.None);

		// Token: 0x0400091E RID: 2334
		public static HygienePropertyDefinition AccessSetByProperty = new HygienePropertyDefinition("nvc_AccessSetBy", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400091F RID: 2335
		public static HygienePropertyDefinition PolicyAppliedProperty = new HygienePropertyDefinition("nvc_PolicyApplied", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000920 RID: 2336
		public static readonly HygienePropertyDefinition HashBucketProperty = new HygienePropertyDefinition("si_HashBucket", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
