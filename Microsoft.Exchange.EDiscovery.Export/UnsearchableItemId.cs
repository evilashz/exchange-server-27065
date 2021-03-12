using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200002A RID: 42
	internal class UnsearchableItemId : ItemId
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005A69 File Offset: 0x00003C69
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005A71 File Offset: 0x00003C71
		public string ErrorCode { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005A7A File Offset: 0x00003C7A
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005A82 File Offset: 0x00003C82
		public string ErrorDescription { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005A8B File Offset: 0x00003C8B
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005A93 File Offset: 0x00003C93
		public DateTime LastAttemptTime { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005A9C File Offset: 0x00003C9C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public string AdditionalInformation { get; set; }

		// Token: 0x0600017B RID: 379 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public static void WriteDummyToStream(Stream fs)
		{
			ItemId.WriteString(string.Empty, fs, Encoding.ASCII);
			ItemId.WriteString(string.Empty, fs, Encoding.Unicode);
			fs.Write(BitConverter.GetBytes(DateTime.MinValue.Ticks), 0, 8);
			ItemId.WriteString(string.Empty, fs, Encoding.Unicode);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005B08 File Offset: 0x00003D08
		public override void WriteToStream(Stream fs)
		{
			base.WriteToStream(fs);
			ItemId.WriteString(this.ErrorCode, fs, Encoding.ASCII);
			ItemId.WriteString(this.ErrorDescription, fs, Encoding.Unicode);
			fs.Write(BitConverter.GetBytes(this.LastAttemptTime.Ticks), 0, 8);
			ItemId.WriteString(this.AdditionalInformation, fs, Encoding.Unicode);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005B6C File Offset: 0x00003D6C
		public override void ReadFromStream(Stream fs, string sourceId)
		{
			base.ReadFromStream(fs, sourceId);
			this.ErrorCode = ItemId.ReadString(fs, sourceId, Encoding.ASCII);
			this.ErrorDescription = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			byte[] array = new byte[8];
			ItemId.SafeRead(fs, array, 0, 8, sourceId);
			this.LastAttemptTime = new DateTime(BitConverter.ToInt64(array, 0), DateTimeKind.Utc);
			this.AdditionalInformation = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
		}
	}
}
