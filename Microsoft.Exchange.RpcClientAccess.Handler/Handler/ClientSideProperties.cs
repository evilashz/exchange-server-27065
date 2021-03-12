using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ClientSideProperties : IEnumerable<PropertyTag>, IEnumerable
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003D64 File Offset: 0x00001F64
		internal ClientSideProperties(PropertyTag[] clientSidePropertyTags, IEnumerable<PropertyId> excludeFromGetPropertyIds)
		{
			this.clientSidePropertyTags = new Dictionary<PropertyId, PropertyTag>(clientSidePropertyTags.Length, PropertyIdComparer.Instance);
			foreach (PropertyTag value in clientSidePropertyTags)
			{
				this.clientSidePropertyTags.Add(value.PropertyId, value);
			}
			this.excludeFromGetPropertyCallsSet = new HashSet<PropertyId>(PropertyIdComparer.Instance);
			foreach (PropertyId item in excludeFromGetPropertyIds)
			{
				this.excludeFromGetPropertyCallsSet.Add(item);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003E10 File Offset: 0x00002010
		internal static ClientSideProperties LogonInstance
		{
			get
			{
				return ClientSideProperties.instanceLogon;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003E17 File Offset: 0x00002017
		internal static ClientSideProperties MessageInstance
		{
			get
			{
				return ClientSideProperties.instanceMessage;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003E1E File Offset: 0x0000201E
		internal static ClientSideProperties FolderInstance
		{
			get
			{
				return ClientSideProperties.instanceFolder;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003E25 File Offset: 0x00002025
		internal static ClientSideProperties AttachmentInstance
		{
			get
			{
				return ClientSideProperties.instanceAttachment;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003E2C File Offset: 0x0000202C
		internal static ClientSideProperties ContentViewInstance
		{
			get
			{
				return ClientSideProperties.instanceContentView;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003E33 File Offset: 0x00002033
		internal static ClientSideProperties HierarchyViewInstance
		{
			get
			{
				return ClientSideProperties.instanceHierarchyView;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003E3A File Offset: 0x0000203A
		internal static ClientSideProperties DeepHierarchyViewInstance
		{
			get
			{
				return ClientSideProperties.instanceDeepHierarchyView;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003E41 File Offset: 0x00002041
		internal static ClientSideProperties RecipientInstance
		{
			get
			{
				return ClientSideProperties.instanceRecipient;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003E48 File Offset: 0x00002048
		internal static ClientSideProperties EmptyInstance
		{
			get
			{
				return ClientSideProperties.instanceEmpty;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E4F File Offset: 0x0000204F
		IEnumerator<PropertyTag> IEnumerable<PropertyTag>.GetEnumerator()
		{
			return this.clientSidePropertyTags.Values.GetEnumerator();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003E66 File Offset: 0x00002066
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<PropertyTag>)this).GetEnumerator();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003E6E File Offset: 0x0000206E
		internal bool Contains(PropertyId propertyId)
		{
			return this.clientSidePropertyTags.ContainsKey(propertyId);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003E7C File Offset: 0x0000207C
		internal bool ExcludeFromGetPropertyList(PropertyId propertyId)
		{
			return this.clientSidePropertyTags.ContainsKey(propertyId) || this.excludeFromGetPropertyCallsSet.Contains(propertyId);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003E9C File Offset: 0x0000209C
		internal bool ShouldBeReturnedIfRequested(PropertyId propertyId)
		{
			return !this.Contains(propertyId) || propertyId == PropertyTag.Subject.PropertyId;
		}

		// Token: 0x0400003C RID: 60
		private static readonly ClientSideProperties instanceLogon = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.StoreEntryId,
			PropertyTag.EntryId,
			PropertyTag.SearchKey,
			PropertyTag.RecordKey,
			PropertyTag.StoreRecordKey,
			PropertyTag.StoreSupportMask,
			PropertyTag.ObjectType,
			PropertyTag.ValidFolderMask,
			PropertyTag.AccessLevel,
			PropertyTag.CodePageId,
			PropertyTag.LocaleId,
			PropertyTag.SortLocaleId,
			PropertyTag.IPMSubtreeFolder,
			PropertyTag.IPMOutboxFolder,
			PropertyTag.IPMSentmailFolder,
			PropertyTag.IPMWastebasketFolder,
			PropertyTag.IPMFinderFolder,
			PropertyTag.IPMShortcutsFolder,
			PropertyTag.IPMViewsFolder,
			PropertyTag.IPMCommonViewsFolder,
			PropertyTag.IPMDafFolder,
			PropertyTag.NonIPMSubtreeFolder,
			PropertyTag.EformsRegistryFolder,
			PropertyTag.SplusFreeBusyFolder,
			PropertyTag.OfflineAddrBookFolder,
			PropertyTag.ArticleIndexFolder,
			PropertyTag.LocaleEformsRegistryFolder,
			PropertyTag.LocalSiteFreeBusyFolder,
			PropertyTag.LocalSiteAddrBookFolder,
			PropertyTag.MTSInFolder,
			PropertyTag.MTSOutFolder,
			PropertyTag.ScheduleFolder,
			PropertyTag.MdbProvider,
			PropertyTag.StoreState,
			PropertyTag.HierarchyServer,
			PropertyTag.LogonRightsOnMailbox
		}, Array<PropertyId>.Empty);

		// Token: 0x0400003D RID: 61
		private static readonly ClientSideProperties instanceMessage = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.StoreSupportMask,
			PropertyTag.MappingSignature,
			PropertyTag.MdbProvider,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.LongTermEntryIdFromTable,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.InstanceKey,
			PropertyTag.ParentEntryId,
			PropertyTag.ReplicaServer,
			PropertyTag.ReplicaVersion,
			PropertyTag.Subject
		}, new PropertyId[]
		{
			PropertyTag.NormalizedSubject.PropertyId,
			PropertyTag.SubjectPrefix.PropertyId,
			PropertyTag.ParentFid.PropertyId,
			PropertyTag.PreviewUnread.PropertyId
		});

		// Token: 0x0400003E RID: 62
		private static readonly ClientSideProperties instanceFolder = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.AccessLevel,
			PropertyTag.ObjectType,
			PropertyTag.StoreSupportMask,
			PropertyTag.MappingSignature,
			PropertyTag.DisplayType,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.SearchKey,
			PropertyTag.MdbProvider,
			PropertyTag.ReplicaServer,
			PropertyTag.ReplicaVersion,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.OfflineFlags
		}, Array<PropertyId>.Empty);

		// Token: 0x0400003F RID: 63
		private static readonly ClientSideProperties instanceAttachment = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.MappingSignature,
			PropertyTag.InstanceKey,
			PropertyTag.SentMailEntryId,
			PropertyTag.DamOrgMsgEntryId,
			PropertyTag.RuleFolderEntryId
		}, Array<PropertyId>.Empty);

		// Token: 0x04000040 RID: 64
		private static readonly ClientSideProperties instanceContentView = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.StoreSupportMask,
			PropertyTag.MappingSignature,
			PropertyTag.MdbProvider,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.LongTermEntryIdFromTable,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.InstanceKey,
			PropertyTag.ParentEntryId,
			PropertyTag.ReplicaServer,
			PropertyTag.ReplicaVersion
		}, new PropertyId[]
		{
			PropertyTag.NormalizedSubject.PropertyId,
			PropertyTag.SubjectPrefix.PropertyId
		});

		// Token: 0x04000041 RID: 65
		private static readonly ClientSideProperties instanceHierarchyView = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.StoreSupportMask,
			PropertyTag.MappingSignature,
			PropertyTag.DisplayType,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.SearchKey,
			PropertyTag.MdbProvider,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.Depth,
			PropertyTag.LongTermEntryIdFromTable,
			PropertyTag.InstanceKey,
			PropertyTag.Access
		}, Array<PropertyId>.Empty);

		// Token: 0x04000042 RID: 66
		private static readonly ClientSideProperties instanceDeepHierarchyView = new ClientSideProperties(new PropertyTag[]
		{
			PropertyTag.ObjectType,
			PropertyTag.StoreSupportMask,
			PropertyTag.MappingSignature,
			PropertyTag.DisplayType,
			PropertyTag.EntryId,
			PropertyTag.RecordKey,
			PropertyTag.SearchKey,
			PropertyTag.MdbProvider,
			PropertyTag.StoreEntryId,
			PropertyTag.StoreRecordKey,
			PropertyTag.LongTermEntryIdFromTable,
			PropertyTag.InstanceKey,
			PropertyTag.Access
		}, Array<PropertyId>.Empty);

		// Token: 0x04000043 RID: 67
		private static readonly ClientSideProperties instanceRecipient = new ClientSideProperties(Array<PropertyTag>.Empty, Array<PropertyId>.Empty);

		// Token: 0x04000044 RID: 68
		private static readonly ClientSideProperties instanceEmpty = new ClientSideProperties(Array<PropertyTag>.Empty, Array<PropertyId>.Empty);

		// Token: 0x04000045 RID: 69
		private readonly Dictionary<PropertyId, PropertyTag> clientSidePropertyTags;

		// Token: 0x04000046 RID: 70
		private readonly HashSet<PropertyId> excludeFromGetPropertyCallsSet;
	}
}
