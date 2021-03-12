using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000610 RID: 1552
	internal abstract class WaveReader : SoundReader
	{
		// Token: 0x06001BE6 RID: 7142 RVA: 0x0003288C File Offset: 0x00030A8C
		protected override bool ReadHeader(BinaryReader reader)
		{
			return this.ReadRiffHeader(reader);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00032895 File Offset: 0x00030A95
		protected virtual bool ReadWaveChunk(BinaryReader reader)
		{
			if (this.GetString(reader, 4) != "RIFF")
			{
				return false;
			}
			reader.ReadInt32();
			return !(this.GetString(reader, 4) != "WAVE");
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000328CC File Offset: 0x00030ACC
		protected virtual bool ReadDataChunk(BinaryReader reader)
		{
			string text = "data";
			int num = 0;
			long num2 = Math.Min(256L, base.WaveStream.Length - base.WaveStream.Position);
			while (num2 >= 0L)
			{
				if (num >= text.Length)
				{
					base.WaveDataLength = reader.ReadInt32();
					base.WaveDataPosition = base.WaveStream.Position;
					return true;
				}
				if (reader.PeekChar() == (int)text[num])
				{
					num++;
					reader.ReadChar();
					num2 -= 1L;
				}
				else if (num == 0)
				{
					reader.ReadChar();
					num2 -= 1L;
				}
				else
				{
					num = 0;
				}
			}
			ExTraceGlobals.UtilTracer.TraceDebug((long)this.GetHashCode(), "ReadDataChunk bailing because data chunk not found");
			return false;
		}

		// Token: 0x06001BE9 RID: 7145
		protected abstract bool ReadRiffHeader(BinaryReader reader);

		// Token: 0x06001BEA RID: 7146
		protected abstract bool ReadFmtChunk(BinaryReader reader);

		// Token: 0x06001BEB RID: 7147 RVA: 0x00032980 File Offset: 0x00030B80
		protected string GetString(BinaryReader reader, int numBytes)
		{
			byte[] array = new byte[numBytes];
			reader.Read(array, 0, array.Length);
			return Encoding.ASCII.GetString(array);
		}
	}
}
