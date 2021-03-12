using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000301 RID: 769
	internal class VirtualNumberManager : ActivityManager
	{
		// Token: 0x06001769 RID: 5993 RVA: 0x00063897 File Offset: 0x00061A97
		internal VirtualNumberManager(ActivityManager manager, VirtualNumberManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000638A1 File Offset: 0x00061AA1
		internal string PrepareForVoiceMail(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "VirtualNumberManager::PrepareForVoiceMail()", new object[0]);
			return null;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000638BC File Offset: 0x00061ABC
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			base.Start(vo, refInfo);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_VirtualNumberCall, null, new object[]
			{
				vo.CallId,
				vo.CurrentCallContext.CallerId.ToDial,
				vo.CurrentCallContext.CalleeInfo.ToString()
			});
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00063919 File Offset: 0x00061B19
		internal override void CheckAuthorization(UMSubscriber u)
		{
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0006391C File Offset: 0x00061B1C
		internal string CheckIfCallFromBlockedNumber(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "VirtualNumberManager: CheckIfCallFromBlockedNumber() ", new object[0]);
			UMSubscriber umsubscriber = (UMSubscriber)vo.CurrentCallContext.CalleeInfo;
			if (umsubscriber.IsBlockedNumber(vo.CurrentCallContext.CallerId))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "VirtualNumberManager: CheckIfCallFromBlockedNumber() : Call Not Blocked", new object[0]);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_VirtualNumberCallBlocked, null, new object[]
				{
					vo.CallId,
					vo.CurrentCallContext.CallerId.ToDial,
					vo.CurrentCallContext.CalleeInfo.ToString()
				});
				return "blockedCall";
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "VirtualNumberManager: CheckIfCallFromBlockedNumber() : Call Blocked ", new object[0]);
			return null;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000639DE File Offset: 0x00061BDE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VirtualNumberManager>(this);
		}

		// Token: 0x02000302 RID: 770
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x0600176F RID: 5999 RVA: 0x000639E6 File Offset: 0x00061BE6
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06001770 RID: 6000 RVA: 0x000639EF File Offset: 0x00061BEF
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "Constructing VirtualNumberManager.", new object[0]);
				return new VirtualNumberManager(manager, this);
			}

			// Token: 0x06001771 RID: 6001 RVA: 0x00063A0E File Offset: 0x00061C0E
			internal override void Load(XmlNode rootNode)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "Loading a new VirtualNumberManager.", new object[0]);
				base.Load(rootNode);
			}
		}
	}
}
