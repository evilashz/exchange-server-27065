using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000D RID: 13
	internal class FolderIdTranslator
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000046D8 File Offset: 0x000028D8
		public FolderIdTranslator(FolderHierarchy sourceHierarchy, FolderHierarchy targetHierarchy)
		{
			this.sourceHierarchy = sourceHierarchy;
			this.targetHierarchy = targetHierarchy;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000046F0 File Offset: 0x000028F0
		public byte[] TranslateFolderId(byte[] sourceEntryId)
		{
			if (sourceEntryId == null)
			{
				return null;
			}
			FolderMapping folderMapping = (FolderMapping)this.sourceHierarchy[sourceEntryId];
			if (folderMapping == null || folderMapping.TargetFolder == null)
			{
				return null;
			}
			return folderMapping.TargetFolder.EntryId;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000472C File Offset: 0x0000292C
		public byte[] TranslateTargetFolderId(byte[] targetEntryId)
		{
			if (targetEntryId == null)
			{
				return null;
			}
			FolderMapping folderMapping = (FolderMapping)this.targetHierarchy[targetEntryId];
			if (folderMapping == null)
			{
				return null;
			}
			byte[] array = folderMapping.FolderRec[this.targetHierarchy.SourceEntryIDPtag] as byte[];
			if (array != null)
			{
				return array;
			}
			if (folderMapping.SourceFolder == null)
			{
				return null;
			}
			return folderMapping.SourceFolder.EntryId;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000478C File Offset: 0x0000298C
		public byte[][] TranslateFolderIds(byte[][] sourceFolderIds)
		{
			if (sourceFolderIds == null)
			{
				return null;
			}
			byte[][] array = new byte[sourceFolderIds.Length][];
			for (int i = 0; i < sourceFolderIds.Length; i++)
			{
				array[i] = (this.TranslateFolderId(sourceFolderIds[i]) ?? sourceFolderIds[i]);
			}
			return array;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000047C9 File Offset: 0x000029C9
		public void TranslateRestriction(RestrictionData r)
		{
			if (r != null)
			{
				r.EnumeratePropValues(new CommonUtils.EnumPropValueDelegate(this.TranslatePropValue));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000047E0 File Offset: 0x000029E0
		public void TranslateRules(RuleData[] ra)
		{
			if (ra != null)
			{
				foreach (RuleData ruleData in ra)
				{
					ruleData.Enumerate(null, new CommonUtils.EnumPropValueDelegate(this.TranslatePropValue), null);
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004818 File Offset: 0x00002A18
		private void TranslatePropValue(PropValueData pvd)
		{
			PropTag propTag = (PropTag)pvd.PropTag;
			if (propTag != PropTag.ParentEntryId)
			{
				if (propTag == PropTag.ReplyTemplateID)
				{
					pvd.Value = null;
					return;
				}
				if (propTag != PropTag.RuleFolderEntryID)
				{
					return;
				}
			}
			byte[] array = pvd.Value as byte[];
			if (array != null)
			{
				pvd.Value = (this.TranslateFolderId(array) ?? array);
			}
		}

		// Token: 0x04000027 RID: 39
		private FolderHierarchy sourceHierarchy;

		// Token: 0x04000028 RID: 40
		private FolderHierarchy targetHierarchy;
	}
}
