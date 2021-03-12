using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200006F RID: 111
	public class EventMatchingRule
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00018516 File Offset: 0x00016716
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0001851E File Offset: 0x0001671E
		public string LogName { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00018527 File Offset: 0x00016727
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0001852F File Offset: 0x0001672F
		public string ProviderName { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00018538 File Offset: 0x00016738
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00018540 File Offset: 0x00016740
		public IEnumerable<int> EventIds { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00018549 File Offset: 0x00016749
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00018551 File Offset: 0x00016751
		public int ResourceNameIndex { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001855A File Offset: 0x0001675A
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00018562 File Offset: 0x00016762
		public bool EvaluateMessage { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001856B File Offset: 0x0001676B
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00018573 File Offset: 0x00016773
		public bool PopulatePropertiesXml { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001857C File Offset: 0x0001677C
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00018584 File Offset: 0x00016784
		public EventMatchingRule.CustomMatching OnMatching { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001858D File Offset: 0x0001678D
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00018595 File Offset: 0x00016795
		public EventMatchingRule.CustomNotification OnNotify { get; set; }

		// Token: 0x060003CA RID: 970 RVA: 0x000185A0 File Offset: 0x000167A0
		public EventMatchingRule(string logName, string providerName, IEnumerable<int> eventIds, int resourceNameIndex = -1, bool evaluateMessage = false, bool populatePropertiesXml = false, EventMatchingRule.CustomMatching onMatching = null, EventMatchingRule.CustomNotification onNotify = null)
		{
			this.LogName = logName;
			this.ProviderName = providerName;
			this.EventIds = eventIds;
			this.ResourceNameIndex = resourceNameIndex;
			this.EvaluateMessage = evaluateMessage;
			this.PopulatePropertiesXml = populatePropertiesXml;
			this.OnMatching = onMatching;
			this.OnNotify = onNotify;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000185FC File Offset: 0x000167FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("LognNme={0}", this.LogName);
			stringBuilder.AppendFormat("ProviderName={0}", this.ProviderName);
			StringBuilder stringBuilder2 = stringBuilder;
			string format = "EventIds={0}";
			object arg;
			if (this.EventIds == null)
			{
				arg = "NULL";
			}
			else
			{
				arg = string.Join(",", from id in this.EventIds
				select id.ToString());
			}
			stringBuilder2.AppendFormat(format, arg);
			stringBuilder.AppendFormat("ResourceNameIndex={0}", this.ResourceNameIndex);
			stringBuilder.AppendFormat("EvalMessage={0}", this.EvaluateMessage);
			stringBuilder.AppendFormat("PopulateProp={0}", this.PopulatePropertiesXml);
			stringBuilder.AppendFormat("OnMatching={0}", (this.OnMatching == null) ? "NULL" : "Defined");
			stringBuilder.AppendFormat("OnNotify={0}", (this.OnNotify == null) ? "NULL" : "Defined");
			return stringBuilder.ToString();
		}

		// Token: 0x040002B2 RID: 690
		public const int NoResourceName = -1;

		// Token: 0x040002B3 RID: 691
		public const string AllProviders = "*";

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x060003CE RID: 974
		public delegate bool CustomMatching(EventLogNotification.EventRecordInternal record);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x060003D2 RID: 978
		public delegate void CustomNotification(EventLogNotification.EventRecordInternal record, ref EventLogNotification.EventNotificationMetadata enm);
	}
}
