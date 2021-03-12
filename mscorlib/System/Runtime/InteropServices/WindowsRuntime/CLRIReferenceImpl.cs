using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D4 RID: 2516
	internal sealed class CLRIReferenceImpl<T> : CLRIPropertyValueImpl, IReference<T>, IPropertyValue, ICustomPropertyProvider
	{
		// Token: 0x06006414 RID: 25620 RVA: 0x001540F6 File Offset: 0x001522F6
		public CLRIReferenceImpl(PropertyType type, T obj) : base(type, obj)
		{
			this._value = obj;
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x0015410C File Offset: 0x0015230C
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x00154114 File Offset: 0x00152314
		public override string ToString()
		{
			if (this._value != null)
			{
				return this._value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x0015413B File Offset: 0x0015233B
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._value, name);
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x0015414E File Offset: 0x0015234E
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._value, name, indexParameterType);
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x00154162 File Offset: 0x00152362
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return this._value.ToString();
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600641A RID: 25626 RVA: 0x00154174 File Offset: 0x00152374
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._value.GetType();
			}
		}

		// Token: 0x0600641B RID: 25627 RVA: 0x00154188 File Offset: 0x00152388
		[FriendAccessAllowed]
		internal static object UnboxHelper(object wrapper)
		{
			IReference<T> reference = (IReference<T>)wrapper;
			return reference.Value;
		}

		// Token: 0x04002C79 RID: 11385
		private T _value;
	}
}
