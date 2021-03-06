using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D0 RID: 2256
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MiniStateTreeNode
	{
		// Token: 0x060053FD RID: 21501 RVA: 0x0015C7BE File Offset: 0x0015A9BE
		private MiniStateTreeNode()
		{
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0015C7DC File Offset: 0x0015A9DC
		private MiniStateTreeNode(IConversationTreeNode conversationTreeNode, Func<StoreObjectId, long> idConverter, ICollection<IConversationTreeNode> nodesToExclude)
		{
			foreach (IConversationTreeNode conversationTreeNode2 in conversationTreeNode.ChildNodes)
			{
				if (nodesToExclude == null || !nodesToExclude.Contains(conversationTreeNode2))
				{
					this.ChildNodes.Add(new MiniStateTreeNode(conversationTreeNode2, idConverter, nodesToExclude));
				}
			}
			foreach (StoreObjectId arg in conversationTreeNode.ToListStoreObjectId())
			{
				try
				{
					long item = idConverter(arg);
					this.ItemIdHashes.Add(item);
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.StorageTracer.TraceError(0L, ex.Message);
				}
			}
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0015C8D8 File Offset: 0x0015AAD8
		internal MiniStateTreeNode(IConversationTree conversationTree, Func<StoreObjectId, long> idConverter, ICollection<IConversationTreeNode> nodesToExclude)
		{
			foreach (IConversationTreeNode conversationTreeNode in conversationTree)
			{
				if (nodesToExclude == null || !nodesToExclude.Contains(conversationTreeNode))
				{
					this.ChildNodes.Add(new MiniStateTreeNode(conversationTreeNode, idConverter, nodesToExclude));
				}
			}
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0015C954 File Offset: 0x0015AB54
		internal static MiniStateTreeNode DeSerialize(byte[] bytes)
		{
			MiniStateTreeNode result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					result = MiniStateTreeNode.DeSerialize(binaryReader);
				}
			}
			return result;
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0015C9AC File Offset: 0x0015ABAC
		internal void Serialize(BinaryWriter writer, short level)
		{
			for (int i = 0; i < this.ItemIdHashes.Count; i++)
			{
				if (i == 0)
				{
					MiniStateTreeNode.WriteLevel(writer, level);
				}
				else
				{
					MiniStateTreeNode.WriteLevel(writer, -1);
				}
				writer.Write(this.ItemIdHashes[i]);
			}
			for (int j = 0; j < this.ChildNodes.Count; j++)
			{
				this.ChildNodes[j].Serialize(writer, level + 1);
			}
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0015CA20 File Offset: 0x0015AC20
		internal KeyValuePair<List<long>, List<long>> GetAffectedIds(MiniStateTreeNode right)
		{
			KeyValuePair<List<long>, List<long>> keyValuePair = MiniStateTreeNode.Diff(this, right);
			HashSet<long> hashSet = new HashSet<long>();
			this.CalculateModifiedIds(hashSet, keyValuePair.Key);
			right.CalculateModifiedIds(hashSet, keyValuePair.Value);
			return new KeyValuePair<List<long>, List<long>>(keyValuePair.Key, new List<long>(hashSet));
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0015CA6C File Offset: 0x0015AC6C
		private static MiniStateTreeNode DeSerialize(BinaryReader reader)
		{
			Stack<MiniStateTreeNode> stack = new Stack<MiniStateTreeNode>();
			MiniStateTreeNode miniStateTreeNode = new MiniStateTreeNode();
			stack.Push(miniStateTreeNode);
			while (reader.BaseStream.Position != reader.BaseStream.Length)
			{
				short num = MiniStateTreeNode.ReadLevel(reader);
				if (num == -1)
				{
					stack.Peek().ItemIdHashes.Add(reader.ReadInt64());
				}
				else
				{
					if ((int)num < stack.Count - 1)
					{
						while ((int)num < stack.Count - 1)
						{
							stack.Pop();
						}
					}
					MiniStateTreeNode miniStateTreeNode2 = new MiniStateTreeNode();
					miniStateTreeNode2.ItemIdHashes.Add(reader.ReadInt64());
					stack.Peek().ChildNodes.Add(miniStateTreeNode2);
					stack.Push(miniStateTreeNode2);
				}
			}
			return miniStateTreeNode;
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0015CB1C File Offset: 0x0015AD1C
		private static KeyValuePair<List<long>, List<long>> Diff(MiniStateTreeNode left, MiniStateTreeNode right)
		{
			List<long> list = new List<long>(left.GetItemIdHashes());
			List<long> list2 = new List<long>(right.GetItemIdHashes());
			list.Sort();
			list2.Sort();
			List<long> list3 = new List<long>();
			List<long> list4 = new List<long>();
			int num = 0;
			int num2 = 0;
			while (num < list.Count || num2 < list2.Count)
			{
				if (num < list.Count && num2 < list2.Count)
				{
					if (list[num] > list2[num2])
					{
						list4.Add(list2[num2++]);
					}
					else if (list[num] < list2[num2])
					{
						list3.Add(list[num++]);
					}
					else
					{
						num++;
						num2++;
					}
				}
				else if (num < list.Count)
				{
					list3.Add(list[num++]);
				}
				else
				{
					list4.Add(list2[num2++]);
				}
			}
			return new KeyValuePair<List<long>, List<long>>(list3, list4);
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0015CC28 File Offset: 0x0015AE28
		private static void WriteLevel(BinaryWriter writer, short level)
		{
			if (level == -1)
			{
				writer.Write(byte.MaxValue);
				return;
			}
			if (level < 143)
			{
				writer.Write((byte)level);
				return;
			}
			level = (short)(32768 + (int)level);
			writer.Write((byte)(level >> 8));
			writer.Write((byte)(level & 255));
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0015CC78 File Offset: 0x0015AE78
		private static short ReadLevel(BinaryReader reader)
		{
			short num = (short)reader.ReadByte();
			if (num == 255)
			{
				num = -1;
			}
			else if (num > 127)
			{
				num = (short)((num & 127) << 8);
				num += (short)reader.ReadByte();
			}
			return num;
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0015CF90 File Offset: 0x0015B190
		private IEnumerable<long> GetItemIdHashes()
		{
			foreach (long itemIdHash in this.ItemIdHashes)
			{
				yield return itemIdHash;
			}
			foreach (MiniStateTreeNode subChild in this.ChildNodes)
			{
				foreach (long itemIdHash2 in subChild.GetItemIdHashes())
				{
					yield return itemIdHash2;
				}
			}
			yield break;
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x0015CFB0 File Offset: 0x0015B1B0
		private void CalculateModifiedIds(HashSet<long> modifiedIds, List<long> uniqueIds)
		{
			int num = 0;
			foreach (long item in this.ItemIdHashes)
			{
				if (uniqueIds.BinarySearch(item) >= 0)
				{
					num++;
				}
			}
			foreach (MiniStateTreeNode miniStateTreeNode in this.ChildNodes)
			{
				if (num == this.ItemIdHashes.Count && num > 0)
				{
					foreach (long item2 in miniStateTreeNode.ItemIdHashes)
					{
						if (uniqueIds.BinarySearch(item2) < 0 && !modifiedIds.Contains(item2))
						{
							modifiedIds.Add(item2);
						}
					}
				}
				miniStateTreeNode.CalculateModifiedIds(modifiedIds, uniqueIds);
			}
		}

		// Token: 0x04002D74 RID: 11636
		internal readonly List<MiniStateTreeNode> ChildNodes = new List<MiniStateTreeNode>();

		// Token: 0x04002D75 RID: 11637
		internal readonly List<long> ItemIdHashes = new List<long>();
	}
}
