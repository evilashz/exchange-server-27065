using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005B6 RID: 1462
	[Serializable]
	internal sealed class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x060044B1 RID: 17585 RVA: 0x000FCA2A File Offset: 0x000FAC2A
		internal RuntimeEventInfo()
		{
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000FCA34 File Offset: 0x000FAC34
		[SecurityCritical]
		internal RuntimeEventInfo(int tkEvent, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkEvent;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			RuntimeType runtimeType = reflectedTypeCache.GetRuntimeType();
			metadataImport.GetEventProps(tkEvent, out this.m_utf8name, out this.m_flags);
			RuntimeMethodInfo runtimeMethodInfo;
			Associates.AssignAssociates(metadataImport, tkEvent, declaredType, runtimeType, out this.m_addMethod, out this.m_removeMethod, out this.m_raiseMethod, out runtimeMethodInfo, out runtimeMethodInfo, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x000FCAB0 File Offset: 0x000FACB0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeEventInfo runtimeEventInfo = o as RuntimeEventInfo;
			return runtimeEventInfo != null && runtimeEventInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimeEventInfo.m_declaringType));
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x000FCAF4 File Offset: 0x000FACF4
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000FCAFC File Offset: 0x000FACFC
		public override string ToString()
		{
			if (this.m_addMethod == null || this.m_addMethod.GetParametersNoCopy().Length == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoPublicAddMethod"));
			}
			return this.m_addMethod.GetParametersNoCopy()[0].ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x000FCB5C File Offset: 0x000FAD5C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x000FCB74 File Offset: 0x000FAD74
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

		// Token: 0x060044B8 RID: 17592 RVA: 0x000FCBC8 File Offset: 0x000FADC8
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

		// Token: 0x060044B9 RID: 17593 RVA: 0x000FCC1A File Offset: 0x000FAE1A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x000FCC22 File Offset: 0x000FAE22
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x000FCC28 File Offset: 0x000FAE28
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

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x000FCC62 File Offset: 0x000FAE62
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x000FCC6A File Offset: 0x000FAE6A
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x000FCC72 File Offset: 0x000FAE72
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x000FCC7F File Offset: 0x000FAE7F
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x000FCC87 File Offset: 0x000FAE87
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x000FCC8F File Offset: 0x000FAE8F
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x000FCC9C File Offset: 0x000FAE9C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000FCCC0 File Offset: 0x000FAEC0
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (this.m_otherMethod == null)
			{
				return new MethodInfo[0];
			}
			for (int i = 0; i < this.m_otherMethod.Length; i++)
			{
				if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
				{
					list.Add(this.m_otherMethod[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000FCD19 File Offset: 0x000FAF19
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_addMethod, nonPublic))
			{
				return null;
			}
			return this.m_addMethod;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000FCD31 File Offset: 0x000FAF31
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_removeMethod, nonPublic))
			{
				return null;
			}
			return this.m_removeMethod;
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x000FCD49 File Offset: 0x000FAF49
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_raiseMethod, nonPublic))
			{
				return null;
			}
			return this.m_raiseMethod;
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x000FCD61 File Offset: 0x000FAF61
		public override EventAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001BDE RID: 7134
		private int m_token;

		// Token: 0x04001BDF RID: 7135
		private EventAttributes m_flags;

		// Token: 0x04001BE0 RID: 7136
		private string m_name;

		// Token: 0x04001BE1 RID: 7137
		[SecurityCritical]
		private unsafe void* m_utf8name;

		// Token: 0x04001BE2 RID: 7138
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001BE3 RID: 7139
		private RuntimeMethodInfo m_addMethod;

		// Token: 0x04001BE4 RID: 7140
		private RuntimeMethodInfo m_removeMethod;

		// Token: 0x04001BE5 RID: 7141
		private RuntimeMethodInfo m_raiseMethod;

		// Token: 0x04001BE6 RID: 7142
		private MethodInfo[] m_otherMethod;

		// Token: 0x04001BE7 RID: 7143
		private RuntimeType m_declaringType;

		// Token: 0x04001BE8 RID: 7144
		private BindingFlags m_bindingFlags;
	}
}
