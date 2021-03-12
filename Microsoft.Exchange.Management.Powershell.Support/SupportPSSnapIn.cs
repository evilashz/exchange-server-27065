using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200000B RID: 11
	[RunInstaller(true)]
	public sealed class SupportPSSnapIn : CustomPSSnapIn
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003F40 File Offset: 0x00002140
		public override string Name
		{
			get
			{
				return SupportPSSnapIn.PSSnapInName;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003F47 File Offset: 0x00002147
		public override string Description
		{
			get
			{
				return Strings.ExchangeSupportPSSnapInDescription;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003F53 File Offset: 0x00002153
		public override string Vendor
		{
			get
			{
				return "Microsoft Corporation";
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003F5C File Offset: 0x0000215C
		public override Collection<CmdletConfigurationEntry> Cmdlets
		{
			get
			{
				if (this.cmdlets == null)
				{
					this.cmdlets = new Collection<CmdletConfigurationEntry>();
					foreach (CmdletConfigurationEntry item in CmdletConfiguration.SupportCmdletConfigurationEntries)
					{
						this.cmdlets.Add(item);
					}
				}
				return this.cmdlets;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003FA8 File Offset: 0x000021A8
		public override Collection<FormatConfigurationEntry> Formats
		{
			get
			{
				if (this.formats == null)
				{
					this.formats = new Collection<FormatConfigurationEntry>();
					foreach (FormatConfigurationEntry item in CmdletConfiguration.SupportFormatConfigurationEntries)
					{
						this.formats.Add(item);
					}
				}
				return this.formats;
			}
		}

		// Token: 0x04000043 RID: 67
		public static readonly string PSSnapInName = "Microsoft.Exchange.Management.Powershell.Support";

		// Token: 0x04000044 RID: 68
		private Collection<CmdletConfigurationEntry> cmdlets;

		// Token: 0x04000045 RID: 69
		private Collection<FormatConfigurationEntry> formats;
	}
}
