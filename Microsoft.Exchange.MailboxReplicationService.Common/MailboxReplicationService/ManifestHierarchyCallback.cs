using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000292 RID: 658
	internal class ManifestHierarchyCallback : IMapiHierarchyManifestCallback
	{
		// Token: 0x06002010 RID: 8208 RVA: 0x0004451F File Offset: 0x0004271F
		public ManifestHierarchyCallback(bool isPagedEnumeration)
		{
			this.isPagedEnumeration = isPagedEnumeration;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00044530 File Offset: 0x00042730
		public void InitializeNextPage(MailboxChangesManifest mailboxChangesManifest, int maxChanges)
		{
			this.changes = mailboxChangesManifest;
			this.changes.ChangedFolders = new List<byte[]>((!this.isPagedEnumeration) ? 4 : maxChanges);
			this.changes.DeletedFolders = new List<byte[]>();
			this.maxChanges = maxChanges;
			this.countEnumeratedChanges = 0;
			bool flag = this.isPagedEnumeration;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00044588 File Offset: 0x00042788
		ManifestCallbackStatus IMapiHierarchyManifestCallback.Change(PropValue[] props)
		{
			byte[] item = null;
			if (props != null)
			{
				foreach (PropValue propValue in props)
				{
					PropTag propTag = propValue.PropTag;
					if (propTag == PropTag.EntryId)
					{
						item = propValue.GetBytes();
					}
				}
			}
			this.changes.ChangedFolders.Add(item);
			return this.ReturnManifestCallbackStatus();
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000445E8 File Offset: 0x000427E8
		ManifestCallbackStatus IMapiHierarchyManifestCallback.Delete(byte[] data)
		{
			this.changes.DeletedFolders.Add(data);
			return this.ReturnManifestCallbackStatus();
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00044601 File Offset: 0x00042801
		private ManifestCallbackStatus ReturnManifestCallbackStatus()
		{
			this.countEnumeratedChanges++;
			if (this.isPagedEnumeration && this.countEnumeratedChanges == this.maxChanges)
			{
				this.changes.HasMoreHierarchyChanges = true;
				return ManifestCallbackStatus.Yield;
			}
			return ManifestCallbackStatus.Continue;
		}

		// Token: 0x04000CFC RID: 3324
		private readonly bool isPagedEnumeration;

		// Token: 0x04000CFD RID: 3325
		private int maxChanges;

		// Token: 0x04000CFE RID: 3326
		private int countEnumeratedChanges;

		// Token: 0x04000CFF RID: 3327
		private MailboxChangesManifest changes;
	}
}
