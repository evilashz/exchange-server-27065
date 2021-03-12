using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005E9 RID: 1513
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ParameterInfo : _ParameterInfo, ICustomAttributeProvider, IObjectReference
	{
		// Token: 0x060046F1 RID: 18161 RVA: 0x001016FD File Offset: 0x000FF8FD
		protected ParameterInfo()
		{
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x00101705 File Offset: 0x000FF905
		internal void SetName(string name)
		{
			this.NameImpl = name;
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x0010170E File Offset: 0x000FF90E
		internal void SetAttributes(ParameterAttributes attributes)
		{
			this.AttrsImpl = attributes;
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00101717 File Offset: 0x000FF917
		[__DynamicallyInvokable]
		public virtual Type ParameterType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ClassImpl;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x0010171F File Offset: 0x000FF91F
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.NameImpl;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060046F6 RID: 18166 RVA: 0x00101727 File Offset: 0x000FF927
		[__DynamicallyInvokable]
		public virtual bool HasDefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x0010172E File Offset: 0x000FF92E
		[__DynamicallyInvokable]
		public virtual object DefaultValue
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x00101735 File Offset: 0x000FF935
		public virtual object RawDefaultValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x0010173C File Offset: 0x000FF93C
		[__DynamicallyInvokable]
		public virtual int Position
		{
			[__DynamicallyInvokable]
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x00101744 File Offset: 0x000FF944
		[__DynamicallyInvokable]
		public virtual ParameterAttributes Attributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.AttrsImpl;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x0010174C File Offset: 0x000FF94C
		[__DynamicallyInvokable]
		public virtual MemberInfo Member
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberImpl;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x060046FC RID: 18172 RVA: 0x00101754 File Offset: 0x000FF954
		[__DynamicallyInvokable]
		public bool IsIn
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x00101761 File Offset: 0x000FF961
		[__DynamicallyInvokable]
		public bool IsOut
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060046FE RID: 18174 RVA: 0x0010176E File Offset: 0x000FF96E
		[__DynamicallyInvokable]
		public bool IsLcid
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x0010177B File Offset: 0x000FF97B
		[__DynamicallyInvokable]
		public bool IsRetval
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x00101788 File Offset: 0x000FF988
		[__DynamicallyInvokable]
		public bool IsOptional
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x00101798 File Offset: 0x000FF998
		public virtual int MetadataToken
		{
			get
			{
				RuntimeParameterInfo runtimeParameterInfo = this as RuntimeParameterInfo;
				if (runtimeParameterInfo != null)
				{
					return runtimeParameterInfo.MetadataToken;
				}
				return 134217728;
			}
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x001017BB File Offset: 0x000FF9BB
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x001017C2 File Offset: 0x000FF9C2
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x001017C9 File Offset: 0x000FF9C9
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x001017E6 File Offset: 0x000FF9E6
		[__DynamicallyInvokable]
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x001017EE File Offset: 0x000FF9EE
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return EmptyArray<object>.Value;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x001017F5 File Offset: 0x000FF9F5
		[__DynamicallyInvokable]
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return EmptyArray<object>.Value;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00101810 File Offset: 0x000FFA10
		[__DynamicallyInvokable]
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x00101827 File Offset: 0x000FFA27
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x0010182E File Offset: 0x000FFA2E
		void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00101835 File Offset: 0x000FFA35
		void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x0010183C File Offset: 0x000FFA3C
		void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00101843 File Offset: 0x000FFA43
		void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0010184C File Offset: 0x000FFA4C
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_NoParameterInfo"));
				}
				ParameterInfo[] array = ((RuntimePropertyInfo)this.MemberImpl).GetIndexParametersNoCopy();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_BadParameterInfo"));
			}
		}

		// Token: 0x04001D31 RID: 7473
		protected string NameImpl;

		// Token: 0x04001D32 RID: 7474
		protected Type ClassImpl;

		// Token: 0x04001D33 RID: 7475
		protected int PositionImpl;

		// Token: 0x04001D34 RID: 7476
		protected ParameterAttributes AttrsImpl;

		// Token: 0x04001D35 RID: 7477
		protected object DefaultValueImpl;

		// Token: 0x04001D36 RID: 7478
		protected MemberInfo MemberImpl;

		// Token: 0x04001D37 RID: 7479
		[OptionalField]
		private IntPtr _importer;

		// Token: 0x04001D38 RID: 7480
		[OptionalField]
		private int _token;

		// Token: 0x04001D39 RID: 7481
		[OptionalField]
		private bool bExtraConstChecked;
	}
}
