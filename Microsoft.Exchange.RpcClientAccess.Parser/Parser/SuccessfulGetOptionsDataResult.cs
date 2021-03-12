using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000257 RID: 599
	internal sealed class SuccessfulGetOptionsDataResult : RopResult
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x00028251 File Offset: 0x00026451
		internal SuccessfulGetOptionsDataResult(byte[] optionsInfo, byte[] helpFileData, string helpFileName) : base(RopId.GetOptionsData, ErrorCode.None, null)
		{
			if (optionsInfo == null)
			{
				throw new ArgumentNullException("optionsInfo");
			}
			if (helpFileData == null)
			{
				throw new ArgumentNullException("helpFileData");
			}
			this.optionsInfo = optionsInfo;
			this.helpFileData = helpFileData;
			this.helpFileName = helpFileName;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00028290 File Offset: 0x00026490
		internal SuccessfulGetOptionsDataResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			reader.ReadByte();
			this.optionsInfo = reader.ReadSizeAndByteArray();
			this.helpFileData = reader.ReadSizeAndByteArray();
			if (this.helpFileData.Length > 0)
			{
				this.helpFileName = reader.ReadString8(string8Encoding, StringFlags.IncludeNull);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000282DC File Offset: 0x000264DC
		internal byte[] OptionsInfo
		{
			get
			{
				return this.optionsInfo;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000282E4 File Offset: 0x000264E4
		internal byte[] HelpFileData
		{
			get
			{
				return this.helpFileData;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000282EC File Offset: 0x000264EC
		internal string HelpFileName
		{
			get
			{
				return this.helpFileName;
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000282F4 File Offset: 0x000264F4
		internal static SuccessfulGetOptionsDataResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulGetOptionsDataResult(reader, string8Encoding);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00028300 File Offset: 0x00026500
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(1);
			writer.WriteSizedBytes(this.optionsInfo);
			writer.WriteSizedBytes(this.helpFileData);
			if (!string.IsNullOrEmpty(this.helpFileName))
			{
				writer.WriteString8(this.helpFileName, base.String8Encoding, StringFlags.IncludeNull);
			}
		}

		// Token: 0x040006F8 RID: 1784
		private byte[] optionsInfo;

		// Token: 0x040006F9 RID: 1785
		private byte[] helpFileData;

		// Token: 0x040006FA RID: 1786
		private string helpFileName;
	}
}
