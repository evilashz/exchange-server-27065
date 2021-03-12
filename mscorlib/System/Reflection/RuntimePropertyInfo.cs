using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020005EF RID: 1519
	[Serializable]
	internal sealed class RuntimePropertyInfo : PropertyInfo, ISerializable
	{
		// Token: 0x06004757 RID: 18263 RVA: 0x00102498 File Offset: 0x00100698
		[SecurityCritical]
		internal RuntimePropertyInfo(int tkProperty, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkProperty;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			ConstArray constArray;
			metadataImport.GetPropertyProps(tkProperty, out this.m_utf8name, out this.m_flags, out constArray);
			RuntimeMethodInfo runtimeMethodInfo;
			Associates.AssignAssociates(metadataImport, tkProperty, declaredType, reflectedTypeCache.GetRuntimeType(), out runtimeMethodInfo, out runtimeMethodInfo, out runtimeMethodInfo, out this.m_getterMethod, out this.m_setterMethod, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x00102510 File Offset: 0x00100710
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimePropertyInfo runtimePropertyInfo = o as RuntimePropertyInfo;
			return runtimePropertyInfo != null && runtimePropertyInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimePropertyInfo.m_declaringType));
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x00102554 File Offset: 0x00100754
		internal unsafe Signature Signature
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_signature == null)
				{
					void* ptr;
					PropertyAttributes propertyAttributes;
					ConstArray constArray;
					this.GetRuntimeModule().MetadataImport.GetPropertyProps(this.m_token, out ptr, out propertyAttributes, out constArray);
					this.m_signature = new Signature(constArray.Signature.ToPointer(), constArray.Length, this.m_declaringType);
				}
				return this.m_signature;
			}
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x001025B6 File Offset: 0x001007B6
		internal bool EqualsSig(RuntimePropertyInfo target)
		{
			return Signature.CompareSig(this.Signature, target.Signature);
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600475B RID: 18267 RVA: 0x001025C9 File Offset: 0x001007C9
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x001025D1 File Offset: 0x001007D1
		public override string ToString()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x001025DC File Offset: 0x001007DC
		private string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			RuntimeType[] arguments = this.Signature.Arguments;
			if (arguments.Length != 0)
			{
				stringBuilder.Append(" [");
				stringBuilder.Append(MethodBase.ConstructParameters(arguments, this.Signature.CallingConvention, serialization));
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0010265B File Offset: 0x0010085B
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00102674 File Offset: 0x00100874
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x001026C8 File Offset: 0x001008C8
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0010271A File Offset: 0x0010091A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x00102722 File Offset: 0x00100922
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x00102728 File Offset: 0x00100928
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = new Utf8String(this.m_utf8name).ToString();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x00102762 File Offset: 0x00100962
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06004765 RID: 18277 RVA: 0x0010276A File Offset: 0x0010096A
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x00102772 File Offset: 0x00100972
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x0010277F File Offset: 0x0010097F
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x00102787 File Offset: 0x00100987
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0010278F File Offset: 0x0010098F
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0010279C File Offset: 0x0010099C
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.Signature.GetCustomModifiers(0, true);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x001027AB File Offset: 0x001009AB
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.Signature.GetCustomModifiers(0, false);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x001027BC File Offset: 0x001009BC
		[SecuritySafeCritical]
		internal object GetConstantValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_token, this.PropertyType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
			}
			return value;
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x00102805 File Offset: 0x00100A05
		public override object GetConstantValue()
		{
			return this.GetConstantValue(false);
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x0010280E File Offset: 0x00100A0E
		public override object GetRawConstantValue()
		{
			return this.GetConstantValue(true);
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x00102818 File Offset: 0x00100A18
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (Associates.IncludeAccessor(this.m_getterMethod, nonPublic))
			{
				list.Add(this.m_getterMethod);
			}
			if (Associates.IncludeAccessor(this.m_setterMethod, nonPublic))
			{
				list.Add(this.m_setterMethod);
			}
			if (this.m_otherMethod != null)
			{
				for (int i = 0; i < this.m_otherMethod.Length; i++)
				{
					if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
					{
						list.Add(this.m_otherMethod[i]);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x0010289E File Offset: 0x00100A9E
		public override Type PropertyType
		{
			get
			{
				return this.Signature.ReturnType;
			}
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x001028AB File Offset: 0x00100AAB
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_getterMethod, nonPublic))
			{
				return null;
			}
			return this.m_getterMethod;
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x001028C3 File Offset: 0x00100AC3
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_setterMethod, nonPublic))
			{
				return null;
			}
			return this.m_setterMethod;
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x001028DC File Offset: 0x00100ADC
		public override ParameterInfo[] GetIndexParameters()
		{
			ParameterInfo[] indexParametersNoCopy = this.GetIndexParametersNoCopy();
			int num = indexParametersNoCopy.Length;
			if (num == 0)
			{
				return indexParametersNoCopy;
			}
			ParameterInfo[] array = new ParameterInfo[num];
			Array.Copy(indexParametersNoCopy, array, num);
			return array;
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x0010290C File Offset: 0x00100B0C
		internal ParameterInfo[] GetIndexParametersNoCopy()
		{
			if (this.m_parameters == null)
			{
				int num = 0;
				ParameterInfo[] array = null;
				MethodInfo methodInfo = this.GetGetMethod(true);
				if (methodInfo != null)
				{
					array = methodInfo.GetParametersNoCopy();
					num = array.Length;
				}
				else
				{
					methodInfo = this.GetSetMethod(true);
					if (methodInfo != null)
					{
						array = methodInfo.GetParametersNoCopy();
						num = array.Length - 1;
					}
				}
				ParameterInfo[] array2 = new ParameterInfo[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = new RuntimeParameterInfo((RuntimeParameterInfo)array[i], this);
				}
				this.m_parameters = array2;
			}
			return this.m_parameters;
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004775 RID: 18293 RVA: 0x00102998 File Offset: 0x00100B98
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x001029A0 File Offset: 0x00100BA0
		public override bool CanRead
		{
			get
			{
				return this.m_getterMethod != null;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x001029AE File Offset: 0x00100BAE
		public override bool CanWrite
		{
			get
			{
				return this.m_setterMethod != null;
			}
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x001029BC File Offset: 0x00100BBC
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, index, null);
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x001029CC File Offset: 0x00100BCC
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_GetMethNotFnd"));
			}
			return getMethod.Invoke(obj, invokeAttr, binder, index, null);
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x00102A06 File Offset: 0x00100C06
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, index, null);
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00102A18 File Offset: 0x00100C18
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_SetMethNotFnd"));
			}
			object[] array;
			if (index != null)
			{
				array = new object[index.Length + 1];
				for (int i = 0; i < index.Length; i++)
				{
					array[i] = index[i];
				}
				array[index.Length] = value;
			}
			else
			{
				array = new object[]
				{
					value
				};
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00102A90 File Offset: 0x00100C90
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, null);
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x00102AC1 File Offset: 0x00100CC1
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x04001D50 RID: 7504
		private int m_token;

		// Token: 0x04001D51 RID: 7505
		private string m_name;

		// Token: 0x04001D52 RID: 7506
		[SecurityCritical]
		private unsafe void* m_utf8name;

		// Token: 0x04001D53 RID: 7507
		private PropertyAttributes m_flags;

		// Token: 0x04001D54 RID: 7508
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001D55 RID: 7509
		private RuntimeMethodInfo m_getterMethod;

		// Token: 0x04001D56 RID: 7510
		private RuntimeMethodInfo m_setterMethod;

		// Token: 0x04001D57 RID: 7511
		private MethodInfo[] m_otherMethod;

		// Token: 0x04001D58 RID: 7512
		private RuntimeType m_declaringType;

		// Token: 0x04001D59 RID: 7513
		private BindingFlags m_bindingFlags;

		// Token: 0x04001D5A RID: 7514
		private Signature m_signature;

		// Token: 0x04001D5B RID: 7515
		private ParameterInfo[] m_parameters;
	}
}
