using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200007F RID: 127
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyFilterFactory : IPropertyFilterFactory
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0002273A File Offset: 0x0002093A
		public PropertyFilterFactory(bool isShallowCopy, bool isInclusion, PropertyTag[] propertyTags)
		{
			this.isShallowCopy = isShallowCopy;
			this.isInclusion = isInclusion;
			this.propertyTags = PropertyFilterFactory.ConvertPropertyTagsToUnicode(propertyTags);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0002275C File Offset: 0x0002095C
		private static PropertyTag[] ConvertPropertyTagsToUnicode(PropertyTag[] propertyTags)
		{
			bool flag = false;
			foreach (PropertyTag propertyTag in propertyTags)
			{
				if (propertyTag.ElementPropertyType == PropertyType.String8)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				PropertyTag[] array = new PropertyTag[propertyTags.Length];
				Array.Copy(propertyTags, array, propertyTags.Length);
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j].ElementPropertyType == PropertyType.String8)
					{
						array[j] = array[j].ChangeElementPropertyType(PropertyType.Unicode);
					}
				}
				return array;
			}
			return propertyTags;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000227F0 File Offset: 0x000209F0
		private static HashSet<PropertyTag> Combine(IEnumerable<PropertyTag> set1, IEnumerable<PropertyTag> set2)
		{
			HashSet<PropertyTag> hashSet = new HashSet<PropertyTag>(set1);
			hashSet.UnionWith(set2);
			return hashSet;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002280C File Offset: 0x00020A0C
		private IPropertyFilter EnsureCachedPropertyFilterCreated(ref IPropertyFilter filter, bool isTopLevel, HashSet<PropertyTag> staticIncludeProperties, HashSet<PropertyTag> staticExcludeProperties)
		{
			if (filter == null)
			{
				filter = new PropertyFilterFactory.PropertyFilter(isTopLevel && this.isInclusion, isTopLevel ? this.propertyTags : Array<PropertyTag>.Empty, staticIncludeProperties, staticExcludeProperties);
			}
			return filter;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0002283A File Offset: 0x00020A3A
		private IMessagePropertyFilter EnsureCachedMessagePropertyFilterCreated(ref IMessagePropertyFilter filter, bool isTopLevel, HashSet<PropertyTag> staticIncludeProperties, HashSet<PropertyTag> staticExcludeProperties)
		{
			if (filter == null)
			{
				filter = new PropertyFilterFactory.MessagePropertyFilter(this.isShallowCopy, isTopLevel && this.isInclusion, isTopLevel ? this.propertyTags : Array<PropertyTag>.Empty, staticIncludeProperties, staticExcludeProperties);
			}
			return filter;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0002286E File Offset: 0x00020A6E
		public IPropertyFilter GetAttachmentCopyToFilter(bool isTopLevel)
		{
			if (isTopLevel)
			{
				return this.EnsureCachedPropertyFilterCreated(ref this.attachmentCopyToFilterTopLevel, true, PropertyFilterFactory.IncludePropertiesForFxAttachmentCopyToCollection, PropertyFilterFactory.ExcludePropertiesForFxAttachmentCopyToCollection);
			}
			return this.EnsureCachedPropertyFilterCreated(ref this.attachmentCopyToFilter, false, PropertyFilterFactory.IncludePropertiesForFxAttachmentCopyToCollection, PropertyFilterFactory.ExcludePropertiesForFxAttachmentCopyToCollection);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000228A2 File Offset: 0x00020AA2
		public IMessagePropertyFilter GetMessageCopyToFilter(bool isTopLevel)
		{
			if (isTopLevel)
			{
				return this.EnsureCachedMessagePropertyFilterCreated(ref this.messageCopyToFilterTopLevel, true, PropertyFilterFactory.IncludePropertiesForFxMessageCopyToCollection, PropertyFilterFactory.ExcludePropertiesForFxMessageCopyToCollection);
			}
			return this.EnsureCachedMessagePropertyFilterCreated(ref this.messageCopyToFilter, false, PropertyFilterFactory.IncludePropertiesForFxMessageCopyToCollection, PropertyFilterFactory.ExcludePropertiesForFxMessageCopyToCollection);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000228D6 File Offset: 0x00020AD6
		public IPropertyFilter GetFolderCopyToFilter()
		{
			return this.EnsureCachedPropertyFilterCreated(ref this.folderCopyToFilter, true, PropertyFilterFactory.IncludePropertiesForFxFolderCopyToCollection, PropertyFilterFactory.ExcludePropertiesForFxFolderCopyToCollection);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000228EF File Offset: 0x00020AEF
		public IPropertyFilter GetCopyFolderFilter()
		{
			return this.EnsureCachedPropertyFilterCreated(ref this.copyFolderFilter, true, PropertyFilterFactory.IncludePropertiesForFxCopyFolderCollection, PropertyFilterFactory.ExcludePropertiesForFxCopyFolderCollection);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00022908 File Offset: 0x00020B08
		public IPropertyFilter GetCopySubfolderFilter()
		{
			return this.EnsureCachedPropertyFilterCreated(ref this.subfolderFilter, false, PropertyFilterFactory.IncludePropertiesForFxSubfolderCopyCollection, PropertyFilterFactory.ExcludePropertiesForFxSubfolderCopyCollection);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00022921 File Offset: 0x00020B21
		public IPropertyFilter GetAttachmentFilter(bool isTopLevel)
		{
			return PropertyFilterFactory.AttachmentDownloadPropertyFilter;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00022928 File Offset: 0x00020B28
		public IPropertyFilter GetEmbeddedMessageFilter(bool isTopLevel)
		{
			return PropertyFilterFactory.AttachmentEmbeddedMessagePropertyFilter;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002292F File Offset: 0x00020B2F
		public IPropertyFilter GetMessageFilter(bool isTopLevel)
		{
			return this.EnsureCachedPropertyFilterCreated(ref this.messageDownloadFilter, isTopLevel, PropertyFilterFactory.IncludePropertiesForFxMessageDownloadCollection, PropertyFilterFactory.ExcludePropertiesForFxMessageDownloadCollection);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00022948 File Offset: 0x00020B48
		public IPropertyFilter GetRecipientFilter()
		{
			return PropertyFilterFactory.RecipientPropertyFilter;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002294F File Offset: 0x00020B4F
		public IPropertyFilter GetIcsHierarchyFilter(bool includeFid, bool includeChangeNumber)
		{
			if (includeFid && includeChangeNumber)
			{
				return IncludeAllPropertyFilter.Instance;
			}
			if (includeFid && !includeChangeNumber)
			{
				return PropertyFilterFactory.IcsHierarchyPropertyFilterOnlyChangeNumber;
			}
			if (!includeFid && includeChangeNumber)
			{
				return PropertyFilterFactory.IcsHierarchyPropertyFilterOnlyFid;
			}
			return PropertyFilterFactory.IcsHierarchyPropertyFilter;
		}

		// Token: 0x040001DF RID: 479
		internal static readonly PropertyTag[] CommonIncludePropertiesForFxCopyTo = new PropertyTag[]
		{
			PropertyTag.MessageFlags,
			PropertyTag.OriginalDisplayBcc,
			PropertyTag.OriginalDisplayCc,
			PropertyTag.OriginalDisplayTo,
			PropertyTag.MessageDeliveryTime,
			PropertyTag.NormalizedSubject
		};

		// Token: 0x040001E0 RID: 480
		internal static readonly PropertyTag[] CommonExcludePropertiesForFxCopyTo = new PropertyTag[]
		{
			PropertyTag.Body,
			PropertyTag.RtfSyncBodyCRC,
			PropertyTag.RtfSyncBodyCount,
			PropertyTag.RtfSyncBodyTag,
			PropertyTag.RtfSyncPrefixCount,
			PropertyTag.RtfSyncTrailingCount
		};

		// Token: 0x040001E1 RID: 481
		internal static readonly PropertyTag[] IncludePropertiesForFxAttachmentCopyTo = new PropertyTag[0];

		// Token: 0x040001E2 RID: 482
		internal static readonly PropertyTag[] ExcludePropertiesForFxAttachmentCopyTo = new PropertyTag[0];

		// Token: 0x040001E3 RID: 483
		internal static readonly PropertyTag[] IncludePropertiesForFxMessageCopyTo = new PropertyTag[]
		{
			PropertyTag.Body,
			PropertyTag.SentMailServerId
		};

		// Token: 0x040001E4 RID: 484
		internal static readonly PropertyTag[] ExcludePropertiesForFxMessageCommon = new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.ParentEntryId,
			PropertyTag.DisplayBcc,
			PropertyTag.DisplayCc,
			PropertyTag.DisplayTo,
			PropertyTag.HasAttach,
			PropertyTag.Access,
			PropertyTag.Mid,
			PropertyTag.Associated,
			PropertyTag.MessageSize,
			PropertyTag.MimeSize,
			PropertyTag.FileSize,
			PropertyTag.InternetNewsGroups,
			PropertyTag.ImapCachedBodystructure,
			PropertyTag.ImapCachedEnvelope,
			PropertyTag.LocalCommitTime,
			PropertyTag.AutoReset,
			PropertyTag.DeletedOn,
			PropertyTag.SMTPTempTblData,
			PropertyTag.SMTPTempTblData2,
			PropertyTag.SMTPTempTblData3,
			PropertyTag.MimeSkeleton
		};

		// Token: 0x040001E5 RID: 485
		internal static readonly PropertyTag[] IncludePropertiesForFxFolderCopy = Array<PropertyTag>.Empty;

		// Token: 0x040001E6 RID: 486
		internal static readonly PropertyTag[] ExcludePropertiesForFxFolderCopy = Array<PropertyTag>.Empty;

		// Token: 0x040001E7 RID: 487
		internal static readonly PropertyTag[] IncludePropertiesForFxSubFolderCopy = Array<PropertyTag>.Empty;

		// Token: 0x040001E8 RID: 488
		internal static readonly PropertyTag[] ExcludePropertiesForFxSubFolderCopy = new PropertyTag[]
		{
			PropertyTag.Comment,
			PropertyTag.DisplayName
		};

		// Token: 0x040001E9 RID: 489
		internal static readonly PropertyTag[] IncludePropertiesForFxCopyFolder = new PropertyTag[]
		{
			PropertyTag.PublicFolderPlatinumHomeMDB,
			PropertyTag.PublicFolderProxyRequired,
			PropertyTag.FolderChildCount,
			PropertyTag.Rights,
			PropertyTag.AddressBookEntryId,
			PropertyTag.DisablePeruserRead,
			PropertyTag.SecureInSite
		};

		// Token: 0x040001EA RID: 490
		internal static readonly PropertyTag[] ExcludePropertiesForFxCopyFolder = new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.ContentCount,
			PropertyTag.ContentUnread,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.FolderType,
			PropertyTag.ParentEntryId,
			PropertyTag.IsNewsgroupAnchor,
			PropertyTag.IsNewsgroup,
			PropertyTag.NewsgroupComp,
			PropertyTag.InetNewsgroupName,
			PropertyTag.NewsfeedAcl,
			PropertyTag.Fid,
			PropertyTag.ParentFid,
			PropertyTag.DisplayName,
			PropertyTag.DeletedMsgCt,
			PropertyTag.DeletedAssocMsgCt,
			PropertyTag.DeletedFolderCt,
			PropertyTag.DeletedMessageSizeExtended,
			PropertyTag.DeletedAssocMessageSizeExtended,
			PropertyTag.DeletedNormalMessageSizeExtended,
			PropertyTag.DeletedOn,
			PropertyTag.LocalCommitTime,
			PropertyTag.LocalCommitTimeMax,
			PropertyTag.DeletedCountTotal,
			PropertyTag.ICSChangeKey,
			PropertyTag.URLName,
			PropertyTag.HierRev,
			PropertyTag.CreationTime,
			PropertyTag.LastModificationTime,
			PropertyTag.Subfolders,
			PropertyTag.SourceKey,
			PropertyTag.ParentSourceKey,
			PropertyTag.ChangeKey,
			PropertyTag.PredecessorChangeList
		};

		// Token: 0x040001EB RID: 491
		internal static readonly PropertyTag[] IncludePropertiesForFxFolderCopyTo = Array<PropertyTag>.Empty;

		// Token: 0x040001EC RID: 492
		internal static readonly PropertyTag[] ExcludePropertiesForFxFolderCopyTo = Array<PropertyTag>.Empty;

		// Token: 0x040001ED RID: 493
		internal static readonly PropertyTag[] ExcludePropertiesForAttachmentDownload = new PropertyTag[]
		{
			PropertyTag.AttachmentNumber,
			PropertyTag.ObjectType
		};

		// Token: 0x040001EE RID: 494
		internal static readonly PropertyTag[] ExcludePropertiesForAttachmentEmbeddedMessage = new PropertyTag[]
		{
			PropertyTag.AttachmentNumber,
			PropertyTag.ObjectType,
			PropertyTag.AttachmentDataObject
		};

		// Token: 0x040001EF RID: 495
		internal static readonly PropertyTag[] ExcludePropertiesRecipient = new PropertyTag[]
		{
			PropertyTag.RowId
		};

		// Token: 0x040001F0 RID: 496
		internal static readonly PropertyTag[] ExcludePropertiesIcsHierarchy = new PropertyTag[]
		{
			PropertyTag.Fid,
			PropertyTag.ChangeNumber
		};

		// Token: 0x040001F1 RID: 497
		internal static readonly PropertyTag[] ExcludeOnlyFidIcsHierarchy = new PropertyTag[]
		{
			PropertyTag.Fid
		};

		// Token: 0x040001F2 RID: 498
		internal static readonly PropertyTag[] ExcludeOnlyChangeNumberIcsHierarchy = new PropertyTag[]
		{
			PropertyTag.ChangeNumber
		};

		// Token: 0x040001F3 RID: 499
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxAttachmentCopyToCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.CommonIncludePropertiesForFxCopyTo, PropertyFilterFactory.IncludePropertiesForFxAttachmentCopyTo);

		// Token: 0x040001F4 RID: 500
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxAttachmentCopyToCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.CommonExcludePropertiesForFxCopyTo, PropertyFilterFactory.ExcludePropertiesForFxAttachmentCopyTo);

		// Token: 0x040001F5 RID: 501
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxMessageCopyToCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.CommonIncludePropertiesForFxCopyTo, PropertyFilterFactory.IncludePropertiesForFxMessageCopyTo);

		// Token: 0x040001F6 RID: 502
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxMessageCopyToCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.CommonExcludePropertiesForFxCopyTo, PropertyFilterFactory.ExcludePropertiesForFxMessageCommon);

		// Token: 0x040001F7 RID: 503
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxMessageDownloadCollection = new HashSet<PropertyTag>();

		// Token: 0x040001F8 RID: 504
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxMessageDownloadCollection = new HashSet<PropertyTag>(PropertyFilterFactory.ExcludePropertiesForFxMessageCommon);

		// Token: 0x040001F9 RID: 505
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxFolderCopyCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.CommonIncludePropertiesForFxCopyTo, PropertyFilterFactory.IncludePropertiesForFxFolderCopy);

		// Token: 0x040001FA RID: 506
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxFolderCopyCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.CommonExcludePropertiesForFxCopyTo, PropertyFilterFactory.ExcludePropertiesForFxFolderCopy);

		// Token: 0x040001FB RID: 507
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxSubfolderCopyCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.IncludePropertiesForFxFolderCopyCollection, PropertyFilterFactory.IncludePropertiesForFxSubFolderCopy);

		// Token: 0x040001FC RID: 508
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxSubfolderCopyCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.ExcludePropertiesForFxFolderCopyCollection, PropertyFilterFactory.ExcludePropertiesForFxSubFolderCopy);

		// Token: 0x040001FD RID: 509
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxFolderCopyToCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.IncludePropertiesForFxFolderCopyCollection, PropertyFilterFactory.IncludePropertiesForFxFolderCopyTo);

		// Token: 0x040001FE RID: 510
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxFolderCopyToCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.ExcludePropertiesForFxFolderCopyCollection, PropertyFilterFactory.ExcludePropertiesForFxFolderCopyTo);

		// Token: 0x040001FF RID: 511
		private static readonly HashSet<PropertyTag> IncludePropertiesForFxCopyFolderCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.IncludePropertiesForFxFolderCopyCollection, PropertyFilterFactory.IncludePropertiesForFxCopyFolder);

		// Token: 0x04000200 RID: 512
		private static readonly HashSet<PropertyTag> ExcludePropertiesForFxCopyFolderCollection = PropertyFilterFactory.Combine(PropertyFilterFactory.ExcludePropertiesForFxFolderCopyCollection, PropertyFilterFactory.ExcludePropertiesForFxCopyFolder);

		// Token: 0x04000201 RID: 513
		private static readonly IPropertyFilter AttachmentDownloadPropertyFilter = new ExcludingPropertyFilter(PropertyFilterFactory.ExcludePropertiesForAttachmentDownload);

		// Token: 0x04000202 RID: 514
		private static readonly IPropertyFilter AttachmentEmbeddedMessagePropertyFilter = new ExcludingPropertyFilter(PropertyFilterFactory.ExcludePropertiesForAttachmentEmbeddedMessage);

		// Token: 0x04000203 RID: 515
		private static readonly IPropertyFilter RecipientPropertyFilter = new ExcludingPropertyFilter(PropertyFilterFactory.ExcludePropertiesRecipient);

		// Token: 0x04000204 RID: 516
		private static readonly IPropertyFilter IcsHierarchyPropertyFilter = new ExcludingPropertyFilter(PropertyFilterFactory.ExcludePropertiesIcsHierarchy);

		// Token: 0x04000205 RID: 517
		private static readonly IPropertyFilter IcsHierarchyPropertyFilterOnlyFid = new ExcludingPropertyFilter(PropertyFilterFactory.ExcludeOnlyFidIcsHierarchy);

		// Token: 0x04000206 RID: 518
		private static readonly IPropertyFilter IcsHierarchyPropertyFilterOnlyChangeNumber = new ExcludingPropertyFilter(PropertyFilterFactory.ExcludeOnlyChangeNumberIcsHierarchy);

		// Token: 0x04000207 RID: 519
		public static readonly PropertyFilterFactory IncludeAllFactory = new PropertyFilterFactory(false, false, Array<PropertyTag>.Empty);

		// Token: 0x04000208 RID: 520
		private readonly bool isShallowCopy;

		// Token: 0x04000209 RID: 521
		private readonly bool isInclusion;

		// Token: 0x0400020A RID: 522
		private readonly PropertyTag[] propertyTags;

		// Token: 0x0400020B RID: 523
		private IPropertyFilter attachmentCopyToFilterTopLevel;

		// Token: 0x0400020C RID: 524
		private IPropertyFilter attachmentCopyToFilter;

		// Token: 0x0400020D RID: 525
		private IMessagePropertyFilter messageCopyToFilterTopLevel;

		// Token: 0x0400020E RID: 526
		private IMessagePropertyFilter messageCopyToFilter;

		// Token: 0x0400020F RID: 527
		private IPropertyFilter folderCopyToFilter;

		// Token: 0x04000210 RID: 528
		private IPropertyFilter copyFolderFilter;

		// Token: 0x04000211 RID: 529
		private IPropertyFilter subfolderFilter;

		// Token: 0x04000212 RID: 530
		private IPropertyFilter messageDownloadFilter;

		// Token: 0x02000080 RID: 128
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class PropertyFilter : IPropertyFilter
		{
			// Token: 0x060004ED RID: 1261 RVA: 0x000232C8 File Offset: 0x000214C8
			public PropertyFilter(bool isInclusion, ICollection<PropertyTag> clientPropertyTags, ICollection<PropertyTag> staticIncludedPropertyTags, ICollection<PropertyTag> staticExcludedPropertyTags)
			{
				this.isInclusion = isInclusion;
				this.clientPropertyTags = clientPropertyTags;
				this.staticIncludedPropertyTags = staticIncludedPropertyTags;
				this.staticExcludedPropertyTags = staticExcludedPropertyTags;
				List<PropertyId> list = null;
				foreach (PropertyTag propertyTag in clientPropertyTags)
				{
					if (propertyTag.PropertyType == PropertyType.Unspecified)
					{
						if (list == null)
						{
							list = new List<PropertyId>();
						}
						list.Add(propertyTag.PropertyId);
					}
				}
				if (list != null)
				{
					this.clientPropertyIds = list;
					return;
				}
				this.clientPropertyIds = Array<PropertyId>.Empty;
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060004EE RID: 1262 RVA: 0x00023364 File Offset: 0x00021564
			protected bool IsInclusionList
			{
				get
				{
					return this.isInclusion;
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x0002336C File Offset: 0x0002156C
			protected ICollection<PropertyTag> ClientProperties
			{
				get
				{
					return this.clientPropertyTags;
				}
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x00023374 File Offset: 0x00021574
			public bool IncludeProperty(PropertyTag propertyTag)
			{
				if (this.isInclusion)
				{
					if (!this.clientPropertyTags.Contains(propertyTag) && !this.clientPropertyIds.Contains(propertyTag.PropertyId))
					{
						return false;
					}
					if (this.staticIncludedPropertyTags.Contains(propertyTag))
					{
						return true;
					}
				}
				else
				{
					if (this.clientPropertyTags.Contains(propertyTag) || this.clientPropertyIds.Contains(propertyTag.PropertyId))
					{
						return false;
					}
					if (propertyTag.IsStringProperty)
					{
						propertyTag = propertyTag.ChangeElementPropertyType(PropertyType.Unicode);
					}
					if (this.staticIncludedPropertyTags.Contains(propertyTag))
					{
						return true;
					}
					if (this.staticExcludedPropertyTags.Contains(propertyTag))
					{
						return false;
					}
				}
				return !propertyTag.IsProviderDefinedNonTransmittable;
			}

			// Token: 0x04000213 RID: 531
			private readonly bool isInclusion;

			// Token: 0x04000214 RID: 532
			private readonly ICollection<PropertyTag> clientPropertyTags;

			// Token: 0x04000215 RID: 533
			private readonly ICollection<PropertyId> clientPropertyIds;

			// Token: 0x04000216 RID: 534
			private readonly ICollection<PropertyTag> staticIncludedPropertyTags;

			// Token: 0x04000217 RID: 535
			private readonly ICollection<PropertyTag> staticExcludedPropertyTags;
		}

		// Token: 0x02000081 RID: 129
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class MessagePropertyFilter : PropertyFilterFactory.PropertyFilter, IMessagePropertyFilter, IPropertyFilter
		{
			// Token: 0x060004F1 RID: 1265 RVA: 0x0002341F File Offset: 0x0002161F
			public MessagePropertyFilter(bool isShallowCopy, bool isInclusion, ICollection<PropertyTag> clientPropertyTags, ICollection<PropertyTag> staticIncludedPropertyTags, ICollection<PropertyTag> staticExcludedPropertyTags) : base(isInclusion, clientPropertyTags, staticIncludedPropertyTags, staticExcludedPropertyTags)
			{
				this.isShallowCopy = isShallowCopy;
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00023434 File Offset: 0x00021634
			public bool IncludeRecipients
			{
				get
				{
					if (!base.IsInclusionList)
					{
						return !base.ClientProperties.Contains(PropertyTag.MessageRecipients);
					}
					return !this.isShallowCopy && base.ClientProperties.Contains(PropertyTag.MessageRecipients);
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0002346C File Offset: 0x0002166C
			public bool IncludeAttachments
			{
				get
				{
					if (!base.IsInclusionList)
					{
						return !base.ClientProperties.Contains(PropertyTag.MessageAttachments);
					}
					return !this.isShallowCopy && base.ClientProperties.Contains(PropertyTag.MessageAttachments);
				}
			}

			// Token: 0x04000218 RID: 536
			private readonly bool isShallowCopy;
		}
	}
}
