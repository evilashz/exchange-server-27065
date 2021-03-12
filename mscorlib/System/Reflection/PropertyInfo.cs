using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005EE RID: 1518
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class PropertyInfo : MemberInfo, _PropertyInfo
	{
		// Token: 0x06004735 RID: 18229 RVA: 0x001023A2 File Offset: 0x001005A2
		[__DynamicallyInvokable]
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (left != null && right != null && !(left is RuntimePropertyInfo) && !(right is RuntimePropertyInfo) && left.Equals(right));
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x001023C9 File Offset: 0x001005C9
		[__DynamicallyInvokable]
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x001023D5 File Offset: 0x001005D5
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x001023DE File Offset: 0x001005DE
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x001023E6 File Offset: 0x001005E6
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x001023EA File Offset: 0x001005EA
		[__DynamicallyInvokable]
		public virtual object GetConstantValue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x001023F1 File Offset: 0x001005F1
		public virtual object GetRawConstantValue()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600473C RID: 18236
		[__DynamicallyInvokable]
		public abstract Type PropertyType { [__DynamicallyInvokable] get; }

		// Token: 0x0600473D RID: 18237
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x0600473E RID: 18238
		[__DynamicallyInvokable]
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x0600473F RID: 18239
		[__DynamicallyInvokable]
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x06004740 RID: 18240
		[__DynamicallyInvokable]
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06004741 RID: 18241
		[__DynamicallyInvokable]
		public abstract ParameterInfo[] GetIndexParameters();

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004742 RID: 18242
		[__DynamicallyInvokable]
		public abstract PropertyAttributes Attributes { [__DynamicallyInvokable] get; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004743 RID: 18243
		[__DynamicallyInvokable]
		public abstract bool CanRead { [__DynamicallyInvokable] get; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004744 RID: 18244
		[__DynamicallyInvokable]
		public abstract bool CanWrite { [__DynamicallyInvokable] get; }

		// Token: 0x06004745 RID: 18245 RVA: 0x001023F8 File Offset: 0x001005F8
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00102402 File Offset: 0x00100602
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004747 RID: 18247
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004748 RID: 18248 RVA: 0x0010240F File Offset: 0x0010060F
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x0010241A File Offset: 0x0010061A
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00102428 File Offset: 0x00100628
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x0010242F File Offset: 0x0010062F
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x00102436 File Offset: 0x00100636
		[__DynamicallyInvokable]
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x0010243F File Offset: 0x0010063F
		[__DynamicallyInvokable]
		public virtual MethodInfo GetMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetGetMethod(true);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600474E RID: 18254 RVA: 0x00102448 File Offset: 0x00100648
		[__DynamicallyInvokable]
		public virtual MethodInfo SetMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetSetMethod(true);
			}
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00102451 File Offset: 0x00100651
		[__DynamicallyInvokable]
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0010245A File Offset: 0x0010065A
		[__DynamicallyInvokable]
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004751 RID: 18257 RVA: 0x00102463 File Offset: 0x00100663
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00102474 File Offset: 0x00100674
		Type _PropertyInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x0010247C File Offset: 0x0010067C
		void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00102483 File Offset: 0x00100683
		void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0010248A File Offset: 0x0010068A
		void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00102491 File Offset: 0x00100691
		void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
