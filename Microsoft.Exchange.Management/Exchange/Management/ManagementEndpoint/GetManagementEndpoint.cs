using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x0200047D RID: 1149
	[Cmdlet("Get", "ManagementEndpoint")]
	public sealed class GetManagementEndpoint : ManagementEndpointBase
	{
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x0009EC4B File Offset: 0x0009CE4B
		// (set) Token: 0x06002865 RID: 10341 RVA: 0x0009EC62 File Offset: 0x0009CE62
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0009EC75 File Offset: 0x0009CE75
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetManagementEndpointTaskModuleFactory();
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x0009EC7C File Offset: 0x0009CE7C
		protected override string GetRedirectionTemplate()
		{
			if (GetManagementEndpoint.PodTemplate == null)
			{
				string podRedirectionTemplate = ExchangePropertyContainer.GetPodRedirectionTemplate(base.SessionState);
				if (!string.IsNullOrEmpty(podRedirectionTemplate))
				{
					GetManagementEndpoint.PodTemplate = podRedirectionTemplate;
				}
				else
				{
					string text = null;
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeCrossForest\\"))
					{
						if (registryKey != null)
						{
							text = (string)registryKey.GetValue("RemotePowershellManagementEndpointUrlTemplate");
							if (text != null)
							{
								string[] array = text.Split(new char[]
								{
									'/'
								}, StringSplitOptions.RemoveEmptyEntries);
								if (array.Length > 0 && array[1].Contains("{0}"))
								{
									text = string.Format(array[1], "pod{0}");
								}
							}
						}
					}
					GetManagementEndpoint.PodTemplate = (text ?? "{0}");
				}
			}
			return GetManagementEndpoint.PodTemplate;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0009ED44 File Offset: 0x0009CF44
		internal override void ProcessRedirectionEntry(IGlobalDirectorySession session)
		{
			string memberName = string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", this.DomainName);
			string redirectServer = session.GetRedirectServer(memberName);
			if (string.IsNullOrEmpty(redirectServer))
			{
				base.WriteError(new CannotDetermineManagementEndpointException(Strings.ErrorCannotDetermineEndpointForTenant(this.DomainName.ToString())), ErrorCategory.InvalidData, null);
			}
			string remotePowershellUrl = string.Format("https://{0}/PowerShell/", redirectServer);
			ManagementEndpoint managementEndpoint = new ManagementEndpoint(remotePowershellUrl, ManagementEndpointVersion.Version3)
			{
				DomainName = this.DomainName
			};
			Guid guid;
			string resourceForest;
			string accountPartition;
			string smtpNextHopDomain;
			string text;
			bool flag;
			if (((GlsMServDirectorySession.GlsLookupMode != GlsLookupMode.MServOnly && session is GlsMServDirectorySession) || session is GlsDirectorySession) && session.TryGetTenantForestsByDomain(this.DomainName.ToString(), out guid, out resourceForest, out accountPartition, out smtpNextHopDomain, out text, out flag) && Guid.Empty != guid)
			{
				managementEndpoint.AccountPartition = accountPartition;
				managementEndpoint.SmtpNextHopDomain = smtpNextHopDomain;
				managementEndpoint.ResourceForest = resourceForest;
				managementEndpoint.ExternalDirectoryOrganizationId = guid;
			}
			ExTraceGlobals.LogTracer.Information<string, ManagementEndpointVersion>(0L, "Get-ManagementEndPoint URL/Version {0}/{1}", managementEndpoint.RemotePowershellUrl, managementEndpoint.Version);
			base.WriteObject(managementEndpoint);
		}

		// Token: 0x04001DFA RID: 7674
		private const string RemotePowershellUrlTemplate = "https://{0}/PowerShell/";

		// Token: 0x04001DFB RID: 7675
		private const string ExchangeCrossForestKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ExchangeCrossForest\\";

		// Token: 0x04001DFC RID: 7676
		private static string PodTemplate;
	}
}
