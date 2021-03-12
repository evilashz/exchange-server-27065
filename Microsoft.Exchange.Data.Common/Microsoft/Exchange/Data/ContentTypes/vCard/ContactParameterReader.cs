using System;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.vCard
{
	// Token: 0x020000C2 RID: 194
	public struct ContactParameterReader
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x0002A861 File Offset: 0x00028A61
		internal ContactParameterReader(ContentLineReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0002A86A File Offset: 0x00028A6A
		public ParameterId ParameterId
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter);
				return ContactCommon.GetParameterEnum(this.reader.ParameterName);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0002A888 File Offset: 0x00028A88
		public string Name
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter);
				return this.reader.ParameterName;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0002A8A1 File Offset: 0x00028AA1
		public string ReadValue()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter);
			return this.reader.ReadParameterValue(true);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002A8BB File Offset: 0x00028ABB
		public bool ReadNextValue()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextParameterValue();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002A8D5 File Offset: 0x00028AD5
		public bool ReadNextParameter()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextParameter();
		}

		// Token: 0x04000672 RID: 1650
		private ContentLineReader reader;
	}
}
