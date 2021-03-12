using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001B2 RID: 434
	internal class AirSyncXsoSchemaState : AirSyncSchemaState, IXsoDataObjectGenerator, IDataObjectGenerator, IClassFilter
	{
		// Token: 0x0600123D RID: 4669 RVA: 0x00062F14 File Offset: 0x00061114
		public AirSyncXsoSchemaState(QueryFilter supportedClassFilter)
		{
			this.supportedClassFilter = supportedClassFilter;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00062F23 File Offset: 0x00061123
		protected AirSyncXsoSchemaState(QueryFilter supportedClassFilter, AirSyncXsoSchemaState innerSchemaState) : base(innerSchemaState)
		{
			this.innerSchemaState = innerSchemaState;
			this.supportedClassFilter = supportedClassFilter;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00062F3A File Offset: 0x0006113A
		public QueryFilter SupportedClassFilter
		{
			get
			{
				return this.supportedClassFilter;
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00062F44 File Offset: 0x00061144
		public static QueryFilter BuildMessageClassFilter(IList<string> supportedIpmTypes)
		{
			QueryFilter result;
			switch (supportedIpmTypes.Count)
			{
			case 0:
				result = AirSyncXsoSchemaState.FalseFilterInstance;
				break;
			case 1:
				result = new TextFilter(StoreObjectSchema.ItemClass, supportedIpmTypes[0], MatchOptions.PrefixOnWords, MatchFlags.IgnoreCase);
				break;
			default:
			{
				QueryFilter[] array = new QueryFilter[supportedIpmTypes.Count];
				for (int i = 0; i < supportedIpmTypes.Count; i++)
				{
					array[i] = new TextFilter(StoreObjectSchema.ItemClass, supportedIpmTypes[i], MatchOptions.PrefixOnWords, MatchFlags.IgnoreCase);
				}
				result = new OrFilter(array);
				break;
			}
			}
			return result;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00062FC1 File Offset: 0x000611C1
		public XsoDataObject GetXsoDataObject()
		{
			return new XsoDataObject(base.GetSchema(1), this, this.SupportedClassFilter);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00062FD6 File Offset: 0x000611D6
		public XsoDataObject GetInnerXsoDataObject()
		{
			if (this.innerSchemaState == null)
			{
				return null;
			}
			return new XsoDataObject(this.innerSchemaState.GetSchema(1), null, null);
		}

		// Token: 0x04000B57 RID: 2903
		public static readonly QueryFilter FalseFilterInstance = new FalseFilter();

		// Token: 0x04000B58 RID: 2904
		private readonly QueryFilter supportedClassFilter;

		// Token: 0x04000B59 RID: 2905
		private AirSyncXsoSchemaState innerSchemaState;
	}
}
