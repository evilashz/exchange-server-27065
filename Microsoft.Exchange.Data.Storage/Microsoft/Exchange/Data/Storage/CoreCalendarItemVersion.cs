using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CoreCalendarItemVersion : DisposableObject
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000254B2 File Offset: 0x000236B2
		private CoreCalendarItemVersion() : this(null)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000254BB File Offset: 0x000236BB
		private CoreCalendarItemVersion(StoreSession session)
		{
			this.session = session;
			this.underlyingMessage = null;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000254D4 File Offset: 0x000236D4
		internal static StoreObjectId CreateVersion(StoreSession session, StoreObjectId itemId, StoreObjectId versionFolderId)
		{
			StoreObjectId result;
			using (CoreCalendarItemVersion coreCalendarItemVersion = new CoreCalendarItemVersion(session))
			{
				using (MapiProp mapiProp = session.GetMapiProp(itemId, OpenEntryFlags.DeferredErrors | OpenEntryFlags.ShowSoftDeletes))
				{
					coreCalendarItemVersion.underlyingMessage = Folder.InternalCreateMapiMessage(session, versionFolderId, CreateMessageType.Normal);
					coreCalendarItemVersion.CopyRequiredData((MapiMessage)mapiProp);
					object thisObject = null;
					bool flag = false;
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
						coreCalendarItemVersion.underlyingMessage.SetReadFlag(SetReadFlags.ClearRnPending | SetReadFlags.CleanNrnPending);
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex, session, thisObject, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("CoreCalendarItemVersion::CreateVersion. Failed to set read flag of underlying message.", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSetReadFlags, ex2, session, thisObject, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("CoreCalendarItemVersion::CreateVersion. Failed to set read flag of underlying message.", new object[0]),
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
				}
				coreCalendarItemVersion.Save();
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(coreCalendarItemVersion.underlyingMessage.GetProp(PropTag.EntryId).GetBytes(), itemId.ObjectType);
				CoreCalendarItemVersion.perfCounters.DumpsterCalendarLogsRate.Increment();
				result = storeObjectId;
			}
			return result;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000256E0 File Offset: 0x000238E0
		private void CopyRequiredData(MapiMessage sourceMessage)
		{
			CoreObject.MapiCopyTo(sourceMessage, this.underlyingMessage, this.session, this.session, CopyPropertiesFlags.None, CopySubObjects.Copy, CoreCalendarItemVersion.ItemBodyAndAttachmentProperties);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00025704 File Offset: 0x00023904
		private void Save()
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
				this.underlyingMessage.SaveChanges();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSaveChanges, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreCalendarItemVersion::Save. Failed to save the underlying message.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotSaveChanges, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("CoreCalendarItemVersion::Save. Failed to save the underlying message.", new object[0]),
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

		// Token: 0x06000412 RID: 1042 RVA: 0x00025824 File Offset: 0x00023A24
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CoreCalendarItemVersion>(this);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0002582C File Offset: 0x00023A2C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.underlyingMessage != null)
			{
				this.underlyingMessage.Dispose();
				this.underlyingMessage = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0400013C RID: 316
		private static readonly NativeStorePropertyDefinition[] ItemBodyAndAttachmentProperties = new NativeStorePropertyDefinition[]
		{
			InternalSchema.HtmlBody,
			InternalSchema.RtfBody,
			InternalSchema.TextBody,
			InternalSchema.MessageDeepAttachments
		};

		// Token: 0x0400013D RID: 317
		private static readonly MiddleTierStoragePerformanceCountersInstance perfCounters = DumpsterFolderHelper.GetPerfCounters();

		// Token: 0x0400013E RID: 318
		private readonly StoreSession session;

		// Token: 0x0400013F RID: 319
		private MapiMessage underlyingMessage;
	}
}
