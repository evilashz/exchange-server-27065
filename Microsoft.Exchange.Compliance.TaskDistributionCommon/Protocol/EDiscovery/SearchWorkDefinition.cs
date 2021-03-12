using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol.EDiscovery
{
	// Token: 0x0200004A RID: 74
	public class SearchWorkDefinition : WorkDefinition
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000AA34 File Offset: 0x00008C34
		static SearchWorkDefinition()
		{
			SearchWorkDefinition.description.ComplianceStructureId = 99;
			SearchWorkDefinition.description.RegisterStringPropertyGetterAndSetter(0, (SearchWorkDefinition item) => item.Query, delegate(SearchWorkDefinition item, string value)
			{
				item.Query = value;
			});
			SearchWorkDefinition.description.RegisterIntegerPropertyGetterAndSetter(0, (SearchWorkDefinition item) => (int)item.Parser, delegate(SearchWorkDefinition item, int value)
			{
				item.Parser = (SearchWorkDefinition.QueryParser)value;
			});
			SearchWorkDefinition.description.RegisterIntegerPropertyGetterAndSetter(1, (SearchWorkDefinition item) => item.DetailCount, delegate(SearchWorkDefinition item, int value)
			{
				item.DetailCount = value;
			});
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000AB26 File Offset: 0x00008D26
		public static ComplianceSerializationDescription<SearchWorkDefinition> Description
		{
			get
			{
				return SearchWorkDefinition.description;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000AB2D File Offset: 0x00008D2D
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000AB35 File Offset: 0x00008D35
		public string Query { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000AB3E File Offset: 0x00008D3E
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000AB46 File Offset: 0x00008D46
		public SearchWorkDefinition.QueryParser Parser { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000AB4F File Offset: 0x00008D4F
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000AB57 File Offset: 0x00008D57
		public int DetailCount { get; set; }

		// Token: 0x04000153 RID: 339
		private static ComplianceSerializationDescription<SearchWorkDefinition> description = new ComplianceSerializationDescription<SearchWorkDefinition>();

		// Token: 0x0200004B RID: 75
		public enum QueryParser
		{
			// Token: 0x0400015E RID: 350
			KQL,
			// Token: 0x0400015F RID: 351
			AQS,
			// Token: 0x04000160 RID: 352
			FQL
		}
	}
}
