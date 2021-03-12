using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000039 RID: 57
	internal abstract class Analysis : IAnalysisAccessor, IXmlSerializable
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00005764 File Offset: 0x00003964
		public Analysis(IDataProviderFactory providers) : this(providers, (AnalysisMember x) => true, (AnalysisMember x) => true)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000057CC File Offset: 0x000039CC
		public Analysis(IDataProviderFactory providers, Func<AnalysisMember, bool> startFilter, Func<AnalysisMember, bool> conclusionsFilter)
		{
			this.Providers = providers;
			this.StartFilter = startFilter;
			this.ConclusionsFilter = conclusionsFilter;
			this.isAnalysisStarted = false;
			this.analysisMemberNames = new Lazy<Dictionary<AnalysisMember, string>>(new Func<Dictionary<AnalysisMember, string>>(this.PopulateAnalysisMemberNames), true);
			this.totalConclusionRules = new Lazy<int>(() => this.Rules.Where(this.ConclusionsFilter).Count<AnalysisMember>(), true);
			this.RootAnalysisMember = new RootAnalysisMember(this);
			this.progressUpdateLock = new object();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000104 RID: 260 RVA: 0x0000584C File Offset: 0x00003A4C
		// (remove) Token: 0x06000105 RID: 261 RVA: 0x00005884 File Offset: 0x00003A84
		public event EventHandler<ProgressUpdateEventArgs> ProgressUpdated;

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000058B9 File Offset: 0x00003AB9
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000058C1 File Offset: 0x00003AC1
		public IDataProviderFactory Providers { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000058CA File Offset: 0x00003ACA
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000058D2 File Offset: 0x00003AD2
		public RootAnalysisMember RootAnalysisMember { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000058DC File Offset: 0x00003ADC
		public int CompletedRules
		{
			get
			{
				int result;
				lock (this.progressUpdateLock)
				{
					result = this.completedConclusionRules;
				}
				return result;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005920 File Offset: 0x00003B20
		public int TotalConclusionRules
		{
			get
			{
				return this.totalConclusionRules.Value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005935 File Offset: 0x00003B35
		public IEnumerable<Result> Conclusions
		{
			get
			{
				this.StartAnalysis();
				return this.AnalysisMembers.Where(this.ConclusionsFilter).SelectMany((AnalysisMember x) => x.GetResults());
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000059D8 File Offset: 0x00003BD8
		public IEnumerable<Result> Errors
		{
			get
			{
				return from x in this.Conclusions
				where !x.HasException && x.Source is Rule && ((RuleResult)x).Value
				where (from y in x.Source.Features.OfType<RuleTypeFeature>()
				where y.RuleType == RuleType.Error
				select y).Any<RuleTypeFeature>()
				select x;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005A98 File Offset: 0x00003C98
		public IEnumerable<Result> Warnings
		{
			get
			{
				return from x in this.Conclusions
				where !x.HasException && x.Source is Rule && ((RuleResult)x).Value
				where (from y in x.Source.Features.OfType<RuleTypeFeature>()
				where y.RuleType == RuleType.Warning
				select y).Any<RuleTypeFeature>()
				select x;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005B58 File Offset: 0x00003D58
		public IEnumerable<Result> Info
		{
			get
			{
				return from x in this.Conclusions
				where !x.HasException && x.Source is Rule && ((RuleResult)x).Value
				where (from y in x.Source.Features.OfType<RuleTypeFeature>()
				where y.RuleType == RuleType.Info
				select y).Any<RuleTypeFeature>()
				select x;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005BB7 File Offset: 0x00003DB7
		public IEnumerable<Result> Exceptions
		{
			get
			{
				return from x in this.Conclusions
				where x.HasException
				select x;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005BE1 File Offset: 0x00003DE1
		public IEnumerable<AnalysisMember> AnalysisMembers
		{
			get
			{
				return this.analysisMemberNames.Value.Keys;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005C1E File Offset: 0x00003E1E
		public IEnumerable<AnalysisMember> Settings
		{
			get
			{
				return from x in this.AnalysisMembers
				where x.GetType().IsGenericType && x.GetType().GetGenericTypeDefinition() == typeof(Setting<>)
				select x;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005C53 File Offset: 0x00003E53
		public IEnumerable<Rule> Rules
		{
			get
			{
				return (from x in this.AnalysisMembers
				where x is Rule
				select x).Cast<Rule>();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005C82 File Offset: 0x00003E82
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005C8A File Offset: 0x00003E8A
		public Func<AnalysisMember, bool> StartFilter { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005C93 File Offset: 0x00003E93
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00005C9B File Offset: 0x00003E9B
		public Func<AnalysisMember, bool> ConclusionsFilter { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005CA4 File Offset: 0x00003EA4
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public ExDateTime StartTime
		{
			get
			{
				long ticks = Thread.VolatileRead(ref this.startTimeTicks);
				return new ExDateTime(ExTimeZone.UtcTimeZone, ticks);
			}
			private set
			{
				long utcTicks = value.UtcTicks;
				Interlocked.Exchange(ref this.startTimeTicks, utcTicks);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005CEC File Offset: 0x00003EEC
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005D10 File Offset: 0x00003F10
		public ExDateTime StopTime
		{
			get
			{
				long ticks = Thread.VolatileRead(ref this.stopTimeTicks);
				return new ExDateTime(ExTimeZone.UtcTimeZone, ticks);
			}
			private set
			{
				long utcTicks = value.UtcTicks;
				Interlocked.Exchange(ref this.stopTimeTicks, utcTicks);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005D54 File Offset: 0x00003F54
		public void StartAnalysis()
		{
			if (this.isAnalysisStarted)
			{
				return;
			}
			this.isAnalysisStarted = true;
			this.StartTime = ExDateTime.Now;
			this.OnAnalysisStart();
			foreach (AnalysisMember analysisMember in from y in this.AnalysisMembers
			where y.RunAs != ConcurrencyType.Synchronous && this.StartFilter(y)
			select y)
			{
				analysisMember.Start();
			}
			Parallel.ForEach<AnalysisMember>(this.AnalysisMembers.Where(this.ConclusionsFilter).ToArray<AnalysisMember>(), delegate(AnalysisMember x)
			{
				x.Start();
			});
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005E0C File Offset: 0x0000400C
		public string GetAnalysisMemberName(AnalysisMember analysisMemeber)
		{
			return this.analysisMemberNames.Value[analysisMemeber];
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005E20 File Offset: 0x00004020
		void IAnalysisAccessor.UpdateProgress(Rule completedRule)
		{
			if (completedRule == null)
			{
				throw new ArgumentNullException("completedRule");
			}
			if (completedRule.Analysis != this)
			{
				throw new AnalysisException(completedRule, Strings.UpdateProgressForWrongAnalysis);
			}
			if (!this.ConclusionsFilter(completedRule))
			{
				return;
			}
			lock (this.progressUpdateLock)
			{
				int completedRules = ++this.completedConclusionRules;
				int value = this.totalConclusionRules.Value;
				ProgressUpdateEventArgs progressUpdateEventArgs = new ProgressUpdateEventArgs(completedRules, value);
				EventHandler<ProgressUpdateEventArgs> progressUpdated = this.ProgressUpdated;
				if (progressUpdated != null)
				{
					progressUpdated(this, progressUpdateEventArgs);
				}
				if (progressUpdateEventArgs.CompletedPercentage == 100)
				{
					this.StopTime = ExDateTime.Now;
					this.OnAnalysisStop();
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005EEC File Offset: 0x000040EC
		void IAnalysisAccessor.CallOnAnalysisMemberStart(AnalysisMember member)
		{
			if (member is RootAnalysisMember)
			{
				return;
			}
			this.OnAnalysisMemberStart(member);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005EFE File Offset: 0x000040FE
		void IAnalysisAccessor.CallOnAnalysisMemberStop(AnalysisMember member)
		{
			if (member is RootAnalysisMember)
			{
				return;
			}
			this.OnAnalysisMemberStop(member);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005F10 File Offset: 0x00004110
		void IAnalysisAccessor.CallOnAnalysisMemberEvaluate(AnalysisMember member, Result result)
		{
			if (member is RootAnalysisMember)
			{
				return;
			}
			this.OnAnalysisMemberEvaluate(member, result);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005F23 File Offset: 0x00004123
		protected virtual void OnAnalysisStart()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005F25 File Offset: 0x00004125
		protected virtual void OnAnalysisStop()
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005F27 File Offset: 0x00004127
		protected virtual void OnAnalysisMemberStart(AnalysisMember member)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005F29 File Offset: 0x00004129
		protected virtual void OnAnalysisMemberStop(AnalysisMember member)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005F2B File Offset: 0x0000412B
		protected virtual void OnAnalysisMemberEvaluate(AnalysisMember member, Result result)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005F2D File Offset: 0x0000412D
		protected virtual void WriteConfiguration(XmlWriter writer)
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005F2F File Offset: 0x0000412F
		protected virtual void ReadConfiguration(XmlReader reader)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005F31 File Offset: 0x00004131
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005F34 File Offset: 0x00004134
		public void ReadXml(XmlReader reader)
		{
			throw new NotSupportedException("Read from XML is not supported.");
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005F40 File Offset: 0x00004140
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("analysis");
			writer.WriteStartElement("configuration");
			this.WriteConfiguration(writer);
			writer.WriteEndElement();
			foreach (AnalysisMember analysisMember in this.AnalysisMembers.Where(this.ConclusionsFilter))
			{
				analysisMember.WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006130 File Offset: 0x00004330
		private Dictionary<AnalysisMember, string> PopulateAnalysisMemberNames()
		{
			Dictionary<AnalysisMember, string> dictionary = new Dictionary<AnalysisMember, string>();
			IEnumerable<KeyValuePair<AnalysisMember, string>> enumerable = from x in base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where typeof(AnalysisMember).IsAssignableFrom(x.PropertyType)
			let analysisMember = (AnalysisMember)x.GetValue(this, null)
			where analysisMember != null
			select new KeyValuePair<AnalysisMember, string>(analysisMember, x.Name);
			foreach (KeyValuePair<AnalysisMember, string> keyValuePair in enumerable)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x040000D1 RID: 209
		private Lazy<Dictionary<AnalysisMember, string>> analysisMemberNames;

		// Token: 0x040000D2 RID: 210
		private int completedConclusionRules;

		// Token: 0x040000D3 RID: 211
		private Lazy<int> totalConclusionRules;

		// Token: 0x040000D4 RID: 212
		private bool isAnalysisStarted;

		// Token: 0x040000D5 RID: 213
		private long startTimeTicks;

		// Token: 0x040000D6 RID: 214
		private long stopTimeTicks;

		// Token: 0x040000D7 RID: 215
		private object progressUpdateLock;
	}
}
