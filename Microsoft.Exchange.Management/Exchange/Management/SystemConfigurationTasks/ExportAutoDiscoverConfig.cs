using System;
using System.DirectoryServices;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F9 RID: 2041
	[Cmdlet("Export", "AutoDiscoverConfig", SupportsShouldProcess = true)]
	public sealed class ExportAutoDiscoverConfig : Task
	{
		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x0012435E File Offset: 0x0012255E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationExportAutoDiscoverConfig;
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x0600472D RID: 18221 RVA: 0x0012436D File Offset: 0x0012256D
		// (set) Token: 0x0600472E RID: 18222 RVA: 0x00124384 File Offset: 0x00122584
		[Parameter]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x0600472F RID: 18223 RVA: 0x00124397 File Offset: 0x00122597
		// (set) Token: 0x06004730 RID: 18224 RVA: 0x001243AE File Offset: 0x001225AE
		[Parameter(Mandatory = false)]
		public PSCredential SourceForestCredential
		{
			get
			{
				return (PSCredential)base.Fields["SourceForestCredential"];
			}
			set
			{
				base.Fields["SourceForestCredential"] = value;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06004731 RID: 18225 RVA: 0x001243C1 File Offset: 0x001225C1
		// (set) Token: 0x06004732 RID: 18226 RVA: 0x001243D8 File Offset: 0x001225D8
		[Parameter(Mandatory = false)]
		public Fqdn PreferredSourceFqdn
		{
			get
			{
				return (Fqdn)base.Fields["PreferredSourceFqdn"];
			}
			set
			{
				base.Fields["PreferredSourceFqdn"] = value;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x001243EB File Offset: 0x001225EB
		// (set) Token: 0x06004734 RID: 18228 RVA: 0x00124402 File Offset: 0x00122602
		[Parameter(Mandatory = false)]
		public bool? DeleteConfig
		{
			get
			{
				return (bool?)base.Fields["DeleteConfig"];
			}
			set
			{
				base.Fields["DeleteConfig"] = value;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x0012441A File Offset: 0x0012261A
		// (set) Token: 0x06004736 RID: 18230 RVA: 0x00124431 File Offset: 0x00122631
		[Parameter(Mandatory = true)]
		public string TargetForestDomainController
		{
			get
			{
				return (string)base.Fields["TargetForestDomainController"];
			}
			set
			{
				base.Fields["TargetForestDomainController"] = value;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x00124444 File Offset: 0x00122644
		// (set) Token: 0x06004738 RID: 18232 RVA: 0x0012445B File Offset: 0x0012265B
		[Parameter(Mandatory = false)]
		public PSCredential TargetForestCredential
		{
			get
			{
				return (PSCredential)base.Fields["TargetForestCredential"];
			}
			set
			{
				base.Fields["TargetForestCredential"] = value;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x0012446E File Offset: 0x0012266E
		// (set) Token: 0x0600473A RID: 18234 RVA: 0x0012448F File Offset: 0x0012268F
		[Parameter]
		public bool MultipleExchangeDeployments
		{
			get
			{
				return (bool)(base.Fields["MultipleExchangeDeployments"] ?? false);
			}
			set
			{
				base.Fields["MultipleExchangeDeployments"] = value;
			}
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x001244A8 File Offset: 0x001226A8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			try
			{
				this.sourceConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, (this.SourceForestCredential == null) ? null : this.SourceForestCredential.GetNetworkCredential(), ADSessionSettings.FromRootOrgScopeSet(), 153, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\AutoDiscover\\ExportAutodiscoverConfig.cs");
				this.serverConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 161, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\AutoDiscover\\ExportAutodiscoverConfig.cs");
				string path = "LDAP://" + this.TargetForestDomainController + "/rootDSE";
				DirectoryEntry directoryEntry = null;
				if (this.TargetForestCredential == null)
				{
					directoryEntry = new DirectoryEntry(path);
				}
				else
				{
					try
					{
						string password = this.TargetForestCredential.Password.ConvertToUnsecureString();
						directoryEntry = new DirectoryEntry(path, this.TargetForestCredential.UserName.Replace("\\\\", "\\"), password);
					}
					catch (DirectoryServicesCOMException exception)
					{
						base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, null);
					}
				}
				try
				{
					directoryEntry.AuthenticationType = (AuthenticationTypes.Signing | AuthenticationTypes.Sealing);
					string str = directoryEntry.Properties["configurationNamingContext"][0] as string;
					this.targetServiceContainer = "CN=Services," + str;
				}
				catch (COMException exception2)
				{
					base.ThrowTerminatingError(exception2, ErrorCategory.InvalidArgument, null);
				}
				path = "LDAP://" + this.TargetForestDomainController + "/" + this.targetServiceContainer;
				directoryEntry = null;
				if (this.TargetForestCredential == null)
				{
					directoryEntry = new DirectoryEntry(path);
				}
				else
				{
					try
					{
						string password2 = this.TargetForestCredential.Password.ConvertToUnsecureString();
						directoryEntry = new DirectoryEntry(path, this.TargetForestCredential.UserName.Replace("\\\\", "\\"), password2);
					}
					catch (DirectoryServicesCOMException exception3)
					{
						base.ThrowTerminatingError(exception3, ErrorCategory.InvalidArgument, null);
					}
				}
				directoryEntry.AuthenticationType = (AuthenticationTypes.Signing | AuthenticationTypes.Sealing);
				try
				{
					if (this.mea == null)
					{
						this.mea = directoryEntry.Children.Add("CN=Microsoft Exchange Autodiscover", "container");
						this.mea.CommitChanges();
					}
				}
				catch (DirectoryServicesCOMException)
				{
				}
				catch (UnauthorizedAccessException)
				{
					this.WriteWarning(Strings.EADCInsufficientRights("Export-AutoDiscoverConfig"));
				}
				try
				{
					this.mea = directoryEntry.Children.Find("CN=Microsoft Exchange Autodiscover");
				}
				catch (DirectoryServicesCOMException)
				{
				}
				catch (UnauthorizedAccessException)
				{
					this.WriteWarning(Strings.EADCInsufficientRights("Export-AutoDiscoverConfig"));
				}
				if (this.mea == null)
				{
					base.WriteError(new InvalidOperationException(Strings.EADCInsufficientRights("Export-AutoDiscoverConfig")), ErrorCategory.InvalidOperation, null);
				}
				this.sourceServiceContainer = this.sourceConfigSession.GetServicesContainer();
				ServicesContainer servicesContainer = this.serverConfigSession.GetServicesContainer();
				if (servicesContainer.Id.Equals(this.sourceServiceContainer.Id))
				{
					this.inSource = true;
				}
				if (servicesContainer.Id.Equals(this.targetServiceContainer))
				{
					this.inTarget = true;
				}
			}
			catch (PSArgumentException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
			}
			catch (LocalizedException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00124850 File Offset: 0x00122A50
		protected override void InternalValidate()
		{
			if (this.inSource && this.inTarget)
			{
				base.WriteError(new InvalidOperationException(Strings.ExportAutoDiscoverSameForest), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x0012487C File Offset: 0x00122A7C
		protected override void InternalProcessRecord()
		{
			try
			{
				this.DeleteCurrentSCP();
				if (this.DeleteConfig == null || this.DeleteConfig == false)
				{
					this.CreateNewSCP();
				}
			}
			catch (DirectoryServicesCOMException)
			{
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (UnauthorizedAccessException)
			{
				this.WriteWarning(Strings.EADCInsufficientRights("Export-AutoDiscoverConfig"));
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00124910 File Offset: 0x00122B10
		private void CreateNewSCP()
		{
			DirectoryEntry directoryEntry = this.mea.Children.Add(this.ConfigCN(), "ServiceConnectionPoint");
			directoryEntry.Properties["Keywords"].Add("67661d7F-8FC4-4fa7-BFAC-E1D7794C1F68");
			if (this.MultipleExchangeDeployments)
			{
				ADPagedReader<AcceptedDomain> adpagedReader = this.sourceConfigSession.FindAllPaged<AcceptedDomain>();
				int num = 0;
				if (adpagedReader != null)
				{
					foreach (AcceptedDomain acceptedDomain in adpagedReader)
					{
						num++;
						if (acceptedDomain.DomainType == AcceptedDomainType.Authoritative)
						{
							directoryEntry.Properties["Keywords"].Add("Domain=" + acceptedDomain.DomainName.Domain);
						}
					}
				}
				if (num == 0)
				{
					this.WriteWarning(Strings.EADCWeakSourceCreds);
				}
			}
			directoryEntry.Properties["ServiceBindingInformation"].Value = "LDAP://" + this.SourceForestFqdn();
			directoryEntry.CommitChanges();
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00124A18 File Offset: 0x00122C18
		private void DeleteCurrentSCP()
		{
			try
			{
				DirectoryEntry entry = this.mea.Children.Find(this.ConfigCN());
				this.mea.Children.Remove(entry);
			}
			catch (DirectoryServicesCOMException)
			{
			}
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x00124A64 File Offset: 0x00122C64
		private string ConfigCN()
		{
			if (this.configCN == null)
			{
				this.configCN = "CN=" + this.SourceForestFqdn();
			}
			return this.configCN;
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00124A8C File Offset: 0x00122C8C
		private string SourceForestFqdn()
		{
			string result = string.Empty;
			if (this.PreferredSourceFqdn != null)
			{
				result = this.PreferredSourceFqdn.ToString();
			}
			else if (this.inSource)
			{
				result = ADForest.GetLocalForest(this.sourceConfigSession.DomainController).Fqdn;
			}
			else
			{
				result = NativeHelpers.CanonicalNameFromDistinguishedName(this.sourceServiceContainer.Id.Parent.Parent.DistinguishedName);
			}
			return result;
		}

		// Token: 0x04002B15 RID: 11029
		private ITopologyConfigurationSession sourceConfigSession;

		// Token: 0x04002B16 RID: 11030
		private ServicesContainer sourceServiceContainer;

		// Token: 0x04002B17 RID: 11031
		private string targetServiceContainer;

		// Token: 0x04002B18 RID: 11032
		private ITopologyConfigurationSession serverConfigSession;

		// Token: 0x04002B19 RID: 11033
		private bool inSource;

		// Token: 0x04002B1A RID: 11034
		private bool inTarget;

		// Token: 0x04002B1B RID: 11035
		private DirectoryEntry mea;

		// Token: 0x04002B1C RID: 11036
		private string configCN;
	}
}
