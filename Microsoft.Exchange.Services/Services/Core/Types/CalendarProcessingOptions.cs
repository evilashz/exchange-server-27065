using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000A69 RID: 2665
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarProcessingOptions : OptionsPropertyChangeTracker
	{
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x00105B3E File Offset: 0x00103D3E
		// (set) Token: 0x06004B9E RID: 19358 RVA: 0x00105B46 File Offset: 0x00103D46
		[DataMember]
		public bool RemoveOldMeetingMessages
		{
			get
			{
				return this.removeOldMeetingMessages;
			}
			set
			{
				this.removeOldMeetingMessages = value;
				base.TrackPropertyChanged("RemoveOldMeetingMessages");
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x00105B5A File Offset: 0x00103D5A
		// (set) Token: 0x06004BA0 RID: 19360 RVA: 0x00105B62 File Offset: 0x00103D62
		[DataMember]
		public bool RemoveForwardedMeetingNotifications
		{
			get
			{
				return this.removeForwardedMeetingNotifications;
			}
			set
			{
				this.removeForwardedMeetingNotifications = value;
				base.TrackPropertyChanged("RemoveForwardedMeetingNotifications");
			}
		}

		// Token: 0x04002AF7 RID: 10999
		private bool removeOldMeetingMessages;

		// Token: 0x04002AF8 RID: 11000
		private bool removeForwardedMeetingNotifications;
	}
}
