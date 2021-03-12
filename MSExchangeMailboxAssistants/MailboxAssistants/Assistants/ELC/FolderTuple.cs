using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000079 RID: 121
	internal class FolderTuple
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0001FD9A File Offset: 0x0001DF9A
		internal FolderTuple(StoreObjectId folderId, StoreObjectId parentId, string displayName, object[] folderProps, bool isRoot)
		{
			this.folderId = folderId;
			this.parentId = parentId;
			this.displayName = displayName;
			this.children = null;
			this.isRootFolder = isRoot;
			this.ResolveProperties(folderProps);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001FDCE File Offset: 0x0001DFCE
		internal FolderTuple(StoreObjectId folderId, StoreObjectId parentId, string displayName, object[] folderProps)
		{
			this.folderId = folderId;
			this.parentId = parentId;
			this.displayName = displayName;
			this.children = null;
			this.isRootFolder = false;
			this.ResolveProperties(folderProps);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001FE01 File Offset: 0x0001E001
		internal FolderTuple(StoreObjectId folderId, StoreObjectId parentId, string displayName, Dictionary<PropertyDefinition, object> folderProps)
		{
			this.folderId = folderId;
			this.parentId = parentId;
			this.displayName = displayName;
			this.children = null;
			this.isRootFolder = false;
			this.folderProps = folderProps;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001FE34 File Offset: 0x0001E034
		private void ResolveProperties(object[] folderProps)
		{
			this.folderProps = new Dictionary<PropertyDefinition, object>();
			if (folderProps != null && folderProps.Length > 0)
			{
				for (FolderHelper.DataColumnIndex dataColumnIndex = FolderHelper.DataColumnIndex.startOfTagPropsIndex; dataColumnIndex <= FolderHelper.DataColumnIndex.containerClassIndex; dataColumnIndex++)
				{
					switch (dataColumnIndex)
					{
					case FolderHelper.DataColumnIndex.startOfTagPropsIndex:
						this.FolderProps.Add(StoreObjectSchema.PolicyTag, folderProps[(int)dataColumnIndex]);
						break;
					case FolderHelper.DataColumnIndex.retentionPeriodIndex:
						this.FolderProps.Add(StoreObjectSchema.RetentionPeriod, folderProps[(int)dataColumnIndex]);
						break;
					case FolderHelper.DataColumnIndex.retentionFlagsIndex:
						this.FolderProps.Add(StoreObjectSchema.RetentionFlags, folderProps[(int)dataColumnIndex]);
						break;
					case FolderHelper.DataColumnIndex.archiveTagIndex:
						this.FolderProps.Add(StoreObjectSchema.ArchiveTag, folderProps[(int)dataColumnIndex]);
						break;
					case FolderHelper.DataColumnIndex.archivePeriodIndex:
						this.FolderProps.Add(StoreObjectSchema.ArchivePeriod, folderProps[(int)dataColumnIndex]);
						break;
					case FolderHelper.DataColumnIndex.retentionTagEntryId:
						this.FolderProps.Add(FolderSchema.RetentionTagEntryId, folderProps[(int)dataColumnIndex]);
						break;
					case FolderHelper.DataColumnIndex.containerClassIndex:
						this.FolderProps.Add(StoreObjectSchema.ContainerClass, folderProps[(int)dataColumnIndex]);
						break;
					}
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001FF2A File Offset: 0x0001E12A
		internal StoreObjectId ParentId
		{
			get
			{
				return this.parentId;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001FF32 File Offset: 0x0001E132
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001FF3A File Offset: 0x0001E13A
		internal Dictionary<string, FolderTuple> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001FF42 File Offset: 0x0001E142
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0001FF4A File Offset: 0x0001E14A
		internal Dictionary<PropertyDefinition, object> FolderProps
		{
			get
			{
				return this.folderProps;
			}
			set
			{
				this.folderProps = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001FF53 File Offset: 0x0001E153
		internal bool IsRootFolder
		{
			get
			{
				return this.isRootFolder;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001FF5B File Offset: 0x0001E15B
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0001FF63 File Offset: 0x0001E163
		internal StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001FF6C File Offset: 0x0001E16C
		internal static bool ArePropTagsSame(FolderTuple left, FolderTuple right)
		{
			for (FolderHelper.DataColumnIndex dataColumnIndex = FolderHelper.DataColumnIndex.startOfTagPropsIndex; dataColumnIndex <= FolderHelper.DataColumnIndex.containerClassIndex; dataColumnIndex++)
			{
				PropertyDefinition propertyDefinition = FolderHelper.DataColumns[(int)dataColumnIndex];
				object obj = left.FolderProps[propertyDefinition];
				object obj2 = right.FolderProps[propertyDefinition];
				bool flag = obj != null && !(obj is PropertyError);
				bool flag2 = obj2 != null && !(obj2 is PropertyError);
				if (flag && flag2)
				{
					if (propertyDefinition == StoreObjectSchema.PolicyTag || propertyDefinition == StoreObjectSchema.ArchiveTag || propertyDefinition == FolderSchema.RetentionTagEntryId)
					{
						if (!(obj is byte[]) || !(obj2 is byte[]) || !ArrayComparer<byte>.Comparer.Equals((byte[])obj, (byte[])obj2))
						{
							return false;
						}
					}
					else if ((propertyDefinition == StoreObjectSchema.RetentionPeriod || propertyDefinition == StoreObjectSchema.RetentionFlags || propertyDefinition == StoreObjectSchema.ArchivePeriod) && (!(obj is int) || !(obj2 is int) || (int)obj != (int)obj2))
					{
						return false;
					}
				}
				else if (flag ^ flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00020064 File Offset: 0x0001E264
		internal static void AssignTagPropsToFolder(FolderTuple source, Folder target, MailboxSession session)
		{
			StoreObjectId objectId = target.Id.ObjectId;
			for (FolderHelper.DataColumnIndex dataColumnIndex = FolderHelper.DataColumnIndex.startOfTagPropsIndex; dataColumnIndex <= FolderHelper.DataColumnIndex.containerClassIndex; dataColumnIndex++)
			{
				PropertyDefinition key = FolderHelper.DataColumns[(int)dataColumnIndex];
				object obj = source.FolderProps[key];
				if (obj != null && !(obj is PropertyError))
				{
					if (dataColumnIndex != FolderHelper.DataColumnIndex.containerClassIndex)
					{
						target[FolderHelper.DataColumns[(int)dataColumnIndex]] = obj;
					}
					else if (target.GetValueOrDefault<string>(FolderHelper.DataColumns[(int)dataColumnIndex], null) == null)
					{
						target[FolderHelper.DataColumns[(int)dataColumnIndex]] = obj;
					}
				}
				else if (target.GetValueOrDefault<object>(FolderHelper.DataColumns[(int)dataColumnIndex]) != null && dataColumnIndex != FolderHelper.DataColumnIndex.containerClassIndex)
				{
					target.DeleteProperties(new PropertyDefinition[]
					{
						FolderHelper.DataColumns[(int)dataColumnIndex]
					});
				}
			}
			FolderSaveResult folderSaveResult = target.Save();
			if (folderSaveResult.OperationResult != OperationResult.Succeeded)
			{
				FolderTuple.Tracer.TraceError<StoreObjectId, FolderSaveResult>(0L, "AssignTagPropsToFolder for folder {0} save result {1}", objectId, folderSaveResult);
				throw new IWPermanentException(Strings.descUnableToSaveFolderTagProperties(objectId.ToString(), session.MailboxOwner.ToString(), folderSaveResult.ToString()));
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0002015D File Offset: 0x0001E35D
		internal void AddChild(string childName, FolderTuple childTuple)
		{
			if (this.children == null)
			{
				this.children = new Dictionary<string, FolderTuple>(StringComparer.OrdinalIgnoreCase);
			}
			this.children[childName ?? string.Empty] = childTuple;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00020190 File Offset: 0x0001E390
		internal bool TryGetChild(string childName, out FolderTuple child)
		{
			child = null;
			FolderTuple.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Looking for child '{0}' under '{1}'.", childName, this.DisplayName);
			if (this.Children != null)
			{
				FolderTuple.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Folder '{0}' has children. Will look for a match.", this.DisplayName);
				return this.Children.TryGetValue(childName, out child);
			}
			FolderTuple.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "No child called '{0}' was found under folder '{1}'.", childName, this.DisplayName);
			return false;
		}

		// Token: 0x04000386 RID: 902
		private static readonly Trace Tracer = ExTraceGlobals.FolderProvisionerTracer;

		// Token: 0x04000387 RID: 903
		private readonly bool isRootFolder;

		// Token: 0x04000388 RID: 904
		private readonly StoreObjectId parentId;

		// Token: 0x04000389 RID: 905
		private readonly string displayName;

		// Token: 0x0400038A RID: 906
		private Dictionary<string, FolderTuple> children;

		// Token: 0x0400038B RID: 907
		private Dictionary<PropertyDefinition, object> folderProps;

		// Token: 0x0400038C RID: 908
		private StoreObjectId folderId;
	}
}
