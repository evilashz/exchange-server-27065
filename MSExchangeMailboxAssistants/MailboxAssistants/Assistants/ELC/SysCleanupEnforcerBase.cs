using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000095 RID: 149
	internal abstract class SysCleanupEnforcerBase
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x0002B759 File Offset: 0x00029959
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("Mailbox:{0} being processed by {1}.", this.MailboxDataForTags.MailboxSession.MailboxOwner, base.GetType().Name);
			}
			return this.toString;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002B794 File Offset: 0x00029994
		internal SysCleanupEnforcerBase(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant)
		{
			this.MailboxDataForTags = mailboxDataForTags;
			this.SysCleanupSubAssistant = sysCleanupSubAssistant;
			this.TagExpirationExecutor = new TagExpirationExecutor(mailboxDataForTags, sysCleanupSubAssistant);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0002B7B7 File Offset: 0x000299B7
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0002B7BF File Offset: 0x000299BF
		protected MailboxDataForTags MailboxDataForTags { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0002B7C8 File Offset: 0x000299C8
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0002B7D0 File Offset: 0x000299D0
		protected SysCleanupSubAssistant SysCleanupSubAssistant { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0002B7D9 File Offset: 0x000299D9
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0002B7E1 File Offset: 0x000299E1
		protected TagExpirationExecutor TagExpirationExecutor { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0002B7EA File Offset: 0x000299EA
		protected bool IsEnabled
		{
			get
			{
				if (this.isEnabled == null)
				{
					this.isEnabled = new bool?(this.QueryIsEnabled());
				}
				return this.isEnabled.Value;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002B818 File Offset: 0x00029A18
		internal void Invoke()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.StartPerfCounterCollect();
				this.InvokeInternal();
			}
			finally
			{
				this.StopPerfCounterCollect(stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002B858 File Offset: 0x00029A58
		protected virtual void InvokeInternal()
		{
			if (this.IsEnabled)
			{
				this.CollectItemsWithGuard();
				this.ExpireItemsAlready();
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002B86E File Offset: 0x00029A6E
		protected virtual void CollectItemsToExpire()
		{
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0002B870 File Offset: 0x00029A70
		protected virtual void ExpireItemsAlready()
		{
			this.TagExpirationExecutor.ExecuteTheDoomed();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0002B87D File Offset: 0x00029A7D
		protected virtual void StartPerfCounterCollect()
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002B87F File Offset: 0x00029A7F
		protected virtual void StopPerfCounterCollect(long timeElapsed)
		{
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0002B881 File Offset: 0x00029A81
		protected virtual bool QueryIsEnabled()
		{
			return true;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0002B884 File Offset: 0x00029A84
		protected int FolderItemTypeCount(Folder folder, ItemQueryType queryType)
		{
			if (queryType != ItemQueryType.None && queryType != ItemQueryType.Associated)
			{
				SysCleanupEnforcerBase.Tracer.TraceDebug<SysCleanupEnforcerBase, string, ItemQueryType>((long)this.GetHashCode(), "{0}:{1} Folder is Empty of type {2}", this, folder.Id.ObjectId.ToHexEntryId(), queryType);
				return 0;
			}
			StorePropertyDefinition propertyDefinition;
			if (queryType == ItemQueryType.Associated)
			{
				propertyDefinition = FolderSchema.AssociatedItemCount;
			}
			else
			{
				propertyDefinition = FolderSchema.ItemCount;
			}
			return (int)folder[propertyDefinition];
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002B8E0 File Offset: 0x00029AE0
		private void CollectItemsWithGuard()
		{
			Exception ex = null;
			try
			{
				this.CollectItemsToExpire();
			}
			catch (ObjectNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (InvalidFolderLanguageIdException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				SysCleanupEnforcerBase.Tracer.TraceDebug<SysCleanupEnforcerBase, Exception>((long)this.GetHashCode(), "{0}: Failed to get items from the folder because the folder was not found or was inaccessible. Exception: '{1}'", this, ex);
			}
		}

		// Token: 0x0400043D RID: 1085
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.CommonCleanupEnforcerOperationsTracer;

		// Token: 0x0400043E RID: 1086
		private static readonly Microsoft.Exchange.Diagnostics.Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400043F RID: 1087
		private bool? isEnabled;

		// Token: 0x04000440 RID: 1088
		private string toString;
	}
}
