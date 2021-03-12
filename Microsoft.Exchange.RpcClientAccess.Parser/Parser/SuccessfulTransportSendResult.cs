using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000373 RID: 883
	internal sealed class SuccessfulTransportSendResult : RopResult
	{
		// Token: 0x0600158A RID: 5514 RVA: 0x00037AE5 File Offset: 0x00035CE5
		internal SuccessfulTransportSendResult(PropertyValue[] propertyValues) : base(RopId.TransportSend, ErrorCode.None, null)
		{
			this.propertyValues = propertyValues;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00037AF8 File Offset: 0x00035CF8
		internal SuccessfulTransportSendResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			if (!reader.ReadBool())
			{
				this.propertyValues = reader.ReadCountAndPropertyValueList(WireFormatStyle.Rop);
				foreach (PropertyValue propertyValue in this.propertyValues)
				{
					propertyValue.ResolveString8Values(string8Encoding);
				}
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00037B4D File Offset: 0x00035D4D
		internal static SuccessfulTransportSendResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulTransportSendResult(reader, string8Encoding);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00037B58 File Offset: 0x00035D58
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			long position = writer.Position;
			bool flag = this.propertyValues == null || this.propertyValues.Length == 0;
			writer.WriteBool(flag);
			if (!flag)
			{
				writer.WriteCountAndPropertyValueList(this.propertyValues, base.String8Encoding, WireFormatStyle.Rop);
			}
		}

		// Token: 0x04000B38 RID: 2872
		private readonly PropertyValue[] propertyValues;
	}
}
