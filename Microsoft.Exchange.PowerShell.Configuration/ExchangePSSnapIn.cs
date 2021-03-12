using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.PowerShell
{
	// Token: 0x02000004 RID: 4
	public abstract class ExchangePSSnapIn : CustomPSSnapIn
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00010CCC File Offset: 0x0000EECC
		public ExchangePSSnapIn()
		{
			Globals.InitializeMultiPerfCounterInstance("EMS");
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00010D04 File Offset: 0x0000EF04
		public override string Vendor
		{
			get
			{
				return "Microsoft Corporation";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00010D0B File Offset: 0x0000EF0B
		public override string Description
		{
			get
			{
				return Strings.ExchangePSSnapInDescription;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00010D14 File Offset: 0x0000EF14
		public override Collection<FormatConfigurationEntry> Formats
		{
			get
			{
				if (this.formats == null)
				{
					this.formats = new Collection<FormatConfigurationEntry>();
					foreach (FormatConfigurationEntry item in this.formatConfigurationEntries)
					{
						this.formats.Add(item);
					}
				}
				return this.formats;
			}
		}

		// Token: 0x0400000B RID: 11
		private FormatConfigurationEntry[] formatConfigurationEntries = new FormatConfigurationEntry[]
		{
			new FormatConfigurationEntry("Exchange.format.ps1xml")
		};

		// Token: 0x0400000C RID: 12
		private Collection<FormatConfigurationEntry> formats;
	}
}
