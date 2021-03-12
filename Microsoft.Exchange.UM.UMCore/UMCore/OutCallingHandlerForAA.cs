using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000199 RID: 409
	internal class OutCallingHandlerForAA : OutCallingHandler
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x000347E5 File Offset: 0x000329E5
		internal OutCallingHandlerForAA(UMAutoAttendant aa, BaseUMCallSession callSession, UMSipPeer outboundProxy) : base(callSession, outboundProxy)
		{
			if (aa == null)
			{
				throw new InvalidArgumentException("UMAutoAttendant");
			}
			this.aa = aa;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00034804 File Offset: 0x00032A04
		protected override string GetCallerName
		{
			get
			{
				return this.aa.Name;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00034811 File Offset: 0x00032A11
		protected override UMDialPlan GetOriginatingDialplan
		{
			get
			{
				return this.callSession.CurrentCallContext.DialPlan;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00034823 File Offset: 0x00032A23
		protected override ExEventLog.EventTuple GetPOPEventTuple
		{
			get
			{
				return UMEventLogConstants.Tuple_AAPlayOnPhoneRequest;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0003482A File Offset: 0x00032A2A
		protected override ExEventLog.EventTuple GetPOPFailureEventTuple
		{
			get
			{
				return UMEventLogConstants.Tuple_AAOutDialingRulesFailure;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00034831 File Offset: 0x00032A31
		protected override DialingPermissionsCheck GetDialingPermissionsChecker
		{
			get
			{
				return new DialingPermissionsCheck(this.callSession.CurrentCallContext.AutoAttendantInfo, this.callSession.CurrentCallContext.CurrentAutoAttendantSettings, this.GetOriginatingDialplan);
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00034860 File Offset: 0x00032A60
		internal void MakeCall(string numberToCall, IList<PlatformSignalingHeader> additionalHeaders)
		{
			string defaultOutboundCallingLineId = this.GetOriginatingDialplan.DefaultOutboundCallingLineId;
			string callerIdToUse;
			if (!string.IsNullOrEmpty(defaultOutboundCallingLineId))
			{
				callerIdToUse = defaultOutboundCallingLineId;
			}
			else
			{
				if (this.aa.PilotIdentifierList.Count <= 0)
				{
					throw new NoCallerIdToUseException();
				}
				callerIdToUse = this.aa.PilotIdentifierList[0];
			}
			base.MakeCall(callerIdToUse, numberToCall, additionalHeaders);
		}

		// Token: 0x04000A13 RID: 2579
		private UMAutoAttendant aa;
	}
}
