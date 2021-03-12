using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200000B RID: 11
	public static class Canary15DataManager
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00004DEC File Offset: 0x00002FEC
		static Canary15DataManager()
		{
			Canary15Trace.TraceVersion();
			Canary15Trace.TraceTimeSpan(Canary15DataManager.defaultPeriod, 0, "Canary15DataManager().DefaultPeriod.");
			Canary15DataSegment.SampleUtcNow();
			Canary15DataManager.NextRefreshTime = Canary15DataSegment.UtcNow;
			Canary15DataManager.segments = new Canary15DataSegment[3];
			for (int i = 0; i < 3; i++)
			{
				Canary15DataManager.segments[i] = Canary15DataSegment.CreateFromADData(i);
			}
			bool flag = Canary15DataManager.segments[0].IsNull || Canary15DataManager.segments[1].IsNull || Canary15DataManager.segments[2].IsNull;
			if (Canary15DataManager.segments[0].IsNull || (Canary15DataManager.segments[1].IsNull && !Canary15DataManager.segments[2].IsNull))
			{
				Canary15DataManager.segments[1].MarkADSegmentForDeletion();
				Canary15DataManager.segments[2].MarkADSegmentForDeletion();
				Canary15DataManager.Create(0);
			}
			if (flag)
			{
				long num = 36000000000L;
				long ticks = Canary15DataManager.segments[0].Header.ReplicationDuration.Ticks;
				if (ticks == 0L)
				{
					ticks = Canary15DataSegment.ReplicationDuration.Ticks;
				}
				long num2 = Canary15DataManager.segments[0].Header.StartTime.Ticks - ticks;
				if (num2 > Canary15DataSegment.UtcNow.Ticks)
				{
					num2 = Canary15DataSegment.UtcNow.Ticks;
				}
				Canary15DataManager.CreateFromLegacyData(2, num2 - num, ticks + num, ticks);
				Canary15DataManager.segments[2].LogToIIS(0);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00004F9E File Offset: 0x0000319E
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00004FA5 File Offset: 0x000031A5
		internal static DateTime NextRefreshTime
		{
			get
			{
				return Canary15DataManager.nextRefreshTime;
			}
			set
			{
				if (Canary15DataManager.nextRefreshTime > value)
				{
					Canary15DataManager.nextRefreshTime = value;
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004FBC File Offset: 0x000031BC
		public static bool GetEntry(long ticks, out byte[] key, out long keyIndex, out int segment)
		{
			bool result;
			lock (Canary15DataManager.segments)
			{
				Canary15DataManager.CheckAndUpdateSegment();
				if (Canary15DataManager.activeSegment != null && Canary15DataManager.activeSegment.FindEntry(ticks, out key, out keyIndex))
				{
					segment = Canary15DataManager.activeSegment.SegmentIndex;
					result = true;
				}
				else if (Canary15DataManager.historySegment != null && Canary15DataManager.historySegment.FindEntry(ticks, out key, out keyIndex))
				{
					segment = Canary15DataManager.historySegment.SegmentIndex;
					result = true;
				}
				else
				{
					key = Canary15DataSegment.BackupKey;
					keyIndex = -2L;
					segment = -2;
					if (Canary15DataManager.traceEnableCounter > 0)
					{
						Canary15DataManager.traceEnableCounter--;
						new DateTime(ticks, DateTimeKind.Utc);
						Canary15Trace.LogToIIS("Canary.T" + Canary15DataManager.traceEnableCounter, ticks.ToString());
						if (Canary15DataManager.activeSegment != null)
						{
							Canary15DataManager.activeSegment.LogToIIS(9);
							Canary15DataManager.activeSegment.Trace(9, "GetEntry()");
						}
						if (Canary15DataManager.historySegment != null)
						{
							Canary15DataManager.historySegment.LogToIIS(9);
							Canary15DataManager.historySegment.Trace(9, "GetEntry()");
						}
						if (Canary15DataManager.pendingSegment != null)
						{
							Canary15DataManager.pendingSegment.LogToIIS(9);
							Canary15DataManager.pendingSegment.Trace(9, "GetEntry()");
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00005118 File Offset: 0x00003318
		public static byte[] ComputeHash(byte[] userContextIdBinary, byte[] timestampBinary, string logOnUniqueKey, out long keyIndex, out int segmentIndex)
		{
			long ticks = BitConverter.ToInt64(timestampBinary, 0);
			byte[] array;
			if (Canary15DataManager.GetEntry(ticks, out array, out keyIndex, out segmentIndex))
			{
				byte[] bytes = new UnicodeEncoding().GetBytes(logOnUniqueKey);
				int num = userContextIdBinary.Length + timestampBinary.Length + bytes.Length;
				num += array.Length;
				byte[] array2 = new byte[num];
				int num2 = 0;
				userContextIdBinary.CopyTo(array2, num2);
				num2 += userContextIdBinary.Length;
				timestampBinary.CopyTo(array2, num2);
				num2 += timestampBinary.Length;
				bytes.CopyTo(array2, num2);
				num2 += bytes.Length;
				array.CopyTo(array2, num2);
				byte[] result;
				using (SHA256Cng sha256Cng = new SHA256Cng())
				{
					result = sha256Cng.ComputeHash(array2);
					sha256Cng.Clear();
				}
				return result;
			}
			return null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000051E4 File Offset: 0x000033E4
		internal static void RecalculateState()
		{
			Canary15DataManager.activeSegment = (Canary15DataManager.historySegment = (Canary15DataManager.pendingSegment = (Canary15DataManager.oldSegment = null)));
			DateTime[] array = new DateTime[Canary15DataManager.segments.Length];
			for (int i = 0; i < Canary15DataManager.segments.Length; i++)
			{
				array[i] = Canary15DataManager.segments[i].Header.StartTime;
			}
			Array.Sort<DateTime, Canary15DataSegment>(array, Canary15DataManager.segments);
			for (int j = Canary15DataManager.segments.Length - 1; j >= 0; j--)
			{
				Canary15DataManager.segments[j].Trace(0, "RecalculateState()");
				if (Canary15DataManager.segments[j].Header.StartTime > Canary15DataSegment.UtcNow)
				{
					Canary15DataManager.pendingSegment = Canary15DataManager.segments[j];
					Canary15DataManager.pendingSegment.State = ((Canary15DataManager.pendingSegment.Header.ReadTime >= Canary15DataManager.pendingSegment.Header.ReadyTime) ? Canary15DataSegment.SegmentState.Propagated : Canary15DataSegment.SegmentState.Pending);
					Canary15DataManager.pendingSegment.Trace(1, "RecalculateState()");
				}
				else if (Canary15DataManager.activeSegment == null)
				{
					Canary15DataManager.segments[j].State = Canary15DataSegment.SegmentState.Active;
					Canary15DataManager.activeSegment = Canary15DataManager.segments[j];
					Canary15DataManager.activeSegment.Trace(2, "RecalculateState()");
				}
				else if (Canary15DataManager.historySegment == null)
				{
					Canary15DataManager.segments[j].State = Canary15DataSegment.SegmentState.History;
					Canary15DataManager.historySegment = Canary15DataManager.segments[j];
					Canary15DataManager.historySegment.Trace(3, "RecalculateState()");
				}
				else
				{
					Canary15DataManager.segments[j].State = Canary15DataSegment.SegmentState.Expired;
					Canary15DataManager.oldSegment = Canary15DataManager.segments[j];
					Canary15DataManager.oldSegment.Trace(4, "RecalculateState()");
				}
				Canary15DataManager.NextRefreshTime = Canary15DataManager.segments[j].NextRefreshTime;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000538B File Offset: 0x0000358B
		internal static void ResetNextRefreshTime()
		{
			Canary15DataManager.nextRefreshTime = Canary15DataManager.maxUtc;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005398 File Offset: 0x00003598
		private static void Create(int index)
		{
			long utcNowTicks = Canary15DataSegment.UtcNowTicks;
			long ticks = Canary15DataManager.defaultPeriod.Ticks;
			Canary15DataManager.segments[index] = Canary15DataSegment.Create(index, utcNowTicks, ticks, Canary15DataManager.initialDefaultNumberOfEntries);
			Canary15DataManager.segments[index].LogToIIS(1);
			Canary15DataManager.segments[index].SaveSegmentToAD();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000053E3 File Offset: 0x000035E3
		private static void CreateFromLegacyData(int index, long startTime, long period, long replicationDuration)
		{
			Canary15DataManager.segments[index] = Canary15DataSegment.CreateFromLegacyData(index, startTime, period, replicationDuration);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000053F8 File Offset: 0x000035F8
		private static void CheckAndUpdateSegment()
		{
			Canary15DataSegment.SampleUtcNow();
			if (Canary15DataSegment.UtcNow >= Canary15DataManager.NextRefreshTime)
			{
				Canary15DataManager.ResetNextRefreshTime();
				Canary15DataManager.RecalculateState();
				if (Canary15DataManager.pendingSegment != null)
				{
					Canary15DataManager.pendingSegment.Trace(8, "CheckAndUpdateSegment()");
					if (Canary15DataManager.pendingSegment.State == Canary15DataSegment.SegmentState.Pending && Canary15DataManager.pendingSegment.NextRefreshTime < Canary15DataSegment.UtcNow)
					{
						Canary15DataSegment.LoadClientAccessADObject();
						Canary15DataManager.pendingSegment.ReadSegmentFromAD();
						Canary15DataManager.NextRefreshTime = Canary15DataManager.pendingSegment.NextRefreshTime;
						Canary15DataManager.pendingSegment.Trace(8, "CheckAndUpdateSegment()");
						Canary15DataManager.pendingSegment.LogToIIS(8);
						return;
					}
				}
				else if (Canary15DataManager.oldSegment != null)
				{
					Canary15DataManager.oldSegment.Trace(8, "CheckAndUpdateSegment()");
					Canary15DataManager.oldSegment.LogToIIS(8);
					Canary15DataManager.activeSegment.Trace(8, "CheckAndUpdateSegment()");
					Canary15DataManager.activeSegment.LogToIIS(8);
					Canary15DataManager.oldSegment.CloneFromSegment(Canary15DataManager.activeSegment);
					Canary15DataManager.oldSegment.Trace(8, "CheckAndUpdateSegment()");
					Canary15DataManager.oldSegment.LogToIIS(8);
					Canary15DataManager.oldSegment.SaveSegmentToAD();
					Canary15DataManager.NextRefreshTime = Canary15DataManager.oldSegment.NextRefreshTime;
				}
			}
		}

		// Token: 0x040001CC RID: 460
		private const int NumberOfSegments = 3;

		// Token: 0x040001CD RID: 461
		private static TimeSpan defaultPeriod = new TimeSpan(28, 0, 0, 0);

		// Token: 0x040001CE RID: 462
		private static int initialDefaultNumberOfEntries = 1;

		// Token: 0x040001CF RID: 463
		private static DateTime maxUtc = new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc);

		// Token: 0x040001D0 RID: 464
		private static Canary15DataSegment[] segments = null;

		// Token: 0x040001D1 RID: 465
		private static Canary15DataSegment activeSegment = null;

		// Token: 0x040001D2 RID: 466
		private static Canary15DataSegment historySegment = null;

		// Token: 0x040001D3 RID: 467
		private static Canary15DataSegment pendingSegment = null;

		// Token: 0x040001D4 RID: 468
		private static Canary15DataSegment oldSegment = null;

		// Token: 0x040001D5 RID: 469
		private static DateTime nextRefreshTime;

		// Token: 0x040001D6 RID: 470
		private static int traceEnableCounter = 10;
	}
}
