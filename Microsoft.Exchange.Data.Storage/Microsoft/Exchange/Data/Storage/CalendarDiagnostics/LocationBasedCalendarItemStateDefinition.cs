using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x0200036D RID: 877
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocationBasedCalendarItemStateDefinition : SinglePropertyValueBasedCalendarItemStateDefinition<string>
	{
		// Token: 0x060026C4 RID: 9924 RVA: 0x0009B3F2 File Offset: 0x000995F2
		public LocationBasedCalendarItemStateDefinition(string targetLocation) : base(CalendarItemBaseSchema.Location, targetLocation)
		{
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x0009B400 File Offset: 0x00099600
		public override string SchemaKey
		{
			get
			{
				return "{0F4C3DE4-541B-4159-B0A9-99869D67C819}";
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x0009B407 File Offset: 0x00099607
		public override StorePropertyDefinition[] RequiredProperties
		{
			get
			{
				return LocationBasedCalendarItemStateDefinition.requiredProperties;
			}
		}

		// Token: 0x04001710 RID: 5904
		private static readonly StorePropertyDefinition[] requiredProperties = new StorePropertyDefinition[]
		{
			CalendarItemBaseSchema.ClientIntent,
			CalendarItemBaseSchema.Location
		};
	}
}
