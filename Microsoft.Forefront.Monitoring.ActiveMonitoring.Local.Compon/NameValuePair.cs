using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000045 RID: 69
	public struct NameValuePair
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000BCF5 File Offset: 0x00009EF5
		internal NameValuePair(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000BD05 File Offset: 0x00009F05
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000BD0D File Offset: 0x00009F0D
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000BD16 File Offset: 0x00009F16
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000BD1E File Offset: 0x00009F1E
		internal string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04000133 RID: 307
		private string name;

		// Token: 0x04000134 RID: 308
		private string value;
	}
}
