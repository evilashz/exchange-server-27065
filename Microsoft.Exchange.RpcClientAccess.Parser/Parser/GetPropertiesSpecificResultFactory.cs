using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E9 RID: 233
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPropertiesSpecificResultFactory : StandardResultFactory
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0000F32E File Offset: 0x0000D52E
		internal GetPropertiesSpecificResultFactory(int maxSize, Encoding string8Encoding) : base(RopId.GetPropertiesSpecific)
		{
			this.maxSize = maxSize;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000F348 File Offset: 0x0000D548
		public RopResult CreateSuccessfulResult(PropertyTag[] columns, PropertyValue[] propertyValues)
		{
			SuccessfulGetPropertiesSpecificResult successfulGetPropertiesSpecificResult = new SuccessfulGetPropertiesSpecificResult(columns, propertyValues);
			successfulGetPropertiesSpecificResult.String8Encoding = this.string8Encoding;
			using (CountWriter countWriter = new CountWriter())
			{
				do
				{
					countWriter.Position = 0L;
					successfulGetPropertiesSpecificResult.Serialize(countWriter);
					if (countWriter.Position <= (long)this.maxSize)
					{
						goto IL_46;
					}
				}
				while (successfulGetPropertiesSpecificResult.RemoveLargestProperty());
				throw new BufferOutOfMemoryException();
				IL_46:;
			}
			return successfulGetPropertiesSpecificResult;
		}

		// Token: 0x040002EF RID: 751
		private readonly int maxSize;

		// Token: 0x040002F0 RID: 752
		private readonly Encoding string8Encoding;
	}
}
