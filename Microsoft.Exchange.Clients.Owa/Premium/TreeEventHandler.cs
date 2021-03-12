using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004E6 RID: 1254
	[OwaEventNamespace("Tree")]
	internal sealed class TreeEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002F9D RID: 12189 RVA: 0x001142A1 File Offset: 0x001124A1
		public static void Register()
		{
			OwaEventRegistry.RegisterEnum(typeof(FolderTreeRenderType));
			OwaEventRegistry.RegisterHandler(typeof(TreeEventHandler));
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x001142C4 File Offset: 0x001124C4
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEventParameter("rdt", typeof(FolderTreeRenderType))]
		[OwaEvent("Expand")]
		[OwaEventParameter("id", typeof(string))]
		public void Expand()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "TreeEventHandler.Expand");
			string text = (string)base.GetParameter("id");
			bool flag = false;
			if (OwaStoreObjectId.IsDummyArchiveFolder(text))
			{
				text = base.UserContext.GetArchiveRootFolderIdString();
				flag = !string.IsNullOrEmpty(text);
			}
			this.RenderFolderTreeChangedNode(OwaStoreObjectId.CreateFromString(text), null, true, flag, (FolderTreeRenderType)base.GetParameter("rdt"));
			if (flag)
			{
				NavigationHost.RenderFavoritesAndNavigationTrees(this.Writer, base.UserContext, null, new NavigationNodeGroupSection[]
				{
					NavigationNodeGroupSection.First
				});
			}
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x00114358 File Offset: 0x00112558
		[OwaEventParameter("exp", typeof(bool))]
		[OwaEventParameter("fSrcD", typeof(bool))]
		[OwaEventParameter("fDstD", typeof(bool))]
		[OwaEvent("Move")]
		[OwaEventParameter("rdt", typeof(FolderTreeRenderType))]
		[OwaEventParameter("cms", typeof(NavigationModule), true)]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		public void Move()
		{
			this.CopyOrMoveFolder(false);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x00114361 File Offset: 0x00112561
		[OwaEventParameter("fSrcD", typeof(bool))]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("fDstD", typeof(bool))]
		[OwaEventParameter("exp", typeof(bool))]
		[OwaEventParameter("rdt", typeof(FolderTreeRenderType))]
		[OwaEventParameter("cms", typeof(NavigationModule), true)]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEvent("Copy")]
		public void Copy()
		{
			this.CopyOrMoveFolder(true);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0011436C File Offset: 0x0011256C
		private void CopyOrMoveFolder(bool isCopy)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "TreeEventHandler." + (isCopy ? "Copy" : "Move"));
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			OwaStoreObjectId owaStoreObjectId2 = (OwaStoreObjectId)base.GetParameter("destId");
			bool isExpanded = (bool)base.GetParameter("exp");
			if (owaStoreObjectId.IsOtherMailbox || owaStoreObjectId2.IsOtherMailbox)
			{
				throw new OwaInvalidRequestException("Cannot copy or move a shared folder");
			}
			if (Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.SearchFolders) || Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId2, DefaultFolderType.SearchFolders))
			{
				throw new OwaInvalidRequestException("Cannot Copy or Move Search Folder");
			}
			NavigationTreeDirtyFlag navigationTreeDirtyFlag = NavigationTreeDirtyFlag.None;
			string displayName;
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[]
			{
				FolderSchema.DisplayName,
				StoreObjectSchema.ContainerClass,
				FolderSchema.IsOutlookSearchFolder,
				FolderSchema.AdminFolderFlags,
				StoreObjectSchema.ParentEntryId
			}))
			{
				displayName = folder.DisplayName;
				string className = folder.ClassName;
				if (Utilities.IsOutlookSearchFolder(folder))
				{
					throw new OwaInvalidRequestException("Cannot Copy or Move Search Folders");
				}
				if (!this.CanFolderHaveSubFolders(owaStoreObjectId2))
				{
					throw new OwaInvalidRequestException("Cannot Copy or Move a folder to this destination");
				}
				if (Utilities.IsELCFolder(folder))
				{
					throw new OwaInvalidRequestException(string.Format("Cannot {0} ELC folders.", isCopy ? "Copy" : "Move"));
				}
				if (!isCopy && ((!owaStoreObjectId.IsPublic && Utilities.IsSpecialFolderForSession(folder.Session as MailboxSession, owaStoreObjectId.StoreObjectId)) || Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
				{
					ExtendedFolderFlags.RemoteHierarchy
				})))
				{
					throw new OwaInvalidRequestException("Cannot move folders that are special or controlled remotely.");
				}
				if (base.UserContext.IsPublicFolderRootId(owaStoreObjectId.StoreObjectId))
				{
					throw new OwaEventHandlerException("Copy/move public root folder is not supported", LocalizedStrings.GetNonEncoded(-177785786), true);
				}
				bool flag = owaStoreObjectId.IsPublic || (bool)base.GetParameter("fSrcD");
				bool flag2 = owaStoreObjectId2.IsPublic || (bool)base.GetParameter("fDstD");
				bool flag3 = !isCopy && owaStoreObjectId.IsArchive != owaStoreObjectId2.IsArchive;
				if (((!flag || !flag2) && (!isCopy || !flag2) && (isCopy || flag || flag2)) || flag3)
				{
					navigationTreeDirtyFlag = this.CheckNavigationTreeDirtyFlag(folder, true);
					if (isCopy || flag)
					{
						navigationTreeDirtyFlag &= ~NavigationTreeDirtyFlag.Favorites;
					}
				}
			}
			if (owaStoreObjectId2.IsArchive)
			{
				navigationTreeDirtyFlag |= NavigationTreeDirtyFlag.Favorites;
			}
			OperationResult operationResult = Utilities.CopyOrMoveFolder(base.UserContext, isCopy, owaStoreObjectId2, new OwaStoreObjectId[]
			{
				owaStoreObjectId
			}).OperationResult;
			if (operationResult == OperationResult.Failed)
			{
				throw new OwaEventHandlerException(isCopy ? "Copy returned an OperationResult.Failed" : "Move returned an OperationResult.Failed", LocalizedStrings.GetNonEncoded(-1597406995));
			}
			if (operationResult == OperationResult.PartiallySucceeded)
			{
				throw new OwaEventHandlerException((isCopy ? "Copy" : "Move") + " returned an OperationResult.PartiallySucceeded", LocalizedStrings.GetNonEncoded(2109230231));
			}
			bool flag4 = true;
			if (!isCopy && owaStoreObjectId.IsPublic == owaStoreObjectId2.IsPublic && owaStoreObjectId.IsArchive == owaStoreObjectId2.IsArchive && StringComparer.InvariantCultureIgnoreCase.Equals(owaStoreObjectId.MailboxOwnerLegacyDN, owaStoreObjectId2.MailboxOwnerLegacyDN))
			{
				flag4 = false;
			}
			OwaStoreObjectId newFolderId;
			if (flag4)
			{
				newFolderId = this.GetSubFolderIdByName(owaStoreObjectId2, displayName);
			}
			else
			{
				newFolderId = owaStoreObjectId;
			}
			this.RenderFolderTreeChangedNode(owaStoreObjectId2, newFolderId, isExpanded, owaStoreObjectId2.IsArchive, (FolderTreeRenderType)base.GetParameter("rdt"));
			RenderingUtilities.RenderNavigationTreeDirtyFlag(this.Writer, base.UserContext, navigationTreeDirtyFlag, (NavigationModule[])base.GetParameter("cms"));
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x00114708 File Offset: 0x00112908
		[OwaEventParameter("fC", typeof(string))]
		[OwaEventParameter("cms", typeof(NavigationModule), true)]
		[OwaEvent("New")]
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEventParameter("fDstD", typeof(bool))]
		[OwaEventParameter("fN", typeof(string))]
		[OwaEventParameter("exp", typeof(bool))]
		[OwaEventParameter("rdt", typeof(FolderTreeRenderType))]
		public void New()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "TreeEventHandler.New");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("destId");
			if (owaStoreObjectId.IsOtherMailbox)
			{
				throw new OwaInvalidRequestException("Cannot add new folder underneath a shared folder");
			}
			string text = (string)base.GetParameter("fC");
			string text2 = (string)base.GetParameter("fN");
			bool isExpanded = (bool)base.GetParameter("exp");
			if (Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.SearchFolders))
			{
				throw new OwaInvalidRequestException("Cannot Create new Search Folder through OWA");
			}
			if (Utilities.IsELCRootFolder(owaStoreObjectId, base.UserContext))
			{
				throw new OwaInvalidRequestException("Cannot create new folders under the root ELC folder.");
			}
			text2 = text2.Trim();
			if (text2.Length == 0)
			{
				throw new OwaEventHandlerException("User did not provide name for new folder", LocalizedStrings.GetNonEncoded(-41080803), true);
			}
			StoreObjectType objectType = owaStoreObjectId.StoreObjectId.ObjectType;
			if (!this.CanFolderHaveSubFolders(owaStoreObjectId))
			{
				throw new OwaInvalidRequestException("Cannot Create new Search Folder through OWA");
			}
			using (Folder folder = Utilities.CreateSubFolder(owaStoreObjectId, objectType, text2, base.UserContext))
			{
				folder.ClassName = text;
				try
				{
					folder.Save();
				}
				catch (ObjectExistedException)
				{
					throw;
				}
				catch (StoragePermanentException innerException)
				{
					throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(995407892), innerException);
				}
				folder.Load();
				OwaStoreObjectId newFolderId = OwaStoreObjectId.CreateFromStoreObject(folder);
				this.RenderFolderTreeChangedNode(owaStoreObjectId, newFolderId, isExpanded, owaStoreObjectId.IsArchive, (FolderTreeRenderType)base.GetParameter("rdt"));
				NavigationTreeDirtyFlag navigationTreeDirtyFlag = FolderTreeNode.GetAffectedTreeFromContainerClass(text);
				if (owaStoreObjectId.IsArchive)
				{
					navigationTreeDirtyFlag |= NavigationTreeDirtyFlag.Favorites;
				}
				if (navigationTreeDirtyFlag != NavigationTreeDirtyFlag.Favorites || owaStoreObjectId.IsArchive)
				{
					RenderingUtilities.RenderNavigationTreeDirtyFlag(this.Writer, base.UserContext, navigationTreeDirtyFlag, (NavigationModule[])base.GetParameter("cms"));
				}
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x001148E4 File Offset: 0x00112AE4
		[OwaEventParameter("destId", typeof(OwaStoreObjectId))]
		[OwaEvent("Rename")]
		[OwaEventParameter("fN", typeof(string))]
		public void Rename()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "TreeEventHandler.Rename");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("destId");
			string text = ((string)base.GetParameter("fN")).Trim();
			string s = text;
			if (text.Length == 0)
			{
				throw new OwaEventHandlerException("User did not provide name for new folder", LocalizedStrings.GetNonEncoded(-41080803), true);
			}
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
			{
				if (!Utilities.CanFolderBeRenamed(base.UserContext, folder))
				{
					throw new OwaInvalidRequestException("Folder cannot be renamed.");
				}
				folder.DisplayName = text;
				FolderSaveResult folderSaveResult = folder.Save();
				if (folderSaveResult.OperationResult != OperationResult.Succeeded)
				{
					if (Utilities.IsFolderNameConflictError(folderSaveResult))
					{
						throw new OwaEventHandlerException("Folder rename did not return OperationResult.Succeeded", LocalizedStrings.GetNonEncoded(1602494619), OwaEventHandlerErrorCode.FolderNameExists, true);
					}
					throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(995407892));
				}
				else
				{
					if (owaStoreObjectId.IsArchive)
					{
						s = string.Format(LocalizedStrings.GetNonEncoded(-83764036), text, Utilities.GetMailboxOwnerDisplayName((MailboxSession)folder.Session));
					}
					this.Writer.Write("<div id=tn>");
					Utilities.HtmlEncode(text, this.Writer, true);
					this.Writer.Write("</div><div id=ntn>");
					Utilities.HtmlEncode(s, this.Writer, true);
					this.Writer.Write("</div>");
				}
			}
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x00114A54 File Offset: 0x00112C54
		[OwaEventParameter("fSrcD", typeof(bool))]
		[OwaEvent("Delete")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("cms", typeof(NavigationModule), true)]
		[OwaEventParameter("pd", typeof(bool))]
		public void Delete()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			if (owaStoreObjectId.IsOtherMailbox || owaStoreObjectId.IsGSCalendar)
			{
				throw new OwaInvalidRequestException("Cannot perform delete on shared folder");
			}
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "TreeEventHandler.Delete");
			if (Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.SearchFolders))
			{
				throw new OwaInvalidRequestException("Cannot Delete Search Folders");
			}
			bool flag = (bool)base.GetParameter("fSrcD");
			bool flag2 = flag || owaStoreObjectId.IsPublic || (bool)base.GetParameter("pd");
			NavigationTreeDirtyFlag flag3 = NavigationTreeDirtyFlag.None;
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[]
			{
				StoreObjectSchema.ContainerClass,
				FolderSchema.IsOutlookSearchFolder,
				FolderSchema.AdminFolderFlags,
				StoreObjectSchema.ParentEntryId
			}))
			{
				string className = folder.ClassName;
				if (Utilities.IsOutlookSearchFolder(folder))
				{
					throw new OwaInvalidRequestException("Cannot Delete Search Folders");
				}
				if (Utilities.IsELCFolder(folder))
				{
					throw new OwaInvalidRequestException("Cannot Delete ELC folders.");
				}
				if (Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
				{
					ExtendedFolderFlags.RemoteHierarchy
				}))
				{
					throw new OwaInvalidRequestException("Cannot delete a folder that is controlled remotely.");
				}
				if (!flag2 || (!owaStoreObjectId.IsPublic && !flag))
				{
					flag3 = this.CheckNavigationTreeDirtyFlag(folder, true);
				}
			}
			OperationResult operationResult = Utilities.Delete(base.UserContext, flag2, new OwaStoreObjectId[]
			{
				owaStoreObjectId
			}).OperationResult;
			if (operationResult == OperationResult.Failed)
			{
				Strings.IDs localizedId = flag2 ? -1691273193 : 1041829989;
				throw new OwaEventHandlerException("Delete returned an OperationResult.Failed", LocalizedStrings.GetNonEncoded(localizedId));
			}
			if (operationResult == OperationResult.PartiallySucceeded)
			{
				throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(995407892));
			}
			if (!flag2)
			{
				this.RenderFolderTreeChangedNode(base.UserContext.GetDeletedItemsFolderId((MailboxSession)owaStoreObjectId.GetSession(base.UserContext)), owaStoreObjectId, false, false, FolderTreeRenderType.None);
			}
			RenderingUtilities.RenderNavigationTreeDirtyFlag(this.Writer, base.UserContext, flag3, (NavigationModule[])base.GetParameter("cms"));
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x00114C68 File Offset: 0x00112E68
		[OwaEvent("MarkAllAsRead")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		public void MarkAllAsRead()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			if (Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.SearchFolders))
			{
				throw new OwaInvalidRequestException("Cannot perform any operation on Search Folder Root");
			}
			using (Folder folderForContent = Utilities.GetFolderForContent<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]))
			{
				bool suppressReadReceipts = Utilities.ShouldSuppressReadReceipt(base.UserContext);
				if (owaStoreObjectId.StoreObjectId.Equals(base.UserContext.JunkEmailFolderId) || !base.UserContext.IsInMyMailbox(folderForContent))
				{
					suppressReadReceipts = true;
				}
				folderForContent.MarkAllAsRead(suppressReadReceipts);
			}
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x00114D0C File Offset: 0x00112F0C
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEvent("EmptyDeletedItems")]
		public void EmptyDeletedItems()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			if (!Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.DeletedItems))
			{
				throw new OwaInvalidRequestException("Can only perform EmptyDeletedItems operation on DeletedItems folder");
			}
			owaStoreObjectId.GetSession(base.UserContext).DeleteAllObjects(DeleteItemFlags.SoftDelete, owaStoreObjectId.StoreObjectId);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00114D60 File Offset: 0x00112F60
		[OwaEventParameter("cms", typeof(NavigationModule), true)]
		[OwaEvent("EmptyJunkEmail")]
		public void EmptyJunkEmail()
		{
			Folder folder = Utilities.SafeFolderBind(base.UserContext.MailboxSession, DefaultFolderType.JunkEmail, new PropertyDefinition[0]);
			NavigationTreeDirtyFlag flag = NavigationTreeDirtyFlag.None;
			if (folder != null)
			{
				using (folder)
				{
					flag = this.CheckNavigationTreeDirtyFlag(folder, false);
				}
			}
			base.UserContext.MailboxSession.DeleteAllObjects(DeleteItemFlags.SoftDelete | DeleteItemFlags.SuppressReadReceipt, base.UserContext.JunkEmailFolderId);
			RenderingUtilities.RenderNavigationTreeDirtyFlag(this.Writer, base.UserContext, flag, (NavigationModule[])base.GetParameter("cms"));
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x00114DF4 File Offset: 0x00112FF4
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		[OwaEvent("EmptyFolder")]
		public void EmptyFolder()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			if (Utilities.IsDefaultFolderId(base.UserContext, owaStoreObjectId, DefaultFolderType.SearchFolders))
			{
				throw new OwaInvalidRequestException("Cannot perform empty folder on Search Folder Root");
			}
			using (Folder folderForContent = Utilities.GetFolderForContent<Folder>(base.UserContext, owaStoreObjectId, new PropertyDefinition[]
			{
				FolderSchema.IsOutlookSearchFolder
			}))
			{
				if (!Utilities.IsOutlookSearchFolder(folderForContent))
				{
					DeleteItemFlags flags = owaStoreObjectId.IsPublic ? DeleteItemFlags.SoftDelete : DeleteItemFlags.MoveToDeletedItems;
					OperationResult operationResult = folderForContent.DeleteAllItems(flags).OperationResult;
					if (operationResult != OperationResult.Succeeded)
					{
						throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(166628739));
					}
				}
			}
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00114EA4 File Offset: 0x001130A4
		[OwaEvent("GetMailboxUsage")]
		public void GetMailboxUsage()
		{
			RenderingUtilities.RenderMailboxQuota(this.Writer, base.UserContext);
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x00114EB8 File Offset: 0x001130B8
		[OwaEvent("GetRootPublicFolderId")]
		public void GetRootPublicFolderId()
		{
			string text = base.UserContext.TryGetPublicFolderRootIdString();
			if (text != null)
			{
				this.Writer.Write(text);
				return;
			}
			throw new OwaEventHandlerException("Cannot get the id of the root public folder", LocalizedStrings.GetNonEncoded(2071101893), true);
		}

		// Token: 0x06002FAB RID: 12203 RVA: 0x00114EF6 File Offset: 0x001130F6
		[OwaEvent("GetPublicFolderReplicaType")]
		[OwaEventParameter("id", typeof(OwaStoreObjectId))]
		public void GetPublicFolderReplicaType()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("id");
			this.Writer.Write("var iHasRplc = 1;");
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x00114F1C File Offset: 0x0011311C
		[OwaEventParameter("mts", typeof(bool), false, true)]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("fts", typeof(bool), false, true)]
		[OwaEvent("PersistExpand")]
		[OwaEventParameter("bts", typeof(bool), false, true)]
		public void PersistExpandStatus()
		{
			base.ThrowIfCannotActAsOwner();
			if (base.UserContext.IsWebPartRequest)
			{
				return;
			}
			using (Folder folder = Utilities.SafeFolderBind(base.UserContext.MailboxSession, DefaultFolderType.Root, new PropertyDefinition[]
			{
				ViewStateProperties.TreeNodeCollapseStatus
			}))
			{
				if (folder != null)
				{
					int num = Utilities.GetFolderProperty<int>(folder, ViewStateProperties.TreeNodeCollapseStatus, 0);
					num = this.SetCollapsedTreeNodes(num, "fts", StatusPersistTreeNodeType.FavoritesRoot);
					num = this.SetCollapsedTreeNodes(num, "mts", StatusPersistTreeNodeType.CurrentNode);
					num = this.SetCollapsedTreeNodes(num, "bts", StatusPersistTreeNodeType.BuddyListRoot);
					folder[ViewStateProperties.TreeNodeCollapseStatus] = num;
					folder.Save();
				}
			}
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x00114FD0 File Offset: 0x001131D0
		private int SetCollapsedTreeNodes(int originalValue, string parameterName, StatusPersistTreeNodeType treeNodeType)
		{
			if (base.IsParameterSet(parameterName))
			{
				if ((bool)base.GetParameter(parameterName))
				{
					originalValue &= (int)(~(int)treeNodeType);
				}
				else
				{
					originalValue |= (int)treeNodeType;
				}
			}
			return originalValue;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00114FF8 File Offset: 0x001131F8
		private void RenderFolderTreeChangedNode(OwaStoreObjectId parentFolderId, OwaStoreObjectId newFolderId, bool isExpanded, bool updateFolderId, FolderTreeRenderType renderType)
		{
			this.Writer.Write("<div id=tn");
			if (base.UserContext.ArchiveAccessed && parentFolderId.Equals(base.UserContext.GetArchiveRootFolderId()))
			{
				this.Writer.Write(" archiveroot=\"1\"");
				MailboxSession mailboxSession = parentFolderId.GetSession(base.UserContext) as MailboxSession;
				if (mailboxSession != null && mailboxSession.MailboxOwner.MailboxInfo.IsRemote)
				{
					this.Writer.Write(" isremote=\"1\"");
				}
			}
			if (updateFolderId)
			{
				this.Writer.Write(" ufid=\"f");
				Utilities.HtmlEncode(parentFolderId.ToString(), this.Writer);
				this.Writer.Write("\"");
			}
			this.Writer.Write(">");
			if (isExpanded)
			{
				this.RenderSiblingNodes(parentFolderId, newFolderId, renderType);
			}
			else
			{
				if (newFolderId == null)
				{
					throw new ArgumentNullException("newFolderId");
				}
				FolderTreeNode folderTreeNode = FolderTreeNode.Load(base.UserContext, newFolderId, renderType);
				if (folderTreeNode != null)
				{
					FolderTreeNode folderTreeNode2 = folderTreeNode;
					folderTreeNode2.CustomAttributes += " _NF=1";
					folderTreeNode.RenderUndecoratedNode(this.Writer);
				}
			}
			this.Writer.Write("</div>");
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00115124 File Offset: 0x00113324
		private void RenderSiblingNodes(OwaStoreObjectId parentFolderId, OwaStoreObjectId newFolderId, FolderTreeRenderType renderType)
		{
			FolderTreeNode folderTreeNode;
			if (parentFolderId.IsPublic)
			{
				folderTreeNode = FolderTreeNode.CreatePublicFolderTreeNode(base.UserContext, parentFolderId.StoreObjectId);
			}
			else if (parentFolderId.IsOtherMailbox)
			{
				folderTreeNode = FolderTreeNode.CreateOtherMailboxRootNode(base.UserContext, parentFolderId, string.Empty, true);
				if (folderTreeNode == null)
				{
					throw new OwaEventHandlerException("User cannot view other's Inbox", LocalizedStrings.GetNonEncoded(995407892), true);
				}
			}
			else
			{
				folderTreeNode = FolderTreeNode.CreateMailboxFolderTreeNode(base.UserContext, (MailboxSession)parentFolderId.GetSession(base.UserContext), parentFolderId.StoreObjectId, renderType);
			}
			if (folderTreeNode == null)
			{
				return;
			}
			if (newFolderId != null)
			{
				foreach (TreeNode treeNode in folderTreeNode.Children)
				{
					FolderTreeNode folderTreeNode2 = (FolderTreeNode)treeNode;
					if (folderTreeNode2.FolderId.Equals(newFolderId))
					{
						FolderTreeNode folderTreeNode3 = folderTreeNode2;
						folderTreeNode3.CustomAttributes += " _NF=1";
					}
				}
			}
			folderTreeNode.RenderUndecoratedChildrenNode(this.Writer);
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00115220 File Offset: 0x00113420
		private NavigationTreeDirtyFlag CheckNavigationTreeDirtyFlag(Folder folder, bool includeSelf)
		{
			NavigationTreeDirtyFlag navigationTreeDirtyFlag = includeSelf ? FolderTreeNode.GetAffectedTreeFromContainerClass(folder.ClassName) : NavigationTreeDirtyFlag.None;
			object[][] array;
			using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, new PropertyDefinition[]
			{
				FolderSchema.IsHidden,
				StoreObjectSchema.ContainerClass
			}))
			{
				array = Utilities.FetchRowsFromQueryResult(queryResult, 10000);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i][0] is bool) || !(bool)array[i][0])
				{
					navigationTreeDirtyFlag |= FolderTreeNode.GetAffectedTreeFromContainerClass(array[i][1] as string);
				}
			}
			return navigationTreeDirtyFlag;
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x001152C8 File Offset: 0x001134C8
		private OwaStoreObjectId GetSubFolderIdByName(OwaStoreObjectId parentFolderId, string subFolderName)
		{
			OwaStoreObjectId result;
			using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, parentFolderId, new PropertyDefinition[0]))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id
				}))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, subFolderName);
					bool flag = queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
					object[][] array = null;
					if (flag)
					{
						array = queryResult.GetRows(1);
					}
					if (array == null || array.Length == 0 || array[0].Length == 0)
					{
						ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Can't find any subfolders of the destinationFolder with DisplayName matching the source folder's DisplayName");
						throw new OwaEventHandlerException("Can't find any subfolders of the destinationFolder with DisplayName matching the source folder's DisplayName", LocalizedStrings.GetNonEncoded(1073923836));
					}
					StoreObjectId objectId = ((VersionedId)array[0][0]).ObjectId;
					result = OwaStoreObjectId.CreateFromStoreObjectId(objectId, parentFolderId);
				}
			}
			return result;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x001153B8 File Offset: 0x001135B8
		private bool CanFolderHaveSubFolders(OwaStoreObjectId folderId)
		{
			if (folderId.IsPublic)
			{
				return true;
			}
			using (Folder folder = Folder.Bind(folderId.GetSession(base.UserContext), folderId.StoreObjectId, new PropertyDefinition[]
			{
				FolderSchema.IsOutlookSearchFolder,
				FolderSchema.ExtendedFolderFlags
			}))
			{
				if (Utilities.IsDefaultFolder(folder, DefaultFolderType.SearchFolders))
				{
					return false;
				}
				if (Utilities.IsOutlookSearchFolder(folder))
				{
					return false;
				}
				if (Utilities.IsELCRootFolder(folder))
				{
					return false;
				}
				if (Utilities.IsOneOfTheFolderFlagsSet(folder, new ExtendedFolderFlags[]
				{
					ExtendedFolderFlags.ExclusivelyBound,
					ExtendedFolderFlags.RemoteHierarchy
				}))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002164 RID: 8548
		public const string EventNamespace = "Tree";

		// Token: 0x04002165 RID: 8549
		public const string MethodExpand = "Expand";

		// Token: 0x04002166 RID: 8550
		public const string MethodPersistExpand = "PersistExpand";

		// Token: 0x04002167 RID: 8551
		public const string MethodMove = "Move";

		// Token: 0x04002168 RID: 8552
		public const string MethodCopy = "Copy";

		// Token: 0x04002169 RID: 8553
		public const string MethodNew = "New";

		// Token: 0x0400216A RID: 8554
		public const string MethodRename = "Rename";

		// Token: 0x0400216B RID: 8555
		public const string MethodDelete = "Delete";

		// Token: 0x0400216C RID: 8556
		public const string MethodMarkAllAsRead = "MarkAllAsRead";

		// Token: 0x0400216D RID: 8557
		public const string MethodEmptyDeletedItems = "EmptyDeletedItems";

		// Token: 0x0400216E RID: 8558
		public const string MethodEmptyJunkEmail = "EmptyJunkEmail";

		// Token: 0x0400216F RID: 8559
		public const string MethodEmptyFolder = "EmptyFolder";

		// Token: 0x04002170 RID: 8560
		public const string MethodGetMailboxUsage = "GetMailboxUsage";

		// Token: 0x04002171 RID: 8561
		public const string MethodGetPublicFolderReplicaType = "GetPublicFolderReplicaType";

		// Token: 0x04002172 RID: 8562
		public const string MethodGetRootPublicFolderId = "GetRootPublicFolderId";

		// Token: 0x04002173 RID: 8563
		public const string Type = "t";

		// Token: 0x04002174 RID: 8564
		public const string SourceFolderId = "id";

		// Token: 0x04002175 RID: 8565
		public const string DestinationFolderId = "destId";

		// Token: 0x04002176 RID: 8566
		public const string IsPermanentDelete = "pd";

		// Token: 0x04002177 RID: 8567
		public const string FolderClass = "fC";

		// Token: 0x04002178 RID: 8568
		public const string FolderName = "fN";

		// Token: 0x04002179 RID: 8569
		public const string RenderType = "rdt";

		// Token: 0x0400217A RID: 8570
		public const string FavoritesTreeStatus = "fts";

		// Token: 0x0400217B RID: 8571
		public const string MailboxFolderTreeStatus = "mts";

		// Token: 0x0400217C RID: 8572
		public const string BuddyListTreeStatus = "bts";

		// Token: 0x0400217D RID: 8573
		public const string IsExpanded = "exp";

		// Token: 0x0400217E RID: 8574
		public const string IsSourceFolderInDeletedItems = "fSrcD";

		// Token: 0x0400217F RID: 8575
		public const string IsDestinationFolderInDeletedItems = "fDstD";

		// Token: 0x04002180 RID: 8576
		public const string ClientNavigationModules = "cms";
	}
}
