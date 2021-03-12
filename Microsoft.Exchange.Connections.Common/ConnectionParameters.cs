using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ConnectionParameters
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002A70 File Offset: 0x00000C70
		public ConnectionParameters(INamedObject id, ILog log, long maxBytesToTransfer = 9223372036854775807L, int timeout = 300000)
		{
			this.Id = id;
			this.Log = log;
			this.MaxBytesToTransfer = maxBytesToTransfer;
			this.Timeout = timeout;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002A95 File Offset: 0x00000C95
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002A9D File Offset: 0x00000C9D
		public INamedObject Id { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002AA6 File Offset: 0x00000CA6
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002AAE File Offset: 0x00000CAE
		public ILog Log { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002AB7 File Offset: 0x00000CB7
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002ABF File Offset: 0x00000CBF
		public long MaxBytesToTransfer { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002AC8 File Offset: 0x00000CC8
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public int Timeout { get; private set; }
	}
}
