using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	internal class EntityNativeBodyProperty : EntityContentProperty, IIntegerProperty, IProperty
	{
		// Token: 0x06001208 RID: 4616 RVA: 0x000620EB File Offset: 0x000602EB
		public EntityNativeBodyProperty() : base(PropertyType.ReadOnly)
		{
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000620F4 File Offset: 0x000602F4
		public int IntegerData
		{
			get
			{
				return (int)base.GetNativeType();
			}
		}
	}
}
