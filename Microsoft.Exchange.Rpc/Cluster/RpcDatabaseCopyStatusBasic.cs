using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	internal class RpcDatabaseCopyStatusBasic
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00018680 File Offset: 0x00017A80
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator ==(RpcDatabaseCopyStatusBasic left, RpcDatabaseCopyStatusBasic right)
		{
			return object.ReferenceEquals(left, right) || (left != null && right != null && (left.m_dbGuid == right.m_dbGuid && <Module>.StringEqual(left.m_dbName, right.m_dbName) && <Module>.StringEqual(left.m_mailboxServer, right.m_mailboxServer) && <Module>.StringEqual(left.m_activeDatabaseCopy, right.m_activeDatabaseCopy) && left.m_statusRetrievedTime == right.m_statusRetrievedTime && left.m_lastLogGeneratedTime == right.m_lastLogGeneratedTime && left.m_dataProtectionTime == right.m_dataProtectionTime && left.m_dataAvailabilityTime == right.m_dataAvailabilityTime && left.m_lastLogGenerated == right.m_lastLogGenerated && left.m_lastLogCopied == right.m_lastLogCopied && left.m_lastLogInspected == right.m_lastLogInspected && left.m_lastLogReplayed == right.m_lastLogReplayed && left.m_serverVersion == right.m_serverVersion && left.m_copyStatus == right.m_copyStatus && left.m_ciCurrentness == right.m_ciCurrentness));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00005794 File Offset: 0x00004B94
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool operator !=(RpcDatabaseCopyStatusBasic left, RpcDatabaseCopyStatusBasic right)
		{
			return ((!(left == right)) ? 1 : 0) != 0;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000057AC File Offset: 0x00004BAC
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x000057C4 File Offset: 0x00004BC4
		public Guid DBGuid
		{
			get
			{
				return this.m_dbGuid;
			}
			set
			{
				this.m_dbGuid = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000057D8 File Offset: 0x00004BD8
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x000057EC File Offset: 0x00004BEC
		public string DBName
		{
			get
			{
				return this.m_dbName;
			}
			set
			{
				this.m_dbName = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00005800 File Offset: 0x00004C00
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x00005814 File Offset: 0x00004C14
		public string MailboxServer
		{
			get
			{
				return this.m_mailboxServer;
			}
			set
			{
				this.m_mailboxServer = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00005828 File Offset: 0x00004C28
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0000583C File Offset: 0x00004C3C
		public string ActiveDatabaseCopy
		{
			get
			{
				return this.m_activeDatabaseCopy;
			}
			set
			{
				this.m_activeDatabaseCopy = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00005850 File Offset: 0x00004C50
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00005868 File Offset: 0x00004C68
		public DateTime StatusRetrievedTime
		{
			get
			{
				return this.m_statusRetrievedTime;
			}
			set
			{
				this.m_statusRetrievedTime = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000587C File Offset: 0x00004C7C
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00005894 File Offset: 0x00004C94
		public DateTime LastLogGeneratedTime
		{
			get
			{
				return this.m_lastLogGeneratedTime;
			}
			set
			{
				this.m_lastLogGeneratedTime = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000058A8 File Offset: 0x00004CA8
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000058C0 File Offset: 0x00004CC0
		public DateTime DataProtectionTime
		{
			get
			{
				return this.m_dataProtectionTime;
			}
			set
			{
				this.m_dataProtectionTime = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000058D4 File Offset: 0x00004CD4
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x000058EC File Offset: 0x00004CEC
		public DateTime DataAvailabilityTime
		{
			get
			{
				return this.m_dataAvailabilityTime;
			}
			set
			{
				this.m_dataAvailabilityTime = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00005900 File Offset: 0x00004D00
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x00005914 File Offset: 0x00004D14
		public long LastLogGenerated
		{
			get
			{
				return this.m_lastLogGenerated;
			}
			set
			{
				this.m_lastLogGenerated = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00005928 File Offset: 0x00004D28
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0000593C File Offset: 0x00004D3C
		public long LastLogCopied
		{
			get
			{
				return this.m_lastLogCopied;
			}
			set
			{
				this.m_lastLogCopied = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00005950 File Offset: 0x00004D50
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00005964 File Offset: 0x00004D64
		public long LastLogInspected
		{
			get
			{
				return this.m_lastLogInspected;
			}
			set
			{
				this.m_lastLogInspected = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00005978 File Offset: 0x00004D78
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0000598C File Offset: 0x00004D8C
		public long LastLogReplayed
		{
			get
			{
				return this.m_lastLogReplayed;
			}
			set
			{
				this.m_lastLogReplayed = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000059A0 File Offset: 0x00004DA0
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x000059B4 File Offset: 0x00004DB4
		public int ServerVersion
		{
			get
			{
				return this.m_serverVersion;
			}
			set
			{
				this.m_serverVersion = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x000059C8 File Offset: 0x00004DC8
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x000059DC File Offset: 0x00004DDC
		public CopyStatusEnum CopyStatus
		{
			get
			{
				return this.m_copyStatus;
			}
			set
			{
				this.m_copyStatus = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x000059F0 File Offset: 0x00004DF0
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00005A04 File Offset: 0x00004E04
		public ContentIndexCurrentness CICurrentness
		{
			get
			{
				return this.m_ciCurrentness;
			}
			set
			{
				this.m_ciCurrentness = value;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00005A18 File Offset: 0x00004E18
		[return: MarshalAs(UnmanagedType.U1)]
		internal bool IsActiveCopy()
		{
			string activeDatabaseCopy = this.m_activeDatabaseCopy;
			return string.Equals(this.m_mailboxServer, activeDatabaseCopy, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00005A3C File Offset: 0x00004E3C
		internal long GetCopyQueueLength()
		{
			if (this.IsActiveCopy())
			{
				return 0L;
			}
			long lastLogGenerated = this.m_lastLogGenerated;
			long lastLogInspected = this.m_lastLogInspected;
			return Math.Max(0L, lastLogGenerated - lastLogInspected);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00005A6C File Offset: 0x00004E6C
		internal long GetReplayQueueLength()
		{
			long lastLogInspected = this.m_lastLogInspected;
			long lastLogReplayed = this.m_lastLogReplayed;
			return Math.Max(0L, lastLogInspected - lastLogReplayed);
		}

		// Token: 0x0400099E RID: 2462
		protected Guid m_dbGuid;

		// Token: 0x0400099F RID: 2463
		protected string m_dbName;

		// Token: 0x040009A0 RID: 2464
		protected string m_mailboxServer;

		// Token: 0x040009A1 RID: 2465
		protected string m_activeDatabaseCopy;

		// Token: 0x040009A2 RID: 2466
		protected DateTime m_statusRetrievedTime;

		// Token: 0x040009A3 RID: 2467
		protected DateTime m_lastLogGeneratedTime;

		// Token: 0x040009A4 RID: 2468
		protected DateTime m_dataProtectionTime;

		// Token: 0x040009A5 RID: 2469
		protected DateTime m_dataAvailabilityTime;

		// Token: 0x040009A6 RID: 2470
		protected long m_lastLogGenerated;

		// Token: 0x040009A7 RID: 2471
		protected long m_lastLogCopied;

		// Token: 0x040009A8 RID: 2472
		protected long m_lastLogInspected;

		// Token: 0x040009A9 RID: 2473
		protected long m_lastLogReplayed;

		// Token: 0x040009AA RID: 2474
		protected int m_serverVersion;

		// Token: 0x040009AB RID: 2475
		protected CopyStatusEnum m_copyStatus;

		// Token: 0x040009AC RID: 2476
		protected ContentIndexCurrentness m_ciCurrentness;
	}
}
