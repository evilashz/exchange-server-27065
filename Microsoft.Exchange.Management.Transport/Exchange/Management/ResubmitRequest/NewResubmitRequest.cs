using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.MailSubmission;
using Microsoft.Exchange.Transport.MessageRepository;

namespace Microsoft.Exchange.Management.ResubmitRequest
{
	// Token: 0x02000081 RID: 129
	[Cmdlet("Add", "ResubmitRequest", SupportsShouldProcess = true, DefaultParameterSetName = "MDBResubmitParams", ConfirmImpact = ConfirmImpact.Medium)]
	public sealed class NewResubmitRequest : NewTaskBase<ResubmitRequest>
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00011158 File Offset: 0x0000F358
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001116F File Offset: 0x0000F36F
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00011182 File Offset: 0x0000F382
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0001118A File Offset: 0x0000F38A
		[Parameter(Mandatory = true)]
		public DateTime StartTime { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00011193 File Offset: 0x0000F393
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0001119B File Offset: 0x0000F39B
		[Parameter(Mandatory = true)]
		public DateTime EndTime { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000111A4 File Offset: 0x0000F3A4
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x000111AC File Offset: 0x0000F3AC
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UnresponsivePrimaryServers { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000111B5 File Offset: 0x0000F3B5
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000111BD File Offset: 0x0000F3BD
		[Parameter(Mandatory = false, ParameterSetName = "MDBResubmitParams")]
		public Guid Destination { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000111C6 File Offset: 0x0000F3C6
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000111CE File Offset: 0x0000F3CE
		[Parameter(Mandatory = false)]
		public Guid CorrelationId { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000111D7 File Offset: 0x0000F3D7
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x000111DF File Offset: 0x0000F3DF
		[Parameter(Mandatory = false)]
		public bool TestOnly { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000111E8 File Offset: 0x0000F3E8
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000111F0 File Offset: 0x0000F3F0
		[Parameter(Mandatory = false, ParameterSetName = "ConditionalResubmitParams")]
		public string Recipient { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000111F9 File Offset: 0x0000F3F9
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00011201 File Offset: 0x0000F401
		[Parameter(Mandatory = false, ParameterSetName = "ConditionalResubmitParams")]
		public string Sender { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001120C File Offset: 0x0000F40C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.AddResubmitRequestConfirmation(this.StartTime.ToString(), this.EndTime.ToString(), this.Destination.ToString());
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001125A File Offset: 0x0000F45A
		protected override IConfigDataProvider CreateSession()
		{
			return new ResubmitRequestDataProvider(this.Server, new ResubmitRequestId(0L));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011270 File Offset: 0x0000F470
		protected override IConfigurable PrepareDataObject()
		{
			return ResubmitRequest.Create(0L, string.Empty, this.StartTime, this.Destination.ToString(), null, this.EndTime, DateTime.UtcNow, 0);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000112B0 File Offset: 0x0000F4B0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.Server == null)
			{
				this.Server = new ServerIdParameter();
			}
			if (this.StartTime >= this.EndTime)
			{
				base.WriteError(new LocalizedException(Strings.InvalidStartAndEndTime), ErrorCategory.InvalidArgument, null);
			}
			if (this.Destination.Equals(Guid.Empty) && !this.TryCreateConditionalString(out this.conditionalString))
			{
				base.WriteError(new LocalizedException(Strings.InvalidParameterSet), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001133C File Offset: 0x0000F53C
		protected override void InternalProcessRecord()
		{
			try
			{
				MessageResubmissionRpcClientImpl messageResubmissionRpcClientImpl = new MessageResubmissionRpcClientImpl(this.Server.Fqdn);
				string[] unresponsivePrimaryServers = (this.UnresponsivePrimaryServers != null) ? this.UnresponsivePrimaryServers.ToArray() : null;
				byte[] reservedBytes = null;
				if (this.TestOnly)
				{
					MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
					mdbefPropertyCollection[65547U] = true;
					reservedBytes = mdbefPropertyCollection.GetBytes();
				}
				AddResubmitRequestStatus addResubmitRequestStatus;
				if (string.IsNullOrEmpty(this.conditionalString))
				{
					addResubmitRequestStatus = messageResubmissionRpcClientImpl.AddMdbResubmitRequest((this.CorrelationId == Guid.Empty) ? Guid.NewGuid() : this.CorrelationId, new Guid(this.DataObject.Destination), this.StartTime.ToUniversalTime().Ticks, this.EndTime.ToUniversalTime().Ticks, unresponsivePrimaryServers, reservedBytes);
				}
				else
				{
					addResubmitRequestStatus = messageResubmissionRpcClientImpl.AddConditionalResubmitRequest((this.CorrelationId == Guid.Empty) ? Guid.NewGuid() : this.CorrelationId, this.StartTime.ToUniversalTime().Ticks, this.EndTime.ToUniversalTime().Ticks, this.conditionalString, unresponsivePrimaryServers, reservedBytes);
				}
				if (addResubmitRequestStatus != AddResubmitRequestStatus.Success)
				{
					base.WriteError(new LocalizedException(Strings.AddResubmitRequestFailed(addResubmitRequestStatus)), ErrorCategory.NotSpecified, null);
				}
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

		// Token: 0x0600047F RID: 1151 RVA: 0x00011510 File Offset: 0x0000F710
		private bool TryCreateConditionalString(out string conditionalString)
		{
			bool result = false;
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(this.Recipient) && SmtpAddress.IsValidSmtpAddress(this.Recipient))
			{
				result = true;
			}
			dictionary.Add("toAddress", this.Recipient);
			if (!string.IsNullOrEmpty(this.Sender) && SmtpAddress.IsValidSmtpAddress(this.Sender))
			{
				result = true;
			}
			dictionary.Add("fromAddress", this.Sender);
			foreach (string text in dictionary.Keys)
			{
				if (!string.IsNullOrEmpty(dictionary[text]))
				{
					stringBuilder.AppendFormat("{0}={1};", text, dictionary[text]);
				}
			}
			conditionalString = stringBuilder.ToString();
			return result;
		}

		// Token: 0x04000184 RID: 388
		private string conditionalString;
	}
}
