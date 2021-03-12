using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200006A RID: 106
	[DataContract(Name = "ConferenceParticipantInformation")]
	internal class OnlineMeetingParticipantInformationResource : Resource
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000976B File Offset: 0x0000796B
		public OnlineMeetingParticipantInformationResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009774 File Offset: 0x00007974
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00009781 File Offset: 0x00007981
		[DataMember(Name = "Role", EmitDefaultValue = false)]
		public ConferencingRole Role
		{
			get
			{
				return base.GetValue<ConferencingRole>("Role");
			}
			set
			{
				base.SetValue<ConferencingRole>("Role", value);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009794 File Offset: 0x00007994
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000097A1 File Offset: 0x000079A1
		[DataMember(Name = "Uri", EmitDefaultValue = false)]
		public string Uri
		{
			get
			{
				return base.GetValue<string>("Uri");
			}
			set
			{
				base.SetValue<string>("Uri", value);
			}
		}

		// Token: 0x040001EB RID: 491
		public const string Token = "onlinemeetingparticipantinformation";
	}
}
