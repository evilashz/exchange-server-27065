using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200022B RID: 555
	internal sealed class SuccessfulCopyToExtendedResult : RopResult
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x00026BA1 File Offset: 0x00024DA1
		internal SuccessfulCopyToExtendedResult(PropertyProblem[] propertyProblems) : base(RopId.CopyToExtended, ErrorCode.None, null)
		{
			this.propertyProblems = propertyProblems;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00026BB7 File Offset: 0x00024DB7
		internal SuccessfulCopyToExtendedResult(Reader reader) : base(reader)
		{
			this.propertyProblems = reader.ReadSizeAndPropertyProblemArray();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00026BCC File Offset: 0x00024DCC
		internal static SuccessfulCopyToExtendedResult Parse(Reader reader)
		{
			return new SuccessfulCopyToExtendedResult(reader);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00026BD4 File Offset: 0x00024DD4
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyProblems(this.propertyProblems);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00026BE9 File Offset: 0x00024DE9
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

		// Token: 0x040006BD RID: 1725
		private readonly PropertyProblem[] propertyProblems;
	}
}
