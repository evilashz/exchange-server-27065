using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200049C RID: 1180
	[OwaEventStruct("SRcp")]
	public sealed class SchedulingRecipientInfo
	{
		// Token: 0x04001E27 RID: 7719
		public const string StructNamespace = "SRcp";

		// Token: 0x04001E28 RID: 7720
		public const string IDName = "ID";

		// Token: 0x04001E29 RID: 7721
		public const string EmailAddressName = "EM";

		// Token: 0x04001E2A RID: 7722
		public const string RoutingTypeName = "RT";

		// Token: 0x04001E2B RID: 7723
		public const string DisplayNameName = "DN";

		// Token: 0x04001E2C RID: 7724
		public const string GetFreeBusyDataName = "FB";

		// Token: 0x04001E2D RID: 7725
		public const string AttendeeTypeName = "AT";

		// Token: 0x04001E2E RID: 7726
		[OwaEventField("ID", false, "")]
		public string ID;

		// Token: 0x04001E2F RID: 7727
		[OwaEventField("EM", false, "")]
		public string EmailAddress;

		// Token: 0x04001E30 RID: 7728
		[OwaEventField("RT", false, "")]
		public string RoutingType;

		// Token: 0x04001E31 RID: 7729
		[OwaEventField("DN", true, null)]
		public string DisplayName;

		// Token: 0x04001E32 RID: 7730
		[OwaEventField("FB", true, false)]
		public bool GetFreeBusyData;

		// Token: 0x04001E33 RID: 7731
		[OwaEventField("AT", false, MeetingAttendeeType.Required)]
		public MeetingAttendeeType AttendeeType;
	}
}
