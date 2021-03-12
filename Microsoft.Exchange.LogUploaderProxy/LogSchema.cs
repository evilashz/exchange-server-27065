using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200001A RID: 26
	public class LogSchema
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00002EB2 File Offset: 0x000010B2
		public LogSchema(string software, string version, string logType, string[] fields)
		{
			this.logSchemaImpl = new LogSchema(software, version, logType, fields);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002ECA File Offset: 0x000010CA
		internal LogSchema LogSchemaImpl
		{
			get
			{
				return this.logSchemaImpl;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002ED2 File Offset: 0x000010D2
		internal string Software
		{
			get
			{
				return this.logSchemaImpl.Software;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002EDF File Offset: 0x000010DF
		internal string Version
		{
			get
			{
				return this.logSchemaImpl.Version;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002EEC File Offset: 0x000010EC
		internal string LogType
		{
			get
			{
				return this.logSchemaImpl.LogType;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002EF9 File Offset: 0x000010F9
		internal string[] Fields
		{
			get
			{
				return this.logSchemaImpl.Fields;
			}
		}

		// Token: 0x04000042 RID: 66
		private LogSchema logSchemaImpl;
	}
}
