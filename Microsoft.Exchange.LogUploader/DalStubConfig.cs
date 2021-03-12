using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000035 RID: 53
	public class DalStubConfig : ConfigurationSection
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B62E File Offset: 0x0000982E
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000B640 File Offset: 0x00009840
		[ConfigurationProperty("Control")]
		public ControlElement Control
		{
			get
			{
				return (ControlElement)base["Control"];
			}
			set
			{
				base["Control"] = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B64E File Offset: 0x0000984E
		[ConfigurationProperty("PartitionSettings", IsRequired = true)]
		public PartitionsCollection Partitions
		{
			get
			{
				return (PartitionsCollection)base["PartitionSettings"];
			}
		}
	}
}
