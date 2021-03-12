using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000074 RID: 116
	internal abstract class PropertySynchronizerBase
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0001DAF7 File Offset: 0x0001BCF7
		internal PropertySynchronizerBase(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcAssistant)
		{
			this.MailboxDataForTags = mailboxDataForTags;
			this.ElcAssistant = elcAssistant;
			this.ElcUserTagInformation = (ElcUserTagInformation)mailboxDataForTags.ElcUserInformation;
			this.PropertiesToBeDeleted = new List<PropertyDefinition>();
			this.PropertiesToBeUpdated = new PropertiesToBeUpdated();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001DB34 File Offset: 0x0001BD34
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0001DB3C File Offset: 0x0001BD3C
		protected MailboxDataForTags MailboxDataForTags { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001DB45 File Offset: 0x0001BD45
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0001DB4D File Offset: 0x0001BD4D
		protected ElcTagSubAssistant ElcAssistant { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001DB56 File Offset: 0x0001BD56
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0001DB5E File Offset: 0x0001BD5E
		protected ElcUserTagInformation ElcUserTagInformation { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001DB67 File Offset: 0x0001BD67
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0001DB6F File Offset: 0x0001BD6F
		protected object[] FolderProperties { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001DB78 File Offset: 0x0001BD78
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0001DB80 File Offset: 0x0001BD80
		protected Folder Folder { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001DB89 File Offset: 0x0001BD89
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001DB91 File Offset: 0x0001BD91
		protected List<PropertyDefinition> PropertiesToBeDeleted { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001DB9A File Offset: 0x0001BD9A
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001DBA2 File Offset: 0x0001BDA2
		protected PropertiesToBeUpdated PropertiesToBeUpdated { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001DBAB File Offset: 0x0001BDAB
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0001DBB3 File Offset: 0x0001BDB3
		protected string DebugString { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001DBBC File Offset: 0x0001BDBC
		protected string FolderDisplayName
		{
			get
			{
				if (this.folderName == null)
				{
					this.folderName = ((this.FolderProperties[1] is string) ? ((string)this.FolderProperties[1]) : string.Empty);
				}
				return this.folderName;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		protected void AddAdriftTagToFai(Guid adriftTagGuid)
		{
			if (this.ElcUserTagInformation.AllAdTags.ContainsKey(adriftTagGuid) && !this.ElcUserTagInformation.StoreTagDictionary.ContainsKey(adriftTagGuid))
			{
				StoreTagData storeTagData = new StoreTagData(this.ElcUserTagInformation.AllAdTags[adriftTagGuid]);
				storeTagData.IsVisible = false;
				this.ElcUserTagInformation.StoreTagDictionary[adriftTagGuid] = storeTagData;
				PropertySynchronizerBase.Tracer.TraceDebug<PropertySynchronizerBase, string>((long)this.GetHashCode(), "{0}: Explicit tag {1} on folder was not present in FAI. Added it.", this, storeTagData.Tag.Name);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001DC7D File Offset: 0x0001BE7D
		protected void AddDeletedTag(Guid deletedTag)
		{
			if (!this.ElcUserTagInformation.DeletedTags.Contains(deletedTag))
			{
				this.ElcUserTagInformation.DeletedTags.Add(deletedTag);
			}
		}

		// Token: 0x0400034F RID: 847
		protected static readonly Trace Tracer = ExTraceGlobals.TagProvisionerTracer;

		// Token: 0x04000350 RID: 848
		protected static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000351 RID: 849
		private string folderName;

		// Token: 0x04000352 RID: 850
		protected bool taggedByPersonalArchiveTag;

		// Token: 0x04000353 RID: 851
		protected bool taggedByPersonalExpiryTag;

		// Token: 0x04000354 RID: 852
		protected bool taggedByDefaultExpiryTag;

		// Token: 0x04000355 RID: 853
		protected bool taggedBySystemExpiryTag;

		// Token: 0x04000356 RID: 854
		protected bool taggedByUncertainExpiryTag;
	}
}
