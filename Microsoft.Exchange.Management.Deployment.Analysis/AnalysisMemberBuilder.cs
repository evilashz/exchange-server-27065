using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000024 RID: 36
	public abstract class AnalysisMemberBuilder : FeatureSet.Builder, IAnalysisDependantSetter
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004D46 File Offset: 0x00002F46
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004D4E File Offset: 0x00002F4E
		private protected Analysis Analysis { protected get; private set; }

		// Token: 0x06000100 RID: 256 RVA: 0x00004D57 File Offset: 0x00002F57
		void IAnalysisDependantSetter.SetAnalysis(Analysis analysis)
		{
			this.Analysis = analysis;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004D6D File Offset: 0x00002F6D
		public Setting<TResult> CopyOfSetting<TResult>(Func<Setting<TResult>> setting)
		{
			return this.CopyOfSetting<TResult, object>(setting, () => this.Analysis.RootAnalysisMember);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004D9C File Offset: 0x00002F9C
		public Setting<TResult> CopyOfSetting<TResult, TParent>(Func<Setting<TResult>> setting, Func<AnalysisMember<TParent>> forEachResult)
		{
			return new Setting<TResult>(this.Analysis, base.BuildFeatureSet(() => setting().Features, Enumerable.Empty<Feature>(), new Feature[]
			{
				new ForEachResultFeature(forEachResult)
			}));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004DF6 File Offset: 0x00002FF6
		public Rule CopyOfRule(Func<Rule> rule)
		{
			return this.CopyOfRule<object>(rule, () => this.Analysis.RootAnalysisMember);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004E28 File Offset: 0x00003028
		public Rule CopyOfRule<TParent>(Func<Rule> rule, Func<AnalysisMember<TParent>> forEachResult)
		{
			return new Rule(this.Analysis, base.BuildFeatureSet(() => rule().Features, Enumerable.Empty<Feature>(), new Feature[]
			{
				new ForEachResultFeature(forEachResult)
			}));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004ED4 File Offset: 0x000030D4
		protected Setting<TResult> Setting<TResult>(Func<TResult> setValue, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildSetting<TResult>(() => this.Analysis.RootAnalysisMember, delegate(Result x)
			{
				Result<TResult> result;
				try
				{
					result = new Result<TResult>(setValue());
				}
				catch (Exception exception)
				{
					result = new Result<TResult>(exception);
				}
				return new Result[]
				{
					result
				};
			}, evaluate, features);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004F98 File Offset: 0x00003198
		protected Setting<TResult> Setting<TResult>(Func<IEnumerable<TResult>> setValues, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildSetting<TResult>(() => this.Analysis.RootAnalysisMember, delegate(Result x)
			{
				IEnumerable<Result> result;
				try
				{
					result = from y in setValues()
					select new Result<TResult>(y);
				}
				catch (Exception exception)
				{
					result = new Result[]
					{
						new Result<TResult>(exception)
					};
				}
				return result;
			}, evaluate, features);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000506C File Offset: 0x0000326C
		protected Setting<TResult> Setting<TResult>(Func<IEnumerable<Result<TResult>>> setResults, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildSetting<TResult>(() => this.Analysis.RootAnalysisMember, delegate(Result x)
			{
				IEnumerable<Result> enumerable;
				try
				{
					enumerable = setResults();
					if (enumerable.Any((Result y) => y == null))
					{
						throw new AnalysisException(Strings.NullResult);
					}
				}
				catch (Exception exception)
				{
					enumerable = new Result[]
					{
						new Result<TResult>(exception)
					};
				}
				return enumerable;
			}, evaluate, features);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005104 File Offset: 0x00003304
		protected Setting<TResult> Setting<TResult, TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, TResult> setValue, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildSetting<TResult>(forEachResult, delegate(Result x)
			{
				Result<TResult> result;
				try
				{
					result = new Result<TResult>(setValue((Result<TParent>)x));
				}
				catch (Exception exception)
				{
					result = new Result<TResult>(exception);
				}
				return new Result[]
				{
					result
				};
			}, evaluate, features);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000051EC File Offset: 0x000033EC
		protected Setting<TResult> Setting<TResult, TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, IEnumerable<TResult>> setValues, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildSetting<TResult>(forEachResult, delegate(Result x)
			{
				IEnumerable<Result> enumerable;
				try
				{
					enumerable = from y in setValues((Result<TParent>)x)
					select new Result<TResult>(y);
					if (enumerable.Any((Result y) => y == null))
					{
						throw new AnalysisException(Strings.NullResult);
					}
				}
				catch (Exception exception)
				{
					enumerable = new Result[]
					{
						new Result<TResult>(exception)
					};
				}
				return enumerable;
			}, evaluate, features);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000052A8 File Offset: 0x000034A8
		protected Setting<TResult> Setting<TResult, TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, IEnumerable<Result<TResult>>> setResults, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildSetting<TResult>(forEachResult, delegate(Result x)
			{
				IEnumerable<Result> enumerable;
				try
				{
					enumerable = setResults((Result<TParent>)x);
					if (enumerable.Any((Result y) => y == null))
					{
						throw new AnalysisException(Strings.NullResult);
					}
				}
				catch (Exception exception)
				{
					enumerable = new Result[]
					{
						new Result<TResult>(exception)
					};
				}
				return enumerable;
			}, evaluate, features);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005338 File Offset: 0x00003538
		protected Rule Rule(Func<bool> condition, Evaluate evaluate, Severity severity, params Feature[] features)
		{
			return this.BuildRule(() => this.Analysis.RootAnalysisMember, delegate(Result x)
			{
				RuleResult ruleResult;
				try
				{
					ruleResult = new RuleResult(condition());
				}
				catch (Exception exception)
				{
					ruleResult = new RuleResult(exception);
				}
				return new Result[]
				{
					ruleResult
				};
			}, evaluate, severity, features);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000053F4 File Offset: 0x000035F4
		protected Rule Rule(Func<Tuple<bool, Severity>> condition, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildRule(() => this.Analysis.RootAnalysisMember, delegate(Result x)
			{
				RuleResult ruleResult;
				try
				{
					Tuple<bool, Severity> tuple = condition();
					ruleResult = new RuleResult(tuple.Item1)
					{
						Severity = new Severity?(tuple.Item2)
					};
				}
				catch (Exception exception)
				{
					ruleResult = new RuleResult(exception);
				}
				return new Result[]
				{
					ruleResult
				};
			}, evaluate, Severity.Info, features);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000548C File Offset: 0x0000368C
		protected Rule Rule<TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, bool> condition, Evaluate evaluate, Severity severity, params Feature[] features)
		{
			return this.BuildRule(forEachResult, delegate(Result x)
			{
				RuleResult ruleResult;
				try
				{
					ruleResult = new RuleResult(condition((Result<TParent>)x));
				}
				catch (Exception exception)
				{
					ruleResult = new RuleResult(exception);
				}
				return new Result[]
				{
					ruleResult
				};
			}, evaluate, severity, features);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005534 File Offset: 0x00003734
		protected Rule Rule<TParent>(Func<AnalysisMember<TParent>> forEachResult, Func<Result<TParent>, Tuple<bool, Severity>> condition, Evaluate evaluate, params Feature[] features)
		{
			return this.BuildRule(forEachResult, delegate(Result x)
			{
				RuleResult ruleResult;
				try
				{
					Tuple<bool, Severity> tuple = condition((Result<TParent>)x);
					ruleResult = new RuleResult(tuple.Item1)
					{
						Severity = new Severity?(tuple.Item2)
					};
				}
				catch (Exception exception)
				{
					ruleResult = new RuleResult(exception);
				}
				return new Result[]
				{
					ruleResult
				};
			}, evaluate, Severity.Info, features);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005568 File Offset: 0x00003768
		private Setting<TResult> BuildSetting<TResult>(Func<AnalysisMember> forEachResultFunc, Func<Result, IEnumerable<Result>> resultsFunc, Evaluate evaluate, IEnumerable<Feature> features)
		{
			if (forEachResultFunc == null)
			{
				throw new ArgumentNullException("forEachResultFunc");
			}
			if (resultsFunc == null)
			{
				throw new ArgumentNullException("resultsFunc");
			}
			if (features == null)
			{
				throw new ArgumentNullException("features");
			}
			return new Setting<TResult>(this.Analysis, base.BuildFeatureSet(features, new Feature[]
			{
				new EvaluationModeFeature(evaluate),
				new ForEachResultFeature(forEachResultFunc),
				new ResultsFeature(resultsFunc)
			}));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000055D8 File Offset: 0x000037D8
		private Rule BuildRule(Func<AnalysisMember> forEachResultFunc, Func<Result, IEnumerable<Result>> resultsFunc, Evaluate evaluate, Severity severity, IEnumerable<Feature> features)
		{
			if (forEachResultFunc == null)
			{
				throw new ArgumentNullException("forEachResultFunc");
			}
			if (resultsFunc == null)
			{
				throw new ArgumentNullException("resultsFunc");
			}
			if (features == null)
			{
				throw new ArgumentNullException("features");
			}
			return new Rule(this.Analysis, base.BuildFeatureSet(features, new Feature[]
			{
				new EvaluationModeFeature(evaluate),
				new SeverityFeature(severity),
				new ForEachResultFeature(forEachResultFunc),
				new ResultsFeature(resultsFunc)
			}));
		}
	}
}
