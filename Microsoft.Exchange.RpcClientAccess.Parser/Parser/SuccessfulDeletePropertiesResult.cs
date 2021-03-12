using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000238 RID: 568
	internal sealed class SuccessfulDeletePropertiesResult : RopResult
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00027483 File Offset: 0x00025683
		internal SuccessfulDeletePropertiesResult(PropertyProblem[] propertyProblems) : base(RopId.DeleteProperties, ErrorCode.None, null)
		{
			this.propertyProblems = propertyProblems;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00027496 File Offset: 0x00025696
		internal SuccessfulDeletePropertiesResult(Reader reader) : base(reader)
		{
			this.propertyProblems = reader.ReadSizeAndPropertyProblemArray();
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000274AB File Offset: 0x000256AB
		public PropertyProblem[] PropertyProblems
		{
			get
			{
				return this.propertyProblems;
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000274B3 File Offset: 0x000256B3
		internal static SuccessfulDeletePropertiesResult Parse(Reader reader)
		{
			return new SuccessfulDeletePropertiesResult(reader);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000274BB File Offset: 0x000256BB
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyProblems(this.propertyProblems);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000274D0 File Offset: 0x000256D0
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

		// Token: 0x040006CF RID: 1743
		private readonly PropertyProblem[] propertyProblems;
	}
}
