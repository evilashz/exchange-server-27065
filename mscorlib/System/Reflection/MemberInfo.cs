using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005D4 RID: 1492
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MemberInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MemberInfo : ICustomAttributeProvider, _MemberInfo
	{
		// Token: 0x060045B8 RID: 17848 RVA: 0x000FE58D File Offset: 0x000FC78D
		internal virtual bool CacheEquals(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060045B9 RID: 17849
		public abstract MemberTypes MemberType { get; }

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060045BA RID: 17850
		[__DynamicallyInvokable]
		public abstract string Name { [__DynamicallyInvokable] get; }

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060045BB RID: 17851
		[__DynamicallyInvokable]
		public abstract Type DeclaringType { [__DynamicallyInvokable] get; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060045BC RID: 17852
		[__DynamicallyInvokable]
		public abstract Type ReflectedType { [__DynamicallyInvokable] get; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060045BD RID: 17853 RVA: 0x000FE594 File Offset: 0x000FC794
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x060045BE RID: 17854
		[__DynamicallyInvokable]
		public abstract object[] GetCustomAttributes(bool inherit);

		// Token: 0x060045BF RID: 17855
		[__DynamicallyInvokable]
		public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x060045C0 RID: 17856
		[__DynamicallyInvokable]
		public abstract bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x060045C1 RID: 17857 RVA: 0x000FE59C File Offset: 0x000FC79C
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000FE5A3 File Offset: 0x000FC7A3
		public virtual int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x000FE5AA File Offset: 0x000FC7AA
		[__DynamicallyInvokable]
		public virtual Module Module
		{
			[__DynamicallyInvokable]
			get
			{
				if (this is Type)
				{
					return ((Type)this).Module;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x000FE5C8 File Offset: 0x000FC7C8
		[__DynamicallyInvokable]
		public static bool operator ==(MemberInfo left, MemberInfo right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			Type left2;
			Type right2;
			if ((left2 = (left as Type)) != null && (right2 = (right as Type)) != null)
			{
				return left2 == right2;
			}
			MethodBase left3;
			MethodBase right3;
			if ((left3 = (left as MethodBase)) != null && (right3 = (right as MethodBase)) != null)
			{
				return left3 == right3;
			}
			FieldInfo left4;
			FieldInfo right4;
			if ((left4 = (left as FieldInfo)) != null && (right4 = (right as FieldInfo)) != null)
			{
				return left4 == right4;
			}
			EventInfo left5;
			EventInfo right5;
			if ((left5 = (left as EventInfo)) != null && (right5 = (right as EventInfo)) != null)
			{
				return left5 == right5;
			}
			PropertyInfo left6;
			PropertyInfo right6;
			return (left6 = (left as PropertyInfo)) != null && (right6 = (right as PropertyInfo)) != null && left6 == right6;
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x000FE6B8 File Offset: 0x000FC8B8
		[__DynamicallyInvokable]
		public static bool operator !=(MemberInfo left, MemberInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x000FE6C4 File Offset: 0x000FC8C4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x000FE6CD File Offset: 0x000FC8CD
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x000FE6D5 File Offset: 0x000FC8D5
		Type _MemberInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x000FE6DD File Offset: 0x000FC8DD
		void _MemberInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x000FE6E4 File Offset: 0x000FC8E4
		void _MemberInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x000FE6EB File Offset: 0x000FC8EB
		void _MemberInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x000FE6F2 File Offset: 0x000FC8F2
		void _MemberInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
