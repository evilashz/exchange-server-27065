using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000074 RID: 116
	public abstract class MessageActionWithIdentity : MessageAction
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000101DE File Offset: 0x0000E3DE
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x000101F5 File Offset: 0x0000E3F5
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = "Identity", Position = 0)]
		public MessageIdentity Identity
		{
			get
			{
				return (MessageIdentity)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00010208 File Offset: 0x0000E408
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001021F File Offset: 0x0000E41F
		protected ServerIdParameter Server
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

		// Token: 0x0600041D RID: 1053 RVA: 0x00010232 File Offset: 0x0000E432
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.Identity != null)
			{
				this.Server = ServerIdParameter.Parse(this.Identity.QueueIdentity.Server);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001026C File Offset: 0x0000E46C
		protected override LocalizedException GetLocalizedException(Exception ex)
		{
			if (ex is QueueViewerException)
			{
				return ErrorMapper.GetLocalizedException((ex as QueueViewerException).ErrorCode, this.Identity, this.Server);
			}
			if (ex is RpcException)
			{
				return ErrorMapper.GetLocalizedException((ex as RpcException).ErrorCode, null, this.Server);
			}
			return null;
		}
	}
}
