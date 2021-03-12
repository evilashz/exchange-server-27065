using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000947 RID: 2375
	[Cmdlet("New", "FlightOverride", SupportsShouldProcess = true)]
	public sealed class NewFlightOverride : NewOverrideBase
	{
		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x0015E7F2 File Offset: 0x0015C9F2
		protected override bool IsFlight
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0015E7F5 File Offset: 0x0015C9F5
		// (set) Token: 0x060054D9 RID: 21721 RVA: 0x0015E80C File Offset: 0x0015CA0C
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Flight
		{
			get
			{
				return base.Fields["Flight"] as string;
			}
			set
			{
				base.Fields["Flight"] = value;
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x0015E81F File Offset: 0x0015CA1F
		// (set) Token: 0x060054DB RID: 21723 RVA: 0x0015E827 File Offset: 0x0015CA27
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public new Version MaxVersion
		{
			get
			{
				return base.MaxVersion;
			}
			set
			{
				base.MaxVersion = value;
			}
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x0015E830 File Offset: 0x0015CA30
		// (set) Token: 0x060054DD RID: 21725 RVA: 0x0015E838 File Offset: 0x0015CA38
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public new Version FixVersion
		{
			get
			{
				return base.FixVersion;
			}
			set
			{
				base.FixVersion = value;
			}
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0015E841 File Offset: 0x0015CA41
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.MaxVersion == null && this.FixVersion == null)
			{
				base.WriteError(new SettingOverrideMaxVersionOrFixVersionRequiredException(), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x0015E87C File Offset: 0x0015CA7C
		protected override SettingOverrideXml GetXml()
		{
			SettingOverrideXml xml = base.GetXml();
			xml.FlightName = this.Flight;
			return xml;
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x0015E89D File Offset: 0x0015CA9D
		protected override VariantConfigurationOverride GetOverride()
		{
			return new VariantConfigurationFlightOverride(this.Flight, base.Parameters.ToArray());
		}
	}
}
