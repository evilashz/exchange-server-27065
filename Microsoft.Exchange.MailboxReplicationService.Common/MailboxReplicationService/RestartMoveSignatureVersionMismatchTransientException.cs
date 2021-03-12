using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200033D RID: 829
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestartMoveSignatureVersionMismatchTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025EC RID: 9708 RVA: 0x000524D5 File Offset: 0x000506D5
		public RestartMoveSignatureVersionMismatchTransientException(LocalizedString mbxId, uint originalVersion, uint currentVersion) : base(MrsStrings.ReportRestartingMoveBecauseMailboxSignatureVersionIsDifferent(mbxId, originalVersion, currentVersion))
		{
			this.mbxId = mbxId;
			this.originalVersion = originalVersion;
			this.currentVersion = currentVersion;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000524FA File Offset: 0x000506FA
		public RestartMoveSignatureVersionMismatchTransientException(LocalizedString mbxId, uint originalVersion, uint currentVersion, Exception innerException) : base(MrsStrings.ReportRestartingMoveBecauseMailboxSignatureVersionIsDifferent(mbxId, originalVersion, currentVersion), innerException)
		{
			this.mbxId = mbxId;
			this.originalVersion = originalVersion;
			this.currentVersion = currentVersion;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x00052524 File Offset: 0x00050724
		protected RestartMoveSignatureVersionMismatchTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxId = (LocalizedString)info.GetValue("mbxId", typeof(LocalizedString));
			this.originalVersion = (uint)info.GetValue("originalVersion", typeof(uint));
			this.currentVersion = (uint)info.GetValue("currentVersion", typeof(uint));
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0005259C File Offset: 0x0005079C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxId", this.mbxId);
			info.AddValue("originalVersion", this.originalVersion);
			info.AddValue("currentVersion", this.currentVersion);
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000525E9 File Offset: 0x000507E9
		public LocalizedString MbxId
		{
			get
			{
				return this.mbxId;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000525F1 File Offset: 0x000507F1
		public uint OriginalVersion
		{
			get
			{
				return this.originalVersion;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x000525F9 File Offset: 0x000507F9
		public uint CurrentVersion
		{
			get
			{
				return this.currentVersion;
			}
		}

		// Token: 0x04001041 RID: 4161
		private readonly LocalizedString mbxId;

		// Token: 0x04001042 RID: 4162
		private readonly uint originalVersion;

		// Token: 0x04001043 RID: 4163
		private readonly uint currentVersion;
	}
}
