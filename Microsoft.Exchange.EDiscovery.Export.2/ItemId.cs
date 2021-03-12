using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000013 RID: 19
	internal class ItemId : BasicItemId
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002A5C File Offset: 0x00000C5C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002A64 File Offset: 0x00000C64
		public uint Size { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002A6D File Offset: 0x00000C6D
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002A75 File Offset: 0x00000C75
		public string ParentFolder { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002A7E File Offset: 0x00000C7E
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002A86 File Offset: 0x00000C86
		public string PrimaryItemId { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002A8F File Offset: 0x00000C8F
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002A97 File Offset: 0x00000C97
		public string UniqueHash { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002AA0 File Offset: 0x00000CA0
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public string InternetMessageId { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002AB1 File Offset: 0x00000CB1
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public string Subject { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002AC2 File Offset: 0x00000CC2
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002ACA File Offset: 0x00000CCA
		public string Sender { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002AD3 File Offset: 0x00000CD3
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002ADB File Offset: 0x00000CDB
		public string SenderSmtpAddress { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002AE4 File Offset: 0x00000CE4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002AEC File Offset: 0x00000CEC
		public DateTime SentTime { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002AF5 File Offset: 0x00000CF5
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002AFD File Offset: 0x00000CFD
		public DateTime ReceivedTime { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002B06 File Offset: 0x00000D06
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002B0E File Offset: 0x00000D0E
		public string BodyPreview { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002B17 File Offset: 0x00000D17
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002B1F File Offset: 0x00000D1F
		public string Importance { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002B28 File Offset: 0x00000D28
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002B30 File Offset: 0x00000D30
		public bool IsRead { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002B39 File Offset: 0x00000D39
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002B41 File Offset: 0x00000D41
		public bool HasAttachment { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002B4A File Offset: 0x00000D4A
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002B52 File Offset: 0x00000D52
		public string ToRecipients { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002B5B File Offset: 0x00000D5B
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002B63 File Offset: 0x00000D63
		public string CcRecipients { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002B6C File Offset: 0x00000D6C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002B74 File Offset: 0x00000D74
		public string BccRecipients { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002B7D File Offset: 0x00000D7D
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002B85 File Offset: 0x00000D85
		public int DocumentId { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002B8E File Offset: 0x00000D8E
		public string IdMarkerDocumentId
		{
			get
			{
				return string.Format("{0}{1}{2}", base.Id, "4887312c-8b40-4fec-a252-f2749065c0e5", this.DocumentId);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002BB0 File Offset: 0x00000DB0
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public string ToGroupExpansionRecipients { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002BC1 File Offset: 0x00000DC1
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002BC9 File Offset: 0x00000DC9
		public string CcGroupExpansionRecipients { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002BD2 File Offset: 0x00000DD2
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002BDA File Offset: 0x00000DDA
		public string BccGroupExpansionRecipients { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002BE3 File Offset: 0x00000DE3
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002BEB File Offset: 0x00000DEB
		public DistributionGroupExpansionError? DGGroupExpansionError { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002BF4 File Offset: 0x00000DF4
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002BFC File Offset: 0x00000DFC
		public bool NeedsDGExpansion { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002C05 File Offset: 0x00000E05
		public string ReportingPath
		{
			get
			{
				return base.SourceId + this.ParentFolder;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002C18 File Offset: 0x00000E18
		public bool IsDuplicate
		{
			get
			{
				return !string.IsNullOrEmpty(this.PrimaryItemId);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002C28 File Offset: 0x00000E28
		public virtual void WriteToStream(Stream fs)
		{
			ItemId.WriteString(base.Id, fs, Encoding.ASCII);
			ItemId.WriteString(this.ParentFolder, fs, Encoding.Unicode);
			ItemId.WriteString(this.PrimaryItemId, fs, Encoding.ASCII);
			ItemId.WriteString(this.Subject, fs, Encoding.Unicode);
			ItemId.WriteString(this.Sender, fs, Encoding.Unicode);
			fs.Write(BitConverter.GetBytes(this.SentTime.Ticks), 0, 8);
			fs.Write(BitConverter.GetBytes(this.ReceivedTime.Ticks), 0, 8);
			ItemId.WriteString(this.BodyPreview, fs, Encoding.Unicode);
			ItemId.WriteString(this.Importance, fs, Encoding.Unicode);
			fs.Write(BitConverter.GetBytes(this.IsRead), 0, 1);
			fs.Write(BitConverter.GetBytes(this.HasAttachment), 0, 1);
			ItemId.WriteString(this.ToRecipients, fs, Encoding.Unicode);
			ItemId.WriteString(this.CcRecipients, fs, Encoding.Unicode);
			ItemId.WriteString(this.BccRecipients, fs, Encoding.Unicode);
			ItemId.WriteString(this.ToGroupExpansionRecipients, fs, Encoding.Unicode);
			ItemId.WriteString(this.CcGroupExpansionRecipients, fs, Encoding.Unicode);
			ItemId.WriteString(this.BccGroupExpansionRecipients, fs, Encoding.Unicode);
			ItemId.WriteString((this.DGGroupExpansionError == null) ? null : this.DGGroupExpansionError.ToString(), fs, Encoding.Unicode);
			byte[] bytes = BitConverter.GetBytes(this.Size);
			fs.Write(bytes, 0, 4);
			if (!this.IsDuplicate)
			{
				ItemId.WriteString(this.UniqueHash, fs, Encoding.Unicode);
				string value = string.Empty;
				if (!string.IsNullOrEmpty(this.UniqueHash))
				{
					UniqueItemHash uniqueItemHash = UniqueItemHash.Parse(this.UniqueHash);
					value = uniqueItemHash.InternetMessageId;
				}
				ItemId.WriteString(value, fs, Encoding.Unicode);
			}
			byte[] bytes2 = BitConverter.GetBytes(this.DocumentId);
			fs.Write(bytes2, 0, 4);
			fs.Write(new byte[]
			{
				this.NeedsDGExpansion ? 1 : 0
			}, 0, 1);
			ItemId.WriteString(this.SenderSmtpAddress, fs, Encoding.Unicode);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002E50 File Offset: 0x00001050
		public virtual void ReadFromStream(Stream fs, string sourceId)
		{
			base.Id = ItemId.ReadString(fs, sourceId, Encoding.ASCII);
			this.ParentFolder = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.PrimaryItemId = ItemId.ReadString(fs, sourceId, Encoding.ASCII);
			this.Subject = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.Sender = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			byte[] array = new byte[8];
			ItemId.SafeRead(fs, array, 0, 8, sourceId);
			this.SentTime = new DateTime(BitConverter.ToInt64(array, 0), DateTimeKind.Utc);
			ItemId.SafeRead(fs, array, 0, 8, sourceId);
			this.ReceivedTime = new DateTime(BitConverter.ToInt64(array, 0), DateTimeKind.Utc);
			this.BodyPreview = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.Importance = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			ItemId.SafeRead(fs, array, 0, 1, sourceId);
			this.IsRead = BitConverter.ToBoolean(array, 0);
			ItemId.SafeRead(fs, array, 0, 1, sourceId);
			this.HasAttachment = BitConverter.ToBoolean(array, 0);
			this.ToRecipients = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.CcRecipients = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.BccRecipients = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.ToGroupExpansionRecipients = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.CcGroupExpansionRecipients = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.BccGroupExpansionRecipients = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			string value = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			this.DGGroupExpansionError = null;
			if (!string.IsNullOrEmpty(value))
			{
				this.DGGroupExpansionError = new DistributionGroupExpansionError?((DistributionGroupExpansionError)Enum.Parse(typeof(DistributionGroupExpansionError), value, true));
			}
			base.SourceId = sourceId;
			byte[] array2 = new byte[4];
			ItemId.SafeRead(fs, array2, 0, array2.Length, sourceId);
			this.Size = BitConverter.ToUInt32(array2, 0);
			if (!this.IsDuplicate)
			{
				this.UniqueHash = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
				if (fs.Position + 1L < fs.Length)
				{
					this.InternetMessageId = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
				}
			}
			if (fs.Position + 4L <= fs.Length)
			{
				byte[] array3 = new byte[4];
				ItemId.SafeRead(fs, array3, 0, array3.Length, sourceId);
				this.DocumentId = BitConverter.ToInt32(array3, 0);
			}
			if (fs.Position + 1L <= fs.Length)
			{
				byte[] array4 = new byte[1];
				ItemId.SafeRead(fs, array4, 0, array4.Length, sourceId);
				this.NeedsDGExpansion = (array4[0] != 0);
			}
			if (fs.Position + 1L < fs.Length)
			{
				this.SenderSmtpAddress = ItemId.ReadString(fs, sourceId, Encoding.Unicode);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000030EC File Offset: 0x000012EC
		protected static void WriteString(string value, Stream stream, Encoding encoding)
		{
			byte[] array = string.IsNullOrEmpty(value) ? new byte[0] : encoding.GetBytes(value);
			stream.Write(BitConverter.GetBytes(array.Length), 0, 4);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000312C File Offset: 0x0000132C
		protected static string ReadString(Stream stream, string sourceId, Encoding encoding)
		{
			byte[] array = new byte[4];
			stream.Read(array, 0, array.Length);
			int num = BitConverter.ToInt32(array, 0);
			if (num > 0)
			{
				byte[] array2 = new byte[num];
				ItemId.SafeRead(stream, array2, 0, num, sourceId);
				return encoding.GetString(array2);
			}
			return null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003174 File Offset: 0x00001374
		protected static void SafeRead(Stream stream, byte[] buffer, int offset, int count, string sourceId)
		{
			int num = stream.Read(buffer, offset, count);
			if (num != count)
			{
				throw new ExportException(ExportErrorType.ItemIdListCorrupted, string.Format(CultureInfo.CurrentCulture, "Item id list for source '{0}' is corrupted.", new object[]
				{
					sourceId
				}));
			}
		}

		// Token: 0x0400003B RID: 59
		public const string DocumentIdMarker = "4887312c-8b40-4fec-a252-f2749065c0e5";
	}
}
