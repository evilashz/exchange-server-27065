using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000499 RID: 1177
	internal abstract class ItemEventHandler : OwaEventHandlerBase
	{
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000FEB0B File Offset: 0x000FCD0B
		protected AnrManager.Options AnrOptions
		{
			get
			{
				return this.anrOptions;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000FEB13 File Offset: 0x000FCD13
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, false)]
		[OwaEvent("Delete")]
		public virtual void Delete()
		{
			this.DoDelete(false);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000FEB1C File Offset: 0x000FCD1C
		[OwaEvent("PermanentDelete")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, false)]
		public virtual void PermanentDelete()
		{
			this.DoDelete(true);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000FEB28 File Offset: 0x000FCD28
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEvent("CleanupDelete")]
		[OwaEventParameter("CK", typeof(string))]
		public virtual void CleanupDelete()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ItemEventHandler.CleanupDelete");
			Item item = null;
			try
			{
				item = this.GetReadOnlyRequestItem<Item>(new PropertyDefinition[0]);
				string strB = (string)base.GetParameter("CK");
				if (string.CompareOrdinal(item.Id.ChangeKeyAsBase64String(), strB) != 0)
				{
					return;
				}
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
					item = null;
				}
			}
			this.DoDelete(true, false);
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000FEBA8 File Offset: 0x000FCDA8
		protected bool GetExchangeParticipantsFromRecipientInfo(RecipientInfo recipientInfo, List<Participant> exchangeParticipants)
		{
			bool flag = false;
			Participant item = null;
			this.AnrOptions.ResolveContactsFirst = base.UserContext.UserOptions.CheckNameInContactsFirst;
			if (recipientInfo.PendingChunk != null)
			{
				RecipientCache recipientCache = AutoCompleteCache.TryGetCache(OwaContext.Current.UserContext);
				ArrayList arrayList = new ArrayList();
				RecipientWell.ResolveAndRenderChunk(this.Writer, recipientInfo.PendingChunk, arrayList, recipientCache, base.UserContext, this.AnrOptions);
				for (int i = 0; i < arrayList.Count; i++)
				{
					RecipientWellNode recipientWellNode = (RecipientWellNode)arrayList[i];
					flag |= Utilities.CreateExchangeParticipant(out item, recipientWellNode.DisplayName, recipientWellNode.RoutingAddress, recipientWellNode.RoutingType, recipientWellNode.AddressOrigin, recipientWellNode.StoreObjectId, recipientWellNode.EmailAddressIndex);
					exchangeParticipants.Add(item);
				}
			}
			else
			{
				flag |= recipientInfo.ToParticipant(out item);
				exchangeParticipants.Add(item);
			}
			return flag;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000FEC88 File Offset: 0x000FCE88
		protected T GetRequestItem<T>(params PropertyDefinition[] prefetchProperties) where T : Item
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string changeKey = (string)base.GetParameter("CK");
			return Utilities.GetItem<T>(base.UserContext, owaStoreObjectId, changeKey, prefetchProperties);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000FECC5 File Offset: 0x000FCEC5
		protected T GetRequestItem<T>(VersionedId versionedId, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.GetRequestItem<T>(versionedId, ItemBindOption.None, prefetchProperties);
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000FECD0 File Offset: 0x000FCED0
		protected T GetRequestItem<T>(VersionedId versionedId, ItemBindOption itemBindOption, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(base.UserContext, versionedId, itemBindOption, prefetchProperties);
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000FECE0 File Offset: 0x000FCEE0
		protected T GetReadOnlyRequestItem<T>(params PropertyDefinition[] prefetchProperties) where T : Item
		{
			OwaStoreObjectId itemId = (OwaStoreObjectId)base.GetParameter("Id");
			return this.GetReadOnlyRequestItem<T>(itemId, prefetchProperties);
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000FED06 File Offset: 0x000FCF06
		protected T GetReadOnlyRequestItem<T>(OwaStoreObjectId itemId, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return Utilities.GetItem<T>(base.UserContext, itemId, prefetchProperties);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000FED18 File Offset: 0x000FCF18
		protected void MoveItemToDestinationFolderIfInScratchPad(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			OwaStoreObjectId owaStoreObjectId = base.GetParameter("fId") as OwaStoreObjectId;
			if (owaStoreObjectId == null)
			{
				return;
			}
			if (!owaStoreObjectId.IsPublic)
			{
				return;
			}
			item.Load();
			if (item.ParentId.Equals(owaStoreObjectId.StoreObjectId))
			{
				return;
			}
			OwaStoreObjectId owaStoreObjectId2 = OwaStoreObjectId.CreateFromStoreObject(item);
			OperationResult operationResult = Utilities.CopyOrMoveItems(base.UserContext, false, owaStoreObjectId, new OwaStoreObjectId[]
			{
				owaStoreObjectId2
			}).OperationResult;
			if (operationResult == OperationResult.Succeeded)
			{
				this.Writer.Write("<div id=divFC>");
				this.Writer.Write(LocalizedStrings.GetHtmlEncoded(-1580283653));
				this.Writer.Write("</div>");
				return;
			}
			this.SaveIdAndChangeKeyInCustomErrorInfo(item);
			throw new OwaEventHandlerException("Could not move the item out from scratch pad into target folder", LocalizedStrings.GetNonEncoded(1665365872), true);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000FEDE8 File Offset: 0x000FCFE8
		protected void SaveIdAndChangeKeyInCustomErrorInfo(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			item.Load();
			if (item.Id == null)
			{
				ExTraceGlobals.MailDataTracer.TraceDebug((long)this.GetHashCode(), "ItemEventHandler.SaveIdAndChangeKeyInCustomErrorInfo: Unable to send id and change key, item Id is null");
				return;
			}
			dictionary.Add("itemId", Utilities.GetIdAsString(item));
			dictionary.Add("ck", item.Id.ChangeKeyAsBase64String());
			base.OwaContext.CustomErrorInfo = dictionary;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000FEE61 File Offset: 0x000FD061
		private void DoDelete(bool permanentDelete)
		{
			this.DoDelete(permanentDelete, true);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000FEE6C File Offset: 0x000FD06C
		private void DoDelete(bool permanentDelete, bool doThrow)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ItemEventHandler." + (permanentDelete ? "PermanentDelete" : "Delete"));
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			Utilities.Delete(base.UserContext, permanentDelete, doThrow, new OwaStoreObjectId[]
			{
				owaStoreObjectId
			});
		}

		// Token: 0x04001E0B RID: 7691
		public const string MethodAutoSave = "AutoSave";

		// Token: 0x04001E0C RID: 7692
		public const string MethodSave = "Save";

		// Token: 0x04001E0D RID: 7693
		public const string MethodDelete = "Delete";

		// Token: 0x04001E0E RID: 7694
		public const string MethodPromoteInlineAttachments = "PromoteInlineAttachments";

		// Token: 0x04001E0F RID: 7695
		public const string MethodCleanupDelete = "CleanupDelete";

		// Token: 0x04001E10 RID: 7696
		public const string MethodPermanentDelete = "PermanentDelete";

		// Token: 0x04001E11 RID: 7697
		public const string Id = "Id";

		// Token: 0x04001E12 RID: 7698
		public const string ChangeKey = "CK";

		// Token: 0x04001E13 RID: 7699
		public const string FolderID = "fId";

		// Token: 0x04001E14 RID: 7700
		private AnrManager.Options anrOptions = new AnrManager.Options();
	}
}
