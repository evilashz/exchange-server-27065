using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Activity
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005708 File Offset: 0x00003908
		private Activity(long traceId)
		{
			this.traceId = traceId;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000572D File Offset: 0x0000392D
		public static Activity Current
		{
			get
			{
				return Activity.currentActivity;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005734 File Offset: 0x00003934
		public static bool IsForeground
		{
			get
			{
				return Activity.foregroundActivity != null && Activity.foregroundActivity.Value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000574E File Offset: 0x0000394E
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005755 File Offset: 0x00003955
		public static bool AllowImplicit
		{
			get
			{
				return Activity.allowImplicit;
			}
			set
			{
				Activity.allowImplicit = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005760 File Offset: 0x00003960
		public static long TraceId
		{
			get
			{
				Activity activity = Activity.Current;
				if (activity == null)
				{
					return 0L;
				}
				return activity.traceId;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000577F File Offset: 0x0000397F
		public IStandardBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005787 File Offset: 0x00003987
		public ProtocolLogSession ProtocolLogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000578F File Offset: 0x0000398F
		private bool IsCurrent
		{
			get
			{
				return Activity.currentActivity == this;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005799 File Offset: 0x00003999
		public static Activity Create(long activityId)
		{
			return new Activity(activityId);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000057A4 File Offset: 0x000039A4
		public void RegisterWatsonReportAction(WatsonReportAction reportAction)
		{
			lock (this.watsonReportActions)
			{
				this.watsonReportActions.Add(reportAction);
			}
			this.InternalRegisterWatsonReportAction(reportAction);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000057F4 File Offset: 0x000039F4
		public void UnregisterWatsonReportAction(WatsonReportAction reportAction)
		{
			lock (this.watsonReportActions)
			{
				this.watsonReportActions.Remove(reportAction);
			}
			this.InternalUnregisterWatsonReportAction(reportAction);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005844 File Offset: 0x00003A44
		public void RegisterBudget(IStandardBudget budget)
		{
			this.budget = budget;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000584D File Offset: 0x00003A4D
		private void OnResume()
		{
			if (this.logSession != null)
			{
				this.logSession.OnClientActivityResume();
			}
			this.RegisterWatsonReportActionsForCurrentThread();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005868 File Offset: 0x00003A68
		private void OnPause()
		{
			if (this.logSession != null)
			{
				this.logSession.OnClientActivityPause();
			}
			this.UnregisterWatsonReportActionsForCurrentThread();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005883 File Offset: 0x00003A83
		private void InternalRegisterWatsonReportAction(WatsonReportAction reportAction)
		{
			if (this.IsCurrent)
			{
				ExWatson.RegisterReportAction(reportAction, WatsonActionScope.Thread);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005894 File Offset: 0x00003A94
		private void InternalUnregisterWatsonReportAction(WatsonReportAction reportAction)
		{
			if (this.IsCurrent)
			{
				ExWatson.UnregisterReportAction(reportAction, WatsonActionScope.Thread);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000058A8 File Offset: 0x00003AA8
		private void RegisterWatsonReportActionsForCurrentThread()
		{
			WatsonReportAction[] array;
			lock (this.watsonReportActions)
			{
				array = this.watsonReportActions.ToArray<WatsonReportAction>();
			}
			foreach (WatsonReportAction reportAction in array)
			{
				this.InternalRegisterWatsonReportAction(reportAction);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005914 File Offset: 0x00003B14
		private void UnregisterWatsonReportActionsForCurrentThread()
		{
			WatsonReportAction[] array;
			lock (this.watsonReportActions)
			{
				array = this.watsonReportActions.ToArray<WatsonReportAction>();
			}
			foreach (WatsonReportAction reportAction in array)
			{
				this.InternalUnregisterWatsonReportAction(reportAction);
			}
		}

		// Token: 0x0400012D RID: 301
		private static bool allowImplicit = false;

		// Token: 0x0400012E RID: 302
		[ThreadStatic]
		private static Activity currentActivity;

		// Token: 0x0400012F RID: 303
		[ThreadStatic]
		private static bool? foregroundActivity;

		// Token: 0x04000130 RID: 304
		private readonly ICollection<WatsonReportAction> watsonReportActions = new List<WatsonReportAction>();

		// Token: 0x04000131 RID: 305
		private readonly ProtocolLogSession logSession = ProtocolLog.CreateNewSession();

		// Token: 0x04000132 RID: 306
		private readonly long traceId;

		// Token: 0x04000133 RID: 307
		private IStandardBudget budget;

		// Token: 0x02000025 RID: 37
		internal sealed class Guard : BaseObject
		{
			// Token: 0x06000178 RID: 376 RVA: 0x00005988 File Offset: 0x00003B88
			public Guard()
			{
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00005996 File Offset: 0x00003B96
			public void AssociateWithCurrentThread(Activity activity, bool foreground)
			{
				if (this.activity != null)
				{
					throw new InvalidOperationException("Activity.Guard should not be reused.");
				}
				if (activity == null)
				{
					return;
				}
				this.activity = activity;
				Activity.currentActivity = activity;
				Activity.foregroundActivity = new bool?(foreground);
				activity.OnResume();
			}

			// Token: 0x0600017A RID: 378 RVA: 0x000059CD File Offset: 0x00003BCD
			protected override void InternalDispose()
			{
				if (this.activity != null)
				{
					this.activity.OnPause();
					Activity.currentActivity = null;
					Activity.foregroundActivity = null;
				}
				base.InternalDispose();
			}

			// Token: 0x0600017B RID: 379 RVA: 0x000059F9 File Offset: 0x00003BF9
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<Activity.Guard>(this);
			}

			// Token: 0x04000134 RID: 308
			private Activity activity;
		}
	}
}
