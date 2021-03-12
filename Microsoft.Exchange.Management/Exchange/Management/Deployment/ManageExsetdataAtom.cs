using System;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000177 RID: 375
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageExsetdataAtom : Task
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0004010F File Offset: 0x0003E30F
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x00040126 File Offset: 0x0003E326
		[Parameter(Mandatory = true)]
		[LocDescription(Strings.IDs.DomainControllerName)]
		public string DomainController
		{
			get
			{
				return (string)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00040139 File Offset: 0x0003E339
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x00040150 File Offset: 0x0003E350
		[LocDescription(Strings.IDs.ExsetdataOrganizationName)]
		[Parameter(Mandatory = false)]
		public string Organization
		{
			get
			{
				return (string)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00040163 File Offset: 0x0003E363
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x0004017A File Offset: 0x0003E37A
		[Parameter(Mandatory = false)]
		[LocDescription(Strings.IDs.ExsetdataLegacyOrganizationName)]
		public string LegacyOrganization
		{
			get
			{
				return (string)base.Fields["LegacyOrganization"];
			}
			set
			{
				base.Fields["LegacyOrganization"] = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0004018D File Offset: 0x0003E38D
		private static ManagedLoggerDelegate LoggerDelegate
		{
			get
			{
				if (ManageExsetdataAtom.managedLoggerDelegate == null)
				{
					ManageExsetdataAtom.managedLoggerDelegate = new ManagedLoggerDelegate(TaskLogger.UnmanagedLog);
				}
				return ManageExsetdataAtom.managedLoggerDelegate;
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000401AC File Offset: 0x0003E3AC
		protected void InstallAtom(AtomID atomID)
		{
			TaskLogger.LogEnter();
			base.WriteVerbose(Strings.LogExsetdataInstallingAtom(atomID.ToString()));
			uint scErr = ExsetdataNativeMethods.SetupAtom((uint)atomID, 61953U, ConfigurationContext.Setup.InstallPath, ConfigurationContext.Setup.InstallPath, this.DomainController, this.Organization, this.LegacyOrganization, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, RoutingGroup.DefaultName, ManageExsetdataAtom.LoggerDelegate);
			this.HandleExsetdataReturnCode(scErr);
			TaskLogger.LogExit();
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00040224 File Offset: 0x0003E424
		protected void BuildToBuildUpgradeAtom(AtomID atomID)
		{
			TaskLogger.LogEnter();
			base.WriteVerbose(Strings.LogExsetdataReinstallingAtom(atomID.ToString()));
			uint scErr = ExsetdataNativeMethods.SetupAtom((uint)atomID, 61955U, ConfigurationContext.Setup.InstallPath, ConfigurationContext.Setup.InstallPath, this.DomainController, this.Organization, this.LegacyOrganization, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, RoutingGroup.DefaultName, ManageExsetdataAtom.LoggerDelegate);
			this.HandleExsetdataReturnCode(scErr);
			TaskLogger.LogExit();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0004029C File Offset: 0x0003E49C
		protected void DisasterRecoveryAtom(AtomID atomID)
		{
			TaskLogger.LogEnter();
			base.WriteVerbose(Strings.LogExsetdataReinstallingAtom(atomID.ToString()));
			uint scErr = ExsetdataNativeMethods.SetupAtom((uint)atomID, 61959U, ConfigurationContext.Setup.InstallPath, ConfigurationContext.Setup.InstallPath, this.DomainController, this.Organization, this.LegacyOrganization, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, RoutingGroup.DefaultName, ManageExsetdataAtom.LoggerDelegate);
			this.HandleExsetdataReturnCode(scErr);
			TaskLogger.LogExit();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00040314 File Offset: 0x0003E514
		protected void UninstallAtom(AtomID atomID)
		{
			TaskLogger.LogEnter();
			base.WriteVerbose(Strings.LogExsetdataUninstallingAtom(atomID.ToString()));
			uint scErr = ExsetdataNativeMethods.SetupAtom((uint)atomID, 61954U, ConfigurationContext.Setup.InstallPath, ConfigurationContext.Setup.InstallPath, this.DomainController, this.Organization, this.LegacyOrganization, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, AdministrativeGroup.DefaultName, RoutingGroup.DefaultName, ManageExsetdataAtom.LoggerDelegate);
			this.HandleExsetdataReturnCode(scErr);
			TaskLogger.LogExit();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0004038C File Offset: 0x0003E58C
		private void HandleExsetdataReturnCode(uint scErr)
		{
			if ((scErr & 2147483648U) == 0U)
			{
				return;
			}
			string text = null;
			string text2 = null;
			uint messageFromExsetdata = ManageExsetdataAtom.GetMessageFromExsetdata(scErr, new CultureInfo("en-US"), ref text);
			if (messageFromExsetdata != 0U)
			{
				base.ThrowTerminatingError(new LocalizedException(Strings.ExceptionExsetdataGetMessageError(scErr, messageFromExsetdata)), ErrorCategory.NotSpecified, null);
			}
			messageFromExsetdata = ManageExsetdataAtom.GetMessageFromExsetdata(scErr, CultureInfo.CurrentUICulture, ref text2);
			if (messageFromExsetdata != 0U)
			{
				base.ThrowTerminatingError(new LocalizedException(Strings.ExceptionExsetdataGetMessageError(scErr, messageFromExsetdata)), ErrorCategory.NotSpecified, null);
			}
			LocalizedString localizedMessage = Strings.ExceptionExsetdataGenericError(scErr, text2.ToString());
			LocalizedString englishMessage = Strings.ExceptionExsetdataGenericError(scErr, text.ToString());
			base.ThrowTerminatingError(new ExsetdataException(scErr, englishMessage, localizedMessage), ErrorCategory.NotSpecified, null);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00040424 File Offset: 0x0003E624
		private static uint GetMessageFromExsetdata(uint scErr, CultureInfo cultureInfo, ref string errorMessage)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			int capacity = stringBuilder.Capacity;
			int lcid = cultureInfo.LCID;
			uint num = ExsetdataNativeMethods.ScGetFormattedError(scErr, lcid, stringBuilder, ref capacity);
			if (num == 3221684458U)
			{
				stringBuilder.EnsureCapacity(capacity);
				capacity = stringBuilder.Capacity;
				num = ExsetdataNativeMethods.ScGetFormattedError(scErr, lcid, stringBuilder, ref capacity);
			}
			if (num == 0U)
			{
				errorMessage = stringBuilder.ToString();
			}
			return num;
		}

		// Token: 0x040006A6 RID: 1702
		private static ManagedLoggerDelegate managedLoggerDelegate;
	}
}
