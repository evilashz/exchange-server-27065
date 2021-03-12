using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x0200066F RID: 1647
	internal class BufferCache
	{
		// Token: 0x06001DD1 RID: 7633 RVA: 0x000363EC File Offset: 0x000345EC
		public BufferCache(int cacheSize = 1000)
		{
			this.maxCacheSize = cacheSize;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0003641C File Offset: 0x0003461C
		public BufferCacheEntry GetBuffer(int size)
		{
			lock (this)
			{
				Queue<LinkedListNode<BufferCacheEntry>> queue;
				if (BufferCache.IsSupportedBufferSize(size) && this.sizeToBufferMap.TryGetValue(size, out queue))
				{
					this.hitCount++;
					LinkedListNode<BufferCacheEntry> linkedListNode = this.ReleaseBuffer(queue);
					this.bufferQueue.Remove(linkedListNode);
					if (linkedListNode.Value.Buffer.Length != size)
					{
						throw new InvalidOperationException(string.Format("Unexpected count {0} for buffer. Expected count was {1}", linkedListNode.Value.Buffer.Length, size));
					}
					Array.Clear(linkedListNode.Value.Buffer, 0, linkedListNode.Value.Buffer.Length);
					return linkedListNode.Value;
				}
				else
				{
					this.missCount++;
				}
			}
			return new BufferCacheEntry(new byte[size], true);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00036510 File Offset: 0x00034710
		public void ReturnBuffer(BufferCacheEntry bufferCacheEntry)
		{
			if (bufferCacheEntry.OwnedByBufferCache && BufferCache.IsSupportedBufferSize(bufferCacheEntry.Buffer.Length))
			{
				lock (this)
				{
					this.returnedBufferCount++;
					if (this.bufferQueue.Count == this.maxCacheSize)
					{
						LinkedListNode<BufferCacheEntry> first = this.bufferQueue.First;
						int key = first.Value.Buffer.Length;
						if (!object.ReferenceEquals(this.sizeToBufferMap[key].Peek(), first))
						{
							throw new InvalidOperationException("Inconsistent datastructure detected in BufferCache");
						}
						this.bufferQueue.RemoveFirst();
						this.ReleaseBuffer(this.sizeToBufferMap[key]);
					}
					LinkedListNode<BufferCacheEntry> bufferNode = this.bufferQueue.AddLast(bufferCacheEntry);
					Queue<LinkedListNode<BufferCacheEntry>> queue;
					if (!this.sizeToBufferMap.TryGetValue(bufferCacheEntry.Buffer.Length, out queue))
					{
						queue = new Queue<LinkedListNode<BufferCacheEntry>>();
						this.sizeToBufferMap[bufferCacheEntry.Buffer.Length] = queue;
					}
					this.AddBuffer(bufferNode, queue);
				}
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0003662C File Offset: 0x0003482C
		private static bool IsSupportedBufferSize(int size)
		{
			return size % BufferCache.OneKiloByteBufferSize == 0 && size <= BufferCache.MaxSupportedByteArraySize;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00036644 File Offset: 0x00034844
		private void AddBuffer(LinkedListNode<BufferCacheEntry> bufferNode, Queue<LinkedListNode<BufferCacheEntry>> queue)
		{
			if (!this.freeBufferSet.Add(bufferNode.Value))
			{
				throw new InvalidOperationException("Trying to add the same buffer twice");
			}
			queue.Enqueue(bufferNode);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0003666C File Offset: 0x0003486C
		private LinkedListNode<BufferCacheEntry> ReleaseBuffer(Queue<LinkedListNode<BufferCacheEntry>> queue)
		{
			LinkedListNode<BufferCacheEntry> linkedListNode = queue.Dequeue();
			if (queue.Count == 0)
			{
				this.sizeToBufferMap.Remove(linkedListNode.Value.Buffer.Length);
			}
			if (!this.freeBufferSet.Remove(linkedListNode.Value))
			{
				throw new InvalidOperationException(string.Format("Did not find entry {0}", linkedListNode.Value.Buffer.Length));
			}
			return linkedListNode;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000366D8 File Offset: 0x000348D8
		public void AddDiagnosticInfoTo(XElement cacheElement, bool verbose)
		{
			cacheElement.SetAttributeValue("HitCount", this.hitCount);
			cacheElement.SetAttributeValue("MissCount", this.missCount);
			if (!verbose)
			{
				return;
			}
			foreach (KeyValuePair<int, Queue<LinkedListNode<BufferCacheEntry>>> keyValuePair in this.sizeToBufferMap)
			{
				XElement xelement = new XElement("entry");
				xelement.SetAttributeValue("Size", keyValuePair.Key);
				xelement.SetAttributeValue("Count", keyValuePair.Value.Count);
				cacheElement.Add(xelement);
			}
		}

		// Token: 0x04001E02 RID: 7682
		public static readonly int OneKiloByteBufferSize = 1024;

		// Token: 0x04001E03 RID: 7683
		private static readonly int MaxSupportedByteArraySize = BufferCache.OneKiloByteBufferSize * 100;

		// Token: 0x04001E04 RID: 7684
		private readonly int maxCacheSize;

		// Token: 0x04001E05 RID: 7685
		private Dictionary<int, Queue<LinkedListNode<BufferCacheEntry>>> sizeToBufferMap = new Dictionary<int, Queue<LinkedListNode<BufferCacheEntry>>>();

		// Token: 0x04001E06 RID: 7686
		private LinkedList<BufferCacheEntry> bufferQueue = new LinkedList<BufferCacheEntry>();

		// Token: 0x04001E07 RID: 7687
		private HashSet<BufferCacheEntry> freeBufferSet = new HashSet<BufferCacheEntry>();

		// Token: 0x04001E08 RID: 7688
		private int hitCount;

		// Token: 0x04001E09 RID: 7689
		private int missCount;

		// Token: 0x04001E0A RID: 7690
		private int returnedBufferCount;
	}
}
