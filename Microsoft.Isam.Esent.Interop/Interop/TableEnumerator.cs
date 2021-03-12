using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200009C RID: 156
	internal abstract class TableEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0001074A File Offset: 0x0000E94A
		protected TableEnumerator(JET_SESID sesid)
		{
			this.Sesid = sesid;
			this.TableidToEnumerate = JET_TABLEID.Nil;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001076B File Offset: 0x0000E96B
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00010773 File Offset: 0x0000E973
		public T Current { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001077C File Offset: 0x0000E97C
		object IEnumerator.Current
		{
			[DebuggerStepThrough]
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00010789 File Offset: 0x0000E989
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00010791 File Offset: 0x0000E991
		private protected JET_SESID Sesid { protected get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001079A File Offset: 0x0000E99A
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x000107A2 File Offset: 0x0000E9A2
		protected JET_TABLEID TableidToEnumerate { get; set; }

		// Token: 0x06000728 RID: 1832 RVA: 0x000107AB File Offset: 0x0000E9AB
		public void Reset()
		{
			this.isAtEnd = false;
			this.moveToFirst = true;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000107BB File Offset: 0x0000E9BB
		public void Dispose()
		{
			this.CloseTable();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000107CC File Offset: 0x0000E9CC
		public bool MoveNext()
		{
			if (this.isAtEnd)
			{
				return false;
			}
			if (this.TableidToEnumerate.IsInvalid)
			{
				this.OpenTable();
			}
			bool flag = true;
			if (this.moveToFirst)
			{
				if (!Api.TryMoveFirst(this.Sesid, this.TableidToEnumerate))
				{
					this.isAtEnd = true;
					return false;
				}
				this.moveToFirst = false;
				flag = false;
			}
			while (flag || this.SkipCurrent())
			{
				if (!Api.TryMoveNext(this.Sesid, this.TableidToEnumerate))
				{
					this.isAtEnd = true;
					return false;
				}
				flag = false;
			}
			this.Current = this.GetCurrent();
			return true;
		}

		// Token: 0x0600072B RID: 1835
		protected abstract void OpenTable();

		// Token: 0x0600072C RID: 1836
		protected abstract T GetCurrent();

		// Token: 0x0600072D RID: 1837 RVA: 0x00010860 File Offset: 0x0000EA60
		protected virtual bool SkipCurrent()
		{
			return false;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00010864 File Offset: 0x0000EA64
		protected virtual void CloseTable()
		{
			if (!this.TableidToEnumerate.IsInvalid)
			{
				Api.JetCloseTable(this.Sesid, this.TableidToEnumerate);
			}
			this.TableidToEnumerate = JET_TABLEID.Nil;
		}

		// Token: 0x04000320 RID: 800
		private bool isAtEnd;

		// Token: 0x04000321 RID: 801
		private bool moveToFirst = true;
	}
}
