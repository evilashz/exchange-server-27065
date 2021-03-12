using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000981 RID: 2433
	internal class ComEventsMethod
	{
		// Token: 0x060061FD RID: 25085 RVA: 0x0014D205 File Offset: 0x0014B405
		internal ComEventsMethod(int dispid)
		{
			this._delegateWrappers = null;
			this._dispid = dispid;
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x0014D21B File Offset: 0x0014B41B
		internal static ComEventsMethod Find(ComEventsMethod methods, int dispid)
		{
			while (methods != null && methods._dispid != dispid)
			{
				methods = methods._next;
			}
			return methods;
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x0014D234 File Offset: 0x0014B434
		internal static ComEventsMethod Add(ComEventsMethod methods, ComEventsMethod method)
		{
			method._next = methods;
			return method;
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x0014D240 File Offset: 0x0014B440
		internal static ComEventsMethod Remove(ComEventsMethod methods, ComEventsMethod method)
		{
			if (methods == method)
			{
				methods = methods._next;
			}
			else
			{
				ComEventsMethod comEventsMethod = methods;
				while (comEventsMethod != null && comEventsMethod._next != method)
				{
					comEventsMethod = comEventsMethod._next;
				}
				if (comEventsMethod != null)
				{
					comEventsMethod._next = method._next;
				}
			}
			return methods;
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06006201 RID: 25089 RVA: 0x0014D282 File Offset: 0x0014B482
		internal int DispId
		{
			get
			{
				return this._dispid;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06006202 RID: 25090 RVA: 0x0014D28A File Offset: 0x0014B48A
		internal bool Empty
		{
			get
			{
				return this._delegateWrappers == null || this._delegateWrappers.Length == 0;
			}
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x0014D2A0 File Offset: 0x0014B4A0
		internal void AddDelegate(Delegate d)
		{
			int num = 0;
			if (this._delegateWrappers != null)
			{
				num = this._delegateWrappers.Length;
			}
			for (int i = 0; i < num; i++)
			{
				if (this._delegateWrappers[i].Delegate.GetType() == d.GetType())
				{
					this._delegateWrappers[i].Delegate = Delegate.Combine(this._delegateWrappers[i].Delegate, d);
					return;
				}
			}
			ComEventsMethod.DelegateWrapper[] array = new ComEventsMethod.DelegateWrapper[num + 1];
			if (num > 0)
			{
				this._delegateWrappers.CopyTo(array, 0);
			}
			ComEventsMethod.DelegateWrapper delegateWrapper = new ComEventsMethod.DelegateWrapper(d);
			array[num] = delegateWrapper;
			this._delegateWrappers = array;
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x0014D338 File Offset: 0x0014B538
		internal void RemoveDelegate(Delegate d)
		{
			int num = this._delegateWrappers.Length;
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (this._delegateWrappers[i].Delegate.GetType() == d.GetType())
				{
					num2 = i;
					break;
				}
			}
			if (num2 < 0)
			{
				return;
			}
			Delegate @delegate = Delegate.Remove(this._delegateWrappers[num2].Delegate, d);
			if (@delegate != null)
			{
				this._delegateWrappers[num2].Delegate = @delegate;
				return;
			}
			if (num == 1)
			{
				this._delegateWrappers = null;
				return;
			}
			ComEventsMethod.DelegateWrapper[] array = new ComEventsMethod.DelegateWrapper[num - 1];
			int j;
			for (j = 0; j < num2; j++)
			{
				array[j] = this._delegateWrappers[j];
			}
			while (j < num - 1)
			{
				array[j] = this._delegateWrappers[j + 1];
				j++;
			}
			this._delegateWrappers = array;
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x0014D408 File Offset: 0x0014B608
		internal object Invoke(object[] args)
		{
			object result = null;
			ComEventsMethod.DelegateWrapper[] delegateWrappers = this._delegateWrappers;
			foreach (ComEventsMethod.DelegateWrapper delegateWrapper in delegateWrappers)
			{
				if (delegateWrapper != null && delegateWrapper.Delegate != null)
				{
					result = delegateWrapper.Invoke(args);
				}
			}
			return result;
		}

		// Token: 0x04002BFC RID: 11260
		private ComEventsMethod.DelegateWrapper[] _delegateWrappers;

		// Token: 0x04002BFD RID: 11261
		private int _dispid;

		// Token: 0x04002BFE RID: 11262
		private ComEventsMethod _next;

		// Token: 0x02000C67 RID: 3175
		internal class DelegateWrapper
		{
			// Token: 0x06006FF4 RID: 28660 RVA: 0x001809A0 File Offset: 0x0017EBA0
			public DelegateWrapper(Delegate d)
			{
				this._d = d;
			}

			// Token: 0x1700134B RID: 4939
			// (get) Token: 0x06006FF5 RID: 28661 RVA: 0x001809AF File Offset: 0x0017EBAF
			// (set) Token: 0x06006FF6 RID: 28662 RVA: 0x001809B7 File Offset: 0x0017EBB7
			public Delegate Delegate
			{
				get
				{
					return this._d;
				}
				set
				{
					this._d = value;
				}
			}

			// Token: 0x06006FF7 RID: 28663 RVA: 0x001809C0 File Offset: 0x0017EBC0
			public object Invoke(object[] args)
			{
				if (this._d == null)
				{
					return null;
				}
				if (!this._once)
				{
					this.PreProcessSignature();
					this._once = true;
				}
				if (this._cachedTargetTypes != null && this._expectedParamsCount == args.Length)
				{
					for (int i = 0; i < this._expectedParamsCount; i++)
					{
						if (this._cachedTargetTypes[i] != null)
						{
							args[i] = Enum.ToObject(this._cachedTargetTypes[i], args[i]);
						}
					}
				}
				return this._d.DynamicInvoke(args);
			}

			// Token: 0x06006FF8 RID: 28664 RVA: 0x00180A40 File Offset: 0x0017EC40
			private void PreProcessSignature()
			{
				ParameterInfo[] parameters = this._d.Method.GetParameters();
				this._expectedParamsCount = parameters.Length;
				Type[] array = new Type[this._expectedParamsCount];
				bool flag = false;
				for (int i = 0; i < this._expectedParamsCount; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					if (parameterInfo.ParameterType.IsByRef && parameterInfo.ParameterType.HasElementType && parameterInfo.ParameterType.GetElementType().IsEnum)
					{
						flag = true;
						array[i] = parameterInfo.ParameterType.GetElementType();
					}
				}
				if (flag)
				{
					this._cachedTargetTypes = array;
				}
			}

			// Token: 0x04003777 RID: 14199
			private Delegate _d;

			// Token: 0x04003778 RID: 14200
			private bool _once;

			// Token: 0x04003779 RID: 14201
			private int _expectedParamsCount;

			// Token: 0x0400377A RID: 14202
			private Type[] _cachedTargetTypes;
		}
	}
}
