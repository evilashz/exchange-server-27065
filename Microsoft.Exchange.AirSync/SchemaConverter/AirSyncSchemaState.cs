using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001AC RID: 428
	internal class AirSyncSchemaState : SchemaState, IAirSyncDataObjectGenerator, IDataObjectGenerator
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x00062DDA File Offset: 0x00060FDA
		public AirSyncSchemaState()
		{
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00062DE2 File Offset: 0x00060FE2
		protected AirSyncSchemaState(AirSyncSchemaState innerSchemaState)
		{
			this.innerSchemaState = innerSchemaState;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00062DF1 File Offset: 0x00060FF1
		public IDictionary Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00062DFC File Offset: 0x00060FFC
		public AirSyncDataObject GetAirSyncDataObject(IDictionary options, IAirSyncMissingPropertyStrategy missingPropertyStrategy)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (missingPropertyStrategy == null)
			{
				throw new ArgumentNullException("missingPropertyStrategy");
			}
			this.options = options;
			this.missingPropertyStrategy = missingPropertyStrategy;
			List<IProperty> schema = base.GetSchema(0);
			for (int i = 0; i < schema.Count; i++)
			{
				AirSyncProperty airSyncProperty = (AirSyncProperty)schema[i];
				airSyncProperty.Options = this.options;
			}
			return new AirSyncDataObject(schema, this.missingPropertyStrategy, this);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00062E74 File Offset: 0x00061074
		public AirSyncDataObject GetInnerAirSyncDataObject(IAirSyncMissingPropertyStrategy strategy)
		{
			if (this.innerSchemaState == null)
			{
				return null;
			}
			List<IProperty> schema = this.innerSchemaState.GetSchema(0);
			for (int i = 0; i < schema.Count; i++)
			{
				AirSyncProperty airSyncProperty = (AirSyncProperty)schema[i];
				airSyncProperty.Options = this.options;
			}
			return new AirSyncDataObject(schema, strategy, null);
		}

		// Token: 0x04000B4E RID: 2894
		private AirSyncSchemaState innerSchemaState;

		// Token: 0x04000B4F RID: 2895
		private IDictionary options;

		// Token: 0x04000B50 RID: 2896
		private IAirSyncMissingPropertyStrategy missingPropertyStrategy;

		// Token: 0x020001AD RID: 429
		public enum SchemaEnum
		{
			// Token: 0x04000B52 RID: 2898
			ClientSide,
			// Token: 0x04000B53 RID: 2899
			ServerSide,
			// Token: 0x04000B54 RID: 2900
			Count
		}
	}
}
