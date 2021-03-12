using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002A0 RID: 672
	internal class FindMeSubscriberManager : ActivityManager, IPAAChild, IPAACommonInterface
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x00058FF9 File Offset: 0x000571F9
		internal FindMeSubscriberManager(ActivityManager manager, FindMeSubscriberManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00059003 File Offset: 0x00057203
		internal object CallerRecordedName
		{
			get
			{
				return this.paaManager.GetCallerRecordedName();
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00059010 File Offset: 0x00057210
		internal object CalleeRecordName
		{
			get
			{
				return this.paaManager.GetCalleeRecordedName();
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0005901D File Offset: 0x0005721D
		public void TerminateCall()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : TerminateCall() called ", new object[0]);
			if (!this.isAlreadyDisposed)
			{
				base.DropCall(base.CallSession, DropCallReason.GracefulHangup);
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0005904A File Offset: 0x0005724A
		public void TerminateCallToTryNextNumberTransfer()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : TerminateCallToTryNextNumberTransfer() called ", new object[0]);
			if (this.isAlreadyDisposed)
			{
				this.paaManager.ContinueFindMe();
				return;
			}
			this.paaManager.DisconnectChildCall();
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00059081 File Offset: 0x00057281
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			this.paaManager = (IPAAParent)vo.CurrentCallContext.LinkedManagerPointer;
			this.paaManager.SetPointerToChild(this);
			base.Start(vo, refInfo);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000590AD File Offset: 0x000572AD
		internal string TerminateFindMe(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : TerminateFindMe() decided by user ", new object[0]);
			this.paaManager.TerminateFindMe();
			return null;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000590D1 File Offset: 0x000572D1
		internal string SendDtmf(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : SendDtmf() = sending dummy DTMF ", new object[0]);
			vo.SendDtmf("D", TimeSpan.Zero);
			return "stopEvent";
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x000590FE File Offset: 0x000572FE
		internal override void DropCall(BaseUMCallSession vo, DropCallReason reason)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : DropCall() ", new object[0]);
			this.paaManager.DisconnectChildCall();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00059121 File Offset: 0x00057321
		internal string AcceptFindMe(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : AcceptFindMe() decided by user ", new object[0]);
			this.paaManager.AcceptCall();
			return null;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00059148 File Offset: 0x00057348
		internal override void OnUserHangup(BaseUMCallSession vo, UMCallSessionEventArgs voiceEventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "FindMeSubscriberManager : OnUserHangup() : the call was disconnected by user ", new object[0]);
			IPAAParent ipaaparent = this.paaManager;
			this.isAlreadyDisposed = true;
			base.OnUserHangup(vo, voiceEventArgs);
			ipaaparent.ContinueFindMe();
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00059187 File Offset: 0x00057387
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FindMeSubscriberManager>(this);
		}

		// Token: 0x04000C9E RID: 3230
		private bool isAlreadyDisposed;

		// Token: 0x04000C9F RID: 3231
		private IPAAParent paaManager;

		// Token: 0x020002A1 RID: 673
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x0600147C RID: 5244 RVA: 0x0005918F File Offset: 0x0005738F
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x0600147D RID: 5245 RVA: 0x00059198 File Offset: 0x00057398
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "Constructing FindMeSubscriberManager.", new object[0]);
				return new FindMeSubscriberManager(manager, this);
			}

			// Token: 0x0600147E RID: 5246 RVA: 0x000591B7 File Offset: 0x000573B7
			internal override void Load(XmlNode rootNode)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.FindMeTracer, this, "Loading a new FindMeSubscriberManager.", new object[0]);
				base.Load(rootNode);
			}
		}
	}
}
