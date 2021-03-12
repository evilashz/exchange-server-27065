using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200022D RID: 557
	internal sealed class SuccessfulCopyToResult : RopResult
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00026C93 File Offset: 0x00024E93
		internal PropertyProblem[] PropertyProblems
		{
			get
			{
				return this.propertyProblems;
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00026C9B File Offset: 0x00024E9B
		internal SuccessfulCopyToResult(PropertyProblem[] propertyProblems) : base(RopId.CopyTo, ErrorCode.None, null)
		{
			this.propertyProblems = propertyProblems;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00026CAE File Offset: 0x00024EAE
		internal SuccessfulCopyToResult(Reader reader) : base(reader)
		{
			this.propertyProblems = reader.ReadSizeAndPropertyProblemArray();
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00026CC3 File Offset: 0x00024EC3
		internal static SuccessfulCopyToResult Parse(Reader reader)
		{
			return new SuccessfulCopyToResult(reader);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00026CCB File Offset: 0x00024ECB
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyProblems(this.propertyProblems);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00026CE0 File Offset: 0x00024EE0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			if (this.propertyProblems != null && this.propertyProblems.Length > 0)
			{
				stringBuilder.Append(" Problems=[");
				Util.AppendToString<PropertyProblem>(stringBuilder, this.propertyProblems);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x040006BF RID: 1727
		private readonly PropertyProblem[] propertyProblems;
	}
}
