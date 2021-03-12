using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F4 RID: 500
	internal class PersonaTypeConverter : EnumConverter
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00042C8C File Offset: 0x00040E8C
		public static PersonType Parse(string propertyString)
		{
			PersonType result;
			if (!EnumValidator.TryParse<PersonType>(propertyString, EnumParseOptions.Default, out result))
			{
				result = PersonType.Unknown;
			}
			return result;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00042CA8 File Offset: 0x00040EA8
		public static string ToString(PersonType propertyValue)
		{
			string result = null;
			switch (propertyValue)
			{
			case PersonType.Unknown:
				result = PersonaTypeConverter.UnknownString;
				break;
			case PersonType.Person:
				result = PersonaTypeConverter.PersonString;
				break;
			case PersonType.DistributionList:
				result = PersonaTypeConverter.DistributionListString;
				break;
			case PersonType.Room:
				result = PersonaTypeConverter.RoomString;
				break;
			case PersonType.Place:
				result = PersonaTypeConverter.PlaceString;
				break;
			case PersonType.ModernGroup:
				if (CallContext.Current != null && (CallContext.Current.WorkloadType == WorkloadType.Owa || CallContext.Current.WorkloadType == WorkloadType.OwaVoice))
				{
					result = PersonaTypeConverter.ModernGroupString;
				}
				else
				{
					result = PersonaTypeConverter.PersonString;
				}
				break;
			}
			return result;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00042D33 File Offset: 0x00040F33
		public override object ConvertToObject(string propertyString)
		{
			return PersonaTypeConverter.Parse(propertyString);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00042D40 File Offset: 0x00040F40
		public override string ConvertToString(object propertyValue)
		{
			return PersonaTypeConverter.ToString((PersonType)propertyValue);
		}

		// Token: 0x04000A7E RID: 2686
		private static readonly string PersonString = PersonType.Person.ToString();

		// Token: 0x04000A7F RID: 2687
		private static readonly string DistributionListString = PersonType.DistributionList.ToString();

		// Token: 0x04000A80 RID: 2688
		private static readonly string RoomString = PersonType.Room.ToString();

		// Token: 0x04000A81 RID: 2689
		private static readonly string PlaceString = PersonType.Place.ToString();

		// Token: 0x04000A82 RID: 2690
		private static readonly string ModernGroupString = PersonType.ModernGroup.ToString();

		// Token: 0x04000A83 RID: 2691
		private static readonly string UnknownString = PersonType.Unknown.ToString();
	}
}
