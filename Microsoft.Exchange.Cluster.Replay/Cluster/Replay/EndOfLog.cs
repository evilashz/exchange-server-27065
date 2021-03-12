using System;
using Microsoft.Exchange.Data.Serialization;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000376 RID: 886
	internal class EndOfLog
	{
		// Token: 0x060023A3 RID: 9123 RVA: 0x000A7559 File Offset: 0x000A5759
		public EndOfLog(long gen, DateTime utc)
		{
			this.m_generationNum = gen;
			this.m_writeTimeUtc = utc;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000A756F File Offset: 0x000A576F
		public EndOfLog()
		{
			this.m_generationNum = 0L;
			this.m_writeTimeUtc = DateTime.FromFileTimeUtc(0L);
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x000A758C File Offset: 0x000A578C
		public long Generation
		{
			get
			{
				return this.m_generationNum;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000A7594 File Offset: 0x000A5794
		public DateTime Utc
		{
			get
			{
				return this.m_writeTimeUtc;
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000A759C File Offset: 0x000A579C
		public void SetValue(long newGenNum, DateTime? writeTimeUtc)
		{
			lock (this)
			{
				this.m_generationNum = newGenNum;
				if (writeTimeUtc != null)
				{
					this.m_writeTimeUtc = writeTimeUtc.Value;
				}
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000A75F0 File Offset: 0x000A57F0
		public void Serialize(byte[] buf, ref int bytePos)
		{
			lock (this)
			{
				Serialization.SerializeUInt64(buf, ref bytePos, (ulong)this.m_generationNum);
				Serialization.SerializeDateTime(buf, ref bytePos, this.m_writeTimeUtc);
			}
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000A7640 File Offset: 0x000A5840
		public void Deserialize(byte[] buf, ref int bytePos)
		{
			lock (this)
			{
				this.m_generationNum = (long)Serialization.DeserializeUInt64(buf, ref bytePos);
				this.m_writeTimeUtc = Serialization.DeserializeDateTime(buf, ref bytePos);
			}
		}

		// Token: 0x04000F41 RID: 3905
		public const int SerializationLength = 16;

		// Token: 0x04000F42 RID: 3906
		private long m_generationNum;

		// Token: 0x04000F43 RID: 3907
		private DateTime m_writeTimeUtc;
	}
}
