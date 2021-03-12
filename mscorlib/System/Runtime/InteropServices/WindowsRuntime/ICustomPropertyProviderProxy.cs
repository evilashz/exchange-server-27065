using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E2 RID: 2530
	internal class ICustomPropertyProviderProxy<T1, T2> : IGetProxyTarget, ICustomPropertyProvider, ICustomQueryInterface, IEnumerable, IBindableVector, IBindableIterable, IBindableVectorView
	{
		// Token: 0x0600646D RID: 25709 RVA: 0x00154A40 File Offset: 0x00152C40
		internal ICustomPropertyProviderProxy(object target, InterfaceForwardingSupport flags)
		{
			this._target = target;
			this._flags = flags;
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x00154A58 File Offset: 0x00152C58
		internal static object CreateInstance(object target)
		{
			InterfaceForwardingSupport interfaceForwardingSupport = InterfaceForwardingSupport.None;
			if (target is IList)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableVector;
			}
			if (target is IList<!0>)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IVector;
			}
			if (target is IBindableVectorView)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableVectorView;
			}
			if (target is IReadOnlyList<T2>)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IVectorView;
			}
			if (target is IEnumerable)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableIterableOrIIterable;
			}
			return new ICustomPropertyProviderProxy<T1, T2>(target, interfaceForwardingSupport);
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x00154AAB File Offset: 0x00152CAB
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._target, name);
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x00154AB9 File Offset: 0x00152CB9
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._target, name, indexParameterType);
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x00154AC8 File Offset: 0x00152CC8
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return IStringableHelper.ToString(this._target);
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x00154AD5 File Offset: 0x00152CD5
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._target.GetType();
			}
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x00154AE2 File Offset: 0x00152CE2
		public override string ToString()
		{
			return IStringableHelper.ToString(this._target);
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x00154AEF File Offset: 0x00152CEF
		object IGetProxyTarget.GetTarget()
		{
			return this._target;
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x00154AF8 File Offset: 0x00152CF8
		[SecurityCritical]
		public CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			if (iid == typeof(IBindableIterable).GUID && (this._flags & InterfaceForwardingSupport.IBindableIterableOrIIterable) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			if (iid == typeof(IBindableVector).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVector | InterfaceForwardingSupport.IVector)) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			if (iid == typeof(IBindableVectorView).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVectorView | InterfaceForwardingSupport.IVectorView)) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x00154B87 File Offset: 0x00152D87
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this._target).GetEnumerator();
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00154B9C File Offset: 0x00152D9C
		object IBindableVector.GetAt(uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.GetAt(index);
			}
			return this.GetVectorOfT().GetAt(index);
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x00154BCC File Offset: 0x00152DCC
		uint IBindableVector.Size
		{
			get
			{
				IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
				if (ibindableVectorNoThrow != null)
				{
					return ibindableVectorNoThrow.Size;
				}
				return this.GetVectorOfT().Size;
			}
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x00154BF8 File Offset: 0x00152DF8
		IBindableVectorView IBindableVector.GetView()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.GetView();
			}
			return new ICustomPropertyProviderProxy<T1, T2>.IVectorViewToIBindableVectorViewAdapter<T1>(this.GetVectorOfT().GetView());
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x00154C28 File Offset: 0x00152E28
		bool IBindableVector.IndexOf(object value, out uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.IndexOf(value, out index);
			}
			return this.GetVectorOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value), out index);
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x00154C5C File Offset: 0x00152E5C
		void IBindableVector.SetAt(uint index, object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.SetAt(index, value);
				return;
			}
			this.GetVectorOfT().SetAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x00154C90 File Offset: 0x00152E90
		void IBindableVector.InsertAt(uint index, object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.InsertAt(index, value);
				return;
			}
			this.GetVectorOfT().InsertAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x00154CC4 File Offset: 0x00152EC4
		void IBindableVector.RemoveAt(uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.RemoveAt(index);
				return;
			}
			this.GetVectorOfT().RemoveAt(index);
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00154CF0 File Offset: 0x00152EF0
		void IBindableVector.Append(object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.Append(value);
				return;
			}
			this.GetVectorOfT().Append(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x00154D20 File Offset: 0x00152F20
		void IBindableVector.RemoveAtEnd()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.RemoveAtEnd();
				return;
			}
			this.GetVectorOfT().RemoveAtEnd();
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00154D4C File Offset: 0x00152F4C
		void IBindableVector.Clear()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.Clear();
				return;
			}
			this.GetVectorOfT().Clear();
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x00154D75 File Offset: 0x00152F75
		[SecuritySafeCritical]
		private IBindableVector GetIBindableVectorNoThrow()
		{
			if ((this._flags & InterfaceForwardingSupport.IBindableVector) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IBindableVector>(this._target);
			}
			return null;
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x00154D8E File Offset: 0x00152F8E
		[SecuritySafeCritical]
		private IVector_Raw<T1> GetVectorOfT()
		{
			if ((this._flags & InterfaceForwardingSupport.IVector) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IVector_Raw<T1>>(this._target);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x00154DAC File Offset: 0x00152FAC
		object IBindableVectorView.GetAt(uint index)
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.GetAt(index);
			}
			return this.GetVectorViewOfT().GetAt(index);
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x00154DDC File Offset: 0x00152FDC
		uint IBindableVectorView.Size
		{
			get
			{
				IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
				if (ibindableVectorViewNoThrow != null)
				{
					return ibindableVectorViewNoThrow.Size;
				}
				return this.GetVectorViewOfT().Size;
			}
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x00154E08 File Offset: 0x00153008
		bool IBindableVectorView.IndexOf(object value, out uint index)
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.IndexOf(value, out index);
			}
			return this.GetVectorViewOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T2>(value), out index);
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x00154E3C File Offset: 0x0015303C
		IBindableIterator IBindableIterable.First()
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.First();
			}
			return new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T2>(this.GetVectorViewOfT().First());
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x00154E6A File Offset: 0x0015306A
		[SecuritySafeCritical]
		private IBindableVectorView GetIBindableVectorViewNoThrow()
		{
			if ((this._flags & InterfaceForwardingSupport.IBindableVectorView) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IBindableVectorView>(this._target);
			}
			return null;
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x00154E83 File Offset: 0x00153083
		[SecuritySafeCritical]
		private IVectorView<T2> GetVectorViewOfT()
		{
			if ((this._flags & InterfaceForwardingSupport.IVectorView) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IVectorView<T2>>(this._target);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x00154EA0 File Offset: 0x001530A0
		private static T ConvertTo<T>(object value)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			return (T)((object)value);
		}

		// Token: 0x04002C86 RID: 11398
		private object _target;

		// Token: 0x04002C87 RID: 11399
		private InterfaceForwardingSupport _flags;

		// Token: 0x02000C73 RID: 3187
		private sealed class IVectorViewToIBindableVectorViewAdapter<T> : IBindableVectorView, IBindableIterable
		{
			// Token: 0x06007022 RID: 28706 RVA: 0x001812C7 File Offset: 0x0017F4C7
			public IVectorViewToIBindableVectorViewAdapter(IVectorView<T> vectorView)
			{
				this._vectorView = vectorView;
			}

			// Token: 0x06007023 RID: 28707 RVA: 0x001812D6 File Offset: 0x0017F4D6
			object IBindableVectorView.GetAt(uint index)
			{
				return this._vectorView.GetAt(index);
			}

			// Token: 0x17001351 RID: 4945
			// (get) Token: 0x06007024 RID: 28708 RVA: 0x001812E9 File Offset: 0x0017F4E9
			uint IBindableVectorView.Size
			{
				get
				{
					return this._vectorView.Size;
				}
			}

			// Token: 0x06007025 RID: 28709 RVA: 0x001812F6 File Offset: 0x0017F4F6
			bool IBindableVectorView.IndexOf(object value, out uint index)
			{
				return this._vectorView.IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T>(value), out index);
			}

			// Token: 0x06007026 RID: 28710 RVA: 0x0018130A File Offset: 0x0017F50A
			IBindableIterator IBindableIterable.First()
			{
				return new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T>(this._vectorView.First());
			}

			// Token: 0x040037A4 RID: 14244
			private IVectorView<T> _vectorView;
		}

		// Token: 0x02000C74 RID: 3188
		private sealed class IteratorOfTToIteratorAdapter<T> : IBindableIterator
		{
			// Token: 0x06007027 RID: 28711 RVA: 0x0018131C File Offset: 0x0017F51C
			public IteratorOfTToIteratorAdapter(IIterator<T> iterator)
			{
				this._iterator = iterator;
			}

			// Token: 0x17001352 RID: 4946
			// (get) Token: 0x06007028 RID: 28712 RVA: 0x0018132B File Offset: 0x0017F52B
			public bool HasCurrent
			{
				get
				{
					return this._iterator.HasCurrent;
				}
			}

			// Token: 0x17001353 RID: 4947
			// (get) Token: 0x06007029 RID: 28713 RVA: 0x00181338 File Offset: 0x0017F538
			public object Current
			{
				get
				{
					return this._iterator.Current;
				}
			}

			// Token: 0x0600702A RID: 28714 RVA: 0x0018134A File Offset: 0x0017F54A
			public bool MoveNext()
			{
				return this._iterator.MoveNext();
			}

			// Token: 0x040037A5 RID: 14245
			private IIterator<T> _iterator;
		}
	}
}
