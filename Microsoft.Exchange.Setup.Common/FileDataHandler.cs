using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class FileDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00006EB0 File Offset: 0x000050B0
		public FileDataHandler(ISetupContext context, string commandText, MsiConfigurationInfo msiConfig, MonadConnection connection) : base(context, commandText, connection)
		{
			this.msiConfigurationInfo = msiConfig;
			this.SelectedInstallableUnits = new List<string>();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006ECE File Offset: 0x000050CE
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("logfile", this.msiConfigurationInfo.LogFilePath);
			SetupLogger.TraceExit();
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006F02 File Offset: 0x00005102
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00006F0A File Offset: 0x0000510A
		public List<string> SelectedInstallableUnits
		{
			get
			{
				return this.selectedInstallableUnits;
			}
			set
			{
				this.selectedInstallableUnits = value;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006F14 File Offset: 0x00005114
		protected List<string> GetFeatures()
		{
			List<string> list = new List<string>();
			foreach (string text in this.SelectedInstallableUnits.ToArray())
			{
				if (InstallableUnitConfigurationInfoManager.IsRoleBasedConfigurableInstallableUnit(text))
				{
					list.Add(text.Replace("Role", ""));
				}
			}
			return list;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006F64 File Offset: 0x00005164
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006F6C File Offset: 0x0000516C
		protected virtual MsiConfigurationInfo MsiConfigurationInfo
		{
			get
			{
				return this.msiConfigurationInfo;
			}
			set
			{
				this.msiConfigurationInfo = value;
			}
		}

		// Token: 0x04000061 RID: 97
		private MsiConfigurationInfo msiConfigurationInfo;

		// Token: 0x04000062 RID: 98
		private List<string> selectedInstallableUnits;
	}
}
