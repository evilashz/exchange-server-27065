using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020005C5 RID: 1477
	[Cmdlet("Set", "ServerMonitor", SupportsShouldProcess = true)]
	public sealed class SetServerMonitor : Task
	{
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000D1B4C File Offset: 0x000CFD4C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetServerMonitor(this.Name, this.TargetResource ?? string.Empty, this.Repairing);
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000D1B6E File Offset: 0x000CFD6E
		// (set) Token: 0x060033BF RID: 13247 RVA: 0x000D1B85 File Offset: 0x000CFD85
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
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

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000D1B98 File Offset: 0x000CFD98
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000D1BAF File Offset: 0x000CFDAF
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000D1BC2 File Offset: 0x000CFDC2
		// (set) Token: 0x060033C3 RID: 13251 RVA: 0x000D1BCA File Offset: 0x000CFDCA
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string TargetResource
		{
			get
			{
				return this.targetResource;
			}
			set
			{
				this.targetResource = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000D1BD3 File Offset: 0x000CFDD3
		// (set) Token: 0x060033C5 RID: 13253 RVA: 0x000D1BDB File Offset: 0x000CFDDB
		[Parameter(Mandatory = true)]
		public bool Repairing
		{
			get
			{
				return this.isRepairing;
			}
			set
			{
				this.isRepairing = value;
			}
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000D1BE4 File Offset: 0x000CFDE4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				LocalizedException ex = null;
				try
				{
					RpcSetServerMonitor.Invoke((!string.IsNullOrWhiteSpace(this.Server.Fqdn)) ? this.Server.Fqdn : this.Server.ToString(), this.Name, this.TargetResource, new bool?(this.isRepairing), 30000);
				}
				catch (ActiveMonitoringServerException ex2)
				{
					ex = ex2;
				}
				catch (ActiveMonitoringServerTransientException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					base.WriteError(ex, ExchangeErrorCategory.ServerOperation, null);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x040023EE RID: 9198
		private bool isRepairing;

		// Token: 0x040023EF RID: 9199
		private string targetResource = string.Empty;
	}
}
