using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000089 RID: 137
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WatsonReporter
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x00015882 File Offset: 0x00013A82
		internal WatsonReporter()
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000158A8 File Offset: 0x00013AA8
		public void ReportWatson(SyncLogSession syncLogSession, string message, Exception exception)
		{
			string text = null;
			this.GetTransportSyncWatsonException(exception, message, out text);
			int hashCode = text.GetHashCode();
			bool flag = true;
			lock (this.syncRoot)
			{
				if (!this.watsonInstances.ContainsKey(hashCode))
				{
					this.watsonInstances.Add(hashCode, new WatsonReporter.WatsonCallstack(message, text, () => this.GetCurrentTime()));
				}
				else if (this.watsonInstances[hashCode].FirstSeenTime + WatsonReporter.ThrottlingPeriod < this.GetCurrentTime())
				{
					this.watsonInstances[hashCode].Reset(syncLogSession);
				}
				else
				{
					this.watsonInstances[hashCode].IncrementCount();
					if (this.watsonInstances[hashCode].CountSinceFirstSeen > WatsonReporter.MaxWatsonsPerCallstackPerThrottlingPeriod)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				syncLogSession.LogError((TSLID)229UL, "ReportException: {0} with hash {1} and inner exception {2}.", new object[]
				{
					message,
					hashCode,
					exception
				});
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000159D4 File Offset: 0x00013BD4
		protected virtual void SendNonTerminatingWatsonReport(ReportTransportSyncWatsonException reportException, string extraData)
		{
			ExWatson.SendReport(reportException, ReportOptions.None, extraData);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000159DE File Offset: 0x00013BDE
		protected virtual ExDateTime GetCurrentTime()
		{
			return ExDateTime.UtcNow;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000159E8 File Offset: 0x00013BE8
		private ReportTransportSyncWatsonException GetTransportSyncWatsonException(Exception exception, string message, out string callstack)
		{
			string text = null;
			string stackTrace = null;
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				if (ex.StackTrace != null)
				{
					text = ex.StackTrace;
					break;
				}
			}
			if (text == null)
			{
				stackTrace = (text = Environment.StackTrace);
			}
			callstack = text;
			return new ReportTransportSyncWatsonException(message, exception, stackTrace);
		}

		// Token: 0x040001E3 RID: 483
		internal static readonly TimeSpan ThrottlingPeriod = TimeSpan.FromHours(1.0);

		// Token: 0x040001E4 RID: 484
		internal static readonly int MaxWatsonsPerCallstackPerThrottlingPeriod = 2;

		// Token: 0x040001E5 RID: 485
		private object syncRoot = new object();

		// Token: 0x040001E6 RID: 486
		private Dictionary<int, WatsonReporter.WatsonCallstack> watsonInstances = new Dictionary<int, WatsonReporter.WatsonCallstack>();

		// Token: 0x0200008A RID: 138
		private class WatsonCallstack
		{
			// Token: 0x060003C7 RID: 967 RVA: 0x00015A49 File Offset: 0x00013C49
			internal WatsonCallstack(string message, string callStack, Func<ExDateTime> getCurrentTime)
			{
				this.Message = message;
				this.CallStack = callStack;
				this.countSinceFirstSeen = 1;
				this.getCurrentTime = getCurrentTime;
				this.firstSeenTime = this.getCurrentTime();
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x00015A7E File Offset: 0x00013C7E
			public ExDateTime FirstSeenTime
			{
				get
				{
					return this.firstSeenTime;
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x060003C9 RID: 969 RVA: 0x00015A86 File Offset: 0x00013C86
			public int CountSinceFirstSeen
			{
				get
				{
					return this.countSinceFirstSeen;
				}
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00015A90 File Offset: 0x00013C90
			internal void Reset(SyncLogSession synclogSession)
			{
				SyncUtilities.ThrowIfArgumentNull("synclogSession", synclogSession);
				if (this.CountSinceFirstSeen > WatsonReporter.MaxWatsonsPerCallstackPerThrottlingPeriod)
				{
					synclogSession.LogError((TSLID)230UL, "Watson: {0} with callstack {1} skipped {2} times in last throttling period {3} for being overactive.", new object[]
					{
						this.Message,
						this.CallStack,
						this.CountSinceFirstSeen - WatsonReporter.MaxWatsonsPerCallstackPerThrottlingPeriod,
						WatsonReporter.ThrottlingPeriod
					});
				}
				this.countSinceFirstSeen = 1;
				this.firstSeenTime = this.getCurrentTime();
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00015B1E File Offset: 0x00013D1E
			internal void IncrementCount()
			{
				this.countSinceFirstSeen++;
			}

			// Token: 0x040001E7 RID: 487
			private readonly string CallStack;

			// Token: 0x040001E8 RID: 488
			private readonly string Message;

			// Token: 0x040001E9 RID: 489
			private readonly Func<ExDateTime> getCurrentTime;

			// Token: 0x040001EA RID: 490
			private ExDateTime firstSeenTime;

			// Token: 0x040001EB RID: 491
			private int countSinceFirstSeen;
		}
	}
}
