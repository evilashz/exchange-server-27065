using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001F8 RID: 504
	internal struct PropertyTag : IEquatable<PropertyTag>, IEquatable<string>, IComparable<PropertyTag>
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x000210EF File Offset: 0x0001F2EF
		public PropertyTag(uint propertyTag)
		{
			this.propertyTag = propertyTag;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000210F8 File Offset: 0x0001F2F8
		public PropertyTag(PropertyId propertyId, PropertyType propertyType)
		{
			this.propertyTag = (uint)((uint)propertyId << 16 | (PropertyId)propertyType);
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00021106 File Offset: 0x0001F306
		public PropertyId PropertyId
		{
			get
			{
				return (PropertyId)((this.propertyTag & 4294901760U) >> 16);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00021118 File Offset: 0x0001F318
		public PropertyType PropertyType
		{
			get
			{
				return (PropertyType)(this.propertyTag & 57343U);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00021128 File Offset: 0x0001F328
		public int? EstimatedValueSize
		{
			get
			{
				PropertyType propertyType = this.PropertyType;
				if (propertyType <= PropertyType.SysTime)
				{
					switch (propertyType)
					{
					case PropertyType.Int16:
						return new int?(2);
					case PropertyType.Int32:
						return new int?(4);
					case PropertyType.Float:
						return new int?(4);
					case PropertyType.Double:
						return new int?(8);
					case PropertyType.Currency:
						return new int?(8);
					case PropertyType.AppTime:
						return new int?(8);
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)12:
					case (PropertyType)14:
					case (PropertyType)15:
					case (PropertyType)16:
					case (PropertyType)17:
					case (PropertyType)18:
					case (PropertyType)19:
						goto IL_E4;
					case PropertyType.Error:
						return new int?(4);
					case PropertyType.Bool:
						return new int?(2);
					case PropertyType.Object:
						break;
					case PropertyType.Int64:
						return new int?(8);
					default:
						switch (propertyType)
						{
						case PropertyType.String8:
						case PropertyType.Unicode:
							break;
						default:
							if (propertyType != PropertyType.SysTime)
							{
								goto IL_E4;
							}
							return new int?(8);
						}
						break;
					}
				}
				else
				{
					if (propertyType == PropertyType.Guid)
					{
						return new int?(16);
					}
					if (propertyType != PropertyType.ServerId && propertyType != PropertyType.Binary)
					{
						goto IL_E4;
					}
				}
				return new int?(0);
				IL_E4:
				return null;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00021222 File Offset: 0x0001F422
		public bool IsMultiValuedProperty
		{
			get
			{
				return (this.PropertyType & (PropertyType)4096) != PropertyType.Unspecified;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00021236 File Offset: 0x0001F436
		public bool IsMultiValueInstanceProperty
		{
			get
			{
				return ((ushort)this.propertyTag & 8192) != 0;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002124B File Offset: 0x0001F44B
		public bool IsStringProperty
		{
			get
			{
				return PropertyTag.IsStringPropertyType(this.PropertyType);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00021258 File Offset: 0x0001F458
		public PropertyType ElementPropertyType
		{
			get
			{
				return this.PropertyType & (PropertyType)53247;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00021267 File Offset: 0x0001F467
		public bool IsNamedProperty
		{
			get
			{
				return PropertyTag.IsNamedPropertyId(this.PropertyId);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00021274 File Offset: 0x0001F474
		public bool IsMarker
		{
			get
			{
				return PropertyTag.MarkerPropertyTags.Contains(this);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00021286 File Offset: 0x0001F486
		public bool IsMetaProperty
		{
			get
			{
				return PropertyTag.MetaPropertyTags.Contains(this);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00021298 File Offset: 0x0001F498
		public bool IsProviderDefinedNonTransmittable
		{
			get
			{
				ushort propertyId = (ushort)this.PropertyId;
				return propertyId >= 26112 && propertyId <= 26623;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000212BF File Offset: 0x0001F4BF
		public static implicit operator uint(PropertyTag propertyTag)
		{
			return propertyTag.propertyTag;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000212C8 File Offset: 0x0001F4C8
		public static PropertyTag CreateError(PropertyId propertyId)
		{
			return new PropertyTag(propertyId, PropertyType.Error);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000212D2 File Offset: 0x0001F4D2
		public static bool operator ==(PropertyTag a, PropertyTag b)
		{
			return a.propertyTag == b.propertyTag;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000212E4 File Offset: 0x0001F4E4
		public static bool operator !=(PropertyTag a, PropertyTag b)
		{
			return a.propertyTag != b.propertyTag;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000212F9 File Offset: 0x0001F4F9
		public static bool HasCompatiblePropertyType(PropertyTag propTag1, PropertyTag propTag2)
		{
			return propTag1.ElementPropertyType == propTag2.ElementPropertyType || (PropertyTag.IsStringPropertyType(propTag1.ElementPropertyType) && PropertyTag.IsStringPropertyType(propTag2.ElementPropertyType));
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00021329 File Offset: 0x0001F529
		public static PropertyTag RemoveMviWithMvIfNeeded(PropertyTag propertyTag)
		{
			if (propertyTag.IsMultiValueInstanceProperty)
			{
				return new PropertyTag(propertyTag.PropertyId, propertyTag.ElementPropertyType);
			}
			return propertyTag;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00021349 File Offset: 0x0001F549
		public static bool IsStringPropertyType(PropertyType propertyType)
		{
			return propertyType == PropertyType.String8 || propertyType == PropertyType.Unicode;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00021357 File Offset: 0x0001F557
		public bool Equals(PropertyTag other)
		{
			return this.propertyTag == other.propertyTag;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00021368 File Offset: 0x0001F568
		public bool Equals(string other)
		{
			return other != null && StringComparer.OrdinalIgnoreCase.Equals(this.ToString(), other);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00021388 File Offset: 0x0001F588
		public int CompareTo(PropertyTag other)
		{
			return this.propertyTag.CompareTo(other.propertyTag);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000213AA File Offset: 0x0001F5AA
		public override bool Equals(object obj)
		{
			if (!(obj is PropertyTag))
			{
				return this.Equals(obj as string);
			}
			return this.Equals((PropertyTag)obj);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000213D0 File Offset: 0x0001F5D0
		public override int GetHashCode()
		{
			return this.propertyTag.GetHashCode();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000213EC File Offset: 0x0001F5EC
		public PropertyTag ChangeElementPropertyType(PropertyType newPropertyType)
		{
			PropertyTag propertyTag = new PropertyTag(this.PropertyId, (newPropertyType & (PropertyType)53247) | checked((PropertyType)(this.propertyTag & 12288U)));
			if (PropertyTag.HasCompatiblePropertyType(this, propertyTag))
			{
				return propertyTag;
			}
			throw new InvalidOperationException(string.Format("The new requested element property type {0} is incompatible with the current one {1} for the PropertyTag {2}", newPropertyType, this.ElementPropertyType, this));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00021458 File Offset: 0x0001F658
		public override string ToString()
		{
			return this.propertyTag.ToString("X8");
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00021478 File Offset: 0x0001F678
		internal static bool IsNamedPropertyId(PropertyId propertyId)
		{
			return propertyId >= (PropertyId)32768;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00021488 File Offset: 0x0001F688
		internal static PropertyType FromClrType(Type type)
		{
			if (typeof(short) == type)
			{
				return PropertyType.Int16;
			}
			if (typeof(int) == type)
			{
				return PropertyType.Int32;
			}
			if (typeof(long) == type)
			{
				return PropertyType.Int64;
			}
			if (typeof(float) == type)
			{
				return PropertyType.Float;
			}
			if (typeof(double) == type)
			{
				return PropertyType.Double;
			}
			if (typeof(ExDateTime) == type)
			{
				return PropertyType.SysTime;
			}
			if (typeof(bool) == type)
			{
				return PropertyType.Bool;
			}
			if (typeof(string) == type)
			{
				return PropertyType.Unicode;
			}
			if (typeof(Guid) == type)
			{
				return PropertyType.Guid;
			}
			if (typeof(byte[]) == type)
			{
				return PropertyType.Binary;
			}
			if (typeof(string[]) == type)
			{
				return PropertyType.MultiValueUnicode;
			}
			if (typeof(byte[][]) == type)
			{
				return PropertyType.MultiValueBinary;
			}
			if (typeof(short[]) == type)
			{
				return PropertyType.MultiValueInt16;
			}
			if (typeof(int[]) == type)
			{
				return PropertyType.MultiValueInt32;
			}
			if (typeof(float[]) == type)
			{
				return PropertyType.MultiValueFloat;
			}
			if (typeof(double[]) == type)
			{
				return PropertyType.MultiValueDouble;
			}
			if (typeof(long[]) == type)
			{
				return PropertyType.MultiValueInt64;
			}
			if (typeof(ExDateTime[]) == type)
			{
				return PropertyType.MultiValueSysTime;
			}
			if (typeof(Guid[]) == type)
			{
				return PropertyType.MultiValueGuid;
			}
			throw new ArgumentException(string.Format("Invalid property type {0}", type));
		}

		// Token: 0x04000524 RID: 1316
		public const int Size = 4;

		// Token: 0x04000525 RID: 1317
		private const uint PropertyIdMask = 4294901760U;

		// Token: 0x04000526 RID: 1318
		private const uint PropertyTypeMask = 57343U;

		// Token: 0x04000527 RID: 1319
		private const ushort ElementPropertyTypeMask = 53247;

		// Token: 0x04000528 RID: 1320
		public static readonly IEqualityComparer<PropertyTag> PropertyIdComparer = new PropertyTag.PropertyIdComparerImpl();

		// Token: 0x04000529 RID: 1321
		private readonly uint propertyTag;

		// Token: 0x0400052A RID: 1322
		internal static readonly PropertyTag StartTopFld = new PropertyTag(1074331651U);

		// Token: 0x0400052B RID: 1323
		internal static readonly PropertyTag StartSubFld = new PropertyTag(1074397187U);

		// Token: 0x0400052C RID: 1324
		internal static readonly PropertyTag EndFolder = new PropertyTag(1074462723U);

		// Token: 0x0400052D RID: 1325
		internal static readonly PropertyTag StartMessage = new PropertyTag(1074528259U);

		// Token: 0x0400052E RID: 1326
		internal static readonly PropertyTag StartFAIMsg = new PropertyTag(1074790403U);

		// Token: 0x0400052F RID: 1327
		internal static readonly PropertyTag EndMessage = new PropertyTag(1074593795U);

		// Token: 0x04000530 RID: 1328
		internal static readonly PropertyTag StartEmbed = new PropertyTag(1073807363U);

		// Token: 0x04000531 RID: 1329
		internal static readonly PropertyTag EndEmbed = new PropertyTag(1073872899U);

		// Token: 0x04000532 RID: 1330
		internal static readonly PropertyTag StartRecip = new PropertyTag(1073938435U);

		// Token: 0x04000533 RID: 1331
		internal static readonly PropertyTag EndRecip = new PropertyTag(1074003971U);

		// Token: 0x04000534 RID: 1332
		internal static readonly PropertyTag NewAttach = new PropertyTag(1073741827U);

		// Token: 0x04000535 RID: 1333
		internal static readonly PropertyTag EndAttach = new PropertyTag(1074659331U);

		// Token: 0x04000536 RID: 1334
		internal static readonly PropertyTag EcWarning = new PropertyTag(1074724867U);

		// Token: 0x04000537 RID: 1335
		private static readonly PropertyTag FXErrorInfo = new PropertyTag(1075314691U);

		// Token: 0x04000538 RID: 1336
		internal static readonly PropertyTag FXDelProp = new PropertyTag(1075183619U);

		// Token: 0x04000539 RID: 1337
		internal static readonly PropertyTag IncrSyncChg = new PropertyTag(1074921475U);

		// Token: 0x0400053A RID: 1338
		internal static readonly PropertyTag IncrSyncChgPartial = new PropertyTag(1081933827U);

		// Token: 0x0400053B RID: 1339
		internal static readonly PropertyTag IncrSyncDel = new PropertyTag(1074987011U);

		// Token: 0x0400053C RID: 1340
		internal static readonly PropertyTag IncrSyncEnd = new PropertyTag(1075052547U);

		// Token: 0x0400053D RID: 1341
		internal static readonly PropertyTag IncrSyncRead = new PropertyTag(1076822019U);

		// Token: 0x0400053E RID: 1342
		internal static readonly PropertyTag IncrSyncStateBegin = new PropertyTag(1077542915U);

		// Token: 0x0400053F RID: 1343
		internal static readonly PropertyTag IncrSyncStateEnd = new PropertyTag(1077608451U);

		// Token: 0x04000540 RID: 1344
		internal static readonly PropertyTag IncrSyncProgressMode = new PropertyTag(1081344011U);

		// Token: 0x04000541 RID: 1345
		internal static readonly PropertyTag IncrSyncProgressPerMsg = new PropertyTag(1081409547U);

		// Token: 0x04000542 RID: 1346
		internal static readonly PropertyTag IncrSyncGroupInfo = new PropertyTag(1081803010U);

		// Token: 0x04000543 RID: 1347
		internal static readonly PropertyTag IncrSyncGroupId = new PropertyTag(1081868291U);

		// Token: 0x04000544 RID: 1348
		internal static readonly PropertyTag IncrSyncMsg = new PropertyTag(1075118083U);

		// Token: 0x04000545 RID: 1349
		internal static readonly PropertyTag IncrSyncMsgPartial = new PropertyTag(1081737219U);

		// Token: 0x04000546 RID: 1350
		internal static readonly PropertyTag MessageAttachments = new PropertyTag(236126221U);

		// Token: 0x04000547 RID: 1351
		internal static readonly PropertyTag MessageRecipients = new PropertyTag(236060685U);

		// Token: 0x04000548 RID: 1352
		internal static readonly PropertyTag ContainerHierarchy = new PropertyTag(906887181U);

		// Token: 0x04000549 RID: 1353
		internal static readonly PropertyTag ContainerContents = new PropertyTag(906952717U);

		// Token: 0x0400054A RID: 1354
		internal static readonly PropertyTag FolderAssociatedContents = new PropertyTag(907018253U);

		// Token: 0x0400054B RID: 1355
		internal static readonly PropertyTag DNPrefix = new PropertyTag(1074266142U);

		// Token: 0x0400054C RID: 1356
		internal static readonly PropertyTag NewFXFolder = new PropertyTag(1074856194U);

		// Token: 0x0400054D RID: 1357
		private static readonly PropertyTag IncrSyncImailStreamContinue = new PropertyTag(1080426499U);

		// Token: 0x0400054E RID: 1358
		private static readonly PropertyTag IncrSyncImailStreamCancel = new PropertyTag(1080492035U);

		// Token: 0x0400054F RID: 1359
		private static readonly PropertyTag IncrSyncImailStream2Continue = new PropertyTag(1081147395U);

		// Token: 0x04000550 RID: 1360
		internal static readonly PropertyTag PropertyGroupMappingInfo = new PropertyTag(258U);

		// Token: 0x04000551 RID: 1361
		internal static readonly PropertyTag OriginalMessageEntryId = new PropertyTag(806289666U);

		// Token: 0x04000552 RID: 1362
		internal static readonly PropertyTag Fid = new PropertyTag(1732771860U);

		// Token: 0x04000553 RID: 1363
		internal static readonly PropertyTag Mid = new PropertyTag(1732902932U);

		// Token: 0x04000554 RID: 1364
		internal static readonly PropertyTag ParentFid = new PropertyTag(1732837396U);

		// Token: 0x04000555 RID: 1365
		internal static readonly PropertyTag SourceKey = new PropertyTag(1709179138U);

		// Token: 0x04000556 RID: 1366
		internal static readonly PropertyTag ParentSourceKey = new PropertyTag(1709244674U);

		// Token: 0x04000557 RID: 1367
		internal static readonly PropertyTag ExternalFid = PropertyTag.SourceKey;

		// Token: 0x04000558 RID: 1368
		internal static readonly PropertyTag ExternalParentFid = PropertyTag.ParentSourceKey;

		// Token: 0x04000559 RID: 1369
		internal static readonly PropertyTag ExternalMid = PropertyTag.SourceKey;

		// Token: 0x0400055A RID: 1370
		internal static readonly PropertyTag LastModificationTime = new PropertyTag(805830720U);

		// Token: 0x0400055B RID: 1371
		internal static readonly PropertyTag ChangeKey = new PropertyTag(1709310210U);

		// Token: 0x0400055C RID: 1372
		internal static readonly PropertyTag ChangeNumber = new PropertyTag(1738801172U);

		// Token: 0x0400055D RID: 1373
		internal static readonly PropertyTag ExternalChangeNumber = PropertyTag.ChangeKey;

		// Token: 0x0400055E RID: 1374
		internal static readonly PropertyTag PredecessorChangeList = new PropertyTag(1709375746U);

		// Token: 0x0400055F RID: 1375
		internal static readonly PropertyTag ExternalPredecessorChangeList = PropertyTag.PredecessorChangeList;

		// Token: 0x04000560 RID: 1376
		internal static readonly PropertyTag ReadChangeNumber = new PropertyTag(1744699412U);

		// Token: 0x04000561 RID: 1377
		internal static readonly PropertyTag DisplayName = new PropertyTag(805371935U);

		// Token: 0x04000562 RID: 1378
		internal static readonly PropertyTag Comment = new PropertyTag(805568543U);

		// Token: 0x04000563 RID: 1379
		internal static readonly PropertyTag MessageSize = new PropertyTag(235405315U);

		// Token: 0x04000564 RID: 1380
		internal static readonly PropertyTag Associated = new PropertyTag(1739194379U);

		// Token: 0x04000565 RID: 1381
		internal static readonly PropertyTag IdsetDeleted = new PropertyTag(1743061250U);

		// Token: 0x04000566 RID: 1382
		internal static readonly PropertyTag IdsetExpired = new PropertyTag(1737687298U);

		// Token: 0x04000567 RID: 1383
		internal static readonly PropertyTag IdsetSoftDeleted = new PropertyTag(1075904770U);

		// Token: 0x04000568 RID: 1384
		internal static readonly PropertyTag IdsetRead = new PropertyTag(1076691202U);

		// Token: 0x04000569 RID: 1385
		internal static readonly PropertyTag IdsetUnread = new PropertyTag(1076756738U);

		// Token: 0x0400056A RID: 1386
		internal static readonly PropertyTag FreeBusyNTSD = new PropertyTag(251658498U);

		// Token: 0x0400056B RID: 1387
		internal static readonly PropertyTag AttachmentNumber = new PropertyTag(237043715U);

		// Token: 0x0400056C RID: 1388
		internal static readonly PropertyTag ObjectType = new PropertyTag(268304387U);

		// Token: 0x0400056D RID: 1389
		internal static readonly PropertyTag AttachmentDataObject = new PropertyTag(922812429U);

		// Token: 0x0400056E RID: 1390
		internal static readonly PropertyTag AttachmentMethod = new PropertyTag(923074563U);

		// Token: 0x0400056F RID: 1391
		internal static readonly PropertyTag ContainerClass = new PropertyTag(907214879U);

		// Token: 0x04000570 RID: 1392
		internal static readonly PropertyTag PackedNamedProps = new PropertyTag(907804930U);

		// Token: 0x04000571 RID: 1393
		private static readonly HashSet<PropertyTag> MarkerPropertyTags = new HashSet<PropertyTag>(new PropertyTag[]
		{
			PropertyTag.StartTopFld,
			PropertyTag.StartSubFld,
			PropertyTag.EndFolder,
			PropertyTag.StartMessage,
			PropertyTag.StartFAIMsg,
			PropertyTag.EndMessage,
			PropertyTag.StartEmbed,
			PropertyTag.EndEmbed,
			PropertyTag.StartRecip,
			PropertyTag.EndRecip,
			PropertyTag.NewAttach,
			PropertyTag.EndAttach,
			PropertyTag.IncrSyncChg,
			PropertyTag.IncrSyncChgPartial,
			PropertyTag.IncrSyncDel,
			PropertyTag.IncrSyncEnd,
			PropertyTag.IncrSyncMsg,
			PropertyTag.IncrSyncRead,
			PropertyTag.IncrSyncStateBegin,
			PropertyTag.IncrSyncStateEnd,
			PropertyTag.IncrSyncProgressMode,
			PropertyTag.IncrSyncProgressPerMsg,
			PropertyTag.IncrSyncGroupInfo,
			PropertyTag.FXErrorInfo,
			PropertyTag.IncrSyncImailStreamContinue,
			PropertyTag.IncrSyncImailStreamCancel,
			PropertyTag.IncrSyncImailStream2Continue,
			PropertyTag.IncrSyncGroupId,
			PropertyTag.IncrSyncMsgPartial
		});

		// Token: 0x04000572 RID: 1394
		private static readonly HashSet<PropertyTag> MetaPropertyTags = new HashSet<PropertyTag>(new PropertyTag[]
		{
			PropertyTag.DNPrefix,
			PropertyTag.FXDelProp,
			PropertyTag.EcWarning,
			PropertyTag.NewFXFolder,
			PropertyTag.FXErrorInfo,
			PropertyTag.IncrSyncGroupId,
			PropertyTag.IncrSyncMsgPartial
		});

		// Token: 0x04000573 RID: 1395
		internal static readonly PropertyTag EntryId = new PropertyTag(268370178U);

		// Token: 0x04000574 RID: 1396
		internal static readonly PropertyTag SenderEntryId = new PropertyTag(202965250U);

		// Token: 0x04000575 RID: 1397
		internal static readonly PropertyTag SentRepresentingEntryId = new PropertyTag(4260098U);

		// Token: 0x04000576 RID: 1398
		internal static readonly PropertyTag ReceivedByEntryId = new PropertyTag(4129026U);

		// Token: 0x04000577 RID: 1399
		internal static readonly PropertyTag ReceivedRepresentingEntryId = new PropertyTag(4391170U);

		// Token: 0x04000578 RID: 1400
		internal static readonly PropertyTag ReadReceiptEntryId = new PropertyTag(4587778U);

		// Token: 0x04000579 RID: 1401
		internal static readonly PropertyTag ReportEntryId = new PropertyTag(4522242U);

		// Token: 0x0400057A RID: 1402
		internal static readonly PropertyTag OriginatorEntryId = new PropertyTag(1717436674U);

		// Token: 0x0400057B RID: 1403
		internal static readonly PropertyTag CreatorEntryId = new PropertyTag(1073283330U);

		// Token: 0x0400057C RID: 1404
		internal static readonly PropertyTag LastModifierEntryId = new PropertyTag(1073414402U);

		// Token: 0x0400057D RID: 1405
		internal static readonly PropertyTag OriginalSenderEntryId = new PropertyTag(5964034U);

		// Token: 0x0400057E RID: 1406
		internal static readonly PropertyTag OriginalSentRepresentingEntryId = new PropertyTag(6160642U);

		// Token: 0x0400057F RID: 1407
		internal static readonly PropertyTag OriginalEntryId = new PropertyTag(974258434U);

		// Token: 0x04000580 RID: 1408
		internal static readonly PropertyTag ReportDestinationEntryId = new PropertyTag(1717895426U);

		// Token: 0x04000581 RID: 1409
		internal static readonly PropertyTag OriginalAuthorEntryId = new PropertyTag(4980994U);

		// Token: 0x04000582 RID: 1410
		public static readonly PropertyTag[] OneOffEntryIdPropertyTags = new PropertyTag[]
		{
			PropertyTag.EntryId,
			PropertyTag.SenderEntryId,
			PropertyTag.SentRepresentingEntryId,
			PropertyTag.ReceivedByEntryId,
			PropertyTag.ReceivedRepresentingEntryId,
			PropertyTag.ReadReceiptEntryId,
			PropertyTag.ReportEntryId,
			PropertyTag.OriginatorEntryId,
			PropertyTag.CreatorEntryId,
			PropertyTag.LastModifierEntryId,
			PropertyTag.OriginalSenderEntryId,
			PropertyTag.OriginalSentRepresentingEntryId,
			PropertyTag.OriginalEntryId,
			PropertyTag.ReportDestinationEntryId,
			PropertyTag.OriginalAuthorEntryId
		};

		// Token: 0x04000583 RID: 1411
		internal static readonly PropertyTag ParentEntryId = new PropertyTag(235471106U);

		// Token: 0x04000584 RID: 1412
		internal static readonly PropertyTag ConflictEntryId = new PropertyTag(1072693506U);

		// Token: 0x04000585 RID: 1413
		internal static readonly PropertyTag RuleFolderEntryId = new PropertyTag(1716584706U);

		// Token: 0x04000586 RID: 1414
		internal static readonly PropertyTag AddressType = new PropertyTag(805437471U);

		// Token: 0x04000587 RID: 1415
		internal static readonly PropertyTag EmailAddress = new PropertyTag(805503007U);

		// Token: 0x04000588 RID: 1416
		internal static readonly PropertyTag RowId = new PropertyTag(805306371U);

		// Token: 0x04000589 RID: 1417
		internal static readonly PropertyTag InstanceKey = new PropertyTag(267780354U);

		// Token: 0x0400058A RID: 1418
		internal static readonly PropertyTag RecipientType = new PropertyTag(202702851U);

		// Token: 0x0400058B RID: 1419
		internal static readonly PropertyTag SearchKey = new PropertyTag(806027522U);

		// Token: 0x0400058C RID: 1420
		internal static readonly PropertyTag TransmittableDisplayName = new PropertyTag(975175711U);

		// Token: 0x0400058D RID: 1421
		public static readonly PropertyTag SimpleDisplayName = new PropertyTag(973013023U);

		// Token: 0x0400058E RID: 1422
		internal static readonly PropertyTag Responsibility = new PropertyTag(235864075U);

		// Token: 0x0400058F RID: 1423
		internal static readonly PropertyTag SendRichInfo = new PropertyTag(977272843U);

		// Token: 0x04000590 RID: 1424
		public static readonly PropertyTag SendInternetEncoding = new PropertyTag(980484099U);

		// Token: 0x04000591 RID: 1425
		internal static readonly PropertyTag[] StandardRecipientPropertyTags = new PropertyTag[]
		{
			PropertyTag.DisplayName,
			PropertyTag.AddressType,
			PropertyTag.EmailAddress,
			PropertyTag.RowId,
			PropertyTag.InstanceKey,
			PropertyTag.RecipientType,
			PropertyTag.EntryId,
			PropertyTag.SearchKey,
			PropertyTag.TransmittableDisplayName,
			PropertyTag.Responsibility,
			PropertyTag.SendRichInfo
		};

		// Token: 0x04000592 RID: 1426
		internal static readonly PropertyTag RtfCompressed = new PropertyTag(269025538U);

		// Token: 0x04000593 RID: 1427
		internal static readonly PropertyTag AlternateBestBody = new PropertyTag(269091074U);

		// Token: 0x04000594 RID: 1428
		internal static readonly PropertyTag HtmlBody = new PropertyTag(269680671U);

		// Token: 0x04000595 RID: 1429
		internal static readonly PropertyTag Html = new PropertyTag(269680898U);

		// Token: 0x04000596 RID: 1430
		internal static readonly PropertyTag Body = new PropertyTag(268435487U);

		// Token: 0x04000597 RID: 1431
		internal static readonly PropertyTag NativeBodyInfo = new PropertyTag(269877251U);

		// Token: 0x04000598 RID: 1432
		internal static readonly PropertyTag RtfInSync = new PropertyTag(236912651U);

		// Token: 0x04000599 RID: 1433
		internal static readonly PropertyTag Preview = new PropertyTag(1071185951U);

		// Token: 0x0400059A RID: 1434
		internal static readonly PropertyTag PreviewUnread = new PropertyTag(1071120415U);

		// Token: 0x0400059B RID: 1435
		internal static readonly PropertyTag SentMailServerId = new PropertyTag(1732247803U);

		// Token: 0x0400059C RID: 1436
		internal static readonly PropertyTag DamOrgMsgServerId = new PropertyTag(1732313339U);

		// Token: 0x0400059D RID: 1437
		internal static readonly PropertyTag SentMailEntryId = new PropertyTag(235536642U);

		// Token: 0x0400059E RID: 1438
		internal static readonly PropertyTag DamOrgMsgEntryId = new PropertyTag(1715863810U);

		// Token: 0x0400059F RID: 1439
		internal static readonly PropertyTag ConflictMsgKey = new PropertyTag(1070203138U);

		// Token: 0x040005A0 RID: 1440
		internal static readonly PropertyTag RuleFolderFid = new PropertyTag(1731592212U);

		// Token: 0x040005A1 RID: 1441
		internal static readonly PropertyTag MessageClass = new PropertyTag(1703966U);

		// Token: 0x040005A2 RID: 1442
		internal static readonly PropertyTag MessageSubmissionIdFromClient = new PropertyTag(1076625666U);

		// Token: 0x040005A3 RID: 1443
		internal static readonly PropertyTag MessageSubmissionId = new PropertyTag(4653314U);

		// Token: 0x040005A4 RID: 1444
		internal static readonly PropertyTag DeferredSendTime = new PropertyTag(1072627776U);

		// Token: 0x040005A5 RID: 1445
		internal static readonly PropertyTag ConversationItemIds = new PropertyTag(1755320578U);

		// Token: 0x040005A6 RID: 1446
		internal static readonly PropertyTag ConversationItemIdsMailboxWide = new PropertyTag(1755386114U);

		// Token: 0x040005A7 RID: 1447
		internal static readonly PropertyTag UnsearchableItems = new PropertyTag(905838850U);

		// Token: 0x040005A8 RID: 1448
		public static readonly PropertyTag MimeSkeleton = new PropertyTag(1693450498U);

		// Token: 0x040005A9 RID: 1449
		internal static readonly PropertyTag Read = new PropertyTag(241762315U);

		// Token: 0x040005AA RID: 1450
		internal static readonly PropertyTag NTSecurityDescriptor = new PropertyTag(237437186U);

		// Token: 0x040005AB RID: 1451
		internal static readonly PropertyTag AclTableAndSecurityDescriptor = new PropertyTag(239010050U);

		// Token: 0x040005AC RID: 1452
		internal static readonly PropertyTag AccessLevel = new PropertyTag(267845635U);

		// Token: 0x040005AD RID: 1453
		internal static readonly PropertyTag DisplayType = new PropertyTag(956301315U);

		// Token: 0x040005AE RID: 1454
		internal static readonly PropertyTag LongTermEntryIdFromTable = new PropertyTag(1718616322U);

		// Token: 0x040005AF RID: 1455
		internal static readonly PropertyTag LongtermEntryId = new PropertyTag(1718616322U);

		// Token: 0x040005B0 RID: 1456
		internal static readonly PropertyTag MappingSignature = new PropertyTag(267911426U);

		// Token: 0x040005B1 RID: 1457
		internal static readonly PropertyTag MdbProvider = new PropertyTag(873726210U);

		// Token: 0x040005B2 RID: 1458
		internal static readonly PropertyTag NormalizedSubject = new PropertyTag(236781599U);

		// Token: 0x040005B3 RID: 1459
		internal static readonly PropertyTag OfflineFlags = new PropertyTag(1715273731U);

		// Token: 0x040005B4 RID: 1460
		internal static readonly PropertyTag RecordKey = new PropertyTag(267976962U);

		// Token: 0x040005B5 RID: 1461
		internal static readonly PropertyTag ReplicaServer = new PropertyTag(1715732511U);

		// Token: 0x040005B6 RID: 1462
		internal static readonly PropertyTag ReplicaVersion = new PropertyTag(1716191252U);

		// Token: 0x040005B7 RID: 1463
		internal static readonly PropertyTag StoreEntryId = new PropertyTag(268108034U);

		// Token: 0x040005B8 RID: 1464
		internal static readonly PropertyTag StoreRecordKey = new PropertyTag(268042498U);

		// Token: 0x040005B9 RID: 1465
		internal static readonly PropertyTag StoreSupportMask = new PropertyTag(873267203U);

		// Token: 0x040005BA RID: 1466
		internal static readonly PropertyTag Subject = new PropertyTag(3604511U);

		// Token: 0x040005BB RID: 1467
		internal static readonly PropertyTag SubjectPrefix = new PropertyTag(3997727U);

		// Token: 0x040005BC RID: 1468
		internal static readonly PropertyTag PostReplyFolderEntries = new PropertyTag(272433410U);

		// Token: 0x040005BD RID: 1469
		internal static readonly PropertyTag Depth = new PropertyTag(805634051U);

		// Token: 0x040005BE RID: 1470
		internal static readonly PropertyTag RuleCondition = new PropertyTag(1719206141U);

		// Token: 0x040005BF RID: 1471
		internal static readonly PropertyTag ValidFolderMask = new PropertyTag(903806979U);

		// Token: 0x040005C0 RID: 1472
		internal static readonly PropertyTag CodePageId = new PropertyTag(1724055555U);

		// Token: 0x040005C1 RID: 1473
		internal static readonly PropertyTag LocaleId = new PropertyTag(1721827331U);

		// Token: 0x040005C2 RID: 1474
		internal static readonly PropertyTag SortLocaleId = new PropertyTag(1728380931U);

		// Token: 0x040005C3 RID: 1475
		internal static readonly PropertyTag IPMSubtreeFolder = new PropertyTag(903872770U);

		// Token: 0x040005C4 RID: 1476
		internal static readonly PropertyTag IPMInboxFolder = new PropertyTag(903938306U);

		// Token: 0x040005C5 RID: 1477
		internal static readonly PropertyTag IPMOutboxFolder = new PropertyTag(904003842U);

		// Token: 0x040005C6 RID: 1478
		internal static readonly PropertyTag IPMSentmailFolder = new PropertyTag(904134914U);

		// Token: 0x040005C7 RID: 1479
		internal static readonly PropertyTag IPMWastebasketFolder = new PropertyTag(904069378U);

		// Token: 0x040005C8 RID: 1480
		internal static readonly PropertyTag IPMFinderFolder = new PropertyTag(904331522U);

		// Token: 0x040005C9 RID: 1481
		internal static readonly PropertyTag IPMShortcutsFolder = new PropertyTag(1714422018U);

		// Token: 0x040005CA RID: 1482
		internal static readonly PropertyTag IPMViewsFolder = new PropertyTag(904200450U);

		// Token: 0x040005CB RID: 1483
		internal static readonly PropertyTag IPMCommonViewsFolder = new PropertyTag(904265986U);

		// Token: 0x040005CC RID: 1484
		internal static readonly PropertyTag IPMDafFolder = new PropertyTag(1713307906U);

		// Token: 0x040005CD RID: 1485
		internal static readonly PropertyTag NonIPMSubtreeFolder = new PropertyTag(1713373442U);

		// Token: 0x040005CE RID: 1486
		internal static readonly PropertyTag EformsRegistryFolder = new PropertyTag(1713438978U);

		// Token: 0x040005CF RID: 1487
		internal static readonly PropertyTag SplusFreeBusyFolder = new PropertyTag(1713504514U);

		// Token: 0x040005D0 RID: 1488
		internal static readonly PropertyTag OfflineAddrBookFolder = new PropertyTag(1713570050U);

		// Token: 0x040005D1 RID: 1489
		internal static readonly PropertyTag ArticleIndexFolder = new PropertyTag(1737097474U);

		// Token: 0x040005D2 RID: 1490
		internal static readonly PropertyTag LocaleEformsRegistryFolder = new PropertyTag(1713635586U);

		// Token: 0x040005D3 RID: 1491
		internal static readonly PropertyTag LocalSiteFreeBusyFolder = new PropertyTag(1713701122U);

		// Token: 0x040005D4 RID: 1492
		internal static readonly PropertyTag LocalSiteAddrBookFolder = new PropertyTag(1713766658U);

		// Token: 0x040005D5 RID: 1493
		internal static readonly PropertyTag MTSInFolder = new PropertyTag(1713897730U);

		// Token: 0x040005D6 RID: 1494
		internal static readonly PropertyTag MTSOutFolder = new PropertyTag(1713963266U);

		// Token: 0x040005D7 RID: 1495
		internal static readonly PropertyTag ScheduleFolder = new PropertyTag(1713242370U);

		// Token: 0x040005D8 RID: 1496
		internal static readonly PropertyTag StoreState = new PropertyTag(873332739U);

		// Token: 0x040005D9 RID: 1497
		internal static readonly PropertyTag HierarchyServer = new PropertyTag(1714618626U);

		// Token: 0x040005DA RID: 1498
		internal static readonly PropertyTag LogonRightsOnMailbox = new PropertyTag(1736245251U);

		// Token: 0x040005DB RID: 1499
		internal static readonly PropertyTag MessageFlags = new PropertyTag(235339779U);

		// Token: 0x040005DC RID: 1500
		internal static readonly PropertyTag OriginalDisplayBcc = new PropertyTag(7471135U);

		// Token: 0x040005DD RID: 1501
		internal static readonly PropertyTag OriginalDisplayCc = new PropertyTag(7536671U);

		// Token: 0x040005DE RID: 1502
		internal static readonly PropertyTag OriginalDisplayTo = new PropertyTag(7602207U);

		// Token: 0x040005DF RID: 1503
		internal static readonly PropertyTag MessageDeliveryTime = new PropertyTag(235274304U);

		// Token: 0x040005E0 RID: 1504
		internal static readonly PropertyTag RtfSyncBodyCRC = new PropertyTag(268828675U);

		// Token: 0x040005E1 RID: 1505
		internal static readonly PropertyTag RtfSyncBodyCount = new PropertyTag(268894211U);

		// Token: 0x040005E2 RID: 1506
		internal static readonly PropertyTag RtfSyncBodyTag = new PropertyTag(268959775U);

		// Token: 0x040005E3 RID: 1507
		internal static readonly PropertyTag RtfSyncPrefixCount = new PropertyTag(269484035U);

		// Token: 0x040005E4 RID: 1508
		internal static readonly PropertyTag RtfSyncTrailingCount = new PropertyTag(269549571U);

		// Token: 0x040005E5 RID: 1509
		internal static readonly PropertyTag CreatorName = new PropertyTag(1073217567U);

		// Token: 0x040005E6 RID: 1510
		internal static readonly PropertyTag UrlCompNamePostfix = new PropertyTag(241238019U);

		// Token: 0x040005E7 RID: 1511
		internal static readonly PropertyTag MimeSize = new PropertyTag(1732640771U);

		// Token: 0x040005E8 RID: 1512
		internal static readonly PropertyTag FileSize = new PropertyTag(1732706307U);

		// Token: 0x040005E9 RID: 1513
		internal static readonly PropertyTag InternetReference = new PropertyTag(272171039U);

		// Token: 0x040005EA RID: 1514
		internal static readonly PropertyTag InternetNewsGroups = new PropertyTag(271974431U);

		// Token: 0x040005EB RID: 1515
		internal static readonly PropertyTag ImapCachedBodystructure = new PropertyTag(1735196930U);

		// Token: 0x040005EC RID: 1516
		internal static readonly PropertyTag ImapCachedEnvelope = new PropertyTag(1735131394U);

		// Token: 0x040005ED RID: 1517
		internal static readonly PropertyTag LocalCommitTime = new PropertyTag(1728643136U);

		// Token: 0x040005EE RID: 1518
		internal static readonly PropertyTag AutoReset = new PropertyTag(1728843848U);

		// Token: 0x040005EF RID: 1519
		internal static readonly PropertyTag DeletedOn = new PropertyTag(1720647744U);

		// Token: 0x040005F0 RID: 1520
		internal static readonly PropertyTag SMTPTempTblData = new PropertyTag(281018626U);

		// Token: 0x040005F1 RID: 1521
		internal static readonly PropertyTag SMTPTempTblData2 = new PropertyTag(281084162U);

		// Token: 0x040005F2 RID: 1522
		internal static readonly PropertyTag SMTPTempTblData3 = new PropertyTag(281149698U);

		// Token: 0x040005F3 RID: 1523
		internal static readonly PropertyTag AttachmentSize = new PropertyTag(236978179U);

		// Token: 0x040005F4 RID: 1524
		internal static readonly PropertyTag DisplayBcc = new PropertyTag(235012127U);

		// Token: 0x040005F5 RID: 1525
		internal static readonly PropertyTag DisplayCc = new PropertyTag(235077663U);

		// Token: 0x040005F6 RID: 1526
		internal static readonly PropertyTag DisplayTo = new PropertyTag(235143199U);

		// Token: 0x040005F7 RID: 1527
		internal static readonly PropertyTag HasAttach = new PropertyTag(236650507U);

		// Token: 0x040005F8 RID: 1528
		internal static readonly PropertyTag Access = new PropertyTag(267649027U);

		// Token: 0x040005F9 RID: 1529
		internal static readonly PropertyTag InstanceId = new PropertyTag(1733099540U);

		// Token: 0x040005FA RID: 1530
		internal static readonly PropertyTag RowType = new PropertyTag(267714563U);

		// Token: 0x040005FB RID: 1531
		internal static readonly PropertyTag SecureSubmitFlags = new PropertyTag(1707474947U);

		// Token: 0x040005FC RID: 1532
		internal static readonly PropertyTag FolderNamedProperties = new PropertyTag(907804930U);

		// Token: 0x040005FD RID: 1533
		internal static readonly PropertyTag ContentCount = new PropertyTag(906100739U);

		// Token: 0x040005FE RID: 1534
		internal static readonly PropertyTag ContentUnread = new PropertyTag(906166275U);

		// Token: 0x040005FF RID: 1535
		internal static readonly PropertyTag FolderType = new PropertyTag(906035203U);

		// Token: 0x04000600 RID: 1536
		internal static readonly PropertyTag IsNewsgroupAnchor = new PropertyTag(1721106443U);

		// Token: 0x04000601 RID: 1537
		internal static readonly PropertyTag IsNewsgroup = new PropertyTag(1721171979U);

		// Token: 0x04000602 RID: 1538
		internal static readonly PropertyTag NewsgroupComp = new PropertyTag(1722089503U);

		// Token: 0x04000603 RID: 1539
		internal static readonly PropertyTag InetNewsgroupName = new PropertyTag(1722220575U);

		// Token: 0x04000604 RID: 1540
		internal static readonly PropertyTag NewsfeedAcl = new PropertyTag(1722155266U);

		// Token: 0x04000605 RID: 1541
		internal static readonly PropertyTag DeletedMsgCt = new PropertyTag(1715470339U);

		// Token: 0x04000606 RID: 1542
		internal static readonly PropertyTag DeletedAssocMsgCt = new PropertyTag(1715666947U);

		// Token: 0x04000607 RID: 1543
		internal static readonly PropertyTag DeletedFolderCt = new PropertyTag(1715535875U);

		// Token: 0x04000608 RID: 1544
		internal static readonly PropertyTag DeletedMessageSizeExtended = new PropertyTag(1721434132U);

		// Token: 0x04000609 RID: 1545
		internal static readonly PropertyTag DeletedAssocMessageSizeExtended = new PropertyTag(1721565204U);

		// Token: 0x0400060A RID: 1546
		internal static readonly PropertyTag DeletedNormalMessageSizeExtended = new PropertyTag(1721499668U);

		// Token: 0x0400060B RID: 1547
		internal static readonly PropertyTag LocalCommitTimeMax = new PropertyTag(1728708672U);

		// Token: 0x0400060C RID: 1548
		internal static readonly PropertyTag DeletedCountTotal = new PropertyTag(1728774147U);

		// Token: 0x0400060D RID: 1549
		internal static readonly PropertyTag ICSChangeKey = new PropertyTag(1716846850U);

		// Token: 0x0400060E RID: 1550
		internal static readonly PropertyTag URLName = new PropertyTag(1728512031U);

		// Token: 0x0400060F RID: 1551
		internal static readonly PropertyTag HierRev = new PropertyTag(1082261568U);

		// Token: 0x04000610 RID: 1552
		internal static readonly PropertyTag Subfolders = new PropertyTag(906625035U);

		// Token: 0x04000611 RID: 1553
		internal static readonly PropertyTag CreationTime = new PropertyTag(805765184U);

		// Token: 0x04000612 RID: 1554
		internal static readonly PropertyTag FolderChildCount = new PropertyTag(1714946051U);

		// Token: 0x04000613 RID: 1555
		internal static readonly PropertyTag Rights = new PropertyTag(1715011587U);

		// Token: 0x04000614 RID: 1556
		internal static readonly PropertyTag AddressBookEntryId = new PropertyTag(1715142914U);

		// Token: 0x04000615 RID: 1557
		internal static readonly PropertyTag DisablePeruserRead = new PropertyTag(1724186635U);

		// Token: 0x04000616 RID: 1558
		internal static readonly PropertyTag SecureInSite = new PropertyTag(1721630731U);

		// Token: 0x04000617 RID: 1559
		internal static readonly PropertyTag PublicFolderPlatinumHomeMDB = new PropertyTag(1730019339U);

		// Token: 0x04000618 RID: 1560
		internal static readonly PropertyTag PublicFolderProxyRequired = new PropertyTag(1730084875U);

		// Token: 0x04000619 RID: 1561
		internal static readonly PropertyTag LongTermId = new PropertyTag(1733820674U);

		// Token: 0x0400061A RID: 1562
		internal static readonly PropertyTag InstanceIdBin = new PropertyTag(1739587842U);

		// Token: 0x0400061B RID: 1563
		internal static readonly PropertyTag LocalDirectoryEntryId = new PropertyTag(873857282U);

		// Token: 0x0400061C RID: 1564
		internal static readonly PropertyTag ConversationId = new PropertyTag(806551810U);

		// Token: 0x0400061D RID: 1565
		internal static readonly PropertyTag ChangeType = new PropertyTag(1733296130U);

		// Token: 0x0400061E RID: 1566
		internal static readonly PropertyTag MailboxOwnerName = new PropertyTag(1713111071U);

		// Token: 0x0400061F RID: 1567
		internal static readonly PropertyTag OOFState = new PropertyTag(1713176587U);

		// Token: 0x04000620 RID: 1568
		internal static readonly PropertyTag Anr = new PropertyTag(906756127U);

		// Token: 0x020001F9 RID: 505
		private sealed class PropertyIdComparerImpl : IEqualityComparer<PropertyTag>
		{
			// Token: 0x06000AE2 RID: 2786 RVA: 0x00022906 File Offset: 0x00020B06
			public bool Equals(PropertyTag x, PropertyTag y)
			{
				return x.PropertyId == y.PropertyId;
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00022918 File Offset: 0x00020B18
			public int GetHashCode(PropertyTag x)
			{
				return (int)x.PropertyId;
			}
		}
	}
}
