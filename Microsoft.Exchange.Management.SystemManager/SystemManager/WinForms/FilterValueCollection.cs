using System;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F2 RID: 498
	public sealed class FilterValueCollection : ICollection, IEnumerable
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x0005E392 File Offset: 0x0005C592
		internal FilterValueCollection(DataListView owner)
		{
			this.owner = owner;
		}

		// Token: 0x1700054B RID: 1355
		public string this[int columnIndex]
		{
			get
			{
				if (!this.owner.IsHandleCreated)
				{
					return "";
				}
				NativeMethods.HDTEXTFILTER hdtextfilter = default(NativeMethods.HDTEXTFILTER);
				hdtextfilter.pszText = new string(new char[256 * Marshal.SystemDefaultCharSize]);
				hdtextfilter.cchTextMax = 256;
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(hdtextfilter));
				string pszText;
				try
				{
					Marshal.StructureToPtr(hdtextfilter, intPtr, false);
					InternalNativeMethods.HDITEM hditem = default(InternalNativeMethods.HDITEM);
					hditem.mask = 256U;
					hditem.pvFilter = intPtr;
					InternalUnsafeNativeMethods.SendMessage(this.owner.HeaderHandle, NativeMethods.HDM_GETITEM, (IntPtr)columnIndex, ref hditem);
					pszText = ((NativeMethods.HDTEXTFILTER)Marshal.PtrToStructure(intPtr, typeof(NativeMethods.HDTEXTFILTER))).pszText;
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return pszText;
			}
			set
			{
				NativeMethods.HDTEXTFILTER hdtextfilter = default(NativeMethods.HDTEXTFILTER);
				hdtextfilter.pszText = value;
				hdtextfilter.cchTextMax = value.Length + 1;
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(hdtextfilter));
				try
				{
					Marshal.StructureToPtr(hdtextfilter, intPtr, false);
					InternalNativeMethods.HDITEM hditem = default(InternalNativeMethods.HDITEM);
					hditem.mask = 256U;
					hditem.type = 0U;
					hditem.pvFilter = intPtr;
					InternalUnsafeNativeMethods.SendMessage(this.owner.HeaderHandle, NativeMethods.HDM_SETITEM, (IntPtr)columnIndex, ref hditem);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0005E52C File Offset: 0x0005C72C
		public void CopyTo(Array array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0005E55A File Offset: 0x0005C75A
		public int Count
		{
			get
			{
				return this.owner.Columns.Count;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0005E56C File Offset: 0x0005C76C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x0005E56F File Offset: 0x0005C76F
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this.owner.Columns).SyncRoot;
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005E584 File Offset: 0x0005C784
		public IEnumerator GetEnumerator()
		{
			string[] array = new string[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x0400084B RID: 2123
		private DataListView owner;
	}
}
