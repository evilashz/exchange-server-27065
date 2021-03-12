using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SpoolerManager
	{
		// Token: 0x060002EE RID: 750 RVA: 0x00016DCB File Offset: 0x00014FCB
		internal SpoolerManager(StoreSession session)
		{
			this.session = session;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00016DDC File Offset: 0x00014FDC
		public void SetSpooler()
		{
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.session.Mailbox.MapiStore.SetSpooler();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetSpooler, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MapiStore.SetSpooler failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetSpooler, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MapiStore.SetSpooler failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
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
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00016F08 File Offset: 0x00015108
		public void TransportNewMail(StoreObjectId messageId, string messageClass, int messageFlags)
		{
			Util.ThrowOnNullArgument(messageId, "messageId");
			Util.ThrowOnNullArgument(messageClass, "messageClass");
			byte[] providerLevelItemId = messageId.ProviderLevelItemId;
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.session.Mailbox.MapiStore.SpoolerNotifyMessageNewMail(providerLevelItemId, messageClass, messageFlags);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotNotifyMessageNewMail, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MapiStore.SpoolerNotifyMessageNewMail failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotNotifyMessageNewMail, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MapiStore.SpoolerNotifyMessageNewMail failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
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
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00017054 File Offset: 0x00015254
		public void SpoolerLockMessage(StoreObjectId messageId, SpoolerMessageLockState lockState)
		{
			EnumValidator.ThrowIfInvalid<SpoolerMessageLockState>(lockState, "lockState");
			Util.ThrowOnNullArgument(messageId, "messageId");
			MapiStore.MessageLockState messageLockState = MapiStore.MessageLockState.Lock;
			switch (lockState)
			{
			case SpoolerMessageLockState.Lock:
				messageLockState = MapiStore.MessageLockState.Lock;
				break;
			case SpoolerMessageLockState.Unlock:
				messageLockState = MapiStore.MessageLockState.Unlock;
				break;
			case SpoolerMessageLockState.Finished:
				messageLockState = MapiStore.MessageLockState.Finished;
				break;
			}
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				this.session.Mailbox.MapiStore.SpoolerSetMessageLockState(messageId.ProviderLevelItemId, messageLockState);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetMessageLockState, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MapiStore.SpoolerSetMessageLockState failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetMessageLockState, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("MapiStore.SpoolerSetMessageLockState failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
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
		}

		// Token: 0x040000C0 RID: 192
		private readonly StoreSession session;
	}
}
