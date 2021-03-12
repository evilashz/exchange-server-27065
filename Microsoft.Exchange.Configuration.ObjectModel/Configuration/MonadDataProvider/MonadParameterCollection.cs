using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D5 RID: 469
	internal class MonadParameterCollection : DbParameterCollection
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00033E9D File Offset: 0x0003209D
		public override int Count
		{
			get
			{
				if (this.items == null)
				{
					return 0;
				}
				return this.items.Count;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00033EB4 File Offset: 0x000320B4
		public override bool IsFixedSize
		{
			get
			{
				return ((IList)this.InnerList).IsFixedSize;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00033EC1 File Offset: 0x000320C1
		public override bool IsReadOnly
		{
			get
			{
				return ((IList)this.InnerList).IsReadOnly;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00033ECE File Offset: 0x000320CE
		public override bool IsSynchronized
		{
			get
			{
				return ((ICollection)this.InnerList).IsSynchronized;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00033EDB File Offset: 0x000320DB
		public override object SyncRoot
		{
			get
			{
				return ((ICollection)this.InnerList).SyncRoot;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00033EE8 File Offset: 0x000320E8
		private List<MonadParameter> InnerList
		{
			get
			{
				if (this.items == null)
				{
					this.items = new List<MonadParameter>();
				}
				return this.items;
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00033F03 File Offset: 0x00032103
		public MonadParameter Add(MonadParameter value)
		{
			this.InnerList.Add(value);
			return value;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00033F12 File Offset: 0x00032112
		public MonadParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new MonadParameter(parameterName, value));
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00033F24 File Offset: 0x00032124
		public MonadParameter AddSwitch(string parameterName)
		{
			return this.Add(new MonadParameter(parameterName)
			{
				IsSwitch = true
			});
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00033F46 File Offset: 0x00032146
		public void AddSwitchAsNeeded(string parameterName, bool needed)
		{
			if (needed)
			{
				this.AddSwitch(parameterName);
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00033F54 File Offset: 0x00032154
		public void Remove(string parameterName)
		{
			int index;
			while (-1 != (index = this.IndexOf(parameterName)))
			{
				this.RemoveAt(index);
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00033F76 File Offset: 0x00032176
		public override int Add(object value)
		{
			this.InnerList.Add((MonadParameter)value);
			return this.Count - 1;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00033F94 File Offset: 0x00032194
		public override void AddRange(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (object obj in values)
			{
				MonadParameter item = (MonadParameter)obj;
				this.InnerList.Add(item);
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00033FFC File Offset: 0x000321FC
		public override void Clear()
		{
			this.InnerList.Clear();
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00034009 File Offset: 0x00032209
		public override bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00034018 File Offset: 0x00032218
		public override bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00034027 File Offset: 0x00032227
		public override void CopyTo(Array array, int index)
		{
			((ICollection)this.InnerList).CopyTo(array, index);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00034036 File Offset: 0x00032236
		public override IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.InnerList).GetEnumerator();
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00034043 File Offset: 0x00032243
		public override int IndexOf(string parameterName)
		{
			return MonadParameterCollection.IndexOf(this.InnerList, parameterName);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00034051 File Offset: 0x00032251
		public override int IndexOf(object value)
		{
			return this.InnerList.IndexOf((MonadParameter)value);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00034064 File Offset: 0x00032264
		public override void Insert(int index, object value)
		{
			this.InnerList.Insert(index, (MonadParameter)value);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00034078 File Offset: 0x00032278
		public override void Remove(object value)
		{
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00034098 File Offset: 0x00032298
		public override void RemoveAt(int index)
		{
			this.InnerList.RemoveAt(index);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000340A8 File Offset: 0x000322A8
		public override void RemoveAt(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new ArgumentException();
			}
			this.RemoveAt(num);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000340D0 File Offset: 0x000322D0
		protected internal static int IndexOf(IEnumerable items, string parameterName)
		{
			if (items != null)
			{
				int num = 0;
				foreach (object obj in items)
				{
					DbParameter dbParameter = (DbParameter)obj;
					if (string.Compare(parameterName, dbParameter.ParameterName) == 0)
					{
						return num;
					}
					num++;
				}
				num = 0;
				foreach (object obj2 in items)
				{
					DbParameter dbParameter2 = (DbParameter)obj2;
					if (string.Compare(parameterName, dbParameter2.ParameterName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return num;
					}
					num++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000341A4 File Offset: 0x000323A4
		protected override DbParameter GetParameter(int index)
		{
			return this.InnerList[index];
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000341B2 File Offset: 0x000323B2
		protected override DbParameter GetParameter(string parameterName)
		{
			return this.GetParameter(this.IndexOf(parameterName));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000341C1 File Offset: 0x000323C1
		protected override void SetParameter(int index, DbParameter value)
		{
			this.InnerList[index] = (MonadParameter)value;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000341D5 File Offset: 0x000323D5
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.SetParameter(this.IndexOf(parameterName), value);
		}

		// Token: 0x040003B4 RID: 948
		private List<MonadParameter> items;
	}
}
