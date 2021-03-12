using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E4E RID: 3662
	internal static class EnumTypes
	{
		// Token: 0x06005E0E RID: 24078 RVA: 0x0012565C File Offset: 0x0012385C
		public static void Register(EdmModel model)
		{
			ArgumentValidator.ThrowIfNull("model", model);
			EnumTypes.RegisterEntry(model, typeof(DayOfWeek), false);
			EnumTypes.RegisterEntry(model, typeof(BodyType), false);
			EnumTypes.RegisterEntry(model, typeof(Importance), false);
			EnumTypes.RegisterEntry(model, typeof(AttendeeType), false);
			EnumTypes.RegisterEntry(model, typeof(ResponseType), false);
			EnumTypes.RegisterEntry(model, typeof(EventType), false);
			EnumTypes.RegisterEntry(model, typeof(FreeBusyStatus), false);
			EnumTypes.RegisterEntry(model, typeof(MeetingMessageType), false);
			EnumTypes.RegisterEntry(model, typeof(RecurrencePatternType), false);
			EnumTypes.RegisterEntry(model, typeof(RecurrenceRangeType), false);
			EnumTypes.RegisterEntry(model, typeof(WeekIndex), false);
		}

		// Token: 0x06005E0F RID: 24079 RVA: 0x00125730 File Offset: 0x00123930
		public static EdmEnumType GetEdmEnumType(Type enumType)
		{
			ArgumentValidator.ThrowIfNull("enumType", enumType);
			EdmEnumType result;
			if (!EnumTypes.TypeMap.TryGetValue(enumType, out result))
			{
				throw new InvalidOperationException(string.Format("Unregistered enum type {0}. Add the entry in EnumTypes.Register()", enumType.FullName));
			}
			return result;
		}

		// Token: 0x06005E10 RID: 24080 RVA: 0x00125770 File Offset: 0x00123970
		private static void RegisterEntry(EdmModel model, Type enumType, bool isFlags)
		{
			EdmEnumType edmEnumType = EnumTypes.BuildEdmEnumType(enumType, isFlags);
			model.AddElement(edmEnumType);
			EnumTypes.TypeMap.Add(enumType, edmEnumType);
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x00125798 File Offset: 0x00123998
		private static EdmEnumType BuildEdmEnumType(Type enumType, bool isFlags)
		{
			ArgumentValidator.ThrowIfNull("enumType", enumType);
			Array values = Enum.GetValues(enumType);
			EdmEnumType edmEnumType = new EdmEnumType(enumType.Namespace, enumType.Name, isFlags);
			foreach (object obj in values)
			{
				string name = Enum.GetName(enumType, obj);
				edmEnumType.AddMember(name, new EdmIntegerConstant((long)((int)obj)));
			}
			return edmEnumType;
		}

		// Token: 0x040032F4 RID: 13044
		private static readonly Dictionary<Type, EdmEnumType> TypeMap = new Dictionary<Type, EdmEnumType>();
	}
}
