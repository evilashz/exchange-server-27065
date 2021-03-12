using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200006D RID: 109
	internal sealed class Synchronizer
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001BE97 File Offset: 0x0001A097
		private Dictionary<StoreObjectId, FolderTagData> folderMapping
		{
			get
			{
				if (this.folderTagDict == null)
				{
					this.folderTagDict = new Dictionary<StoreObjectId, FolderTagData>();
				}
				return this.folderTagDict;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001BEB2 File Offset: 0x0001A0B2
		public Synchronizer(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcAssistant, bool fullCrawl)
		{
			this.mailboxDataForTags = mailboxDataForTags;
			this.elcAssistant = elcAssistant;
			this.fullCrawl = fullCrawl;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001BECF File Offset: 0x0001A0CF
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + this.mailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by Synchronizer.";
			}
			return this.toString;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001BF09 File Offset: 0x0001A109
		public void Invoke()
		{
			this.UpdateEntireMailbox();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001BF14 File Offset: 0x0001A114
		private void UpdateEntireMailbox()
		{
			using (IEnumerator<List<object[]>> folderHierarchy = FolderProcessor.GetFolderHierarchy(DefaultFolderType.Root, this.mailboxDataForTags.MailboxSession, Synchronizer.FolderDataColumns))
			{
				while (folderHierarchy != null && folderHierarchy.MoveNext())
				{
					List<object[]> list = folderHierarchy.Current;
					if (list != null)
					{
						this.ProcessFolder(list);
					}
				}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001BF74 File Offset: 0x0001A174
		private void ProcessFolder(List<object[]> folderList)
		{
			for (int i = 0; i < folderList.Count; i++)
			{
				try
				{
					if (!this.fullCrawl)
					{
						StoreObjectId storeObjectId = folderList[i][5] as StoreObjectId;
						VersionedId versionedId = folderList[i][0] as VersionedId;
						int flags = 0;
						if (folderList[i][3] is int)
						{
							flags = (int)folderList[i][3];
						}
						bool flag = false;
						Guid value = ElcMailboxHelper.GetGuidFromBytes(folderList[i][7], new Guid?(Guid.Empty), true, folderList[i][1]).Value;
						Guid value2 = ElcMailboxHelper.GetGuidFromBytes(folderList[i][2], new Guid?(Guid.Empty), true, folderList[i][1]).Value;
						DefaultFolderType defaultFolderType = this.mailboxDataForTags.MailboxSession.IsDefaultFolderType(versionedId.ObjectId);
						bool flag2 = defaultFolderType == DefaultFolderType.Calendar || defaultFolderType == DefaultFolderType.Tasks || defaultFolderType == DefaultFolderType.DeletedItems || defaultFolderType == DefaultFolderType.SentItems || defaultFolderType == DefaultFolderType.JunkEmail || defaultFolderType == DefaultFolderType.Drafts;
						if (!flag2 && folderList[i][11] is string)
						{
							flag2 = (ObjectClass.IsCalendarFolder((string)folderList[i][11]) || ObjectClass.IsTaskFolder((string)folderList[i][11]));
						}
						if (flag2)
						{
							foreach (AdTagData adTagData in this.mailboxDataForTags.ElcUserTagInformation.GetPolicyTagsList())
							{
								if (adTagData.Tag.Type == ElcFolderType.Calendar || adTagData.Tag.Type == ElcFolderType.Tasks || adTagData.Tag.Type == ElcFolderType.DeletedItems || adTagData.Tag.Type == ElcFolderType.SentItems || adTagData.Tag.Type == ElcFolderType.JunkEmail || adTagData.Tag.Type == ElcFolderType.All || adTagData.Tag.Type == ElcFolderType.Drafts)
								{
									flag = true;
								}
							}
							if (value != Guid.Empty || value2 != Guid.Empty)
							{
								flag = true;
							}
						}
						if ((flag || FlagsMan.DoesFolderNeedRescan(flags)) && !this.folderMapping.ContainsKey(versionedId.ObjectId))
						{
							FolderTagData folderTagData = new FolderTagData();
							folderTagData.ArchiveGuid = value;
							folderTagData.RetentionGuid = value2;
							this.folderMapping.Add(versionedId.ObjectId, folderTagData);
						}
						if (storeObjectId != null && this.folderMapping.ContainsKey(storeObjectId) && !this.folderMapping.ContainsKey(versionedId.ObjectId))
						{
							this.folderMapping.Add(versionedId.ObjectId, this.folderMapping[storeObjectId]);
						}
						if (versionedId != null && !this.folderMapping.ContainsKey(versionedId.ObjectId))
						{
							goto IL_3A5;
						}
					}
					FolderPropertySynchronizer folderPropertySynchronizer = new FolderPropertySynchronizer(this.mailboxDataForTags, this.elcAssistant, this.parentFolderIdToPropertyMap, folderList, i);
					folderPropertySynchronizer.Update();
				}
				catch (ArgumentOutOfRangeException ex)
				{
					string text = string.Format("{0} Corrupted Data. Skip current folder {1} at index {2}. FolderList count is {3} and folder properties length is {4}. Exception: {5}", new object[]
					{
						this,
						folderList[i][1],
						i,
						folderList.Count,
						folderList[i].Length,
						ex
					});
					Synchronizer.Tracer.TraceDebug((long)this.GetHashCode(), text);
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_MRMSkippingFolder, null, new object[]
					{
						folderList[i][1],
						text
					});
				}
				catch (ObjectNotFoundException ex2)
				{
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_MRMSkippingFolder, null, new object[]
					{
						folderList[i][1],
						ex2.ToString()
					});
				}
				IL_3A5:;
			}
			Synchronizer.Tracer.TraceDebug<Synchronizer>((long)this.GetHashCode(), "{0}: Start update parentFolderIdToPropertyMap", this);
			Dictionary<StoreObjectId, Synchronizer.FolderPropertySet> dictionary = new Dictionary<StoreObjectId, Synchronizer.FolderPropertySet>();
			StoreObjectId storeObjectId2 = ((VersionedId)folderList[folderList.Count - 1][0]).ObjectId;
			for (int j = folderList.Count - 1; j >= 0; j--)
			{
				if (((VersionedId)folderList[j][0]).ObjectId.Equals(storeObjectId2))
				{
					dictionary[storeObjectId2] = new Synchronizer.FolderPropertySet(folderList[j][2], folderList[j][7], (StoreObjectId)folderList[j][5]);
					storeObjectId2 = (StoreObjectId)folderList[j][5];
					Synchronizer.Tracer.TraceDebug<Synchronizer, object, StoreObjectId>((long)this.GetHashCode(), "{0}: Add folder {1} (ID: {2}) to parentFolderIdToPropertyMap from current folderList", this, folderList[j][1], storeObjectId2);
				}
			}
			if (this.parentFolderIdToPropertyMap != null)
			{
				Synchronizer.FolderPropertySet folderPropertySet;
				while (this.parentFolderIdToPropertyMap.TryGetValue(storeObjectId2, out folderPropertySet))
				{
					dictionary[storeObjectId2] = folderPropertySet;
					storeObjectId2 = folderPropertySet.ParentFolderId;
					Synchronizer.Tracer.TraceDebug<Synchronizer, StoreObjectId>((long)this.GetHashCode(), "{0}: Add folder (ID: {1}) to parentFolderIdToPropertyMap from previous map", this, storeObjectId2);
				}
			}
			this.parentFolderIdToPropertyMap = dictionary;
		}

		// Token: 0x04000312 RID: 786
		internal const int FolderIdIndex = 0;

		// Token: 0x04000313 RID: 787
		internal const int DisplayNameIndex = 1;

		// Token: 0x04000314 RID: 788
		internal const int FolderPolicyTagIndex = 2;

		// Token: 0x04000315 RID: 789
		internal const int RetentionFlagsIndex = 3;

		// Token: 0x04000316 RID: 790
		internal const int FolderRetentionPeriodIndex = 4;

		// Token: 0x04000317 RID: 791
		internal const int ParentIdIndex = 5;

		// Token: 0x04000318 RID: 792
		internal const int CompactDefaultPolicyIndex = 6;

		// Token: 0x04000319 RID: 793
		internal const int ArchiveTagIndex = 7;

		// Token: 0x0400031A RID: 794
		internal const int FolderArchivePeriodIndex = 8;

		// Token: 0x0400031B RID: 795
		internal const int FolderExplicitPolicyTagIndex = 9;

		// Token: 0x0400031C RID: 796
		internal const int FolderExplicitArchiveTagIndex = 10;

		// Token: 0x0400031D RID: 797
		internal const int FolderContainerClassIndex = 11;

		// Token: 0x0400031E RID: 798
		internal static readonly PropertyDefinition[] FolderDataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.DisplayName,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.ParentItemId,
			FolderSchema.RetentionTagEntryId,
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			StoreObjectSchema.ExplicitPolicyTag,
			StoreObjectSchema.ExplicitArchiveTag,
			StoreObjectSchema.ContainerClass
		};

		// Token: 0x0400031F RID: 799
		private static readonly Trace Tracer = ExTraceGlobals.TagProvisionerTracer;

		// Token: 0x04000320 RID: 800
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000321 RID: 801
		private MailboxDataForTags mailboxDataForTags;

		// Token: 0x04000322 RID: 802
		private ElcTagSubAssistant elcAssistant;

		// Token: 0x04000323 RID: 803
		private string toString;

		// Token: 0x04000324 RID: 804
		private Dictionary<StoreObjectId, FolderTagData> folderTagDict;

		// Token: 0x04000325 RID: 805
		private bool fullCrawl;

		// Token: 0x04000326 RID: 806
		private Dictionary<StoreObjectId, Synchronizer.FolderPropertySet> parentFolderIdToPropertyMap;

		// Token: 0x0200006E RID: 110
		internal class FolderPropertySet
		{
			// Token: 0x060003E8 RID: 1000 RVA: 0x0001C53E File Offset: 0x0001A73E
			internal FolderPropertySet(object policyTagProperty, object archiveTagProperty, StoreObjectId parentFolderId)
			{
				this.policyTagProperty = policyTagProperty;
				this.archiveTagProperty = archiveTagProperty;
				this.parentFolderId = parentFolderId;
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001C55B File Offset: 0x0001A75B
			internal object PolicyTagProperty
			{
				get
				{
					return this.policyTagProperty;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001C563 File Offset: 0x0001A763
			internal object ArchiveTagProperty
			{
				get
				{
					return this.archiveTagProperty;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001C56B File Offset: 0x0001A76B
			internal StoreObjectId ParentFolderId
			{
				get
				{
					return this.parentFolderId;
				}
			}

			// Token: 0x04000327 RID: 807
			private object policyTagProperty;

			// Token: 0x04000328 RID: 808
			private object archiveTagProperty;

			// Token: 0x04000329 RID: 809
			private StoreObjectId parentFolderId;
		}
	}
}
