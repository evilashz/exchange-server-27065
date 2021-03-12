using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FreeEntryIdStrategy : EntryIdStrategy
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0002D39C File Offset: 0x0002B59C
		internal static byte[] GetRootIdDelegate(MailboxSession session)
		{
			return session.Mailbox.MapiStore.GetIpmSubtreeFolderEntryId();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002D3AE File Offset: 0x0002B5AE
		internal static byte[] GetConfigurationIdDelegate(MailboxSession session)
		{
			return session.Mailbox.MapiStore.GetNonIpmSubtreeFolderEntryId();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002D3C0 File Offset: 0x0002B5C0
		internal static byte[] GetInboxIdDelegate(MailboxSession session)
		{
			return session.Mailbox.MapiStore.GetInboxFolderEntryId();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002D3D2 File Offset: 0x0002B5D2
		internal static byte[] GetSpoolerQueueIdDelegate(MailboxSession session)
		{
			return session.Mailbox.MapiStore.GetSpoolerQueueFolderEntryId();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002D3E4 File Offset: 0x0002B5E4
		internal FreeEntryIdStrategy(FreeEntryIdStrategy.GetFreeIdDelegate getFreeId)
		{
			this.getFreeId = getFreeId;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002D3F3 File Offset: 0x0002B5F3
		internal override void GetDependentProperties(object location, IList<StorePropertyDefinition> result)
		{
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002D3F8 File Offset: 0x0002B5F8
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			StoreSession session = context.Session;
			bool flag = false;
			byte[] result;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = this.getFreeId(context.Session);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenFolder, ex, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("FreeEntryIdStrategy::GetEntryId. Hit exception when adding ``free'' default folders.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotOpenFolder, ex2, session, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("FreeEntryIdStrategy::GetEntryId. Hit exception when adding ``free'' default folders.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002D524 File Offset: 0x0002B724
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			throw new NotSupportedException(string.Format("The default folder cannot be changed. Delegate = {0}.", this.getFreeId.ToString()));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002D540 File Offset: 0x0002B740
		internal override FolderSaveResult UnsetEntryId(DefaultFolderContext context)
		{
			throw new NotSupportedException(string.Format("The default folder cannot be unset. Delegate = {0}.", this.getFreeId.ToString()));
		}

		// Token: 0x04000172 RID: 370
		private FreeEntryIdStrategy.GetFreeIdDelegate getFreeId;

		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x0600057D RID: 1405
		internal delegate byte[] GetFreeIdDelegate(MailboxSession session);
	}
}
