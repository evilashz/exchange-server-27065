using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C5 RID: 2501
	[Cmdlet("Set", "ClientAccessServer", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetClientAccessServer : SetTopologySystemConfigurationObjectTask<ClientAccessServerIdParameter, ClientAccessServer, Server>
	{
		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x00175648 File Offset: 0x00173848
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetClientAccessServer(this.Identity.ToString());
			}
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x06005911 RID: 22801 RVA: 0x0017565A File Offset: 0x0017385A
		// (set) Token: 0x06005912 RID: 22802 RVA: 0x00175671 File Offset: 0x00173871
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Uri AutoDiscoverServiceInternalUri
		{
			get
			{
				return (Uri)base.Fields["AutoDiscoverServiceInternalUri"];
			}
			set
			{
				base.Fields["AutoDiscoverServiceInternalUri"] = value;
			}
		}

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x00175684 File Offset: 0x00173884
		// (set) Token: 0x06005914 RID: 22804 RVA: 0x0017569B File Offset: 0x0017389B
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public MultiValuedProperty<string> AutoDiscoverSiteScope
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["AutoDiscoverSiteScope"];
			}
			set
			{
				base.Fields["AutoDiscoverSiteScope"] = value;
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x06005915 RID: 22805 RVA: 0x001756AE File Offset: 0x001738AE
		// (set) Token: 0x06005916 RID: 22806 RVA: 0x001756C5 File Offset: 0x001738C5
		[ValidateNotNull]
		[Parameter(Mandatory = false, ParameterSetName = "AlternateServiceAccount")]
		public PSCredential[] AlternateServiceAccountCredential
		{
			get
			{
				return (PSCredential[])base.Fields["AlternateServiceAccountCredential"];
			}
			set
			{
				base.Fields["AlternateServiceAccountCredential"] = value;
			}
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x06005917 RID: 22807 RVA: 0x001756D8 File Offset: 0x001738D8
		// (set) Token: 0x06005918 RID: 22808 RVA: 0x001756FE File Offset: 0x001738FE
		[Parameter(Mandatory = false, ParameterSetName = "AlternateServiceAccount")]
		public SwitchParameter CleanUpInvalidAlternateServiceAccountCredentials
		{
			get
			{
				return (SwitchParameter)(base.Fields["CleanUpInvalidAlternateServiceAccountCredentials"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CleanUpInvalidAlternateServiceAccountCredentials"] = value;
			}
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x00175716 File Offset: 0x00173916
		// (set) Token: 0x0600591A RID: 22810 RVA: 0x0017573C File Offset: 0x0017393C
		[Parameter(Mandatory = false, ParameterSetName = "AlternateServiceAccount")]
		public SwitchParameter RemoveAlternateServiceAccountCredentials
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveAlternateServiceAccountCredentials"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveAlternateServiceAccountCredentials"] = value;
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x0600591B RID: 22811 RVA: 0x00175754 File Offset: 0x00173954
		// (set) Token: 0x0600591C RID: 22812 RVA: 0x0017576B File Offset: 0x0017396B
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public ClientAccessArrayIdParameter Array
		{
			get
			{
				return (ClientAccessArrayIdParameter)base.Fields["ClientAccessArray"];
			}
			set
			{
				base.Fields["ClientAccessArray"] = value;
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0017577E File Offset: 0x0017397E
		// (set) Token: 0x0600591E RID: 22814 RVA: 0x00175786 File Offset: 0x00173986
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override ClientAccessServerIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x00175790 File Offset: 0x00173990
		internal static void EnsureRunningOnTargetServer(Task task, Server targetServer)
		{
			if (!targetServer.Id.Equals(LocalServer.GetServer().Id))
			{
				task.WriteError(new InvalidOperationException(Strings.CannotManipulateAlternateServiceAccountsRemotely(LocalServer.GetServer().Fqdn, targetServer.Fqdn)), ErrorCategory.InvalidOperation, targetServer.Identity);
			}
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x001757E0 File Offset: 0x001739E0
		protected override void InternalStateReset()
		{
			this.alternateServiceAccountConfiguration = null;
			this.alternateServiceAccountCredentialsToRemove.Clear();
			base.InternalStateReset();
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x001757FC File Offset: 0x001739FC
		protected override IConfigurable PrepareDataObject()
		{
			Server server = (Server)base.PrepareDataObject();
			if (base.ParameterSetName == "AlternateServiceAccount")
			{
				if (this.NeedAlternateServiceAccountPasswords)
				{
					SetClientAccessServer.EnsureRunningOnTargetServer(this, server);
					this.alternateServiceAccountConfiguration = AlternateServiceAccountConfiguration.LoadWithPasswordsFromRegistry();
				}
				else
				{
					this.alternateServiceAccountConfiguration = AlternateServiceAccountConfiguration.LoadFromRegistry(server.Fqdn);
				}
			}
			return server;
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x00175858 File Offset: 0x00173A58
		protected sealed override void InternalValidate()
		{
			if (this.Instance.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServerNameModified), ErrorCategory.InvalidOperation, this.Identity);
			}
			base.InternalValidate();
			if (base.Fields.IsModified("AutoDiscoverServiceInternalUri") && this.AutoDiscoverServiceInternalUri != null && (!this.AutoDiscoverServiceInternalUri.IsWellFormedOriginalString() || !Uri.IsWellFormedUriString(this.AutoDiscoverServiceInternalUri.ToString(), UriKind.Absolute)))
			{
				base.WriteError(new ArgumentException(Strings.AutoDiscoverUrlIsBad, "AutoDiscoverServiceInternalUri"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			if (base.ParameterSetName == "AlternateServiceAccount")
			{
				if (this.CleanUpInvalidAlternateServiceAccountCredentials.ToBool() && this.RemoveAlternateServiceAccountCredentials.ToBool())
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCleanUpAndRemoveAlternateServiceAccountsAreMutuallyExclusive), ErrorCategory.SyntaxError, this.DataObject.Identity);
				}
				if (this.CleanUpInvalidAlternateServiceAccountCredentials.ToBool())
				{
					AlternateServiceAccountCredential[] array = this.alternateServiceAccountConfiguration.AllCredentials.ToArray<AlternateServiceAccountCredential>();
					ICollection<string> collection = new HashSet<string>(Microsoft.Exchange.Data.Directory.Management.AlternateServiceAccountCredential.UserNameComparer);
					foreach (AlternateServiceAccountCredential alternateServiceAccountCredential in array)
					{
						if (base.Stopping)
						{
							break;
						}
						bool flag = !collection.Contains(alternateServiceAccountCredential.QualifiedUserName);
						if (flag)
						{
							SecurityStatus securityStatus = SecurityStatus.DecryptFailure;
							flag &= alternateServiceAccountCredential.IsValid;
							if (flag)
							{
								base.WriteVerbose(Strings.VerboseValidatingAlternateServiceAccountCredential(alternateServiceAccountCredential.QualifiedUserName, alternateServiceAccountCredential.WhenAdded.Value));
								flag &= alternateServiceAccountCredential.TryAuthenticate(out securityStatus);
							}
							if (!flag)
							{
								base.WriteVerbose(Strings.VerboseFoundInvalidAlternateServiceAccountCredential(alternateServiceAccountCredential.QualifiedUserName, alternateServiceAccountCredential.WhenAdded ?? DateTime.MinValue, securityStatus.ToString()));
							}
						}
						if (flag)
						{
							base.WriteVerbose(Strings.VerboseFoundValidAlternateServiceAccountCredential(alternateServiceAccountCredential.QualifiedUserName, alternateServiceAccountCredential.WhenAdded.Value));
							collection.Add(alternateServiceAccountCredential.QualifiedUserName);
						}
						else
						{
							this.alternateServiceAccountCredentialsToRemove.Add(alternateServiceAccountCredential);
						}
					}
					if (array.Length > 0 && array.Length == this.alternateServiceAccountCredentialsToRemove.Count)
					{
						this.WriteWarning(Strings.AllAlternateServiceAccountCredentialsAreInvalidOnCleanup(this.DataObject.Fqdn));
						this.alternateServiceAccountCredentialsToRemove.Clear();
					}
				}
			}
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x00175ABC File Offset: 0x00173CBC
		protected sealed override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			if (this.DataObject.MajorVersion != LocalServer.GetServer().MajorVersion)
			{
				base.WriteError(new CannotModifyCrossVersionObjectException(this.DataObject.Id.DistinguishedName), ErrorCategory.InvalidOperation, null);
				return;
			}
			ClientAccessServer clientAccessServer = new ClientAccessServer(this.DataObject);
			if (base.Fields.IsModified("ClientAccessArray"))
			{
				ClientAccessArray clientAccessArrayFromIdParameter = this.GetClientAccessArrayFromIdParameter();
				if (clientAccessArrayFromIdParameter == null)
				{
					clientAccessServer.ClientAccessArray = null;
				}
				else
				{
					if (clientAccessArrayFromIdParameter.IsPriorTo15ExchangeObjectVersion)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorCannotSetToOldClientAccessArray(clientAccessArrayFromIdParameter.ExchangeVersion.ToString(), ClientAccessArray.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidOperation, this.Identity);
						return;
					}
					clientAccessServer.ClientAccessArray = (ADObjectId)clientAccessArrayFromIdParameter.Identity;
				}
			}
			bool flag = false;
			ADServiceConnectionPoint adserviceConnectionPoint = null;
			ADObjectId childId = clientAccessServer.Id.GetChildId("Protocols").GetChildId("Autodiscover").GetChildId(clientAccessServer.Name);
			if (base.Fields.IsModified("AutoDiscoverServiceInternalUri") && this.AutoDiscoverServiceInternalUri == null && base.Fields.IsModified("AutoDiscoverSiteScope") && this.AutoDiscoverSiteScope == null)
			{
				adserviceConnectionPoint = new ADServiceConnectionPoint();
				adserviceConnectionPoint.SetId(childId);
				base.DataSession.Delete(adserviceConnectionPoint);
				ADObjectId parent = adserviceConnectionPoint.Id.Parent;
				ADContainer adcontainer = new ADContainer();
				adcontainer.SetId(parent);
				base.DataSession.Delete(adcontainer);
				flag = true;
			}
			else
			{
				adserviceConnectionPoint = ((IConfigurationSession)base.DataSession).Read<ADServiceConnectionPoint>(childId);
				if (adserviceConnectionPoint == null)
				{
					adserviceConnectionPoint = new ADServiceConnectionPoint();
					adserviceConnectionPoint.SetId(childId);
					if (!base.Fields.IsModified("AutoDiscoverServiceInternalUri"))
					{
						string text = ComputerInformation.DnsFullyQualifiedDomainName;
						if (string.IsNullOrEmpty(text))
						{
							text = ComputerInformation.DnsPhysicalHostName;
						}
						adserviceConnectionPoint.ServiceBindingInformation.Add("https://" + text + "/Autodiscover/Autodiscover.xml");
					}
					if (!base.Fields.IsModified("AutoDiscoverSiteScope"))
					{
						adserviceConnectionPoint.Keywords.Add("77378F46-2C66-4aa9-A6A6-3E7A48B19596");
						string siteName = NativeHelpers.GetSiteName(false);
						if (!string.IsNullOrEmpty(siteName))
						{
							adserviceConnectionPoint.Keywords.Add("Site=" + siteName);
						}
					}
					adserviceConnectionPoint.ServiceDnsName = ComputerInformation.DnsPhysicalHostName;
					adserviceConnectionPoint.ServiceClassName = "ms-Exchange-AutoDiscover-Service";
					flag = true;
				}
				if (base.Fields.IsModified("AutoDiscoverServiceInternalUri"))
				{
					adserviceConnectionPoint.ServiceBindingInformation.Clear();
					if (this.AutoDiscoverServiceInternalUri != null)
					{
						adserviceConnectionPoint.ServiceBindingInformation.Add(this.AutoDiscoverServiceInternalUri.ToString());
					}
					flag = true;
				}
				if (base.Fields.IsModified("AutoDiscoverSiteScope"))
				{
					adserviceConnectionPoint.Keywords.Clear();
					adserviceConnectionPoint.Keywords.Add("77378F46-2C66-4aa9-A6A6-3E7A48B19596");
					if (this.AutoDiscoverSiteScope != null)
					{
						foreach (string str in this.AutoDiscoverSiteScope)
						{
							adserviceConnectionPoint.Keywords.Add("Site=" + str);
						}
					}
					flag = true;
				}
				if (flag)
				{
					ADObjectId parent2 = adserviceConnectionPoint.Id.Parent;
					if (((IConfigurationSession)base.DataSession).Read<ADContainer>(parent2) == null)
					{
						ADContainer adcontainer2 = new ADContainer();
						adcontainer2.SetId(parent2);
						base.DataSession.Save(adcontainer2);
					}
					base.DataSession.Save(adserviceConnectionPoint);
				}
			}
			bool flag2 = false;
			if (this.CleanUpInvalidAlternateServiceAccountCredentials.ToBool() && this.alternateServiceAccountCredentialsToRemove.Count > 0)
			{
				foreach (AlternateServiceAccountCredential credential in this.alternateServiceAccountCredentialsToRemove)
				{
					this.alternateServiceAccountConfiguration.RemoveCredential(credential);
				}
				flag2 = true;
			}
			if (this.RemoveAlternateServiceAccountCredentials.ToBool())
			{
				flag2 = this.alternateServiceAccountConfiguration.RemoveAllCredentials();
				flag2 = true;
			}
			if (this.AlternateServiceAccountCredential != null)
			{
				for (int i = this.AlternateServiceAccountCredential.Length - 1; i >= 0; i--)
				{
					this.alternateServiceAccountConfiguration.AddCredential(this.AlternateServiceAccountCredential[i]);
					flag2 = true;
				}
			}
			if (this.DataObject.ObjectState != ObjectState.Unchanged)
			{
				base.InternalProcessRecord();
			}
			else if (!flag && !flag2)
			{
				this.WriteWarning(Strings.WarningForceMessage);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x00175F2C File Offset: 0x0017412C
		private bool NeedAlternateServiceAccountPasswords
		{
			get
			{
				return this.CleanUpInvalidAlternateServiceAccountCredentials.ToBool() || this.AlternateServiceAccountCredential != null;
			}
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x00175F58 File Offset: 0x00174158
		private ClientAccessArray GetClientAccessArrayFromIdParameter()
		{
			if (this.Array == null)
			{
				return null;
			}
			IEnumerable<ClientAccessArray> objects = this.Array.GetObjects<ClientAccessArray>(null, base.DataSession);
			ClientAccessArray result;
			using (IEnumerator<ClientAccessArray> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorClientAccessArrayNotFound(this.Array.ToString()));
				}
				ClientAccessArray clientAccessArray = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorClientAccessArrayNotUnique(this.Array.ToString()));
				}
				result = clientAccessArray;
			}
			return result;
		}

		// Token: 0x0400330B RID: 13067
		private const string AutoDiscoverServiceInternalUriTag = "AutoDiscoverServiceInternalUri";

		// Token: 0x0400330C RID: 13068
		private const string AutoDiscoverSiteScopeTag = "AutoDiscoverSiteScope";

		// Token: 0x0400330D RID: 13069
		private const string AlternateServiceAccountParameterSet = "AlternateServiceAccount";

		// Token: 0x0400330E RID: 13070
		private const string AlternateServiceAccountCredentialTag = "AlternateServiceAccountCredential";

		// Token: 0x0400330F RID: 13071
		private const string CleanUpInvalidAlternateServiceAccountCredentialsTag = "CleanUpInvalidAlternateServiceAccountCredentials";

		// Token: 0x04003310 RID: 13072
		private const string ClientAccessArrayTag = "ClientAccessArray";

		// Token: 0x04003311 RID: 13073
		private const string RemoveAlternateServiceAccountCredentialsTag = "RemoveAlternateServiceAccountCredentials";

		// Token: 0x04003312 RID: 13074
		private AlternateServiceAccountConfiguration alternateServiceAccountConfiguration;

		// Token: 0x04003313 RID: 13075
		private readonly List<AlternateServiceAccountCredential> alternateServiceAccountCredentialsToRemove = new List<AlternateServiceAccountCredential>();
	}
}
