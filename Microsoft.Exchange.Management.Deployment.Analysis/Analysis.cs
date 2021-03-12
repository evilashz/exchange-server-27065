using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000007 RID: 7
	public abstract class Analysis
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002E2C File Offset: 0x0000102C
		protected Analysis(Func<AnalysisMember, bool> immediateEvaluationFilter, Func<AnalysisMember, bool> conclusionsFilter, AnalysisThreading threadMode)
		{
			if (immediateEvaluationFilter == null)
			{
				throw new ArgumentNullException("immediateEvaluationFilter");
			}
			if (conclusionsFilter == null)
			{
				throw new ArgumentNullException("conclusionsFilter");
			}
			this.immediateEvaluationFilter = immediateEvaluationFilter;
			this.conclusionsFilter = conclusionsFilter;
			this.currentState = new Optimistic<Analysis.AnalysisState>(new Analysis.AnalysisState(this), new Resolver<Analysis.AnalysisState>(Analysis.AnalysisState.Resolve));
			this.currentProgress = new Optimistic<AnalysisProgress>(new AnalysisProgress(0, 0), new Resolver<AnalysisProgress>(AnalysisProgress.Resolve));
			this.threadMode = threadMode;
			this.rootAnalysisMember = new RootAnalysisMember(this);
			this.completedManualResetEvent = new ManualResetEvent(false);
			this.startAnalysisLock = new object();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00002ED0 File Offset: 0x000010D0
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00002F08 File Offset: 0x00001108
		public event EventHandler<ProgressUpdateEventArgs> ProgressUpdated;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F40 File Offset: 0x00001140
		public AnalysisStatus Status
		{
			get
			{
				Analysis.AnalysisState safeValue = this.currentState.SafeValue;
				if (safeValue.IsCanceled)
				{
					return AnalysisStatus.Canceled;
				}
				if (safeValue.IsCompleted)
				{
					return AnalysisStatus.Completed;
				}
				if (safeValue.IsStarted)
				{
					return AnalysisStatus.Running;
				}
				return AnalysisStatus.Ready;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F78 File Offset: 0x00001178
		public AnalysisProgress Progress
		{
			get
			{
				return this.currentProgress.SafeValue;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F85 File Offset: 0x00001185
		public AnalysisThreading ThreadMode
		{
			get
			{
				return this.threadMode;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002F8D File Offset: 0x0000118D
		public RootAnalysisMember RootAnalysisMember
		{
			get
			{
				return this.rootAnalysisMember;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F98 File Offset: 0x00001198
		public IEnumerable<AnalysisMember> AnalysisMembers
		{
			get
			{
				Analysis.AnalysisState safeValue = this.currentState.SafeValue;
				Analysis.AnalysisState analysisState = this.currentState.Update(safeValue, safeValue.DiscoverMembers());
				return analysisState.AnalysisMembers;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002FF5 File Offset: 0x000011F5
		public IEnumerable<AnalysisMember> Settings
		{
			get
			{
				return from x in this.AnalysisMembers
				where x.GetType().IsGenericType && x.GetType().GetGenericTypeDefinition() == typeof(Setting<>)
				select x;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000302A File Offset: 0x0000122A
		public IEnumerable<Rule> Rules
		{
			get
			{
				return (from x in this.AnalysisMembers
				where x is Rule
				select x).Cast<Rule>();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000305C File Offset: 0x0000125C
		public ExDateTime StartTime
		{
			get
			{
				Analysis.AnalysisState unsafeValue = this.currentState.UnsafeValue;
				if (unsafeValue.IsStarted)
				{
					return unsafeValue.StartTime;
				}
				Analysis.AnalysisState safeValue = this.currentState.SafeValue;
				return safeValue.StartTime;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003098 File Offset: 0x00001298
		public ExDateTime StopTime
		{
			get
			{
				Analysis.AnalysisState unsafeValue = this.currentState.UnsafeValue;
				if (unsafeValue.IsCompleted)
				{
					return unsafeValue.StopTime;
				}
				Analysis.AnalysisState safeValue = this.currentState.SafeValue;
				return safeValue.StopTime;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030D4 File Offset: 0x000012D4
		public Exception CancellationException
		{
			get
			{
				Analysis.AnalysisState unsafeValue = this.currentState.UnsafeValue;
				if (unsafeValue.IsCanceled)
				{
					return unsafeValue.CancellationException;
				}
				Analysis.AnalysisState safeValue = this.currentState.SafeValue;
				return safeValue.CancellationException;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003190 File Offset: 0x00001390
		public void StartAnalysis()
		{
			Analysis.AnalysisState unsafeValue = this.currentState.UnsafeValue;
			if (unsafeValue.IsStarted || unsafeValue.IsCanceled)
			{
				return;
			}
			lock (this.startAnalysisLock)
			{
				Analysis.AnalysisState safeValue = this.currentState.SafeValue;
				if (safeValue.IsStarted || unsafeValue.IsCanceled)
				{
					return;
				}
				Analysis.AnalysisState updatedValue = safeValue.SetAsStarted();
				this.currentState.Update(safeValue, updatedValue);
			}
			AnalysisProgress progress = this.currentProgress.Update(this.currentProgress.UnsafeValue, new AnalysisProgress((from x in this.AnalysisMembers
			where !(x is RootAnalysisMember)
			select x).Count((AnalysisMember x) => x.IsConclusion), 0));
			try
			{
				this.OnAnalysisStart();
			}
			catch (Exception inner)
			{
				this.Cancel(new CriticalException(null, inner));
			}
			EventHandler<ProgressUpdateEventArgs> progressUpdated = this.ProgressUpdated;
			if (progressUpdated != null)
			{
				progressUpdated(this, new ProgressUpdateEventArgs(progress));
			}
			List<AnalysisMember> source = (from x in this.AnalysisMembers
			where x.IsConclusion
			select x).ToList<AnalysisMember>();
			if (source.Any<AnalysisMember>())
			{
				if (this.threadMode == AnalysisThreading.Single)
				{
					using (IEnumerator<AnalysisMember> enumerator = (from x in source
					orderby (int)x.EvaluationMode
					select x).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AnalysisMember analysisMember = enumerator.Current;
							analysisMember.Start();
						}
						return;
					}
				}
				using (IEnumerator<AnalysisMember> enumerator2 = (from x in this.AnalysisMembers
				where x.EvaluationMode == Evaluate.OnAnalysisStart && this.immediateEvaluationFilter(x)
				select x).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						AnalysisMember member = enumerator2.Current;
						Task.Factory.StartNew(delegate()
						{
							member.Start();
						}, TaskCreationOptions.LongRunning);
					}
				}
				Parallel.ForEach<AnalysisMember>((from x in this.AnalysisMembers
				where x.IsConclusion && (x.EvaluationMode != Evaluate.OnAnalysisStart || !this.immediateEvaluationFilter(x))
				select x).ToList<AnalysisMember>(), delegate(AnalysisMember x)
				{
					x.Start();
				});
				return;
			}
			this.CompleteAnalysis();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003448 File Offset: 0x00001648
		public void WaitUntilComplete()
		{
			this.StartAnalysis();
			this.completedManualResetEvent.WaitOne();
			Analysis.AnalysisState safeValue = this.currentState.SafeValue;
			if (safeValue.IsCanceled)
			{
				throw safeValue.CancellationException;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003482 File Offset: 0x00001682
		public void WaitUntilComplete(TimeSpan timeout)
		{
			this.StartAnalysis();
			this.completedManualResetEvent.WaitOne(timeout);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003497 File Offset: 0x00001697
		public void Cancel()
		{
			this.CancelAnalysis(new CanceledException());
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034A4 File Offset: 0x000016A4
		public void Cancel(string reason)
		{
			this.CancelAnalysis(new CanceledException(reason));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000034B2 File Offset: 0x000016B2
		protected void Cancel(CriticalException exception)
		{
			this.CancelAnalysis(exception);
		}

		// Token: 0x06000073 RID: 115
		protected abstract void OnAnalysisStart();

		// Token: 0x06000074 RID: 116
		protected abstract void OnAnalysisStop();

		// Token: 0x06000075 RID: 117
		protected abstract void OnAnalysisMemberStart(AnalysisMember member);

		// Token: 0x06000076 RID: 118
		protected abstract void OnAnalysisMemberStop(AnalysisMember member);

		// Token: 0x06000077 RID: 119
		protected abstract void OnAnalysisMemberEvaluate(AnalysisMember member, Result result);

		// Token: 0x06000078 RID: 120 RVA: 0x000034BC File Offset: 0x000016BC
		private void CancelAnalysis(Exception exception)
		{
			Analysis.AnalysisState unsafeValue = this.currentState.UnsafeValue;
			if (unsafeValue.IsCanceled)
			{
				return;
			}
			this.currentState.Update(unsafeValue, unsafeValue.SetAsCancled(exception));
			this.completedManualResetEvent.Set();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003500 File Offset: 0x00001700
		private void CompleteAnalysis()
		{
			Analysis.AnalysisState safeValue = this.currentState.SafeValue;
			AnalysisProgress safeValue2 = this.currentProgress.SafeValue;
			if (!safeValue.IsStarted || safeValue2.CompletedConclusions != safeValue2.TotalConclusions)
			{
				return;
			}
			Analysis.AnalysisState updatedValue = safeValue.SetAsCompleted();
			this.currentState.Update(safeValue, updatedValue);
			try
			{
				this.OnAnalysisStop();
			}
			catch (Exception inner)
			{
				this.Cancel(new CriticalException(null, inner));
			}
			this.completedManualResetEvent.Set();
		}

		// Token: 0x04000018 RID: 24
		private readonly Optimistic<Analysis.AnalysisState> currentState;

		// Token: 0x04000019 RID: 25
		private readonly Optimistic<AnalysisProgress> currentProgress;

		// Token: 0x0400001A RID: 26
		private readonly Func<AnalysisMember, bool> immediateEvaluationFilter;

		// Token: 0x0400001B RID: 27
		private readonly Func<AnalysisMember, bool> conclusionsFilter;

		// Token: 0x0400001C RID: 28
		private readonly AnalysisThreading threadMode;

		// Token: 0x0400001D RID: 29
		private readonly RootAnalysisMember rootAnalysisMember;

		// Token: 0x0400001E RID: 30
		private readonly ManualResetEvent completedManualResetEvent;

		// Token: 0x0400001F RID: 31
		private readonly object startAnalysisLock;

		// Token: 0x02000008 RID: 8
		public abstract class AnalysisMemberBase
		{
			// Token: 0x06000083 RID: 131 RVA: 0x00003588 File Offset: 0x00001788
			protected AnalysisMemberBase(Analysis analysis)
			{
				this.analysis = analysis;
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000084 RID: 132 RVA: 0x00003597 File Offset: 0x00001797
			public Analysis Analysis
			{
				get
				{
					return this.analysis;
				}
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000085 RID: 133 RVA: 0x000035A0 File Offset: 0x000017A0
			public string Name
			{
				get
				{
					Analysis.AnalysisState unsafeValue = this.analysis.currentState.UnsafeValue;
					if (unsafeValue.HasDiscoveredAnalysisMembers)
					{
						return unsafeValue.GetName(this);
					}
					Analysis.AnalysisState safeValue = this.analysis.currentState.SafeValue;
					if (safeValue.HasDiscoveredAnalysisMembers)
					{
						return safeValue.GetName(this);
					}
					Analysis.AnalysisState analysisState = this.analysis.currentState.Update(safeValue, safeValue.DiscoverMembers());
					return analysisState.GetName(this);
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000086 RID: 134 RVA: 0x0000360E File Offset: 0x0000180E
			public bool IsAnalysisCanceled
			{
				get
				{
					return this.analysis.currentState.SafeValue.IsCanceled;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000087 RID: 135 RVA: 0x00003625 File Offset: 0x00001825
			protected Func<AnalysisMember, bool> AnalysisConclusionsFilter
			{
				get
				{
					return this.analysis.conclusionsFilter;
				}
			}

			// Token: 0x06000088 RID: 136 RVA: 0x00003632 File Offset: 0x00001832
			protected void CancelAnalysis(CriticalException exception)
			{
				this.analysis.Cancel(exception);
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00003640 File Offset: 0x00001840
			protected virtual void OnStart()
			{
				AnalysisMember analysisMember = this as AnalysisMember;
				if (analysisMember == null || analysisMember is RootAnalysisMember)
				{
					return;
				}
				try
				{
					this.analysis.OnAnalysisMemberStart(analysisMember);
				}
				catch (Exception inner)
				{
					this.analysis.Cancel(new CriticalException(analysisMember, inner));
				}
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00003694 File Offset: 0x00001894
			protected virtual void OnEvaluate(Result result)
			{
				AnalysisMember analysisMember = this as AnalysisMember;
				if (analysisMember == null || analysisMember is RootAnalysisMember)
				{
					return;
				}
				try
				{
					this.analysis.OnAnalysisMemberEvaluate(analysisMember, result);
				}
				catch (Exception inner)
				{
					this.analysis.Cancel(new CriticalException(analysisMember, inner));
				}
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00003730 File Offset: 0x00001930
			protected virtual void OnComplete()
			{
				AnalysisMember analysisMember = this as AnalysisMember;
				if (analysisMember == null || analysisMember is RootAnalysisMember)
				{
					return;
				}
				try
				{
					this.analysis.OnAnalysisMemberStop(analysisMember);
				}
				catch (Exception inner)
				{
					this.analysis.Cancel(new CriticalException(analysisMember, inner));
				}
				bool isAnalysisComplete = false;
				if (analysisMember.IsConclusion)
				{
					AnalysisProgress unsafeValue = this.analysis.currentProgress.UnsafeValue;
					AnalysisProgress updatedProgress = new AnalysisProgress(unsafeValue.TotalConclusions, unsafeValue.CompletedConclusions + 1);
					isAnalysisComplete = (updatedProgress.CompletedConclusions == updatedProgress.TotalConclusions);
					this.analysis.currentProgress.Update(unsafeValue, updatedProgress, delegate(AnalysisProgress original, AnalysisProgress current, AnalysisProgress updated)
					{
						isAnalysisComplete = (updatedProgress.CompletedConclusions == updatedProgress.TotalConclusions);
						return new AnalysisProgress(current.TotalConclusions, current.CompletedConclusions + 1);
					});
					EventHandler<ProgressUpdateEventArgs> progressUpdated = this.analysis.ProgressUpdated;
					if (progressUpdated != null)
					{
						try
						{
							progressUpdated(this.analysis, new ProgressUpdateEventArgs(updatedProgress));
						}
						catch (Exception inner2)
						{
							this.analysis.Cancel(new CriticalException(analysisMember, inner2));
						}
					}
				}
				if (isAnalysisComplete)
				{
					this.analysis.CompleteAnalysis();
				}
			}

			// Token: 0x04000028 RID: 40
			private readonly Analysis analysis;
		}

		// Token: 0x02000009 RID: 9
		private sealed class AnalysisState
		{
			// Token: 0x0600008C RID: 140 RVA: 0x00003880 File Offset: 0x00001A80
			public AnalysisState(Analysis analysis)
			{
				this.analysis = analysis;
				this.hasDiscoveredAnalysisMembers = false;
				this.isStarted = false;
				this.isCompleted = false;
				this.isCanceled = false;
				this.analysisMemberNameMap = null;
				this.startTime = default(ExDateTime);
				this.stopTime = default(ExDateTime);
				this.cancellationException = null;
			}

			// Token: 0x0600008D RID: 141 RVA: 0x000038DC File Offset: 0x00001ADC
			private AnalysisState(Analysis analysis, bool hasDiscoveredAnalysisMembers, bool isStarted, bool isCompleted, bool isCanceled, Dictionary<Analysis.AnalysisMemberBase, string> analysisMemberNameMap, ExDateTime startTime, ExDateTime stopTime, Exception cancellationException)
			{
				this.analysis = analysis;
				this.hasDiscoveredAnalysisMembers = hasDiscoveredAnalysisMembers;
				this.isStarted = isStarted;
				this.isCompleted = isCompleted;
				this.isCanceled = isCanceled;
				this.analysisMemberNameMap = analysisMemberNameMap;
				this.startTime = startTime;
				this.stopTime = stopTime;
				this.cancellationException = cancellationException;
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600008E RID: 142 RVA: 0x00003934 File Offset: 0x00001B34
			public bool HasDiscoveredAnalysisMembers
			{
				get
				{
					return this.hasDiscoveredAnalysisMembers;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600008F RID: 143 RVA: 0x0000393C File Offset: 0x00001B3C
			public bool IsStarted
			{
				get
				{
					return this.isStarted;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00003944 File Offset: 0x00001B44
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000091 RID: 145 RVA: 0x0000394C File Offset: 0x00001B4C
			public bool IsCanceled
			{
				get
				{
					return this.isCanceled;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000092 RID: 146 RVA: 0x00003954 File Offset: 0x00001B54
			public IEnumerable<AnalysisMember> AnalysisMembers
			{
				get
				{
					if (!this.hasDiscoveredAnalysisMembers)
					{
						throw new InvalidOperationException(Strings.CannotGetMembersBeforeDiscovery);
					}
					return this.analysisMemberNameMap.Keys.Cast<AnalysisMember>();
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000093 RID: 147 RVA: 0x0000397E File Offset: 0x00001B7E
			public ExDateTime StartTime
			{
				get
				{
					if (!this.isStarted)
					{
						throw new InvalidOperationException(Strings.CannotGetStartTimeBeforeStart);
					}
					return this.startTime;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000094 RID: 148 RVA: 0x0000399E File Offset: 0x00001B9E
			public ExDateTime StopTime
			{
				get
				{
					if (!this.isCompleted)
					{
						throw new InvalidOperationException(Strings.CannotGetStopTimeBeforeCompletion);
					}
					return this.stopTime;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000095 RID: 149 RVA: 0x000039BE File Offset: 0x00001BBE
			public Exception CancellationException
			{
				get
				{
					if (!this.IsCanceled)
					{
						throw new InvalidOperationException(Strings.CannotGetCancellationExceptionWithoutCancellation);
					}
					return this.cancellationException;
				}
			}

			// Token: 0x06000096 RID: 150 RVA: 0x000039E0 File Offset: 0x00001BE0
			public static Analysis.AnalysisState Resolve(Analysis.AnalysisState originalValue, Analysis.AnalysisState currentValue, Analysis.AnalysisState updatedValue)
			{
				return new Analysis.AnalysisState(updatedValue.analysis, currentValue.hasDiscoveredAnalysisMembers || updatedValue.hasDiscoveredAnalysisMembers, currentValue.isStarted || updatedValue.isStarted, currentValue.isCompleted || updatedValue.isCompleted, currentValue.isCanceled || updatedValue.isCanceled, currentValue.analysisMemberNameMap ?? updatedValue.analysisMemberNameMap, (currentValue.startTime < updatedValue.startTime) ? currentValue.startTime : updatedValue.startTime, (currentValue.stopTime < updatedValue.stopTime) ? currentValue.stopTime : updatedValue.stopTime, currentValue.cancellationException ?? updatedValue.cancellationException);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00003AA0 File Offset: 0x00001CA0
			public Analysis.AnalysisState SetAsStarted()
			{
				if (this.isStarted)
				{
					return this;
				}
				return this.With(default(Optional<bool>), true, default(Optional<bool>), default(Optional<bool>), default(Optional<Dictionary<Analysis.AnalysisMemberBase, string>>), ExDateTime.Now, default(Optional<ExDateTime>), default(Optional<Exception>));
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003B08 File Offset: 0x00001D08
			public Analysis.AnalysisState SetAsCompleted()
			{
				if (this.isCompleted)
				{
					return this;
				}
				return this.With(default(Optional<bool>), default(Optional<bool>), true, default(Optional<bool>), default(Optional<Dictionary<Analysis.AnalysisMemberBase, string>>), default(Optional<ExDateTime>), ExDateTime.Now, default(Optional<Exception>));
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00003B70 File Offset: 0x00001D70
			public Analysis.AnalysisState SetAsCancled(Exception cancellationException)
			{
				if (this.isCanceled)
				{
					return this;
				}
				return this.With(default(Optional<bool>), default(Optional<bool>), default(Optional<bool>), true, default(Optional<Dictionary<Analysis.AnalysisMemberBase, string>>), default(Optional<ExDateTime>), default(Optional<ExDateTime>), cancellationException);
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00003BD1 File Offset: 0x00001DD1
			public string GetName(Analysis.AnalysisMemberBase analysisMember)
			{
				if (!this.hasDiscoveredAnalysisMembers)
				{
					throw new InvalidOperationException(Strings.CannotGetMemberNameBeforeDiscovery);
				}
				return this.analysisMemberNameMap[analysisMember];
			}

			// Token: 0x0600009B RID: 155 RVA: 0x00003E94 File Offset: 0x00002094
			public Analysis.AnalysisState DiscoverMembers()
			{
				Dictionary<Analysis.AnalysisMemberBase, string> value = (from x in this.analysis.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where typeof(AnalysisMember).IsAssignableFrom(x.PropertyType)
				let analysisMember = (Analysis.AnalysisMemberBase)x.GetValue(this.analysis, null)
				where analysisMember != null
				select new
				{
					Member = analysisMember,
					Name = x.Name
				}).ToDictionary(y => y.Member, y => y.Name);
				return this.With(true, default(Optional<bool>), default(Optional<bool>), default(Optional<bool>), value, default(Optional<ExDateTime>), default(Optional<ExDateTime>), default(Optional<Exception>));
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00003FB8 File Offset: 0x000021B8
			private Analysis.AnalysisState With(Optional<bool> hasDiscoveredAnalysisMembers = default(Optional<bool>), Optional<bool> isStarted = default(Optional<bool>), Optional<bool> isCompleted = default(Optional<bool>), Optional<bool> isCanceled = default(Optional<bool>), Optional<Dictionary<Analysis.AnalysisMemberBase, string>> analysisMemberNameMap = default(Optional<Dictionary<Analysis.AnalysisMemberBase, string>>), Optional<ExDateTime> startTime = default(Optional<ExDateTime>), Optional<ExDateTime> stopTime = default(Optional<ExDateTime>), Optional<Exception> cancellationException = default(Optional<Exception>))
			{
				return new Analysis.AnalysisState(this.analysis, hasDiscoveredAnalysisMembers.DefaultTo(this.hasDiscoveredAnalysisMembers), isStarted.DefaultTo(this.isStarted), isCompleted.DefaultTo(this.isCompleted), isCanceled.DefaultTo(this.isCanceled), analysisMemberNameMap.DefaultTo(this.analysisMemberNameMap), startTime.DefaultTo(this.startTime), stopTime.DefaultTo(this.stopTime), cancellationException.DefaultTo(this.cancellationException));
			}

			// Token: 0x04000029 RID: 41
			private readonly Analysis analysis;

			// Token: 0x0400002A RID: 42
			private readonly bool hasDiscoveredAnalysisMembers;

			// Token: 0x0400002B RID: 43
			private readonly bool isStarted;

			// Token: 0x0400002C RID: 44
			private readonly bool isCompleted;

			// Token: 0x0400002D RID: 45
			private readonly bool isCanceled;

			// Token: 0x0400002E RID: 46
			private readonly Dictionary<Analysis.AnalysisMemberBase, string> analysisMemberNameMap;

			// Token: 0x0400002F RID: 47
			private readonly ExDateTime startTime;

			// Token: 0x04000030 RID: 48
			private readonly ExDateTime stopTime;

			// Token: 0x04000031 RID: 49
			private readonly Exception cancellationException;
		}
	}
}
