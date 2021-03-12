using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200000C RID: 12
	public class Canary15DataSegment
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00005522 File Offset: 0x00003722
		internal static byte[] BackupKey
		{
			get
			{
				return Canary15DataSegment.adObjectIdsBinary;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000552C File Offset: 0x0000372C
		static Canary15DataSegment()
		{
			Canary15Trace.TraceDateTime(Canary15DataSegment.UtcNow, 0, "Canary15DataSegment().UtcNow.");
			Canary15Trace.TraceTimeSpan(Canary15DataSegment.defaultRefreshPeriod, 1, "Canary15DataSegment().defaultRefreshPeriod.");
			Canary15Trace.TraceTimeSpan(Canary15DataSegment.ReplicationDuration, 2, "Canary15DataSegment().ReplicationDuration.");
			Canary15DataSegment.topoConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 119, ".cctor", "f:\\15.00.1497\\sources\\dev\\clients\\src\\common\\Canary15DataSegment.cs");
			Canary15DataSegment.adClientAccessObjectId = Canary15DataSegment.topoConfigSession.GetClientAccessContainerId();
			Canary15DataSegment.LoadClientAccessADObject();
			byte[] array = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest().ObjectGuid.ToByteArray();
			byte[] array2 = Canary15DataSegment.topoConfigSession.GetDatabasesContainerId().ObjectGuid.ToByteArray();
			Canary15DataSegment.adObjectIdsBinary = new byte[array.Length + array2.Length];
			array.CopyTo(Canary15DataSegment.adObjectIdsBinary, 0);
			array2.CopyTo(Canary15DataSegment.adObjectIdsBinary, array.Length);
			if (Canary15Trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				using (SHA256Cng sha256Cng = new SHA256Cng())
				{
					byte[] bytes = sha256Cng.ComputeHash(Canary15DataSegment.adObjectIdsBinary);
					Canary15Trace.TraceDebug(2L, "adObjectIdsBinaryHash:{0}", new object[]
					{
						Canary15DataSegment.GetHexString(bytes)
					});
					sha256Cng.Clear();
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00005684 File Offset: 0x00003884
		private Canary15DataSegment(int segmentIndex)
		{
			this.segmentIndex = segmentIndex;
			switch (segmentIndex)
			{
			case 0:
				this.adPropertyDefinition = ContainerSchema.CanaryData0;
				return;
			case 1:
				this.adPropertyDefinition = ContainerSchema.CanaryData1;
				return;
			default:
				this.adPropertyDefinition = ContainerSchema.CanaryData2;
				return;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000056D3 File Offset: 0x000038D3
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000056DB File Offset: 0x000038DB
		public int SegmentIndex
		{
			get
			{
				return this.segmentIndex;
			}
			set
			{
				this.segmentIndex = value;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000056E4 File Offset: 0x000038E4
		internal static Canary15DataSegment CreateFromADData(int index)
		{
			Canary15DataSegment canary15DataSegment = new Canary15DataSegment(index);
			canary15DataSegment.ReadSegmentFromAD();
			canary15DataSegment.Trace(0, "CreateFromADData()");
			return canary15DataSegment;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000570C File Offset: 0x0000390C
		internal static Canary15DataSegment Create(int index, long startTime, long period, int numberOfEntries)
		{
			Canary15DataSegment canary15DataSegment = new Canary15DataSegment(index);
			canary15DataSegment.Init(startTime, period, numberOfEntries, Canary15DataSegment.ReplicationDuration.Ticks);
			canary15DataSegment.LogToIIS(7);
			return canary15DataSegment;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000573C File Offset: 0x0000393C
		internal static Canary15DataSegment CreateFromLegacyData(int index, long startTime, long period, long replicationDuration)
		{
			Canary15DataSegment canary15DataSegment = new Canary15DataSegment(index);
			int num = Canary15DataSegment.adObjectIdsBinary.Length;
			canary15DataSegment.header = new Canary15DataSegment.DataSegmentHeader(startTime, startTime, startTime, period, 1, num, replicationDuration);
			canary15DataSegment.data = new byte[1][];
			canary15DataSegment.data[0] = new byte[num];
			Canary15DataSegment.adObjectIdsBinary.CopyTo(canary15DataSegment.data[0], 0);
			canary15DataSegment.header.ComputeState(canary15DataSegment.data);
			canary15DataSegment.Trace(0, "CreateFromLegacyData()");
			return canary15DataSegment;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000057B5 File Offset: 0x000039B5
		internal static long UtcNowTicks
		{
			get
			{
				return Canary15DataSegment.utcNowTicks;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000057BC File Offset: 0x000039BC
		internal static DateTime UtcNow
		{
			get
			{
				return new DateTime(Canary15DataSegment.utcNowTicks, DateTimeKind.Utc);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000057C9 File Offset: 0x000039C9
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000057D1 File Offset: 0x000039D1
		internal Canary15DataSegment.DataSegmentHeader Header
		{
			get
			{
				return this.header;
			}
			set
			{
				this.header = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000057DA File Offset: 0x000039DA
		internal bool IsNull
		{
			get
			{
				return this.header.State == Canary15DataSegment.SegmentState.Null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000057EA File Offset: 0x000039EA
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000057F7 File Offset: 0x000039F7
		internal Canary15DataSegment.SegmentState State
		{
			get
			{
				return this.header.State;
			}
			set
			{
				this.header.State = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00005808 File Offset: 0x00003A08
		internal DateTime NextRefreshTime
		{
			get
			{
				Canary15DataSegment.SegmentState state = this.State;
				if (state == Canary15DataSegment.SegmentState.Pending)
				{
					return this.Header.ReadyTime;
				}
				return Canary15DataSegment.UtcNow + Canary15DataSegment.defaultRefreshPeriod;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000583C File Offset: 0x00003A3C
		internal static void SampleUtcNow()
		{
			Canary15DataSegment.utcNowTicks = DateTime.UtcNow.Ticks;
			Canary15Trace.TraceDateTime(Canary15DataSegment.UtcNow, 0, "SampleUtcNow()");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000586B File Offset: 0x00003A6B
		internal static void LoadClientAccessADObject()
		{
			Canary15DataSegment.adClientAccessContainer = Canary15DataSegment.topoConfigSession.Read<Container>(Canary15DataSegment.adClientAccessObjectId);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00005884 File Offset: 0x00003A84
		internal void CloneFromSegment(Canary15DataSegment segment)
		{
			long startTime = segment.header.EndTime.Ticks + 1L;
			this.Init(startTime, segment.header.Period.Ticks, segment.Header.NumberOfEntries, segment.header.ReplicationDuration.Ticks);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000058E0 File Offset: 0x00003AE0
		internal void ReadSegmentFromAD()
		{
			byte[] array = (byte[])Canary15DataSegment.adClientAccessContainer[this.adPropertyDefinition];
			Canary15Trace.TraceByteArray(0, "ReadSegmentFromAD", array);
			this.header = new Canary15DataSegment.DataSegmentHeader(array, Canary15DataSegment.UtcNowTicks);
			if ((this.header.Bits & Canary15DataSegment.SegmentFlags.InvalidHeader) != Canary15DataSegment.SegmentFlags.InvalidHeader && this.header.Bits != Canary15DataSegment.SegmentFlags.None)
			{
				int num = this.header.HeaderSize;
				int entrySize = this.header.EntrySize;
				int numberOfEntries = this.header.NumberOfEntries;
				this.data = new byte[numberOfEntries][];
				for (int i = 0; i < numberOfEntries; i++)
				{
					this.data[i] = new byte[entrySize];
					Array.Copy(array, num, this.data[i], 0, entrySize);
					num += entrySize;
				}
				this.header.Bits |= Canary15DataSegment.SegmentFlags.Data;
			}
			this.header.ComputeState(this.data);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000059C9 File Offset: 0x00003BC9
		internal void MarkADSegmentForDeletion()
		{
			Canary15DataSegment.adClientAccessContainer[this.adPropertyDefinition] = null;
			this.data = null;
			this.header.Bits = Canary15DataSegment.SegmentFlags.None;
			this.header.State = Canary15DataSegment.SegmentState.Null;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000059FC File Offset: 0x00003BFC
		internal void SaveSegmentToAD()
		{
			if (this.header.State != Canary15DataSegment.SegmentState.New)
			{
				this.Trace(0, "SaveSegmentToAD().Skip");
				this.LogToIIS(60);
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(this.header.HeaderSize + this.header.DataSize))
			{
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				this.header.Serialize(binaryWriter);
				for (int i = 0; i < this.header.NumberOfEntries; i++)
				{
					binaryWriter.Write(this.data[i]);
				}
				Canary15DataSegment.adClientAccessContainer[this.adPropertyDefinition] = memoryStream.ToArray();
			}
			Canary15DataSegment.topoConfigSession.Save(Canary15DataSegment.adClientAccessContainer);
			this.header.State = Canary15DataSegment.SegmentState.Pending;
			this.LogToIIS(61);
			this.Trace(1, "SaveSegmentToAD().Done");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005AE4 File Offset: 0x00003CE4
		internal bool FindEntry(long timestamp, out byte[] entry, out long index)
		{
			long num = timestamp - this.header.StartTime.Ticks;
			long num2 = Canary15DataSegment.UtcNowTicks - this.header.EndTime.Ticks;
			bool result = num - num2 > 0L;
			index = -1L;
			if (this.data != null && this.data.Length > 0 && (this.State == Canary15DataSegment.SegmentState.Active || this.State == Canary15DataSegment.SegmentState.History) && num >= 0L)
			{
				long num3 = num / this.header.Period.Ticks;
				if (num3 >= (long)this.data.Length)
				{
					num3 = (long)(this.data.Length - 1);
					result = true;
				}
				entry = this.data[(int)(checked((IntPtr)num3))];
				index = num3;
				return result;
			}
			entry = null;
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005BA0 File Offset: 0x00003DA0
		private static string GetHexString(byte[] bytes)
		{
			if (!Canary15Trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				return null;
			}
			if (bytes == null)
			{
				return "NULL_BYTE_ARRAY";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005BF4 File Offset: 0x00003DF4
		private void Init(long startTime, long period, int numberOfEntries, long replicationDuration)
		{
			if (numberOfEntries > 0)
			{
				using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
				{
					int num = aesCryptoServiceProvider.Key.Length + aesCryptoServiceProvider.IV.Length;
					long ticks = (Canary15DataSegment.UtcNow + Canary15DataSegment.ReplicationDuration).Ticks;
					if (startTime < ticks)
					{
						startTime = ticks;
					}
					this.header = new Canary15DataSegment.DataSegmentHeader(startTime, ticks, Canary15DataSegment.UtcNowTicks, period, numberOfEntries, num, replicationDuration);
					this.data = new byte[numberOfEntries][];
					for (int i = 0; i < numberOfEntries; i++)
					{
						aesCryptoServiceProvider.GenerateKey();
						aesCryptoServiceProvider.GenerateIV();
						this.data[i] = new byte[num];
						aesCryptoServiceProvider.Key.CopyTo(this.data[i], 0);
						aesCryptoServiceProvider.IV.CopyTo(this.data[i], aesCryptoServiceProvider.Key.Length);
					}
					this.header.ComputeState(this.data);
					this.Trace(0, "Init()");
				}
			}
		}

		// Token: 0x040001D7 RID: 471
		private const int NumberOfSegments = 3;

		// Token: 0x040001D8 RID: 472
		public static TimeSpan ReplicationDuration = new TimeSpan(28, 0, 0, 0);

		// Token: 0x040001D9 RID: 473
		private static byte[] adObjectIdsBinary;

		// Token: 0x040001DA RID: 474
		private static ITopologyConfigurationSession topoConfigSession;

		// Token: 0x040001DB RID: 475
		private static Container adClientAccessContainer;

		// Token: 0x040001DC RID: 476
		private static ADObjectId adClientAccessObjectId;

		// Token: 0x040001DD RID: 477
		private static long utcNowTicks = DateTime.UtcNow.Ticks;

		// Token: 0x040001DE RID: 478
		private static TimeSpan defaultRefreshPeriod = new TimeSpan(24, 0, 0);

		// Token: 0x040001DF RID: 479
		private int segmentIndex;

		// Token: 0x040001E0 RID: 480
		private Canary15DataSegment.DataSegmentHeader header;

		// Token: 0x040001E1 RID: 481
		private ADPropertyDefinition adPropertyDefinition;

		// Token: 0x040001E2 RID: 482
		private byte[][] data;

		// Token: 0x0200000D RID: 13
		[Flags]
		internal enum SegmentFlags
		{
			// Token: 0x040001E4 RID: 484
			None = 0,
			// Token: 0x040001E5 RID: 485
			Header = 1,
			// Token: 0x040001E6 RID: 486
			Data = 2,
			// Token: 0x040001E7 RID: 487
			InvalidHeader = 4,
			// Token: 0x040001E8 RID: 488
			InvalidData = 8
		}

		// Token: 0x0200000E RID: 14
		internal enum SegmentState
		{
			// Token: 0x040001EA RID: 490
			Null,
			// Token: 0x040001EB RID: 491
			New,
			// Token: 0x040001EC RID: 492
			Propagated,
			// Token: 0x040001ED RID: 493
			History,
			// Token: 0x040001EE RID: 494
			Active,
			// Token: 0x040001EF RID: 495
			Pending,
			// Token: 0x040001F0 RID: 496
			Expired,
			// Token: 0x040001F1 RID: 497
			Invalid
		}

		// Token: 0x0200000F RID: 15
		internal class DataSegmentHeader
		{
			// Token: 0x0600005B RID: 91 RVA: 0x00005CF4 File Offset: 0x00003EF4
			internal DataSegmentHeader(long startTime, long readyTime, long readTime, long period, int numberOfEntries, int entrySize, long replicationDuration)
			{
				this.segmentFlags = Canary15DataSegment.SegmentFlags.Header;
				this.version = 0;
				this.headerSize = 56;
				this.entrySize = entrySize;
				this.numberOfEntries = numberOfEntries;
				this.period = period;
				this.startTime = startTime;
				this.endTime = this.startTime + (long)this.numberOfEntries * this.period;
				this.readyTime = readyTime;
				this.readTime = readTime;
				this.replicationDuration = replicationDuration;
				this.Trace(0, "DataSegmentHeader()");
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00005D7C File Offset: 0x00003F7C
			internal DataSegmentHeader(byte[] headerByteArray, long readtime)
			{
				this.Trace(1, "DateSegmentHeader().BeforeDeserialize");
				this.segmentState = Canary15DataSegment.SegmentState.Null;
				this.readTime = readtime;
				if (headerByteArray != null && headerByteArray.Length > 0)
				{
					this.segmentFlags |= Canary15DataSegment.SegmentFlags.Header;
					using (MemoryStream memoryStream = new MemoryStream(headerByteArray))
					{
						BinaryReader binaryReader = new BinaryReader(memoryStream);
						this.version = binaryReader.ReadInt32();
						this.headerSize = binaryReader.ReadInt32();
						this.entrySize = binaryReader.ReadInt32();
						this.numberOfEntries = binaryReader.ReadInt32();
						this.period = binaryReader.ReadInt64();
						this.startTime = binaryReader.ReadInt64();
						this.endTime = binaryReader.ReadInt64();
						this.readyTime = binaryReader.ReadInt64();
						this.replicationDuration = binaryReader.ReadInt64();
					}
					this.ValidateHeader();
				}
				else
				{
					this.segmentFlags = Canary15DataSegment.SegmentFlags.None;
				}
				this.Trace(1, "DataSegmentHeader().AfterDeserialize");
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600005D RID: 93 RVA: 0x00005E78 File Offset: 0x00004078
			internal int Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00005E80 File Offset: 0x00004080
			internal int HeaderSize
			{
				get
				{
					return this.headerSize;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00005E88 File Offset: 0x00004088
			internal int EntrySize
			{
				get
				{
					return this.entrySize;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00005E90 File Offset: 0x00004090
			internal DateTime StartTime
			{
				get
				{
					return new DateTime(this.startTime, DateTimeKind.Utc);
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00005E9E File Offset: 0x0000409E
			internal DateTime EndTime
			{
				get
				{
					return new DateTime(this.endTime, DateTimeKind.Utc);
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00005EAC File Offset: 0x000040AC
			internal DateTime ReadTime
			{
				get
				{
					return new DateTime(this.readTime, DateTimeKind.Utc);
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000063 RID: 99 RVA: 0x00005EBA File Offset: 0x000040BA
			internal DateTime ReadyTime
			{
				get
				{
					return new DateTime(this.readyTime, DateTimeKind.Utc);
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000064 RID: 100 RVA: 0x00005EC8 File Offset: 0x000040C8
			internal TimeSpan ReplicationDuration
			{
				get
				{
					return new TimeSpan(this.replicationDuration);
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000065 RID: 101 RVA: 0x00005ED5 File Offset: 0x000040D5
			internal TimeSpan Period
			{
				get
				{
					return new TimeSpan(this.period);
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000066 RID: 102 RVA: 0x00005EE2 File Offset: 0x000040E2
			internal int NumberOfEntries
			{
				get
				{
					return this.numberOfEntries;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000067 RID: 103 RVA: 0x00005EEA File Offset: 0x000040EA
			// (set) Token: 0x06000068 RID: 104 RVA: 0x00005EF2 File Offset: 0x000040F2
			internal Canary15DataSegment.SegmentFlags Bits
			{
				get
				{
					return this.segmentFlags;
				}
				set
				{
					this.segmentFlags = value;
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000069 RID: 105 RVA: 0x00005EFB File Offset: 0x000040FB
			// (set) Token: 0x0600006A RID: 106 RVA: 0x00005F03 File Offset: 0x00004103
			internal Canary15DataSegment.SegmentState State
			{
				get
				{
					return this.segmentState;
				}
				set
				{
					this.segmentState = value;
				}
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600006B RID: 107 RVA: 0x00005F0C File Offset: 0x0000410C
			internal int DataSize
			{
				get
				{
					return this.NumberOfEntries * this.EntrySize;
				}
			}

			// Token: 0x0600006C RID: 108 RVA: 0x00005F1C File Offset: 0x0000411C
			internal void Serialize(BinaryWriter binaryWriter)
			{
				this.Trace(2, "Serialize()");
				binaryWriter.Write(this.version);
				binaryWriter.Write(this.headerSize);
				binaryWriter.Write(this.entrySize);
				binaryWriter.Write(this.numberOfEntries);
				binaryWriter.Write(this.period);
				binaryWriter.Write(this.startTime);
				binaryWriter.Write(this.endTime);
				binaryWriter.Write(this.readyTime);
				binaryWriter.Write(this.replicationDuration);
			}

			// Token: 0x0600006D RID: 109 RVA: 0x00005FA4 File Offset: 0x000041A4
			internal void ComputeState(byte[][] data)
			{
				this.TraceState(0, "ComputeState()");
				if ((this.Bits & (Canary15DataSegment.SegmentFlags.InvalidHeader | Canary15DataSegment.SegmentFlags.InvalidData)) != Canary15DataSegment.SegmentFlags.None)
				{
					this.State = Canary15DataSegment.SegmentState.Invalid;
					return;
				}
				if (data == null || data.Length == 0)
				{
					this.State = Canary15DataSegment.SegmentState.Null;
					this.Bits -= 2;
					this.TraceState(1, "ComputeState()");
					return;
				}
				this.Bits |= Canary15DataSegment.SegmentFlags.Data;
				if (this.readTime < this.readyTime)
				{
					this.State = Canary15DataSegment.SegmentState.New;
				}
				else if (this.startTime > Canary15DataSegment.UtcNowTicks)
				{
					this.State = Canary15DataSegment.SegmentState.Propagated;
				}
				else
				{
					this.State = Canary15DataSegment.SegmentState.Active;
				}
				this.TraceState(2, "ComputeState()");
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00006048 File Offset: 0x00004248
			private void ValidateHeader()
			{
				this.TraceState(0, "ValidateHeader()");
				if ((this.Bits & Canary15DataSegment.SegmentFlags.InvalidHeader) == Canary15DataSegment.SegmentFlags.InvalidHeader)
				{
					return;
				}
				this.Bits |= Canary15DataSegment.SegmentFlags.InvalidHeader;
				if (this.Version < 0)
				{
					return;
				}
				if (this.startTime >= this.endTime || this.startTime < this.readyTime)
				{
					return;
				}
				this.Bits &= ~Canary15DataSegment.SegmentFlags.InvalidHeader;
				this.TraceState(1, "ValidateHeader()");
			}

			// Token: 0x040001F2 RID: 498
			private const int DefaultVersion = 0;

			// Token: 0x040001F3 RID: 499
			private readonly int version;

			// Token: 0x040001F4 RID: 500
			private readonly int headerSize;

			// Token: 0x040001F5 RID: 501
			private readonly int entrySize;

			// Token: 0x040001F6 RID: 502
			private readonly int numberOfEntries;

			// Token: 0x040001F7 RID: 503
			private readonly long period;

			// Token: 0x040001F8 RID: 504
			private readonly long startTime;

			// Token: 0x040001F9 RID: 505
			private readonly long endTime;

			// Token: 0x040001FA RID: 506
			private readonly long readyTime;

			// Token: 0x040001FB RID: 507
			private readonly long replicationDuration;

			// Token: 0x040001FC RID: 508
			private readonly long readTime;

			// Token: 0x040001FD RID: 509
			private Canary15DataSegment.SegmentFlags segmentFlags;

			// Token: 0x040001FE RID: 510
			private Canary15DataSegment.SegmentState segmentState;
		}
	}
}
