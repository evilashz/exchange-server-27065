using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005A8 RID: 1448
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x06004429 RID: 17449 RVA: 0x000FA21F File Offset: 0x000F841F
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600442A RID: 17450 RVA: 0x000FA234 File Offset: 0x000F8434
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x000FA24C File Offset: 0x000F844C
		public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			Type argumentType;
			if (fieldInfo != null)
			{
				argumentType = fieldInfo.FieldType;
			}
			else
			{
				if (!(propertyInfo != null))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMemberForNamedArgument"));
				}
				argumentType = propertyInfo.PropertyType;
			}
			this.m_memberInfo = memberInfo;
			this.m_value = new CustomAttributeTypedArgument(argumentType, value);
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x000FA2C5 File Offset: 0x000F84C5
		public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			this.m_memberInfo = memberInfo;
			this.m_value = typedArgument;
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x000FA2EC File Offset: 0x000F84EC
		public override string ToString()
		{
			if (this.m_memberInfo == null)
			{
				return base.ToString();
			}
			return string.Format(CultureInfo.CurrentCulture, "{0} = {1}", this.MemberInfo.Name, this.TypedValue.ToString(this.ArgumentType != typeof(object)));
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x000FA355 File Offset: 0x000F8555
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x000FA367 File Offset: 0x000F8567
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x000FA377 File Offset: 0x000F8577
		internal Type ArgumentType
		{
			get
			{
				if (!(this.m_memberInfo is FieldInfo))
				{
					return ((PropertyInfo)this.m_memberInfo).PropertyType;
				}
				return ((FieldInfo)this.m_memberInfo).FieldType;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06004431 RID: 17457 RVA: 0x000FA3A7 File Offset: 0x000F85A7
		public MemberInfo MemberInfo
		{
			get
			{
				return this.m_memberInfo;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x000FA3AF File Offset: 0x000F85AF
		[__DynamicallyInvokable]
		public CustomAttributeTypedArgument TypedValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06004433 RID: 17459 RVA: 0x000FA3B7 File Offset: 0x000F85B7
		[__DynamicallyInvokable]
		public string MemberName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberInfo.Name;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x000FA3C4 File Offset: 0x000F85C4
		[__DynamicallyInvokable]
		public bool IsField
		{
			[__DynamicallyInvokable]
			get
			{
				return this.MemberInfo is FieldInfo;
			}
		}

		// Token: 0x04001BA5 RID: 7077
		private MemberInfo m_memberInfo;

		// Token: 0x04001BA6 RID: 7078
		private CustomAttributeTypedArgument m_value;
	}
}
