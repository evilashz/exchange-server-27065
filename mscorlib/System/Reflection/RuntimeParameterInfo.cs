using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005EA RID: 1514
	[Serializable]
	internal sealed class RuntimeParameterInfo : ParameterInfo, ISerializable
	{
		// Token: 0x0600470F RID: 18191 RVA: 0x00101958 File Offset: 0x000FFB58
		[SecurityCritical]
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo parameterInfo;
			return RuntimeParameterInfo.GetParameters(method, member, sig, out parameterInfo, false);
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x00101970 File Offset: 0x000FFB70
		[SecurityCritical]
		internal static ParameterInfo GetReturnParameter(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo result;
			RuntimeParameterInfo.GetParameters(method, member, sig, out result, true);
			return result;
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x0010198C File Offset: 0x000FFB8C
		[SecurityCritical]
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo methodHandle, MemberInfo member, Signature sig, out ParameterInfo returnParameter, bool fetchReturnParameter)
		{
			returnParameter = null;
			int num = sig.Arguments.Length;
			ParameterInfo[] array = fetchReturnParameter ? null : new ParameterInfo[num];
			int methodDef = RuntimeMethodHandle.GetMethodDef(methodHandle);
			int num2 = 0;
			if (!System.Reflection.MetadataToken.IsNullToken(methodDef))
			{
				MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(RuntimeMethodHandle.GetDeclaringType(methodHandle));
				MetadataEnumResult metadataEnumResult;
				metadataImport.EnumParams(methodDef, out metadataEnumResult);
				num2 = metadataEnumResult.Length;
				if (num2 > num + 1)
				{
					throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
				}
				for (int i = 0; i < num2; i++)
				{
					int num3 = metadataEnumResult[i];
					int num4;
					ParameterAttributes attributes;
					metadataImport.GetParamDefProps(num3, out num4, out attributes);
					num4--;
					if (fetchReturnParameter && num4 == -1)
					{
						if (returnParameter != null)
						{
							throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
						}
						returnParameter = new RuntimeParameterInfo(sig, metadataImport, num3, num4, attributes, member);
					}
					else if (!fetchReturnParameter && num4 >= 0)
					{
						if (num4 >= num)
						{
							throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ParameterSignatureMismatch"));
						}
						array[num4] = new RuntimeParameterInfo(sig, metadataImport, num3, num4, attributes, member);
					}
				}
			}
			if (fetchReturnParameter)
			{
				if (returnParameter == null)
				{
					returnParameter = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, -1, ParameterAttributes.None, member);
				}
			}
			else if (num2 < array.Length + 1)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] == null)
					{
						array[j] = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, j, ParameterAttributes.None, member);
					}
				}
			}
			return array;
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x00101AE4 File Offset: 0x000FFCE4
		internal MethodBase DefiningMethod
		{
			get
			{
				return (this.m_originalMember != null) ? this.m_originalMember : (this.MemberImpl as MethodBase);
			}
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x00101B14 File Offset: 0x000FFD14
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(typeof(ParameterInfo));
			info.AddValue("AttrsImpl", this.Attributes);
			info.AddValue("ClassImpl", this.ParameterType);
			info.AddValue("DefaultValueImpl", this.DefaultValue);
			info.AddValue("MemberImpl", this.Member);
			info.AddValue("NameImpl", this.Name);
			info.AddValue("PositionImpl", this.Position);
			info.AddValue("_token", this.m_tkParamDef);
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x00101BBB File Offset: 0x000FFDBB
		internal RuntimeParameterInfo(RuntimeParameterInfo accessor, RuntimePropertyInfo property) : this(accessor, property)
		{
			this.m_signature = property.Signature;
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00101BD4 File Offset: 0x000FFDD4
		private RuntimeParameterInfo(RuntimeParameterInfo accessor, MemberInfo member)
		{
			this.MemberImpl = member;
			this.m_originalMember = (accessor.MemberImpl as MethodBase);
			this.NameImpl = accessor.Name;
			this.m_nameIsCached = true;
			this.ClassImpl = accessor.ParameterType;
			this.PositionImpl = accessor.Position;
			this.AttrsImpl = accessor.Attributes;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(accessor.MetadataToken) ? 134217728 : accessor.MetadataToken);
			this.m_scope = accessor.m_scope;
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x00101C64 File Offset: 0x000FFE64
		private RuntimeParameterInfo(Signature signature, MetadataImport scope, int tkParamDef, int position, ParameterAttributes attributes, MemberInfo member)
		{
			this.PositionImpl = position;
			this.MemberImpl = member;
			this.m_signature = signature;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(tkParamDef) ? 134217728 : tkParamDef);
			this.m_scope = scope;
			this.AttrsImpl = attributes;
			this.ClassImpl = null;
			this.NameImpl = null;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00101CC4 File Offset: 0x000FFEC4
		internal RuntimeParameterInfo(MethodInfo owner, string name, Type parameterType, int position)
		{
			this.MemberImpl = owner;
			this.NameImpl = name;
			this.m_nameIsCached = true;
			this.m_noMetadata = true;
			this.ClassImpl = parameterType;
			this.PositionImpl = position;
			this.AttrsImpl = ParameterAttributes.None;
			this.m_tkParamDef = 134217728;
			this.m_scope = MetadataImport.EmptyImport;
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x00101D24 File Offset: 0x000FFF24
		public override Type ParameterType
		{
			get
			{
				if (this.ClassImpl == null)
				{
					RuntimeType classImpl;
					if (this.PositionImpl == -1)
					{
						classImpl = this.m_signature.ReturnType;
					}
					else
					{
						classImpl = this.m_signature.Arguments[this.PositionImpl];
					}
					this.ClassImpl = classImpl;
				}
				return this.ClassImpl;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x00101D78 File Offset: 0x000FFF78
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.m_nameIsCached)
				{
					if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
					{
						string nameImpl = this.m_scope.GetName(this.m_tkParamDef).ToString();
						this.NameImpl = nameImpl;
					}
					this.m_nameIsCached = true;
				}
				return this.NameImpl;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600471A RID: 18202 RVA: 0x00101DD4 File Offset: 0x000FFFD4
		public override bool HasDefaultValue
		{
			get
			{
				if (this.m_noMetadata || this.m_noDefaultValue)
				{
					return false;
				}
				object defaultValueInternal = this.GetDefaultValueInternal(false);
				return defaultValueInternal != DBNull.Value;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x00101E06 File Offset: 0x00100006
		public override object DefaultValue
		{
			get
			{
				return this.GetDefaultValue(false);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600471C RID: 18204 RVA: 0x00101E0F File Offset: 0x0010000F
		public override object RawDefaultValue
		{
			get
			{
				return this.GetDefaultValue(true);
			}
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00101E18 File Offset: 0x00100018
		private object GetDefaultValue(bool raw)
		{
			if (this.m_noMetadata)
			{
				return null;
			}
			object obj = this.GetDefaultValueInternal(raw);
			if (obj == DBNull.Value && base.IsOptional)
			{
				obj = Type.Missing;
			}
			return obj;
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00101E50 File Offset: 0x00100050
		[SecuritySafeCritical]
		private object GetDefaultValueInternal(bool raw)
		{
			if (this.m_noDefaultValue)
			{
				return DBNull.Value;
			}
			object obj = null;
			if (this.ParameterType == typeof(DateTime))
			{
				if (raw)
				{
					CustomAttributeTypedArgument customAttributeTypedArgument = CustomAttributeData.Filter(CustomAttributeData.GetCustomAttributes(this), typeof(DateTimeConstantAttribute), 0);
					if (customAttributeTypedArgument.ArgumentType != null)
					{
						return new DateTime((long)customAttributeTypedArgument.Value);
					}
				}
				else
				{
					object[] customAttributes = this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						return ((DateTimeConstantAttribute)customAttributes[0]).Value;
					}
				}
			}
			if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				obj = MdConstant.GetValue(this.m_scope, this.m_tkParamDef, this.ParameterType.GetTypeHandleInternal(), raw);
			}
			if (obj == DBNull.Value)
			{
				if (raw)
				{
					using (IEnumerator<CustomAttributeData> enumerator = CustomAttributeData.GetCustomAttributes(this).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CustomAttributeData customAttributeData = enumerator.Current;
							Type declaringType = customAttributeData.Constructor.DeclaringType;
							if (declaringType == typeof(DateTimeConstantAttribute))
							{
								obj = DateTimeConstantAttribute.GetRawDateTimeConstant(customAttributeData);
							}
							else if (declaringType == typeof(DecimalConstantAttribute))
							{
								obj = DecimalConstantAttribute.GetRawDecimalConstant(customAttributeData);
							}
							else if (declaringType.IsSubclassOf(RuntimeParameterInfo.s_CustomConstantAttributeType))
							{
								obj = CustomConstantAttribute.GetRawConstant(customAttributeData);
							}
						}
						goto IL_1A7;
					}
				}
				object[] customAttributes2 = this.GetCustomAttributes(RuntimeParameterInfo.s_CustomConstantAttributeType, false);
				if (customAttributes2.Length != 0)
				{
					obj = ((CustomConstantAttribute)customAttributes2[0]).Value;
				}
				else
				{
					customAttributes2 = this.GetCustomAttributes(RuntimeParameterInfo.s_DecimalConstantAttributeType, false);
					if (customAttributes2.Length != 0)
					{
						obj = ((DecimalConstantAttribute)customAttributes2[0]).Value;
					}
				}
			}
			IL_1A7:
			if (obj == DBNull.Value)
			{
				this.m_noDefaultValue = true;
			}
			return obj;
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x00102024 File Offset: 0x00100224
		internal RuntimeModule GetRuntimeModule()
		{
			RuntimeMethodInfo runtimeMethodInfo = this.Member as RuntimeMethodInfo;
			RuntimeConstructorInfo runtimeConstructorInfo = this.Member as RuntimeConstructorInfo;
			RuntimePropertyInfo runtimePropertyInfo = this.Member as RuntimePropertyInfo;
			if (runtimeMethodInfo != null)
			{
				return runtimeMethodInfo.GetRuntimeModule();
			}
			if (runtimeConstructorInfo != null)
			{
				return runtimeConstructorInfo.GetRuntimeModule();
			}
			if (runtimePropertyInfo != null)
			{
				return runtimePropertyInfo.GetRuntimeModule();
			}
			return null;
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x00102086 File Offset: 0x00100286
		public override int MetadataToken
		{
			get
			{
				return this.m_tkParamDef;
			}
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x0010208E File Offset: 0x0010028E
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, true);
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x001020A4 File Offset: 0x001002A4
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, false);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x001020BA File Offset: 0x001002BA
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return EmptyArray<object>.Value;
			}
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x001020E4 File Offset: 0x001002E4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return EmptyArray<object>.Value;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x0010214C File Offset: 0x0010034C
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return false;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x001021AD File Offset: 0x001003AD
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x001021B8 File Offset: 0x001003B8
		internal RemotingParameterCachedData RemotingCache
		{
			get
			{
				RemotingParameterCachedData remotingParameterCachedData = this.m_cachedData;
				if (remotingParameterCachedData == null)
				{
					remotingParameterCachedData = new RemotingParameterCachedData(this);
					RemotingParameterCachedData remotingParameterCachedData2 = Interlocked.CompareExchange<RemotingParameterCachedData>(ref this.m_cachedData, remotingParameterCachedData, null);
					if (remotingParameterCachedData2 != null)
					{
						remotingParameterCachedData = remotingParameterCachedData2;
					}
				}
				return remotingParameterCachedData;
			}
		}

		// Token: 0x04001D3A RID: 7482
		private static readonly Type s_DecimalConstantAttributeType = typeof(DecimalConstantAttribute);

		// Token: 0x04001D3B RID: 7483
		private static readonly Type s_CustomConstantAttributeType = typeof(CustomConstantAttribute);

		// Token: 0x04001D3C RID: 7484
		[NonSerialized]
		private int m_tkParamDef;

		// Token: 0x04001D3D RID: 7485
		[NonSerialized]
		private MetadataImport m_scope;

		// Token: 0x04001D3E RID: 7486
		[NonSerialized]
		private Signature m_signature;

		// Token: 0x04001D3F RID: 7487
		[NonSerialized]
		private volatile bool m_nameIsCached;

		// Token: 0x04001D40 RID: 7488
		[NonSerialized]
		private readonly bool m_noMetadata;

		// Token: 0x04001D41 RID: 7489
		[NonSerialized]
		private bool m_noDefaultValue;

		// Token: 0x04001D42 RID: 7490
		[NonSerialized]
		private MethodBase m_originalMember;

		// Token: 0x04001D43 RID: 7491
		private RemotingParameterCachedData m_cachedData;
	}
}
