using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x0200060F RID: 1551
	internal abstract class SoundReader : IDisposable
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00032702 File Offset: 0x00030902
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x0003270A File Offset: 0x0003090A
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
			protected set
			{
				this.waveFormat = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00032713 File Offset: 0x00030913
		internal string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0003271B File Offset: 0x0003091B
		// (set) Token: 0x06001BD8 RID: 7128 RVA: 0x00032723 File Offset: 0x00030923
		protected internal long WaveDataPosition
		{
			get
			{
				return this.waveDataPosition;
			}
			protected set
			{
				this.waveDataPosition = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0003272C File Offset: 0x0003092C
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x00032734 File Offset: 0x00030934
		protected internal int WaveDataLength
		{
			get
			{
				return this.waveDataLength;
			}
			protected set
			{
				this.waveDataLength = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0003273D File Offset: 0x0003093D
		protected FileStream WaveStream
		{
			get
			{
				return this.waveStream;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001BDC RID: 7132
		protected abstract int MinimumLength { get; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00032745 File Offset: 0x00030945
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0003274D File Offset: 0x0003094D
		protected int FormatLength
		{
			get
			{
				return this.formatLength;
			}
			set
			{
				this.formatLength = value;
			}
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00032756 File Offset: 0x00030956
		public void Dispose()
		{
			if (this.waveStream != null)
			{
				this.waveStream.Close();
				this.waveStream = null;
			}
			if (this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0003278C File Offset: 0x0003098C
		internal int Read(byte[] buffer, int count)
		{
			return this.Read(buffer, 0, count);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00032798 File Offset: 0x00030998
		internal int Read(byte[] buffer, int offset, int count)
		{
			int num = this.WaveDataLength - ((int)this.WaveStream.Position - (int)this.WaveDataPosition);
			if (num < 0)
			{
				throw new InvalidWaveFormatException(this.FilePath);
			}
			int count2 = Math.Min(count, num);
			return this.WaveStream.Read(buffer, offset, count2);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000327E7 File Offset: 0x000309E7
		protected void Create(string fileName)
		{
			if (!this.Initialize(fileName))
			{
				this.Dispose();
				throw new InvalidWaveFormatException(fileName);
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00032800 File Offset: 0x00030A00
		protected bool Initialize(string fileName)
		{
			this.filePath = fileName;
			this.waveStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			if (this.WaveStream.Length < (long)this.MinimumLength)
			{
				return false;
			}
			bool result;
			try
			{
				this.reader = new BinaryReader(this.waveStream, Encoding.ASCII);
				result = this.ReadHeader(this.reader);
			}
			catch (EndOfStreamException)
			{
				result = false;
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001BE4 RID: 7140
		protected abstract bool ReadHeader(BinaryReader reader);

		// Token: 0x04001CBE RID: 7358
		private FileStream waveStream;

		// Token: 0x04001CBF RID: 7359
		private BinaryReader reader;

		// Token: 0x04001CC0 RID: 7360
		private long waveDataPosition;

		// Token: 0x04001CC1 RID: 7361
		private int waveDataLength;

		// Token: 0x04001CC2 RID: 7362
		private WaveFormat waveFormat;

		// Token: 0x04001CC3 RID: 7363
		private string filePath;

		// Token: 0x04001CC4 RID: 7364
		private int formatLength;
	}
}
