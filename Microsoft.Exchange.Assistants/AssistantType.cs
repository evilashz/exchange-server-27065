using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000022 RID: 34
	internal sealed class AssistantType : Base, IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000596D File Offset: 0x00003B6D
		public AssistantType(IEventBasedAssistantType baseType, PerformanceCountersPerAssistantInstance performanceCounters)
		{
			this.baseType = baseType;
			this.PerformanceCounters = performanceCounters;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005983 File Offset: 0x00003B83
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000598B File Offset: 0x00003B8B
		public PerformanceCountersPerAssistantInstance PerformanceCounters { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005994 File Offset: 0x00003B94
		public LocalizedString Name
		{
			get
			{
				return this.baseType.Name;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000059A1 File Offset: 0x00003BA1
		public string NonLocalizedName
		{
			get
			{
				return this.baseType.NonLocalizedName;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000059AE File Offset: 0x00003BAE
		public IMailboxFilter MailboxFilter
		{
			get
			{
				return this.baseType as IMailboxFilter;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000059BB File Offset: 0x00003BBB
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return this.baseType.EventMask;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000059C8 File Offset: 0x00003BC8
		public bool NeedsMailboxSession
		{
			get
			{
				return this.baseType.NeedsMailboxSession;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000059D5 File Offset: 0x00003BD5
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return this.baseType.PreloadItemProperties;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000059E2 File Offset: 0x00003BE2
		public bool ProcessesPublicDatabases
		{
			get
			{
				return this.baseType.ProcessesPublicDatabases;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000059EF File Offset: 0x00003BEF
		public Guid Identity
		{
			get
			{
				return this.baseType.Identity;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000059FC File Offset: 0x00003BFC
		public static AssistantType[] CreateArray(IEventBasedAssistantType[] eventBasedAssistantTypeArray, PerformanceCountersPerAssistantInstance performanceCountersPerAssistantsTotal)
		{
			AssistantType[] array = new AssistantType[eventBasedAssistantTypeArray.Length];
			int num = 0;
			foreach (IEventBasedAssistantType eventBasedAssistantType in eventBasedAssistantTypeArray)
			{
				string text = eventBasedAssistantType.GetType().Name;
				if (text.EndsWith("Type"))
				{
					text = text.Substring(0, text.Length - 4);
				}
				PerformanceCountersPerAssistantInstance performanceCounters = new PerformanceCountersPerAssistantInstance(text, performanceCountersPerAssistantsTotal);
				array[num++] = new AssistantType(eventBasedAssistantType, performanceCounters);
			}
			return array;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A72 File Offset: 0x00003C72
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return this.baseType.CreateInstance(databaseInfo);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005A80 File Offset: 0x00003C80
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableEventBasedAssistantType queryableEventBasedAssistantType = queryableObject as QueryableEventBasedAssistantType;
			if (queryableEventBasedAssistantType != null)
			{
				queryableEventBasedAssistantType.AssistantGuid = this.Identity;
				queryableEventBasedAssistantType.AssistantName = this.NonLocalizedName;
				if (this.MailboxFilter != null)
				{
					queryableEventBasedAssistantType.MailboxType = this.MailboxFilter.MailboxType.ToString();
				}
				queryableEventBasedAssistantType.MapiEventType = this.EventMask.ToString();
				queryableEventBasedAssistantType.NeedMailboxSession = this.NeedsMailboxSession;
			}
		}

		// Token: 0x040000F6 RID: 246
		private IEventBasedAssistantType baseType;
	}
}
