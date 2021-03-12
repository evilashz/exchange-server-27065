using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ComplianceSearchCondition
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00007494 File Offset: 0x00005694
		static ComplianceSearchCondition()
		{
			ComplianceSearchCondition.description.ComplianceStructureId = 8;
			ComplianceSearchCondition.description.RegisterShortPropertyGetterAndSetter(0, (ComplianceSearchCondition item) => (short)item.Name, delegate(ComplianceSearchCondition item, short value)
			{
				item.Name = (ComplianceSearchCondition.ConditionName)value;
			});
			ComplianceSearchCondition.description.RegisterStringPropertyGetterAndSetter(0, (ComplianceSearchCondition item) => item.Content, delegate(ComplianceSearchCondition item, string value)
			{
				item.Content = value;
			});
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007540 File Offset: 0x00005740
		public ComplianceSearchCondition()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007548 File Offset: 0x00005748
		public ComplianceSearchCondition(ComplianceSearchCondition.ConditionName name, string content)
		{
			this.Name = name;
			this.Content = content;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000755E File Offset: 0x0000575E
		public static ComplianceSerializationDescription<ComplianceSearchCondition> Description
		{
			get
			{
				return ComplianceSearchCondition.description;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00007565 File Offset: 0x00005765
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000756D File Offset: 0x0000576D
		public ComplianceSearchCondition.ConditionName Name { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007576 File Offset: 0x00005776
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000757E File Offset: 0x0000577E
		public string Content { get; set; }

		// Token: 0x0600012F RID: 303 RVA: 0x00007587 File Offset: 0x00005787
		internal byte[] ToBlob()
		{
			return ComplianceSerializer.Serialize<ComplianceSearchCondition>(ComplianceSearchCondition.Description, this);
		}

		// Token: 0x040000A8 RID: 168
		private static ComplianceSerializationDescription<ComplianceSearchCondition> description = new ComplianceSerializationDescription<ComplianceSearchCondition>();

		// Token: 0x0200002D RID: 45
		internal enum ConditionName : short
		{
			// Token: 0x040000B0 RID: 176
			UnknownCondition,
			// Token: 0x040000B1 RID: 177
			StartDate,
			// Token: 0x040000B2 RID: 178
			EndDate
		}
	}
}
