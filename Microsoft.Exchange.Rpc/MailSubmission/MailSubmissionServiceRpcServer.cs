using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MailSubmission
{
	// Token: 0x02000293 RID: 659
	internal abstract class MailSubmissionServiceRpcServer : RpcServerBase
	{
		// Token: 0x06000C45 RID: 3141
		public abstract byte[] SubmitMessage(byte[] inBlob);

		// Token: 0x06000C46 RID: 3142
		public abstract SubmissionStatus SubmitMessage(string MessageClass, string ServerDN, string ServerSADN, byte[] MailboxGuid, byte[] EntryId, byte[] ParentEntryId, byte[] MdbGuid, int eventCounter, byte[] ipAddress);

		// Token: 0x06000C47 RID: 3143
		public abstract SubmissionStatus SubmitDumpsterMessages(string StorageGroupDN, long startTime, long endTime);

		// Token: 0x06000C48 RID: 3144
		public abstract AddResubmitRequestStatus AddMdbResubmitRequest(Guid requestGuid, Guid MdbGuid, long startTime, long endTime, string[] unresponsivePrimaryServers, byte[] reservedBytes);

		// Token: 0x06000C49 RID: 3145
		public abstract MailSubmissionResult SubmitMessage2(string ServerDN, Guid MailboxGuid, Guid MdbGuid, int eventCounter, byte[] EntryId, byte[] ParentEntryId, string serverFQDN, byte[] ipAddress);

		// Token: 0x06000C4A RID: 3146
		[return: MarshalAs(UnmanagedType.U1)]
		public abstract bool QueryDumpsterStats(string StorageGroupDN, ref long ticksOfOldestDeliveryTime, ref long queueSize, ref int numberOfItems);

		// Token: 0x06000C4B RID: 3147
		public abstract byte[] ShadowHeartBeat(byte[] inBlob);

		// Token: 0x06000C4C RID: 3148
		public abstract byte[] GetResubmitRequest(byte[] inBytes);

		// Token: 0x06000C4D RID: 3149
		public abstract byte[] SetResubmitRequest(byte[] inBytes);

		// Token: 0x06000C4E RID: 3150
		public abstract byte[] RemoveResubmitRequest(byte[] inBytes);

		// Token: 0x06000C4F RID: 3151
		public abstract AddResubmitRequestStatus AddConditionalResubmitRequest(Guid requestGuid, long startTime, long endTime, string conditions, string[] unresponsivePrimaryServers, byte[] reservedBytes);

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002C924 File Offset: 0x0002BD24
		public MailSubmissionServiceRpcServer()
		{
		}

		// Token: 0x04000D4A RID: 3402
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IBridgeheadSubmission_v1_0_s_ifspec;

		// Token: 0x04000D4B RID: 3403
		public byte[] m_sdLocalSystemBinaryForm;

		// Token: 0x04000D4C RID: 3404
		public SubmissionStatus m_rpcStatus;

		// Token: 0x04000D4D RID: 3405
		public static OutOfMemoryException Oome = new OutOfMemoryException();
	}
}
