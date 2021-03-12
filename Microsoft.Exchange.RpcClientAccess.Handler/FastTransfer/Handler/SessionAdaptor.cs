using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000084 RID: 132
	internal sealed class SessionAdaptor : ISession
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00023568 File Offset: 0x00021768
		public SessionAdaptor(StoreSession session)
		{
			this.session = session;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00023578 File Offset: 0x00021778
		public bool TryResolveToNamedProperty(PropertyTag propertyTag, out NamedProperty namedProperty)
		{
			if (propertyTag.IsNamedProperty)
			{
				this.PropertyIdList[0] = (ushort)propertyTag.PropertyId;
				NamedPropertyDefinition.NamedPropertyKey[] namesFromIds = NamedPropConverter.GetNamesFromIds(this.session, this.PropertyIdList);
				namedProperty = namesFromIds[0].ToNamedProperty();
				return namedProperty != null;
			}
			namedProperty = null;
			return false;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000235CC File Offset: 0x000217CC
		public bool TryResolveFromNamedProperty(NamedProperty namedProperty, ref PropertyTag propertyTag)
		{
			NamedPropertyDefinition.NamedPropertyKey namedPropertyKey = namedProperty.ToNamedPropertyKey();
			if (namedPropertyKey != null)
			{
				this.NamedPropertyList[0] = namedPropertyKey;
				ushort[] idsFromNames = NamedPropConverter.GetIdsFromNames(this.session, true, this.NamedPropertyList);
				propertyTag = new PropertyTag((PropertyId)idsFromNames[0], propertyTag.PropertyType);
				return true;
			}
			return false;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0002361C File Offset: 0x0002181C
		private List<ushort> PropertyIdList
		{
			get
			{
				if (this.propertyIdList == null)
				{
					ushort[] collection = new ushort[1];
					this.propertyIdList = new List<ushort>(collection);
				}
				return this.propertyIdList;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0002364C File Offset: 0x0002184C
		private List<NamedPropertyDefinition.NamedPropertyKey> NamedPropertyList
		{
			get
			{
				if (this.namedPropertyList == null)
				{
					NamedPropertyDefinition.NamedPropertyKey[] collection = new NamedPropertyDefinition.NamedPropertyKey[1];
					this.namedPropertyList = new List<NamedPropertyDefinition.NamedPropertyKey>(collection);
				}
				return this.namedPropertyList;
			}
		}

		// Token: 0x0400021E RID: 542
		private readonly StoreSession session;

		// Token: 0x0400021F RID: 543
		private List<ushort> propertyIdList;

		// Token: 0x04000220 RID: 544
		private List<NamedPropertyDefinition.NamedPropertyKey> namedPropertyList;
	}
}
