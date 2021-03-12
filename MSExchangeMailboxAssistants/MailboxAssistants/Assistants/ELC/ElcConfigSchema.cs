using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200004C RID: 76
	internal class ElcConfigSchema : ConfigSchemaBase
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00010925 File Offset: 0x0000EB25
		public override string Name
		{
			get
			{
				return "ELC";
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0001092C File Offset: 0x0000EB2C
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0001093E File Offset: 0x0000EB3E
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "90:00:00:00", ExcludeRange = false)]
		[ConfigurationProperty("SingleItemRecoveryRetention", DefaultValue = "90:00:00:00")]
		public TimeSpan SingleItemRecoveryRetention
		{
			get
			{
				return (TimeSpan)base["SingleItemRecoveryRetention"];
			}
			set
			{
				base["SingleItemRecoveryRetention"] = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00010951 File Offset: 0x0000EB51
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00010963 File Offset: 0x0000EB63
		[ConfigurationProperty("IgnoreManagedFolder", DefaultValue = true)]
		public bool IgnoreManagedFolder
		{
			get
			{
				return (bool)base["IgnoreManagedFolder"];
			}
			set
			{
				base["IgnoreManagedFolder"] = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00010976 File Offset: 0x0000EB76
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00010988 File Offset: 0x0000EB88
		[ConfigurationProperty("DisableElcRemoteArchive", DefaultValue = false)]
		public bool DisableElcRemoteArchive
		{
			get
			{
				return (bool)base["DisableElcRemoteArchive"];
			}
			set
			{
				base["DisableElcRemoteArchive"] = value;
			}
		}

		// Token: 0x0200004D RID: 77
		public static class Setting
		{
			// Token: 0x0400024A RID: 586
			public const string SingleItemRecoveryRetention = "SingleItemRecoveryRetention";

			// Token: 0x0400024B RID: 587
			public const string IgnoreManagedFolder = "IgnoreManagedFolder";

			// Token: 0x0400024C RID: 588
			public const string DisableElcRemoteArchive = "DisableElcRemoteArchive";
		}
	}
}
