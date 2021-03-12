using System;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000073 RID: 115
	internal sealed class AgentRecord : ICloneable
	{
		// Token: 0x0600037A RID: 890 RVA: 0x00011C73 File Offset: 0x0000FE73
		public AgentRecord(string id, string name, string type, int sequenceNumber, bool isInternal)
		{
			this.id = id;
			this.isInternal = isInternal;
			this.name = name;
			this.type = type;
			this.sequenceNumber = sequenceNumber;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public bool IsInternal
		{
			get
			{
				return this.isInternal;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00011CB8 File Offset: 0x0000FEB8
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		public int SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public Agent Instance
		{
			get
			{
				return this.instance;
			}
			set
			{
				this.instance = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00011CD9 File Offset: 0x0000FED9
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00011CE1 File Offset: 0x0000FEE1
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00011CEA File Offset: 0x0000FEEA
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00011CF2 File Offset: 0x0000FEF2
		public AgentRecord Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011CFB File Offset: 0x0000FEFB
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04000458 RID: 1112
		private readonly string id;

		// Token: 0x04000459 RID: 1113
		private readonly string name;

		// Token: 0x0400045A RID: 1114
		private readonly string type;

		// Token: 0x0400045B RID: 1115
		private readonly int sequenceNumber;

		// Token: 0x0400045C RID: 1116
		private readonly bool isInternal;

		// Token: 0x0400045D RID: 1117
		private Agent instance;

		// Token: 0x0400045E RID: 1118
		private bool enabled;

		// Token: 0x0400045F RID: 1119
		private AgentRecord next;
	}
}
