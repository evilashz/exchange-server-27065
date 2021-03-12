using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class ComplianceJobId : ObjectId
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00006112 File Offset: 0x00004312
		public ComplianceJobId()
		{
			this.complianceJobId = Guid.NewGuid();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006125 File Offset: 0x00004325
		public ComplianceJobId(ComplianceJobId id)
		{
			this.complianceJobId = id.complianceJobId;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006139 File Offset: 0x00004339
		public ComplianceJobId(Guid guid)
		{
			this.complianceJobId = guid;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006148 File Offset: 0x00004348
		public ComplianceJobId(byte[] bytes) : this(new Guid(bytes))
		{
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00006156 File Offset: 0x00004356
		public Guid Guid
		{
			get
			{
				return this.complianceJobId;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006160 File Offset: 0x00004360
		public override byte[] GetBytes()
		{
			return this.complianceJobId.ToByteArray();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000617C File Offset: 0x0000437C
		public override string ToString()
		{
			return this.complianceJobId.ToString();
		}

		// Token: 0x04000092 RID: 146
		private readonly Guid complianceJobId;
	}
}
