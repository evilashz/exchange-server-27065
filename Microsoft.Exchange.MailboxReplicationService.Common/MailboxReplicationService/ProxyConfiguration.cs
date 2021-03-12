using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000043 RID: 67
	[DataContract]
	internal sealed class ProxyConfiguration
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00005CCF File Offset: 0x00003ECF
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00005CD7 File Offset: 0x00003ED7
		[DataMember(Name = "ExportBufferSizeKB")]
		public int ExportBufferSizeKB { get; set; }

		// Token: 0x0600033F RID: 831 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public ProxyConfiguration()
		{
			this.ExportBufferSizeKB = ConfigBase<MRSConfigSchema>.GetConfig<int>("ExportBufferSizeKB");
		}
	}
}
