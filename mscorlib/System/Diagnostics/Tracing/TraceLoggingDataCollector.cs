using System;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000454 RID: 1108
	[SecuritySafeCritical]
	internal class TraceLoggingDataCollector
	{
		// Token: 0x060035F3 RID: 13811 RVA: 0x000CFD0C File Offset: 0x000CDF0C
		private TraceLoggingDataCollector()
		{
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000CFD14 File Offset: 0x000CDF14
		public int BeginBufferedArray()
		{
			return DataCollector.ThreadInstance.BeginBufferedArray();
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000CFD20 File Offset: 0x000CDF20
		public void EndBufferedArray(int bookmark, int count)
		{
			DataCollector.ThreadInstance.EndBufferedArray(bookmark, count);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000CFD2E File Offset: 0x000CDF2E
		public TraceLoggingDataCollector AddGroup()
		{
			return this;
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000CFD31 File Offset: 0x000CDF31
		public unsafe void AddScalar(bool value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000CFD41 File Offset: 0x000CDF41
		public unsafe void AddScalar(sbyte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000CFD51 File Offset: 0x000CDF51
		public unsafe void AddScalar(byte value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000CFD61 File Offset: 0x000CDF61
		public unsafe void AddScalar(short value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000CFD71 File Offset: 0x000CDF71
		public unsafe void AddScalar(ushort value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000CFD81 File Offset: 0x000CDF81
		public unsafe void AddScalar(int value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000CFD91 File Offset: 0x000CDF91
		public unsafe void AddScalar(uint value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000CFDA1 File Offset: 0x000CDFA1
		public unsafe void AddScalar(long value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000CFDB1 File Offset: 0x000CDFB1
		public unsafe void AddScalar(ulong value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000CFDC1 File Offset: 0x000CDFC1
		public unsafe void AddScalar(IntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), IntPtr.Size);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000CFDD5 File Offset: 0x000CDFD5
		public unsafe void AddScalar(UIntPtr value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), UIntPtr.Size);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000CFDE9 File Offset: 0x000CDFE9
		public unsafe void AddScalar(float value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 4);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000CFDF9 File Offset: 0x000CDFF9
		public unsafe void AddScalar(double value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000CFE09 File Offset: 0x000CE009
		public unsafe void AddScalar(char value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 2);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000CFE19 File Offset: 0x000CE019
		public unsafe void AddScalar(Guid value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 16);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000CFE2A File Offset: 0x000CE02A
		public void AddBinary(string value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : (value.Length * 2));
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000CFE45 File Offset: 0x000CE045
		public void AddBinary(byte[] value)
		{
			DataCollector.ThreadInstance.AddBinary(value, (value == null) ? 0 : value.Length);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000CFE5B File Offset: 0x000CE05B
		public void AddArray(bool[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000CFE72 File Offset: 0x000CE072
		public void AddArray(sbyte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000CFE89 File Offset: 0x000CE089
		public void AddArray(short[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000CFEA0 File Offset: 0x000CE0A0
		public void AddArray(ushort[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000CFEB7 File Offset: 0x000CE0B7
		public void AddArray(int[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000CFECE File Offset: 0x000CE0CE
		public void AddArray(uint[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000CFEE5 File Offset: 0x000CE0E5
		public void AddArray(long[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000CFEFC File Offset: 0x000CE0FC
		public void AddArray(ulong[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000CFF13 File Offset: 0x000CE113
		public void AddArray(IntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, IntPtr.Size);
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000CFF2E File Offset: 0x000CE12E
		public void AddArray(UIntPtr[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, UIntPtr.Size);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000CFF49 File Offset: 0x000CE149
		public void AddArray(float[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 4);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000CFF60 File Offset: 0x000CE160
		public void AddArray(double[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 8);
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000CFF77 File Offset: 0x000CE177
		public void AddArray(char[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 2);
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000CFF8E File Offset: 0x000CE18E
		public void AddArray(Guid[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 16);
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000CFFA6 File Offset: 0x000CE1A6
		public void AddCustom(byte[] value)
		{
			DataCollector.ThreadInstance.AddArray(value, (value == null) ? 0 : value.Length, 1);
		}

		// Token: 0x040017B2 RID: 6066
		internal static readonly TraceLoggingDataCollector Instance = new TraceLoggingDataCollector();
	}
}
