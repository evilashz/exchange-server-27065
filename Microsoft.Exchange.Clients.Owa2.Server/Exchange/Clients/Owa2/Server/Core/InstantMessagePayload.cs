using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000142 RID: 322
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessagePayload : NotificationPayloadBase
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0002C72C File Offset: 0x0002A92C
		public InstantMessagePayload()
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002C734 File Offset: 0x0002A934
		public InstantMessagePayload(InstantMessagePayloadType payloadType)
		{
			this.PayloadType = payloadType;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002C743 File Offset: 0x0002A943
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x0002C74B File Offset: 0x0002A94B
		[IgnoreDataMember]
		public InstantMessagePayloadType PayloadType { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002C754 File Offset: 0x0002A954
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x0002C766 File Offset: 0x0002A966
		[DataMember(Name = "PayloadType")]
		public string PayloadTypeString
		{
			get
			{
				return this.PayloadType.ToString();
			}
			set
			{
				this.PayloadType = InstantMessageUtilities.ParseEnumValue<InstantMessagePayloadType>(value, InstantMessagePayloadType.None);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002C775 File Offset: 0x0002A975
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0002C77D File Offset: 0x0002A97D
		[DataMember(EmitDefaultValue = false)]
		public int? ChatSessionId { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002C786 File Offset: 0x0002A986
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0002C78E File Offset: 0x0002A98E
		[DataMember(EmitDefaultValue = false)]
		public string SipUri { get; set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002C797 File Offset: 0x0002A997
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0002C79F File Offset: 0x0002A99F
		[DataMember(EmitDefaultValue = false)]
		public string DisplayName { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002C7A8 File Offset: 0x0002A9A8
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0002C7B0 File Offset: 0x0002A9B0
		[IgnoreDataMember]
		public InstantMessagePresenceType NewUserPresence { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0002C7B9 File Offset: 0x0002A9B9
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0002C7D5 File Offset: 0x0002A9D5
		[DataMember(Name = "NewUserPresence", EmitDefaultValue = false)]
		public string NewUserPresenceString
		{
			get
			{
				if (this.NewUserPresence == InstantMessagePresenceType.None)
				{
					return null;
				}
				return this.NewUserPresence.ToString();
			}
			set
			{
				this.NewUserPresence = InstantMessageUtilities.ParseEnumValue<InstantMessagePresenceType>(value, InstantMessagePresenceType.None);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002C7E4 File Offset: 0x0002A9E4
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0002C7EC File Offset: 0x0002A9EC
		[DataMember(EmitDefaultValue = false)]
		public PresenceChange[] UserPresenceChanges { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0002C7F5 File Offset: 0x0002A9F5
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0002C7FD File Offset: 0x0002A9FD
		[DataMember(EmitDefaultValue = false)]
		public string[] DeletedGroupIds { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0002C806 File Offset: 0x0002AA06
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0002C80E File Offset: 0x0002AA0E
		[DataMember(EmitDefaultValue = false)]
		public string[] DeletedBuddySipUrls { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0002C817 File Offset: 0x0002AA17
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0002C81F File Offset: 0x0002AA1F
		[DataMember(EmitDefaultValue = false)]
		public InstantMessageContact[] PendingContacts { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0002C828 File Offset: 0x0002AA28
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0002C830 File Offset: 0x0002AA30
		[DataMember(EmitDefaultValue = false)]
		public InstantMessageGroup[] AddedGroups { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0002C839 File Offset: 0x0002AA39
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x0002C841 File Offset: 0x0002AA41
		[DataMember(EmitDefaultValue = false)]
		public InstantMessageBuddy[] AddedContacts { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0002C84A File Offset: 0x0002AA4A
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x0002C852 File Offset: 0x0002AA52
		[DataMember(EmitDefaultValue = false)]
		public InstantMessageGroup[] RenamedGroups { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0002C85B File Offset: 0x0002AA5B
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0002C863 File Offset: 0x0002AA63
		[DataMember(EmitDefaultValue = false)]
		public string ToastMessage { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0002C86C File Offset: 0x0002AA6C
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0002C874 File Offset: 0x0002AA74
		[DataMember(EmitDefaultValue = false)]
		public bool IsConference { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0002C87D File Offset: 0x0002AA7D
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x0002C885 File Offset: 0x0002AA85
		[DataMember(EmitDefaultValue = false)]
		public string MessageContent { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0002C88E File Offset: 0x0002AA8E
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x0002C896 File Offset: 0x0002AA96
		[DataMember(EmitDefaultValue = false)]
		public string MessageFormat { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0002C89F File Offset: 0x0002AA9F
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x0002C8A7 File Offset: 0x0002AAA7
		[DataMember(EmitDefaultValue = false)]
		public string MessageSubject { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0002C8B0 File Offset: 0x0002AAB0
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		[DataMember(EmitDefaultValue = false)]
		public bool IsNewSession { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0002C8C1 File Offset: 0x0002AAC1
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x0002C8C9 File Offset: 0x0002AAC9
		[DataMember(EmitDefaultValue = false)]
		public bool SignOnStarted { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002C8D2 File Offset: 0x0002AAD2
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0002C8DA File Offset: 0x0002AADA
		[IgnoreDataMember]
		public UserActivityType UserActivity { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002C8E3 File Offset: 0x0002AAE3
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0002C8FF File Offset: 0x0002AAFF
		[DataMember(Name = "UserActivity", EmitDefaultValue = false)]
		public string UserActivityString
		{
			get
			{
				if (this.UserActivity == UserActivityType.None)
				{
					return null;
				}
				return this.UserActivity.ToString();
			}
			set
			{
				this.UserActivity = InstantMessageUtilities.ParseEnumValue<UserActivityType>(value, UserActivityType.None);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002C90E File Offset: 0x0002AB0E
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0002C916 File Offset: 0x0002AB16
		[IgnoreDataMember]
		public InstantMessageServiceError ServiceError { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002C91F File Offset: 0x0002AB1F
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0002C93B File Offset: 0x0002AB3B
		[DataMember(Name = "ServiceError", EmitDefaultValue = false)]
		public string ServiceErrorString
		{
			get
			{
				if (this.ServiceError == InstantMessageServiceError.None)
				{
					return null;
				}
				return this.ServiceError.ToString();
			}
			set
			{
				this.ServiceError = InstantMessageUtilities.ParseEnumValue<InstantMessageServiceError>(value, InstantMessageServiceError.None);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002C94A File Offset: 0x0002AB4A
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0002C952 File Offset: 0x0002AB52
		[DataMember(EmitDefaultValue = false)]
		public string ErrorMessage { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002C95B File Offset: 0x0002AB5B
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0002C963 File Offset: 0x0002AB63
		[DataMember(EmitDefaultValue = false)]
		public bool IsUserInUcsMode { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002C96C File Offset: 0x0002AB6C
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0002C974 File Offset: 0x0002AB74
		[DataMember(EmitDefaultValue = false)]
		public int? ReconnectInterval { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0002C97D File Offset: 0x0002AB7D
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0002C985 File Offset: 0x0002AB85
		[IgnoreDataMember]
		public InstantMessageOperationType FailedOperationType { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0002C98E File Offset: 0x0002AB8E
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0002C9AA File Offset: 0x0002ABAA
		[DataMember(Name = "FailedOperationType", EmitDefaultValue = false)]
		public string FailedOperationTypeString
		{
			get
			{
				if (this.FailedOperationType == InstantMessageOperationType.Unspecified)
				{
					return null;
				}
				return this.FailedOperationType.ToString();
			}
			set
			{
				this.FailedOperationType = InstantMessageUtilities.ParseEnumValue<InstantMessageOperationType>(value, InstantMessageOperationType.Unspecified);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002C9B9 File Offset: 0x0002ABB9
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0002C9C1 File Offset: 0x0002ABC1
		[DataMember(EmitDefaultValue = false)]
		public Persona Persona { get; set; }

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002C9CC File Offset: 0x0002ABCC
		public static InstantMessagePayload ReportError(string errorMessage, InstantMessageOperationType failedOperationType)
		{
			return new InstantMessagePayload(InstantMessagePayloadType.ReportError)
			{
				ErrorMessage = errorMessage,
				FailedOperationType = failedOperationType
			};
		}
	}
}
