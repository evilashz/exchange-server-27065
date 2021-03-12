using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005B8 RID: 1464
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_FieldInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class FieldInfo : MemberInfo, _FieldInfo
	{
		// Token: 0x060044C8 RID: 17608 RVA: 0x000FCD6C File Offset: 0x000FAF6C
		[__DynamicallyInvokable]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			FieldInfo fieldInfo = RuntimeType.GetFieldInfo(handle.GetRuntimeFieldInfo());
			Type declaringType = fieldInfo.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FieldDeclaringTypeGeneric"), fieldInfo.Name, declaringType.GetGenericTypeDefinition()));
			}
			return fieldInfo;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x000FCDDE File Offset: 0x000FAFDE
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			return RuntimeType.GetFieldInfo(declaringType.GetRuntimeType(), handle.GetRuntimeFieldInfo());
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x000FCE14 File Offset: 0x000FB014
		[__DynamicallyInvokable]
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeFieldInfo) && !(right is RuntimeFieldInfo) && left.Equals(right));
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x000FCE3B File Offset: 0x000FB03B
		[__DynamicallyInvokable]
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x000FCE47 File Offset: 0x000FB047
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000FCE50 File Offset: 0x000FB050
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x000FCE58 File Offset: 0x000FB058
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000FCE5B File Offset: 0x000FB05B
		public virtual Type[] GetRequiredCustomModifiers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000FCE62 File Offset: 0x000FB062
		public virtual Type[] GetOptionalCustomModifiers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x000FCE69 File Offset: 0x000FB069
		[CLSCompliant(false)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x000FCE7A File Offset: 0x000FB07A
		[CLSCompliant(false)]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060044D4 RID: 17620
		[__DynamicallyInvokable]
		public abstract RuntimeFieldHandle FieldHandle { [__DynamicallyInvokable] get; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060044D5 RID: 17621
		[__DynamicallyInvokable]
		public abstract Type FieldType { [__DynamicallyInvokable] get; }

		// Token: 0x060044D6 RID: 17622
		[__DynamicallyInvokable]
		public abstract object GetValue(object obj);

		// Token: 0x060044D7 RID: 17623 RVA: 0x000FCE8B File Offset: 0x000FB08B
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_AbstractNonCLS"));
		}

		// Token: 0x060044D8 RID: 17624
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060044D9 RID: 17625
		[__DynamicallyInvokable]
		public abstract FieldAttributes Attributes { [__DynamicallyInvokable] get; }

		// Token: 0x060044DA RID: 17626 RVA: 0x000FCE9C File Offset: 0x000FB09C
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, null);
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x000FCEAD File Offset: 0x000FB0AD
		[__DynamicallyInvokable]
		public bool IsPublic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x000FCEBA File Offset: 0x000FB0BA
		[__DynamicallyInvokable]
		public bool IsPrivate
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000FCEC7 File Offset: 0x000FB0C7
		[__DynamicallyInvokable]
		public bool IsFamily
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000FCED4 File Offset: 0x000FB0D4
		[__DynamicallyInvokable]
		public bool IsAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000FCEE1 File Offset: 0x000FB0E1
		[__DynamicallyInvokable]
		public bool IsFamilyAndAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x000FCEEE File Offset: 0x000FB0EE
		[__DynamicallyInvokable]
		public bool IsFamilyOrAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000FCEFB File Offset: 0x000FB0FB
		[__DynamicallyInvokable]
		public bool IsStatic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x000FCF09 File Offset: 0x000FB109
		[__DynamicallyInvokable]
		public bool IsInitOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000FCF17 File Offset: 0x000FB117
		[__DynamicallyInvokable]
		public bool IsLiteral
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x000FCF25 File Offset: 0x000FB125
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x000FCF36 File Offset: 0x000FB136
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x000FCF47 File Offset: 0x000FB147
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x000FCF58 File Offset: 0x000FB158
		public virtual bool IsSecurityCritical
		{
			get
			{
				return this.FieldHandle.IsSecurityCritical();
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x000FCF74 File Offset: 0x000FB174
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				return this.FieldHandle.IsSecuritySafeCritical();
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x000FCF90 File Offset: 0x000FB190
		public virtual bool IsSecurityTransparent
		{
			get
			{
				return this.FieldHandle.IsSecurityTransparent();
			}
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x000FCFAB File Offset: 0x000FB1AB
		Type _FieldInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x000FCFB3 File Offset: 0x000FB1B3
		void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x000FCFBA File Offset: 0x000FB1BA
		void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x000FCFC1 File Offset: 0x000FB1C1
		void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
		void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
