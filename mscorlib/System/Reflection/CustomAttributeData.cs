using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005A7 RID: 1447
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CustomAttributeData
	{
		// Token: 0x06004408 RID: 17416 RVA: 0x000F92AE File Offset: 0x000F74AE
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000F92CA File Offset: 0x000F74CA
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000F92E6 File Offset: 0x000F74E6
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000F9302 File Offset: 0x000F7502
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000F9318 File Offset: 0x000F7518
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, true, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000F9390 File Offset: 0x000F7590
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeFieldInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000F9408 File Offset: 0x000F7608
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeMethodInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, true, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000F9480 File Offset: 0x000F7680
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeConstructorInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x000F9493 File Offset: 0x000F7693
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeEventInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000F94A6 File Offset: 0x000F76A6
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimePropertyInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000F94B9 File Offset: 0x000F76B9
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeModule target)
		{
			if (target.IsResource())
			{
				return new List<CustomAttributeData>();
			}
			return CustomAttributeData.GetCustomAttributes(target, target.MetadataToken);
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000F94D8 File Offset: 0x000F76D8
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeAssembly target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes((RuntimeModule)target.ManifestModule, RuntimeAssembly.GetToken(target.GetNativeHandle()));
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, false, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000F955C File Offset: 0x000F775C
		[SecuritySafeCritical]
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeParameterInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			int num = 0;
			Attribute[] customAttributes2 = PseudoCustomAttribute.GetCustomAttributes(target, typeof(object) as RuntimeType, out num);
			if (num == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + num];
			customAttributes.CopyTo(array, num);
			for (int i = 0; i < num; i++)
			{
				array[i] = new CustomAttributeData(customAttributes2[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x000F95D4 File Offset: 0x000F77D4
		private static CustomAttributeEncoding TypeToCustomAttributeEncoding(RuntimeType type)
		{
			if (type == (RuntimeType)typeof(int))
			{
				return CustomAttributeEncoding.Int32;
			}
			if (type.IsEnum)
			{
				return CustomAttributeEncoding.Enum;
			}
			if (type == (RuntimeType)typeof(string))
			{
				return CustomAttributeEncoding.String;
			}
			if (type == (RuntimeType)typeof(Type))
			{
				return CustomAttributeEncoding.Type;
			}
			if (type == (RuntimeType)typeof(object))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsArray)
			{
				return CustomAttributeEncoding.Array;
			}
			if (type == (RuntimeType)typeof(char))
			{
				return CustomAttributeEncoding.Char;
			}
			if (type == (RuntimeType)typeof(bool))
			{
				return CustomAttributeEncoding.Boolean;
			}
			if (type == (RuntimeType)typeof(byte))
			{
				return CustomAttributeEncoding.Byte;
			}
			if (type == (RuntimeType)typeof(sbyte))
			{
				return CustomAttributeEncoding.SByte;
			}
			if (type == (RuntimeType)typeof(short))
			{
				return CustomAttributeEncoding.Int16;
			}
			if (type == (RuntimeType)typeof(ushort))
			{
				return CustomAttributeEncoding.UInt16;
			}
			if (type == (RuntimeType)typeof(uint))
			{
				return CustomAttributeEncoding.UInt32;
			}
			if (type == (RuntimeType)typeof(long))
			{
				return CustomAttributeEncoding.Int64;
			}
			if (type == (RuntimeType)typeof(ulong))
			{
				return CustomAttributeEncoding.UInt64;
			}
			if (type == (RuntimeType)typeof(float))
			{
				return CustomAttributeEncoding.Float;
			}
			if (type == (RuntimeType)typeof(double))
			{
				return CustomAttributeEncoding.Double;
			}
			if (type == (RuntimeType)typeof(Enum))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsClass)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsInterface)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsValueType)
			{
				return CustomAttributeEncoding.Undefined;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKindOfTypeForCA"), "type");
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x000F97C4 File Offset: 0x000F79C4
		private static CustomAttributeType InitCustomAttributeType(RuntimeType parameterType)
		{
			CustomAttributeEncoding customAttributeEncoding = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			CustomAttributeEncoding customAttributeEncoding2 = CustomAttributeEncoding.Undefined;
			CustomAttributeEncoding encodedEnumType = CustomAttributeEncoding.Undefined;
			string enumName = null;
			if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				parameterType = (RuntimeType)parameterType.GetElementType();
				customAttributeEncoding2 = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum || customAttributeEncoding2 == CustomAttributeEncoding.Enum)
			{
				encodedEnumType = CustomAttributeData.TypeToCustomAttributeEncoding((RuntimeType)Enum.GetUnderlyingType(parameterType));
				enumName = parameterType.AssemblyQualifiedName;
			}
			return new CustomAttributeType(customAttributeEncoding, customAttributeEncoding2, encodedEnumType, enumName);
		}

		// Token: 0x06004417 RID: 17431 RVA: 0x000F9824 File Offset: 0x000F7A24
		[SecurityCritical]
		private static IList<CustomAttributeData> GetCustomAttributes(RuntimeModule module, int tkTarget)
		{
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(module, tkTarget);
			CustomAttributeData[] array = new CustomAttributeData[customAttributeRecords.Length];
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				array[i] = new CustomAttributeData(module, customAttributeRecords[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004418 RID: 17432 RVA: 0x000F9868 File Offset: 0x000F7A68
		[SecurityCritical]
		internal static CustomAttributeRecord[] GetCustomAttributeRecords(RuntimeModule module, int targetToken)
		{
			MetadataImport metadataImport = module.MetadataImport;
			MetadataEnumResult metadataEnumResult;
			metadataImport.EnumCustomAttributes(targetToken, out metadataEnumResult);
			CustomAttributeRecord[] array = new CustomAttributeRecord[metadataEnumResult.Length];
			for (int i = 0; i < array.Length; i++)
			{
				metadataImport.GetCustomAttributeProps(metadataEnumResult[i], out array[i].tkCtor.Value, out array[i].blob);
			}
			return array;
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x000F98D0 File Offset: 0x000F7AD0
		internal static CustomAttributeTypedArgument Filter(IList<CustomAttributeData> attrs, Type caType, int parameter)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].Constructor.DeclaringType == caType)
				{
					return attrs[i].ConstructorArguments[parameter];
				}
			}
			return default(CustomAttributeTypedArgument);
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000F9923 File Offset: 0x000F7B23
		protected CustomAttributeData()
		{
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000F992C File Offset: 0x000F7B2C
		[SecuritySafeCritical]
		private CustomAttributeData(RuntimeModule scope, CustomAttributeRecord caRecord)
		{
			this.m_scope = scope;
			this.m_ctor = (RuntimeConstructorInfo)RuntimeType.GetMethodBase(scope, caRecord.tkCtor);
			ParameterInfo[] parametersNoCopy = this.m_ctor.GetParametersNoCopy();
			this.m_ctorParams = new CustomAttributeCtorParameter[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				this.m_ctorParams[i] = new CustomAttributeCtorParameter(CustomAttributeData.InitCustomAttributeType((RuntimeType)parametersNoCopy[i].ParameterType));
			}
			FieldInfo[] fields = this.m_ctor.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = this.m_ctor.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.m_namedParams = new CustomAttributeNamedParameter[properties.Length + fields.Length];
			for (int j = 0; j < fields.Length; j++)
			{
				this.m_namedParams[j] = new CustomAttributeNamedParameter(fields[j].Name, CustomAttributeEncoding.Field, CustomAttributeData.InitCustomAttributeType((RuntimeType)fields[j].FieldType));
			}
			for (int k = 0; k < properties.Length; k++)
			{
				this.m_namedParams[k + fields.Length] = new CustomAttributeNamedParameter(properties[k].Name, CustomAttributeEncoding.Property, CustomAttributeData.InitCustomAttributeType((RuntimeType)properties[k].PropertyType));
			}
			this.m_members = new MemberInfo[fields.Length + properties.Length];
			fields.CopyTo(this.m_members, 0);
			properties.CopyTo(this.m_members, fields.Length);
			CustomAttributeEncodedArgument.ParseAttributeArguments(caRecord.blob, ref this.m_ctorParams, ref this.m_namedParams, this.m_scope);
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x000F9AB8 File Offset: 0x000F7CB8
		internal CustomAttributeData(Attribute attribute)
		{
			if (attribute is DllImportAttribute)
			{
				this.Init((DllImportAttribute)attribute);
				return;
			}
			if (attribute is FieldOffsetAttribute)
			{
				this.Init((FieldOffsetAttribute)attribute);
				return;
			}
			if (attribute is MarshalAsAttribute)
			{
				this.Init((MarshalAsAttribute)attribute);
				return;
			}
			if (attribute is TypeForwardedToAttribute)
			{
				this.Init((TypeForwardedToAttribute)attribute);
				return;
			}
			this.Init(attribute);
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x000F9B28 File Offset: 0x000F7D28
		private void Init(DllImportAttribute dllImport)
		{
			Type typeFromHandle = typeof(DllImportAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(dllImport.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), dllImport.EntryPoint),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), dllImport.CharSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), dllImport.ExactSpelling),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), dllImport.SetLastError),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), dllImport.PreserveSig),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), dllImport.CallingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), dllImport.BestFitMapping),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), dllImport.ThrowOnUnmappableChar)
			});
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x000F9C90 File Offset: 0x000F7E90
		private void Init(FieldOffsetAttribute fieldOffset)
		{
			this.m_ctor = typeof(FieldOffsetAttribute).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(fieldOffset.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x000F9CF0 File Offset: 0x000F7EF0
		private void Init(MarshalAsAttribute marshalAs)
		{
			Type typeFromHandle = typeof(MarshalAsAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(marshalAs.Value)
			});
			int num = 3;
			if (marshalAs.MarshalType != null)
			{
				num++;
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				num++;
			}
			if (marshalAs.MarshalCookie != null)
			{
				num++;
			}
			num++;
			num++;
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				num++;
			}
			CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
			num = 0;
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("ArraySubType"), marshalAs.ArraySubType);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeParamIndex"), marshalAs.SizeParamIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeConst"), marshalAs.SizeConst);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("IidParameterIndex"), marshalAs.IidParameterIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArraySubType"), marshalAs.SafeArraySubType);
			if (marshalAs.MarshalType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalType"), marshalAs.MarshalType);
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalTypeRef"), marshalAs.MarshalTypeRef);
			}
			if (marshalAs.MarshalCookie != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalCookie"), marshalAs.MarshalCookie);
			}
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArrayUserDefinedSubType"), marshalAs.SafeArrayUserDefinedSubType);
			}
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x000F9F0C File Offset: 0x000F810C
		private void Init(TypeForwardedToAttribute forwardedTo)
		{
			Type typeFromHandle = typeof(TypeForwardedToAttribute);
			Type[] types = new Type[]
			{
				typeof(Type)
			};
			this.m_ctor = typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(Type), forwardedTo.Destination)
			});
			CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[0];
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x000F9F8B File Offset: 0x000F818B
		private void Init(object pca)
		{
			this.m_ctor = pca.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[0]);
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[0]);
		}

		// Token: 0x06004422 RID: 17442 RVA: 0x000F9FC4 File Offset: 0x000F81C4
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.ConstructorArguments.Count; i++)
			{
				text += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", this.ConstructorArguments[i]);
			}
			string text2 = "";
			for (int j = 0; j < this.NamedArguments.Count; j++)
			{
				text2 += string.Format(CultureInfo.CurrentCulture, (j == 0 && text.Length == 0) ? "{0}" : ", {0}", this.NamedArguments[j]);
			}
			return string.Format(CultureInfo.CurrentCulture, "[{0}({1}{2})]", this.Constructor.DeclaringType.FullName, text, text2);
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x000FA094 File Offset: 0x000F8294
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x000FA09C File Offset: 0x000F829C
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x000FA0A2 File Offset: 0x000F82A2
		[__DynamicallyInvokable]
		public Type AttributeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x000FA0AF File Offset: 0x000F82AF
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.m_ctor;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x000FA0B8 File Offset: 0x000F82B8
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_typedCtorArgs == null)
				{
					CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[this.m_ctorParams.Length];
					for (int i = 0; i < array.Length; i++)
					{
						CustomAttributeEncodedArgument customAttributeEncodedArgument = this.m_ctorParams[i].CustomAttributeEncodedArgument;
						array[i] = new CustomAttributeTypedArgument(this.m_scope, this.m_ctorParams[i].CustomAttributeEncodedArgument);
					}
					this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				}
				return this.m_typedCtorArgs;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x000FA130 File Offset: 0x000F8330
		[__DynamicallyInvokable]
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_namedArgs == null)
				{
					if (this.m_namedParams == null)
					{
						return null;
					}
					int num = 0;
					for (int i = 0; i < this.m_namedParams.Length; i++)
					{
						if (this.m_namedParams[i].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							num++;
						}
					}
					CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
					int j = 0;
					int num2 = 0;
					while (j < this.m_namedParams.Length)
					{
						if (this.m_namedParams[j].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							array[num2++] = new CustomAttributeNamedArgument(this.m_members[j], new CustomAttributeTypedArgument(this.m_scope, this.m_namedParams[j].EncodedArgument));
						}
						j++;
					}
					this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
				}
				return this.m_namedArgs;
			}
		}

		// Token: 0x04001B9E RID: 7070
		private ConstructorInfo m_ctor;

		// Token: 0x04001B9F RID: 7071
		private RuntimeModule m_scope;

		// Token: 0x04001BA0 RID: 7072
		private MemberInfo[] m_members;

		// Token: 0x04001BA1 RID: 7073
		private CustomAttributeCtorParameter[] m_ctorParams;

		// Token: 0x04001BA2 RID: 7074
		private CustomAttributeNamedParameter[] m_namedParams;

		// Token: 0x04001BA3 RID: 7075
		private IList<CustomAttributeTypedArgument> m_typedCtorArgs;

		// Token: 0x04001BA4 RID: 7076
		private IList<CustomAttributeNamedArgument> m_namedArgs;
	}
}
