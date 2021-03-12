using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000053 RID: 83
	internal class ItemUpdater
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x000120AC File Offset: 0x000102AC
		internal ItemUpdater(MailboxDataForFolders mailboxData, ProvisionedFolder provisionedFolder, ElcSubAssistant elcAssistant)
		{
			this.mailboxData = mailboxData;
			this.provisionedFolder = provisionedFolder;
			this.elcAssistant = elcAssistant;
			this.mailboxOwner = mailboxData.MailboxSession.MailboxOwner;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000120DC File Offset: 0x000102DC
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Concat(new object[]
				{
					"Item updater for mailbox ",
					this.mailboxOwner,
					" for folder ",
					this.provisionedFolder.DisplayName
				});
			}
			return this.toString;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00012134 File Offset: 0x00010334
		internal int SetProperty(List<ItemData> listToSet, PropertyDefinition propertyDefinition, object propertyValue)
		{
			int num = 0;
			foreach (ItemData itemData in listToSet)
			{
				this.provisionedFolder.CurrentItems = new VersionedId[]
				{
					itemData.Id
				};
				this.elcAssistant.ThrowIfShuttingDown(this.mailboxOwner);
				if (this.SetProperty(itemData, propertyDefinition, propertyValue))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012428 File Offset: 0x00010628
		internal bool SetProperty(ItemData item, PropertyDefinition propertyDefinition, object propertyValue)
		{
			ItemUpdater.<>c__DisplayClass2 CS$<>8__locals1 = new ItemUpdater.<>c__DisplayClass2();
			CS$<>8__locals1.item = item;
			CS$<>8__locals1.propertyDefinition = propertyDefinition;
			CS$<>8__locals1.propertyValue = propertyValue;
			CS$<>8__locals1.<>4__this = this;
			ItemUpdater.Tracer.TraceDebug<ItemUpdater, PropertyDefinition, VersionedId>((long)this.GetHashCode(), "{0}: setting property '{1}' on item '{2}'.", this, CS$<>8__locals1.propertyDefinition, CS$<>8__locals1.item.Id);
			CS$<>8__locals1.success = false;
			ILUtil.DoTryFilterCatch(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<SetProperty>b__0)), new FilterDelegate(null, (UIntPtr)ldftn(ExceptionFilter)), new CatchDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<SetProperty>b__1)));
			return CS$<>8__locals1.success;
		}

		// Token: 0x0400026A RID: 618
		private static readonly Trace Tracer = ExTraceGlobals.CommonEnforcerOperationsTracer;

		// Token: 0x0400026B RID: 619
		private MailboxDataForFolders mailboxData;

		// Token: 0x0400026C RID: 620
		private IExchangePrincipal mailboxOwner;

		// Token: 0x0400026D RID: 621
		private ProvisionedFolder provisionedFolder;

		// Token: 0x0400026E RID: 622
		private string toString;

		// Token: 0x0400026F RID: 623
		private ElcSubAssistant elcAssistant;
	}
}
