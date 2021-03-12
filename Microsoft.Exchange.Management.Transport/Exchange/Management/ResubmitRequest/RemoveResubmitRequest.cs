using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.Management.ResubmitRequest
{
	// Token: 0x02000083 RID: 131
	[Cmdlet("Remove", "ResubmitRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveResubmitRequest : RemoveTaskBase<ResubmitRequestIdentityParameter, ResubmitRequest>
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000117B5 File Offset: 0x0000F9B5
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000117CC File Offset: 0x0000F9CC
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000117DF File Offset: 0x0000F9DF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveResubmiRequestMessage;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000117E6 File Offset: 0x0000F9E6
		protected override IConfigDataProvider CreateSession()
		{
			return new ResubmitRequestDataProvider(this.Server, (this.Identity == null) ? null : this.Identity.Identity);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected override void InternalValidate()
		{
			try
			{
				if (this.Server == null)
				{
					this.Server = new ServerIdParameter();
				}
				base.InternalValidate();
			}
			catch (RpcException rpcException)
			{
				GetResubmitRequest.ProcessRpcError(rpcException, this.Server.Fqdn, this);
			}
			catch (ResubmitRequestException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (LocalizedException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011890 File Offset: 0x0000FA90
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (RpcException rpcException)
			{
				GetResubmitRequest.ProcessRpcError(rpcException, this.Server.Fqdn, this);
			}
			catch (ResubmitRequestException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (LocalizedException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.Client, null);
			}
		}
	}
}
