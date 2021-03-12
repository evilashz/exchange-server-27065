using System;
using System.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200029B RID: 667
	internal sealed class SelectedObjects : IComparable
	{
		// Token: 0x06001C54 RID: 7252 RVA: 0x0007AD44 File Offset: 0x00078F44
		public SelectedObjects(ICollection selectedObjects)
		{
			this.selectedObjects = selectedObjects;
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x0007AD53 File Offset: 0x00078F53
		public int Count
		{
			get
			{
				return this.selectedObjects.Count;
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0007AD60 File Offset: 0x00078F60
		private object[] GetSortedArray()
		{
			object[] array = this.selectedObjects as object[];
			if (!this.isSortedArray)
			{
				if (array == null)
				{
					array = new object[this.selectedObjects.Count];
					this.selectedObjects.CopyTo(array, 0);
				}
				ExTraceGlobals.ProgramFlowTracer.TracePerformance<int>(0L, "-->SelectedObjects.GetSortedArray: sorting {0} objects.", array.Length);
				Array.Sort(array, new SelectedObjects.ADObjectIdComparer());
				ExTraceGlobals.ProgramFlowTracer.TracePerformance<int>(0L, "<--SelectedObjects.GetSortedArray: sorting {0} objects.", array.Length);
				this.selectedObjects = array;
				this.isSortedArray = true;
			}
			return array;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0007ADE4 File Offset: 0x00078FE4
		public int CompareTo(object obj)
		{
			if (obj == this)
			{
				return 0;
			}
			SelectedObjects selectedObjects = obj as SelectedObjects;
			if (selectedObjects == null)
			{
				return -1;
			}
			if (this.Count != selectedObjects.Count)
			{
				return this.Count - selectedObjects.Count;
			}
			object[] sortedArray = this.GetSortedArray();
			object[] sortedArray2 = selectedObjects.GetSortedArray();
			for (int i = 0; i < sortedArray.Length; i++)
			{
				if (!object.Equals(sortedArray[i], sortedArray2[i]))
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0007AE4B File Offset: 0x0007904B
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0007AE58 File Offset: 0x00079058
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				object[] sortedArray = this.GetSortedArray();
				this.hashCode = sortedArray.Length.GetHashCode();
				foreach (object obj in sortedArray)
				{
					this.hashCode = (this.hashCode << 13 | (int)((uint)this.hashCode >> 19));
					if (obj != null)
					{
						this.hashCode ^= obj.GetHashCode();
					}
				}
			}
			return this.hashCode;
		}

		// Token: 0x04000A91 RID: 2705
		private bool isSortedArray;

		// Token: 0x04000A92 RID: 2706
		private ICollection selectedObjects;

		// Token: 0x04000A93 RID: 2707
		private int hashCode;

		// Token: 0x0200029C RID: 668
		private class ADObjectIdComparer : IComparer
		{
			// Token: 0x06001C5A RID: 7258 RVA: 0x0007AED4 File Offset: 0x000790D4
			int IComparer.Compare(object x, object y)
			{
				int result = 0;
				ADObjectId adobjectId = x as ADObjectId;
				ADObjectId adobjectId2 = y as ADObjectId;
				if (adobjectId != null && adobjectId2 != null)
				{
					result = adobjectId.ObjectGuid.CompareTo((y as ADObjectId).ObjectGuid);
				}
				else if (x is IComparable && y is IComparable)
				{
					result = (x as IComparable).CompareTo(y);
				}
				return result;
			}
		}
	}
}
