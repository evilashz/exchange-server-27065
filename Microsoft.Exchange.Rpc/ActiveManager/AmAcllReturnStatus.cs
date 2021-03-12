using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.ActiveManager
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	internal sealed class AmAcllReturnStatus
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x00001ED0 File Offset: 0x000012D0
		public AmAcllReturnStatus()
		{
			this.m_noLoss = false;
			this.m_mountAllowed = false;
			this.m_mountDialOverrideUsed = false;
			this.m_lastLogGenShipped = -1L;
			this.m_lastLogGenNotified = -1L;
			this.m_numLogsLost = -1L;
			this.m_lastInspectedLogTime = DateTime.MinValue;
			this.m_lastErrorMsg = null;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00001F38 File Offset: 0x00001338
		public sealed override string ToString()
		{
			object[] array = new object[8];
			array[0] = this.m_noLoss;
			array[1] = this.m_mountAllowed;
			array[2] = this.m_mountDialOverrideUsed;
			array[3] = this.m_lastLogGenShipped;
			array[4] = this.m_lastLogGenNotified;
			array[5] = this.m_numLogsLost;
			array[6] = this.m_lastInspectedLogTime;
			string lastErrorMsg;
			if (this.m_lastErrorMsg == null)
			{
				lastErrorMsg = AmAcllReturnStatus.s_StringNone;
			}
			else
			{
				lastErrorMsg = this.m_lastErrorMsg;
			}
			array[7] = lastErrorMsg;
			return string.Format(AmAcllReturnStatus.s_ToStringFormat, array);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00001FE0 File Offset: 0x000013E0
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00001FF4 File Offset: 0x000013F4
		public bool NoLoss
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_noLoss;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_noLoss = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00002008 File Offset: 0x00001408
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0000201C File Offset: 0x0000141C
		public bool MountAllowed
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_mountAllowed;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_mountAllowed = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00002030 File Offset: 0x00001430
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00002044 File Offset: 0x00001444
		public bool MountDialOverrideUsed
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.m_mountDialOverrideUsed;
			}
			[param: MarshalAs(UnmanagedType.U1)]
			set
			{
				this.m_mountDialOverrideUsed = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00002058 File Offset: 0x00001458
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0000206C File Offset: 0x0000146C
		public long LastLogGenerationShipped
		{
			get
			{
				return this.m_lastLogGenShipped;
			}
			set
			{
				this.m_lastLogGenShipped = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00002080 File Offset: 0x00001480
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00002094 File Offset: 0x00001494
		public long LastLogGenerationNotified
		{
			get
			{
				return this.m_lastLogGenNotified;
			}
			set
			{
				this.m_lastLogGenNotified = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x000020A8 File Offset: 0x000014A8
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x000020BC File Offset: 0x000014BC
		public long NumberOfLogsLost
		{
			get
			{
				return this.m_numLogsLost;
			}
			set
			{
				this.m_numLogsLost = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x000020D0 File Offset: 0x000014D0
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x000020E8 File Offset: 0x000014E8
		public DateTime LastInspectedLogTime
		{
			get
			{
				return this.m_lastInspectedLogTime;
			}
			set
			{
				this.m_lastInspectedLogTime = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000020FC File Offset: 0x000014FC
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00002110 File Offset: 0x00001510
		public string LastError
		{
			get
			{
				return this.m_lastErrorMsg;
			}
			set
			{
				this.m_lastErrorMsg = value;
			}
		}

		// Token: 0x0400094E RID: 2382
		private bool m_noLoss;

		// Token: 0x0400094F RID: 2383
		private bool m_mountAllowed;

		// Token: 0x04000950 RID: 2384
		private bool m_mountDialOverrideUsed;

		// Token: 0x04000951 RID: 2385
		private long m_lastLogGenShipped;

		// Token: 0x04000952 RID: 2386
		private long m_lastLogGenNotified;

		// Token: 0x04000953 RID: 2387
		private long m_numLogsLost;

		// Token: 0x04000954 RID: 2388
		private DateTime m_lastInspectedLogTime;

		// Token: 0x04000955 RID: 2389
		private string m_lastErrorMsg;

		// Token: 0x04000956 RID: 2390
		private static string s_StringNone = "<none>";

		// Token: 0x04000957 RID: 2391
		private static string s_ToStringFormat = "AmAcllReturnStatus: [NoLoss={0}, MountAllowed={1}, MountDialOverrideUsed={2}, LastLogGenerationShipped={3}, LastLogGenerationNotified={4}, NumberOfLogsLost={5}, LastInspectedLogTime={6}, LastError={7}]";

		// Token: 0x04000958 RID: 2392
		public const long InvalidLogGeneration = -1L;
	}
}
