using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000005 RID: 5
	public class FqlQuery
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003605 File Offset: 0x00001805
		public FqlQuery()
		{
			this.valueBuilder = new StringBuilder();
			this.hashedValueBuilder = new StringBuilder();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003623 File Offset: 0x00001823
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000362B File Offset: 0x0000182B
		public string TermLength { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003634 File Offset: 0x00001834
		public string Value
		{
			get
			{
				return this.valueBuilder.ToString();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003641 File Offset: 0x00001841
		public string ScrubbedValue
		{
			get
			{
				return this.hashedValueBuilder.ToString();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000364E File Offset: 0x0000184E
		public override string ToString()
		{
			return this.valueBuilder.ToString();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000365B File Offset: 0x0000185B
		public void Append(FqlQuery fqlString)
		{
			this.valueBuilder.Append(fqlString.valueBuilder);
			this.hashedValueBuilder.Append(fqlString.hashedValueBuilder);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003681 File Offset: 0x00001881
		public void Append(string value)
		{
			this.valueBuilder.Append(value);
			this.hashedValueBuilder.Append(value);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000369D File Offset: 0x0000189D
		public void Append(char value)
		{
			this.valueBuilder.Append(value);
			this.hashedValueBuilder.Append(value);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000036B9 File Offset: 0x000018B9
		public void AppendValue(string value, string valueReplacement)
		{
			this.valueBuilder.Append(value);
			this.hashedValueBuilder.Append(valueReplacement);
		}

		// Token: 0x04000012 RID: 18
		private StringBuilder valueBuilder;

		// Token: 0x04000013 RID: 19
		private StringBuilder hashedValueBuilder;
	}
}
