using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000017 RID: 23
	[DataContract]
	internal sealed class FolderRec
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00003C98 File Offset: 0x00001E98
		public FolderRec()
		{
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003CA0 File Offset: 0x00001EA0
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00003CA8 File Offset: 0x00001EA8
		[DataMember(IsRequired = true)]
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
			set
			{
				this.entryId = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003CB1 File Offset: 0x00001EB1
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00003CB9 File Offset: 0x00001EB9
		[DataMember(IsRequired = true)]
		public byte[] ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003CC2 File Offset: 0x00001EC2
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00003CCA File Offset: 0x00001ECA
		[DataMember(IsRequired = true)]
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
			set
			{
				this.folderName = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00003CD3 File Offset: 0x00001ED3
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00003CDB File Offset: 0x00001EDB
		[DataMember(IsRequired = true, Name = "FolderType")]
		public int FolderTypeValue
		{
			get
			{
				return this.folderType;
			}
			set
			{
				this.folderType = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003CE4 File Offset: 0x00001EE4
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00003CEC File Offset: 0x00001EEC
		[DataMember(EmitDefaultValue = false)]
		public string FolderClass
		{
			get
			{
				return this.folderClass;
			}
			set
			{
				this.folderClass = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00003CF5 File Offset: 0x00001EF5
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00003CFD File Offset: 0x00001EFD
		[DataMember(EmitDefaultValue = false)]
		public DateTime LastModifyTimestamp
		{
			get
			{
				return this.lastModifyTimestamp;
			}
			set
			{
				this.lastModifyTimestamp = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003D06 File Offset: 0x00001F06
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00003D0E File Offset: 0x00001F0E
		[DataMember(EmitDefaultValue = false)]
		public bool IsGhosted { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00003D17 File Offset: 0x00001F17
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00003D1F File Offset: 0x00001F1F
		[DataMember(EmitDefaultValue = false)]
		public PropValueData[] AdditionalProps
		{
			get
			{
				return this.additionalProps;
			}
			set
			{
				this.additionalProps = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003D28 File Offset: 0x00001F28
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00003D30 File Offset: 0x00001F30
		[DataMember(EmitDefaultValue = false, Name = "PromotedProperties")]
		public int[] PromotedPropertiesList
		{
			get
			{
				return this.promotedProperties;
			}
			set
			{
				this.promotedProperties = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00003D39 File Offset: 0x00001F39
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00003D41 File Offset: 0x00001F41
		[DataMember(EmitDefaultValue = false)]
		public SortOrderData[] Views
		{
			get
			{
				return this.views;
			}
			set
			{
				this.views = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003D4A File Offset: 0x00001F4A
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00003D52 File Offset: 0x00001F52
		[DataMember(EmitDefaultValue = false)]
		public ICSViewData[] ICSViews
		{
			get
			{
				return this.icsViews;
			}
			set
			{
				this.icsViews = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003D5B File Offset: 0x00001F5B
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00003D63 File Offset: 0x00001F63
		[DataMember(EmitDefaultValue = false)]
		public RestrictionData[] Restrictions
		{
			get
			{
				return this.restrictions;
			}
			set
			{
				this.restrictions = value;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003D6C File Offset: 0x00001F6C
		public FolderRec(byte[] entryId, byte[] parentId, FolderType folderType, string folderName, DateTime lastModifyTimestamp, PropValueData[] additionalProps) : this(entryId, parentId, folderType, null, folderName, lastModifyTimestamp, additionalProps)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003D80 File Offset: 0x00001F80
		public FolderRec(byte[] entryId, byte[] parentId, FolderType folderType, string folderClass, string folderName, DateTime lastModifyTimestamp, PropValueData[] additionalProps)
		{
			if (CommonUtils.IsSameEntryId(entryId, parentId))
			{
				parentId = null;
			}
			this.entryId = entryId;
			this.parentId = parentId;
			this.folderType = (int)folderType;
			this.folderClass = folderClass;
			this.folderName = folderName;
			this.lastModifyTimestamp = lastModifyTimestamp;
			this.additionalProps = additionalProps;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public FolderRec(FolderRec folderRec)
		{
			this.CopyFrom(folderRec);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003DE3 File Offset: 0x00001FE3
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00003DEB File Offset: 0x00001FEB
		public FolderType FolderType
		{
			get
			{
				return (FolderType)this.folderType;
			}
			set
			{
				this.folderType = (int)value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public DateTime LocalCommitTimeMax
		{
			get
			{
				object obj = this[PropTag.LocalCommitTimeMax];
				if (obj == null)
				{
					return DateTime.MinValue;
				}
				return (DateTime)obj;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00003E1C File Offset: 0x0000201C
		public int DeletedCountTotal
		{
			get
			{
				object obj = this[PropTag.DeletedCountTotal];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x170000BD RID: 189
		public object this[PropTag additionalPtag]
		{
			get
			{
				if (this.additionalProps != null)
				{
					foreach (PropValueData propValueData in this.additionalProps)
					{
						if (propValueData.PropTag == (int)additionalPtag)
						{
							return propValueData.Value;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003E84 File Offset: 0x00002084
		public static FolderRec Create(StoreSession storageSession, NativeStorePropertyDefinition[] definitions, object[] values)
		{
			PropValue[] array = new PropValue[definitions.Length];
			ICollection<uint> collection = PropertyTagCache.Cache.PropertyTagsFromPropertyDefinitions(storageSession, definitions);
			byte[] array2 = null;
			int num = 0;
			foreach (uint num2 in collection)
			{
				if (num2 == 268370178U)
				{
					array2 = storageSession.IdConverter.GetLongTermIdFromId(storageSession.IdConverter.GetFidFromId(StoreObjectId.FromProviderSpecificId((byte[])values[num])));
				}
				num++;
			}
			num = 0;
			foreach (uint num3 in collection)
			{
				object obj = values[num];
				PropTag propTag = (PropTag)num3;
				if (propTag == PropTag.LTID)
				{
					obj = array2;
				}
				if (obj == null)
				{
					propTag = propTag.ChangePropType(PropType.Null);
				}
				else if (obj is PropertyError)
				{
					propTag = propTag.ChangePropType(PropType.Error);
					obj = (int)((PropertyError)obj).PropertyErrorCode;
				}
				else if (obj is ExDateTime)
				{
					obj = (DateTime)((ExDateTime)obj);
				}
				array[num] = new PropValue(propTag, obj);
				num++;
			}
			return FolderRec.Create(storageSession, array);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003FE0 File Offset: 0x000021E0
		public static FolderRec Create(PropValue[] pva)
		{
			return FolderRec.Create(null, pva);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00003FEC File Offset: 0x000021EC
		public static FolderRec Create(StoreSession storageSession, PropValue[] pva)
		{
			byte[] array = null;
			byte[] array2 = null;
			FolderType folderType = FolderType.Generic;
			string text = null;
			DateTime dateTime = DateTime.MinValue;
			List<PropValueData> list = new List<PropValueData>();
			foreach (PropValue native in pva)
			{
				if (!native.IsNull() && !native.IsError())
				{
					PropTag propTag = native.PropTag;
					if (propTag <= PropTag.EntryId)
					{
						if (propTag == PropTag.ParentEntryId)
						{
							array2 = native.GetBytes();
							goto IL_CD;
						}
						if (propTag == PropTag.EntryId)
						{
							array = native.GetBytes();
							goto IL_CD;
						}
					}
					else
					{
						if (propTag == PropTag.DisplayName)
						{
							text = native.GetString();
							goto IL_CD;
						}
						if (propTag == PropTag.LastModificationTime)
						{
							dateTime = native.GetDateTime();
							goto IL_CD;
						}
						if (propTag == PropTag.FolderType)
						{
							folderType = (FolderType)native.GetInt();
							goto IL_CD;
						}
					}
					list.Add(DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(native));
				}
				IL_CD:;
			}
			if (array != null)
			{
				FolderRec folderRec = new FolderRec(array, array2, folderType, text, dateTime, (list.Count > 0) ? list.ToArray() : null);
				if (storageSession != null && folderRec[PropTag.ReplicaList] != null)
				{
					folderRec.IsGhosted = !CoreFolder.IsContentAvailable(storageSession, CoreFolder.GetContentMailboxInfo(ReplicaListProperty.GetStringArrayFromBytes((byte[])folderRec[PropTag.ReplicaList])));
				}
				return folderRec;
			}
			return null;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000413C File Offset: 0x0000233C
		public static FolderRec Create(MapiFolder folder, PropTag[] additionalPtagsToLoad)
		{
			PropTag[] array;
			if (additionalPtagsToLoad == null)
			{
				array = FolderRec.PtagsToLoad;
			}
			else
			{
				List<PropTag> list = new List<PropTag>();
				list.AddRange(FolderRec.PtagsToLoad);
				list.AddRange(additionalPtagsToLoad);
				array = list.ToArray();
			}
			PropValue[] props = folder.GetProps(array);
			byte[] array2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				PropTag propTag = array[i];
				PropTag propTag2 = propTag;
				if (propTag2 != PropTag.EntryId)
				{
					if (propTag2 == PropTag.LTID)
					{
						props[i] = new PropValue(PropTag.LTID, folder.MapiStore.GlobalIdFromId(folder.MapiStore.GetFidFromEntryId(array2)));
					}
				}
				else
				{
					array2 = (byte[])props[i].Value;
				}
			}
			return FolderRec.Create(props);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000041F6 File Offset: 0x000023F6
		public PropTag[] GetPromotedProperties()
		{
			return DataConverter<PropTagConverter, PropTag, int>.GetNative(this.promotedProperties);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004203 File Offset: 0x00002403
		public void SetPromotedProperties(PropTag[] properties)
		{
			this.promotedProperties = DataConverter<PropTagConverter, PropTag, int>.GetData(properties);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004214 File Offset: 0x00002414
		public void CopyFrom(FolderRec sourceRec)
		{
			this.entryId = sourceRec.EntryId;
			this.parentId = sourceRec.ParentId;
			this.folderName = sourceRec.FolderName;
			this.folderType = sourceRec.folderType;
			this.folderClass = sourceRec.FolderClass;
			this.lastModifyTimestamp = sourceRec.LastModifyTimestamp;
			this.IsGhosted = sourceRec.IsGhosted;
			this.additionalProps = sourceRec.AdditionalProps;
			this.promotedProperties = sourceRec.promotedProperties;
			this.views = sourceRec.Views;
			this.icsViews = sourceRec.ICSViews;
			this.restrictions = sourceRec.Restrictions;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000042B4 File Offset: 0x000024B4
		public override string ToString()
		{
			return string.Format("{0}: EntryID: {1}, ParentID: {2}, Type: {3}", new object[]
			{
				this.FolderName,
				TraceUtils.DumpEntryId(this.EntryId),
				TraceUtils.DumpEntryId(this.ParentId),
				this.FolderType
			});
		}

		// Token: 0x040000C5 RID: 197
		private byte[] entryId;

		// Token: 0x040000C6 RID: 198
		private byte[] parentId;

		// Token: 0x040000C7 RID: 199
		private string folderName;

		// Token: 0x040000C8 RID: 200
		private int folderType;

		// Token: 0x040000C9 RID: 201
		private string folderClass;

		// Token: 0x040000CA RID: 202
		private DateTime lastModifyTimestamp;

		// Token: 0x040000CB RID: 203
		private PropValueData[] additionalProps;

		// Token: 0x040000CC RID: 204
		private int[] promotedProperties;

		// Token: 0x040000CD RID: 205
		private SortOrderData[] views;

		// Token: 0x040000CE RID: 206
		private ICSViewData[] icsViews;

		// Token: 0x040000CF RID: 207
		private RestrictionData[] restrictions;

		// Token: 0x040000D0 RID: 208
		public static readonly PropTag[] PtagsToLoad = new PropTag[]
		{
			PropTag.EntryId,
			PropTag.ParentEntryId,
			PropTag.FolderType,
			PropTag.DisplayName,
			PropTag.LastModificationTime
		};
	}
}
