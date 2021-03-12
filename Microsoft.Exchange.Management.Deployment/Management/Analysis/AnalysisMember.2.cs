using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200003C RID: 60
	internal abstract class AnalysisMember<T> : AnalysisMember
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public AnalysisMember(Func<AnalysisMember> parent, ConcurrencyType runAs, Analysis analysis, IEnumerable<Feature> features, Func<Result, IEnumerable<Result<T>>> setFunction) : base(parent, runAs, analysis, features)
		{
			this.setFunction = setFunction;
			this.values = new List<Result<T>>();
			this.producerLock = new object();
			this.rwls = new ReaderWriterLockSlim();
			this.parentResultEnumerator = new Lazy<IEnumerator<Result>>(() => base.Parent.GetResults().GetEnumerator(), true);
			this.producerEnumerator = Enumerable.Empty<Result<T>>().GetEnumerator();
			this.task = null;
			this.taskLock = new object();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00006D72 File Offset: 0x00004F72
		public override Type ValueType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006D7E File Offset: 0x00004F7E
		public Results<T> Results
		{
			get
			{
				return new Results<T>(this, new AnalysisMember<T>.ConsumerEnumerable(this));
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006DB4 File Offset: 0x00004FB4
		public Results<T> RelativeResults(Result relativeTo)
		{
			HashSet<AnalysisMember> ancestors = new HashSet<AnalysisMember>(base.AncestorsAndSelf());
			AnalysisMember commonAncestor = (from x in relativeTo.Source.AncestorsAndSelf()
			where ancestors.Contains(x)
			select x).First<AnalysisMember>();
			Result result = (from x in relativeTo.AncestorsAndSelf()
			where x.Source == commonAncestor
			select x).First<Result>();
			return result.DescendantsOfType<T>(this);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006E30 File Offset: 0x00005030
		public override void Start()
		{
			if (base.RunAs == ConcurrencyType.Synchronous)
			{
				while (this.ProduceNextResult())
				{
				}
				return;
			}
			if (this.task != null)
			{
				return;
			}
			lock (this.taskLock)
			{
				if (this.task == null)
				{
					this.task = new Task(delegate()
					{
						while (this.ProduceNextResult())
						{
						}
					}, TaskCreationOptions.LongRunning);
					this.task.Start();
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006EB8 File Offset: 0x000050B8
		public override IEnumerable<Result> GetResults()
		{
			return this.Results;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006EC0 File Offset: 0x000050C0
		private bool ProduceNextResult()
		{
			bool result;
			lock (this.producerLock)
			{
				if (base.StartTime == default(ExDateTime))
				{
					base.StartTime = ExDateTime.Now;
					this.OnStart();
				}
				ExDateTime now = ExDateTime.Now;
				if (this.producerEnumerator.MoveNext())
				{
					this.ProcessResult(now);
					result = true;
				}
				else
				{
					while (this.parentResultEnumerator.Value.MoveNext())
					{
						if (!this.parentResultEnumerator.Value.Current.HasException)
						{
							this.producerEnumerator = this.setFunction(this.parentResultEnumerator.Value.Current).GetEnumerator();
							if (this.producerEnumerator.MoveNext())
							{
								this.ProcessResult(now);
								return true;
							}
						}
					}
					if (base.StopTime == default(ExDateTime))
					{
						base.StopTime = ExDateTime.Now;
						this.OnStop();
						if (this is Rule)
						{
							((IAnalysisAccessor)base.Analysis).UpdateProgress(this as Rule);
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007000 File Offset: 0x00005200
		private void ProcessResult(ExDateTime evaluationStartTime)
		{
			this.rwls.EnterWriteLock();
			Result<T> result = null;
			try
			{
				result = this.producerEnumerator.Current;
			}
			catch (Exception exception)
			{
				result = new Result<T>(exception);
			}
			finally
			{
				if (result == null)
				{
					result = new Result<T>(new AnalysisException(this, Strings.CannotReturnNullForResult));
				}
				if (result.HasException && result.Exception is FailureException)
				{
					((FailureException)result.Exception).AnalysisMemberSource = this;
				}
				((IResultAccessor)result).SetParent(this.parentResultEnumerator.Value.Current);
				((IResultAccessor)result).SetSource(this);
				((IResultAccessor)result).SetStartTime(evaluationStartTime);
				((IResultAccessor)result).SetStopTime(ExDateTime.Now);
				this.values.Add(result);
				this.rwls.ExitWriteLock();
			}
			this.OnEvaluate(result);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000070E0 File Offset: 0x000052E0
		private void OnStart()
		{
			((IAnalysisAccessor)base.Analysis).CallOnAnalysisMemberStart(this);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000070EE File Offset: 0x000052EE
		private void OnStop()
		{
			((IAnalysisAccessor)base.Analysis).CallOnAnalysisMemberStop(this);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000070FC File Offset: 0x000052FC
		private void OnEvaluate(Result result)
		{
			((IAnalysisAccessor)base.Analysis).CallOnAnalysisMemberEvaluate(this, result);
		}

		// Token: 0x040000F9 RID: 249
		private readonly Func<Result, IEnumerable<Result<T>>> setFunction;

		// Token: 0x040000FA RID: 250
		private readonly List<Result<T>> values;

		// Token: 0x040000FB RID: 251
		private readonly object producerLock;

		// Token: 0x040000FC RID: 252
		private readonly ReaderWriterLockSlim rwls;

		// Token: 0x040000FD RID: 253
		private readonly Lazy<IEnumerator<Result>> parentResultEnumerator;

		// Token: 0x040000FE RID: 254
		private readonly object taskLock;

		// Token: 0x040000FF RID: 255
		private IEnumerator<Result<T>> producerEnumerator;

		// Token: 0x04000100 RID: 256
		private Task task;

		// Token: 0x0200003D RID: 61
		private class ConsumerEnumerable : IEnumerable<Result<T>>, IEnumerable
		{
			// Token: 0x06000174 RID: 372 RVA: 0x0000710B File Offset: 0x0000530B
			public ConsumerEnumerable(AnalysisMember<T> owner)
			{
				this.owner = owner;
			}

			// Token: 0x06000175 RID: 373 RVA: 0x0000711A File Offset: 0x0000531A
			public IEnumerator<Result<T>> GetEnumerator()
			{
				return new AnalysisMember<T>.ConsumerEnumerator(this.owner);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00007127 File Offset: 0x00005327
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000101 RID: 257
			private AnalysisMember<T> owner;
		}

		// Token: 0x0200003E RID: 62
		private class ConsumerEnumerator : IEnumerator<Result<T>>, IDisposable, IEnumerator
		{
			// Token: 0x06000177 RID: 375 RVA: 0x0000712F File Offset: 0x0000532F
			public ConsumerEnumerator(AnalysisMember<T> owner)
			{
				this.owner = owner;
				this.currentIndex = -1;
				this.hasValue = false;
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000178 RID: 376 RVA: 0x0000714C File Offset: 0x0000534C
			public Result<T> Current
			{
				get
				{
					if (!this.hasValue)
					{
						throw new InvalidOperationException("Current property called before MoveNext().");
					}
					return this.currentValue;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x06000179 RID: 377 RVA: 0x00007167 File Offset: 0x00005367
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600017A RID: 378 RVA: 0x00007170 File Offset: 0x00005370
			public bool MoveNext()
			{
				this.currentIndex++;
				this.owner.rwls.EnterReadLock();
				try
				{
					if (this.currentIndex < this.owner.values.Count)
					{
						this.currentValue = this.owner.values[this.currentIndex];
						this.hasValue = true;
						return true;
					}
				}
				finally
				{
					this.owner.rwls.ExitReadLock();
				}
				return this.GetNextValueFromSource();
			}

			// Token: 0x0600017B RID: 379 RVA: 0x00007208 File Offset: 0x00005408
			public void Reset()
			{
				this.currentIndex = -1;
				this.hasValue = false;
			}

			// Token: 0x0600017C RID: 380 RVA: 0x00007218 File Offset: 0x00005418
			public void Dispose()
			{
			}

			// Token: 0x0600017D RID: 381 RVA: 0x0000721C File Offset: 0x0000541C
			private bool GetNextValueFromSource()
			{
				this.owner.ProduceNextResult();
				this.owner.rwls.EnterReadLock();
				try
				{
					if (this.currentIndex < this.owner.values.Count)
					{
						this.currentValue = this.owner.values[this.currentIndex];
						this.hasValue = true;
						return true;
					}
				}
				finally
				{
					this.owner.rwls.ExitReadLock();
				}
				this.hasValue = false;
				return false;
			}

			// Token: 0x04000102 RID: 258
			private AnalysisMember<T> owner;

			// Token: 0x04000103 RID: 259
			private int currentIndex;

			// Token: 0x04000104 RID: 260
			private Result<T> currentValue;

			// Token: 0x04000105 RID: 261
			private bool hasValue;
		}
	}
}
