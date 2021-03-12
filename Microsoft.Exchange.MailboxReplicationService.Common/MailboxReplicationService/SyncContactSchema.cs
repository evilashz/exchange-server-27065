using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A2 RID: 418
	internal static class SyncContactSchema
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00024945 File Offset: 0x00022B45
		public static PropertyTag WorkAddressCity
		{
			get
			{
				return SyncContactSchema.workAddressCity;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002494C File Offset: 0x00022B4C
		public static PropertyTag LegacyWebPage
		{
			get
			{
				return SyncContactSchema.legacyWebPage;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00024953 File Offset: 0x00022B53
		public static PropertyTag WorkAddressCountry
		{
			get
			{
				return SyncContactSchema.workAddressCountry;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002495A File Offset: 0x00022B5A
		public static PropertyTag Email1EmailAddress
		{
			get
			{
				return SyncContactSchema.email1EmailAddress;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00024961 File Offset: 0x00022B61
		public static PropertyTag Email2EmailAddress
		{
			get
			{
				return SyncContactSchema.email2EmailAddress;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00024968 File Offset: 0x00022B68
		public static PropertyTag Email3EmailAddress
		{
			get
			{
				return SyncContactSchema.email3EmailAddress;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0002496F File Offset: 0x00022B6F
		public static PropertyTag FileAsStringInternal
		{
			get
			{
				return SyncContactSchema.fileAsStringInternal;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00024976 File Offset: 0x00022B76
		public static PropertyTag WorkAddressPostalCode
		{
			get
			{
				return SyncContactSchema.workAddressPostalCode;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0002497D File Offset: 0x00022B7D
		public static PropertyTag WorkAddressState
		{
			get
			{
				return SyncContactSchema.workAddressState;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00024984 File Offset: 0x00022B84
		public static PropertyTag WorkAddressStreet
		{
			get
			{
				return SyncContactSchema.workAddressStreet;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0002498B File Offset: 0x00022B8B
		public static PropertyTag YomiFirstName
		{
			get
			{
				return SyncContactSchema.yomiFirstName;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00024992 File Offset: 0x00022B92
		public static PropertyTag YomiLastName
		{
			get
			{
				return SyncContactSchema.yomiLastName;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00024999 File Offset: 0x00022B99
		public static PropertyTag YomiCompany
		{
			get
			{
				return SyncContactSchema.yomiCompany;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000249A0 File Offset: 0x00022BA0
		public static PropertyTag DisplayNameFirstLast
		{
			get
			{
				return SyncContactSchema.displayNameFirstLast;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000249A7 File Offset: 0x00022BA7
		public static PropertyTag DisplayNameLastFirst
		{
			get
			{
				return SyncContactSchema.displayNameLastFirst;
			}
		}

		// Token: 0x040008D0 RID: 2256
		internal const string PartnerNetworkIDOutlook = "outlook.com";

		// Token: 0x040008D1 RID: 2257
		private static readonly PropertyTag workAddressCity = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32768, PropType.String));

		// Token: 0x040008D2 RID: 2258
		private static readonly PropertyTag legacyWebPage = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32769, PropType.String));

		// Token: 0x040008D3 RID: 2259
		private static readonly PropertyTag workAddressCountry = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32770, PropType.String));

		// Token: 0x040008D4 RID: 2260
		private static readonly PropertyTag email1EmailAddress = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32771, PropType.String));

		// Token: 0x040008D5 RID: 2261
		private static readonly PropertyTag email2EmailAddress = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32772, PropType.String));

		// Token: 0x040008D6 RID: 2262
		private static readonly PropertyTag email3EmailAddress = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32773, PropType.String));

		// Token: 0x040008D7 RID: 2263
		private static readonly PropertyTag fileAsStringInternal = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32774, PropType.String));

		// Token: 0x040008D8 RID: 2264
		private static readonly PropertyTag workAddressPostalCode = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32775, PropType.String));

		// Token: 0x040008D9 RID: 2265
		private static readonly PropertyTag workAddressState = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32776, PropType.String));

		// Token: 0x040008DA RID: 2266
		private static readonly PropertyTag workAddressStreet = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32777, PropType.String));

		// Token: 0x040008DB RID: 2267
		private static readonly PropertyTag yomiFirstName = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32778, PropType.String));

		// Token: 0x040008DC RID: 2268
		private static readonly PropertyTag yomiLastName = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32779, PropType.String));

		// Token: 0x040008DD RID: 2269
		private static readonly PropertyTag yomiCompany = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32780, PropType.String));

		// Token: 0x040008DE RID: 2270
		private static readonly PropertyTag displayNameFirstLast = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32781, PropType.String));

		// Token: 0x040008DF RID: 2271
		private static readonly PropertyTag displayNameLastFirst = new PropertyTag((uint)PropTagHelper.PropTagFromIdAndType(32782, PropType.String));

		// Token: 0x040008E0 RID: 2272
		public static readonly Dictionary<PropertyTag, NamedProperty> PropertyTagToNamedProperties = new Dictionary<PropertyTag, NamedProperty>
		{
			{
				SyncContactSchema.WorkAddressCity,
				new NamedProperty(WellKnownPropertySet.Address, 32838U)
			},
			{
				SyncContactSchema.LegacyWebPage,
				new NamedProperty(WellKnownPropertySet.Address, 32811U)
			},
			{
				SyncContactSchema.WorkAddressCountry,
				new NamedProperty(WellKnownPropertySet.Address, 32841U)
			},
			{
				SyncContactSchema.Email1EmailAddress,
				new NamedProperty(WellKnownPropertySet.Address, 32899U)
			},
			{
				SyncContactSchema.Email2EmailAddress,
				new NamedProperty(WellKnownPropertySet.Address, 32915U)
			},
			{
				SyncContactSchema.Email3EmailAddress,
				new NamedProperty(WellKnownPropertySet.Address, 32931U)
			},
			{
				SyncContactSchema.FileAsStringInternal,
				new NamedProperty(WellKnownPropertySet.Address, 32773U)
			},
			{
				SyncContactSchema.WorkAddressPostalCode,
				new NamedProperty(WellKnownPropertySet.Address, 32840U)
			},
			{
				SyncContactSchema.WorkAddressState,
				new NamedProperty(WellKnownPropertySet.Address, 32839U)
			},
			{
				SyncContactSchema.WorkAddressStreet,
				new NamedProperty(WellKnownPropertySet.Address, 32837U)
			},
			{
				SyncContactSchema.YomiFirstName,
				new NamedProperty(WellKnownPropertySet.Address, 32812U)
			},
			{
				SyncContactSchema.YomiLastName,
				new NamedProperty(WellKnownPropertySet.Address, 32813U)
			},
			{
				SyncContactSchema.YomiCompany,
				new NamedProperty(WellKnownPropertySet.Address, 32814U)
			},
			{
				SyncContactSchema.DisplayNameFirstLast,
				new NamedProperty(WellKnownPropertySet.Address, "DisplayNameFirstLast")
			},
			{
				SyncContactSchema.DisplayNameLastFirst,
				new NamedProperty(WellKnownPropertySet.Address, "DisplayNameLastFirst")
			}
		};

		// Token: 0x040008E1 RID: 2273
		public static readonly List<PropertyTag> AllContactPropertyTags = new List<PropertyTag>
		{
			new PropertyTag(980811807U),
			new PropertyTag(977338432U),
			new PropertyTag(976224287U),
			new PropertyTag(976093215U),
			new PropertyTag(977403968U),
			new PropertyTag(974848031U),
			SyncContactSchema.WorkAddressCity,
			new PropertyTag(973602847U),
			SyncContactSchema.LegacyWebPage,
			SyncContactSchema.WorkAddressCountry,
			new PropertyTag(974651423U),
			SyncContactSchema.Email1EmailAddress,
			SyncContactSchema.Email2EmailAddress,
			SyncContactSchema.Email3EmailAddress,
			new PropertyTag(975437855U),
			SyncContactSchema.FileAsStringInternal,
			new PropertyTag(236781599U),
			new PropertyTag(973471775U),
			new PropertyTag(973078559U),
			new PropertyTag(977535007U),
			new PropertyTag(978911263U),
			new PropertyTag(978976799U),
			new PropertyTag(975503391U),
			new PropertyTag(973668383U),
			new PropertyTag(976158751U),
			new PropertyTag(979042335U),
			new PropertyTag(979107871U),
			new PropertyTag(979173407U),
			new PropertyTag(974913567U),
			new PropertyTag(974520351U),
			new PropertyTag(979304479U),
			new PropertyTag(979370015U),
			new PropertyTag(975044639U),
			new PropertyTag(979435551U),
			new PropertyTag(979501087U),
			new PropertyTag(979566623U),
			new PropertyTag(975241247U),
			SyncContactSchema.WorkAddressPostalCode,
			new PropertyTag(974192671U),
			new PropertyTag(977797151U),
			SyncContactSchema.WorkAddressState,
			SyncContactSchema.WorkAddressStreet,
			new PropertyTag(974585887U),
			SyncContactSchema.YomiFirstName,
			SyncContactSchema.YomiLastName,
			SyncContactSchema.YomiCompany,
			new PropertyTag(974716959U),
			new PropertyTag(974979103U),
			new PropertyTag(980942879U),
			SyncContactSchema.DisplayNameFirstLast,
			SyncContactSchema.DisplayNameLastFirst
		};
	}
}
