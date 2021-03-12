using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F9 RID: 761
	internal struct LocaleInfo
	{
		// Token: 0x0600119F RID: 4511 RVA: 0x000307F3 File Offset: 0x0002E9F3
		public LocaleInfo(int stringLocaleId, int sortLocaleId, int codePageId)
		{
			this.stringLocaleId = stringLocaleId;
			this.sortLocaleId = sortLocaleId;
			this.codePageId = codePageId;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0003080A File Offset: 0x0002EA0A
		public int StringLocaleId
		{
			get
			{
				return this.stringLocaleId;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00030812 File Offset: 0x0002EA12
		public int SortLocaleId
		{
			get
			{
				return this.sortLocaleId;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0003081A File Offset: 0x0002EA1A
		public int CodePageId
		{
			get
			{
				return this.codePageId;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00030824 File Offset: 0x0002EA24
		public static LocaleInfo Parse(Reader reader)
		{
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			int num3 = reader.ReadInt32();
			return new LocaleInfo(num, num2, num3);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003084E File Offset: 0x0002EA4E
		public override string ToString()
		{
			return "LocaleInfo: " + this.ToBareString();
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00030860 File Offset: 0x0002EA60
		public string ToBareString()
		{
			return string.Format("String[{0}] Sort[{1}] CodePage[{2}]", this.sortLocaleId, this.stringLocaleId, this.codePageId);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003088D File Offset: 0x0002EA8D
		internal void Serialize(Writer writer)
		{
			writer.WriteInt32(this.stringLocaleId);
			writer.WriteInt32(this.sortLocaleId);
			writer.WriteInt32(this.codePageId);
		}

		// Token: 0x04000993 RID: 2451
		private readonly int stringLocaleId;

		// Token: 0x04000994 RID: 2452
		private readonly int sortLocaleId;

		// Token: 0x04000995 RID: 2453
		private readonly int codePageId;
	}
}
