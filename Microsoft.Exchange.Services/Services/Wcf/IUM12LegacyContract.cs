using System;
using System.ServiceModel;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000064 RID: 100
	[ServiceContract(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	internal interface IUM12LegacyContract
	{
		// Token: 0x0600022F RID: 559
		[OperationContract(Action = "*", ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/IsUMEnabled")]
		[XmlSerializerFormat]
		[return: MessageParameter(Name = "IsUMEnabledResponse")]
		bool IsUMEnabled();

		// Token: 0x06000230 RID: 560
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/GetUMProperties")]
		[return: MessageParameter(Name = "GetUMPropertiesResponse")]
		UMProperties GetUMProperties();

		// Token: 0x06000231 RID: 561
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/SetOofStatus")]
		void SetOofStatus(bool status);

		// Token: 0x06000232 RID: 562
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/SetPlayOnPhoneDialString")]
		[XmlSerializerFormat]
		void SetPlayOnPhoneDialString(string dialString);

		// Token: 0x06000233 RID: 563
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/SetTelephoneAccessFolderEmail")]
		void SetTelephoneAccessFolderEmail(string base64FolderId);

		// Token: 0x06000234 RID: 564
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/SetMissedCallNotificationEnabled")]
		[XmlSerializerFormat]
		void SetMissedCallNotificationEnabled(bool status);

		// Token: 0x06000235 RID: 565
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/ResetPIN")]
		[XmlSerializerFormat]
		void ResetPIN();

		// Token: 0x06000236 RID: 566
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/PlayOnPhoneResponse")]
		[return: MessageParameter(Name = "PlayOnPhoneResponse")]
		string PlayOnPhone([MessageParameter(Name = "entryId")] string base64ObjectId, [MessageParameter(Name = "DialString")] string dialString);

		// Token: 0x06000237 RID: 567
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/GetCallInfo")]
		[return: MessageParameter(Name = "GetCallInfoResponse")]
		UMCallInfo GetCallInfo([MessageParameter(Name = "CallId")] string callId);

		// Token: 0x06000238 RID: 568
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/Disconnect")]
		void Disconnect([MessageParameter(Name = "CallId")] string callId);

		// Token: 0x06000239 RID: 569
		[XmlSerializerFormat]
		[OperationContract(ReplyAction = "http://schemas.microsoft.com/exchange/services/2006/messages/PlayOnPhoneGreetingResponse")]
		[return: MessageParameter(Name = "PlayOnPhoneGreetingResponse")]
		string PlayOnPhoneGreeting([MessageParameter(Name = "GreetingType")] UMGreetingType greetingType, [MessageParameter(Name = "DialString")] string dialString);
	}
}
