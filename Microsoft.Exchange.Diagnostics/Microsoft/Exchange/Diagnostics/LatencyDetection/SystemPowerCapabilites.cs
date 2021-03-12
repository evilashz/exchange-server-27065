using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000186 RID: 390
	internal struct SystemPowerCapabilites
	{
		// Token: 0x040007B5 RID: 1973
		public bool PowerButtonPresent;

		// Token: 0x040007B6 RID: 1974
		public bool SleepButtonPresent;

		// Token: 0x040007B7 RID: 1975
		public bool LidPresent;

		// Token: 0x040007B8 RID: 1976
		public bool SystemS1;

		// Token: 0x040007B9 RID: 1977
		public bool SystemS2;

		// Token: 0x040007BA RID: 1978
		public bool SystemS3;

		// Token: 0x040007BB RID: 1979
		public bool SystemS4;

		// Token: 0x040007BC RID: 1980
		public bool SystemS5;

		// Token: 0x040007BD RID: 1981
		public bool HiberFilePresent;

		// Token: 0x040007BE RID: 1982
		public bool FullWake;

		// Token: 0x040007BF RID: 1983
		public bool VideoDimPresent;

		// Token: 0x040007C0 RID: 1984
		public bool ApmPresent;

		// Token: 0x040007C1 RID: 1985
		public bool UpsPresent;

		// Token: 0x040007C2 RID: 1986
		public bool ThermalControl;

		// Token: 0x040007C3 RID: 1987
		public bool ProcessorThrottle;

		// Token: 0x040007C4 RID: 1988
		public byte ProcessorMinThrottle;

		// Token: 0x040007C5 RID: 1989
		public byte ProcessorMaxThrottle;

		// Token: 0x040007C6 RID: 1990
		public bool FastSystemS4;

		// Token: 0x040007C7 RID: 1991
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Bool)]
		public byte[] Spare2;

		// Token: 0x040007C8 RID: 1992
		public bool DiskSpinDown;

		// Token: 0x040007C9 RID: 1993
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.Bool)]
		public byte[] Spare3;

		// Token: 0x040007CA RID: 1994
		public bool SystemBatteriesPresent;

		// Token: 0x040007CB RID: 1995
		public bool BatteriesAreShortTerm;

		// Token: 0x040007CC RID: 1996
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
		public BatteryReportingScale[] BatteryScale;

		// Token: 0x040007CD RID: 1997
		public int AcOnLineWake;

		// Token: 0x040007CE RID: 1998
		public int SoftLidWake;

		// Token: 0x040007CF RID: 1999
		public int RtcWake;

		// Token: 0x040007D0 RID: 2000
		public int MinDeviceWakeState;

		// Token: 0x040007D1 RID: 2001
		public int DefaultLowLatencyWake;
	}
}
