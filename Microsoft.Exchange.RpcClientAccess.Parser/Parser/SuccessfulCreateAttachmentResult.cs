using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000230 RID: 560
	internal sealed class SuccessfulCreateAttachmentResult : RopResult
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00026E3D File Offset: 0x0002503D
		internal uint AttachmentNumber
		{
			get
			{
				return this.attachmentNumber;
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00026E45 File Offset: 0x00025045
		internal SuccessfulCreateAttachmentResult(IServerObject serverObject, uint attachmentNumber) : base(RopId.CreateAttachment, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.attachmentNumber = attachmentNumber;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00026E66 File Offset: 0x00025066
		internal SuccessfulCreateAttachmentResult(Reader reader) : base(reader)
		{
			this.attachmentNumber = reader.ReadUInt32();
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00026E7B File Offset: 0x0002507B
		internal static SuccessfulCreateAttachmentResult Parse(Reader reader)
		{
			return new SuccessfulCreateAttachmentResult(reader);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00026E83 File Offset: 0x00025083
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.attachmentNumber);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00026E98 File Offset: 0x00025098
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Number=").Append(this.AttachmentNumber);
		}

		// Token: 0x040006C4 RID: 1732
		private readonly uint attachmentNumber;
	}
}
