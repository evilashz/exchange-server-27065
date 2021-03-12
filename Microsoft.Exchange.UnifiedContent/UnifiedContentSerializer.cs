using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x0200000E RID: 14
	public class UnifiedContentSerializer
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003BC0 File Offset: 0x00001DC0
		internal UnifiedContentSerializer(Stream stream, SharedStream sharedStream, List<IExtractedContent> contentList = null)
		{
			this.sharedStream = sharedStream;
			this.outputStream = stream;
			this.contentList = (contentList ?? new List<IExtractedContent>());
			using (SharedContentWriter sharedContentWriter = new SharedContentWriter(stream))
			{
				this.WriteHeader(sharedContentWriter, sharedStream.SharedName);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003C38 File Offset: 0x00001E38
		internal List<IExtractedContent> ContentCollection
		{
			get
			{
				return this.contentList;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003C40 File Offset: 0x00001E40
		internal List<IRawContent> RawContentCollection
		{
			get
			{
				return this.contentList.Cast<IRawContent>().ToList<IRawContent>();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003C54 File Offset: 0x00001E54
		public void Commit()
		{
			this.contentList = this.uncommittedContent;
			this.uncommittedContent = new List<IExtractedContent>();
			this.sharedStream.Flush();
			foreach (UnifiedContentSerializer.HeaderInfo headerInfo in this.headerInfoList)
			{
				this.WriteStreamHeader(headerInfo.Id, headerInfo.Content);
				headerInfo.Content.NeedsUpdate = true;
			}
			this.headerInfoList.Clear();
			this.outputStream.Flush();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003CFC File Offset: 0x00001EFC
		public void WriteProperty(UnifiedContentSerializer.PropertyId id, string data)
		{
			long value = 8L + SharedContentWriter.ComputeLength(data);
			using (SharedContentWriter sharedContentWriter = new SharedContentWriter(this.outputStream))
			{
				sharedContentWriter.Write(value);
				sharedContentWriter.Write(UnifiedContentSerializer.EntryId.Property);
				sharedContentWriter.Write(id);
				sharedContentWriter.Write(data);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D58 File Offset: 0x00001F58
		internal SharedContent AddStream(UnifiedContentSerializer.EntryId id, Stream stream, string name)
		{
			int count = this.uncommittedContent.Count;
			SharedContent sharedContent = (SharedContent)this.LookupStream(count, stream);
			if (sharedContent != null)
			{
				this.AddStreamHeader(id, sharedContent);
				return sharedContent;
			}
			return this.WriteStream(id, stream, name);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003D98 File Offset: 0x00001F98
		internal SharedContent WriteStream(UnifiedContentSerializer.EntryId id, Stream stream, string name)
		{
			SharedContent sharedContent = SharedContent.Create(this.sharedStream, stream, name);
			sharedContent.FileName = name;
			this.AddStreamHeader(id, sharedContent);
			return sharedContent;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003DC4 File Offset: 0x00001FC4
		internal SharedContent AddNewStream(UnifiedContentSerializer.EntryId id, Stream stream, string name)
		{
			int count = this.uncommittedContent.Count;
			if ((SharedContent)this.LookupStream(count, stream) == null)
			{
				return this.WriteStream(id, stream, name);
			}
			return null;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003E28 File Offset: 0x00002028
		internal List<IExtractedContent> LookupStream(string name, Stream stream)
		{
			uint hash = Crc32.ComputeHash(stream, 0L);
			IEnumerable<IExtractedContent> source = from c in this.contentList
			where c.FileName == name && !c.IsModified(hash)
			select c;
			return source.ToList<IExtractedContent>();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003E70 File Offset: 0x00002070
		private IExtractedContent LookupStream(int index, Stream stream)
		{
			if (this.contentList.Count > index)
			{
				IExtractedContent extractedContent = this.contentList[index];
				if (extractedContent != null && !extractedContent.IsModified(stream))
				{
					return extractedContent;
				}
			}
			return null;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003EA8 File Offset: 0x000020A8
		private void AddStreamHeader(UnifiedContentSerializer.EntryId id, SharedContent sharedContent)
		{
			this.headerInfoList.Add(new UnifiedContentSerializer.HeaderInfo
			{
				Id = id,
				Content = sharedContent
			});
			this.uncommittedContent.Add(sharedContent);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003EE8 File Offset: 0x000020E8
		private void WriteStreamHeader(UnifiedContentSerializer.EntryId id, SharedContent sharedContent)
		{
			using (SharedContentWriter sharedContentWriter = new SharedContentWriter(this.outputStream))
			{
				long num = 0L;
				long position = this.outputStream.Position;
				sharedContentWriter.Write(num);
				sharedContentWriter.Write(id);
				if (sharedContent.FileName != null)
				{
					sharedContentWriter.Write(sharedContent.FileName);
				}
				sharedContentWriter.Write(sharedContent.RawDataEntryPosition);
				sharedContentWriter.Write(sharedContent.Properties.Count);
				foreach (KeyValuePair<string, object> keyValuePair in sharedContent.Properties)
				{
					sharedContentWriter.Write(keyValuePair.Key);
					if (keyValuePair.Value == null)
					{
						sharedContentWriter.Write(0U);
					}
					else
					{
						Type type = keyValuePair.Value.GetType();
						if (type == typeof(string))
						{
							sharedContentWriter.Write(Encoding.Unicode.GetBytes((string)keyValuePair.Value));
						}
						else if (type == typeof(byte[]))
						{
							sharedContentWriter.Write((byte[])keyValuePair.Value);
						}
						else
						{
							ulong value = Convert.ToUInt64(keyValuePair.Value);
							sharedContentWriter.Write(BitConverter.GetBytes(value));
						}
					}
				}
				num = this.outputStream.Position - position - 8L;
				this.outputStream.Position = position;
				sharedContentWriter.Write(num);
				this.outputStream.Position += num;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000040A0 File Offset: 0x000022A0
		private void WriteHeader(SharedContentWriter writer, string shareName)
		{
			byte[] bytes = Encoding.ASCII.GetBytes("PPMAIL01");
			writer.Write(bytes, 0, bytes.Length);
			long value = 4L + SharedContentWriter.ComputeLength(shareName);
			writer.Write(value);
			writer.Write(UnifiedContentSerializer.EntryId.PpeHeader);
			writer.Write(shareName);
		}

		// Token: 0x04000051 RID: 81
		private readonly SharedStream sharedStream;

		// Token: 0x04000052 RID: 82
		private readonly Stream outputStream;

		// Token: 0x04000053 RID: 83
		private readonly List<UnifiedContentSerializer.HeaderInfo> headerInfoList = new List<UnifiedContentSerializer.HeaderInfo>();

		// Token: 0x04000054 RID: 84
		private List<IExtractedContent> contentList;

		// Token: 0x04000055 RID: 85
		private List<IExtractedContent> uncommittedContent = new List<IExtractedContent>();

		// Token: 0x0200000F RID: 15
		public enum EntryId
		{
			// Token: 0x04000057 RID: 87
			PpeHeader,
			// Token: 0x04000058 RID: 88
			Property,
			// Token: 0x04000059 RID: 89
			Body,
			// Token: 0x0400005A RID: 90
			Attachment,
			// Token: 0x0400005B RID: 91
			File,
			// Token: 0x0400005C RID: 92
			Stream
		}

		// Token: 0x02000010 RID: 16
		public enum PropertyId
		{
			// Token: 0x0400005E RID: 94
			Subject = 1
		}

		// Token: 0x02000011 RID: 17
		internal struct HeaderInfo
		{
			// Token: 0x0600009B RID: 155 RVA: 0x000040E7 File Offset: 0x000022E7
			public HeaderInfo(UnifiedContentSerializer.EntryId id, SharedContent content)
			{
				this.Id = id;
				this.Content = content;
			}

			// Token: 0x0400005F RID: 95
			public UnifiedContentSerializer.EntryId Id;

			// Token: 0x04000060 RID: 96
			public SharedContent Content;
		}
	}
}
