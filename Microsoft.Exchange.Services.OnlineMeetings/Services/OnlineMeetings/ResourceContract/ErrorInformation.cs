using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000088 RID: 136
	[DataContract(Name = "reason")]
	internal class ErrorInformation
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000A009 File Offset: 0x00008209
		static ErrorInformation()
		{
			ErrorInformation.PopulateCommonCodes();
			ErrorInformation.PopulateApplicationCodes();
			ErrorInformation.PopulateMeCodes();
			ErrorInformation.PopulatePeopleCodes();
			ErrorInformation.PopulateOnlineMeetingCodes();
			ErrorInformation.PopulateCommunicationsCodes();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000A033 File Offset: 0x00008233
		public ErrorInformation()
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000A03B File Offset: 0x0000823B
		public ErrorInformation(ErrorSubcode errorSubcode)
		{
			this.Subcode = errorSubcode;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000A04C File Offset: 0x0000824C
		public ErrorInformation(ErrorSubcode errorSubcode, IDictionary<string, string> errorProperties)
		{
			this.Subcode = errorSubcode;
			if (errorProperties != null && errorProperties.Keys.Count > 0)
			{
				this.errorProperties = new Collection<ErrorInformation.Property>();
				foreach (string text in errorProperties.Keys)
				{
					this.errorProperties.Add(new ErrorInformation.Property(text, errorProperties[text]));
				}
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000A0D4 File Offset: 0x000082D4
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000A0DC File Offset: 0x000082DC
		[DataMember(Name = "Links", EmitDefaultValue = false)]
		public Collection<Link> Links { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000A0E5 File Offset: 0x000082E5
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000A0ED File Offset: 0x000082ED
		[IgnoreDataMember]
		public ErrorCode Code { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A0F6 File Offset: 0x000082F6
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000A0FE File Offset: 0x000082FE
		[IgnoreDataMember]
		public ErrorSubcode Subcode { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000A108 File Offset: 0x00008308
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000A13F File Offset: 0x0000833F
		[DataMember(Name = "Message", EmitDefaultValue = false)]
		public string Message
		{
			get
			{
				string result = string.Empty;
				if (ErrorInformation.MessageMap.ContainsKey(this.Subcode))
				{
					result = ErrorInformation.MessageMap[this.Subcode];
				}
				return result;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000A146 File Offset: 0x00008346
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000A14E File Offset: 0x0000834E
		[DataMember(Name = "Property", EmitDefaultValue = false)]
		public Collection<ErrorInformation.Property> ErrorProperties
		{
			get
			{
				return this.errorProperties;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000A155 File Offset: 0x00008355
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000A167 File Offset: 0x00008367
		[DataMember(Name = "Code", EmitDefaultValue = false)]
		private string ErrorCodeJson
		{
			get
			{
				return this.Code.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000A16E File Offset: 0x0000836E
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000A180 File Offset: 0x00008380
		[DataMember(Name = "Subcode", EmitDefaultValue = false)]
		private string ErrorSubcodeJson
		{
			get
			{
				return this.Subcode.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000A188 File Offset: 0x00008388
		private static void PopulateCommonCodes()
		{
			ErrorInformation.MessageMap.Add(ErrorSubcode.ResourceNotFound, "The requested resource could not be found.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.Forbidden, "The requested operation is not allowed.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.BadRequest, "Bad Request.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.ServiceFailure, "There was a failure in completing the operation.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.MethodNotAllowed, "The method to support the given request does not exist.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvalidOperation, "The requested operation is not valid in the current context.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.TooManyRequests, "There are too many outstanding requests. Retry again later.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.Conflict, "There was a conflict that prevents the operation to be completed.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.RequestTooLarge, "The request size was too large to perform the operation.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvalidResourceKey, "The resource key supplied is invalid.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.ResourceExists, "The resource already exists.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.ResourceTerminating, "The resources is being terminated.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvalidResourceState, "The resource state is invalid.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvalidRequestBody, "The request body is invalid.");
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000A27B File Offset: 0x0000847B
		private static void PopulateApplicationCodes()
		{
			ErrorInformation.MessageMap.Add(ErrorSubcode.ApplicationNotFound, "The application was not found.");
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A291 File Offset: 0x00008491
		private static void PopulateOnlineMeetingCodes()
		{
			ErrorInformation.MessageMap.Add(ErrorSubcode.OnlineMeetingNotFound, "The online meeting was not found.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.OnlineMeetingExists, "The online meeting already exists.");
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A2BB File Offset: 0x000084BB
		private static void PopulateMeCodes()
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A2BD File Offset: 0x000084BD
		private static void PopulatePeopleCodes()
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A2C0 File Offset: 0x000084C0
		private static void PopulateCommunicationsCodes()
		{
			ErrorInformation.MessageMap.Add(ErrorSubcode.ConversationNotFound, "The conversation was not found.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvitationNotFound, "The invitation was not found.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallNotFound, "The call was not found.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.SessionNotFound, "The session was not found.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.ConversationOperationFailed, "The conversation operation failed.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvalidInvitationType, "The invitation type given is invalid.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.SessionContextNotChangable, "The session context cannot be changed once created.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.PendingSessionRenegotiation, "Theere is already a pending negotiation in progress.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallNotAnswered, "The call was not answered.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallCancelled, "The call was cancelled.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallDeclined, "The call was declined.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallFailed, "The call failed to connect.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallTransfered, "The call was transferred.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallReplaced, "The call was replaced.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.InvalidSDP, "The SDP supplied was invalid.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.MediaTypeNotSupported, "The media type is not supported.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.OfferAnswerFailure, "There was a failure in offer/answer negotiation.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.AudioUnavailable, "The audio is not available for this conference.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.UserNotEnabledForOutsideVoice, "The user is not enabled for outside voice.");
			ErrorInformation.MessageMap.Add(ErrorSubcode.CallTransferFailed, "The call transfer failed.");
		}

		// Token: 0x04000276 RID: 630
		public const string Token = "Error";

		// Token: 0x04000277 RID: 631
		private static readonly Dictionary<ErrorSubcode, string> MessageMap = new Dictionary<ErrorSubcode, string>();

		// Token: 0x04000278 RID: 632
		private readonly Collection<ErrorInformation.Property> errorProperties;

		// Token: 0x02000089 RID: 137
		internal class Property
		{
			// Token: 0x060003A4 RID: 932 RVA: 0x0000A45D File Offset: 0x0000865D
			public Property()
			{
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x0000A465 File Offset: 0x00008665
			public Property(string name, string val)
			{
				this.Name = name;
				this.Val = val;
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000A47B File Offset: 0x0000867B
			// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000A483 File Offset: 0x00008683
			[DataMember(Name = "Name", EmitDefaultValue = false)]
			public string Name { get; set; }

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000A48C File Offset: 0x0000868C
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000A494 File Offset: 0x00008694
			[DataMember(Name = "Value", EmitDefaultValue = false)]
			public string Val { get; set; }
		}
	}
}
