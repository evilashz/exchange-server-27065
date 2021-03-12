using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000778 RID: 1912
	[Serializable]
	internal class AnalysisRule
	{
		// Token: 0x0600438C RID: 17292 RVA: 0x001151EB File Offset: 0x001133EB
		protected AnalysisRule()
		{
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x001151F3 File Offset: 0x001133F3
		// (set) Token: 0x0600438E RID: 17294 RVA: 0x001151FB File Offset: 0x001133FB
		public string Name { get; protected set; }

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x00115204 File Offset: 0x00113404
		// (set) Token: 0x06004390 RID: 17296 RVA: 0x0011520C File Offset: 0x0011340C
		public AnalysisRule.RuleAlertLevel AlertLevel { get; protected set; }

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06004391 RID: 17297 RVA: 0x00115215 File Offset: 0x00113415
		// (set) Token: 0x06004392 RID: 17298 RVA: 0x0011521D File Offset: 0x0011341D
		public string Message { get; protected set; }

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06004393 RID: 17299 RVA: 0x0011522E File Offset: 0x0011342E
		public string[] RuleProperties
		{
			get
			{
				return (from f in this.RequiredProperties
				select f.Name).ToArray<string>();
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x0011525D File Offset: 0x0011345D
		// (set) Token: 0x06004395 RID: 17301 RVA: 0x00115265 File Offset: 0x00113465
		internal IEnumerable<PropertyDefinition> RequiredProperties { get; set; }

		// Token: 0x06004396 RID: 17302 RVA: 0x00115270 File Offset: 0x00113470
		public void Analyze(LinkedList<CalendarLogAnalysis> list)
		{
			for (LinkedListNode<CalendarLogAnalysis> linkedListNode = list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				this.AnalyzeLog(linkedListNode);
			}
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x00115297 File Offset: 0x00113497
		protected virtual void AnalyzeLog(LinkedListNode<CalendarLogAnalysis> logNode)
		{
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x00115299 File Offset: 0x00113499
		public override string ToString()
		{
			return string.Format("{0}-{1}:{2}", this.AlertLevel, this.Name, this.Message);
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x001152BC File Offset: 0x001134BC
		public static IEnumerable<AnalysisRule> GetAnalysisRules()
		{
			return new List<AnalysisRule>
			{
				new MessageClassCheck(),
				new MissingOrganizerEmailAddressCheck(),
				new MissingSenderEmailAddressCheck(),
				new StartTimeCheck(),
				new EndTimeCheck(),
				new CheckClientIntent(),
				new KnowniPhoneIssues(),
				new SentRepresentingChangeCheck()
			};
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x00115328 File Offset: 0x00113528
		internal AnalysisRule Clone()
		{
			return new AnalysisRule
			{
				AlertLevel = this.AlertLevel,
				Message = this.Message,
				Name = this.Name,
				RequiredProperties = this.RequiredProperties
			};
		}

		// Token: 0x02000779 RID: 1913
		public enum RuleAlertLevel
		{
			// Token: 0x04002A11 RID: 10769
			Info,
			// Token: 0x04002A12 RID: 10770
			Warning,
			// Token: 0x04002A13 RID: 10771
			Error
		}
	}
}
