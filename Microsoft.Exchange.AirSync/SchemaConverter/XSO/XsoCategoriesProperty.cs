using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200020A RID: 522
	internal class XsoCategoriesProperty : XsoMultiValuedStringProperty
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x0007487D File Offset: 0x00072A7D
		public XsoCategoriesProperty() : base(ItemSchema.Categories)
		{
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0007488C File Offset: 0x00072A8C
		private MasterCategoryList MasterCategoryList
		{
			get
			{
				if (this.masterCategoryList == null)
				{
					try
					{
						try
						{
							this.masterCategoryList = Command.CurrentCommand.MailboxSession.GetMasterCategoryList();
						}
						catch (CorruptDataException arg)
						{
							AirSyncDiagnostics.TraceDebug<CorruptDataException>(ExTraceGlobals.XsoTracer, this, "Failed to get MCL, exception: {0}", arg);
							Command.CurrentCommand.MailboxSession.DeleteMasterCategoryList();
							this.masterCategoryList = Command.CurrentCommand.MailboxSession.GetMasterCategoryList();
						}
					}
					catch (LocalizedException arg2)
					{
						AirSyncDiagnostics.TraceDebug<LocalizedException>(ExTraceGlobals.XsoTracer, this, "Failed to load MCL, exception: {0}", arg2);
						Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "FailedToLoadMCL");
					}
				}
				return this.masterCategoryList;
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00074940 File Offset: 0x00072B40
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.InternalCopyFromModified(srcProperty);
			IMultivaluedProperty<string> multivaluedProperty = srcProperty as IMultivaluedProperty<string>;
			foreach (string name in multivaluedProperty)
			{
				this.CheckAddCategory(name);
			}
			try
			{
				if (this.needToSaveMCL && this.MasterCategoryList != null)
				{
					this.MasterCategoryList.Save();
					this.needToSaveMCL = false;
				}
			}
			catch (LocalizedException arg)
			{
				AirSyncDiagnostics.TraceDebug<LocalizedException>(ExTraceGlobals.XsoTracer, this, "Failed to save MCL, exception: {0}", arg);
				Command.CurrentCommand.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "FailedToSaveMCL");
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000749F0 File Offset: 0x00072BF0
		private void CheckAddCategory(string name)
		{
			int hashCode = name.GetHashCode();
			if (!Command.CurrentCommand.SyncStatusSyncData.ContainsClientCategoryHash(hashCode))
			{
				Command.CurrentCommand.SyncStatusSyncData.AddClientCategoryHash(hashCode);
				if (this.MasterCategoryList != null && !this.MasterCategoryList.Contains(name))
				{
					Category item = Category.Create(name, 9, true);
					this.MasterCategoryList.Add(item);
					this.needToSaveMCL = true;
				}
				if (!Command.CurrentCommand.ShouldSaveSyncStatus)
				{
					throw new InvalidOperationException(string.Format("ShouldSaveSyncStatus should be true. Current command is {0}.", Command.CurrentCommand.GetType().ToString()));
				}
			}
		}

		// Token: 0x04000C57 RID: 3159
		private const int DefaultColor = 9;

		// Token: 0x04000C58 RID: 3160
		private MasterCategoryList masterCategoryList;

		// Token: 0x04000C59 RID: 3161
		private bool needToSaveMCL;
	}
}
