using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200081D RID: 2077
	internal class SyncSchema : ADPropertyUnionSchema
	{
		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x0600669E RID: 26270 RVA: 0x0016B39B File Offset: 0x0016959B
		public static SyncSchema Instance
		{
			get
			{
				if (SyncSchema.instance == null)
				{
					SyncSchema.instance = ObjectSchema.GetInstance<SyncSchema>();
				}
				return SyncSchema.instance;
			}
		}

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x0016B3B3 File Offset: 0x001695B3
		public override ReadOnlyCollection<ADObjectSchema> ObjectSchemas
		{
			get
			{
				return SyncSchema.AllSyncSchemas;
			}
		}

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x060066A0 RID: 26272 RVA: 0x0016B3BA File Offset: 0x001695BA
		public ReadOnlyCollection<SyncPropertyDefinition> AllLinkedProperties
		{
			get
			{
				return this.allLinkedProperties;
			}
		}

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x0016B3C2 File Offset: 0x001695C2
		public ReadOnlyCollection<SyncPropertyDefinition> AllBackSyncLinkedProperties
		{
			get
			{
				return this.allBackSyncLinkedProperties;
			}
		}

		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x060066A2 RID: 26274 RVA: 0x0016B3CA File Offset: 0x001695CA
		public ReadOnlyCollection<SyncPropertyDefinition> AllBackSyncShadowLinkedProperties
		{
			get
			{
				return this.allBackSyncShadowLinkedProperties;
			}
		}

		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x060066A3 RID: 26275 RVA: 0x0016B3D2 File Offset: 0x001695D2
		public ReadOnlyCollection<SyncPropertyDefinition> AllBackSyncShadowBaseProperties
		{
			get
			{
				return this.allBackSyncShadowBaseProperties;
			}
		}

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x060066A4 RID: 26276 RVA: 0x0016B3DA File Offset: 0x001695DA
		public ReadOnlyCollection<SyncPropertyDefinition> AllForwardSyncProperties
		{
			get
			{
				return this.allForwardSyncProperties;
			}
		}

		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x0016B3E2 File Offset: 0x001695E2
		public ReadOnlyCollection<SyncPropertyDefinition> AllForwardSyncLinkedProperties
		{
			get
			{
				return this.allForwardSyncLinkedProperties;
			}
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x0016B3EA File Offset: 0x001695EA
		public ReadOnlyCollection<SyncPropertyDefinition> AllBackSyncBaseProperties
		{
			get
			{
				return this.allBackSyncBaseProperties;
			}
		}

		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x0016B3F2 File Offset: 0x001695F2
		public ReadOnlyCollection<SyncPropertyDefinition> AllBackSyncProperties
		{
			get
			{
				return this.allBackSyncProperties;
			}
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x0016B3FA File Offset: 0x001695FA
		public ReadOnlyCollection<SyncPropertyDefinition> AllShadowProperties
		{
			get
			{
				return this.allShadowProperties;
			}
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x0016B402 File Offset: 0x00169602
		public SyncSchema()
		{
			SyncObjectSchema.InitializeSyncPropertyCollections(base.AllProperties, out this.allLinkedProperties, out this.allForwardSyncProperties, out this.allForwardSyncLinkedProperties, out this.allBackSyncProperties, out this.allShadowProperties);
			this.InitializeBackSyncPropertyCollections();
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x0016B43C File Offset: 0x0016963C
		private void InitializeBackSyncPropertyCollections()
		{
			List<SyncPropertyDefinition> list = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list2 = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list3 = new List<SyncPropertyDefinition>();
			List<SyncPropertyDefinition> list4 = new List<SyncPropertyDefinition>();
			foreach (SyncPropertyDefinition syncPropertyDefinition in this.allBackSyncProperties)
			{
				if (syncPropertyDefinition.IsSyncLink)
				{
					list.Add(syncPropertyDefinition);
				}
				else
				{
					list2.Add(syncPropertyDefinition);
				}
			}
			foreach (SyncPropertyDefinition syncPropertyDefinition2 in this.allShadowProperties)
			{
				if (syncPropertyDefinition2.IsSyncLink)
				{
					list3.Add(syncPropertyDefinition2);
				}
				else
				{
					list4.Add(syncPropertyDefinition2);
				}
			}
			this.allBackSyncLinkedProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list);
			this.allBackSyncBaseProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list2);
			this.allBackSyncShadowLinkedProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list3);
			this.allBackSyncShadowBaseProperties = new ReadOnlyCollection<SyncPropertyDefinition>(list4);
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x0016B548 File Offset: 0x00169748
		internal bool TryGetLinkedPropertyDefinitionByMsoPropertyName(string propertyName, out SyncPropertyDefinition propertyDefinition)
		{
			propertyDefinition = null;
			foreach (SyncPropertyDefinition syncPropertyDefinition in this.allLinkedProperties)
			{
				if (propertyName.Equals(syncPropertyDefinition.MsoPropertyName, StringComparison.OrdinalIgnoreCase))
				{
					propertyDefinition = syncPropertyDefinition;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040043BC RID: 17340
		private static readonly ReadOnlyCollection<ADObjectSchema> AllSyncSchemas = new ReadOnlyCollection<ADObjectSchema>(new ADObjectSchema[]
		{
			ObjectSchema.GetInstance<SyncGroupSchema>(),
			ObjectSchema.GetInstance<SyncCompanySchema>(),
			ObjectSchema.GetInstance<SyncContactSchema>(),
			ObjectSchema.GetInstance<SyncUserSchema>()
		});

		// Token: 0x040043BD RID: 17341
		private static SyncSchema instance;

		// Token: 0x040043BE RID: 17342
		private ReadOnlyCollection<SyncPropertyDefinition> allLinkedProperties;

		// Token: 0x040043BF RID: 17343
		private ReadOnlyCollection<SyncPropertyDefinition> allForwardSyncProperties;

		// Token: 0x040043C0 RID: 17344
		private ReadOnlyCollection<SyncPropertyDefinition> allForwardSyncLinkedProperties;

		// Token: 0x040043C1 RID: 17345
		private ReadOnlyCollection<SyncPropertyDefinition> allBackSyncProperties;

		// Token: 0x040043C2 RID: 17346
		private ReadOnlyCollection<SyncPropertyDefinition> allBackSyncLinkedProperties;

		// Token: 0x040043C3 RID: 17347
		private ReadOnlyCollection<SyncPropertyDefinition> allBackSyncShadowLinkedProperties;

		// Token: 0x040043C4 RID: 17348
		private ReadOnlyCollection<SyncPropertyDefinition> allBackSyncShadowBaseProperties;

		// Token: 0x040043C5 RID: 17349
		private ReadOnlyCollection<SyncPropertyDefinition> allBackSyncBaseProperties;

		// Token: 0x040043C6 RID: 17350
		private ReadOnlyCollection<SyncPropertyDefinition> allShadowProperties;
	}
}
