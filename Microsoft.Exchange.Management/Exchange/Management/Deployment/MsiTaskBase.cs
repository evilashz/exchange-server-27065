using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000296 RID: 662
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class MsiTaskBase : Task
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x000649EC File Offset: 0x00062BEC
		protected MsiTaskBase()
		{
			base.Fields["PropertyValues"] = string.Empty;
			base.Fields["LogMode"] = InstallLogMode.Error;
			base.Fields["LogFile"] = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, "msi.log");
			base.Fields["Activity"] = string.Empty;
			base.Fields["Canceled"] = false;
			base.Fields.ResetChangeTracking();
			this.uiHandlerObject = new MsiUIHandler();
			this.uiHandlerObject.OnProgress = new MsiUIHandler.ProgressHandler(this.UpdateProgress);
			this.uiHandlerObject.IsCanceled = new MsiUIHandler.IsCanceledHandler(this.IsCanceled);
			this.uiHandlerObject.OnMsiError = new MsiUIHandler.MsiErrorHandler(this.OnMsiError);
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00064ACF File Offset: 0x00062CCF
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x00064AE6 File Offset: 0x00062CE6
		[Parameter(Mandatory = false)]
		public string PropertyValues
		{
			get
			{
				return (string)base.Fields["PropertyValues"];
			}
			set
			{
				base.Fields["PropertyValues"] = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00064AF9 File Offset: 0x00062CF9
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x00064B10 File Offset: 0x00062D10
		[Parameter(Mandatory = false)]
		public string LogFile
		{
			get
			{
				return (string)base.Fields["LogFile"];
			}
			set
			{
				base.Fields["LogFile"] = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00064B23 File Offset: 0x00062D23
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x00064B3A File Offset: 0x00062D3A
		[Parameter(Mandatory = false)]
		public InstallLogMode LogMode
		{
			get
			{
				return (InstallLogMode)base.Fields["LogMode"];
			}
			set
			{
				base.Fields["LogMode"] = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00064B52 File Offset: 0x00062D52
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x00064B69 File Offset: 0x00062D69
		[Parameter(Mandatory = false)]
		public string[] Features
		{
			get
			{
				return (string[])base.Fields["Features"];
			}
			set
			{
				base.Fields["Features"] = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00064B7C File Offset: 0x00062D7C
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x00064B93 File Offset: 0x00062D93
		internal bool Canceled
		{
			get
			{
				return (bool)base.Fields["Canceled"];
			}
			set
			{
				base.Fields["Canceled"] = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00064BAB File Offset: 0x00062DAB
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x00064BC2 File Offset: 0x00062DC2
		internal LocalizedString Activity
		{
			get
			{
				return (LocalizedString)base.Fields["Activity"];
			}
			set
			{
				base.Fields["Activity"] = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00064BDA File Offset: 0x00062DDA
		internal MsiUIHandler UIHandler
		{
			get
			{
				return this.uiHandlerObject;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00064BE2 File Offset: 0x00062DE2
		internal string LastMsiError
		{
			get
			{
				return this.lastMsiError;
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00064BEA File Offset: 0x00062DEA
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (!this.PropertyValues.Contains(" REBOOT="))
			{
				this.PropertyValues += " REBOOT=ReallySuppress";
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00064C24 File Offset: 0x00062E24
		internal void SetLogging()
		{
			TaskLogger.LogEnter();
			if (this.LogMode != InstallLogMode.None)
			{
				MsiNativeMethods.EnableLog(this.LogMode, this.LogFile, InstallLogAttributes.Append);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00064C4C File Offset: 0x00062E4C
		internal void UpdateProgress(int progress)
		{
			TaskLogger.LogEnter();
			ExProgressRecord exProgressRecord = new ExProgressRecord(0, this.Activity, Strings.MsiProgressStatus);
			exProgressRecord.RecordType = ProgressRecordType.Processing;
			exProgressRecord.PercentComplete = progress;
			if (!base.Stopping)
			{
				try
				{
					base.WriteProgress(exProgressRecord);
				}
				catch (PipelineStoppedException)
				{
					this.Canceled = true;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00064CB0 File Offset: 0x00062EB0
		internal void OnMsiError(string errorMsg)
		{
			TaskLogger.LogEnter();
			this.lastMsiError = errorMsg;
			TaskLogger.LogExit();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00064CC3 File Offset: 0x00062EC3
		internal bool IsCanceled()
		{
			return base.Stopping || this.Canceled;
		}

		// Token: 0x04000A14 RID: 2580
		private MsiUIHandler uiHandlerObject;

		// Token: 0x04000A15 RID: 2581
		private string lastMsiError;
	}
}
