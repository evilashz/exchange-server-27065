using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001C5 RID: 453
	public class LogSchema
	{
		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		public LogSchema(string software, string version, string logType, string[] fields)
		{
			this.software = software;
			this.version = version;
			this.logType = logType;
			this.fields = fields;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002EEDD File Offset: 0x0002D0DD
		internal string Software
		{
			get
			{
				return this.software;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002EEE5 File Offset: 0x0002D0E5
		internal string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002EEED File Offset: 0x0002D0ED
		internal string LogType
		{
			get
			{
				return this.logType;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002EEF5 File Offset: 0x0002D0F5
		internal string[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x0400096D RID: 2413
		private string software;

		// Token: 0x0400096E RID: 2414
		private string version;

		// Token: 0x0400096F RID: 2415
		private string logType;

		// Token: 0x04000970 RID: 2416
		private string[] fields;
	}
}
