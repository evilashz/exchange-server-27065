using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B42 RID: 2882
	internal sealed class SafeJobHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003E10 RID: 15888 RVA: 0x000A22E1 File Offset: 0x000A04E1
		private SafeJobHandle() : base(true)
		{
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000A22EC File Offset: 0x000A04EC
		public unsafe void SetExtendedLimits(NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION limits)
		{
			void* jobObjectInfo = (void*)(&limits);
			if (!NativeMethods.SetInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation, jobObjectInfo, Marshal.SizeOf(typeof(NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION))))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000A2324 File Offset: 0x000A0524
		public unsafe void SetUIRestrictions(JobObjectUILimit limits)
		{
			if (limits == JobObjectUILimit.Default)
			{
				return;
			}
			SafeJobHandle.BasicUIRestrictions basicUIRestrictions = new SafeJobHandle.BasicUIRestrictions(limits);
			void* jobObjectInfo = (void*)(&basicUIRestrictions);
			if (!NativeMethods.SetInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectBasicUIRestrictions, jobObjectInfo, Marshal.SizeOf(typeof(SafeJobHandle.BasicUIRestrictions))))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000A236C File Offset: 0x000A056C
		public unsafe void SetCpuRateLimit(NativeMethods.JOBOBJECT_CPU_RATE_CONTROL_INFORMATION limits)
		{
			void* jobObjectInfo = (void*)(&limits);
			if (!NativeMethods.SetInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectCpuRateControlInformation, jobObjectInfo, Marshal.SizeOf(typeof(NativeMethods.JOBOBJECT_CPU_RATE_CONTROL_INFORMATION))))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000A23A4 File Offset: 0x000A05A4
		public unsafe void SetNotificationLimit(NativeMethods.JOBOBJECT_NOTIFICATION_LIMIT_INFORMATION limits)
		{
			void* jobObjectInfo = (void*)(&limits);
			if (!NativeMethods.SetInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectNotificationLimitInformation, jobObjectInfo, Marshal.SizeOf(typeof(NativeMethods.JOBOBJECT_NOTIFICATION_LIMIT_INFORMATION))))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x000A23DC File Offset: 0x000A05DC
		public unsafe NativeMethods.JOBOBJECT_LIMIT_VIOLATION_INFORMATION GetLimitViolationInfo()
		{
			NativeMethods.JOBOBJECT_LIMIT_VIOLATION_INFORMATION result = default(NativeMethods.JOBOBJECT_LIMIT_VIOLATION_INFORMATION);
			void* jobObjectInfo = (void*)(&result);
			uint num;
			if (!NativeMethods.QueryInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectLimitViolationInformation, jobObjectInfo, (uint)Marshal.SizeOf(typeof(NativeMethods.JOBOBJECT_LIMIT_VIOLATION_INFORMATION)), out num))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			return result;
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000A2420 File Offset: 0x000A0620
		public unsafe NativeMethods.JOBOBJECT_CPU_RATE_CONTROL_INFORMATION GetJobCpuLimitInfo()
		{
			NativeMethods.JOBOBJECT_CPU_RATE_CONTROL_INFORMATION result = default(NativeMethods.JOBOBJECT_CPU_RATE_CONTROL_INFORMATION);
			void* jobObjectInfo = (void*)(&result);
			uint num;
			if (!NativeMethods.QueryInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectCpuRateControlInformation, jobObjectInfo, (uint)Marshal.SizeOf(typeof(NativeMethods.JOBOBJECT_CPU_RATE_CONTROL_INFORMATION)), out num))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			return result;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000A2464 File Offset: 0x000A0664
		public bool Add(Process process)
		{
			if (!NativeMethods.AssignProcessToJobObject(this, process.Handle))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.ProcessManagerTracer.TraceError<int>(0L, "Attempt to assign process to job failed with 0x{0:X}", lastWin32Error);
				return false;
			}
			return true;
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000A249C File Offset: 0x000A069C
		public unsafe int SetCompletionPort(NativeMethods.JobObjectAssociateCompletionPort completionPort)
		{
			void* jobObjectInfo = (void*)(&completionPort);
			if (!NativeMethods.SetInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectAssociateCompletionPortInformation, jobObjectInfo, Marshal.SizeOf(typeof(NativeMethods.JobObjectAssociateCompletionPort))))
			{
				return Marshal.GetLastWin32Error();
			}
			return 0;
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000A24D0 File Offset: 0x000A06D0
		public unsafe int[] GetAssignedProcessIds()
		{
			NativeMethods.JobObjectBasicProcessIdList jobObjectBasicProcessIdList = default(NativeMethods.JobObjectBasicProcessIdList);
			NativeMethods.JobObjectBasicProcessIdList* ptr = &jobObjectBasicProcessIdList;
			void* ptr2 = (void*)(&jobObjectBasicProcessIdList);
			uint num;
			if (!NativeMethods.QueryInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectBasicProcessIdList, ptr2, (uint)Marshal.SizeOf(typeof(NativeMethods.JobObjectBasicProcessIdList)), out num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == 234)
				{
					uint num2 = (uint)((long)Marshal.SizeOf(typeof(NativeMethods.JobObjectBasicProcessIdList)) + (long)sizeof(UIntPtr) * (long)((ulong)ptr->NumberOfAssignedProcess));
					fixed (byte* ptr3 = new byte[num2])
					{
						ptr2 = (void*)ptr3;
						if (!NativeMethods.QueryInformationJobObject(this, NativeMethods.JOBOBJECTINFOCLASS.JobObjectBasicProcessIdList, ptr2, num2, out num))
						{
							Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
						}
						return this.InternalGetProcessIdList((NativeMethods.JobObjectBasicProcessIdList*)ptr2);
					}
				}
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			return this.InternalGetProcessIdList(ptr);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000A25A8 File Offset: 0x000A07A8
		private unsafe int[] InternalGetProcessIdList(NativeMethods.JobObjectBasicProcessIdList* pinnedIdList)
		{
			if (pinnedIdList->NumberOfProcessIdsInList < pinnedIdList->NumberOfAssignedProcess)
			{
				throw new InvalidOperationException("Invalid number of assigned processes.");
			}
			int[] array = new int[pinnedIdList->NumberOfAssignedProcess];
			IntPtr value = Marshal.OffsetOf(typeof(NativeMethods.JobObjectBasicProcessIdList), "ProcessIdList");
			for (uint num = 0U; num < pinnedIdList->NumberOfProcessIdsInList; num += 1U)
			{
				UIntPtr uintPtr = (UIntPtr)Marshal.PtrToStructure((IntPtr)((void*)(pinnedIdList + value.ToInt32() / sizeof(NativeMethods.JobObjectBasicProcessIdList))), typeof(UIntPtr));
				array[(int)((UIntPtr)num)] = (int)uintPtr.ToUInt32();
				value = (IntPtr)((long)value + (long)sizeof(UIntPtr));
			}
			return array;
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000A2644 File Offset: 0x000A0844
		internal bool Add(SafeProcessHandle process)
		{
			if (process == null || process.IsInvalid)
			{
				return false;
			}
			bool flag;
			if (!NativeMethods.IsProcessInJob(process, this, out flag))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				ExTraceGlobals.ProcessManagerTracer.TraceError<int>(0L, "Failed to check if process was part of job", lastWin32Error);
				return false;
			}
			if (!flag && !NativeMethods.AssignProcessToJobObject(this, process))
			{
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				ExTraceGlobals.ProcessManagerTracer.TraceError<int>(0L, "Attempt to assign process to job failed with 0x{0:X}", lastWin32Error2);
				return false;
			}
			return true;
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x000A26AA File Offset: 0x000A08AA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return SafeJobHandle.CloseHandle(this.handle);
		}

		// Token: 0x06003E1D RID: 15901
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x02000B43 RID: 2883
		private struct BasicUIRestrictions
		{
			// Token: 0x06003E1E RID: 15902 RVA: 0x000A26B7 File Offset: 0x000A08B7
			public BasicUIRestrictions(JobObjectUILimit limit)
			{
				this.restrictions = (uint)limit;
			}

			// Token: 0x17000F4C RID: 3916
			// (get) Token: 0x06003E1F RID: 15903 RVA: 0x000A26C0 File Offset: 0x000A08C0
			public JobObjectUILimit Restrictions
			{
				get
				{
					return (JobObjectUILimit)this.restrictions;
				}
			}

			// Token: 0x040035F0 RID: 13808
			private uint restrictions;
		}
	}
}
