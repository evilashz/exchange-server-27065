using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x0200036E RID: 878
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ResponseTypeBasedCalendarItemStateDefinition : SinglePropertyValueBasedCalendarItemStateDefinition<int>
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x0009B43A File Offset: 0x0009963A
		public ResponseTypeBasedCalendarItemStateDefinition(ResponseType response) : base(CalendarItemBaseSchema.ResponseType, (int)response)
		{
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x0009B448 File Offset: 0x00099648
		public override string SchemaKey
		{
			get
			{
				return "{0144B45E-68EF-4a2f-8970-AA1A597EB048}";
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0009B44F File Offset: 0x0009964F
		public override StorePropertyDefinition[] RequiredProperties
		{
			get
			{
				return ResponseTypeBasedCalendarItemStateDefinition.requiredProperties;
			}
		}

		// Token: 0x04001711 RID: 5905
		private static readonly StorePropertyDefinition[] requiredProperties = new StorePropertyDefinition[]
		{
			CalendarItemBaseSchema.ClientIntent,
			CalendarItemBaseSchema.ResponseType
		};
	}
}
