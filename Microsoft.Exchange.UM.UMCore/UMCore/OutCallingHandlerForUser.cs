using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200019A RID: 410
	internal class OutCallingHandlerForUser : OutCallingHandler
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x000348BB File Offset: 0x00032ABB
		internal OutCallingHandlerForUser(UMSubscriber caller, BaseUMCallSession callSession, UMSipPeer outboundProxy, TypeOfOutboundCall type) : base(callSession, outboundProxy)
		{
			if (caller == null)
			{
				throw new InvalidArgumentException("caller");
			}
			this.callType = type;
			this.caller = caller;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000348E2 File Offset: 0x00032AE2
		protected override string GetCallerName
		{
			get
			{
				return this.caller.ADRecipient.Name;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000348F4 File Offset: 0x00032AF4
		protected override UMDialPlan GetOriginatingDialplan
		{
			get
			{
				return this.caller.DialPlan;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00034904 File Offset: 0x00032B04
		protected override ExEventLog.EventTuple GetPOPEventTuple
		{
			get
			{
				TypeOfOutboundCall typeOfOutboundCall = this.callType;
				if (typeOfOutboundCall == TypeOfOutboundCall.PlayOnPhone)
				{
					return UMEventLogConstants.Tuple_PlayOnPhoneRequest;
				}
				return UMEventLogConstants.Tuple_OutDialingRequest;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00034928 File Offset: 0x00032B28
		protected override ExEventLog.EventTuple GetPOPFailureEventTuple
		{
			get
			{
				TypeOfOutboundCall typeOfOutboundCall = this.callType;
				if (typeOfOutboundCall == TypeOfOutboundCall.PlayOnPhone)
				{
					return UMEventLogConstants.Tuple_OutDialingRulesFailure;
				}
				return UMEventLogConstants.Tuple_FindMeOutDialingRulesFailure;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0003494B File Offset: 0x00032B4B
		protected override DialingPermissionsCheck GetDialingPermissionsChecker
		{
			get
			{
				return new DialingPermissionsCheck(this.caller.ADUser, this.GetOriginatingDialplan);
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00034963 File Offset: 0x00032B63
		internal override void MakeCall(string callerIdToUse, string numberToCall, IList<PlatformSignalingHeader> additionalHeaders)
		{
			if (this.callType == TypeOfOutboundCall.PlayOnPhone)
			{
				this.callSession.PlayOnPhoneSMTPAddress = this.caller.MailAddress;
			}
			base.MakeCall(callerIdToUse, numberToCall, additionalHeaders);
		}

		// Token: 0x04000A14 RID: 2580
		private TypeOfOutboundCall callType;

		// Token: 0x04000A15 RID: 2581
		private UMSubscriber caller;
	}
}
