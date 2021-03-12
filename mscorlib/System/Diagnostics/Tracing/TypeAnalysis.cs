using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045B RID: 1115
	internal sealed class TypeAnalysis
	{
		// Token: 0x06003648 RID: 13896 RVA: 0x000D083C File Offset: 0x000CEA3C
		public TypeAnalysis(Type dataType, EventDataAttribute eventAttrib, List<Type> recursionCheck)
		{
			IEnumerable<PropertyInfo> enumerable = Statics.GetProperties(dataType);
			List<PropertyAnalysis> list = new List<PropertyAnalysis>();
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				if (!Statics.HasCustomAttribute(propertyInfo, typeof(EventIgnoreAttribute)) && propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0)
				{
					MethodInfo getMethod = Statics.GetGetMethod(propertyInfo);
					if (!(getMethod == null) && !getMethod.IsStatic && getMethod.IsPublic)
					{
						Type propertyType = propertyInfo.PropertyType;
						TraceLoggingTypeInfo typeInfoInstance = Statics.GetTypeInfoInstance(propertyType, recursionCheck);
						EventFieldAttribute customAttribute = Statics.GetCustomAttribute<EventFieldAttribute>(propertyInfo);
						string text = (customAttribute != null && customAttribute.Name != null) ? customAttribute.Name : (Statics.ShouldOverrideFieldName(propertyInfo.Name) ? typeInfoInstance.Name : propertyInfo.Name);
						list.Add(new PropertyAnalysis(text, getMethod, typeInfoInstance, customAttribute));
					}
				}
			}
			this.properties = list.ToArray();
			foreach (PropertyAnalysis propertyAnalysis in this.properties)
			{
				TraceLoggingTypeInfo typeInfo = propertyAnalysis.typeInfo;
				this.level = (EventLevel)Statics.Combine((int)typeInfo.Level, (int)this.level);
				this.opcode = (EventOpcode)Statics.Combine((int)typeInfo.Opcode, (int)this.opcode);
				this.keywords |= typeInfo.Keywords;
				this.tags |= typeInfo.Tags;
			}
			if (eventAttrib != null)
			{
				this.level = (EventLevel)Statics.Combine((int)eventAttrib.Level, (int)this.level);
				this.opcode = (EventOpcode)Statics.Combine((int)eventAttrib.Opcode, (int)this.opcode);
				this.keywords |= eventAttrib.Keywords;
				this.tags |= eventAttrib.Tags;
				this.name = eventAttrib.Name;
			}
			if (this.name == null)
			{
				this.name = dataType.Name;
			}
		}

		// Token: 0x040017F1 RID: 6129
		internal readonly PropertyAnalysis[] properties;

		// Token: 0x040017F2 RID: 6130
		internal readonly string name;

		// Token: 0x040017F3 RID: 6131
		internal readonly EventKeywords keywords;

		// Token: 0x040017F4 RID: 6132
		internal readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x040017F5 RID: 6133
		internal readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x040017F6 RID: 6134
		internal readonly EventTags tags;
	}
}
