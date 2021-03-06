using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A36 RID: 2614
	public class TestIPListProvider<TIdentity, TProvider> : SystemConfigurationObjectActionTask<TIdentity, TProvider> where TIdentity : IIdentityParameter, new() where TProvider : IPListProvider, new()
	{
		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x06005D63 RID: 23907 RVA: 0x00189798 File Offset: 0x00187998
		// (set) Token: 0x06005D64 RID: 23908 RVA: 0x001897A0 File Offset: 0x001879A0
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
		public IPAddress IPAddress
		{
			get
			{
				return this.ipAddress;
			}
			set
			{
				this.ipAddress = value;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06005D65 RID: 23909 RVA: 0x001897A9 File Offset: 0x001879A9
		// (set) Token: 0x06005D66 RID: 23910 RVA: 0x001897C0 File Offset: 0x001879C0
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x001897D4 File Offset: 0x001879D4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				base.GetType().FullName
			});
			if (this.Server == null)
			{
				try
				{
					this.serverObject = ((ITopologyConfigurationSession)base.DataSession).ReadLocalServer();
					goto IL_A9;
				}
				catch (TransientException exception)
				{
					this.WriteError(exception, ErrorCategory.ResourceUnavailable, this.DataObject, false);
					return;
				}
			}
			this.serverObject = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			if (this.serverObject != null)
			{
				goto IL_A9;
			}
			return;
			IL_A9:
			if (this.serverObject == null || (!this.serverObject.IsHubTransportServer && !this.serverObject.IsEdgeServer))
			{
				this.WriteError(new LocalizedException(Strings.ErrorInvalidServerRole((this.serverObject != null) ? this.serverObject.Name : Environment.MachineName)), ErrorCategory.InvalidOperation, this.serverObject, false);
				return;
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x001898FC File Offset: 0x00187AFC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				base.GetType().FullName
			});
			TestProviderResult<TProvider> testProviderResult = new TestProviderResult<TProvider>();
			testProviderResult.Provider = this.DataObject;
			testProviderResult.Matched = Provider.Query(this.serverObject, testProviderResult.Provider, this.ipAddress, out testProviderResult.ProviderResult);
			base.WriteObject(testProviderResult);
			TaskLogger.LogExit();
		}

		// Token: 0x040034B2 RID: 13490
		private IPAddress ipAddress;

		// Token: 0x040034B3 RID: 13491
		private Server serverObject;
	}
}
