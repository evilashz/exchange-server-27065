using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000036 RID: 54
	public class PartitionElement : ConfigurationElement
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B668 File Offset: 0x00009868
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000B67A File Offset: 0x0000987A
		[ConfigurationProperty("Name", DefaultValue = "", IsKey = true, IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B688 File Offset: 0x00009888
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000B69A File Offset: 0x0000989A
		[IntegerValidator(MinValue = 0, MaxValue = 1)]
		[ConfigurationProperty("CopyId", DefaultValue = 0, IsKey = false, IsRequired = true)]
		public int CopyId
		{
			get
			{
				return (int)base["CopyId"];
			}
			set
			{
				base["CopyId"] = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B6AD File Offset: 0x000098AD
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000B6BF File Offset: 0x000098BF
		[IntegerValidator(MinValue = 0, MaxValue = 23)]
		[ConfigurationProperty("PartitionId", DefaultValue = 0, IsKey = false, IsRequired = true)]
		public int PartitionId
		{
			get
			{
				return (int)base["PartitionId"];
			}
			set
			{
				base["PartitionId"] = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B6D2 File Offset: 0x000098D2
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000B6E4 File Offset: 0x000098E4
		[ConfigurationProperty("DbWriteFailurePercent", DefaultValue = 0, IsKey = false, IsRequired = false)]
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		public int DbWriteFailurePercent
		{
			get
			{
				return (int)base["DbWriteFailurePercent"];
			}
			set
			{
				base["DbWriteFailurePercent"] = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B6F7 File Offset: 0x000098F7
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000B709 File Offset: 0x00009909
		[ConfigurationProperty("WriteToRealDB", IsRequired = false, DefaultValue = "false")]
		public bool WriteToRealDB
		{
			get
			{
				return (bool)base["WriteToRealDB"];
			}
			set
			{
				base["WriteToRealDB"] = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000B71C File Offset: 0x0000991C
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000B72E File Offset: 0x0000992E
		[ConfigurationProperty("DBWriteTime", IsRequired = false, DefaultValue = "00:00:04")]
		[TimeSpanValidator(MinValueString = "00:00:00.100")]
		public TimeSpan DBWriteTime
		{
			get
			{
				return (TimeSpan)base["DBWriteTime"];
			}
			set
			{
				base["DBWriteTime"] = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B741 File Offset: 0x00009941
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000B753 File Offset: 0x00009953
		[ConfigurationProperty("IsHealthy", IsRequired = false, DefaultValue = "true")]
		public bool IsHealthy
		{
			get
			{
				return (bool)base["IsHealthy"];
			}
			set
			{
				base["IsHealthy"] = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000B766 File Offset: 0x00009966
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000B778 File Offset: 0x00009978
		[ConfigurationProperty("ExceptionString", DefaultValue = "", IsKey = false, IsRequired = false)]
		public string ExceptionString
		{
			get
			{
				return (string)base["ExceptionString"];
			}
			set
			{
				base["ExceptionString"] = value;
			}
		}
	}
}
