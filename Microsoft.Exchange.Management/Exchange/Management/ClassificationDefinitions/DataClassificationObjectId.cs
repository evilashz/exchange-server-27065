using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000832 RID: 2098
	[Serializable]
	public sealed class DataClassificationObjectId : ObjectId
	{
		// Token: 0x060048AA RID: 18602 RVA: 0x0012A46B File Offset: 0x0012866B
		internal DataClassificationObjectId(string ruleId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ruleId", ruleId);
			this.ruleId = ruleId;
			this.organizationHierarchy = null;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x0012A48C File Offset: 0x0012868C
		internal DataClassificationObjectId(string organizationHierarchy, string ruleId) : this(ruleId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("organizationHierarchy", organizationHierarchy);
			this.organizationHierarchy = organizationHierarchy;
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x0012A4A7 File Offset: 0x001286A7
		public string Name
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x0012A4AF File Offset: 0x001286AF
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.organizationHierarchy))
			{
				return ClassificationDefinitionUtils.CreateHierarchicalIdentityString(this.organizationHierarchy, this.ruleId);
			}
			return this.ruleId;
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0012A4D6 File Offset: 0x001286D6
		public static implicit operator string(DataClassificationObjectId dataClassificationObjectId)
		{
			return dataClassificationObjectId.ToString();
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0012A4DE File Offset: 0x001286DE
		public static implicit operator DataClassificationIdParameter(DataClassificationObjectId dataClassificationObjectId)
		{
			return DataClassificationIdParameter.Parse(dataClassificationObjectId.ToString());
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0012A4EB File Offset: 0x001286EB
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this);
		}

		// Token: 0x04002C11 RID: 11281
		private readonly string organizationHierarchy;

		// Token: 0x04002C12 RID: 11282
		private readonly string ruleId;
	}
}
