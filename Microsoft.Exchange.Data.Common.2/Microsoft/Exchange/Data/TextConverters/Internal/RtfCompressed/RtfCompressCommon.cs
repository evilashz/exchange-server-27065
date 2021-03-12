using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters.Internal.RtfCompressed
{
	// Token: 0x0200027A RID: 634
	internal class RtfCompressCommon
	{
		// Token: 0x060019A2 RID: 6562 RVA: 0x000CA4B0 File Offset: 0x000C86B0
		protected RtfCompressCommon(Stream input, bool push, Stream output, int inputBufferSize, int outputBufferSize)
		{
			if (push)
			{
				this.pushSource = (input as ConverterStream);
				this.pushSink = output;
				this.writeBuffer = new byte[outputBufferSize];
				this.writeEnd = this.writeBuffer.Length;
				this.writeCurrent = this.writeStart;
				return;
			}
			this.pullSource = input;
			this.pullSink = (output as ConverterStream);
			this.readBuffer = new byte[inputBufferSize];
			this.readEnd = this.readBuffer.Length;
			this.readCurrent = this.readEnd;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000CA53C File Offset: 0x000C873C
		protected static void ToBytes(uint value, byte[] buffer, int offset)
		{
			buffer[offset++] = (byte)(value & 255U);
			buffer[offset++] = (byte)((value & 65280U) >> 8);
			buffer[offset++] = (byte)((value & 16711680U) >> 16);
			buffer[offset++] = (byte)((value & 4278190080U) >> 24);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000CA594 File Offset: 0x000C8794
		protected bool ReadMoreData()
		{
			if (this.pushSource != null)
			{
				int num;
				if (!this.pushSource.GetInputChunk(out this.readBuffer, out this.readStart, out num, out this.inputEndOfFile))
				{
					this.readEnd = this.readCurrent;
					return false;
				}
				this.readCurrent = this.readStart;
				this.readEnd = this.readCurrent + num;
			}
			else
			{
				this.readStart = 0;
				this.readCurrent = this.readStart;
				this.readEnd = this.pullSource.Read(this.readBuffer, 0, this.readBuffer.Length);
				if (this.readEnd == 0)
				{
					this.inputEndOfFile = true;
				}
			}
			return true;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000CA638 File Offset: 0x000C8838
		protected void ReportRead()
		{
			this.readFileOffset += this.readCurrent - this.readStart;
			if (this.pushSource != null)
			{
				this.pushSource.ReportRead(this.readCurrent - this.readStart);
				this.readStart = this.readCurrent;
				return;
			}
			this.readStart = this.readCurrent;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000CA698 File Offset: 0x000C8898
		protected bool GetOutputSpace()
		{
			if (this.pullSink != null)
			{
				int num;
				this.pullSink.GetOutputBuffer(out this.writeBuffer, out this.writeCurrent, out num);
				this.writeStart = this.writeCurrent;
				this.writeEnd = this.writeCurrent + num;
				if (num == 0)
				{
					return false;
				}
			}
			else
			{
				this.writeCurrent = 0;
			}
			return true;
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000CA6F0 File Offset: 0x000C88F0
		protected void FlushOutput()
		{
			this.writeFileOffset += this.writeCurrent - this.writeStart;
			if (this.pullSink != null)
			{
				this.pullSink.ReportOutput(this.writeCurrent - this.writeStart);
				this.writeStart = this.writeCurrent;
				return;
			}
			this.pushSink.Write(this.writeBuffer, this.writeStart, this.writeCurrent - this.writeStart);
			this.writeCurrent = this.writeEnd;
		}

		// Token: 0x04001ED1 RID: 7889
		protected const uint MagicCompressed = 1967544908U;

		// Token: 0x04001ED2 RID: 7890
		protected const uint MagicUncompressed = 1095517517U;

		// Token: 0x04001ED3 RID: 7891
		protected const int WindowSize = 4096;

		// Token: 0x04001ED4 RID: 7892
		protected const int LookAheadMost = 17;

		// Token: 0x04001ED5 RID: 7893
		protected const int MaxExpansion = 17;

		// Token: 0x04001ED6 RID: 7894
		protected const int BreakEven = 1;

		// Token: 0x04001ED7 RID: 7895
		protected static readonly byte[] PreloadData = CTSGlobals.AsciiEncoding.GetBytes("{\\rtf1\\ansi\\mac\\deff0\\deftab720{\\fonttbl;}{\\f0\\fnil \\froman \\fswiss \\fmodern \\fscript \\fdecor MS Sans SerifSymbolArialTimes New RomanCourier{\\colortbl\\red0\\green0\\blue0\r\n\\par \\pard\\plain\\f0\\fs20\\b\\i\\u\\tab\\tx");

		// Token: 0x04001ED8 RID: 7896
		protected bool endOfFile;

		// Token: 0x04001ED9 RID: 7897
		protected ConverterStream pushSource;

		// Token: 0x04001EDA RID: 7898
		protected Stream pullSource;

		// Token: 0x04001EDB RID: 7899
		protected bool inputEndOfFile;

		// Token: 0x04001EDC RID: 7900
		protected Stream pushSink;

		// Token: 0x04001EDD RID: 7901
		protected ConverterStream pullSink;

		// Token: 0x04001EDE RID: 7902
		protected int readFileOffset;

		// Token: 0x04001EDF RID: 7903
		protected int writeFileOffset;

		// Token: 0x04001EE0 RID: 7904
		protected byte[] readBuffer;

		// Token: 0x04001EE1 RID: 7905
		protected int readStart;

		// Token: 0x04001EE2 RID: 7906
		protected int readCurrent;

		// Token: 0x04001EE3 RID: 7907
		protected int readEnd;

		// Token: 0x04001EE4 RID: 7908
		protected byte[] writeBuffer;

		// Token: 0x04001EE5 RID: 7909
		protected int writeStart;

		// Token: 0x04001EE6 RID: 7910
		protected int writeCurrent;

		// Token: 0x04001EE7 RID: 7911
		protected int writeEnd;

		// Token: 0x04001EE8 RID: 7912
		protected byte[] window;

		// Token: 0x04001EE9 RID: 7913
		protected int windowCurrent;
	}
}
