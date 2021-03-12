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
	// Token: 0x02000159 RID: 345
	[Cmdlet("New", "SearchDocumentFormat", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewSearchDocumentFormat : DataAccessTask<Server>
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00039FD3 File Offset: 0x000381D3
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00039FEA File Offset: 0x000381EA
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

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00039FFD File Offset: 0x000381FD
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0003A014 File Offset: 0x00038214
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Extension
		{
			get
			{
				return (string)base.Fields["Extension"];
			}
			set
			{
				base.Fields["Extension"] = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0003A027 File Offset: 0x00038227
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0003A03E File Offset: 0x0003823E
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0003A051 File Offset: 0x00038251
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0003A068 File Offset: 0x00038268
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string MimeType
		{
			get
			{
				return (string)base.Fields["MimeType"];
			}
			set
			{
				base.Fields["MimeType"] = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0003A07B File Offset: 0x0003827B
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0003A092 File Offset: 0x00038292
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

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0003A0A5 File Offset: 0x000382A5
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0003A0BC File Offset: 0x000382BC
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
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

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003A0D4 File Offset: 0x000382D4
		public NewSearchDocumentFormat()
		{
			this.Enabled = true;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003A0E3 File Offset: 0x000382E3
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 113, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ContentIndex\\NewSearchDocumentFormat.cs");
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003A10D File Offset: 0x0003830D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewSearchDocumentFormat;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003A114 File Offset: 0x00038314
		protected override void InternalProcessRecord()
		{
			try
			{
				DocumentFormatManager documentFormatManager = new DocumentFormatManager((this.Server == null) ? "localhost" : this.Server.Fqdn);
				documentFormatManager.AddFilterBasedFormat(this.Identity.ToString(), this.Name, this.MimeType, this.Extension);
				documentFormatManager.EnableParsing(this.Identity.ToString(), this.Enabled);
			}
			catch (PerformingFastOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003A19C File Offset: 0x0003839C
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				base.GetDataObject<Server>(this.Server, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server)), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server)));
			}
		}
	}
}
