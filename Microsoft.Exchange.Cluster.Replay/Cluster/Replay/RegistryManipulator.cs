using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000C0 RID: 192
	internal abstract class RegistryManipulator : IDisposeTrackable, IDisposable
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x000265E5 File Offset: 0x000247E5
		protected RegistryManipulator(string root, SafeHandle handle)
		{
			this.Root = root;
			this.Handle = handle;
			this.m_disposed = false;
			this.m_disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00026610 File Offset: 0x00024810
		public bool IsInitialReplication
		{
			get
			{
				RegistryValue registryValue = null;
				try
				{
					registryValue = this.GetValue(string.Empty, "InitialReplicationSucceeded");
				}
				catch (AmRegistryException)
				{
				}
				catch (ClusterApiException)
				{
				}
				return registryValue == null || (int)registryValue.Value == 0;
			}
		}

		// Token: 0x060007D6 RID: 2006
		public abstract string[] GetSubKeyNames(string keyName);

		// Token: 0x060007D7 RID: 2007
		public abstract string[] GetValueNames(string keyName);

		// Token: 0x060007D8 RID: 2008
		public abstract void SetValue(string keyName, RegistryValue value);

		// Token: 0x060007D9 RID: 2009
		public abstract RegistryValue GetValue(string keyName, string valueName);

		// Token: 0x060007DA RID: 2010
		public abstract void DeleteValue(string keyName, string valueName);

		// Token: 0x060007DB RID: 2011
		public abstract void DeleteKey(string keyName);

		// Token: 0x060007DC RID: 2012
		public abstract void CreateKey(string keyName);

		// Token: 0x060007DD RID: 2013 RVA: 0x00026668 File Offset: 0x00024868
		public void SetInitialReplication()
		{
			this.SetValue(string.Empty, new RegistryValue("InitialReplicationSucceeded", 1, RegistryValueKind.DWord));
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00026686 File Offset: 0x00024886
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00026695 File Offset: 0x00024895
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RegistryManipulator>(this);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002669D File Offset: 0x0002489D
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000266B2 File Offset: 0x000248B2
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed && disposing && this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Dispose();
			}
			this.m_disposed = true;
		}

		// Token: 0x0400037B RID: 891
		protected const string FirstMountValue = "InitialReplicationSucceeded";

		// Token: 0x0400037C RID: 892
		public string Root;

		// Token: 0x0400037D RID: 893
		public SafeHandle Handle;

		// Token: 0x0400037E RID: 894
		private DisposeTracker m_disposeTracker;

		// Token: 0x0400037F RID: 895
		private bool m_disposed;
	}
}
