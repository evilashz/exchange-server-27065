using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	internal sealed class AmDatabaseMoveResult
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00002250 File Offset: 0x00001650
		private void BuildDebugString()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.AppendFormat("AmDatabaseMoveResult: [ DbGuid='{0}', ", this.m_dbGuid);
			stringBuilder.AppendFormat("DbName='{0}', ", this.m_dbName);
			stringBuilder.AppendFormat("FromServer='{0}', ", this.m_fromServerFqdn);
			stringBuilder.AppendFormat("FinalActiveServer='{0}', ", this.m_finalActiveServerFqdn);
			stringBuilder.AppendFormat("MoveStatus='{0}', ", this.m_dbMoveStatus);
			stringBuilder.AppendFormat("MountStatusAtMoveStart='{0}', ", this.m_dbMountStatusAtStart);
			stringBuilder.AppendFormat("MountStatusAtMoveEnd='{0}', ", this.m_dbMountStatusAtEnd);
			stringBuilder.AppendFormat("ErrorInfo='{0}', ", this.m_errorInfo);
			int num = 0;
			List<AmDbRpcOperationSubStatus> attemptedServerSubStatuses = this.m_attemptedServerSubStatuses;
			if (attemptedServerSubStatuses != null && attemptedServerSubStatuses.Count > 0)
			{
				num = this.m_attemptedServerSubStatuses.Count;
			}
			stringBuilder.AppendFormat("AttemptedServerSubStatuses: [Count='{0}'] ", num);
			stringBuilder.Append("]");
			this.m_debugString = stringBuilder.ToString();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000235C File Offset: 0x0000175C
		public AmDatabaseMoveResult(Guid dbGuid, string dbName, string fromServerFqdn, string finalActiveServerFqdn, AmDbMoveStatus dbMoveStatus, AmDbMountStatus dbMountStatusAtStart, AmDbMountStatus dbMountStatusAtEnd, RpcErrorExceptionInfo errorInfo, List<AmDbRpcOperationSubStatus> attemptedServerSubStatuses)
		{
			this.m_isLegacy = false;
			this.BuildDebugString();
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000023C4 File Offset: 0x000017C4
		public sealed override string ToString()
		{
			return this.m_debugString;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x000023D8 File Offset: 0x000017D8
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x000023EC File Offset: 0x000017EC
		public bool IsLegacy
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_isLegacy;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_isLegacy = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00002400 File Offset: 0x00001800
		public Guid DbGuid
		{
			get
			{
				return this.m_dbGuid;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00002418 File Offset: 0x00001818
		public string DbName
		{
			get
			{
				return this.m_dbName;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000242C File Offset: 0x0000182C
		public string FromServerFqdn
		{
			get
			{
				return this.m_fromServerFqdn;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00002440 File Offset: 0x00001840
		public string FinalActiveServerFqdn
		{
			get
			{
				return this.m_finalActiveServerFqdn;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00002454 File Offset: 0x00001854
		public AmDbMoveStatus MoveStatus
		{
			get
			{
				return this.m_dbMoveStatus;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00002468 File Offset: 0x00001868
		public AmDbMountStatus MountStatusAtStart
		{
			get
			{
				return this.m_dbMountStatusAtStart;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000247C File Offset: 0x0000187C
		public AmDbMountStatus MountStatusAtEnd
		{
			get
			{
				return this.m_dbMountStatusAtEnd;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00002490 File Offset: 0x00001890
		public RpcErrorExceptionInfo ErrorInfo
		{
			get
			{
				return this.m_errorInfo;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000024A4 File Offset: 0x000018A4
		public List<AmDbRpcOperationSubStatus> AttemptedServerSubStatuses
		{
			get
			{
				return this.m_attemptedServerSubStatuses;
			}
		}

		// Token: 0x0400095D RID: 2397
		private readonly Guid m_dbGuid = dbGuid;

		// Token: 0x0400095E RID: 2398
		private readonly string m_dbName = dbName;

		// Token: 0x0400095F RID: 2399
		private readonly string m_fromServerFqdn = fromServerFqdn;

		// Token: 0x04000960 RID: 2400
		private readonly string m_finalActiveServerFqdn = finalActiveServerFqdn;

		// Token: 0x04000961 RID: 2401
		private readonly AmDbMoveStatus m_dbMoveStatus = dbMoveStatus;

		// Token: 0x04000962 RID: 2402
		private readonly AmDbMountStatus m_dbMountStatusAtStart = dbMountStatusAtStart;

		// Token: 0x04000963 RID: 2403
		private readonly AmDbMountStatus m_dbMountStatusAtEnd = dbMountStatusAtEnd;

		// Token: 0x04000964 RID: 2404
		private readonly RpcErrorExceptionInfo m_errorInfo = errorInfo;

		// Token: 0x04000965 RID: 2405
		private readonly List<AmDbRpcOperationSubStatus> m_attemptedServerSubStatuses = attemptedServerSubStatuses;

		// Token: 0x04000966 RID: 2406
		private bool m_isLegacy;

		// Token: 0x04000967 RID: 2407
		private string m_debugString;
	}
}
