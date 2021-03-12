using System;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics.Internal
{
	// Token: 0x02000163 RID: 355
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class TaggedFieldAttribute : Attribute
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x000261C6 File Offset: 0x000243C6
		public TaggedFieldAttribute(int id)
		{
			this.id = id;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000261D5 File Offset: 0x000243D5
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000261E0 File Offset: 0x000243E0
		public static FieldInfo FindTaggedField(Type type, BindingFlags bindingFlags, int fieldId)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(TaggedFieldAttribute), false);
				if (customAttributes.Length > 0)
				{
					TaggedFieldAttribute taggedFieldAttribute = (TaggedFieldAttribute)customAttributes[0];
					if (taggedFieldAttribute.Id == fieldId)
					{
						return fieldInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x040006E8 RID: 1768
		private readonly int id;
	}
}
