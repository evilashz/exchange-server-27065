using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class TransportAgentObjectId : ObjectId
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000043F3 File Offset: 0x000025F3
		public TransportAgentObjectId(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			this.identity = identity;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004415 File Offset: 0x00002615
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.identity);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004427 File Offset: 0x00002627
		public override string ToString()
		{
			return this.identity;
		}

		// Token: 0x04000031 RID: 49
		private readonly string identity;
	}
}
