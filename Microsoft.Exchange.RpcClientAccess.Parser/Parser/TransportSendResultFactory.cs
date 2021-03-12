using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000145 RID: 325
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransportSendResultFactory : StandardResultFactory
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001107E File Offset: 0x0000F27E
		internal TransportSendResultFactory(int maxSize, Encoding string8Encoding) : base(RopId.TransportSend)
		{
			this.maxSize = maxSize;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00011098 File Offset: 0x0000F298
		public RopResult CreateSuccessfulResult(PropertyValue[] propertyValues)
		{
			using (CountWriter countWriter = new CountWriter())
			{
				countWriter.WriteBool(false);
				countWriter.WriteCountAndPropertyValueList(propertyValues, this.string8Encoding, WireFormatStyle.Rop);
				if (countWriter.Position > (long)this.maxSize)
				{
					propertyValues = null;
				}
			}
			return new SuccessfulTransportSendResult(propertyValues);
		}

		// Token: 0x04000328 RID: 808
		private readonly int maxSize;

		// Token: 0x04000329 RID: 809
		private readonly Encoding string8Encoding;
	}
}
