using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200002E RID: 46
	internal class LogTypeInstance : ConfigurationElement
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B2F0 File Offset: 0x000094F0
		[ConfigurationProperty("dir", IsRequired = true)]
		public string Dir
		{
			get
			{
				return (string)base["dir"];
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B302 File Offset: 0x00009502
		[ConfigurationProperty("schema", IsRequired = true)]
		public LogSchemaType Schema
		{
			get
			{
				return (LogSchemaType)base["schema"];
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000B314 File Offset: 0x00009514
		[ConfigurationProperty("prefix", IsRequired = true)]
		public string Prefix
		{
			get
			{
				return (string)base["prefix"];
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B326 File Offset: 0x00009526
		[ConfigurationProperty("cfgName", IsRequired = true)]
		public string ConfigName
		{
			get
			{
				return (string)base["cfgName"];
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000B338 File Offset: 0x00009538
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000B34A File Offset: 0x0000954A
		[ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)base["enabled"];
			}
			internal set
			{
				base["enabled"] = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000B35D File Offset: 0x0000955D
		[ConfigurationProperty("LogManagerPluginName", IsRequired = false)]
		public string LogManagerPlugin
		{
			get
			{
				return (string)base["LogManagerPluginName"];
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000B36F File Offset: 0x0000956F
		[ConfigurationProperty("watermarkDir", IsRequired = false)]
		public string WatermarkDirCfg
		{
			get
			{
				return (string)base["watermarkDir"];
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B381 File Offset: 0x00009581
		[ConfigurationProperty("instanceName", IsRequired = false, DefaultValue = null)]
		public string InstanceName
		{
			get
			{
				return (string)base["instanceName"];
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000B393 File Offset: 0x00009593
		[ConfigurationProperty("outputDir", IsRequired = false, DefaultValue = null)]
		public string OutputDirectory
		{
			get
			{
				return (string)base["outputDir"];
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public string Instance
		{
			get
			{
				string text;
				if (string.IsNullOrWhiteSpace(this.InstanceName))
				{
					text = this.Prefix;
				}
				else
				{
					text = this.InstanceName;
				}
				foreach (char c in text)
				{
					if (!char.IsLetterOrDigit(c))
					{
						throw new ArgumentOutOfRangeException("instance name is restricted to be a~z A~Z only");
					}
				}
				return text;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B401 File Offset: 0x00009601
		public string WatermarkFileDirectory
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.WatermarkDirCfg))
				{
					return this.Dir;
				}
				return this.WatermarkDirCfg;
			}
		}

		// Token: 0x04000161 RID: 353
		public const string DirectoryKey = "dir";

		// Token: 0x04000162 RID: 354
		public const string SchemaKey = "schema";

		// Token: 0x04000163 RID: 355
		public const string PrefixKey = "prefix";

		// Token: 0x04000164 RID: 356
		public const string ConfigurationKey = "cfgName";

		// Token: 0x04000165 RID: 357
		public const string EnabledKey = "enabled";
	}
}
