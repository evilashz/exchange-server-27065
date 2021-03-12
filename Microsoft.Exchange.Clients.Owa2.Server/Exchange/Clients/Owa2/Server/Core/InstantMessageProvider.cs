using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Clients.Owa.Server.LyncIMLogging;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200013A RID: 314
	internal abstract class InstantMessageProvider : DisposeTrackableBase
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x00024714 File Offset: 0x00022914
		protected InstantMessageProvider(IUserContext userContext, InstantMessageNotifier notifier)
		{
			if (notifier == null)
			{
				throw new ArgumentNullException("payload");
			}
			this.Notifier = notifier;
			this.UserContext = userContext;
			this.IsEarlierSignInSuccessful = true;
			this.ExpandedGroupIds = new HashSet<string>();
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002474A File Offset: 0x0002294A
		internal static bool IsInitialized
		{
			get
			{
				return InstantMessageProvider.isInitialized;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00024751 File Offset: 0x00022951
		internal static object InitializationLock
		{
			get
			{
				return InstantMessageProvider.initializationLock;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00024758 File Offset: 0x00022958
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0002475F File Offset: 0x0002295F
		internal static string OcsServerName { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00024767 File Offset: 0x00022967
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0002476E File Offset: 0x0002296E
		internal static int IMPortNumber { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00024776 File Offset: 0x00022976
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0002477D File Offset: 0x0002297D
		internal static Func<long> GetElapsedMilliseconds { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00024785 File Offset: 0x00022985
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0002478C File Offset: 0x0002298C
		internal static int ActivityBasedPresenceDuration { get; private set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00024794 File Offset: 0x00022994
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0002479B File Offset: 0x0002299B
		internal static bool ArePerfCountersEnabled { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000247A3 File Offset: 0x000229A3
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x000247AA File Offset: 0x000229AA
		internal static IXSOFactory XsoFactory { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000247B2 File Offset: 0x000229B2
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x000247BA File Offset: 0x000229BA
		internal HashSet<string> ExpandedGroupIds { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000247C3 File Offset: 0x000229C3
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x000247CB File Offset: 0x000229CB
		internal virtual bool IsActivityBasedPresenceSet { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000247D4 File Offset: 0x000229D4
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x000247DC File Offset: 0x000229DC
		internal IUserContext UserContext { get; private set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000247E5 File Offset: 0x000229E5
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000247ED File Offset: 0x000229ED
		internal InstantMessageNotifier Notifier { get; private set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000247F6 File Offset: 0x000229F6
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000247FE File Offset: 0x000229FE
		internal bool IsEarlierSignInSuccessful { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000AB3 RID: 2739
		internal abstract bool IsSessionStarted { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000AB4 RID: 2740
		internal abstract bool IsUserUcsMode { get; }

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00024810 File Offset: 0x00022A10
		public static InstantMessageOperationError Initialize()
		{
			if (InstantMessageProvider.isInitialized)
			{
				return InstantMessageOperationError.Success;
			}
			InstantMessageOperationError result;
			try
			{
				if (!Monitor.TryEnter(InstantMessageProvider.initializationLock))
				{
					result = InstantMessageOperationError.InitializationInProgress;
				}
				else if (InstantMessageProvider.IsInitialized)
				{
					result = InstantMessageOperationError.Success;
				}
				else
				{
					Stopwatch stopwatch = Stopwatch.StartNew();
					InstantMessagingConfiguration instantMessagingConfiguration = null;
					try
					{
						bool appSetting = BaseApplication.GetAppSetting<bool>("EnableIMForOwaPremium", false);
						if (appSetting)
						{
							ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Globals.Initialize: OWA2 Instant Messaging integration is disabled by web.config.");
							return InstantMessageOperationError.NotEnabled;
						}
						if (!VdirConfiguration.Instance.InstantMessagingEnabled)
						{
							return InstantMessageOperationError.NotEnabled;
						}
						instantMessagingConfiguration = InstantMessagingConfiguration.GetInstance(VdirConfiguration.Instance);
						if (!instantMessagingConfiguration.CheckConfiguration())
						{
							return InstantMessageOperationError.NotConfigured;
						}
					}
					finally
					{
						stopwatch.Stop();
						OwaApplication.GetRequestDetailsLogger.Set(InstantMessageSignIn.LogMetadata.CheckConfiguration, stopwatch.ElapsedMilliseconds);
					}
					if (InstantMessageProvider.Initialize(instantMessagingConfiguration.ServerName, instantMessagingConfiguration.PortNumber, () => Globals.ApplicationTime, Globals.ActivityBasedPresenceDuration, Globals.ArePerfCountersEnabled))
					{
						ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Globals.Initialize: Success!");
						result = InstantMessageOperationError.Success;
					}
					else
					{
						ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Globals.Initialize: Initialization failed.");
						result = InstantMessageOperationError.NotConfigured;
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(InstantMessageProvider.initializationLock))
				{
					Monitor.Exit(InstantMessageProvider.initializationLock);
				}
			}
			return result;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00024980 File Offset: 0x00022B80
		public static void DisposeProvider()
		{
			InstantMessageOCSProvider.DisposeEndpointManager();
		}

		// Token: 0x06000AB7 RID: 2743
		internal abstract void EstablishSession();

		// Token: 0x06000AB8 RID: 2744
		internal abstract void GetExpandedGroups(MailboxSession session);

		// Token: 0x06000AB9 RID: 2745
		internal abstract int ResetPresence();

		// Token: 0x06000ABA RID: 2746
		internal abstract int SendChatMessage(ChatMessage message);

		// Token: 0x06000ABB RID: 2747
		internal abstract int SendNewChatMessage(ChatMessage message);

		// Token: 0x06000ABC RID: 2748
		internal abstract void AddBuddy(IMailboxSession mailboxsession, InstantMessageBuddy buddy, InstantMessageGroup group);

		// Token: 0x06000ABD RID: 2749
		internal abstract void RemoveBuddy(IMailboxSession mailboxsession, InstantMessageBuddy buddy, StoreId contactId);

		// Token: 0x06000ABE RID: 2750
		internal abstract void EndChatSession(int chatSessionId, bool disconnectSession);

		// Token: 0x06000ABF RID: 2751
		internal abstract void NotifyTyping(int chatSessionId, bool typingCanceled);

		// Token: 0x06000AC0 RID: 2752
		internal abstract int PublishSelfPresence(InstantMessagePresenceType presence);

		// Token: 0x06000AC1 RID: 2753
		protected abstract void CreateGroup(string groupName);

		// Token: 0x06000AC2 RID: 2754
		internal abstract void AcceptBuddy(IMailboxSession mailboxsession, InstantMessageBuddy buddy, InstantMessageGroup group);

		// Token: 0x06000AC3 RID: 2755
		internal abstract void DeclineBuddy(InstantMessageBuddy buddy);

		// Token: 0x06000AC4 RID: 2756
		protected abstract void GetBuddyList();

		// Token: 0x06000AC5 RID: 2757
		internal abstract void AddSubscription(string[] sipUris);

		// Token: 0x06000AC6 RID: 2758
		internal abstract void RemoveSubscription(string sipUri);

		// Token: 0x06000AC7 RID: 2759
		internal abstract void QueryPresence(string[] sipUris);

		// Token: 0x06000AC8 RID: 2760
		internal abstract void PublishResetStatus();

		// Token: 0x06000AC9 RID: 2761
		internal abstract int ParticipateInConversation(int conversationId);

		// Token: 0x06000ACA RID: 2762 RVA: 0x00024988 File Offset: 0x00022B88
		protected static void Log(Enum key, object value)
		{
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			if (getRequestDetailsLogger != null)
			{
				OwaApplication.GetRequestDetailsLogger.Set(key, value);
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000249AB File Offset: 0x00022BAB
		private static bool Initialize(string ocsServerName, int imPortNumber, Func<long> getElapsedMilliseconds, int activityBasedPresenceDuration, bool arePerfCountersEnabled)
		{
			InstantMessageProvider.OcsServerName = ocsServerName;
			InstantMessageProvider.IMPortNumber = imPortNumber;
			InstantMessageProvider.GetElapsedMilliseconds = getElapsedMilliseconds;
			InstantMessageProvider.ActivityBasedPresenceDuration = activityBasedPresenceDuration;
			InstantMessageProvider.ArePerfCountersEnabled = arePerfCountersEnabled;
			InstantMessageProvider.XsoFactory = new XSOFactory();
			InstantMessageProvider.isInitialized = InstantMessageOCSProvider.InitializeProvider();
			return InstantMessageProvider.isInitialized;
		}

		// Token: 0x040006E3 RID: 1763
		private static bool isInitialized = false;

		// Token: 0x040006E4 RID: 1764
		private static object initializationLock = new object();
	}
}
