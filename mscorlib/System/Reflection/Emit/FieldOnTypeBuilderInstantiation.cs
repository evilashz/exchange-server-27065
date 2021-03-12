using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200063D RID: 1597
	internal sealed class FieldOnTypeBuilderInstantiation : FieldInfo
	{
		// Token: 0x06004DD4 RID: 19924 RVA: 0x001174F8 File Offset: 0x001156F8
		internal static FieldInfo GetField(FieldInfo Field, TypeBuilderInstantiation type)
		{
			FieldInfo fieldInfo;
			if (type.m_hashtable.Contains(Field))
			{
				fieldInfo = (type.m_hashtable[Field] as FieldInfo);
			}
			else
			{
				fieldInfo = new FieldOnTypeBuilderInstantiation(Field, type);
				type.m_hashtable[Field] = fieldInfo;
			}
			return fieldInfo;
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0011753F File Offset: 0x0011573F
		internal FieldOnTypeBuilderInstantiation(FieldInfo field, TypeBuilderInstantiation type)
		{
			this.m_field = field;
			this.m_type = type;
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x00117555 File Offset: 0x00115755
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x0011755D File Offset: 0x0011575D
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x00117560 File Offset: 0x00115760
		public override string Name
		{
			get
			{
				return this.m_field.Name;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004DD9 RID: 19929 RVA: 0x0011756D File Offset: 0x0011576D
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004DDA RID: 19930 RVA: 0x00117575 File Offset: 0x00115775
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0011757D File Offset: 0x0011577D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0011758B File Offset: 0x0011578B
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0011759A File Offset: 0x0011579A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004DDE RID: 19934 RVA: 0x001175AC File Offset: 0x001157AC
		internal int MetadataTokenInternal
		{
			get
			{
				FieldBuilder fieldBuilder = this.m_field as FieldBuilder;
				if (fieldBuilder != null)
				{
					return fieldBuilder.MetadataTokenInternal;
				}
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004DDF RID: 19935 RVA: 0x001175E0 File Offset: 0x001157E0
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x001175ED File Offset: 0x001157ED
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x001175F5 File Offset: 0x001157F5
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_field.GetRequiredCustomModifiers();
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x00117602 File Offset: 0x00115802
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_field.GetOptionalCustomModifiers();
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0011760F File Offset: 0x0011580F
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00117616 File Offset: 0x00115816
		public override object GetValueDirect(TypedReference obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004DE5 RID: 19941 RVA: 0x0011761D File Offset: 0x0011581D
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004DE6 RID: 19942 RVA: 0x00117624 File Offset: 0x00115824
		public override Type FieldType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0011762B File Offset: 0x0011582B
		public override object GetValue(object obj)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x00117632 File Offset: 0x00115832
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004DE9 RID: 19945 RVA: 0x00117639 File Offset: 0x00115839
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x0400211E RID: 8478
		private FieldInfo m_field;

		// Token: 0x0400211F RID: 8479
		private TypeBuilderInstantiation m_type;
	}
}
