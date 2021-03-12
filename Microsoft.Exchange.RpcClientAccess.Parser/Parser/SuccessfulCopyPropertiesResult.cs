using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000229 RID: 553
	internal sealed class SuccessfulCopyPropertiesResult : RopResult
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00026AAD File Offset: 0x00024CAD
		internal PropertyProblem[] PropertyProblems
		{
			get
			{
				return this.propertyProblems;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00026AB5 File Offset: 0x00024CB5
		internal SuccessfulCopyPropertiesResult(PropertyProblem[] propertyProblems) : base(RopId.CopyProperties, ErrorCode.None, null)
		{
			this.propertyProblems = propertyProblems;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00026AC8 File Offset: 0x00024CC8
		internal SuccessfulCopyPropertiesResult(Reader reader) : base(reader)
		{
			this.propertyProblems = reader.ReadSizeAndPropertyProblemArray();
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00026ADD File Offset: 0x00024CDD
		internal static SuccessfulCopyPropertiesResult Parse(Reader reader)
		{
			return new SuccessfulCopyPropertiesResult(reader);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00026AE5 File Offset: 0x00024CE5
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyProblems(this.propertyProblems);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00026AFA File Offset: 0x00024CFA
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

		// Token: 0x040006BB RID: 1723
		private readonly PropertyProblem[] propertyProblems;
	}
}
