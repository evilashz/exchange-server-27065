using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A14 RID: 2580
	[DataContract]
	public class CalendarShareInviteResponse : CalendarActionResponse
	{
		// Token: 0x060048D4 RID: 18644 RVA: 0x00101E3C File Offset: 0x0010003C
		public CalendarShareInviteResponse()
		{
			this.success = new List<string>();
			this.failure = new List<ShareInviteFailure>();
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x00101E5A File Offset: 0x0010005A
		public void AddSucessResponse(string emailAddress)
		{
			this.success.Add(emailAddress);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00101E68 File Offset: 0x00100068
		public void AddFailureResponse(string emailAddress, string message)
		{
			this.failure.Add(new ShareInviteFailure
			{
				Recipient = emailAddress,
				Error = message
			});
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x00101E95 File Offset: 0x00100095
		[DataMember]
		public string[] SuccessResponses
		{
			get
			{
				return this.success.ToArray();
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x00101EA2 File Offset: 0x001000A2
		[DataMember]
		public ShareInviteFailure[] FailureResponses
		{
			get
			{
				return this.failure.ToArray();
			}
		}

		// Token: 0x040029C2 RID: 10690
		private readonly List<string> success;

		// Token: 0x040029C3 RID: 10691
		private readonly List<ShareInviteFailure> failure;
	}
}
