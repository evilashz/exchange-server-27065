using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000420 RID: 1056
	public static class WellKnownProperties
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x0008146F File Offset: 0x0007F66F
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00081476 File Offset: 0x0007F676
		public static ExtendedPropertyUri Hidden { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x10f4",
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x0008147E File Offset: 0x0007F67E
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00081485 File Offset: 0x0007F685
		public static ExtendedPropertyUri VoiceMessageAttachmentOrder { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x6805",
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x0008148D File Offset: 0x0007F68D
		// (set) Token: 0x060023E7 RID: 9191 RVA: 0x00081494 File Offset: 0x0007F694
		public static ExtendedPropertyUri PstnCallbackTelephoneNumber { get; private set; } = new ExtendedPropertyUri
		{
			PropertyName = "PstnCallbackTelephoneNumber",
			DistinguishedPropertySetId = DistinguishedPropertySet.UnifiedMessaging,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x0008149C File Offset: 0x0007F69C
		// (set) Token: 0x060023E9 RID: 9193 RVA: 0x000814A3 File Offset: 0x0007F6A3
		public static ExtendedPropertyUri VoiceMessageDuration { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x6801",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000814AB File Offset: 0x0007F6AB
		// (set) Token: 0x060023EB RID: 9195 RVA: 0x000814B2 File Offset: 0x0007F6B2
		public static ExtendedPropertyUri IsClassified { get; private set; } = new ExtendedPropertyUri
		{
			PropertyId = 34229,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000814BA File Offset: 0x0007F6BA
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x000814C1 File Offset: 0x0007F6C1
		public static ExtendedPropertyUri ClassificationGuid { get; private set; } = new ExtendedPropertyUri
		{
			PropertyId = 34232,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000814C9 File Offset: 0x0007F6C9
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000814D0 File Offset: 0x0007F6D0
		public static ExtendedPropertyUri Classification { get; private set; } = new ExtendedPropertyUri
		{
			PropertyId = 34230,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000814D8 File Offset: 0x0007F6D8
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x000814DF File Offset: 0x0007F6DF
		public static ExtendedPropertyUri ClassificationDescription { get; private set; } = new ExtendedPropertyUri
		{
			PropertyId = 34231,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000814E7 File Offset: 0x0007F6E7
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000814EE File Offset: 0x0007F6EE
		public static ExtendedPropertyUri ClassificationKeep { get; private set; } = new ExtendedPropertyUri
		{
			PropertyId = 34234,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000814F6 File Offset: 0x0007F6F6
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x000814FD File Offset: 0x0007F6FD
		public static ExtendedPropertyUri SharingInstanceGuid { get; private set; } = new ExtendedPropertyUri
		{
			PropertyId = 35356,
			DistinguishedPropertySetId = DistinguishedPropertySet.Sharing,
			PropertyType = MapiPropertyType.CLSID
		};

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00081505 File Offset: 0x0007F705
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x0008150C File Offset: 0x0007F70C
		public static ExtendedPropertyUri MessageBccMe { get; private set; } = new ExtendedPropertyUri
		{
			PropertyName = "MessageBccMe",
			PropertySetId = "41F28F13-83F4-4114-A584-EEDB5A6B0BFF",
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x00081514 File Offset: 0x0007F714
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x0008151B File Offset: 0x0007F71B
		public static ExtendedPropertyUri NormalizedSubject { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0xe1d",
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x00081523 File Offset: 0x0007F723
		// (set) Token: 0x060023FB RID: 9211 RVA: 0x0008152A File Offset: 0x0007F72A
		public static ExtendedPropertyUri RetentionFlags { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x301d",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060023FC RID: 9212 RVA: 0x00081532 File Offset: 0x0007F732
		// (set) Token: 0x060023FD RID: 9213 RVA: 0x00081539 File Offset: 0x0007F739
		public static ExtendedPropertyUri RetentionPeriod { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x301a",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00081541 File Offset: 0x0007F741
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x00081548 File Offset: 0x0007F748
		public static ExtendedPropertyUri ArchivePeriod { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x301e",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x00081550 File Offset: 0x0007F750
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x00081557 File Offset: 0x0007F757
		public static ExtendedPropertyUri NativeBodyInfo { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x1016",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x0008155F File Offset: 0x0007F75F
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x00081566 File Offset: 0x0007F766
		public static ExtendedPropertyUri InternetMessageHeaders { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x7d",
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0008156E File Offset: 0x0007F76E
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x00081575 File Offset: 0x0007F775
		public static ExtendedPropertyUri FlagStatus { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x1090",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0008157D File Offset: 0x0007F77D
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x00081584 File Offset: 0x0007F784
		public static ExtendedPropertyUri LastVerbExecuted { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x1081",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0008158C File Offset: 0x0007F78C
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x00081593 File Offset: 0x0007F793
		public static ExtendedPropertyUri LastVerbExecutionTime { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x1082",
			PropertyType = MapiPropertyType.SystemTime
		};

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x0008159B File Offset: 0x0007F79B
		// (set) Token: 0x0600240B RID: 9227 RVA: 0x000815A2 File Offset: 0x0007F7A2
		public static ExtendedPropertyUri DocumentId { get; private set; } = new ExtendedPropertyUri
		{
			PropertyTag = "0x6815",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x000815AA File Offset: 0x0007F7AA
		// (set) Token: 0x0600240D RID: 9229 RVA: 0x000815B1 File Offset: 0x0007F7B1
		public static ExtendedPropertyUri WorkingSetSourcePartitionInternal { get; private set; } = new ExtendedPropertyUri
		{
			PropertyName = "WorkingSetSourcePartitionInternal",
			PropertySetId = "95A4668D-CFBE-4D15-B4AE-3E61B9EF078B",
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x000815B9 File Offset: 0x0007F7B9
		public static PropertyUri Location
		{
			get
			{
				return WellKnownProperties.locationPropertyUri;
			}
		}

		// Token: 0x0400137E RID: 4990
		public const string MessagingNamespaceGuidString = "41F28F13-83F4-4114-A584-EEDB5A6B0BFF";

		// Token: 0x0400137F RID: 4991
		public const string WorkingSetNamespaceGuidString = "95A4668D-CFBE-4D15-B4AE-3E61B9EF078B";

		// Token: 0x04001380 RID: 4992
		private static readonly PropertyUri locationPropertyUri = new PropertyUri(PropertyUriEnum.EnhancedLocation);
	}
}
