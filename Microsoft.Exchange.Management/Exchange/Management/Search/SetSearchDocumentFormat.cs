using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Search.Fast;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000161 RID: 353
	[Cmdlet("Set", "SearchDocumentFormat", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetSearchDocumentFormat : DataAccessTask<Server>
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003AE38 File Offset: 0x00039038
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x0003AE4F File Offset: 0x0003904F
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003AE67 File Offset: 0x00039067
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x0003AE7E File Offset: 0x0003907E
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public SearchDocumentFormatId Identity
		{
			get
			{
				return (SearchDocumentFormatId)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0003AE91 File Offset: 0x00039091
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x0003AEA8 File Offset: 0x000390A8
		[Parameter(Mandatory = false)]
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

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0003AEBB File Offset: 0x000390BB
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 72, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ContentIndex\\SetSearchDocumentFormat.cs");
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0003AEE5 File Offset: 0x000390E5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSearchDocumentFormat;
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0003AEEC File Offset: 0x000390EC
		protected override void InternalProcessRecord()
		{
			try
			{
				DocumentFormatManager documentFormatManager = new DocumentFormatManager((this.Server == null) ? "localhost" : this.Server.Fqdn);
				documentFormatManager.EnableParsing(this.Identity.ToString(), this.Enabled);
			}
			catch (PerformingFastOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003AF50 File Offset: 0x00039150
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server)));
			}
		}
	}
}
