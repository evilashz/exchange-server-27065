using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005A5 RID: 1445
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class ConstructorInfo : MethodBase, _ConstructorInfo
	{
		// Token: 0x060043C8 RID: 17352 RVA: 0x000F88DC File Offset: 0x000F6ADC
		[__DynamicallyInvokable]
		public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeConstructorInfo) && !(right is RuntimeConstructorInfo) && left.Equals(right));
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000F8903 File Offset: 0x000F6B03
		[__DynamicallyInvokable]
		public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000F890F File Offset: 0x000F6B0F
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x000F8918 File Offset: 0x000F6B18
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000F8920 File Offset: 0x000F6B20
		internal virtual Type GetReturnType()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060043CD RID: 17357 RVA: 0x000F8927 File Offset: 0x000F6B27
		[ComVisible(true)]
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x060043CE RID: 17358
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x060043CF RID: 17359 RVA: 0x000F892A File Offset: 0x000F6B2A
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object Invoke(object[] parameters)
		{
			return this.Invoke(BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x000F8936 File Offset: 0x000F6B36
		Type _ConstructorInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x000F893E File Offset: 0x000F6B3E
		object _ConstructorInfo.Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x000F894D File Offset: 0x000F6B4D
		object _ConstructorInfo.Invoke_3(object obj, object[] parameters)
		{
			return base.Invoke(obj, parameters);
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000F8957 File Offset: 0x000F6B57
		object _ConstructorInfo.Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.Invoke(invokeAttr, binder, parameters, culture);
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000F8964 File Offset: 0x000F6B64
		object _ConstructorInfo.Invoke_5(object[] parameters)
		{
			return this.Invoke(parameters);
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x000F896D File Offset: 0x000F6B6D
		void _ConstructorInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x000F8974 File Offset: 0x000F6B74
		void _ConstructorInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000F897B File Offset: 0x000F6B7B
		void _ConstructorInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000F8982 File Offset: 0x000F6B82
		void _ConstructorInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001B8F RID: 7055
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x04001B90 RID: 7056
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static readonly string TypeConstructorName = ".cctor";
	}
}
