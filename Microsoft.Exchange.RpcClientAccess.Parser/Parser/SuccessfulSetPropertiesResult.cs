using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200036A RID: 874
	internal sealed class SuccessfulSetPropertiesResult : RopResult
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x000376E4 File Offset: 0x000358E4
		internal SuccessfulSetPropertiesResult(RopId ropId, PropertyProblem[] propertyProblems) : base(ropId, ErrorCode.None, null)
		{
			if (ropId != RopId.SetProperties && ropId != RopId.SetPropertiesNoReplicate)
			{
				throw new ArgumentException("SetProperties and SetPropertiesNoReplicate are the only valid rop types.");
			}
			Util.ThrowOnNullArgument(propertyProblems, "propertyProblems");
			this.propertyProblems = propertyProblems;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00037716 File Offset: 0x00035916
		internal SuccessfulSetPropertiesResult(Reader reader) : base(reader)
		{
			this.propertyProblems = reader.ReadSizeAndPropertyProblemArray();
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0003772B File Offset: 0x0003592B
		public PropertyProblem[] PropertyProblems
		{
			get
			{
				return this.propertyProblems;
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00037733 File Offset: 0x00035933
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulSetPropertiesResult(reader);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0003773B File Offset: 0x0003593B
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedPropertyProblems(this.propertyProblems);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00037750 File Offset: 0x00035950
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Problems=[");
			Util.AppendToString<PropertyProblem>(stringBuilder, this.propertyProblems);
			stringBuilder.Append("]");
		}

		// Token: 0x04000B30 RID: 2864
		private readonly PropertyProblem[] propertyProblems;
	}
}
