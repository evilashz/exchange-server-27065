using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E7 RID: 231
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPropertiesAllResultFactory : StandardResultFactory
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000F295 File Offset: 0x0000D495
		internal GetPropertiesAllResultFactory(int maxSize, Encoding string8Encoding) : base(RopId.GetPropertiesAll)
		{
			this.maxSize = maxSize;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		public RopResult CreateSuccessfulResult(PropertyValue[] propertyValues)
		{
			SuccessfulGetPropertiesAllResult successfulGetPropertiesAllResult = new SuccessfulGetPropertiesAllResult(propertyValues);
			successfulGetPropertiesAllResult.String8Encoding = this.string8Encoding;
			using (CountWriter countWriter = new CountWriter())
			{
				do
				{
					countWriter.Position = 0L;
					successfulGetPropertiesAllResult.Serialize(countWriter);
					if (countWriter.Position <= (long)this.maxSize)
					{
						goto IL_45;
					}
				}
				while (successfulGetPropertiesAllResult.RemoveLargestProperty());
				throw new BufferOutOfMemoryException();
				IL_45:;
			}
			return successfulGetPropertiesAllResult;
		}

		// Token: 0x040002ED RID: 749
		private readonly int maxSize;

		// Token: 0x040002EE RID: 750
		private readonly Encoding string8Encoding;
	}
}
