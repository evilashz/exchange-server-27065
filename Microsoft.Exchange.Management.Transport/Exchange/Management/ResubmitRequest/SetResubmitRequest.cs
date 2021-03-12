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
	// Token: 0x02000085 RID: 133
	[Cmdlet("Set", "ResubmitRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class SetResubmitRequest : SetObjectWithIdentityTaskBase<ResubmitRequestIdentityParameter, ResubmitRequest, ResubmitRequest>
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00011D1B File Offset: 0x0000FF1B
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00011D32 File Offset: 0x0000FF32
		[ValidateNotNullOrEmpty]
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00011D45 File Offset: 0x0000FF45
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00011D5C File Offset: 0x0000FF5C
		[Parameter(Mandatory = true)]
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

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00011D74 File Offset: 0x0000FF74
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetResubmitRequestConfirmation;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00011D7B File Offset: 0x0000FF7B
		protected override IConfigDataProvider CreateSession()
		{
			return new ResubmitRequestDataProvider(this.Server, (this.Identity == null) ? null : this.Identity.Identity);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00011DA0 File Offset: 0x0000FFA0
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x00011E24 File Offset: 0x00010024
		protected override void InternalProcessRecord()
		{
			this.DataObject.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			if (this.Enabled)
			{
				this.DataObject.State = ResubmitRequestState.Running;
			}
			else
			{
				this.DataObject.State = ResubmitRequestState.Paused;
			}
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
