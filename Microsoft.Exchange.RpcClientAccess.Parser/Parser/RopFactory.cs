using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200021F RID: 543
	internal static class RopFactory
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x00026218 File Offset: 0x00024418
		internal static void CreateRops(byte[] inputBuffer, int inputIndex, int inputSize, IParseLogonTracker logonTracker, ServerObjectHandleTable serverObjectHandleTable, ref List<Rop> ropList)
		{
			using (Reader reader = Reader.CreateBufferReader(new ArraySegment<byte>(inputBuffer, inputIndex, inputSize)))
			{
				while (reader.Position < reader.Length)
				{
					long position = reader.Position;
					RopId ropId = (RopId)reader.PeekByte(0L);
					Rop rop;
					if (!RopFactory.TryCreateRop(ropId, out rop))
					{
						throw new BufferParseException(string.Format("Invalid rop type found: {0}", ropId));
					}
					rop.ParseInput(reader, serverObjectHandleTable, logonTracker);
					long position2 = reader.Position;
					if (ropList == null)
					{
						ropList = new List<Rop>();
					}
					ropList.Add(rop);
				}
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000262B8 File Offset: 0x000244B8
		internal static bool TryCreateRop(RopId ropId, out Rop rop)
		{
			Rop.CreateRopDelegate createRopDelegate;
			if (RopFactory.ropDictionary.TryGetValue(ropId, out createRopDelegate))
			{
				rop = createRopDelegate();
				return true;
			}
			rop = null;
			return false;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000262E4 File Offset: 0x000244E4
		private static Dictionary<RopId, Rop.CreateRopDelegate> PopulateRopDictionary()
		{
			Assembly assembly = typeof(RopFactory).GetTypeInfo().Assembly;
			Type typeFromHandle = typeof(Rop);
			Dictionary<RopId, Rop.CreateRopDelegate> dictionary = new Dictionary<RopId, Rop.CreateRopDelegate>(new RopIdComparer());
			foreach (TypeInfo typeInfo in assembly.DefinedTypes)
			{
				if (!typeInfo.IsAbstract && typeInfo.IsSubclassOf(typeFromHandle))
				{
					FieldInfo declaredField = typeInfo.GetDeclaredField("RopType");
					RopId key = (RopId)declaredField.GetValue(null);
					MethodInfo declaredMethod = typeInfo.GetDeclaredMethod("CreateRop");
					Rop.CreateRopDelegate value = (Rop.CreateRopDelegate)declaredMethod.CreateDelegate(typeof(Rop.CreateRopDelegate));
					dictionary[key] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x040006A7 RID: 1703
		private const int MaxInputBufferSize = 32768;

		// Token: 0x040006A8 RID: 1704
		private static Dictionary<RopId, Rop.CreateRopDelegate> ropDictionary = RopFactory.PopulateRopDictionary();
	}
}
