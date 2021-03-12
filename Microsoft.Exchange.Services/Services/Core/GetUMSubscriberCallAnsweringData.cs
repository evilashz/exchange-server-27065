using System;
using System.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000339 RID: 825
	internal sealed class GetUMSubscriberCallAnsweringData : SingleStepServiceCommand<GetUMSubscriberCallAnsweringDataRequest, GetUMSubscriberCallAnsweringDataResponseMessage>
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x0007ABB8 File Offset: 0x00078DB8
		public GetUMSubscriberCallAnsweringData(CallContext callContext, GetUMSubscriberCallAnsweringDataRequest request) : base(callContext, request)
		{
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0007ABC2 File Offset: 0x00078DC2
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUMSubscriberCallAnsweringDataResponseMessage(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0007ABEC File Offset: 0x00078DEC
		internal override ServiceResult<GetUMSubscriberCallAnsweringDataResponseMessage> Execute()
		{
			IRecipientSession adrecipientSession = base.CallContext.ADRecipientSessionContext.GetADRecipientSession();
			ADUser user = adrecipientSession.FindADUserByObjectId(base.CallContext.AccessingADUser.ObjectId);
			TimeSpan timeout;
			try
			{
				timeout = XmlConvert.ToTimeSpan(base.Request.Timeout);
			}
			catch (FormatException innerException)
			{
				throw new InvalidValueForPropertyException((CoreResources.IDs)3078968203U, innerException);
			}
			UMSubscriberCallAnsweringData umsubscriberCallAnsweringData;
			using (XSOUMUserMailboxAccessor xsoumuserMailboxAccessor = new XSOUMUserMailboxAccessor(user, base.MailboxIdentityMailboxSession))
			{
				umsubscriberCallAnsweringData = xsoumuserMailboxAccessor.GetUMSubscriberCallAnsweringData(timeout);
			}
			GetUMSubscriberCallAnsweringDataResponseMessage getUMSubscriberCallAnsweringDataResponseMessage = new GetUMSubscriberCallAnsweringDataResponseMessage();
			if (umsubscriberCallAnsweringData != null)
			{
				getUMSubscriberCallAnsweringDataResponseMessage.IsOOF = umsubscriberCallAnsweringData.IsOOF;
				getUMSubscriberCallAnsweringDataResponseMessage.TaskTimedOut = umsubscriberCallAnsweringData.TaskTimedOut;
				getUMSubscriberCallAnsweringDataResponseMessage.IsMailboxQuotaExceeded = umsubscriberCallAnsweringData.IsMailboxQuotaExceeded;
				getUMSubscriberCallAnsweringDataResponseMessage.IsTranscriptionEnabledInMailboxConfig = umsubscriberCallAnsweringData.IsTranscriptionEnabledInMailboxConfig;
				getUMSubscriberCallAnsweringDataResponseMessage.Greeting = umsubscriberCallAnsweringData.RawGreeting;
				getUMSubscriberCallAnsweringDataResponseMessage.GreetingName = ((umsubscriberCallAnsweringData.Greeting != null) ? umsubscriberCallAnsweringData.Greeting.ExtraInfo : null);
			}
			return new ServiceResult<GetUMSubscriberCallAnsweringDataResponseMessage>(getUMSubscriberCallAnsweringDataResponseMessage);
		}
	}
}
