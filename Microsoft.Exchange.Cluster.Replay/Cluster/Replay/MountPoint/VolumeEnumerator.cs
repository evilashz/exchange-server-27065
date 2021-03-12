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
	// Token: 0x0200023E RID: 574
	internal class VolumeEnumerator : DisposeTrackableBase
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00057785 File Offset: 0x00055985
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.VolumeManagerTracer;
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00057878 File Offset: 0x00055A78
		public IEnumerable<MountedFolderPath> GetVolumes()
		{
			string volumeName;
			while (this.GetNextVolume(out volumeName))
			{
				yield return new MountedFolderPath(volumeName);
			}
			yield break;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x000579C0 File Offset: 0x00055BC0
		public IEnumerable<ExchangeVolume> GetExchangeVolumes(string exchangeVolumesRootPath, string databasesRootPath, int numDbsPerVolume)
		{
			string volumeName;
			while (this.GetNextVolume(out volumeName))
			{
				ExchangeVolume exVol = ExchangeVolume.GetInstance(new MountedFolderPath(volumeName), exchangeVolumesRootPath, databasesRootPath, numDbsPerVolume);
				yield return exVol;
			}
			yield break;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000579F4 File Offset: 0x00055BF4
		private bool GetNextVolume(out string volumeName)
		{
			int num = 50;
			StringBuilder stringBuilder = new StringBuilder(num);
			volumeName = null;
			if (this.m_findHandle != null)
			{
				bool flag = NativeMethods.FindNextVolume(this.m_findHandle, stringBuilder, (uint)num);
				if (flag)
				{
					volumeName = stringBuilder.ToString();
				}
				else
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 18 && VolumeEnumerator.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						Exception arg = new Win32Exception(lastWin32Error);
						VolumeEnumerator.Tracer.TraceError<int, Exception>((long)this.GetHashCode(), "FindNextVolume() failed with Win32 EC: {0}. Win32Exception: {1}", lastWin32Error, arg);
					}
				}
				return flag;
			}
			this.m_findHandle = NativeMethods.FindFirstVolume(stringBuilder, (uint)num);
			if (this.m_findHandle == null || this.m_findHandle.IsInvalid)
			{
				if (VolumeEnumerator.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					Exception arg2 = new Win32Exception(lastWin32Error2);
					VolumeEnumerator.Tracer.TraceError<int, Exception>((long)this.GetHashCode(), "FindFirstVolume() failed with Win32 EC: {0}. Win32Exception: {1}", lastWin32Error2, arg2);
				}
				return false;
			}
			volumeName = stringBuilder.ToString();
			return true;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00057AD2 File Offset: 0x00055CD2
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

		// Token: 0x060015E1 RID: 5601 RVA: 0x00057AFE File Offset: 0x00055CFE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VolumeEnumerator>(this);
		}

		// Token: 0x0400089E RID: 2206
		private SafeVolumeFindHandle m_findHandle;
	}
}
