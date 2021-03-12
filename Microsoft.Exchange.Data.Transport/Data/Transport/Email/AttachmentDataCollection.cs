using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000C2 RID: 194
	internal class AttachmentDataCollection<T> : IEnumerable<T>, IEnumerable where T : AttachmentData
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00009E58 File Offset: 0x00008058
		public int Count
		{
			get
			{
				int num = 0;
				foreach (T t in this.list)
				{
					if (t != null)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00009EB4 File Offset: 0x000080B4
		public List<T> InternalList
		{
			[DebuggerStepThrough]
			get
			{
				return this.list;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00009EBC File Offset: 0x000080BC
		public AttachmentDataCollection()
		{
			this.list = new List<T>();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00009ED0 File Offset: 0x000080D0
		public int Add(T item)
		{
			int count = this.list.Count;
			this.list.Add(item);
			return count;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009EF8 File Offset: 0x000080F8
		public bool RemoveAtPrivateIndex(int index)
		{
			T t = this.list[index];
			this.list[index] = default(T);
			return t != null;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00009F34 File Offset: 0x00008134
		public void Clear()
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				this.list[i] = default(T);
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00009F6C File Offset: 0x0000816C
		public void Reset()
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				MimeAttachmentData mimeAttachmentData = this.list[i] as MimeAttachmentData;
				if (mimeAttachmentData != null)
				{
					mimeAttachmentData.Referenced = false;
				}
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00009FB0 File Offset: 0x000081B0
		public T GetDataAtPrivateIndex(int privateIndex)
		{
			return this.list[privateIndex];
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00009FC0 File Offset: 0x000081C0
		public T GetDataAtPublicIndex(int publicIndex)
		{
			int privateIndex = this.GetPrivateIndex(publicIndex);
			if (privateIndex >= 0)
			{
				return this.list[privateIndex];
			}
			return default(T);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00009FF0 File Offset: 0x000081F0
		public int GetPrivateIndex(int publicIndex)
		{
			int num = 0;
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i] != null)
				{
					if (publicIndex == num)
					{
						return i;
					}
					num++;
				}
			}
			return -1;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000A104 File Offset: 0x00008304
		public IEnumerator<T> GetEnumerator()
		{
			for (int index = 0; index < this.list.Count; index++)
			{
				T data = this.list[index];
				if (data != null)
				{
					yield return data;
				}
			}
			yield break;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000A120 File Offset: 0x00008320
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400027C RID: 636
		private List<T> list;
	}
}
