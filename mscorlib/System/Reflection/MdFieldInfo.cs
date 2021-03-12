using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005BB RID: 1467
	[Serializable]
	internal sealed class MdFieldInfo : RuntimeFieldInfo, ISerializable
	{
		// Token: 0x0600451C RID: 17692 RVA: 0x000FD8D4 File Offset: 0x000FBAD4
		internal MdFieldInfo(int tkField, FieldAttributes fieldAttributes, RuntimeTypeHandle declaringTypeHandle, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags) : base(reflectedTypeCache, declaringTypeHandle.GetRuntimeType(), bindingFlags)
		{
			this.m_tkField = tkField;
			this.m_name = null;
			this.m_fieldAttributes = fieldAttributes;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000FD8FC File Offset: 0x000FBAFC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			MdFieldInfo mdFieldInfo = o as MdFieldInfo;
			return mdFieldInfo != null && mdFieldInfo.m_tkField == this.m_tkField && this.m_declaringType.GetTypeHandleInternal().GetModuleHandle().Equals(mdFieldInfo.m_declaringType.GetTypeHandleInternal().GetModuleHandle());
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x000FD954 File Offset: 0x000FBB54
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.GetRuntimeModule().MetadataImport.GetName(this.m_tkField).ToString();
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x000FD99C File Offset: 0x000FBB9C
		public override int MetadataToken
		{
			get
			{
				return this.m_tkField;
			}
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x000FD9A4 File Offset: 0x000FBBA4
		internal override RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x000FD9B1 File Offset: 0x000FBBB1
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x000FD9B8 File Offset: 0x000FBBB8
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06004523 RID: 17699 RVA: 0x000FD9C0 File Offset: 0x000FBBC0
		public override bool IsSecurityCritical
		{
			get
			{
				return this.DeclaringType.IsSecurityCritical;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x000FD9CD File Offset: 0x000FBBCD
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.DeclaringType.IsSecuritySafeCritical;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06004525 RID: 17701 RVA: 0x000FD9DA File Offset: 0x000FBBDA
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.DeclaringType.IsSecurityTransparent;
			}
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x000FD9E7 File Offset: 0x000FBBE7
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValueDirect(TypedReference obj)
		{
			return this.GetValue(null);
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x000FD9F0 File Offset: 0x000FBBF0
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x000FDA01 File Offset: 0x000FBC01
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj)
		{
			return this.GetValue(false);
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x000FDA0A File Offset: 0x000FBC0A
		public override object GetRawConstantValue()
		{
			return this.GetValue(true);
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x000FDA14 File Offset: 0x000FBC14
		[SecuritySafeCritical]
		private object GetValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_tkField, this.FieldType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new NotSupportedException(Environment.GetResourceString("Arg_EnumLitValueNotFound"));
			}
			return value;
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x000FDA5D File Offset: 0x000FBC5D
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new FieldAccessException(Environment.GetResourceString("Acc_ReadOnly"));
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x000FDA70 File Offset: 0x000FBC70
		public override Type FieldType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_fieldType == null)
				{
					ConstArray sigOfFieldDef = this.GetRuntimeModule().MetadataImport.GetSigOfFieldDef(this.m_tkField);
					this.m_fieldType = new Signature(sigOfFieldDef.Signature.ToPointer(), sigOfFieldDef.Length, this.m_declaringType).FieldType;
				}
				return this.m_fieldType;
			}
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000FDAD7 File Offset: 0x000FBCD7
		public override Type[] GetRequiredCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x000FDADE File Offset: 0x000FBCDE
		public override Type[] GetOptionalCustomModifiers()
		{
			return EmptyArray<Type>.Value;
		}

		// Token: 0x04001C06 RID: 7174
		private int m_tkField;

		// Token: 0x04001C07 RID: 7175
		private string m_name;

		// Token: 0x04001C08 RID: 7176
		private RuntimeType m_fieldType;

		// Token: 0x04001C09 RID: 7177
		private FieldAttributes m_fieldAttributes;
	}
}
