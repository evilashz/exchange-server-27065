using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000056 RID: 86
	[DataContract(Name = "conferenceResource")]
	[Parent("conversation")]
	[Get(typeof(ConversationConferenceResource))]
	internal class ConversationConferenceResource : MeetingResource
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00009220 File Offset: 0x00007420
		public ConversationConferenceResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00009229 File Offset: 0x00007429
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00009236 File Offset: 0x00007436
		[DataMember(Name = "organizerName", EmitDefaultValue = false)]
		public string OrganizerName
		{
			get
			{
				return base.GetValue<string>("organizerName");
			}
			set
			{
				base.SetValue<string>("organizerName", value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00009244 File Offset: 0x00007444
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00009251 File Offset: 0x00007451
		[DataMember(Name = "disclaimerBody", EmitDefaultValue = false)]
		public string Disclaimer
		{
			get
			{
				return base.GetValue<string>("disclaimerBody");
			}
			set
			{
				base.SetValue<string>("disclaimerBody", value);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000925F File Offset: 0x0000745F
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000926C File Offset: 0x0000746C
		[DataMember(Name = "disclaimerTitle", EmitDefaultValue = false)]
		public string DisclaimerTitle
		{
			get
			{
				return base.GetValue<string>("disclaimerTitle");
			}
			set
			{
				base.SetValue<string>("disclaimerTitle", value);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000927A File Offset: 0x0000747A
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00009287 File Offset: 0x00007487
		[DataMember(Name = "hostingNetwork", EmitDefaultValue = false)]
		public string HostingNetwork
		{
			get
			{
				return base.GetValue<string>("hostingNetwork");
			}
			set
			{
				base.SetValue<string>("hostingNetwork", value);
			}
		}

		// Token: 0x040001AC RID: 428
		public const string Token = "onlineMeeting";
	}
}
