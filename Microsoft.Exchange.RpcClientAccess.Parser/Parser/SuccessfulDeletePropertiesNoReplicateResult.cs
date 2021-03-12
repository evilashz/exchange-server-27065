using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000237 RID: 567
	internal sealed class SuccessfulDeletePropertiesNoReplicateResult : RopResult
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x000273FE File Offset: 0x000255FE
		internal SuccessfulDeletePropertiesNoReplicateResult(PropertyProblem[] propertyProblems) : base(RopId.DeletePropertiesNoReplicate, ErrorCode.None, null)
		{
			this.propertyProblems = propertyProblems;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00027411 File Offset: 0x00025611
		internal SuccessfulDeletePropertiesNoReplicateResult(Reader reader) : base(reader)
		{
			this.propertyProblems = reader.ReadSizeAndPropertyProblemArray();
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00027426 File Offset: 0x00025626
		internal static SuccessfulDeletePropertiesNoReplicateResult Parse(Reader reader)
		{
			return new SuccessfulDeletePropertiesNoReplicateResult(reader);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002742E File Offset: 0x0002562E
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyProblems(this.propertyProblems);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00027443 File Offset: 0x00025643
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

		// Token: 0x040006CE RID: 1742
		private readonly PropertyProblem[] propertyProblems;
	}
}
