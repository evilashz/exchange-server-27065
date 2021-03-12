using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Servicelets.ExchangeCertificate
{
	// Token: 0x02000006 RID: 6
	internal struct ServiceData
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000423E File Offset: 0x0000243E
		public ServiceData(string domain, AllowedServices flags)
		{
			this.domain = domain;
			this.thumbprint = null;
			this.flag = flags;
			this.iisServices = new List<IisService>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004260 File Offset: 0x00002460
		public ServiceData(string domain, string thumbPrint, AllowedServices flags)
		{
			this.domain = domain;
			this.thumbprint = thumbPrint;
			this.flag = flags;
			this.iisServices = new List<IisService>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00004282 File Offset: 0x00002482
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000428A File Offset: 0x0000248A
		public AllowedServices Flag
		{
			get
			{
				return this.flag;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00004292 File Offset: 0x00002492
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000429A File Offset: 0x0000249A
		public List<IisService> IisServices
		{
			get
			{
				return this.iisServices;
			}
		}

		// Token: 0x04000006 RID: 6
		private string domain;

		// Token: 0x04000007 RID: 7
		private string thumbprint;

		// Token: 0x04000008 RID: 8
		private AllowedServices flag;

		// Token: 0x04000009 RID: 9
		private List<IisService> iisServices;
	}
}
