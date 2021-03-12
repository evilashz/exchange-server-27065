using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200003A RID: 58
	internal class AuxiliaryData
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004A11 File Offset: 0x00002C11
		private AuxiliaryData(ArraySegment<byte> auxiliaryInputBuffer)
		{
			Util.ThrowOnNullArgument(auxiliaryInputBuffer.Array, "auxiliaryInputBuffer");
			this.inputBlocks = AuxiliaryData.ParseAuxiliaryBuffer(auxiliaryInputBuffer);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004A42 File Offset: 0x00002C42
		public IReadOnlyList<AuxiliaryBlock> Input
		{
			get
			{
				return this.inputBlocks;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00004A4A File Offset: 0x00002C4A
		public IReadOnlyList<AuxiliaryBlock> Output
		{
			get
			{
				return this.outputBlocks;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004A52 File Offset: 0x00002C52
		public static AuxiliaryData GetEmptyAuxiliaryData()
		{
			return AuxiliaryData.Parse(null);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004A5A File Offset: 0x00002C5A
		public static AuxiliaryData Parse(byte[] auxIn)
		{
			return AuxiliaryData.Parse(new ArraySegment<byte>(auxIn ?? Array<byte>.Empty));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004A70 File Offset: 0x00002C70
		public static AuxiliaryData Parse(ArraySegment<byte> auxIn)
		{
			return new AuxiliaryData(auxIn);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004A78 File Offset: 0x00002C78
		public void AppendOutput(IEnumerable<AuxiliaryBlock> outputBlocks)
		{
			Util.ThrowOnNullArgument(outputBlocks, "outputBlocks");
			foreach (AuxiliaryBlock outputBlock in outputBlocks)
			{
				this.AppendOutput(outputBlock);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004ACC File Offset: 0x00002CCC
		public void AppendOutput(AuxiliaryBlock outputBlock)
		{
			Util.ThrowOnNullArgument(outputBlock, "outputBlock");
			this.outputBlocks.Add(outputBlock);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public void ReportClientPerformance(IClientPerformanceDataSink sink, Predicate<AuxiliaryBlock> includeBlock)
		{
			foreach (AuxiliaryBlock auxiliaryBlock in this.inputBlocks)
			{
				if (includeBlock(auxiliaryBlock))
				{
					auxiliaryBlock.ReportClientPerformance(sink);
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004B44 File Offset: 0x00002D44
		public ArraySegment<byte> Serialize(ArraySegment<byte> auxiliaryOutputBuffer)
		{
			int count;
			this.Serialize(auxiliaryOutputBuffer, out count);
			return auxiliaryOutputBuffer.SubSegment(0, count);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004B62 File Offset: 0x00002D62
		public void Serialize(ArraySegment<byte> auxiliaryOutputBuffer, out int auxiliaryOutputSize)
		{
			AuxiliaryData.SerializeAuxiliaryBlocks(this.outputBlocks, auxiliaryOutputBuffer, out auxiliaryOutputSize);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004B74 File Offset: 0x00002D74
		public int CalculateSerializedOutputSize()
		{
			int num = 0;
			foreach (AuxiliaryBlock auxiliaryBlock in this.outputBlocks)
			{
				num += (int)auxiliaryBlock.CalculateSerializedSize();
			}
			return num;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004BCC File Offset: 0x00002DCC
		internal static AuxiliaryData CreateEmpty()
		{
			return AuxiliaryData.Parse(null);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004BD4 File Offset: 0x00002DD4
		internal static void SerializeAuxiliaryBlocks(IEnumerable<AuxiliaryBlock> auxiliaryBlocks, ArraySegment<byte> auxiliaryBuffer, out int size)
		{
			using (BufferWriter bufferWriter = new BufferWriter(auxiliaryBuffer))
			{
				IList<AuxiliaryBlock> list = auxiliaryBlocks as IList<AuxiliaryBlock>;
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (!list[i].TrySerialize(bufferWriter))
						{
							break;
						}
					}
				}
				else
				{
					foreach (AuxiliaryBlock auxiliaryBlock in auxiliaryBlocks)
					{
						if (!auxiliaryBlock.TrySerialize(bufferWriter))
						{
							break;
						}
					}
				}
				size = (int)bufferWriter.Position;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004C78 File Offset: 0x00002E78
		internal static List<AuxiliaryBlock> ParseAuxiliaryBuffer(ArraySegment<byte> buffer)
		{
			List<AuxiliaryBlock> list = new List<AuxiliaryBlock>(4);
			using (BufferReader bufferReader = Reader.CreateBufferReader(buffer))
			{
				while (bufferReader.Length - bufferReader.Position > 0L)
				{
					list.Add(AuxiliaryBlock.Parse(bufferReader));
				}
			}
			return list;
		}

		// Token: 0x040000BA RID: 186
		private readonly List<AuxiliaryBlock> inputBlocks;

		// Token: 0x040000BB RID: 187
		private readonly List<AuxiliaryBlock> outputBlocks = new List<AuxiliaryBlock>(4);
	}
}
