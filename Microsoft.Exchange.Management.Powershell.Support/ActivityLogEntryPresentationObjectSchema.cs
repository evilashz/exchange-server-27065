using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200000C RID: 12
	internal class ActivityLogEntryPresentationObjectSchema : ObjectSchema
	{
		// Token: 0x04000046 RID: 70
		public static readonly SimplePropertyDefinition ClientId = new SimplePropertyDefinition("ClientId", typeof(string), Microsoft.Exchange.Data.Storage.ActivityLog.ClientId.Min.ToString());

		// Token: 0x04000047 RID: 71
		public static readonly SimplePropertyDefinition ActivityId = new SimplePropertyDefinition("ActivityId", typeof(string), Enum.GetName(typeof(ActivityId), Microsoft.Exchange.Data.Storage.ActivityLog.ActivityId.Min));

		// Token: 0x04000048 RID: 72
		public static readonly SimplePropertyDefinition TimeStamp = new SimplePropertyDefinition("TimeStamp", typeof(ExDateTime), ExDateTime.MinValue);

		// Token: 0x04000049 RID: 73
		public static readonly SimplePropertyDefinition EntryId = new SimplePropertyDefinition("EntryId", typeof(string), string.Empty);

		// Token: 0x0400004A RID: 74
		public static readonly SimplePropertyDefinition CustomProperties = new SimplePropertyDefinition("CustomProperties", typeof(string), string.Empty);
	}
}
