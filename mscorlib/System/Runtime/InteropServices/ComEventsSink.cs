using System;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000982 RID: 2434
	[SecurityCritical]
	internal class ComEventsSink : NativeMethods.IDispatch, ICustomQueryInterface
	{
		// Token: 0x06006206 RID: 25094 RVA: 0x0014D44A File Offset: 0x0014B64A
		internal ComEventsSink(object rcw, Guid iid)
		{
			this._iidSourceItf = iid;
			this.Advise(rcw);
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x0014D460 File Offset: 0x0014B660
		internal static ComEventsSink Find(ComEventsSink sinks, ref Guid iid)
		{
			ComEventsSink comEventsSink = sinks;
			while (comEventsSink != null && comEventsSink._iidSourceItf != iid)
			{
				comEventsSink = comEventsSink._next;
			}
			return comEventsSink;
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x0014D48F File Offset: 0x0014B68F
		internal static ComEventsSink Add(ComEventsSink sinks, ComEventsSink sink)
		{
			sink._next = sinks;
			return sink;
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x0014D499 File Offset: 0x0014B699
		[SecurityCritical]
		internal static ComEventsSink RemoveAll(ComEventsSink sinks)
		{
			while (sinks != null)
			{
				sinks.Unadvise();
				sinks = sinks._next;
			}
			return null;
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x0014D4B0 File Offset: 0x0014B6B0
		[SecurityCritical]
		internal static ComEventsSink Remove(ComEventsSink sinks, ComEventsSink sink)
		{
			if (sink == sinks)
			{
				sinks = sinks._next;
			}
			else
			{
				ComEventsSink comEventsSink = sinks;
				while (comEventsSink != null && comEventsSink._next != sink)
				{
					comEventsSink = comEventsSink._next;
				}
				if (comEventsSink != null)
				{
					comEventsSink._next = sink._next;
				}
			}
			sink.Unadvise();
			return sinks;
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x0014D4F8 File Offset: 0x0014B6F8
		public ComEventsMethod RemoveMethod(ComEventsMethod method)
		{
			this._methods = ComEventsMethod.Remove(this._methods, method);
			return this._methods;
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x0014D512 File Offset: 0x0014B712
		public ComEventsMethod FindMethod(int dispid)
		{
			return ComEventsMethod.Find(this._methods, dispid);
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x0014D520 File Offset: 0x0014B720
		public ComEventsMethod AddMethod(int dispid)
		{
			ComEventsMethod comEventsMethod = new ComEventsMethod(dispid);
			this._methods = ComEventsMethod.Add(this._methods, comEventsMethod);
			return comEventsMethod;
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x0014D547 File Offset: 0x0014B747
		[SecurityCritical]
		void NativeMethods.IDispatch.GetTypeInfoCount(out uint pctinfo)
		{
			pctinfo = 0U;
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x0014D54C File Offset: 0x0014B74C
		[SecurityCritical]
		void NativeMethods.IDispatch.GetTypeInfo(uint iTInfo, int lcid, out IntPtr info)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x0014D553 File Offset: 0x0014B753
		[SecurityCritical]
		void NativeMethods.IDispatch.GetIDsOfNames(ref Guid iid, string[] names, uint cNames, int lcid, int[] rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x0014D55C File Offset: 0x0014B75C
		private unsafe static Variant* GetVariant(Variant* pSrc)
		{
			if (pSrc->VariantType == (VarEnum)16396)
			{
				Variant* ptr = (Variant*)((void*)pSrc->AsByRefVariant);
				if ((ptr->VariantType & (VarEnum)20479) == (VarEnum)16396)
				{
					return ptr;
				}
			}
			return pSrc;
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x0014D598 File Offset: 0x0014B798
		[SecurityCritical]
		unsafe void NativeMethods.IDispatch.Invoke(int dispid, ref Guid riid, int lcid, INVOKEKIND wFlags, ref DISPPARAMS pDispParams, IntPtr pvarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ComEventsMethod comEventsMethod = this.FindMethod(dispid);
			if (comEventsMethod == null)
			{
				return;
			}
			object[] array = new object[pDispParams.cArgs];
			int[] array2 = new int[pDispParams.cArgs];
			bool[] array3 = new bool[pDispParams.cArgs];
			Variant* ptr = (Variant*)((void*)pDispParams.rgvarg);
			int* ptr2 = (int*)((void*)pDispParams.rgdispidNamedArgs);
			int i;
			int num;
			for (i = 0; i < pDispParams.cNamedArgs; i++)
			{
				num = ptr2[i];
				Variant* variant = ComEventsSink.GetVariant(ptr + i);
				array[num] = variant->ToObject();
				array3[num] = true;
				if (variant->IsByRef)
				{
					array2[num] = i;
				}
				else
				{
					array2[num] = -1;
				}
			}
			num = 0;
			while (i < pDispParams.cArgs)
			{
				while (array3[num])
				{
					num++;
				}
				Variant* variant2 = ComEventsSink.GetVariant(ptr + (pDispParams.cArgs - 1 - i));
				array[num] = variant2->ToObject();
				if (variant2->IsByRef)
				{
					array2[num] = pDispParams.cArgs - 1 - i;
				}
				else
				{
					array2[num] = -1;
				}
				num++;
				i++;
			}
			object obj = comEventsMethod.Invoke(array);
			if (pvarResult != IntPtr.Zero)
			{
				Marshal.GetNativeVariantForObject(obj, pvarResult);
			}
			for (i = 0; i < pDispParams.cArgs; i++)
			{
				int num2 = array2[i];
				if (num2 != -1)
				{
					ComEventsSink.GetVariant(ptr + num2)->CopyFromIndirect(array[i]);
				}
			}
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x0014D720 File Offset: 0x0014B920
		[SecurityCritical]
		CustomQueryInterfaceResult ICustomQueryInterface.GetInterface(ref Guid iid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			if (iid == this._iidSourceItf || iid == typeof(NativeMethods.IDispatch).GUID)
			{
				ppv = Marshal.GetComInterfaceForObject(this, typeof(NativeMethods.IDispatch), CustomQueryInterfaceMode.Ignore);
				return CustomQueryInterfaceResult.Handled;
			}
			if (iid == ComEventsSink.IID_IManagedObject)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x0014D790 File Offset: 0x0014B990
		private void Advise(object rcw)
		{
			IConnectionPointContainer connectionPointContainer = (IConnectionPointContainer)rcw;
			IConnectionPoint connectionPoint;
			connectionPointContainer.FindConnectionPoint(ref this._iidSourceItf, out connectionPoint);
			connectionPoint.Advise(this, out this._cookie);
			this._connectionPoint = connectionPoint;
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x0014D7C8 File Offset: 0x0014B9C8
		[SecurityCritical]
		private void Unadvise()
		{
			try
			{
				this._connectionPoint.Unadvise(this._cookie);
				Marshal.ReleaseComObject(this._connectionPoint);
			}
			catch (Exception)
			{
			}
			finally
			{
				this._connectionPoint = null;
			}
		}

		// Token: 0x04002BFF RID: 11263
		private Guid _iidSourceItf;

		// Token: 0x04002C00 RID: 11264
		private IConnectionPoint _connectionPoint;

		// Token: 0x04002C01 RID: 11265
		private int _cookie;

		// Token: 0x04002C02 RID: 11266
		private ComEventsMethod _methods;

		// Token: 0x04002C03 RID: 11267
		private ComEventsSink _next;

		// Token: 0x04002C04 RID: 11268
		private const VarEnum VT_BYREF_VARIANT = (VarEnum)16396;

		// Token: 0x04002C05 RID: 11269
		private const VarEnum VT_TYPEMASK = (VarEnum)4095;

		// Token: 0x04002C06 RID: 11270
		private const VarEnum VT_BYREF_TYPEMASK = (VarEnum)20479;

		// Token: 0x04002C07 RID: 11271
		private static Guid IID_IManagedObject = new Guid("{C3FCC19E-A970-11D2-8B5A-00A0C9B7C9C4}");
	}
}
