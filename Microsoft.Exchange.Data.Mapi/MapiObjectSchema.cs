using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200001C RID: 28
	internal abstract class MapiObjectSchema : ObjectSchema
	{
		// Token: 0x04000092 RID: 146
		public static readonly MapiPropertyDefinition Identity = new MapiPropertyDefinition("Identity", typeof(MapiObjectId), PropTag.Null, MapiPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000093 RID: 147
		public static readonly MapiPropertyDefinition ObjectState = new MapiPropertyDefinition("ObjectState", typeof(ObjectState), PropTag.Null, MapiPropertyDefinitionFlags.None, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000094 RID: 148
		public static readonly MapiPropertyDefinition ExchangeVersion = new MapiPropertyDefinition("ExchangeVersion", typeof(ExchangeObjectVersion), PropTag.Null, MapiPropertyDefinitionFlags.None, ExchangeObjectVersion.Exchange2003, ExchangeObjectVersion.Exchange2003, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
