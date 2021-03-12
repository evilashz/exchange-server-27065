using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000513 RID: 1299
	[DebuggerTypeProxy(typeof(SystemThreading_ThreadLocalDebugView<>))]
	[DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ThreadLocal<T> : IDisposable
	{
		// Token: 0x06003DC9 RID: 15817 RVA: 0x000E5613 File Offset: 0x000E3813
		[__DynamicallyInvokable]
		public ThreadLocal()
		{
			this.Initialize(null, false);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000E562F File Offset: 0x000E382F
		[__DynamicallyInvokable]
		public ThreadLocal(bool trackAllValues)
		{
			this.Initialize(null, trackAllValues);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000E564B File Offset: 0x000E384B
		[__DynamicallyInvokable]
		public ThreadLocal(Func<T> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, false);
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000E5675 File Offset: 0x000E3875
		[__DynamicallyInvokable]
		public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, trackAllValues);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000E56A0 File Offset: 0x000E38A0
		private void Initialize(Func<T> valueFactory, bool trackAllValues)
		{
			this.m_valueFactory = valueFactory;
			this.m_trackAllValues = trackAllValues;
			try
			{
			}
			finally
			{
				this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
				this.m_initialized = true;
			}
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000E56E8 File Offset: 0x000E38E8
		[__DynamicallyInvokable]
		~ThreadLocal()
		{
			this.Dispose(false);
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000E5718 File Offset: 0x000E3918
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000E5728 File Offset: 0x000E3928
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			ThreadLocal<T>.IdManager obj = ThreadLocal<T>.s_idManager;
			int num;
			lock (obj)
			{
				num = ~this.m_idComplement;
				this.m_idComplement = 0;
				if (num < 0 || !this.m_initialized)
				{
					return;
				}
				this.m_initialized = false;
				for (ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next; next != null; next = next.Next)
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = next.SlotArray;
					if (slotArray != null)
					{
						next.SlotArray = null;
						slotArray[num].Value.Value = default(T);
						slotArray[num].Value = null;
					}
				}
			}
			this.m_linkedSlot = null;
			ThreadLocal<T>.s_idManager.ReturnId(num);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000E57FC File Offset: 0x000E39FC
		[__DynamicallyInvokable]
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x000E5820 File Offset: 0x000E3A20
		// (set) Token: 0x06003DD3 RID: 15827 RVA: 0x000E5874 File Offset: 0x000E3A74
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array != null && num >= 0 && num < array.Length && (value = array[num].Value) != null && this.m_initialized)
				{
					return value.Value;
				}
				return this.GetValueSlow();
			}
			[__DynamicallyInvokable]
			set
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value2;
				if (array != null && num >= 0 && num < array.Length && (value2 = array[num].Value) != null && this.m_initialized)
				{
					value2.Value = value;
					return;
				}
				this.SetValueSlow(value, array);
			}
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000E58C8 File Offset: 0x000E3AC8
		private T GetValueSlow()
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
			}
			Debugger.NotifyOfCrossThreadDependency();
			T t;
			if (this.m_valueFactory == null)
			{
				t = default(T);
			}
			else
			{
				t = this.m_valueFactory();
				if (this.IsValueCreated)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_Value_RecursiveCallsToValue"));
				}
			}
			this.Value = t;
			return t;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000E5934 File Offset: 0x000E3B34
		private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
			}
			if (slotArray == null)
			{
				slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(num + 1)];
				ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (num >= slotArray.Length)
			{
				this.GrowTable(ref slotArray, num + 1);
				ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (slotArray[num].Value == null)
			{
				this.CreateLinkedSlot(slotArray, num, value);
				return;
			}
			ThreadLocal<T>.LinkedSlot value2 = slotArray[num].Value;
			if (!this.m_initialized)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
			}
			value2.Value = value;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000E59F4 File Offset: 0x000E3BF4
		private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
			ThreadLocal<T>.IdManager obj = ThreadLocal<T>.s_idManager;
			lock (obj)
			{
				if (!this.m_initialized)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
				}
				ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next;
				linkedSlot.Next = next;
				linkedSlot.Previous = this.m_linkedSlot;
				linkedSlot.Value = value;
				if (next != null)
				{
					next.Previous = linkedSlot;
				}
				this.m_linkedSlot.Next = linkedSlot;
				slotArray[id].Value = linkedSlot;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x000E5AA4 File Offset: 0x000E3CA4
		[__DynamicallyInvokable]
		public IList<T> Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.m_trackAllValues)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_ValuesNotAvailable"));
				}
				List<T> valuesAsList = this.GetValuesAsList();
				if (valuesAsList == null)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
				}
				return valuesAsList;
			}
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000E5AE4 File Offset: 0x000E3CE4
		private List<T> GetValuesAsList()
		{
			List<T> list = new List<T>();
			int num = ~this.m_idComplement;
			if (num == -1)
			{
				return null;
			}
			for (ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next; next != null; next = next.Next)
			{
				list.Add(next.Value);
			}
			return list;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x000E5B30 File Offset: 0x000E3D30
		private int ValuesCountForDebugDisplay
		{
			get
			{
				int num = 0;
				for (ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next; next != null; next = next.Next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x000E5B60 File Offset: 0x000E3D60
		[__DynamicallyInvokable]
		public bool IsValueCreated
		{
			[__DynamicallyInvokable]
			get
			{
				int num = ~this.m_idComplement;
				if (num < 0)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
				}
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				return array != null && num < array.Length && array[num].Value != null;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000E5BAC File Offset: 0x000E3DAC
		internal T ValueForDebugDisplay
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array == null || num >= array.Length || (value = array[num].Value) == null || !this.m_initialized)
				{
					return default(T);
				}
				return value.Value;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000E5BFC File Offset: 0x000E3DFC
		internal List<T> ValuesForDebugDisplay
		{
			get
			{
				return this.GetValuesAsList();
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000E5C04 File Offset: 0x000E3E04
		private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
		{
			int newTableSize = ThreadLocal<T>.GetNewTableSize(minLength);
			ThreadLocal<T>.LinkedSlotVolatile[] array = new ThreadLocal<T>.LinkedSlotVolatile[newTableSize];
			ThreadLocal<T>.IdManager obj = ThreadLocal<T>.s_idManager;
			lock (obj)
			{
				for (int i = 0; i < table.Length; i++)
				{
					ThreadLocal<T>.LinkedSlot value = table[i].Value;
					if (value != null && value.SlotArray != null)
					{
						value.SlotArray = array;
						array[i] = table[i];
					}
				}
			}
			table = array;
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000E5CA0 File Offset: 0x000E3EA0
		private static int GetNewTableSize(int minSize)
		{
			if (minSize > 2146435071)
			{
				return int.MaxValue;
			}
			int num = minSize - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			return num;
		}

		// Token: 0x040019BC RID: 6588
		private Func<T> m_valueFactory;

		// Token: 0x040019BD RID: 6589
		[ThreadStatic]
		private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;

		// Token: 0x040019BE RID: 6590
		[ThreadStatic]
		private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;

		// Token: 0x040019BF RID: 6591
		private int m_idComplement;

		// Token: 0x040019C0 RID: 6592
		private volatile bool m_initialized;

		// Token: 0x040019C1 RID: 6593
		private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();

		// Token: 0x040019C2 RID: 6594
		private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot(null);

		// Token: 0x040019C3 RID: 6595
		private bool m_trackAllValues;

		// Token: 0x02000BC5 RID: 3013
		private struct LinkedSlotVolatile
		{
			// Token: 0x0400355C RID: 13660
			internal volatile ThreadLocal<T>.LinkedSlot Value;
		}

		// Token: 0x02000BC6 RID: 3014
		private sealed class LinkedSlot
		{
			// Token: 0x06006E5C RID: 28252 RVA: 0x0017BB23 File Offset: 0x00179D23
			internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
			{
				this.SlotArray = slotArray;
			}

			// Token: 0x0400355D RID: 13661
			internal volatile ThreadLocal<T>.LinkedSlot Next;

			// Token: 0x0400355E RID: 13662
			internal volatile ThreadLocal<T>.LinkedSlot Previous;

			// Token: 0x0400355F RID: 13663
			internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04003560 RID: 13664
			internal T Value;
		}

		// Token: 0x02000BC7 RID: 3015
		private class IdManager
		{
			// Token: 0x06006E5D RID: 28253 RVA: 0x0017BB34 File Offset: 0x00179D34
			internal int GetId()
			{
				List<bool> freeIds = this.m_freeIds;
				int result;
				lock (freeIds)
				{
					int num = this.m_nextIdToTry;
					while (num < this.m_freeIds.Count && !this.m_freeIds[num])
					{
						num++;
					}
					if (num == this.m_freeIds.Count)
					{
						this.m_freeIds.Add(false);
					}
					else
					{
						this.m_freeIds[num] = false;
					}
					this.m_nextIdToTry = num + 1;
					result = num;
				}
				return result;
			}

			// Token: 0x06006E5E RID: 28254 RVA: 0x0017BBCC File Offset: 0x00179DCC
			internal void ReturnId(int id)
			{
				List<bool> freeIds = this.m_freeIds;
				lock (freeIds)
				{
					this.m_freeIds[id] = true;
					if (id < this.m_nextIdToTry)
					{
						this.m_nextIdToTry = id;
					}
				}
			}

			// Token: 0x04003561 RID: 13665
			private int m_nextIdToTry;

			// Token: 0x04003562 RID: 13666
			private List<bool> m_freeIds = new List<bool>();
		}

		// Token: 0x02000BC8 RID: 3016
		private class FinalizationHelper
		{
			// Token: 0x06006E60 RID: 28256 RVA: 0x0017BC37 File Offset: 0x00179E37
			internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
			{
				this.SlotArray = slotArray;
				this.m_trackAllValues = trackAllValues;
			}

			// Token: 0x06006E61 RID: 28257 RVA: 0x0017BC50 File Offset: 0x00179E50
			protected override void Finalize()
			{
				try
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = this.SlotArray;
					for (int i = 0; i < slotArray.Length; i++)
					{
						ThreadLocal<T>.LinkedSlot value = slotArray[i].Value;
						if (value != null)
						{
							if (this.m_trackAllValues)
							{
								value.SlotArray = null;
							}
							else
							{
								ThreadLocal<T>.IdManager s_idManager = ThreadLocal<T>.s_idManager;
								lock (s_idManager)
								{
									if (value.Next != null)
									{
										value.Next.Previous = value.Previous;
									}
									value.Previous.Next = value.Next;
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x04003563 RID: 13667
			internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04003564 RID: 13668
			private bool m_trackAllValues;
		}
	}
}
