using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200003B RID: 59
	internal class EventDefinition
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00009124 File Offset: 0x00007324
		internal string TaskPropertyName
		{
			get
			{
				return this.taskPropertyName;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000912C File Offset: 0x0000732C
		internal string IdPropertyName
		{
			get
			{
				return this.idPropertyName;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009134 File Offset: 0x00007334
		internal string RtsPropertyName
		{
			get
			{
				return this.rtsPropertyName;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000913C File Offset: 0x0000733C
		internal string OperationPropertyName
		{
			get
			{
				return this.operationPropertyName;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00009144 File Offset: 0x00007344
		internal string OperationTypePropertyName
		{
			get
			{
				return this.operationTypePropertyName;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000914C File Offset: 0x0000734C
		internal string CountPropertyName
		{
			get
			{
				return this.countPropertyName;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00009154 File Offset: 0x00007354
		internal string ErrorPropertyName
		{
			get
			{
				return this.errorPropertyName;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000915C File Offset: 0x0000735C
		internal EventDefinition(string taskPropertyName, string idPropertyName, string rtsPropertyName, string operationPropertyName, string operationTypePropertyName, string countPropertyName, string errorPropertyName)
		{
			this.taskPropertyName = taskPropertyName;
			this.idPropertyName = idPropertyName;
			this.rtsPropertyName = rtsPropertyName;
			this.operationPropertyName = operationPropertyName;
			this.operationTypePropertyName = operationTypePropertyName;
			this.countPropertyName = countPropertyName;
			this.errorPropertyName = errorPropertyName;
		}

		// Token: 0x040000DF RID: 223
		private string taskPropertyName;

		// Token: 0x040000E0 RID: 224
		private string idPropertyName;

		// Token: 0x040000E1 RID: 225
		private string rtsPropertyName;

		// Token: 0x040000E2 RID: 226
		private string operationPropertyName;

		// Token: 0x040000E3 RID: 227
		private string operationTypePropertyName;

		// Token: 0x040000E4 RID: 228
		private string countPropertyName;

		// Token: 0x040000E5 RID: 229
		private string errorPropertyName;
	}
}
