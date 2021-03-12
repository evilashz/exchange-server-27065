using System;
using System.Security.AccessControl;
using Microsoft.Exchange.Rpc.MailSubmission;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x0200013B RID: 315
	internal class MessageResubmissionRpcServerImpl : MailSubmissionServiceRpcServer
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0003342A File Offset: 0x0003162A
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x00033440 File Offset: 0x00031640
		public static IMessageResubmissionRpcServer MessageResubmissionImpl
		{
			get
			{
				IMessageResubmissionRpcServer result;
				if ((result = MessageResubmissionRpcServerImpl.messageResubmissionImpl) == null)
				{
					result = (MessageResubmissionRpcServerImpl.messageResubmissionImpl = Components.MessageResubmissionComponent);
				}
				return result;
			}
			set
			{
				MessageResubmissionRpcServerImpl.messageResubmissionImpl = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00033448 File Offset: 0x00031648
		public ObjectSecurity LocalSystemSecurityDescriptor
		{
			set
			{
				this.m_sdLocalSystemBinaryForm = value.GetSecurityDescriptorBinaryForm();
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00033456 File Offset: 0x00031656
		public override byte[] SubmitMessage(byte[] inBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003345D File Offset: 0x0003165D
		public override SubmissionStatus SubmitMessage(string messageClass, string serverDN, string serverSADN, byte[] mailboxGuid, byte[] entryId, byte[] parentEntryId, byte[] mdbGuid, int eventCounter, byte[] ipAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00033464 File Offset: 0x00031664
		public override SubmissionStatus SubmitDumpsterMessages(string storageGroupDN, long startTime, long endTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003346B File Offset: 0x0003166B
		public override MailSubmissionResult SubmitMessage2(string serverDN, Guid mailboxGuid, Guid mdbGuid, int eventCounter, byte[] entryId, byte[] parentEntryId, string serverFQDN, byte[] ipAddress)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00033472 File Offset: 0x00031672
		public override bool QueryDumpsterStats(string storageGroupDN, ref long ticksOfOldestDeliveryTime, ref long queueSize, ref int numberOfItems)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00033479 File Offset: 0x00031679
		public override byte[] ShadowHeartBeat(byte[] inBlob)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00033480 File Offset: 0x00031680
		public override AddResubmitRequestStatus AddMdbResubmitRequest(Guid requestGuid, Guid mdbGuid, long startTime, long endTime, string[] unresponsivePrimaryServers, byte[] reservedBytes)
		{
			return MessageResubmissionRpcServerImpl.MessageResubmissionImpl.AddMdbResubmitRequest(requestGuid, mdbGuid, startTime, endTime, unresponsivePrimaryServers, reservedBytes);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00033495 File Offset: 0x00031695
		public override AddResubmitRequestStatus AddConditionalResubmitRequest(Guid requestGuid, long startTime, long endTime, string conditions, string[] unresponsivePrimaryServers, byte[] reservedBytes)
		{
			return MessageResubmissionRpcServerImpl.MessageResubmissionImpl.AddConditionalResubmitRequest(requestGuid, startTime, endTime, conditions, unresponsivePrimaryServers, reservedBytes);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x000334AA File Offset: 0x000316AA
		public override byte[] GetResubmitRequest(byte[] requestRaw)
		{
			return MessageResubmissionRpcServerImpl.MessageResubmissionImpl.GetResubmitRequest(requestRaw);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000334B7 File Offset: 0x000316B7
		public override byte[] SetResubmitRequest(byte[] requestRaw)
		{
			return MessageResubmissionRpcServerImpl.MessageResubmissionImpl.SetResubmitRequest(requestRaw);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000334C4 File Offset: 0x000316C4
		public override byte[] RemoveResubmitRequest(byte[] requestRaw)
		{
			return MessageResubmissionRpcServerImpl.MessageResubmissionImpl.RemoveResubmitRequest(requestRaw);
		}

		// Token: 0x040005FA RID: 1530
		private static IMessageResubmissionRpcServer messageResubmissionImpl;
	}
}
