using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MapiHttpHandler;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SessionContext
	{
		// Token: 0x0600029C RID: 668 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public SessionContext(UserContext userContext, string mailboxIdentifier, SessionContextIdentifier sessionContextIdentifier, TimeSpan idleTimeout, SessionContext.ISessionEnvironment sessionEnvironment = null)
		{
			this.userContext = userContext;
			this.mailboxIdentifier = mailboxIdentifier;
			this.sessionEnvironment = (sessionEnvironment ?? SessionContext.DefaultSessionEnvironment.Instance);
			this.sessionContextIdentifier = sessionContextIdentifier;
			this.contextHandle = null;
			this.creationTime = this.sessionEnvironment.GetUtcNow();
			this.lastActivity = this.sessionEnvironment.GetTickCount();
			this.lastActivityTime = this.creationTime;
			this.idleTimeout = idleTimeout;
			this.rundownReason = null;
			this.rundownTime = ExDateTime.MinValue;
			this.markForRundownReason = null;
			this.TraceState("Creation", null, this.rundownTime, TimeSpan.Zero, this.idleTimeout, this.idleTimeout, this.activityCount);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00010B24 File Offset: 0x0000ED24
		public string MailboxIdentifier
		{
			get
			{
				return this.mailboxIdentifier;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public long Id
		{
			get
			{
				return this.sessionContextIdentifier.Id;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00010B39 File Offset: 0x0000ED39
		public string Cookie
		{
			get
			{
				return this.sessionContextIdentifier.Cookie;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00010B46 File Offset: 0x0000ED46
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00010B4E File Offset: 0x0000ED4E
		public object ContextHandle
		{
			get
			{
				return this.contextHandle;
			}
			set
			{
				this.contextHandle = value;
				if (this.contextHandle == null)
				{
					this.MarkForRundown(SessionRundownReason.ContextHandleCleared);
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00010B66 File Offset: 0x0000ED66
		public SessionContextIdentifier Identifier
		{
			get
			{
				return this.sessionContextIdentifier;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00010B6E File Offset: 0x0000ED6E
		public AsyncOperationTracker AsyncOperationTracker
		{
			get
			{
				return this.asyncOperationTracker;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010B78 File Offset: 0x0000ED78
		public TimeSpan ExpirationInfo
		{
			get
			{
				TimeSpan result;
				bool flag;
				ExDateTime exDateTime;
				this.GetState("get_ExpirationInfo", out result, out flag, out exDateTime);
				return result;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00010B98 File Offset: 0x0000ED98
		public bool IsRundown
		{
			get
			{
				TimeSpan timeSpan;
				bool result;
				ExDateTime exDateTime;
				this.GetState("get_IsRundown", out timeSpan, out result, out exDateTime);
				return result;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public ExDateTime Expires
		{
			get
			{
				TimeSpan t;
				bool flag;
				ExDateTime result;
				this.GetState("get_Expires", out t, out flag, out result);
				if (flag)
				{
					return result;
				}
				return this.sessionEnvironment.GetUtcNow() + t;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public ExDateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x170000BA RID: 186
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		public TimeSpan IdleTimeout
		{
			set
			{
				this.TryUpdateState("set_IdleTimeout", true, null, new TimeSpan?(value), false, false);
				TimeSpan t;
				bool flag;
				ExDateTime exDateTime;
				this.GetState("set_IdleTimeout", out t, out flag, out exDateTime);
				this.sessionEnvironment.WakeUpIdleContextMonitor(this.sessionEnvironment.GetUtcNow() + t);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public SessionRundownReason? RundownReason
		{
			get
			{
				return this.rundownReason;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00010C54 File Offset: 0x0000EE54
		public ExDateTime RundownTime
		{
			get
			{
				return this.rundownTime;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public void MarkForRundown(SessionRundownReason rundownReason)
		{
			if (rundownReason == SessionRundownReason.Expired)
			{
				throw new InvalidOperationException("Cannot explicitly expire a session context.");
			}
			this.TryUpdateState("MarkForRundown", false, new SessionRundownReason?(rundownReason), null, false, false);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00010C98 File Offset: 0x0000EE98
		public bool TryAddReference()
		{
			return this.TryUpdateState("TryAddReference", true, null, null, true, false);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public void ReleaseReference()
		{
			this.TryUpdateState("ReleaseReference", true, null, null, false, true);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		public SessionContextInfo GetSessionContextInfo()
		{
			AsyncOperationInfo[] activeAsyncOperations = null;
			AsyncOperationInfo[] completedAsyncOperations = null;
			AsyncOperationInfo[] failedAsyncOperations = null;
			this.asyncOperationTracker.GetAsyncOperationInfo(out activeAsyncOperations, out completedAsyncOperations, out failedAsyncOperations);
			return new SessionContextInfo(this.creationTime, this.rundownReason, this.rundownTime, this.activityCount, this.lastActivityTime, this.sessionContextIdentifier.Cookie, activeAsyncOperations, completedAsyncOperations, failedAsyncOperations);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00010D50 File Offset: 0x0000EF50
		private void GetState(string methodName, out TimeSpan remainingTime, out bool isRundown, out ExDateTime rundownTime)
		{
			remainingTime = TimeSpan.Zero;
			isRundown = false;
			rundownTime = ExDateTime.MinValue;
			int num;
			TimeSpan timeSpan;
			TimeSpan timeSpan2;
			SessionRundownReason? sessionRundownReason;
			ExDateTime exDateTime;
			TimeSpan idleTime;
			lock (this.sessionContextLock)
			{
				num = this.activityCount;
				timeSpan = this.idleTimeout;
				this.ComputeState(out timeSpan2, out sessionRundownReason, out exDateTime, out idleTime);
			}
			this.TraceState(methodName, sessionRundownReason, exDateTime, idleTime, timeSpan, timeSpan2, num);
			remainingTime = timeSpan2;
			isRundown = (sessionRundownReason != null);
			rundownTime = exDateTime;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00010DEC File Offset: 0x0000EFEC
		private bool TryUpdateState(string methodName, bool setLastActivity, SessionRundownReason? setRundownReason, TimeSpan? newIdleTimeout, bool newActivity, bool releaseActivity)
		{
			bool result = false;
			TimeSpan remainingTime;
			SessionRundownReason? sessionRundownReason;
			ExDateTime exDateTime;
			TimeSpan idleTime;
			int num;
			TimeSpan timeSpan;
			lock (this.sessionContextLock)
			{
				this.ComputeState(out remainingTime, out sessionRundownReason, out exDateTime, out idleTime);
				if (setRundownReason != null)
				{
					this.markForRundownReason = setRundownReason;
					result = true;
				}
				if (sessionRundownReason == null)
				{
					if (setLastActivity)
					{
						this.lastActivity = this.sessionEnvironment.GetTickCount();
						this.lastActivityTime = this.sessionEnvironment.GetUtcNow();
						result = true;
					}
					if (newIdleTimeout != null)
					{
						this.idleTimeout = newIdleTimeout.Value;
						result = true;
					}
					if (newActivity)
					{
						this.activityCount++;
						result = true;
					}
				}
				if (releaseActivity)
				{
					if (this.activityCount == 0)
					{
						throw new InvalidOperationException("Cannot release a reference on a SessionContext object with no references.");
					}
					this.activityCount--;
					result = true;
				}
				num = this.activityCount;
				timeSpan = this.idleTimeout;
				this.ComputeState(out remainingTime, out sessionRundownReason, out exDateTime, out idleTime);
			}
			this.TraceState(methodName, sessionRundownReason, exDateTime, idleTime, timeSpan, remainingTime, num);
			return result;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00010F04 File Offset: 0x0000F104
		private void TraceState(string methodName, SessionRundownReason? rundownReason, ExDateTime rundownTime, TimeSpan idleTime, TimeSpan idleTimeout, TimeSpan remainingTime, int activityCount)
		{
			if (ExTraceGlobals.SessionContextTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (rundownReason != null)
				{
					ExTraceGlobals.SessionContextTracer.TraceInformation(53980, 0L, "SessionContext: [{0}] {1}; ContextHandle={2}, RundownReason={3}, RundownTime={4}", new object[]
					{
						this.Id,
						methodName,
						(this.contextHandle != null) ? this.contextHandle.ToString() : "<null>",
						rundownReason.Value,
						rundownTime
					});
					return;
				}
				ExTraceGlobals.SessionContextTracer.TraceInformation(47520, 0L, "SessionContext: [{0}] {1}; ContextHandle={2}, IdleTime={3}, IdleTimeout={4}, TimeRemaining={5}, ActivityCount={6}", new object[]
				{
					this.Id,
					methodName,
					(this.contextHandle != null) ? this.contextHandle.ToString() : "<null>",
					idleTime.TotalMilliseconds,
					idleTimeout.TotalMilliseconds,
					remainingTime.TotalMilliseconds,
					activityCount
				});
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011014 File Offset: 0x0000F214
		private void ComputeState(out TimeSpan remainingTime, out SessionRundownReason? rundownReason, out ExDateTime rundownTime, out TimeSpan idleTime)
		{
			remainingTime = TimeSpan.Zero;
			rundownReason = null;
			rundownTime = ExDateTime.MinValue;
			idleTime = TimeSpan.FromMilliseconds((double)(this.sessionEnvironment.GetTickCount() - this.lastActivity));
			if (this.rundownReason != null)
			{
				rundownReason = this.rundownReason;
				rundownTime = this.rundownTime;
				idleTime = ((this.rundownReason.Value == SessionRundownReason.Expired) ? this.idleTimeout : TimeSpan.Zero);
				return;
			}
			if (this.markForRundownReason != null)
			{
				remainingTime = TimeSpan.Zero;
				idleTime = TimeSpan.Zero;
				if (this.activityCount == 0)
				{
					this.rundownReason = this.markForRundownReason;
					this.rundownTime = this.sessionEnvironment.GetUtcNow();
					rundownTime = this.rundownTime;
				}
				return;
			}
			if (this.activityCount != 0)
			{
				idleTime = TimeSpan.Zero;
				remainingTime = this.idleTimeout;
				return;
			}
			if (idleTime > this.idleTimeout)
			{
				remainingTime = TimeSpan.Zero;
				this.rundownReason = new SessionRundownReason?(SessionRundownReason.Expired);
				this.rundownTime = this.sessionEnvironment.GetUtcNow();
				rundownReason = this.rundownReason;
				rundownTime = this.rundownTime;
				idleTime = this.idleTimeout;
				return;
			}
			remainingTime = this.idleTimeout - idleTime;
		}

		// Token: 0x0400012B RID: 299
		private readonly object sessionContextLock = new object();

		// Token: 0x0400012C RID: 300
		private readonly UserContext userContext;

		// Token: 0x0400012D RID: 301
		private readonly string mailboxIdentifier;

		// Token: 0x0400012E RID: 302
		private readonly SessionContextIdentifier sessionContextIdentifier;

		// Token: 0x0400012F RID: 303
		private readonly ExDateTime creationTime;

		// Token: 0x04000130 RID: 304
		private readonly AsyncOperationTracker asyncOperationTracker = new AsyncOperationTracker();

		// Token: 0x04000131 RID: 305
		private TimeSpan idleTimeout;

		// Token: 0x04000132 RID: 306
		private object contextHandle;

		// Token: 0x04000133 RID: 307
		private SessionRundownReason? rundownReason;

		// Token: 0x04000134 RID: 308
		private ExDateTime rundownTime;

		// Token: 0x04000135 RID: 309
		private SessionRundownReason? markForRundownReason;

		// Token: 0x04000136 RID: 310
		private int activityCount;

		// Token: 0x04000137 RID: 311
		private int lastActivity;

		// Token: 0x04000138 RID: 312
		private ExDateTime lastActivityTime;

		// Token: 0x04000139 RID: 313
		private SessionContext.ISessionEnvironment sessionEnvironment;

		// Token: 0x0200004B RID: 75
		internal interface ISessionEnvironment
		{
			// Token: 0x060002B4 RID: 692
			int GetTickCount();

			// Token: 0x060002B5 RID: 693
			ExDateTime GetUtcNow();

			// Token: 0x060002B6 RID: 694
			void WakeUpIdleContextMonitor(ExDateTime wakeupTime);
		}

		// Token: 0x0200004C RID: 76
		private sealed class DefaultSessionEnvironment : SessionContext.ISessionEnvironment
		{
			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x00011199 File Offset: 0x0000F399
			public static SessionContext.DefaultSessionEnvironment Instance
			{
				get
				{
					if (SessionContext.DefaultSessionEnvironment.instance == null)
					{
						SessionContext.DefaultSessionEnvironment.instance = new SessionContext.DefaultSessionEnvironment();
					}
					return SessionContext.DefaultSessionEnvironment.instance;
				}
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x000111B1 File Offset: 0x0000F3B1
			int SessionContext.ISessionEnvironment.GetTickCount()
			{
				return Environment.TickCount;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x000111B8 File Offset: 0x0000F3B8
			ExDateTime SessionContext.ISessionEnvironment.GetUtcNow()
			{
				return ExDateTime.UtcNow;
			}

			// Token: 0x060002BA RID: 698 RVA: 0x000111BF File Offset: 0x0000F3BF
			void SessionContext.ISessionEnvironment.WakeUpIdleContextMonitor(ExDateTime wakeupTime)
			{
				SessionContextManager.WakeupIdleContextMonitor(wakeupTime);
			}

			// Token: 0x0400013A RID: 314
			private static SessionContext.DefaultSessionEnvironment instance;
		}
	}
}
