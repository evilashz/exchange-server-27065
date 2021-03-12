using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Ceres.ContentEngine.Parsing.Component;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Search.Fast;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000156 RID: 342
	[Cmdlet("Get", "SearchDocumentFormat", DefaultParameterSetName = "Identity")]
	public sealed class GetSearchDocumentFormat : DataAccessTask<Server>
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00039475 File Offset: 0x00037675
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0003948C File Offset: 0x0003768C
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
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

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0003949F File Offset: 0x0003769F
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x000394B6 File Offset: 0x000376B6
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

		// Token: 0x06000C6D RID: 3181 RVA: 0x000394C9 File Offset: 0x000376C9
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 61, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ContentIndex\\GetSearchDocumentFormat.cs");
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000394F3 File Offset: 0x000376F3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageGetSearchDocumentFormat;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000394FC File Offset: 0x000376FC
		protected override void InternalProcessRecord()
		{
			try
			{
				DocumentFormatManager documentFormatManager = new DocumentFormatManager((this.Server == null) ? "localhost" : this.Server.Fqdn);
				if (this.Identity != null)
				{
					base.WriteObject(new SearchDocumentFormatInfoObject(documentFormatManager.GetFormat(this.Identity.ToString())));
				}
				else
				{
					IList<FileFormatInfo> list = documentFormatManager.ListSupportedFormats();
					foreach (FileFormatInfo ffInfo in list)
					{
						base.WriteObject(new SearchDocumentFormatInfoObject(ffInfo));
					}
				}
			}
			catch (PerformingFastOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000395B8 File Offset: 0x000377B8
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server)));
			}
		}
	}
}
