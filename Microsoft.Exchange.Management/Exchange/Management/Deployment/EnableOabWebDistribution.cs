using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001CC RID: 460
	[Cmdlet("Enable", "OabWebDistribution", SupportsShouldProcess = false, DefaultParameterSetName = "Identity")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class EnableOabWebDistribution : SetOfflineAddressBookInternal
	{
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00047F13 File Offset: 0x00046113
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return LocalizedString.Empty;
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00047F1A File Offset: 0x0004611A
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.skipExecution = false;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00047F34 File Offset: 0x00046134
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.skipExecution)
			{
				return;
			}
			this.DataObject = (OfflineAddressBook)this.PrepareDataObject();
			if (this.DataObject.WebDistributionEnabled)
			{
				base.WriteVerbose(Strings.VerboseDefaultOABWebDistributionEnabled(this.DataObject.DistinguishedName));
				this.skipExecution = true;
				return;
			}
			ITopologyConfigurationSession topologyConfigurationSession = base.DataSession as ITopologyConfigurationSession;
			this.localOABVDirs = topologyConfigurationSession.FindOABVirtualDirectoriesForLocalServer();
			if (this.localOABVDirs == null || this.localOABVDirs.Length == 0)
			{
				base.WriteVerbose(Strings.VerboseNoOabVDirOnLocalServer);
				this.skipExecution = true;
				return;
			}
			if (this.DataObject.IsReadOnly)
			{
				base.WriteVerbose(Strings.VerboseDefaultOABIsNewerThanVersionE12(this.DataObject.DistinguishedName, this.DataObject.ExchangeVersion.ExchangeBuild.ToString()));
				this.skipExecution = true;
				return;
			}
			if (this.DataObject.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				base.WriteVerbose(Strings.VerboseDefaultOABIsOlderThanVersionE12(this.DataObject.DistinguishedName));
				this.skipExecution = true;
				return;
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00048054 File Offset: 0x00046254
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.skipExecution)
			{
				return;
			}
			MultiValuedProperty<OfflineAddressBookVersion> versions = this.DataObject.Versions;
			if (!versions.Contains(OfflineAddressBookVersion.Version4))
			{
				versions.Add(OfflineAddressBookVersion.Version4);
				this.DataObject.Versions = versions;
			}
			foreach (ADOabVirtualDirectory adoabVirtualDirectory in this.localOABVDirs)
			{
				this.DataObject.VirtualDirectories.Add(adoabVirtualDirectory.Id);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04000762 RID: 1890
		private bool skipExecution;

		// Token: 0x04000763 RID: 1891
		private ADOabVirtualDirectory[] localOABVDirs;
	}
}
