using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000BE RID: 190
	internal class RegistryReplicator : IDisposeTrackable, IDisposable
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00025EE8 File Offset: 0x000240E8
		internal RegistryReplicator(RealRegistry localRegistry, ClusterRegistry clusterRegistry, ManualResetEvent keyChanged)
		{
			this.m_localRegistry = localRegistry;
			this.m_clusterRegistry = clusterRegistry;
			this.m_keyChanged = keyChanged;
			this.m_isCopyEnabled = true;
			this.m_isValid = true;
			this.m_isCopying = false;
			this.m_disposed = false;
			this.m_isMarkedForRemoval = false;
			this.m_disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00025F3F File Offset: 0x0002413F
		internal SafeHandle Handle
		{
			get
			{
				return this.m_localRegistry.Handle;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00025F4C File Offset: 0x0002414C
		internal ManualResetEvent KeyChanged
		{
			get
			{
				return this.m_keyChanged;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00025F54 File Offset: 0x00024154
		internal bool IsCopyEnabled
		{
			get
			{
				return this.m_isCopyEnabled;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00025F5C File Offset: 0x0002415C
		internal bool IsCopying
		{
			get
			{
				return this.m_isCopying;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00025F64 File Offset: 0x00024164
		internal bool IsValid
		{
			get
			{
				return this.m_isValid;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00025F6C File Offset: 0x0002416C
		internal bool IsInitialReplication
		{
			get
			{
				return this.m_localRegistry.IsInitialReplication | this.m_clusterRegistry.IsInitialReplication;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00025F85 File Offset: 0x00024185
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00025F94 File Offset: 0x00024194
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RegistryReplicator>(this);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00025F9C File Offset: 0x0002419C
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00025FB1 File Offset: 0x000241B1
		internal void SetInitialReplication()
		{
			this.m_localRegistry.SetInitialReplication();
			this.m_clusterRegistry.SetInitialReplication();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00025FC9 File Offset: 0x000241C9
		internal void SetValid()
		{
			this.m_isValid = true;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00025FD2 File Offset: 0x000241D2
		internal void SetInvalid()
		{
			this.m_isValid = false;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00025FDB File Offset: 0x000241DB
		internal void EnableCopy()
		{
			this.m_isCopyEnabled = true;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00025FE4 File Offset: 0x000241E4
		internal void DisableCopy()
		{
			this.m_isCopyEnabled = false;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00025FED File Offset: 0x000241ED
		internal void SetMarkedForRemoval()
		{
			this.m_isMarkedForRemoval = true;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00025FF6 File Offset: 0x000241F6
		internal void ResetMarkedForRemoval()
		{
			this.m_isMarkedForRemoval = false;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00025FFF File Offset: 0x000241FF
		internal bool IsMarkedForRemoval
		{
			get
			{
				return this.m_isMarkedForRemoval;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00026007 File Offset: 0x00024207
		internal void InverseCopy()
		{
			this.CopyWorker(this.m_clusterRegistry, this.m_localRegistry);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0002601B File Offset: 0x0002421B
		internal void Copy(AmClusterHandle handle)
		{
			if (handle != null)
			{
				this.m_clusterRegistry.SetClusterHandle(handle);
			}
			this.CopyWorker(this.m_localRegistry, this.m_clusterRegistry);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00026040 File Offset: 0x00024240
		private void CopyWorker(RegistryManipulator source, RegistryManipulator destination)
		{
			string text = null;
			string text2 = null;
			bool flag = true;
			bool flag2 = true;
			this.m_isCopying = true;
			try
			{
				Queue<string> queue = new Queue<string>();
				Queue<string> queue2 = new Queue<string>();
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				queue.Enqueue(string.Empty);
				queue2.Enqueue(string.Empty);
				while (this.m_isValid)
				{
					if (flag)
					{
						list.Clear();
						if (queue.Count > 0)
						{
							text = queue.Dequeue();
							flag = false;
							foreach (string text3 in source.GetSubKeyNames(text))
							{
								string item = text3;
								if (text.CompareTo(string.Empty) != 0)
								{
									item = text + "\\" + text3;
								}
								queue.Enqueue(item);
							}
						}
						else
						{
							text = null;
						}
					}
					if (flag2)
					{
						list2.Clear();
						if (queue2.Count > 0)
						{
							text2 = queue2.Dequeue();
							foreach (string text4 in destination.GetSubKeyNames(text2))
							{
								string item2 = text4;
								if (text2.CompareTo(string.Empty) != 0)
								{
									item2 = text2 + "\\" + text4;
								}
								queue2.Enqueue(item2);
							}
						}
						else
						{
							text2 = null;
						}
					}
					int num;
					if (text == null)
					{
						num = 1;
					}
					else if (text2 == null)
					{
						num = -1;
					}
					else
					{
						num = string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
					}
					if (num == 0)
					{
						foreach (string item3 in source.GetValueNames(text))
						{
							list.Add(item3);
						}
						list.Sort();
						foreach (string item4 in destination.GetValueNames(text2))
						{
							list2.Add(item4);
						}
						list2.Sort();
						int num2 = 0;
						int num3 = 0;
						while (num3 < list.Count || num2 < list2.Count)
						{
							int num4;
							if (num3 >= list.Count)
							{
								num4 = 1;
							}
							else if (num2 >= list2.Count)
							{
								num4 = -1;
							}
							else
							{
								num4 = string.Compare(list[num3], list2[num2]);
							}
							if (num4 == 0)
							{
								RegistryValue value = source.GetValue(text, list[num3]);
								RegistryValue value2 = destination.GetValue(text2, list2[num2]);
								if (value != null && !value.Equals(value2))
								{
									destination.SetValue(text, value);
								}
								else if (value == null)
								{
									destination.DeleteValue(text2, list2[num2]);
								}
								num3++;
								num2++;
							}
							else if (num4 < 0)
							{
								RegistryValue value3 = source.GetValue(text, list[num3]);
								if (value3 != null)
								{
									destination.SetValue(text, value3);
								}
								num3++;
							}
							else
							{
								destination.DeleteValue(text2, list2[num2]);
								num2++;
							}
						}
						flag = true;
						flag2 = true;
					}
					else if (num < 0 && text != null)
					{
						destination.CreateKey(text);
						flag2 = false;
						text2 = text;
					}
					else
					{
						if (text2 == null)
						{
							return;
						}
						destination.DeleteKey(text2);
						flag2 = true;
					}
				}
				AmTrace.Warning("Skipping a copy notification since the database has changed master. Source root: {0}", new object[]
				{
					source.Root
				});
			}
			finally
			{
				this.m_isCopying = false;
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00026380 File Offset: 0x00024580
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed && disposing)
			{
				this.SetInvalid();
				if (this.m_localRegistry != null)
				{
					this.m_localRegistry.Dispose();
				}
				if (this.m_clusterRegistry != null)
				{
					this.m_clusterRegistry.Dispose();
				}
				if (this.m_keyChanged != null)
				{
					this.m_keyChanged.Close();
				}
				if (this.m_disposeTracker != null)
				{
					this.m_disposeTracker.Dispose();
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x0400036F RID: 879
		private RealRegistry m_localRegistry;

		// Token: 0x04000370 RID: 880
		private ClusterRegistry m_clusterRegistry;

		// Token: 0x04000371 RID: 881
		private ManualResetEvent m_keyChanged;

		// Token: 0x04000372 RID: 882
		private bool m_isCopyEnabled;

		// Token: 0x04000373 RID: 883
		private bool m_isValid;

		// Token: 0x04000374 RID: 884
		private bool m_isCopying;

		// Token: 0x04000375 RID: 885
		private bool m_disposed;

		// Token: 0x04000376 RID: 886
		private bool m_isMarkedForRemoval;

		// Token: 0x04000377 RID: 887
		private DisposeTracker m_disposeTracker;
	}
}
