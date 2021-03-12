using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000034 RID: 52
	public class ControlElement : ConfigurationElement
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B5B7 File Offset: 0x000097B7
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000B5C9 File Offset: 0x000097C9
		[ConfigurationProperty("SwitchOn", DefaultValue = "false", IsRequired = true)]
		public bool SwitchOn
		{
			get
			{
				return (bool)base["SwitchOn"];
			}
			set
			{
				base["SwitchOn"] = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B5DC File Offset: 0x000097DC
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000B5EE File Offset: 0x000097EE
		[ConfigurationProperty("CopiesCount", DefaultValue = 1, IsKey = false, IsRequired = false)]
		[IntegerValidator(MinValue = 1, MaxValue = 2)]
		public int CopiesCount
		{
			get
			{
				return (int)base["CopiesCount"];
			}
			set
			{
				base["CopiesCount"] = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000B601 File Offset: 0x00009801
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000B613 File Offset: 0x00009813
		[IntegerValidator(MinValue = 1, MaxValue = 24)]
		[ConfigurationProperty("PartitionsCount", DefaultValue = 4, IsKey = false, IsRequired = false)]
		public int PartitionsCount
		{
			get
			{
				return (int)base["PartitionsCount"];
			}
			set
			{
				base["PartitionsCount"] = value;
			}
		}
	}
}
