using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x0200007B RID: 123
	[Cmdlet("Redirect", "Message", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	[LocDescription(QueueViewerStrings.IDs.RedirectMessageTask)]
	public sealed class RedirectMessage : MessageAction
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00010860 File Offset: 0x0000EA60
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00010877 File Offset: 0x0000EA77
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001088A File Offset: 0x0000EA8A
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x000108A1 File Offset: 0x0000EAA1
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<Fqdn> Target
		{
			get
			{
				return (MultiValuedProperty<Fqdn>)base.Fields["Target"];
			}
			set
			{
				base.Fields["Target"] = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000108B4 File Offset: 0x0000EAB4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return QueueViewerStrings.ConfirmationMessageRedirectMessage(string.Join<Fqdn>(",", this.Target));
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000108CC File Offset: 0x0000EACC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 796, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\Queueviewer\\MessageTasks.cs");
			foreach (Fqdn fqdn in this.Target)
			{
				Server server = topologyConfigurationSession.FindServerByFqdn(fqdn);
				if (server == null)
				{
					base.WriteError(new LocalizedException(QueueViewerStrings.UnknownServer(fqdn)), ErrorCategory.InvalidArgument, null);
				}
				else if (!server.IsHubTransportServer)
				{
					base.WriteError(new LocalizedException(QueueViewerStrings.NotTransportHubServer(fqdn)), ErrorCategory.InvalidArgument, null);
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00010994 File Offset: 0x0000EB94
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleMessageInfo> queueViewerClient = new QueueViewerClient<ExtensibleMessageInfo>((string)this.Server))
			{
				queueViewerClient.RedirectMessage(this.Target);
				base.WriteVerbose(QueueViewerStrings.SuccessMessageRedirectMessageRequestCompleted);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000109E8 File Offset: 0x0000EBE8
		protected override LocalizedException GetLocalizedException(Exception ex)
		{
			if (ex is QueueViewerException)
			{
				return ErrorMapper.GetLocalizedException((ex as QueueViewerException).ErrorCode, null, this.Server);
			}
			if (ex is RpcException)
			{
				return ErrorMapper.GetLocalizedException((ex as RpcException).ErrorCode, null, this.Server);
			}
			return null;
		}
	}
}
