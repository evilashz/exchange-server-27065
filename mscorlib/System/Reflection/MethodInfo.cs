using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005DB RID: 1499
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MethodInfo : MethodBase, _MethodInfo
	{
		// Token: 0x06004613 RID: 17939 RVA: 0x000FF1DC File Offset: 0x000FD3DC
		[__DynamicallyInvokable]
		public static bool operator ==(MethodInfo left, MethodInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeMethodInfo) && !(right is RuntimeMethodInfo) && left.Equals(right));
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000FF203 File Offset: 0x000FD403
		[__DynamicallyInvokable]
		public static bool operator !=(MethodInfo left, MethodInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x000FF20F File Offset: 0x000FD40F
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x000FF218 File Offset: 0x000FD418
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x000FF220 File Offset: 0x000FD420
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x000FF223 File Offset: 0x000FD423
		[__DynamicallyInvokable]
		public virtual Type ReturnType
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x000FF22A File Offset: 0x000FD42A
		[__DynamicallyInvokable]
		public virtual ParameterInfo ReturnParameter
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600461A RID: 17946
		public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		// Token: 0x0600461B RID: 17947
		[__DynamicallyInvokable]
		public abstract MethodInfo GetBaseDefinition();

		// Token: 0x0600461C RID: 17948 RVA: 0x000FF231 File Offset: 0x000FD431
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public override Type[] GetGenericArguments()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000FF242 File Offset: 0x000FD442
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000FF253 File Offset: 0x000FD453
		[__DynamicallyInvokable]
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000FF264 File Offset: 0x000FD464
		[__DynamicallyInvokable]
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000FF275 File Offset: 0x000FD475
		[__DynamicallyInvokable]
		public virtual Delegate CreateDelegate(Type delegateType, object target)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000FF286 File Offset: 0x000FD486
		Type _MethodInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000FF28E File Offset: 0x000FD48E
		void _MethodInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000FF295 File Offset: 0x000FD495
		void _MethodInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x000FF29C File Offset: 0x000FD49C
		void _MethodInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000FF2A3 File Offset: 0x000FD4A3
		void _MethodInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
