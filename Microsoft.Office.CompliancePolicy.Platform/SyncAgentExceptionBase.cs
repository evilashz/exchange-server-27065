using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class SyncAgentExceptionBase : CompliancePolicyException
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00005DCE File Offset: 0x00003FCE
		public SyncAgentExceptionBase(string message) : this(message, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005DD9 File Offset: 0x00003FD9
		public SyncAgentExceptionBase(string message, Exception innerException) : this(message, innerException, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005DE5 File Offset: 0x00003FE5
		public SyncAgentExceptionBase(SerializationInfo serializationInfo, StreamingContext context) : this(serializationInfo, context, false, SyncAgentErrorCode.Generic)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public SyncAgentExceptionBase(string message, bool isPerObjectException, SyncAgentErrorCode errorCode) : this(message, null, isPerObjectException, errorCode)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005DFD File Offset: 0x00003FFD
		public SyncAgentExceptionBase(string message, Exception innerException, bool isPerObjectException, SyncAgentErrorCode errorCode) : base(message, innerException)
		{
			this.IsPerObjectException = isPerObjectException;
			this.ErrorCode = errorCode;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005E16 File Offset: 0x00004016
		public SyncAgentExceptionBase(SerializationInfo serializationInfo, StreamingContext context, bool isPerObjectException, SyncAgentErrorCode errorCode) : base(serializationInfo, context)
		{
			this.IsPerObjectException = isPerObjectException;
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00005E2F File Offset: 0x0000402F
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00005E37 File Offset: 0x00004037
		public bool IsPerObjectException { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005E40 File Offset: 0x00004040
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00005E48 File Offset: 0x00004048
		public SyncAgentErrorCode ErrorCode { get; private set; }
	}
}
