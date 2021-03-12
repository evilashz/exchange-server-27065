using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F0 RID: 1008
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x06003323 RID: 13091 RVA: 0x000C26EC File Offset: 0x000C08EC
		public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
		{
			this.m_id = 0;
			this.m_version = 0;
			this.m_channel = 0;
			this.m_traceloggingId = traceloggingId;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_task = 0;
			this.m_keywords = keywords;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000C2728 File Offset: 0x000C0928
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[]
				{
					1,
					ushort.MaxValue
				}));
			}
			this.m_traceloggingId = 0;
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[]
				{
					1,
					ushort.MaxValue
				}));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000C2819 File Offset: 0x000C0A19
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000C2821 File Offset: 0x000C0A21
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000C2829 File Offset: 0x000C0A29
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000C2831 File Offset: 0x000C0A31
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000C2839 File Offset: 0x000C0A39
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000C2841 File Offset: 0x000C0A41
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000C2849 File Offset: 0x000C0A49
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000C2851 File Offset: 0x000C0A51
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000C2869 File Offset: 0x000C0A69
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000C289C File Offset: 0x000C0A9C
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000C290E File Offset: 0x000C0B0E
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000C2918 File Offset: 0x000C0B18
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x04001676 RID: 5750
		[FieldOffset(0)]
		private int m_traceloggingId;

		// Token: 0x04001677 RID: 5751
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x04001678 RID: 5752
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x04001679 RID: 5753
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x0400167A RID: 5754
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x0400167B RID: 5755
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x0400167C RID: 5756
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x0400167D RID: 5757
		[FieldOffset(8)]
		private long m_keywords;
	}
}
