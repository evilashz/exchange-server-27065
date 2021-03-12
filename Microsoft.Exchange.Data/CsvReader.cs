using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000206 RID: 518
	internal sealed class CsvReader : DisposeTrackableBase
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x000362A4 File Offset: 0x000344A4
		public CsvReader(StreamReader streamReader, bool isFirstLineHeader = true)
		{
			if (streamReader == null)
			{
				throw new ArgumentException("streamReader");
			}
			this.streamReader = streamReader;
			this.buffer = new char[65536];
			this.currentArrayOffset = this.buffer.Length;
			this.line = new StringBuilder(this.buffer.Length);
			if (isFirstLineHeader)
			{
				this.Headers = this.ReadLine();
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0003630C File Offset: 0x0003450C
		public int CharacterOffset
		{
			get
			{
				return this.characterOffset;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00036314 File Offset: 0x00034514
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x0003631C File Offset: 0x0003451C
		public string[] Headers { get; private set; }

		// Token: 0x06001209 RID: 4617 RVA: 0x00036325 File Offset: 0x00034525
		internal void Seek(int characterOffset, long streamOffset)
		{
			this.streamReader.BaseStream.Seek(streamOffset, SeekOrigin.Begin);
			this.streamReader.DiscardBufferedData();
			this.currentArrayOffset = this.buffer.Length;
			this.characterOffset = characterOffset;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0003635C File Offset: 0x0003455C
		public string[] ReadLine()
		{
			bool flag = false;
			this.line.Length = 0;
			this.DevourCRLF();
			do
			{
				this.FillBufferIfNecessary();
				if (this.bufferSize == 0)
				{
					break;
				}
				int num = this.currentArrayOffset;
				while (this.currentArrayOffset < this.bufferSize)
				{
					char c = this.buffer[this.currentArrayOffset];
					if (c <= '\r')
					{
						if (c == '\n' || c == '\r')
						{
							goto IL_75;
						}
					}
					else if (c != '"')
					{
						switch (c)
						{
						case '\u2028':
						case '\u2029':
							goto IL_75;
						}
					}
					else
					{
						flag = !flag;
					}
					IL_78:
					this.currentArrayOffset++;
					this.characterOffset++;
					continue;
					IL_75:
					if (flag)
					{
						goto IL_78;
					}
					break;
				}
				int num2 = this.currentArrayOffset;
				if (num2 == num)
				{
					break;
				}
				this.line.Append(this.buffer, num, num2 - num);
			}
			while (this.currentArrayOffset >= this.bufferSize);
			this.DevourCRLF();
			if (this.line.Length == 0)
			{
				return null;
			}
			this.line.Append(',');
			MatchCollection matchCollection = CsvReader.regex.Matches(this.line.ToString());
			string[] array = new string[matchCollection.Count];
			for (int i = 0; i < array.Length; i++)
			{
				string text = matchCollection[i].Value;
				text = text.Substring(0, text.Length - 1);
				if (!string.IsNullOrEmpty(text))
				{
					text = text.Trim();
					if (text.Length > 0 && text[0] == '"')
					{
						int num3 = text.Length - 1;
						while (text[num3] != '"')
						{
							num3--;
						}
						text = text.Substring(1, num3 - 1);
					}
					text = text.Replace("\"\"", "\"");
				}
				array[i] = text;
			}
			return array;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00036610 File Offset: 0x00034810
		public IEnumerable<string[]> ReadRows()
		{
			string[] row;
			while ((row = this.ReadLine()) != null)
			{
				yield return row;
			}
			yield break;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003662D File Offset: 0x0003482D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CsvReader>(this);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00036635 File Offset: 0x00034835
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (this.streamReader != null)
			{
				this.streamReader.Dispose();
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003664C File Offset: 0x0003484C
		private void DevourCRLF()
		{
			for (;;)
			{
				this.FillBufferIfNecessary();
				if (this.bufferSize == 0)
				{
					break;
				}
				while (this.currentArrayOffset < this.bufferSize && (this.buffer[this.currentArrayOffset] == '\r' || this.buffer[this.currentArrayOffset] == '\n' || this.buffer[this.currentArrayOffset] == '\u2028' || this.buffer[this.currentArrayOffset] == '\u2029'))
				{
					this.currentArrayOffset++;
					this.characterOffset++;
				}
				if (this.currentArrayOffset != this.buffer.Length)
				{
					return;
				}
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000366F0 File Offset: 0x000348F0
		private void FillBufferIfNecessary()
		{
			if (this.currentArrayOffset == this.buffer.Length)
			{
				this.currentArrayOffset = 0;
				this.bufferSize = 0;
				int num;
				do
				{
					num = this.streamReader.Read(this.buffer, this.bufferSize, this.buffer.Length - this.bufferSize);
					this.bufferSize += num;
				}
				while (num != 0 && this.bufferSize < this.buffer.Length);
			}
		}

		// Token: 0x04000ADE RID: 2782
		private static readonly Regex regex = new Regex("(\"(?:[^\"]|\"\")*\"|[^\",]*),", RegexOptions.Compiled);

		// Token: 0x04000ADF RID: 2783
		private StreamReader streamReader;

		// Token: 0x04000AE0 RID: 2784
		private char[] buffer;

		// Token: 0x04000AE1 RID: 2785
		private int currentArrayOffset;

		// Token: 0x04000AE2 RID: 2786
		private int bufferSize;

		// Token: 0x04000AE3 RID: 2787
		private StringBuilder line;

		// Token: 0x04000AE4 RID: 2788
		private int characterOffset;
	}
}
