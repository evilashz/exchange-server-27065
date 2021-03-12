using System;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal class AmClusterNotifyHandle : SafeDisposeTrackerHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000CA47 File Offset: 0x0000AC47
		public AmClusterNotifyHandle() : base(true)
		{
			base.SetHandle((IntPtr)(-1));
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		public static AmClusterNotifyHandle InvalidHandle
		{
			get
			{
				return AmClusterNotifyHandle.sm_invalidHandle;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000CA63 File Offset: 0x0000AC63
		internal void DangerousCloseHandle()
		{
			this.CloseNotifyPort();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		protected override bool ReleaseHandle()
		{
			return this.CloseNotifyPort();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000CA74 File Offset: 0x0000AC74
		private bool CloseNotifyPort()
		{
			bool result = true;
			lock (this)
			{
				if (!this.m_isClosed)
				{
					this.m_isClosed = true;
					if (!this.IsInvalid)
					{
						AmTrace.Debug("Calling CloseClusterNotifyPort() (handle=0x{0:x})", new object[]
						{
							this.handle
						});
						try
						{
							result = ClusapiMethods.CloseClusterNotifyPort(this.handle);
							goto IL_BD;
						}
						catch (AccessViolationException ex)
						{
							AmTrace.Error("Ignoring AccessViolationException exception while Closing cluster notify port (error={0})", new object[]
							{
								ex
							});
							goto IL_BD;
						}
					}
					AmTrace.Debug("Skipped CloseClusterNotifyPort() since handle is invalid (handle=0x{0:x})", new object[]
					{
						this.handle
					});
				}
				else
				{
					AmTrace.Debug("Skipped CloseClusterNotifyPort() the handle was closed already (handle=0x{0:x})", new object[]
					{
						this.handle
					});
				}
				IL_BD:;
			}
			return result;
		}

		// Token: 0x040000CA RID: 202
		private static AmClusterNotifyHandle sm_invalidHandle = new AmClusterNotifyHandle();

		// Token: 0x040000CB RID: 203
		private bool m_isClosed;
	}
}
