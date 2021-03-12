using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000020 RID: 32
	public class DatabaseHeaderInfo
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		public DatabaseHeaderInfo(byte[] serializedLgposLastAttached, DateTime lastRepairedTime, int repairCountSinceLastDefrag, int repairCountBeforeLastDefrag)
		{
			this.serializedLgposLastAttached = serializedLgposLastAttached;
			this.lastRepairedTime = lastRepairedTime;
			this.repairCountSinceLastDefrag = repairCountSinceLastDefrag;
			this.repairCountBeforeLastDefrag = repairCountBeforeLastDefrag;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000DA51 File Offset: 0x0000BC51
		public byte[] SerializedLgposLastAttached
		{
			get
			{
				return this.serializedLgposLastAttached;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000DA59 File Offset: 0x0000BC59
		public DateTime LastRepairedTime
		{
			get
			{
				return this.lastRepairedTime;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000DA61 File Offset: 0x0000BC61
		public int RepairCountSinceLastDefrag
		{
			get
			{
				return this.repairCountSinceLastDefrag;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000DA69 File Offset: 0x0000BC69
		public int RepairCountBeforeLastDefrag
		{
			get
			{
				return this.repairCountBeforeLastDefrag;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000DA71 File Offset: 0x0000BC71
		public bool DatabaseRepaired
		{
			get
			{
				return this.repairCountSinceLastDefrag > 0 || this.repairCountBeforeLastDefrag > 0;
			}
		}

		// Token: 0x040000AF RID: 175
		private readonly byte[] serializedLgposLastAttached;

		// Token: 0x040000B0 RID: 176
		private readonly DateTime lastRepairedTime;

		// Token: 0x040000B1 RID: 177
		private readonly int repairCountSinceLastDefrag;

		// Token: 0x040000B2 RID: 178
		private readonly int repairCountBeforeLastDefrag;
	}
}
