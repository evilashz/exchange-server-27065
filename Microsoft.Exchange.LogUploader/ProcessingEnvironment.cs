using System;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000030 RID: 48
	internal class ProcessingEnvironment : ConfigurationElement
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B44A File Offset: 0x0000964A
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000B45C File Offset: 0x0000965C
		[ConfigurationProperty("Logs", IsRequired = true)]
		public LogTypeInstanceCollection Logs
		{
			get
			{
				return (LogTypeInstanceCollection)base["Logs"];
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000B46E File Offset: 0x0000966E
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000B476 File Offset: 0x00009676
		public string EnvironmentName { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000B47F File Offset: 0x0000967F
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000B487 File Offset: 0x00009687
		public string Region { get; private set; }

		// Token: 0x06000269 RID: 617 RVA: 0x0000B490 File Offset: 0x00009690
		protected override void PostDeserialize()
		{
			this.EnvironmentName = string.Empty;
			this.Region = string.Empty;
			string[] array = this.Name.Split(new char[]
			{
				'-'
			}, StringSplitOptions.RemoveEmptyEntries);
			string text = null;
			if (array.Length < 1)
			{
				text = "Invalid name. empty name? " + this.Name;
			}
			else
			{
				this.EnvironmentName = array[0];
				if (array.Length > 1)
				{
					this.Region = array[1];
				}
			}
			if (text != null)
			{
				throw new ArgumentException(text);
			}
		}

		// Token: 0x04000166 RID: 358
		public const string NameKey = "name";

		// Token: 0x04000167 RID: 359
		public const string LogTypeInstancesKey = "Logs";
	}
}
