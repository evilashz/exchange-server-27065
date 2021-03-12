using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000022 RID: 34
	public abstract class AnalysisMember : Analysis.AnalysisMemberBase
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000048FC File Offset: 0x00002AFC
		public AnalysisMember(Analysis analysis, FeatureSet features) : base(analysis)
		{
			this.parent = features.GetFeature<ForEachResultFeature>().ForEachResultFunc;
			this.evaluationMode = features.GetFeature<EvaluationModeFeature>().EvaluationMode;
			this.features = features;
			this.currentState = new Optimistic<AnalysisMember.AnalysisMemberState>(new AnalysisMember.AnalysisMemberState(), new Resolver<AnalysisMember.AnalysisMemberState>(AnalysisMember.AnalysisMemberState.Resolve));
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004955 File Offset: 0x00002B55
		public AnalysisMember Parent
		{
			get
			{
				return this.parent();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004962 File Offset: 0x00002B62
		public Evaluate EvaluationMode
		{
			get
			{
				return this.evaluationMode;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000496A File Offset: 0x00002B6A
		public bool IsConclusion
		{
			get
			{
				return base.AnalysisConclusionsFilter(this);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EA RID: 234
		public abstract Type ValueType { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EB RID: 235
		public abstract IEnumerable<Result> UntypedResults { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EC RID: 236
		public abstract IEnumerable<Result> CachedResults { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004978 File Offset: 0x00002B78
		public ExDateTime StartTime
		{
			get
			{
				AnalysisMember.AnalysisMemberState unsafeValue = this.currentState.UnsafeValue;
				if (unsafeValue.HasStarted)
				{
					return unsafeValue.StartTime;
				}
				AnalysisMember.AnalysisMemberState safeValue = this.currentState.SafeValue;
				return safeValue.StartTime;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000049B4 File Offset: 0x00002BB4
		public ExDateTime StopTime
		{
			get
			{
				AnalysisMember.AnalysisMemberState unsafeValue = this.currentState.UnsafeValue;
				if (unsafeValue.HasCompleted)
				{
					return unsafeValue.StopTime;
				}
				AnalysisMember.AnalysisMemberState safeValue = this.currentState.SafeValue;
				return safeValue.StopTime;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000049EE File Offset: 0x00002BEE
		public FeatureSet Features
		{
			get
			{
				return this.features;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public IEnumerable<AnalysisMember> AncestorsAndSelf()
		{
			for (AnalysisMember current = this; current != null; current = current.Parent)
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x060000F1 RID: 241
		public abstract void Start();

		// Token: 0x060000F2 RID: 242 RVA: 0x00004B08 File Offset: 0x00002D08
		protected override void OnStart()
		{
			AnalysisMember.AnalysisMemberState unsafeValue = this.currentState.UnsafeValue;
			this.currentState.Update(unsafeValue, unsafeValue.SetAsStarted());
			base.OnStart();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004B3C File Offset: 0x00002D3C
		protected override void OnComplete()
		{
			AnalysisMember.AnalysisMemberState unsafeValue = this.currentState.UnsafeValue;
			this.currentState.Update(unsafeValue, unsafeValue.SetAsCompleted());
			base.OnComplete();
		}

		// Token: 0x0400005A RID: 90
		private readonly Optimistic<AnalysisMember.AnalysisMemberState> currentState;

		// Token: 0x0400005B RID: 91
		private readonly Func<AnalysisMember> parent;

		// Token: 0x0400005C RID: 92
		private readonly Evaluate evaluationMode;

		// Token: 0x0400005D RID: 93
		private readonly FeatureSet features;

		// Token: 0x02000023 RID: 35
		private sealed class AnalysisMemberState
		{
			// Token: 0x060000F4 RID: 244 RVA: 0x00004B6E File Offset: 0x00002D6E
			public AnalysisMemberState()
			{
				this.hasStarted = false;
				this.hasCompleted = false;
				this.startTime = default(ExDateTime);
				this.stopTime = default(ExDateTime);
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x00004B9C File Offset: 0x00002D9C
			private AnalysisMemberState(bool hasStarted, bool hasCompleted, ExDateTime startTime, ExDateTime stopTime)
			{
				this.hasStarted = hasStarted;
				this.hasCompleted = hasCompleted;
				this.startTime = startTime;
				this.stopTime = stopTime;
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004BC1 File Offset: 0x00002DC1
			public bool HasStarted
			{
				get
				{
					return this.hasStarted;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004BC9 File Offset: 0x00002DC9
			public bool HasCompleted
			{
				get
				{
					return this.hasCompleted;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004BD1 File Offset: 0x00002DD1
			public ExDateTime StartTime
			{
				get
				{
					if (!this.hasStarted)
					{
						throw new InvalidOperationException(Strings.CannotGetStartTimeBeforeMemberStart);
					}
					return this.startTime;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004BF1 File Offset: 0x00002DF1
			public ExDateTime StopTime
			{
				get
				{
					if (!this.hasCompleted)
					{
						throw new InvalidOperationException(Strings.CannotGetStopTimeBeforeMemberCompletion);
					}
					return this.stopTime;
				}
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00004C14 File Offset: 0x00002E14
			public static AnalysisMember.AnalysisMemberState Resolve(AnalysisMember.AnalysisMemberState originalValue, AnalysisMember.AnalysisMemberState currentValue, AnalysisMember.AnalysisMemberState updatedValue)
			{
				return new AnalysisMember.AnalysisMemberState(currentValue.hasStarted || updatedValue.hasStarted, currentValue.hasCompleted || updatedValue.hasCompleted, (currentValue.startTime < updatedValue.startTime) ? currentValue.startTime : updatedValue.startTime, (currentValue.stopTime < updatedValue.stopTime) ? currentValue.stopTime : updatedValue.stopTime);
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00004C8C File Offset: 0x00002E8C
			public AnalysisMember.AnalysisMemberState SetAsStarted()
			{
				if (this.hasStarted)
				{
					return this;
				}
				return this.With(true, default(Optional<bool>), ExDateTime.Now, default(Optional<ExDateTime>));
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00004CCC File Offset: 0x00002ECC
			public AnalysisMember.AnalysisMemberState SetAsCompleted()
			{
				if (this.hasCompleted)
				{
					return this;
				}
				return this.With(default(Optional<bool>), true, default(Optional<ExDateTime>), ExDateTime.Now);
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00004D0B File Offset: 0x00002F0B
			private AnalysisMember.AnalysisMemberState With(Optional<bool> hasStarted = default(Optional<bool>), Optional<bool> hasCompleted = default(Optional<bool>), Optional<ExDateTime> startTime = default(Optional<ExDateTime>), Optional<ExDateTime> stopTime = default(Optional<ExDateTime>))
			{
				return new AnalysisMember.AnalysisMemberState(hasStarted.DefaultTo(this.hasStarted), hasCompleted.DefaultTo(this.hasCompleted), startTime.DefaultTo(this.startTime), stopTime.DefaultTo(this.stopTime));
			}

			// Token: 0x0400005E RID: 94
			private readonly bool hasStarted;

			// Token: 0x0400005F RID: 95
			private readonly bool hasCompleted;

			// Token: 0x04000060 RID: 96
			private readonly ExDateTime startTime;

			// Token: 0x04000061 RID: 97
			private readonly ExDateTime stopTime;
		}
	}
}
