using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Rpc.MailSubmission;
using Microsoft.Exchange.Transport.MessageResubmission;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Transport.MessageRepository
{
	// Token: 0x02000139 RID: 313
	internal sealed class MessageResubmissionRpcClientImpl
	{
		// Token: 0x06000DE3 RID: 3555 RVA: 0x000331B3 File Offset: 0x000313B3
		public MessageResubmissionRpcClientImpl(string serverName)
		{
			this.rpcClient = new MessageResubmissionRpcClientImpl.RpcClientFacade(serverName);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x000331C7 File Offset: 0x000313C7
		public MessageResubmissionRpcClientImpl(IMessageResubmissionRpcServer rpcClient = null)
		{
			this.rpcClient = rpcClient;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000331D6 File Offset: 0x000313D6
		public AddResubmitRequestStatus AddMdbResubmitRequest(Guid requestGuid, Guid mdbGuid, long startTimeInTicks, long endTimeInTicks, string[] unresponsivePrimaryServers, byte[] reservedBytes)
		{
			return this.rpcClient.AddMdbResubmitRequest(requestGuid, mdbGuid, startTimeInTicks, endTimeInTicks, unresponsivePrimaryServers, reservedBytes);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000331EC File Offset: 0x000313EC
		public AddResubmitRequestStatus AddConditionalResubmitRequest(Guid requestGuid, long startTimeInTicks, long endTimeInTicks, string conditions, string[] unresponsivePrimaryServers, byte[] reservedBytes)
		{
			return this.rpcClient.AddConditionalResubmitRequest(requestGuid, startTimeInTicks, endTimeInTicks, conditions, unresponsivePrimaryServers, reservedBytes);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00033204 File Offset: 0x00031404
		public ResubmitRequest[] GetResubmitRequest(ResubmitRequestId identity)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ResubmitRequestIdentity"] = ((identity == null) ? null : identity.ToString());
			return (ResubmitRequest[])Serialization.BytesToObject(this.rpcClient.GetResubmitRequest(Serialization.ObjectToBytes(dictionary)));
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003324C File Offset: 0x0003144C
		public void SetResubmitRequest(ResubmitRequestId identity, bool enabled)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ResubmitRequestIdentity"] = identity.ToString();
			dictionary["DumpsterRequestEnabled"] = enabled;
			byte[] mBinaryData = this.rpcClient.SetResubmitRequest(Serialization.ObjectToBytes(dictionary));
			ResubmitRequestResponse output = (ResubmitRequestResponse)Serialization.BytesToObject(mBinaryData);
			MessageResubmissionRpcClientImpl.ThrowExcpetionIfFailure(output, identity.ResubmitRequestRowId);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x000332BC File Offset: 0x000314BC
		public void RemoveResubmitRequest(ResubmitRequestId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ResubmitRequestIdentity"] = identity.ToString();
			byte[] mBinaryData = this.rpcClient.RemoveResubmitRequest(Serialization.ObjectToBytes(dictionary));
			ResubmitRequestResponse output = (ResubmitRequestResponse)Serialization.BytesToObject(mBinaryData);
			MessageResubmissionRpcClientImpl.ThrowExcpetionIfFailure(output, identity.ResubmitRequestRowId);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00033318 File Offset: 0x00031518
		private static void ThrowExcpetionIfFailure(ResubmitRequestResponse output, long requestId)
		{
			if (output.ResponseCode == ResubmitRequestResponseCode.Success)
			{
				return;
			}
			string message;
			switch (output.ResponseCode)
			{
			case ResubmitRequestResponseCode.CannotModifyCompletedRequest:
				message = Strings.CannotModifyCompletedRequest(requestId).ToString();
				break;
			case ResubmitRequestResponseCode.ResubmitRequestDoesNotExist:
				message = Strings.ResubmitRequestDoesNotExist(requestId).ToString();
				break;
			case ResubmitRequestResponseCode.CannotRemoveRequestInRunningState:
				message = Strings.CannotRemoveRequestInRunningState(requestId).ToString();
				break;
			default:
				message = output.ErrorMessage;
				break;
			}
			throw new ResubmitRequestException(output.ResponseCode, message);
		}

		// Token: 0x040005F7 RID: 1527
		private readonly IMessageResubmissionRpcServer rpcClient;

		// Token: 0x0200013A RID: 314
		private class RpcClientFacade : IMessageResubmissionRpcServer
		{
			// Token: 0x06000DEB RID: 3563 RVA: 0x000333A8 File Offset: 0x000315A8
			internal RpcClientFacade(string serverName)
			{
				MailSubmissionServiceRpcClient mailSubmissionServiceRpcClient = new MailSubmissionServiceRpcClient(serverName);
				mailSubmissionServiceRpcClient.SetTimeOut(5000);
				this.rpcClient = mailSubmissionServiceRpcClient;
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x000333D4 File Offset: 0x000315D4
			public byte[] GetResubmitRequest(byte[] rawRequest)
			{
				return this.rpcClient.GetResubmitRequest(rawRequest);
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x000333E2 File Offset: 0x000315E2
			public byte[] SetResubmitRequest(byte[] requestRaw)
			{
				return this.rpcClient.SetResubmitRequest(requestRaw);
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x000333F0 File Offset: 0x000315F0
			public byte[] RemoveResubmitRequest(byte[] requestRaw)
			{
				return this.rpcClient.RemoveResubmitRequest(requestRaw);
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x000333FE File Offset: 0x000315FE
			public AddResubmitRequestStatus AddMdbResubmitRequest(Guid requestGuid, Guid mdbGuid, long startTimeInTicks, long endTimeInTicks, string[] unresponsivePrimaryServers, byte[] reservedBytes)
			{
				return this.rpcClient.AddMdbResubmitRequest(requestGuid, mdbGuid, startTimeInTicks, endTimeInTicks, unresponsivePrimaryServers, reservedBytes);
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x00033414 File Offset: 0x00031614
			public AddResubmitRequestStatus AddConditionalResubmitRequest(Guid requestGuid, long startTimeInTicks, long endTimeInTicks, string conditions, string[] unresponsivePrimaryServers, byte[] reservedBytes)
			{
				return this.rpcClient.AddConditionalResubmitRequest(requestGuid, startTimeInTicks, endTimeInTicks, conditions, unresponsivePrimaryServers, reservedBytes);
			}

			// Token: 0x040005F8 RID: 1528
			private const int ConnectionTimeoutMsec = 5000;

			// Token: 0x040005F9 RID: 1529
			private MailSubmissionServiceRpcClient rpcClient;
		}
	}
}
