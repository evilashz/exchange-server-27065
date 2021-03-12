using System;
using System.Collections;
using System.Data;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000016 RID: 22
	internal abstract class BaseConfigCollection : CollectionBase
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004777 File Offset: 0x00002977
		public BaseConfigCollection()
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000477F File Offset: 0x0000297F
		public BaseConfigCollection(ConfigObject[] configObjectArray)
		{
			if (configObjectArray != null)
			{
				this.AddRange(configObjectArray);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004791 File Offset: 0x00002991
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004799 File Offset: 0x00002999
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000047A2 File Offset: 0x000029A2
		public virtual int Add(ConfigObject configObject)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyException();
			}
			return base.List.Add(configObject);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000047BE File Offset: 0x000029BE
		public virtual void AddRange(ConfigObject[] configObjectArray)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyException();
			}
			base.InnerList.AddRange(configObjectArray);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000047DA File Offset: 0x000029DA
		public virtual void Insert(int index, ConfigObject configObject)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyException();
			}
			base.List.Insert(index, configObject);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000047F7 File Offset: 0x000029F7
		public virtual void Replace(int index, ConfigObject configObject)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyException();
			}
			base.List[index] = configObject;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004814 File Offset: 0x00002A14
		public virtual void Remove(ConfigObject configObject)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyException();
			}
			base.List.Remove(configObject);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004830 File Offset: 0x00002A30
		public virtual void RemoveRange(int index, int count)
		{
			if (this.isReadOnly)
			{
				throw new ReadOnlyException();
			}
			base.InnerList.RemoveRange(index, count);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000484D File Offset: 0x00002A4D
		public virtual void CopyTo(ConfigObject[] configObjectArray, int index)
		{
			base.List.CopyTo(configObjectArray, index);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000485C File Offset: 0x00002A5C
		public virtual bool Contains(ConfigObject configObject)
		{
			return base.List.Contains(configObject);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000486A File Offset: 0x00002A6A
		public virtual bool ContainsIdentity(string identity)
		{
			return -1 != this.IndexOfIdentity(identity);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004879 File Offset: 0x00002A79
		public virtual int IndexOf(ConfigObject configObject)
		{
			return base.List.IndexOf(configObject);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004888 File Offset: 0x00002A88
		public virtual int IndexOfIdentity(string identity)
		{
			for (int i = 0; i < base.List.Count; i++)
			{
				if (((ConfigObject)base.List[i]).Identity == identity)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000049 RID: 73
		private bool isReadOnly;
	}
}
