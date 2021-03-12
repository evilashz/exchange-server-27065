using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Cluster.Shared.MountPoint;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x0200023C RID: 572
	internal class MountPointEnumerator : DisposeTrackableBase
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x000565AD File Offset: 0x000547AD
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.VolumeManagerTracer;
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000565B4 File Offset: 0x000547B4
		public MountPointEnumerator(string volumeName)
		{
			this.m_volumeName = volumeName;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000566A8 File Offset: 0x000548A8
		public IEnumerable<MountedFolderPath> GetMountPoints()
		{
			string mountPoint;
			while (this.GetNextMountPoint(out mountPoint))
			{
				yield return new MountedFolderPath(mountPoint);
			}
			yield break;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000566C8 File Offset: 0x000548C8
		private bool GetNextMountPoint(out string mountPoint)
		{
			int num = 260;
			StringBuilder stringBuilder = new StringBuilder(num);
			mountPoint = null;
			if (this.m_findHandle != null)
			{
				bool flag = NativeMethods.FindNextVolumeMountPoint(this.m_findHandle, stringBuilder, (uint)num);
				if (flag)
				{
					mountPoint = stringBuilder.ToString();
				}
				else
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 18 && MountPointEnumerator.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						Exception arg = new Win32Exception(lastWin32Error);
						MountPointEnumerator.Tracer.TraceError<int, Exception>((long)this.GetHashCode(), "FindNextVolumeMountPoint() failed with Win32 EC: {0}. Win32Exception: {1}", lastWin32Error, arg);
					}
				}
				return flag;
			}
			this.m_findHandle = NativeMethods.FindFirstVolumeMountPoint(this.m_volumeName, stringBuilder, (uint)num);
			if (this.m_findHandle == null || this.m_findHandle.IsInvalid)
			{
				if (MountPointEnumerator.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					Exception arg2 = new Win32Exception(lastWin32Error2);
					MountPointEnumerator.Tracer.TraceError<int, Exception>((long)this.GetHashCode(), "FindFirstVolumeMountPoint() failed with Win32 EC: {0}. Win32Exception: {1}", lastWin32Error2, arg2);
				}
				return false;
			}
			mountPoint = stringBuilder.ToString();
			return true;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000567AF File Offset: 0x000549AF
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.m_findHandle != null)
			{
				if (!this.m_findHandle.IsInvalid)
				{
					this.m_findHandle.Close();
				}
				this.m_findHandle = null;
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000567DB File Offset: 0x000549DB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MountPointEnumerator>(this);
		}

		// Token: 0x04000896 RID: 2198
		private readonly string m_volumeName;

		// Token: 0x04000897 RID: 2199
		private SafeVolumeMountPointFindHandle m_findHandle;
	}
}
