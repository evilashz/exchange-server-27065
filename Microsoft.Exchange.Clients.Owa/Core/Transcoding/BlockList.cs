using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002E3 RID: 739
	internal sealed class BlockList
	{
		// Token: 0x06001C5C RID: 7260 RVA: 0x000A27C6 File Offset: 0x000A09C6
		public BlockList(int maxBlocklistSize, TimeSpan maxBlockTime)
		{
			this.maxBlocklistSize = maxBlocklistSize;
			this.maxBlockTime = maxBlockTime;
			this.blockList = new Dictionary<string, ExDateTime>();
			this.thisLock = new object();
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000A27F4 File Offset: 0x000A09F4
		public bool CheckItem(string docId)
		{
			if (string.IsNullOrEmpty(docId))
			{
				throw new ArgumentException("Input invalid document ID.", "docId");
			}
			bool result;
			lock (this.thisLock)
			{
				if (this.blockList.ContainsKey(docId))
				{
					if (ExDateTime.UtcNow - this.blockList[docId] > this.maxBlockTime)
					{
						this.blockList.Remove(docId);
						result = false;
					}
					else
					{
						result = true;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x000A2890 File Offset: 0x000A0A90
		public int Count
		{
			get
			{
				int count;
				lock (this.thisLock)
				{
					count = this.blockList.Count;
				}
				return count;
			}
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000A28D8 File Offset: 0x000A0AD8
		public void AddNew(string docId)
		{
			if (string.IsNullOrEmpty(docId))
			{
				throw new ArgumentException("Input invalid document ID", "docId");
			}
			ExTraceGlobals.TranscodingTracer.TraceDebug<string>((long)this.GetHashCode(), "Add document {0} to block list", docId);
			lock (this.thisLock)
			{
				if (this.blockList.Count >= this.maxBlocklistSize)
				{
					this.blockList.Clear();
				}
				if (!this.blockList.ContainsKey(docId))
				{
					this.blockList.Add(docId, ExDateTime.UtcNow);
				}
			}
		}

		// Token: 0x040014EA RID: 5354
		private Dictionary<string, ExDateTime> blockList;

		// Token: 0x040014EB RID: 5355
		private object thisLock;

		// Token: 0x040014EC RID: 5356
		private TimeSpan maxBlockTime;

		// Token: 0x040014ED RID: 5357
		private int maxBlocklistSize;
	}
}
