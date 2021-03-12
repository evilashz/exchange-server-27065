using System;
using System.ComponentModel;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.MailSubmission;

namespace Microsoft.Exchange.Management.ResubmitRequest
{
	// Token: 0x02000082 RID: 130
	[Cmdlet("Get", "ResubmitRequest", DefaultParameterSetName = "Identity")]
	public sealed class GetResubmitRequest : GetObjectWithIdentityTaskBase<ResubmitRequestIdentityParameter, ResubmitRequest>
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000115F8 File Offset: 0x0000F7F8
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001160F File Offset: 0x0000F80F
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x06000483 RID: 1155 RVA: 0x00011624 File Offset: 0x0000F824
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

		// Token: 0x06000484 RID: 1156 RVA: 0x000116A8 File Offset: 0x0000F8A8
		protected override IConfigDataProvider CreateSession()
		{
			return new ResubmitRequestDataProvider(this.Server, (this.Identity == null) ? null : this.Identity.Identity);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000116CC File Offset: 0x0000F8CC
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

		// Token: 0x06000486 RID: 1158 RVA: 0x0001173C File Offset: 0x0000F93C
		internal static void ProcessRpcError(RpcException rpcException, string server, Task task)
		{
			LocalizedException exception;
			ErrorCategory category;
			if (rpcException.ErrorCode == MailSubmissionServiceRpcClient.EndpointNotRegistered)
			{
				exception = new LocalizedException(Strings.TransportRpcNotRegistered(server));
				category = ErrorCategory.ResourceUnavailable;
			}
			else if (rpcException.ErrorCode == MailSubmissionServiceRpcClient.ServerUnavailable)
			{
				exception = new LocalizedException(Strings.TransportRpcUnavailable(server));
				category = ErrorCategory.InvalidOperation;
			}
			else
			{
				Win32Exception ex = new Win32Exception(rpcException.ErrorCode);
				exception = new LocalizedException(Strings.ResubmitRequestGenericRpcError(ex.Message), ex);
				category = ErrorCategory.InvalidOperation;
			}
			task.WriteError(exception, category, null);
		}
	}
}
