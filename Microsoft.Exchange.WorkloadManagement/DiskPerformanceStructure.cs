using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001F RID: 31
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct DiskPerformanceStructure
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00005896 File Offset: 0x00003A96
		public static bool operator ==(DiskPerformanceStructure lhs, DiskPerformanceStructure rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000058A0 File Offset: 0x00003AA0
		public static bool operator !=(DiskPerformanceStructure lhs, DiskPerformanceStructure rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000058AD File Offset: 0x00003AAD
		public override bool Equals(object obj)
		{
			return obj is DiskPerformanceStructure && this.Equals((DiskPerformanceStructure)obj);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000058C8 File Offset: 0x00003AC8
		public override int GetHashCode()
		{
			return this.BytesRead.GetHashCode() ^ this.BytesWritten.GetHashCode() ^ this.ReadTime.GetHashCode() ^ this.WriteTime.GetHashCode() ^ this.IdleTime.GetHashCode() ^ this.ReadCount.GetHashCode() ^ this.WriteCount.GetHashCode() ^ this.QueueDepth.GetHashCode() ^ this.SplitCount.GetHashCode() ^ this.QueryTime.GetHashCode() ^ this.StorageDeviceNumber.GetHashCode() ^ this.StorageManagerName.GetHashCode();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000598C File Offset: 0x00003B8C
		public bool Equals(DiskPerformanceStructure obj)
		{
			return this.BytesRead == obj.BytesRead && this.BytesWritten == obj.BytesWritten && this.ReadTime == obj.ReadTime && this.WriteTime == obj.WriteTime && this.IdleTime == obj.IdleTime && this.ReadCount == obj.ReadCount && this.WriteCount == obj.WriteCount && this.QueueDepth == obj.QueueDepth && this.SplitCount == obj.SplitCount && this.QueryTime == obj.QueryTime && this.StorageDeviceNumber == obj.StorageDeviceNumber && this.StorageManagerName == obj.StorageManagerName;
		}

		// Token: 0x04000087 RID: 135
		public readonly long BytesRead;

		// Token: 0x04000088 RID: 136
		public readonly long BytesWritten;

		// Token: 0x04000089 RID: 137
		public readonly long ReadTime;

		// Token: 0x0400008A RID: 138
		public readonly long WriteTime;

		// Token: 0x0400008B RID: 139
		public readonly long IdleTime;

		// Token: 0x0400008C RID: 140
		public readonly int ReadCount;

		// Token: 0x0400008D RID: 141
		public readonly int WriteCount;

		// Token: 0x0400008E RID: 142
		public readonly int QueueDepth;

		// Token: 0x0400008F RID: 143
		public readonly int SplitCount;

		// Token: 0x04000090 RID: 144
		public readonly long QueryTime;

		// Token: 0x04000091 RID: 145
		public readonly int StorageDeviceNumber;

		// Token: 0x04000092 RID: 146
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
		public readonly string StorageManagerName;
	}
}
