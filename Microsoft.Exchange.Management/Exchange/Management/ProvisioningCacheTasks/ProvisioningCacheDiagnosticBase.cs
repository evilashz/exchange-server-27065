using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningCacheTasks
{
	// Token: 0x02000643 RID: 1603
	public abstract class ProvisioningCacheDiagnosticBase : Task
	{
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x000E8445 File Offset: 0x000E6645
		// (set) Token: 0x06003821 RID: 14369 RVA: 0x000E845C File Offset: 0x000E665C
		[Parameter(Mandatory = true, ParameterSetName = "OrganizationCache", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "GlobalCache", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public Fqdn Server
		{
			get
			{
				return (Fqdn)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06003822 RID: 14370 RVA: 0x000E846F File Offset: 0x000E666F
		// (set) Token: 0x06003823 RID: 14371 RVA: 0x000E8486 File Offset: 0x000E6686
		[Parameter(Mandatory = true, ParameterSetName = "OrganizationCache")]
		[Parameter(Mandatory = true, ParameterSetName = "GlobalCache")]
		[ValidateNotNullOrEmpty]
		public string Application
		{
			get
			{
				return (string)base.Fields["Application"];
			}
			set
			{
				base.Fields["Application"] = value;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06003824 RID: 14372 RVA: 0x000E8499 File Offset: 0x000E6699
		// (set) Token: 0x06003825 RID: 14373 RVA: 0x000E84BF File Offset: 0x000E66BF
		[Parameter(Mandatory = true, ParameterSetName = "GlobalCache")]
		public SwitchParameter GlobalCache
		{
			get
			{
				return (SwitchParameter)(base.Fields["GlobalCache"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GlobalCache"] = value;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06003826 RID: 14374 RVA: 0x000E84D7 File Offset: 0x000E66D7
		// (set) Token: 0x06003827 RID: 14375 RVA: 0x000E84EE File Offset: 0x000E66EE
		[Parameter(Mandatory = false, ParameterSetName = "OrganizationCache")]
		public MultiValuedProperty<OrganizationIdParameter> Organizations
		{
			get
			{
				return (MultiValuedProperty<OrganizationIdParameter>)base.Fields["Organizations"];
			}
			set
			{
				base.Fields["Organizations"] = value;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000E8501 File Offset: 0x000E6701
		// (set) Token: 0x06003829 RID: 14377 RVA: 0x000E8527 File Offset: 0x000E6727
		[Parameter(Mandatory = false, ParameterSetName = "OrganizationCache")]
		public SwitchParameter CurrentOrganization
		{
			get
			{
				return (SwitchParameter)(base.Fields["CurrentOrganization"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CurrentOrganization"] = value;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600382A RID: 14378 RVA: 0x000E853F File Offset: 0x000E673F
		// (set) Token: 0x0600382B RID: 14379 RVA: 0x000E8556 File Offset: 0x000E6756
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Guid> CacheKeys
		{
			get
			{
				return (MultiValuedProperty<Guid>)base.Fields["CacheKeys"];
			}
			set
			{
				base.Fields["CacheKeys"] = value;
			}
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000E856C File Offset: 0x000E676C
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			if (!TaskHelper.GetLocalServerFqdn(new Task.TaskWarningLoggingDelegate(this.WriteWarning)).StartsWith(this.Server, StringComparison.OrdinalIgnoreCase))
			{
				string remoteServerFqdn = this.Server.ToString();
				int e15MinVersion = Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.E15MinVersion;
				CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, remoteServerFqdn, e15MinVersion, false, this.ConfirmationMessage, null);
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000E85CC File Offset: 0x000E67CC
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.CheckExclusiveParameters(new object[]
			{
				"Organizations",
				"CurrentOrganization"
			});
			if (!CacheApplicationManager.IsApplicationDefined(this.Application))
			{
				base.WriteError(new ArgumentException(Strings.ErrorApplicationNotDefined(this.Application)), (ErrorCategory)1000, null);
			}
			ICollection<Guid> collection = this.ResolveOrganizations();
			DiagnosticType diagnosticType = this.GetDiagnosticType();
			DiagnosticType command = diagnosticType;
			ICollection<Guid> orgIds = collection;
			ICollection<Guid> entries;
			if (this.CacheKeys != null)
			{
				ICollection<Guid> cacheKeys = this.CacheKeys;
				entries = cacheKeys;
			}
			else
			{
				entries = new List<Guid>();
			}
			DiagnosticCommand diagnosticCommand = new DiagnosticCommand(command, orgIds, entries);
			using (NamedPipeClientStream namedPipeClientStream = this.PrepareClientStream())
			{
				byte[] array = diagnosticCommand.ToSendMessage();
				byte[] array2 = new byte[5000];
				try
				{
					namedPipeClientStream.Write(array, 0, array.Length);
					namedPipeClientStream.Flush();
					int num;
					do
					{
						Array.Clear(array2, 0, array2.Length);
						num = namedPipeClientStream.Read(array2, 0, array2.Length);
						if (num > 0)
						{
							this.ProcessReceivedData(array2, num);
						}
					}
					while (num > 0 && namedPipeClientStream.IsConnected);
				}
				catch (IOException exception)
				{
					base.WriteError(exception, (ErrorCategory)1001, null);
				}
				catch (ObjectDisposedException exception2)
				{
					base.WriteError(exception2, (ErrorCategory)1001, null);
				}
				catch (NotSupportedException exception3)
				{
					base.WriteError(exception3, (ErrorCategory)1001, null);
				}
				catch (InvalidOperationException exception4)
				{
					base.WriteError(exception4, (ErrorCategory)1001, null);
				}
			}
		}

		// Token: 0x0600382E RID: 14382
		protected abstract void ProcessReceivedData(byte[] buffer, int bufLen);

		// Token: 0x0600382F RID: 14383
		internal abstract DiagnosticType GetDiagnosticType();

		// Token: 0x06003830 RID: 14384 RVA: 0x000E875C File Offset: 0x000E695C
		private ICollection<Guid> ResolveOrganizations()
		{
			if (this.GlobalCache)
			{
				return null;
			}
			List<Guid> list = new List<Guid>();
			if (this.CurrentOrganization)
			{
				if (OrganizationId.ForestWideOrgId.Equals(base.ExecutingUserOrganizationId))
				{
					list.Add(Guid.Empty);
				}
				else
				{
					list.Add(base.ExecutingUserOrganizationId.ConfigurationUnit.ObjectGuid);
				}
				return list;
			}
			if (this.GlobalCache || this.Organizations == null)
			{
				return list;
			}
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 258, "ResolveOrganizations", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ProvisioningCache\\ProvisioningCacheDiagnosticBase.cs");
			foreach (OrganizationIdParameter organizationIdParameter in this.Organizations)
			{
				IEnumerable<ExchangeConfigurationUnit> objects = organizationIdParameter.GetObjects<ExchangeConfigurationUnit>(null, session);
				bool flag = true;
				foreach (ExchangeConfigurationUnit exchangeConfigurationUnit in objects)
				{
					flag = false;
					if (!list.Contains(exchangeConfigurationUnit.Guid))
					{
						list.Add(exchangeConfigurationUnit.Guid);
					}
				}
				if (flag)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(organizationIdParameter.ToString())), ExchangeErrorCategory.ServerOperation, null);
				}
			}
			return list;
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000E88C4 File Offset: 0x000E6AC4
		private NamedPipeClientStream PrepareClientStream()
		{
			NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", CacheApplicationManager.GetAppPipeName(this.Application), PipeDirection.InOut);
			try
			{
				namedPipeClientStream.Connect(2000);
			}
			catch (TimeoutException)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorConnectToApplicationTimeout(this.Application, this.Server)), (ErrorCategory)1002, null);
			}
			return namedPipeClientStream;
		}

		// Token: 0x040025C2 RID: 9666
		private const int recvBufSize = 5000;
	}
}
