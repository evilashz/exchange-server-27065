using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000AA RID: 170
	public class TimingContext
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x000168FC File Offset: 0x00014AFC
		public static void TraceStart(LID lid, uint did, uint cid)
		{
			Stopwatch stopwatch = TimingContext.GetStopWatch();
			stopwatch.Stop();
			TimingContext.TraceTiming(lid, did, cid, TimingContext.GetTime().UtcNow);
			stopwatch.Restart();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00016930 File Offset: 0x00014B30
		public static void TraceElapsed(LID lid, uint did, uint cid)
		{
			Stopwatch stopwatch = TimingContext.GetStopWatch();
			stopwatch.Stop();
			TimingContext.TraceTiming(lid, did, cid, stopwatch.ToTimeSpan());
			stopwatch.Restart();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001695D File Offset: 0x00014B5D
		public static void TraceStop(LID lid, uint did, uint cid)
		{
			TimingContext.GetStopWatch().Stop();
			TimingContext.TraceTiming(lid, did, cid, TimingContext.GetTime().UtcNow);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001697B File Offset: 0x00014B7B
		public static uint GetContextIdentifier()
		{
			return TimingContext.identifier++;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001698A File Offset: 0x00014B8A
		private static void TraceTiming(LID lid, uint did, uint cid, DateTime current)
		{
			TimingContext.TraceTiming(lid, did, cid, 813694976U, (ulong)current.Ticks);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000169A0 File Offset: 0x00014BA0
		private static void TraceTiming(LID lid, uint did, uint cid, TimeSpan elapsed)
		{
			TimingContext.TraceTiming(lid, did, cid, 814743552U, (ulong)elapsed.Ticks);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000169B8 File Offset: 0x00014BB8
		private unsafe static void TraceTiming(LID lid, uint did, uint cid, uint signature, ulong info)
		{
			byte[] array;
			int num;
			DiagnosticContext.Shared.GetBufferPointer(TimingContext.GetBuffer(), DiagnosticContext.Shared.SizeOfRecordFromSignature(signature), out array, out num);
			fixed (byte* ptr = &array[num])
			{
				TimingContext.LocationAndTimeRecord* ptr2 = (TimingContext.LocationAndTimeRecord*)ptr;
				ptr2->Lid = DiagnosticContext.Shared.AdjustLID(lid.Value, signature);
				ptr2->Tid = (uint)Environment.CurrentManagedThreadId;
				ptr2->Did = did;
				ptr2->Cid = cid;
				ptr2->Info = info;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00016A1B File Offset: 0x00014C1B
		public static void ExtractInfo(out byte[] info)
		{
			DiagnosticContext.Shared.ExtractInfo(TimingContext.GetBuffer(), 1024, out info);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00016A2D File Offset: 0x00014C2D
		public static void Reset()
		{
			DiagnosticContext.Shared.Reset(TimingContext.GetBuffer());
			TimingContext.GetStopWatch().Restart();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00016A44 File Offset: 0x00014C44
		private static BipBuffer GetBuffer()
		{
			BipBuffer bipBuffer = TimingContext.timingBuffer;
			if (bipBuffer == null)
			{
				bipBuffer = (TimingContext.timingBuffer = new BipBuffer(1024));
			}
			return bipBuffer;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00016A6C File Offset: 0x00014C6C
		private static Stopwatch GetStopWatch()
		{
			if (TimingContext.stopWatch == null)
			{
				TimingContext.stopWatch = Stopwatch.StartNew();
			}
			return TimingContext.stopWatch;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00016A84 File Offset: 0x00014C84
		private static DeterministicTime GetTime()
		{
			if (TimingContext.time == null)
			{
				TimingContext.time = new DeterministicTime();
			}
			return TimingContext.time;
		}

		// Token: 0x04000714 RID: 1812
		private const int MaxTimingBufferSize = 1024;

		// Token: 0x04000715 RID: 1813
		[ThreadStatic]
		private static BipBuffer timingBuffer;

		// Token: 0x04000716 RID: 1814
		[ThreadStatic]
		private static Stopwatch stopWatch;

		// Token: 0x04000717 RID: 1815
		[ThreadStatic]
		private static DeterministicTime time;

		// Token: 0x04000718 RID: 1816
		[ThreadStatic]
		private static uint identifier;

		// Token: 0x020000AB RID: 171
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct LocationAndTimeRecord
		{
			// Token: 0x06000843 RID: 2115 RVA: 0x00016AA4 File Offset: 0x00014CA4
			public static IList<TimingContext.LocationAndTimeRecord> Parse(byte[] buffer)
			{
				IList<TimingContext.LocationAndTimeRecord> list = new List<TimingContext.LocationAndTimeRecord>(10);
				if (buffer != null && buffer.Length > 0)
				{
					int num = 0;
					while (num + 24 <= buffer.Length)
					{
						uint num2 = BitConverter.ToUInt32(buffer, num);
						uint tid = BitConverter.ToUInt32(buffer, num + 4);
						uint did = BitConverter.ToUInt32(buffer, num + 8);
						uint cid = BitConverter.ToUInt32(buffer, num + 12);
						ulong info = BitConverter.ToUInt64(buffer, num + 16);
						uint num3 = num2 & DiagnosticContext.ContextSignatureMask;
						if (num3 == 813694976U || num3 == 814743552U)
						{
							list.Add(new TimingContext.LocationAndTimeRecord
							{
								Lid = num2,
								Tid = tid,
								Did = did,
								Cid = cid,
								Info = info
							});
						}
						num += 24;
					}
				}
				return list;
			}

			// Token: 0x04000719 RID: 1817
			public const uint SignatureCurrent = 813694976U;

			// Token: 0x0400071A RID: 1818
			public const uint SignatureElapsed = 814743552U;

			// Token: 0x0400071B RID: 1819
			public uint Lid;

			// Token: 0x0400071C RID: 1820
			public uint Tid;

			// Token: 0x0400071D RID: 1821
			public uint Did;

			// Token: 0x0400071E RID: 1822
			public uint Cid;

			// Token: 0x0400071F RID: 1823
			public ulong Info;
		}
	}
}
