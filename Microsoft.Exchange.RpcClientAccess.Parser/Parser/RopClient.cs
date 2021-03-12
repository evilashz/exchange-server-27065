using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200021A RID: 538
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RopClient
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x000250F1 File Offset: 0x000232F1
		public static byte[][] CreateInputBuffer(IList<Rop> ropList)
		{
			return RopClient.CreateInputBuffer(ropList, RopClient.ArrayWithSingleUninitializedHandle);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00025134 File Offset: 0x00023334
		public static byte[][] CreateInputBuffer(IList<Rop> ropList, IList<ServerObjectHandle> serverObjectHandles)
		{
			List<byte[]> list = new List<byte[]>(1);
			list.Add(BufferWriter.Serialize(delegate(Writer writer)
			{
				RopClient.BuildRopStream(writer, ropList, serverObjectHandles);
			}));
			if (ropList.Count > 0)
			{
				using (IEnumerator<Rop> enumerator = ropList[ropList.Count - 1].SplitChainedData())
				{
					while (enumerator.MoveNext())
					{
						Rop item = enumerator.Current;
						List<Rop> chainedRopList = new List<Rop>(1);
						chainedRopList.Add(item);
						list.Add(BufferWriter.Serialize(delegate(Writer writer)
						{
							RopClient.BuildRopStream(writer, chainedRopList, null);
						}));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00025218 File Offset: 0x00023418
		public static byte[] CreateAuxiliaryBuffer(IEnumerable<AuxiliaryBlock> auxiliaryBlocks)
		{
			byte[] array = new byte[4096];
			int num;
			AuxiliaryData.SerializeAuxiliaryBlocks(auxiliaryBlocks, new ArraySegment<byte>(array), out num);
			byte[] array2 = new byte[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00025250 File Offset: 0x00023450
		public static List<RopResult> ParseOneRop<T>(ArraySegment<byte> segmentRopOut) where T : Rop, new()
		{
			List<RopResult> result;
			using (BufferReader bufferReader = Reader.CreateBufferReader(segmentRopOut))
			{
				T t = Activator.CreateInstance<T>();
				ServerObjectHandleTable serverObjectHandleTable;
				RopClient.ParseRopStream(bufferReader, t, out serverObjectHandleTable);
				if (t.ChainedResults != null)
				{
					result = t.ChainedResults;
				}
				else
				{
					result = new List<RopResult>(1)
					{
						t.Result
					};
				}
			}
			return result;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000252D4 File Offset: 0x000234D4
		public static List<AuxiliaryBlock> ParseAuxiliaryBuffer(ArraySegment<byte> segmentAuxOut)
		{
			return AuxiliaryData.ParseAuxiliaryBuffer(segmentAuxOut);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000252DC File Offset: 0x000234DC
		private static void BuildRopStream(Writer writer, IList<Rop> ropList, IList<ServerObjectHandle> serverObjectHandles)
		{
			long position = writer.Position;
			writer.WriteUInt16(0);
			foreach (Rop rop in ropList)
			{
				rop.SerializeInput(writer, CTSGlobals.AsciiEncoding);
			}
			int num = (int)(writer.Position - position);
			if (serverObjectHandles != null)
			{
				foreach (ServerObjectHandle serverObjectHandle in serverObjectHandles)
				{
					serverObjectHandle.Serialize(writer);
				}
			}
			long position2 = writer.Position;
			writer.Position = position;
			writer.WriteUInt16((ushort)num);
			writer.Position = position2;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000253A8 File Offset: 0x000235A8
		private static void ParseRopStream(Reader reader, Rop rop, out ServerObjectHandleTable serverObjectHandleTable)
		{
			ushort num = reader.ReadUInt16();
			long position = reader.Position;
			reader.Position = (long)((ulong)num);
			serverObjectHandleTable = ServerObjectHandleTable.Parse(reader);
			reader.Position = position;
			RopId ropId = (RopId)reader.PeekByte(0L);
			if (ropId != rop.RopId)
			{
				throw new BufferParseException(string.Format("Not expected ROP; found={0}, expected={1}", ropId, rop.RopId));
			}
			rop.ParseOutput(reader, CTSGlobals.AsciiEncoding);
		}

		// Token: 0x04000694 RID: 1684
		private const int MaxSizeAuxIn = 4096;

		// Token: 0x04000695 RID: 1685
		private static readonly ServerObjectHandle[] ArrayWithSingleUninitializedHandle = new ServerObjectHandle[]
		{
			ServerObjectHandle.None
		};
	}
}
