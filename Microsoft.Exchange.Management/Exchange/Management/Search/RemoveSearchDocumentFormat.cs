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
	// Token: 0x0200015A RID: 346
	[Cmdlet("Remove", "SearchDocumentFormat", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class RemoveSearchDocumentFormat : DataAccessTask<Server>
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0003A1DA File Offset: 0x000383DA
		// (set) Token: 0x06000C98 RID: 3224 RVA: 0x0003A1F1 File Offset: 0x000383F1
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0003A204 File Offset: 0x00038404
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x0003A21B File Offset: 0x0003841B
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

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003A22E File Offset: 0x0003842E
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 61, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ContentIndex\\RemoveSearchDocumentFormat.cs");
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0003A258 File Offset: 0x00038458
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveSearchDocumentFormat;
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003A260 File Offset: 0x00038460
		protected override void InternalProcessRecord()
		{
			try
			{
				DocumentFormatManager documentFormatManager = new DocumentFormatManager((this.Server == null) ? "localhost" : this.Server.Fqdn);
				documentFormatManager.RemoveFormat(this.Identity.ToString());
			}
			catch (PerformingFastOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003A2BC File Offset: 0x000384BC
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server)));
			}
		}
	}
}
