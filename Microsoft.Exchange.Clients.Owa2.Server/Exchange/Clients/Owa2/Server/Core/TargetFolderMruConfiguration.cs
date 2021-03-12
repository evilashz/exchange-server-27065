using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200040F RID: 1039
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class TargetFolderMruConfiguration
	{
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x0007F054 File Offset: 0x0007D254
		// (set) Token: 0x0600228F RID: 8847 RVA: 0x0007F05C File Offset: 0x0007D25C
		[DataMember]
		public TargetFolderMRUEntry[] FolderMruEntries
		{
			get
			{
				return this.folderMruEntries;
			}
			set
			{
				this.folderMruEntries = value;
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0007F068 File Offset: 0x0007D268
		internal void LoadAll(CallContext callContext)
		{
			SimpleConfiguration<TargetFolderMRUEntry> simpleConfiguration = new SimpleConfiguration<TargetFolderMRUEntry>();
			simpleConfiguration.Load(callContext);
			bool flag = this.ConvertLegacyItemIdFormatIfNecessary(simpleConfiguration, callContext);
			this.PopulateConfigEntries(simpleConfiguration.Entries);
			if (flag)
			{
				this.Save(callContext);
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0007F0A4 File Offset: 0x0007D2A4
		internal void Save(CallContext callContext)
		{
			new SimpleConfiguration<TargetFolderMRUEntry>
			{
				Entries = new List<TargetFolderMRUEntry>(this.folderMruEntries)
			}.Save(callContext);
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0007F0D0 File Offset: 0x0007D2D0
		private bool ConvertLegacyItemIdFormatIfNecessary(SimpleConfiguration<TargetFolderMRUEntry> folderMruConfig, CallContext callContext)
		{
			bool result = false;
			foreach (TargetFolderMRUEntry targetFolderMRUEntry in folderMruConfig.Entries)
			{
				if (targetFolderMRUEntry.EwsFolderIdEntry == null)
				{
					try
					{
						OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(targetFolderMRUEntry.FolderId);
						ExchangePrincipal exchangePrincipal = (owaStoreObjectId.MailboxOwnerLegacyDN != null) ? ExchangePrincipal.FromLegacyDN(callContext.SessionCache.GetMailboxIdentityMailboxSession().GetADSessionSettings(), owaStoreObjectId.MailboxOwnerLegacyDN) : callContext.AccessingPrincipal;
						new IdConverter(callContext);
						FolderId folderIdFromStoreId = IdConverter.GetFolderIdFromStoreId(owaStoreObjectId.StoreId, new MailboxId(exchangePrincipal.MailboxInfo.MailboxGuid));
						targetFolderMRUEntry.EwsFolderIdEntry = folderIdFromStoreId.Id;
						result = true;
					}
					catch (OwaInvalidIdFormatException)
					{
					}
					catch (OwaInvalidRequestException)
					{
					}
					catch (ObjectNotFoundException)
					{
					}
				}
			}
			return result;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007F1C8 File Offset: 0x0007D3C8
		private void PopulateConfigEntries(IList<TargetFolderMRUEntry> entries)
		{
			this.folderMruEntries = new TargetFolderMRUEntry[entries.Count];
			for (int i = 0; i < entries.Count; i++)
			{
				this.folderMruEntries[i] = entries[i];
			}
		}

		// Token: 0x04001329 RID: 4905
		private TargetFolderMRUEntry[] folderMruEntries;
	}
}
