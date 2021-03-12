using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200038C RID: 908
	internal sealed class ServerObjectHandleTable
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000388FF File Offset: 0x00036AFF
		public int LastIndex
		{
			get
			{
				return this.serverObjectHandleList.Count - 1;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0003890E File Offset: 0x00036B0E
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x00038916 File Offset: 0x00036B16
		public int HighestIndexAccessed
		{
			get
			{
				return this.highestIndexAccessed;
			}
			set
			{
				this.highestIndexAccessed = value;
			}
		}

		// Token: 0x170003AA RID: 938
		public ServerObjectHandle this[int key]
		{
			get
			{
				if (key > this.LastIndex || key < 0)
				{
					throw new BufferParseException("Invalid ServerObjectHandleTable Index");
				}
				this.AccessIndex(key);
				return this.serverObjectHandleList[key];
			}
			set
			{
				if (key > this.LastIndex || key < 0)
				{
					throw new BufferParseException("Invalid ServerObjectHandleTable Index");
				}
				this.AccessIndex(key);
				this.serverObjectHandleList[key] = value;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0003897C File Offset: 0x00036B7C
		public ServerObjectHandleTable()
		{
			this.serverObjectHandleList = new List<ServerObjectHandle>(255);
			for (int i = 0; i < 255; i++)
			{
				this.serverObjectHandleList.Add(ServerObjectHandle.None);
			}
			this.highestIndexAccessed = -1;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000389C8 File Offset: 0x00036BC8
		private ServerObjectHandleTable(Reader reader)
		{
			int num = (int)(reader.Length - reader.Position);
			int num2 = num / 4;
			if (num2 > 256)
			{
				num2 = 256;
			}
			this.serverObjectHandleList = new List<ServerObjectHandle>(num2);
			for (int i = 0; i < num2; i++)
			{
				this.serverObjectHandleList.Add(ServerObjectHandle.Parse(reader));
			}
			this.highestIndexAccessed = -1;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00038A2C File Offset: 0x00036C2C
		public static ServerObjectHandleTable Parse(Reader reader)
		{
			return new ServerObjectHandleTable(reader);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00038A34 File Offset: 0x00036C34
		internal void Serialize(Writer writer)
		{
			for (int i = 0; i <= this.highestIndexAccessed; i++)
			{
				this.serverObjectHandleList[i].Serialize(writer);
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00038A67 File Offset: 0x00036C67
		public void MarkLastHandle()
		{
			this.highestIndexAccessed = this.LastIndex;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00038A75 File Offset: 0x00036C75
		public void AccessIndex(int index)
		{
			if (index > this.LastIndex)
			{
				throw new BufferParseException(string.Format("Invalid handle table index. Index = {0}. LastIndex = {1}.", index, this.LastIndex));
			}
			if (index > this.highestIndexAccessed)
			{
				this.highestIndexAccessed = index;
			}
		}

		// Token: 0x04000B6C RID: 2924
		private IList<ServerObjectHandle> serverObjectHandleList;

		// Token: 0x04000B6D RID: 2925
		private int highestIndexAccessed;
	}
}
