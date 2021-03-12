using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000025 RID: 37
	public abstract class AnalysisMember<T> : AnalysisMember
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005658 File Offset: 0x00003858
		protected AnalysisMember(Analysis analysis, FeatureSet features) : base(analysis, features)
		{
			this.producer = null;
			this.results = new ResultsCache<T>();
			this.producerLock = new object();
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000567F File Offset: 0x0000387F
		public override Type ValueType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000568B File Offset: 0x0000388B
		public Results<T> Results
		{
			get
			{
				return new Results<T>(this, this.CreateConsumerEnumerable());
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005699 File Offset: 0x00003899
		public override IEnumerable<Result> UntypedResults
		{
			get
			{
				return this.Results;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000056A4 File Offset: 0x000038A4
		public override IEnumerable<Result> CachedResults
		{
			get
			{
				if (this.results.IsComplete)
				{
					return this.results;
				}
				IEnumerable<Result> result;
				lock (this.producerLock)
				{
					result = this.results;
				}
				return result;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000056FC File Offset: 0x000038FC
		public Result<T> Result
		{
			get
			{
				if (this.Results.Skip(1).Any<Result<T>>())
				{
					throw new MultipleResultsException(this);
				}
				Result<T> result = this.Results.FirstOrDefault<Result<T>>();
				if (result == null)
				{
					throw new EmptyResultsException(this);
				}
				return result;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000573A File Offset: 0x0000393A
		public T Value
		{
			get
			{
				return this.Result.Value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005748 File Offset: 0x00003948
		public T ValueOrDefault
		{
			get
			{
				T result;
				try
				{
					result = this.Result.ValueOrDefault;
				}
				catch
				{
					result = default(T);
				}
				return result;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000579C File Offset: 0x0000399C
		public Results<T> RelativeResults(Result relativeTo)
		{
			HashSet<AnalysisMember> @object = new HashSet<AnalysisMember>(base.AncestorsAndSelf());
			AnalysisMember commonAncestor = relativeTo.Source.AncestorsAndSelf().First(new Func<AnalysisMember, bool>(@object.Contains));
			Result result = relativeTo.AncestorsAndSelf().First((Result x) => x.Source == commonAncestor);
			return result.DescendantsOfType<T>(this);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000057FC File Offset: 0x000039FC
		public override void Start()
		{
			IEnumerator enumerator = this.Results.GetEnumerator();
			while (enumerator.MoveNext())
			{
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D74 File Offset: 0x00003F74
		private IEnumerator<Result<T>> CreateProducerEnumerator()
		{
			AnalysisMember parent = base.Features.GetFeature<ForEachResultFeature>().ForEachResultFunc();
			if (parent == null)
			{
				this.OnStart();
				Result<T> rootResult = base.Analysis.RootAnalysisMember.UntypedResults.Single<Result>() as Result<T>;
				this.results = this.results.Add(rootResult);
				this.OnEvaluate(rootResult);
				yield return rootResult;
				this.OnComplete();
			}
			else
			{
				Func<Result, IEnumerable<Result>> resultFunc = base.Features.GetFeature<ResultsFeature>().ResultFunc;
				Func<FeatureSet, Result, bool> func;
				if (!base.Features.HasFeature<FilterFeature>())
				{
					func = ((FeatureSet fs, Result r) => true);
				}
				else
				{
					func = base.Features.GetFeature<FilterFeature>().FilterFunc;
				}
				Func<FeatureSet, Result, bool> filterFunc = func;
				this.OnStart();
				foreach (Result parentResult in parent.UntypedResults)
				{
					if (!parentResult.HasException)
					{
						ExDateTime startTime = ExDateTime.Now;
						Stopwatch stopWatch = Stopwatch.StartNew();
						foreach (Result result in resultFunc(parentResult))
						{
							Result<T> producerResult = (Result<T>)result;
							if (base.IsAnalysisCanceled)
							{
								yield break;
							}
							stopWatch.Stop();
							ExDateTime stopTime = startTime + stopWatch.Elapsed;
							if (producerResult.HasException)
							{
								if (producerResult.Exception is AnalysisException)
								{
									AnalysisException ex = (AnalysisException)producerResult.Exception;
									ex.AnalysisMemberSource = this;
								}
								if (producerResult.Exception is CriticalException)
								{
									base.CancelAnalysis((CriticalException)producerResult.Exception);
								}
							}
							Result<T> filterResult = producerResult;
							try
							{
								if (!filterFunc(base.Features, producerResult))
								{
									filterResult = new Result<T>(new FilteredException(this, producerResult));
								}
							}
							catch (Exception inner)
							{
								base.CancelAnalysis(new CriticalException(this, inner));
							}
							Result<T> consumerResult;
							if (filterResult is RuleResult)
							{
								RuleResult toCopy = filterResult as RuleResult;
								RuleResult ruleResult = new RuleResult(toCopy, this, parentResult, startTime, stopTime);
								consumerResult = (ruleResult as Result<T>);
							}
							else
							{
								consumerResult = new Result<T>(filterResult, this, parentResult, startTime, stopTime);
							}
							this.results = this.results.Add(consumerResult);
							this.OnEvaluate(consumerResult);
							yield return consumerResult;
							startTime = stopTime;
							stopWatch.Restart();
						}
					}
				}
				this.results = this.results.AsCompleted();
				this.OnComplete();
			}
			yield break;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006134 File Offset: 0x00004334
		private IEnumerable<Result<T>> CreateConsumerEnumerable()
		{
			int nextResultIndex = 0;
			ResultsCache<T> unsafeResults = this.results;
			foreach (Result<T> result in unsafeResults.Skip(nextResultIndex))
			{
				yield return result;
			}
			nextResultIndex = unsafeResults.Count;
			if (!unsafeResults.IsComplete)
			{
				ResultsCache<T> currentResults;
				do
				{
					lock (this.producerLock)
					{
						if (nextResultIndex == this.results.Count)
						{
							if (this.producer == null)
							{
								this.producer = this.CreateProducerEnumerator();
							}
							try
							{
								if (!this.producer.MoveNext())
								{
									yield break;
								}
							}
							catch (Exception inner)
							{
								base.CancelAnalysis(new CriticalException(this, inner));
							}
						}
						currentResults = this.results;
					}
					foreach (Result<T> result2 in currentResults.Skip(nextResultIndex))
					{
						yield return result2;
					}
					nextResultIndex = currentResults.Count;
				}
				while (!currentResults.IsComplete);
			}
			yield break;
		}

		// Token: 0x04000063 RID: 99
		private readonly object producerLock;

		// Token: 0x04000064 RID: 100
		private IEnumerator<Result<T>> producer;

		// Token: 0x04000065 RID: 101
		private ResultsCache<T> results;
	}
}
