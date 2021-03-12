using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C3 RID: 451
	internal sealed class PersonaShape : Shape
	{
		// Token: 0x06000C57 RID: 3159 RVA: 0x0004032C File Offset: 0x0003E52C
		static PersonaShape()
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			foreach (PropertyInformation propertyInformation in PersonaShape.defaultProperties)
			{
				list.AddRange(propertyInformation.GetPropertyDefinitions(null));
			}
			PersonaShape.DefaultPropertyDefinitions = list.ToArray();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0004045C File Offset: 0x0003E65C
		private PersonaShape(List<PropertyInformation> defaultProperties) : base(Schema.Persona, PersonaSchema.GetSchema(), null, defaultProperties)
		{
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00040470 File Offset: 0x0003E670
		internal static PersonaShape CreateShape()
		{
			return new PersonaShape(PersonaShape.defaultProperties);
		}

		// Token: 0x04000A19 RID: 2585
		private static readonly List<PropertyInformation> defaultProperties = new List<PropertyInformation>
		{
			PersonaSchema.PersonaId,
			PersonaSchema.ADObjectId,
			PersonaSchema.PersonaType,
			PersonaSchema.EmailAddress,
			PersonaSchema.CompanyName,
			PersonaSchema.DisplayName,
			PersonaSchema.DisplayNameFirstLast,
			PersonaSchema.DisplayNameLastFirst,
			PersonaSchema.GivenName,
			PersonaSchema.Surname,
			PersonaSchema.EmailAddresses,
			PersonaSchema.ImAddress,
			PersonaSchema.FileAs,
			PersonaSchema.HomeCity,
			PersonaSchema.WorkCity,
			PersonaSchema.CreationTime,
			PersonaSchema.IsFavorite
		};

		// Token: 0x04000A1A RID: 2586
		internal static readonly PropertyDefinition[] DefaultPropertyDefinitions;
	}
}
