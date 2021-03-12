using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000070 RID: 112
	[DataContract(Name = "phoneDialInInformationResource")]
	internal class PhoneDialInInformationResource : OnlineMeetingCapabilityResource
	{
		// Token: 0x06000323 RID: 803 RVA: 0x000098EC File Offset: 0x00007AEC
		public PhoneDialInInformationResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000098F5 File Offset: 0x00007AF5
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00009902 File Offset: 0x00007B02
		[DataMember(Name = "defaultRegion", EmitDefaultValue = false)]
		public string DefaultRegion
		{
			get
			{
				return base.GetValue<string>("defaultRegion");
			}
			set
			{
				base.SetValue<string>("defaultRegion", value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00009910 File Offset: 0x00007B10
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000991D File Offset: 0x00007B1D
		[DataMember(Name = "dialInRegion", EmitDefaultValue = false)]
		public ResourceCollection<DialInRegionResource> DialInRegions
		{
			get
			{
				return base.GetValue<ResourceCollection<DialInRegionResource>>("dialInRegion");
			}
			set
			{
				base.SetValue<ResourceCollection<DialInRegionResource>>("dialInRegion", value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000992B File Offset: 0x00007B2B
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00009938 File Offset: 0x00007B38
		[DataMember(Name = "externalDirectoryUri", EmitDefaultValue = false)]
		public string ExternalDirectoryUri
		{
			get
			{
				return base.GetValue<string>("externalDirectoryUri");
			}
			set
			{
				base.SetValue<string>("externalDirectoryUri", value);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00009946 File Offset: 0x00007B46
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00009953 File Offset: 0x00007B53
		[DataMember(Name = "internalDirectoryUri", EmitDefaultValue = false)]
		public string InternalDirectoryUri
		{
			get
			{
				return base.GetValue<string>("internalDirectoryUri");
			}
			set
			{
				base.SetValue<string>("internalDirectoryUri", value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00009961 File Offset: 0x00007B61
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000996E File Offset: 0x00007B6E
		[DataMember(Name = "conferenceId", EmitDefaultValue = false)]
		public string ConferenceId
		{
			get
			{
				return base.GetValue<string>("conferenceId");
			}
			set
			{
				base.SetValue<string>("conferenceId", value);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000997C File Offset: 0x00007B7C
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00009989 File Offset: 0x00007B89
		[DataMember(Name = "isAudioConferenceProviderEnabled", EmitDefaultValue = false)]
		public bool IsAudioConferenceProviderEnabled
		{
			get
			{
				return base.GetValue<bool>("isAudioConferenceProviderEnabled");
			}
			set
			{
				base.SetValue<bool>("isAudioConferenceProviderEnabled", value);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000999C File Offset: 0x00007B9C
		// (set) Token: 0x06000331 RID: 817 RVA: 0x000099A9 File Offset: 0x00007BA9
		[DataMember(Name = "participantPassCode", EmitDefaultValue = false)]
		public string ParticipantPassCode
		{
			get
			{
				return base.GetValue<string>("participantPassCode");
			}
			set
			{
				base.SetValue<string>("participantPassCode", value);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000099B7 File Offset: 0x00007BB7
		// (set) Token: 0x06000333 RID: 819 RVA: 0x000099C4 File Offset: 0x00007BC4
		[DataMember(Name = "tollFreeNumbers", EmitDefaultValue = false)]
		public string[] TollFreeNumbers
		{
			get
			{
				return base.GetValue<string[]>("tollFreeNumbers");
			}
			set
			{
				base.SetValue<string[]>("tollFreeNumbers", value);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000099D2 File Offset: 0x00007BD2
		// (set) Token: 0x06000335 RID: 821 RVA: 0x000099DF File Offset: 0x00007BDF
		[DataMember(Name = "tollNumber", EmitDefaultValue = false)]
		public string TollNumber
		{
			get
			{
				return base.GetValue<string>("tollNumber");
			}
			set
			{
				base.SetValue<string>("tollNumber", value);
			}
		}

		// Token: 0x040001F8 RID: 504
		public const string Token = "phoneDialInInformation";

		// Token: 0x02000071 RID: 113
		internal static class PropertyNames
		{
			// Token: 0x040001F9 RID: 505
			public const string DefaultRegion = "defaultRegion";

			// Token: 0x040001FA RID: 506
			public const string DialInRegion = "dialInRegion";

			// Token: 0x040001FB RID: 507
			public const string ExternalDirectoryUri = "externalDirectoryUri";

			// Token: 0x040001FC RID: 508
			public const string InternalDirectoryUri = "internalDirectoryUri";

			// Token: 0x040001FD RID: 509
			public const string ConferenceId = "conferenceId";

			// Token: 0x040001FE RID: 510
			public const string IsAudioConferenceProviderEnabled = "isAudioConferenceProviderEnabled";

			// Token: 0x040001FF RID: 511
			public const string ParticipantPassCode = "participantPassCode";

			// Token: 0x04000200 RID: 512
			public const string TollFreeNumbers = "tollFreeNumbers";

			// Token: 0x04000201 RID: 513
			public const string TollNumber = "tollNumber";
		}
	}
}
