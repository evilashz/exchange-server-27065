using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasSyntheticFolder : EasFolderBase, ISourceFolder, IDestinationFolder, IFolder, IDisposable
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00006B2B File Offset: 0x00004D2B
		private EasSyntheticFolder(string serverId, string parentId, EasFolderType folderType, Func<EasFolderBase, FolderRec> createFolderRec) : base(serverId, parentId, folderType.ToString(), folderType)
		{
			this.FolderRec = createFolderRec(this);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006B4F File Offset: 0x00004D4F
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006B57 File Offset: 0x00004D57
		internal FolderRec FolderRec { get; private set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00006B60 File Offset: 0x00004D60
		void ISourceFolder.CopyTo(IFxProxy fxFolderProxy, CopyPropertiesFlags flags, PropTag[] propTagsToExclude)
		{
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006B62 File Offset: 0x00004D62
		void ISourceFolder.ExportMessages(IFxProxy destFolderProxy, CopyMessagesFlags flags, byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006B69 File Offset: 0x00004D69
		FolderChangesManifest ISourceFolder.EnumerateChanges(EnumerateContentChangesFlags flags, int maxChanges)
		{
			return base.CreateInitializedChangesManifest();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006B71 File Offset: 0x00004D71
		List<MessageRec> ISourceFolder.EnumerateMessagesPaged(int maxPageSize)
		{
			return null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006B74 File Offset: 0x00004D74
		int ISourceFolder.GetEstimatedItemCount()
		{
			return 0;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006B77 File Offset: 0x00004D77
		bool IDestinationFolder.SetSearchCriteria(RestrictionData restriction, byte[][] entryIds, SearchCriteriaFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006B7E File Offset: 0x00004D7E
		PropProblemData[] IDestinationFolder.SetSecurityDescriptor(SecurityProp secProp, RawSecurityDescriptor sd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006B85 File Offset: 0x00004D85
		IFxProxy IDestinationFolder.GetFxProxy(FastTransferFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006B8C File Offset: 0x00004D8C
		void IDestinationFolder.SetReadFlagsOnMessages(SetReadFlags flags, byte[][] entryIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006B93 File Offset: 0x00004D93
		void IDestinationFolder.SetMessageProps(byte[] entryId, PropValueData[] propValues)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006B9A File Offset: 0x00004D9A
		void IDestinationFolder.SetRules(RuleData[] rules)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006BA1 File Offset: 0x00004DA1
		void IDestinationFolder.SetACL(SecurityProp secProp, PropValueData[][] aclData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006BA8 File Offset: 0x00004DA8
		void IDestinationFolder.SetExtendedAcl(AclFlags aclFlags, PropValueData[][] aclData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006BAF File Offset: 0x00004DAF
		void IDestinationFolder.Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006BB6 File Offset: 0x00004DB6
		Guid IDestinationFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006BBD File Offset: 0x00004DBD
		protected override FolderRec InternalGetFolderRec(PropTag[] additionalPtagsToLoad, GetFolderRecFlags flags)
		{
			return this.FolderRec;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006BC5 File Offset: 0x00004DC5
		protected override List<MessageRec> InternalLookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			return EasSyntheticFolder.EmptyMessageRec;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006BCC File Offset: 0x00004DCC
		private static FolderRec CreateRootFolderRec(EasFolderBase folder)
		{
			return new FolderRec(folder.EntryId, null, FolderType.Root, folder.DisplayName, DateTime.MinValue, null);
		}

		// Token: 0x04000057 RID: 87
		private const string RootServerId = "BBAA51E4-3863-42D1-9CE2-817B0DEEE67E";

		// Token: 0x04000058 RID: 88
		private const string IpmSubtreeServerId = "0";

		// Token: 0x04000059 RID: 89
		internal static readonly EasSyntheticFolder RootFolder = new EasSyntheticFolder("BBAA51E4-3863-42D1-9CE2-817B0DEEE67E", "BBAA51E4-3863-42D1-9CE2-817B0DEEE67E", EasFolderType.SyntheticRoot, new Func<EasFolderBase, FolderRec>(EasSyntheticFolder.CreateRootFolderRec));

		// Token: 0x0400005A RID: 90
		internal static readonly EasSyntheticFolder IpmSubtreeFolder = new EasSyntheticFolder("0", "BBAA51E4-3863-42D1-9CE2-817B0DEEE67E", EasFolderType.SyntheticIpmSubtree, new Func<EasFolderBase, FolderRec>(EasFolderBase.CreateGenericFolderRec));

		// Token: 0x0400005B RID: 91
		private static readonly List<MessageRec> EmptyMessageRec = new List<MessageRec>(0);
	}
}
