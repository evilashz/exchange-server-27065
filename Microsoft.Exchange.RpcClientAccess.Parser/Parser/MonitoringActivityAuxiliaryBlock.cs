using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200001E RID: 30
	internal sealed class MonitoringActivityAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003A75 File Offset: 0x00001C75
		public MonitoringActivityAuxiliaryBlock(string activityContent) : base(1, AuxiliaryBlockTypes.MonitoringActivity)
		{
			this.activityContent = activityContent;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A87 File Offset: 0x00001C87
		internal MonitoringActivityAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.activityContent = reader.ReadString8(MonitoringActivityAuxiliaryBlock.traceEncoding, StringFlags.IncludeNull);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003AA2 File Offset: 0x00001CA2
		public string ActivityContent
		{
			get
			{
				return this.activityContent;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003AAA File Offset: 0x00001CAA
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteString8(this.activityContent, MonitoringActivityAuxiliaryBlock.traceEncoding, StringFlags.IncludeNull);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003AC8 File Offset: 0x00001CC8
		protected override int Truncate(int maxSerializedSize, int currentSize)
		{
			byte[] bytes = MonitoringActivityAuxiliaryBlock.traceEncoding.GetBytes(this.ActivityContent);
			if (currentSize > maxSerializedSize && currentSize - maxSerializedSize < bytes.Length)
			{
				this.activityContent = MonitoringActivityAuxiliaryBlock.traceEncoding.GetString(bytes, 0, maxSerializedSize - (currentSize - bytes.Length));
				return maxSerializedSize;
			}
			return currentSize;
		}

		// Token: 0x04000086 RID: 134
		private static readonly Encoding traceEncoding = Encoding.UTF8;

		// Token: 0x04000087 RID: 135
		private string activityContent;
	}
}
