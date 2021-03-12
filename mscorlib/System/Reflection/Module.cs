using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005E0 RID: 1504
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class Module : _Module, ISerializable, ICustomAttributeProvider
	{
		// Token: 0x0600466B RID: 18027 RVA: 0x0010007C File Offset: 0x000FE27C
		static Module()
		{
			__Filters @object = new __Filters();
			Module.FilterTypeName = new TypeFilter(@object.FilterTypeName);
			Module.FilterTypeNameIgnoreCase = new TypeFilter(@object.FilterTypeNameIgnoreCase);
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x001000BB File Offset: 0x000FE2BB
		[__DynamicallyInvokable]
		public static bool operator ==(Module left, Module right)
		{
			return left == right || (left != null && right != null && !(left is RuntimeModule) && !(right is RuntimeModule) && left.Equals(right));
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x001000E2 File Offset: 0x000FE2E2
		[__DynamicallyInvokable]
		public static bool operator !=(Module left, Module right)
		{
			return !(left == right);
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x001000EE File Offset: 0x000FE2EE
		[__DynamicallyInvokable]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x001000F7 File Offset: 0x000FE2F7
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x001000FF File Offset: 0x000FE2FF
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ScopeName;
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004672 RID: 18034 RVA: 0x00100107 File Offset: 0x000FE307
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x0010010F File Offset: 0x000FE30F
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00100116 File Offset: 0x000FE316
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x0010011D File Offset: 0x000FE31D
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00100124 File Offset: 0x000FE324
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x0010012B File Offset: 0x000FE32B
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x00100138 File Offset: 0x000FE338
		public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x00100164 File Offset: 0x000FE364
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00100170 File Offset: 0x000FE370
		public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x0010019C File Offset: 0x000FE39C
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x001001A8 File Offset: 0x000FE3A8
		public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001001D4 File Offset: 0x000FE3D4
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001001E0 File Offset: 0x000FE3E0
		public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x0010020C File Offset: 0x000FE40C
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveSignature(metadataToken);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x00100238 File Offset: 0x000FE438
		public virtual string ResolveString(int metadataToken)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.ResolveString(metadataToken);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x00100264 File Offset: 0x000FE464
		public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				runtimeModule.GetPEKind(out peKind, out machine);
			}
			throw new NotImplementedException();
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x00100290 File Offset: 0x000FE490
		public virtual int MDStreamVersion
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.MDStreamVersion;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x001002B9 File Offset: 0x000FE4B9
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x001002C0 File Offset: 0x000FE4C0
		[ComVisible(true)]
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x001002CB File Offset: 0x000FE4CB
		[ComVisible(true)]
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x001002D6 File Offset: 0x000FE4D6
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x001002DD File Offset: 0x000FE4DD
		[__DynamicallyInvokable]
		public virtual string FullyQualifiedName
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x001002E4 File Offset: 0x000FE4E4
		public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			Type[] types = this.GetTypes();
			int num = 0;
			for (int i = 0; i < types.Length; i++)
			{
				if (filter != null && !filter(types[i], filterCriteria))
				{
					types[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == types.Length)
			{
				return types;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j] != null)
				{
					array[num++] = types[j];
				}
			}
			return array;
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x0010035C File Offset: 0x000FE55C
		public virtual Type[] GetTypes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x00100364 File Offset: 0x000FE564
		public virtual Guid ModuleVersionId
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.ModuleVersionId;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x00100390 File Offset: 0x000FE590
		public virtual int MetadataToken
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.MetadataToken;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001003BC File Offset: 0x000FE5BC
		public virtual bool IsResource()
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.IsResource();
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x001003E5 File Offset: 0x000FE5E5
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001003F0 File Offset: 0x000FE5F0
		public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.GetFields(bindingFlags);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x0010041A File Offset: 0x000FE61A
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x00100428 File Offset: 0x000FE628
		public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.GetField(name, bindingAttr);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00100453 File Offset: 0x000FE653
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00100460 File Offset: 0x000FE660
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			RuntimeModule runtimeModule = this as RuntimeModule;
			if (runtimeModule != null)
			{
				return runtimeModule.GetMethods(bindingFlags);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x0010048C File Offset: 0x000FE68C
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x001004EC File Offset: 0x000FE6EC
		public MethodInfo GetMethod(string name, Type[] types)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00100546 File Offset: 0x000FE746
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00100563 File Offset: 0x000FE763
		protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x0010056C File Offset: 0x000FE76C
		public virtual string ScopeName
		{
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.ScopeName;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x00100598 File Offset: 0x000FE798
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.Name;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x001005C4 File Offset: 0x000FE7C4
		[__DynamicallyInvokable]
		public virtual Assembly Assembly
		{
			[__DynamicallyInvokable]
			get
			{
				RuntimeModule runtimeModule = this as RuntimeModule;
				if (runtimeModule != null)
				{
					return runtimeModule.Assembly;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x001005ED File Offset: 0x000FE7ED
		public ModuleHandle ModuleHandle
		{
			get
			{
				return this.GetModuleHandle();
			}
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x001005F5 File Offset: 0x000FE7F5
		internal virtual ModuleHandle GetModuleHandle()
		{
			return ModuleHandle.EmptyHandle;
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001005FC File Offset: 0x000FE7FC
		public virtual X509Certificate GetSignerCertificate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x00100603 File Offset: 0x000FE803
		void _Module.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x0010060A File Offset: 0x000FE80A
		void _Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x00100611 File Offset: 0x000FE811
		void _Module.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00100618 File Offset: 0x000FE818
		void _Module.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001CFF RID: 7423
		public static readonly TypeFilter FilterTypeName;

		// Token: 0x04001D00 RID: 7424
		public static readonly TypeFilter FilterTypeNameIgnoreCase;

		// Token: 0x04001D01 RID: 7425
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
