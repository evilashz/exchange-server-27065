﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Util;
using System.Threading;

namespace System
{
	// Token: 0x0200013E RID: 318
	internal sealed class SharedStatics
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0003823D File Offset: 0x0003643D
		private SharedStatics()
		{
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00038248 File Offset: 0x00036448
		public static string Remoting_Identity_IDGuid
		{
			[SecuritySafeCritical]
			get
			{
				if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(SharedStatics._sharedStatics, ref flag);
						if (SharedStatics._sharedStatics._Remoting_Identity_IDGuid == null)
						{
							SharedStatics._sharedStatics._Remoting_Identity_IDGuid = Guid.NewGuid().ToString().Replace('-', '_');
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(SharedStatics._sharedStatics);
						}
					}
				}
				return SharedStatics._sharedStatics._Remoting_Identity_IDGuid;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000382D8 File Offset: 0x000364D8
		[SecuritySafeCritical]
		public static Tokenizer.StringMaker GetSharedStringMaker()
		{
			Tokenizer.StringMaker stringMaker = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				if (SharedStatics._sharedStatics._maker != null)
				{
					stringMaker = SharedStatics._sharedStatics._maker;
					SharedStatics._sharedStatics._maker = null;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
			if (stringMaker == null)
			{
				stringMaker = new Tokenizer.StringMaker();
			}
			return stringMaker;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00038348 File Offset: 0x00036548
		[SecuritySafeCritical]
		public static void ReleaseSharedStringMaker(ref Tokenizer.StringMaker maker)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				SharedStatics._sharedStatics._maker = maker;
				maker = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00038398 File Offset: 0x00036598
		internal static int Remoting_Identity_GetNextSeqNum()
		{
			return Interlocked.Increment(ref SharedStatics._sharedStatics._Remoting_Identity_IDSeqNum);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000383A9 File Offset: 0x000365A9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static long AddMemoryFailPointReservation(long size)
		{
			return Interlocked.Add(ref SharedStatics._sharedStatics._memFailPointReservedMemory, size);
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x000383BB File Offset: 0x000365BB
		internal static ulong MemoryFailPointReservedMemory
		{
			get
			{
				return (ulong)Volatile.Read(ref SharedStatics._sharedStatics._memFailPointReservedMemory);
			}
		}

		// Token: 0x04000683 RID: 1667
		private static SharedStatics _sharedStatics;

		// Token: 0x04000684 RID: 1668
		private volatile string _Remoting_Identity_IDGuid;

		// Token: 0x04000685 RID: 1669
		private Tokenizer.StringMaker _maker;

		// Token: 0x04000686 RID: 1670
		private int _Remoting_Identity_IDSeqNum;

		// Token: 0x04000687 RID: 1671
		private long _memFailPointReservedMemory;
	}
}
