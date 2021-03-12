using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E1 RID: 737
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ConversationActionType
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x000281B3 File Offset: 0x000263B3
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x000281BB File Offset: 0x000263BB
		public ConversationActionTypeType Action
		{
			get
			{
				return this.actionField;
			}
			set
			{
				this.actionField = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x000281C4 File Offset: 0x000263C4
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x000281CC File Offset: 0x000263CC
		public ItemIdType ConversationId
		{
			get
			{
				return this.conversationIdField;
			}
			set
			{
				this.conversationIdField = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x000281D5 File Offset: 0x000263D5
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x000281DD File Offset: 0x000263DD
		public TargetFolderIdType ContextFolderId
		{
			get
			{
				return this.contextFolderIdField;
			}
			set
			{
				this.contextFolderIdField = value;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000281E6 File Offset: 0x000263E6
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x000281EE File Offset: 0x000263EE
		public DateTime ConversationLastSyncTime
		{
			get
			{
				return this.conversationLastSyncTimeField;
			}
			set
			{
				this.conversationLastSyncTimeField = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x000281F7 File Offset: 0x000263F7
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x000281FF File Offset: 0x000263FF
		[XmlIgnore]
		public bool ConversationLastSyncTimeSpecified
		{
			get
			{
				return this.conversationLastSyncTimeFieldSpecified;
			}
			set
			{
				this.conversationLastSyncTimeFieldSpecified = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00028208 File Offset: 0x00026408
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x00028210 File Offset: 0x00026410
		public bool ProcessRightAway
		{
			get
			{
				return this.processRightAwayField;
			}
			set
			{
				this.processRightAwayField = value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00028219 File Offset: 0x00026419
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x00028221 File Offset: 0x00026421
		[XmlIgnore]
		public bool ProcessRightAwaySpecified
		{
			get
			{
				return this.processRightAwayFieldSpecified;
			}
			set
			{
				this.processRightAwayFieldSpecified = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0002822A File Offset: 0x0002642A
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x00028232 File Offset: 0x00026432
		public TargetFolderIdType DestinationFolderId
		{
			get
			{
				return this.destinationFolderIdField;
			}
			set
			{
				this.destinationFolderIdField = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0002823B File Offset: 0x0002643B
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x00028243 File Offset: 0x00026443
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories
		{
			get
			{
				return this.categoriesField;
			}
			set
			{
				this.categoriesField = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0002824C File Offset: 0x0002644C
		// (set) Token: 0x060018F5 RID: 6389 RVA: 0x00028254 File Offset: 0x00026454
		public bool EnableAlwaysDelete
		{
			get
			{
				return this.enableAlwaysDeleteField;
			}
			set
			{
				this.enableAlwaysDeleteField = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0002825D File Offset: 0x0002645D
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x00028265 File Offset: 0x00026465
		[XmlIgnore]
		public bool EnableAlwaysDeleteSpecified
		{
			get
			{
				return this.enableAlwaysDeleteFieldSpecified;
			}
			set
			{
				this.enableAlwaysDeleteFieldSpecified = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0002826E File Offset: 0x0002646E
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x00028276 File Offset: 0x00026476
		public bool IsRead
		{
			get
			{
				return this.isReadField;
			}
			set
			{
				this.isReadField = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0002827F File Offset: 0x0002647F
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x00028287 File Offset: 0x00026487
		[XmlIgnore]
		public bool IsReadSpecified
		{
			get
			{
				return this.isReadFieldSpecified;
			}
			set
			{
				this.isReadFieldSpecified = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00028290 File Offset: 0x00026490
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x00028298 File Offset: 0x00026498
		public DisposalType DeleteType
		{
			get
			{
				return this.deleteTypeField;
			}
			set
			{
				this.deleteTypeField = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x000282A1 File Offset: 0x000264A1
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x000282A9 File Offset: 0x000264A9
		[XmlIgnore]
		public bool DeleteTypeSpecified
		{
			get
			{
				return this.deleteTypeFieldSpecified;
			}
			set
			{
				this.deleteTypeFieldSpecified = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x000282B2 File Offset: 0x000264B2
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x000282BA File Offset: 0x000264BA
		public RetentionType RetentionPolicyType
		{
			get
			{
				return this.retentionPolicyTypeField;
			}
			set
			{
				this.retentionPolicyTypeField = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x000282C3 File Offset: 0x000264C3
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x000282CB File Offset: 0x000264CB
		[XmlIgnore]
		public bool RetentionPolicyTypeSpecified
		{
			get
			{
				return this.retentionPolicyTypeFieldSpecified;
			}
			set
			{
				this.retentionPolicyTypeFieldSpecified = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x000282D4 File Offset: 0x000264D4
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x000282DC File Offset: 0x000264DC
		public string RetentionPolicyTagId
		{
			get
			{
				return this.retentionPolicyTagIdField;
			}
			set
			{
				this.retentionPolicyTagIdField = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x000282E5 File Offset: 0x000264E5
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x000282ED File Offset: 0x000264ED
		public FlagType Flag
		{
			get
			{
				return this.flagField;
			}
			set
			{
				this.flagField = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x000282F6 File Offset: 0x000264F6
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x000282FE File Offset: 0x000264FE
		public bool SuppressReadReceipts
		{
			get
			{
				return this.suppressReadReceiptsField;
			}
			set
			{
				this.suppressReadReceiptsField = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x00028307 File Offset: 0x00026507
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0002830F File Offset: 0x0002650F
		[XmlIgnore]
		public bool SuppressReadReceiptsSpecified
		{
			get
			{
				return this.suppressReadReceiptsFieldSpecified;
			}
			set
			{
				this.suppressReadReceiptsFieldSpecified = value;
			}
		}

		// Token: 0x040010D6 RID: 4310
		private ConversationActionTypeType actionField;

		// Token: 0x040010D7 RID: 4311
		private ItemIdType conversationIdField;

		// Token: 0x040010D8 RID: 4312
		private TargetFolderIdType contextFolderIdField;

		// Token: 0x040010D9 RID: 4313
		private DateTime conversationLastSyncTimeField;

		// Token: 0x040010DA RID: 4314
		private bool conversationLastSyncTimeFieldSpecified;

		// Token: 0x040010DB RID: 4315
		private bool processRightAwayField;

		// Token: 0x040010DC RID: 4316
		private bool processRightAwayFieldSpecified;

		// Token: 0x040010DD RID: 4317
		private TargetFolderIdType destinationFolderIdField;

		// Token: 0x040010DE RID: 4318
		private string[] categoriesField;

		// Token: 0x040010DF RID: 4319
		private bool enableAlwaysDeleteField;

		// Token: 0x040010E0 RID: 4320
		private bool enableAlwaysDeleteFieldSpecified;

		// Token: 0x040010E1 RID: 4321
		private bool isReadField;

		// Token: 0x040010E2 RID: 4322
		private bool isReadFieldSpecified;

		// Token: 0x040010E3 RID: 4323
		private DisposalType deleteTypeField;

		// Token: 0x040010E4 RID: 4324
		private bool deleteTypeFieldSpecified;

		// Token: 0x040010E5 RID: 4325
		private RetentionType retentionPolicyTypeField;

		// Token: 0x040010E6 RID: 4326
		private bool retentionPolicyTypeFieldSpecified;

		// Token: 0x040010E7 RID: 4327
		private string retentionPolicyTagIdField;

		// Token: 0x040010E8 RID: 4328
		private FlagType flagField;

		// Token: 0x040010E9 RID: 4329
		private bool suppressReadReceiptsField;

		// Token: 0x040010EA RID: 4330
		private bool suppressReadReceiptsFieldSpecified;
	}
}
