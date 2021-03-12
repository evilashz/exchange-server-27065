using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecipientCacheContactWriter : BaseComponent
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004A0B File Offset: 0x00002C0B
		internal RecipientCacheContactWriter()
		{
			this.DiagnosticsSession.ComponentName = "RecipientCacheContactWriter";
			this.DiagnosticsSession.Tracer = ExTraceGlobals.RecipientCacheContactWriterTracer;
			this.MaxContactUpdatesCount = PeopleRelevanceConfig.Instance.MaxContactUpdatesCount;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004A43 File Offset: 0x00002C43
		public override string Description
		{
			get
			{
				return "RecipientCacheContactWriter updates the recipient cache contacts based on the capture flags set byt PeopleRelevanceClassifier.";
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004A4A File Offset: 0x00002C4A
		public override string Name
		{
			get
			{
				return "RecipientCacheContactWriter";
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004A51 File Offset: 0x00002C51
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004A59 File Offset: 0x00002C59
		public int MaxContactUpdatesCount { get; set; }

		// Token: 0x060000CA RID: 202 RVA: 0x00004A64 File Offset: 0x00002C64
		protected override void InternalProcessDocument(DocumentContext data)
		{
			this.DiagnosticsSession.TraceDebug<IIdentity>("Processing document - {0}", data.Document.Identity);
			DocumentProcessingContext documentProcessingContext = (DocumentProcessingContext)data.AsyncResult.AsyncState;
			Util.ThrowOnNullArgument(documentProcessingContext, "processingContext");
			Util.ThrowOnNullArgument(documentProcessingContext.Session, "session");
			string text = documentProcessingContext.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			object obj;
			if (!data.Document.TryGetProperty(PeopleRelevanceSchema.ContactList, out obj))
			{
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Contact list is empty. No Recipient Cache changes to process.", text), new object[0]);
				return;
			}
			this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Processing the Recipient Cache chages.", text), new object[0]);
			IDictionary<string, IInferenceRecipient> contactList = (IDictionary<string, IInferenceRecipient>)obj;
			data.Document.TryGetProperty(PeopleRelevanceSchema.TopRankedContacts, out obj);
			IList<string> topRanked = (IList<string>)obj;
			RecipientCacheContactWriter.RecipientCacheContactWriterContext recipientCacheContactWriterContext = new RecipientCacheContactWriter.RecipientCacheContactWriterContext(documentProcessingContext.Session, this.DiagnosticsSession)
			{
				ItemsToDelete = new Dictionary<StoreId, IInferenceRecipient>(),
				DeletedItemsToDelete = new HashSet<StoreId>()
			};
			try
			{
				this.ValidateRecipientCache(data, contactList, topRanked, recipientCacheContactWriterContext);
				RecipientCacheContactWriter.RecipientCacheChangeList recipientCacheChangeList = this.DetermineChangeList(contactList, topRanked, recipientCacheContactWriterContext);
				this.ProcessStoreChanges(recipientCacheChangeList, recipientCacheContactWriterContext);
				this.LogRecipientCacheUpdateSummary(recipientCacheChangeList, text);
			}
			finally
			{
				recipientCacheContactWriterContext.DisposeAll();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004BC4 File Offset: 0x00002DC4
		private static void ClearCaptureFlags(IInferenceRecipient recipient)
		{
			recipient.CaptureFlag = 0;
			recipient.RelevanceCategoryAtLastCapture = recipient.RelevanceCategory;
			recipient.HasUpdatedData = false;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004BE0 File Offset: 0x00002DE0
		private static bool IsGlobalException(LocalizedException exception)
		{
			return exception is MailboxUnavailableException || exception is SessionDeadException || exception is ConnectionFailedPermanentException || exception is ObjectNotInitializedException;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004C08 File Offset: 0x00002E08
		private static StoreId FindContactAndRemoveDuplicates(IInferenceRecipient recipient, bool deleteAll, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			if (!context.SortedRecipientCacheData.ContainsKey(recipient.SmtpAddress))
			{
				return null;
			}
			StoreId storeId = (StoreId)context.SortedRecipientCacheData[recipient.SmtpAddress][0][0];
			if (deleteAll)
			{
				context.ItemsToDelete[storeId] = recipient;
			}
			RecipientCacheContactWriter.RemoveDuplicates(recipient.SmtpAddress, context);
			return storeId;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004C68 File Offset: 0x00002E68
		private static void RemoveDuplicates(string smtp, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			for (int i = 1; i < context.SortedRecipientCacheData[smtp].Count; i++)
			{
				StoreId key = (StoreId)context.SortedRecipientCacheData[smtp][i][0];
				context.ItemsToDelete[key] = null;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004CB8 File Offset: 0x00002EB8
		private static void SetContactProperties(Contact contact, IInferenceRecipient recipient)
		{
			string displayName;
			if (!string.IsNullOrEmpty(recipient.DisplayName))
			{
				displayName = recipient.DisplayName;
			}
			else
			{
				displayName = recipient.SmtpAddress;
			}
			contact.EmailAddresses[EmailAddressIndex.Email1] = new Participant(displayName, recipient.SmtpAddress, "SMTP");
			contact.DisplayName = displayName;
			contact.PersonType = RecipientCacheContactWriter.GetPersonType(recipient);
			contact.RelevanceScore = recipient.RecipientRank;
			contact.PartnerNetworkId = WellKnownNetworkNames.RecipientCache;
			contact.SetOrDeleteProperty(ContactSchema.IMAddress, recipient.SipUri);
			contact.SetOrDeleteProperty(ContactSchema.Account, recipient.Alias);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004D4C File Offset: 0x00002F4C
		private static PersonType GetPersonType(IInferenceRecipient recipient)
		{
			if (recipient.IsDistributionList)
			{
				return PersonType.DistributionList;
			}
			RecipientDisplayType recipientDisplayType = recipient.RecipientDisplayType;
			if (recipientDisplayType == RecipientDisplayType.SyncedConferenceRoomMailbox || recipientDisplayType == RecipientDisplayType.ConferenceRoomMailbox)
			{
				return PersonType.Room;
			}
			if (recipientDisplayType != RecipientDisplayType.GroupMailboxUser)
			{
				return PersonType.Person;
			}
			return PersonType.ModernGroup;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004D84 File Offset: 0x00002F84
		private static bool UpdateEmailAddress(Contact contact, string oldEmailAddress, string newEmailAddress, string oldEmailAddressDisplayName, string newDisplayName, StringBuilder updates)
		{
			if ((!string.IsNullOrEmpty(newEmailAddress) && !StringComparer.OrdinalIgnoreCase.Equals(newEmailAddress, oldEmailAddress)) || (!string.IsNullOrEmpty(newDisplayName) && !StringComparer.OrdinalIgnoreCase.Equals(newDisplayName, oldEmailAddressDisplayName)))
			{
				contact.EmailAddresses[EmailAddressIndex.Email1] = new Participant(newDisplayName, newEmailAddress, "SMTP");
				updates.AppendFormat("E:{0}->{1};", oldEmailAddress, newEmailAddress);
				return true;
			}
			return false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004DEA File Offset: 0x00002FEA
		private static bool UpdateDisplayName(Contact contact, string oldDisplayName, string displayName, StringBuilder updates)
		{
			if (!string.IsNullOrEmpty(displayName) && !StringComparer.OrdinalIgnoreCase.Equals(displayName, oldDisplayName))
			{
				contact.DisplayName = displayName;
				updates.AppendFormat("D:{0}->{1};", oldDisplayName, displayName);
				return true;
			}
			return false;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004E1C File Offset: 0x0000301C
		private static bool UpdateIMAddress(Contact contact, string newIMAddress, StringBuilder updates)
		{
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.IMAddress, string.Empty);
			if (!string.IsNullOrEmpty(newIMAddress) && !StringComparer.OrdinalIgnoreCase.Equals(newIMAddress, valueOrDefault))
			{
				contact.ImAddress = newIMAddress;
				updates.AppendFormat("I:{0}->{1};", valueOrDefault, newIMAddress);
				return true;
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E68 File Offset: 0x00003068
		private static bool UpdateAlias(Contact contact, string newAlias, StringBuilder updates)
		{
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.Account, string.Empty);
			if (!string.IsNullOrEmpty(newAlias) && !StringComparer.OrdinalIgnoreCase.Equals(newAlias, valueOrDefault))
			{
				contact.SafeSetProperty(ContactSchema.Account, newAlias);
				updates.AppendFormat("A:{0}->{1};", valueOrDefault, newAlias);
				return true;
			}
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004EBC File Offset: 0x000030BC
		private static bool UpdateRelevance(Contact contact, int newRelevance, StringBuilder updates)
		{
			object obj = contact.TryGetProperty(ContactSchema.RelevanceScore);
			bool flag = false;
			int num;
			if (obj is PropertyError)
			{
				flag = true;
				num = int.MaxValue;
			}
			else
			{
				num = (int)obj;
				if (newRelevance != num)
				{
					flag = true;
				}
			}
			if (flag)
			{
				contact.SafeSetProperty(ContactSchema.RelevanceScore, newRelevance);
				updates.AppendFormat("R:{0}->{1};", num, newRelevance);
				return true;
			}
			return false;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004F28 File Offset: 0x00003128
		private static bool UpdatePersonaType(Contact contact, IInferenceRecipient recipient, StringBuilder updates)
		{
			object obj = contact.TryGetProperty(ContactSchema.PersonType);
			PersonType personType = PersonType.Unknown;
			PersonType personType2 = RecipientCacheContactWriter.GetPersonType(recipient);
			bool flag;
			if (obj is PropertyError)
			{
				flag = true;
			}
			else
			{
				personType = (PersonType)obj;
				flag = (personType != personType2);
			}
			if (flag)
			{
				contact.PersonType = personType2;
				updates.AppendFormat("L:{0}->{1};", personType, personType2);
				return true;
			}
			return false;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004F8C File Offset: 0x0000318C
		private static bool UpdateNetworkId(Contact contact, StringBuilder updates)
		{
			string valueOrDefault = contact.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			if (!StringComparer.Ordinal.Equals(WellKnownNetworkNames.RecipientCache, valueOrDefault))
			{
				contact.SafeSetProperty(ContactSchema.PartnerNetworkId, WellKnownNetworkNames.RecipientCache);
				updates.AppendFormat("N:{0}->{1};", valueOrDefault, WellKnownNetworkNames.RecipientCache);
				return true;
			}
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004FE4 File Offset: 0x000031E4
		private static bool UpdateContactProperties(Contact contact, IInferenceRecipient recipient, out string updatedProperties)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string valueOrDefault = contact.GetValueOrDefault<string>(StoreObjectSchema.DisplayName, string.Empty);
			string valueOrDefault2 = contact.GetValueOrDefault<string>(ContactSchema.Email1EmailAddress, string.Empty);
			string valueOrDefault3 = contact.GetValueOrDefault<string>(ContactSchema.Email1DisplayName, string.Empty);
			string text;
			if (!string.IsNullOrEmpty(recipient.DisplayName))
			{
				text = recipient.DisplayName;
			}
			else if (string.IsNullOrEmpty(valueOrDefault3))
			{
				text = recipient.SmtpAddress;
			}
			else
			{
				text = valueOrDefault3;
			}
			bool flag = RecipientCacheContactWriter.UpdateEmailAddress(contact, valueOrDefault2, recipient.SmtpAddress, valueOrDefault3, text, stringBuilder);
			bool flag2 = RecipientCacheContactWriter.UpdateDisplayName(contact, valueOrDefault, text, stringBuilder);
			bool flag3 = RecipientCacheContactWriter.UpdateIMAddress(contact, recipient.SipUri, stringBuilder);
			bool flag4 = RecipientCacheContactWriter.UpdateAlias(contact, recipient.Alias, stringBuilder);
			bool flag5 = RecipientCacheContactWriter.UpdateRelevance(contact, recipient.RecipientRank, stringBuilder);
			bool flag6 = RecipientCacheContactWriter.UpdatePersonaType(contact, recipient, stringBuilder);
			bool flag7 = RecipientCacheContactWriter.UpdateNetworkId(contact, stringBuilder);
			updatedProperties = stringBuilder.ToString();
			return flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000050DC File Offset: 0x000032DC
		private static void DeleteRecipientCacheContact(IInferenceRecipient recipient, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			Util.ThrowOnConditionFailed(recipient.RecipientRank == int.MaxValue, "Delete flag should be applied only for irrelevant entries");
			if (RecipientCacheContactWriter.FindContactAndRemoveDuplicates(recipient, true, context) == null)
			{
				RecipientCacheContactWriter.ClearCaptureFlags(recipient);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005112 File Offset: 0x00003312
		private static string GetLowercaseStringProperty(object property)
		{
			if (property == null || property is PropertyError)
			{
				return string.Empty;
			}
			return ((string)property).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005138 File Offset: 0x00003338
		private void EnsureRecipientCacheContactUpToDate(IInferenceRecipient recipient, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			StoreId storeId = RecipientCacheContactWriter.FindContactAndRemoveDuplicates(recipient, false, context);
			try
			{
				if (storeId == null)
				{
					using (Contact contact = Contact.Create(context.Session, context.Session.GetDefaultFolderId(DefaultFolderType.RecipientCache)))
					{
						RecipientCacheContactWriter.SetContactProperties(contact, recipient);
						context.BulkAutomaticLink.Link(contact);
						contact.Save(SaveMode.NoConflictResolutionForceSave);
						contact.Load();
						context.BulkAutomaticLink.NotifyContactSaved(contact);
						goto IL_C8;
					}
				}
				using (Contact contact2 = Contact.Bind(context.Session, storeId, RecipientCacheContactWriter.QueryProperties))
				{
					string arg = null;
					if (RecipientCacheContactWriter.UpdateContactProperties(contact2, recipient, out arg))
					{
						contact2.Save(SaveMode.NoConflictResolutionForceSave);
						this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Effective changes:{1}", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, arg), new object[0]);
					}
				}
				IL_C8:;
			}
			catch (MessageSubmissionExceededException ex)
			{
				this.DiagnosticsSession.TraceError<MessageSubmissionExceededException>("Received MessageSubmissionExceeded exception - {0}", ex);
				this.DiagnosticsSession.SendInformationalWatsonReport(ex, "Recipient Cache contact cannot be saved to store (EnsureRecipientCacheContactUpToDate)");
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005260 File Offset: 0x00003460
		private void ProcessChange(IInferenceRecipient recipient, RecipientCacheContactWriter.RecipientCacheContactWriterContext context, RecipientCacheContactWriter.RecipientCacheChangeList changeList)
		{
			CaptureFlag captureFlag = (CaptureFlag)recipient.CaptureFlag;
			if (captureFlag == CaptureFlag.None && recipient.HasUpdatedData)
			{
				captureFlag = CaptureFlag.Update;
			}
			switch (captureFlag)
			{
			case CaptureFlag.Add:
				this.EnsureRecipientCacheContactUpToDate(recipient, context);
				RecipientCacheContactWriter.ClearCaptureFlags(recipient);
				changeList.AddsProcessed++;
				return;
			case CaptureFlag.Update:
				this.EnsureRecipientCacheContactUpToDate(recipient, context);
				RecipientCacheContactWriter.ClearCaptureFlags(recipient);
				changeList.UpdatesProcessed++;
				return;
			case CaptureFlag.Delete:
				RecipientCacheContactWriter.DeleteRecipientCacheContact(recipient, context);
				return;
			}
			Util.ThrowOnConditionFailed(false, "Invalid capture flag " + recipient.CaptureFlag);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005320 File Offset: 0x00003520
		private RecipientCacheContactWriter.RecipientCacheChangeList DetermineChangeList(IDictionary<string, IInferenceRecipient> contactList, IList<string> topRanked, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			RecipientCacheContactWriter.RecipientCacheChangeList recipientCacheChangeList = new RecipientCacheContactWriter.RecipientCacheChangeList(this.MaxContactUpdatesCount);
			List<KeyValuePair<int, IInferenceRecipient>> list = new List<KeyValuePair<int, IInferenceRecipient>>();
			foreach (string text in context.DeletedRecipientCacheItems.Keys)
			{
				if (contactList.ContainsKey(text))
				{
					IInferenceRecipient inferenceRecipient = contactList[text];
					if (inferenceRecipient.CaptureFlag == 4 || (inferenceRecipient.CaptureFlag == 0 && inferenceRecipient.RecipientRank == 2147483647))
					{
						this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Deleted recipient cache entry for {1} is already irrelevant or marked for deletion", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, text), new object[0]);
					}
					else
					{
						this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Deleted recipient cache entry for {1} found, and deletion triggered.", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, text), new object[0]);
						inferenceRecipient.RecipientRank = int.MaxValue;
						inferenceRecipient.RawRecipientWeight = 0.0;
						inferenceRecipient.CaptureFlag = 4;
					}
				}
				else
				{
					this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Deleted recipient cache entry for {1} is ignored, as the corresponding contact does not exist in the inference model", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, text), new object[0]);
				}
				context.DeletedItemsToDelete.Add(context.DeletedRecipientCacheItems[text]);
			}
			if (topRanked != null && topRanked.Count > 0)
			{
				bool flag = false;
				foreach (string key in topRanked)
				{
					IInferenceRecipient inferenceRecipient2 = contactList[key];
					CaptureFlag captureFlag = (CaptureFlag)inferenceRecipient2.CaptureFlag;
					switch (captureFlag)
					{
					case CaptureFlag.None:
						if (inferenceRecipient2.HasUpdatedData)
						{
							list.Add(new KeyValuePair<int, IInferenceRecipient>(int.MaxValue, inferenceRecipient2));
						}
						break;
					case CaptureFlag.Add:
						recipientCacheChangeList.AddRecipientToProcessingList(inferenceRecipient2, this.DiagnosticsSession, context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
						if (recipientCacheChangeList.QuotaRemaining == 0)
						{
							flag = true;
						}
						break;
					case CaptureFlag.Update:
						list.Add(new KeyValuePair<int, IInferenceRecipient>(Math.Abs(inferenceRecipient2.RelevanceCategory - inferenceRecipient2.RelevanceCategoryAtLastCapture), inferenceRecipient2));
						break;
					case (CaptureFlag)3:
						goto IL_242;
					case CaptureFlag.Delete:
						break;
					default:
						goto IL_242;
					}
					IL_259:
					if (!flag)
					{
						continue;
					}
					break;
					IL_242:
					Util.ThrowOnConditionFailed(false, "Invalid capture flag " + captureFlag);
					goto IL_259;
				}
			}
			list.Sort((KeyValuePair<int, IInferenceRecipient> a, KeyValuePair<int, IInferenceRecipient> b) => a.Key.CompareTo(b.Key));
			foreach (KeyValuePair<int, IInferenceRecipient> keyValuePair in list)
			{
				if (recipientCacheChangeList.QuotaRemaining == 0)
				{
					break;
				}
				recipientCacheChangeList.AddRecipientToProcessingList(keyValuePair.Value, this.DiagnosticsSession, context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			int num = int.MaxValue;
			if (topRanked != null && topRanked.Count > 0)
			{
				num = contactList[topRanked[topRanked.Count - 1]].RecipientRank;
			}
			foreach (IInferenceRecipient inferenceRecipient3 in contactList.Values)
			{
				if (inferenceRecipient3.SmtpAddress != null)
				{
					CaptureFlag captureFlag2 = (CaptureFlag)inferenceRecipient3.CaptureFlag;
					switch (captureFlag2)
					{
					case CaptureFlag.None:
						if (inferenceRecipient3.HasUpdatedData && inferenceRecipient3.RecipientRank <= num)
						{
							recipientCacheChangeList.UpdatesTotal++;
							continue;
						}
						continue;
					case CaptureFlag.Add:
						recipientCacheChangeList.AddsTotal++;
						continue;
					case CaptureFlag.Update:
						recipientCacheChangeList.UpdatesTotal++;
						continue;
					case CaptureFlag.Delete:
						recipientCacheChangeList.AddRecipientToProcessingList(inferenceRecipient3, this.DiagnosticsSession, context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
						recipientCacheChangeList.DeletesTotal++;
						continue;
					}
					Util.ThrowOnConditionFailed(false, "Invalid capture flag " + captureFlag2);
				}
			}
			return recipientCacheChangeList;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000057D4 File Offset: 0x000039D4
		private void ValidateRecipientCache(DocumentContext data, IDictionary<string, IInferenceRecipient> contactList, IList<string> topRanked, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			object obj;
			bool flag;
			if (data.Document.TryGetProperty(PeopleRelevanceSchema.LastRecipientCacheValidationTime, out obj))
			{
				ExDateTime dt = (ExDateTime)obj;
				flag = (utcNow - dt > PeopleRelevanceConfig.Instance.RecipientCacheValidationInterval);
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			Exception ex = null;
			bool flag2 = false;
			HashSet<string> hashSet = (topRanked != null) ? new HashSet<string>(topRanked) : new HashSet<string>();
			int num = 0;
			int num2 = 0;
			try
			{
				foreach (KeyValuePair<string, List<object[]>> keyValuePair in context.SortedRecipientCacheData)
				{
					RecipientCacheContactWriter.RemoveDuplicates(keyValuePair.Key, context);
					object[] array = keyValuePair.Value[0];
					StoreId key = (StoreId)array[0];
					int relevanceRank = 1;
					if (array[2] != null && !(array[2] is PropertyError))
					{
						relevanceRank = (int)array[2];
					}
					if (contactList.ContainsKey(keyValuePair.Key))
					{
						hashSet.Remove(keyValuePair.Key);
						IInferenceRecipient inferenceRecipient = contactList[keyValuePair.Key];
						if (inferenceRecipient.CaptureFlag == 0)
						{
							if (inferenceRecipient.RelevanceCategory == 2147483647)
							{
								Util.ThrowOnConditionFailed(inferenceRecipient.RelevanceCategoryAtLastCapture == int.MaxValue, "Invalid relevance category in the model item");
								context.ItemsToDelete[key] = null;
							}
							else
							{
								int relevanceCategoryForRank = InferenceRecipient.GetRelevanceCategoryForRank(relevanceRank);
								if (relevanceCategoryForRank != inferenceRecipient.RelevanceCategoryAtLastCapture && relevanceCategoryForRank != inferenceRecipient.RelevanceCategory)
								{
									inferenceRecipient.CaptureFlag = 2;
								}
							}
						}
					}
					else
					{
						context.ItemsToDelete[key] = null;
					}
				}
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
				flag2 = true;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			foreach (string key2 in hashSet)
			{
				if (contactList[key2].CaptureFlag == 0)
				{
					contactList[key2].CaptureFlag = 1;
					num++;
				}
			}
			bool flag3;
			if (context.TryCheckRecipientCacheFolderExists(out flag3) && flag3)
			{
				context.SortedRecipientCache.SeekToOffset(SeekReference.OriginBeginning, 0);
			}
			if (ex != null)
			{
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, string.Format("U={0} - Encountered exception while validating the Recipient Cache: {1} {2}", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, ex.Message, ex.StackTrace), new object[0]);
			}
			if (!flag2)
			{
				data.Document.SetProperty(PeopleRelevanceSchema.LastRecipientCacheValidationTime, utcNow);
			}
			this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Recipient Cache Validation found {1} entries to delete, {2} to update and {3} to add", new object[]
			{
				context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress,
				context.ItemsToDelete.Count,
				num2,
				num
			}), new object[0]);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005B24 File Offset: 0x00003D24
		private void ProcessStoreChanges(RecipientCacheContactWriter.RecipientCacheChangeList changeList, RecipientCacheContactWriter.RecipientCacheContactWriterContext context)
		{
			if (changeList.TotalCount == 0)
			{
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - No updates to process.", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress), new object[0]);
				return;
			}
			foreach (IInferenceRecipient inferenceRecipient in changeList.ProcessingList.Values)
			{
				LocalizedException ex = null;
				try
				{
					this.LogContactUpdateData(inferenceRecipient, context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					this.ProcessChange(inferenceRecipient, context, changeList);
				}
				catch (StorageTransientException ex2)
				{
					ex = ex2;
				}
				catch (StoragePermanentException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					changeList.FailureCount++;
					bool flag = RecipientCacheContactWriter.IsGlobalException(ex);
					this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, string.Format("U={0} - Encountered exception while processing a store update for recipient {1}. IsGlobalException? {2}: {3}", new object[]
					{
						context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress,
						inferenceRecipient.SmtpAddress,
						flag,
						ex.Message
					}), new object[0]);
					if (flag)
					{
						break;
					}
				}
			}
			this.ApplyItemDeletes(context, changeList);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005C9C File Offset: 0x00003E9C
		private void ApplyItemDeletes(RecipientCacheContactWriter.RecipientCacheContactWriterContext context, RecipientCacheContactWriter.RecipientCacheChangeList changeList)
		{
			if (context.ItemsToDelete.Count == 0 && context.DeletedItemsToDelete.Count == 0)
			{
				return;
			}
			bool flag;
			if (!context.TryCheckRecipientCacheFolderExists(out flag))
			{
				changeList.FailureCount += context.ItemsToDelete.Count;
				return;
			}
			if (!flag)
			{
				this.DiagnosticsSession.TraceDebug("ApplyItemDeletes not executed. RecipientCacheFolder does not exist", new object[0]);
				changeList.DeletesProcessed = context.ItemsToDelete.Count;
			}
			else if (context.ItemsToDelete.Count > 0)
			{
				StoreId[] array = new StoreId[context.ItemsToDelete.Count];
				context.ItemsToDelete.Keys.CopyTo(array, 0);
				AggregateOperationResult aggregateOperationResult = context.RecipientCacheFolder.DeleteObjects(DeleteItemFlags.HardDelete, array);
				if (aggregateOperationResult.OperationResult == OperationResult.Succeeded)
				{
					changeList.DeletesProcessed = context.ItemsToDelete.Count;
				}
				else
				{
					for (int i = 0; i < aggregateOperationResult.GroupOperationResults.Length; i++)
					{
						if (aggregateOperationResult.GroupOperationResults[i].OperationResult != OperationResult.Succeeded)
						{
							IInferenceRecipient inferenceRecipient = context.ItemsToDelete[array[i]];
							this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, string.Format("U={0} - Encountered exception while attempting to delete {1}: {2}", context.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, (inferenceRecipient == null) ? array[i].ToString() : inferenceRecipient.EmailAddress, aggregateOperationResult.GroupOperationResults[i].Exception.Message), new object[0]);
							context.ItemsToDelete.Remove(array[i]);
							changeList.FailureCount++;
						}
						else
						{
							changeList.DeletesProcessed++;
						}
					}
				}
			}
			if (context.DeletedItemsToDelete.Count > 0)
			{
				StoreId[] array2 = new StoreId[context.DeletedItemsToDelete.Count];
				context.DeletedItemsToDelete.CopyTo(array2, 0);
				context.DeletedItemsFolder.DeleteObjects(DeleteItemFlags.HardDelete, array2);
			}
			foreach (IInferenceRecipient inferenceRecipient2 in context.ItemsToDelete.Values)
			{
				if (inferenceRecipient2 != null)
				{
					RecipientCacheContactWriter.ClearCaptureFlags(inferenceRecipient2);
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005ECC File Offset: 0x000040CC
		private void LogRecipientCacheUpdateSummary(RecipientCacheContactWriter.RecipientCacheChangeList recipientCacheChangeList, string userIdentity)
		{
			this.DiagnosticsSession.LogDiagnosticsInfo((recipientCacheChangeList.FailureCount == 0) ? DiagnosticsLoggingTag.Informational : DiagnosticsLoggingTag.Warnings, string.Format("Summary: U={0};TC={1};FC={2};ADD={3}/{4}/{5};UPT={6}/{7}/{8};DEL={9}/{10}/{11}", new object[]
			{
				userIdentity,
				recipientCacheChangeList.TotalCount,
				recipientCacheChangeList.FailureCount,
				recipientCacheChangeList.AddsProcessed,
				recipientCacheChangeList.AddsForProcessing,
				recipientCacheChangeList.AddsTotal,
				recipientCacheChangeList.UpdatesProcessed,
				recipientCacheChangeList.UpdatesForProcessing,
				recipientCacheChangeList.UpdatesTotal,
				recipientCacheChangeList.DeletesProcessed,
				recipientCacheChangeList.DeletesForProcessing,
				recipientCacheChangeList.DeletesTotal
			}), new object[0]);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005FAC File Offset: 0x000041AC
		private void LogContactUpdateData(IInferenceRecipient recipient, string userIdentity)
		{
			this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Applying '{1}' to E={2};TSC={3};R={4};C={5};LC={6};W={7};LTW={8};IM={9};A={10};T={11};UD={12}", new object[]
			{
				userIdentity,
				(CaptureFlag)recipient.CaptureFlag,
				recipient.SmtpAddress,
				recipient.TotalSentCount,
				recipient.RecipientRank,
				recipient.RelevanceCategory,
				recipient.RelevanceCategoryAtLastCapture,
				recipient.RawRecipientWeight,
				recipient.LastUsedInTimeWindow,
				recipient.SipUri,
				recipient.Alias,
				recipient.RecipientDisplayType,
				recipient.HasUpdatedData
			}), new object[0]);
		}

		// Token: 0x04000064 RID: 100
		private const string ComponentDescription = "RecipientCacheContactWriter updates the recipient cache contacts based on the capture flags set byt PeopleRelevanceClassifier.";

		// Token: 0x04000065 RID: 101
		private const string ComponentName = "RecipientCacheContactWriter";

		// Token: 0x04000066 RID: 102
		private static readonly PropertyDefinition[] QueryProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ContactSchema.Email1EmailAddress,
			ContactSchema.RelevanceScore,
			ContactSchema.Email1DisplayName,
			ContactSchema.IMAddress,
			ContactSchema.PartnerNetworkId,
			StoreObjectSchema.DisplayName,
			ContactSchema.Account,
			ContactSchema.PersonType
		};

		// Token: 0x0200001C RID: 28
		private class RecipientCacheChangeList
		{
			// Token: 0x060000E5 RID: 229 RVA: 0x000060E3 File Offset: 0x000042E3
			public RecipientCacheChangeList(int quota)
			{
				this.ProcessingList = new SortedList<string, IInferenceRecipient>();
				this.QuotaRemaining = quota;
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000E6 RID: 230 RVA: 0x000060FD File Offset: 0x000042FD
			// (set) Token: 0x060000E7 RID: 231 RVA: 0x00006105 File Offset: 0x00004305
			public SortedList<string, IInferenceRecipient> ProcessingList { get; private set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000610E File Offset: 0x0000430E
			// (set) Token: 0x060000E9 RID: 233 RVA: 0x00006116 File Offset: 0x00004316
			public int AddsForProcessing { get; private set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000EA RID: 234 RVA: 0x0000611F File Offset: 0x0000431F
			// (set) Token: 0x060000EB RID: 235 RVA: 0x00006127 File Offset: 0x00004327
			public int DeletesForProcessing { get; private set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000EC RID: 236 RVA: 0x00006130 File Offset: 0x00004330
			// (set) Token: 0x060000ED RID: 237 RVA: 0x00006138 File Offset: 0x00004338
			public int UpdatesForProcessing { get; private set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000EE RID: 238 RVA: 0x00006141 File Offset: 0x00004341
			// (set) Token: 0x060000EF RID: 239 RVA: 0x00006149 File Offset: 0x00004349
			public int AddsTotal { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000F0 RID: 240 RVA: 0x00006152 File Offset: 0x00004352
			// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000615A File Offset: 0x0000435A
			public int DeletesTotal { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006163 File Offset: 0x00004363
			// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000616B File Offset: 0x0000436B
			public int UpdatesTotal { get; set; }

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006174 File Offset: 0x00004374
			// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000617C File Offset: 0x0000437C
			public int AddsProcessed { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006185 File Offset: 0x00004385
			// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000618D File Offset: 0x0000438D
			public int DeletesProcessed { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006196 File Offset: 0x00004396
			// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000619E File Offset: 0x0000439E
			public int UpdatesProcessed { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000FA RID: 250 RVA: 0x000061A7 File Offset: 0x000043A7
			// (set) Token: 0x060000FB RID: 251 RVA: 0x000061AF File Offset: 0x000043AF
			public int FailureCount { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000FC RID: 252 RVA: 0x000061B8 File Offset: 0x000043B8
			// (set) Token: 0x060000FD RID: 253 RVA: 0x000061C0 File Offset: 0x000043C0
			public int QuotaRemaining { get; private set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000FE RID: 254 RVA: 0x000061C9 File Offset: 0x000043C9
			public int TotalCount
			{
				get
				{
					return this.AddsTotal + this.DeletesTotal + this.UpdatesTotal;
				}
			}

			// Token: 0x060000FF RID: 255 RVA: 0x000061E0 File Offset: 0x000043E0
			public void AddRecipientToProcessingList(IInferenceRecipient recipient, IDiagnosticsSession diagnosticsSession, string mailboxOwner)
			{
				if (this.ProcessingList.ContainsKey(recipient.SmtpAddress))
				{
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, string.Format("U={0} - Duplicate processing entry for {1} found. ", mailboxOwner, recipient.ToString()), new object[0]);
					return;
				}
				this.ProcessingList.Add(recipient.SmtpAddress, recipient);
				CaptureFlag captureFlag = (CaptureFlag)recipient.CaptureFlag;
				if (captureFlag == CaptureFlag.None && recipient.HasUpdatedData)
				{
					captureFlag = CaptureFlag.Update;
				}
				switch (captureFlag)
				{
				case CaptureFlag.Add:
					this.AddsForProcessing++;
					this.QuotaRemaining--;
					return;
				case CaptureFlag.Update:
					this.UpdatesForProcessing++;
					this.QuotaRemaining--;
					return;
				case CaptureFlag.Delete:
					this.DeletesForProcessing++;
					return;
				}
				Util.ThrowOnConditionFailed(false, "Invalid capture flag " + recipient.CaptureFlag);
			}
		}

		// Token: 0x0200001D RID: 29
		private class RecipientCacheContactWriterContext
		{
			// Token: 0x06000100 RID: 256 RVA: 0x000062C5 File Offset: 0x000044C5
			internal RecipientCacheContactWriterContext(MailboxSession session, IDiagnosticsSession diagnosticSession)
			{
				this.Session = session;
				this.DiagnosticsSession = diagnosticSession;
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000101 RID: 257 RVA: 0x000062DB File Offset: 0x000044DB
			internal Folder RecipientCacheFolder
			{
				get
				{
					Util.ThrowOnNullArgument(this.Session, "session");
					if (this.recipientCacheFolder == null)
					{
						this.recipientCacheFolder = Folder.Bind(this.Session, this.RecipientCacheFolderId);
					}
					return this.recipientCacheFolder;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00006312 File Offset: 0x00004512
			internal Folder DeletedItemsFolder
			{
				get
				{
					Util.ThrowOnNullArgument(this.Session, "session");
					if (this.deletedItemsFolder == null)
					{
						this.deletedItemsFolder = Folder.Bind(this.Session, this.DeletedItemsFolderId);
					}
					return this.deletedItemsFolder;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000103 RID: 259 RVA: 0x0000634C File Offset: 0x0000454C
			internal QueryResult SortedRecipientCache
			{
				get
				{
					if (this.sortedRecipientCache == null)
					{
						SortBy[] sortColumns = new SortBy[]
						{
							new SortBy(ContactSchema.Email1EmailAddress, SortOrder.Ascending),
							new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Descending)
						};
						this.sortedRecipientCache = this.RecipientCacheFolder.ItemQuery(ItemQueryType.None, null, sortColumns, RecipientCacheContactWriter.QueryProperties);
					}
					return this.sortedRecipientCache;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000104 RID: 260 RVA: 0x000063A8 File Offset: 0x000045A8
			internal Dictionary<string, List<object[]>> SortedRecipientCacheData
			{
				get
				{
					if (this.sortedRecipientCacheData == null)
					{
						bool flag = false;
						try
						{
							this.sortedRecipientCacheData = new Dictionary<string, List<object[]>>(PeopleRelevanceConfig.Instance.MaxRelevantRecipientsCount);
							bool flag2 = false;
							do
							{
								object[][] rows = this.SortedRecipientCache.GetRows(1000, out flag2);
								foreach (object[] array2 in rows)
								{
									string lowercaseStringProperty = RecipientCacheContactWriter.GetLowercaseStringProperty(array2[1]);
									if (this.sortedRecipientCacheData.ContainsKey(lowercaseStringProperty))
									{
										this.sortedRecipientCacheData[lowercaseStringProperty].Add(array2);
									}
									else
									{
										this.sortedRecipientCacheData.Add(lowercaseStringProperty, new List<object[]>(new object[][]
										{
											array2
										}));
									}
								}
							}
							while (flag2);
							flag = true;
						}
						finally
						{
							if (!flag)
							{
								this.sortedRecipientCacheData = null;
							}
						}
					}
					return this.sortedRecipientCacheData;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000105 RID: 261 RVA: 0x00006484 File Offset: 0x00004684
			internal Dictionary<string, StoreId> DeletedRecipientCacheItems
			{
				get
				{
					if (this.deletedRecipientCacheItems != null)
					{
						return this.deletedRecipientCacheItems;
					}
					Util.ThrowOnNullArgument(this.Session, "session");
					Dictionary<string, StoreId> dictionary = new Dictionary<string, StoreId>();
					Exception ex = null;
					try
					{
						using (QueryResult queryResult = this.DeletedItemsFolder.ItemQuery(ItemQueryType.None, null, RecipientCacheContactWriter.RecipientCacheContactWriterContext.DeletedRecipientCacheContactsSortBy, RecipientCacheContactWriter.QueryProperties))
						{
							bool flag = true;
							IL_C1:
							while (flag && queryResult.SeekToCondition(SeekReference.OriginCurrent, RecipientCacheContactWriter.RecipientCacheContactWriterContext.DeletedRecipientCacheContactsFilter))
							{
								object[][] rows = queryResult.GetRows(10000);
								int num = 0;
								for (;;)
								{
									StoreId storeId = (StoreId)rows[num][0];
									string lowercaseStringProperty = RecipientCacheContactWriter.GetLowercaseStringProperty(rows[num][1]);
									if (!dictionary.ContainsKey(lowercaseStringProperty))
									{
										dictionary.Add(lowercaseStringProperty, storeId);
									}
									else
									{
										this.DeletedItemsToDelete.Add(storeId);
									}
									if (++num == rows.Length)
									{
										break;
									}
									string lowercaseStringProperty2 = RecipientCacheContactWriter.GetLowercaseStringProperty(rows[num][5]);
									if (!lowercaseStringProperty2.Equals(WellKnownNetworkNames.RecipientCache, StringComparison.InvariantCultureIgnoreCase))
									{
										goto IL_C1;
									}
								}
								flag = false;
							}
						}
					}
					catch (StorageTransientException ex2)
					{
						ex = ex2;
					}
					catch (StoragePermanentException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, string.Format("U={0} - Encountered exception while retrieving the deleted Recipient Cache items: {1} {2}", this.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress, ex.Message, ex.StackTrace), new object[0]);
					}
					this.deletedRecipientCacheItems = dictionary;
					return this.deletedRecipientCacheItems;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000106 RID: 262 RVA: 0x000065FC File Offset: 0x000047FC
			// (set) Token: 0x06000107 RID: 263 RVA: 0x00006604 File Offset: 0x00004804
			internal MailboxSession Session { get; private set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000108 RID: 264 RVA: 0x0000660D File Offset: 0x0000480D
			// (set) Token: 0x06000109 RID: 265 RVA: 0x00006615 File Offset: 0x00004815
			internal IDiagnosticsSession DiagnosticsSession { get; private set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600010A RID: 266 RVA: 0x0000661E File Offset: 0x0000481E
			internal BulkAutomaticLink BulkAutomaticLink
			{
				get
				{
					if (this.bulkAutomaticLink == null)
					{
						this.bulkAutomaticLink = new BulkAutomaticLink(this.Session);
					}
					return this.bulkAutomaticLink;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600010B RID: 267 RVA: 0x0000663F File Offset: 0x0000483F
			// (set) Token: 0x0600010C RID: 268 RVA: 0x00006647 File Offset: 0x00004847
			internal Dictionary<StoreId, IInferenceRecipient> ItemsToDelete { get; set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600010D RID: 269 RVA: 0x00006650 File Offset: 0x00004850
			// (set) Token: 0x0600010E RID: 270 RVA: 0x00006658 File Offset: 0x00004858
			internal HashSet<StoreId> DeletedItemsToDelete { get; set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600010F RID: 271 RVA: 0x00006661 File Offset: 0x00004861
			private StoreObjectId RecipientCacheFolderId
			{
				get
				{
					if (this.recipientCacheFolder == null)
					{
						this.recipientCacheFolderId = this.Session.GetDefaultFolderId(DefaultFolderType.RecipientCache);
						if (this.recipientCacheFolderId == null)
						{
							throw new ObjectNotInitializedException(new LocalizedString("RecipientCache folder not initialized"), null);
						}
					}
					return this.recipientCacheFolderId;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000110 RID: 272 RVA: 0x0000669D File Offset: 0x0000489D
			private StoreObjectId DeletedItemsFolderId
			{
				get
				{
					if (this.deletedItemsFolderId == null)
					{
						this.deletedItemsFolderId = this.Session.GetDefaultFolderId(DefaultFolderType.DeletedItems);
					}
					return this.deletedItemsFolderId;
				}
			}

			// Token: 0x06000111 RID: 273 RVA: 0x000066C0 File Offset: 0x000048C0
			internal bool TryCheckRecipientCacheFolderExists(out bool exists)
			{
				LocalizedException ex = null;
				exists = false;
				try
				{
					exists = (this.RecipientCacheFolder != null);
				}
				catch (StorageTransientException ex2)
				{
					ex = ex2;
				}
				catch (StoragePermanentException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					this.DiagnosticsSession.TraceDebug<string, string>("Encountered an exception checking if RecipientCache folder exists: {0} {1}", ex.Message, ex.StackTrace);
					return false;
				}
				return true;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x0000672C File Offset: 0x0000492C
			internal void DisposeAll()
			{
				if (this.sortedRecipientCache != null)
				{
					this.sortedRecipientCache.Dispose();
					this.sortedRecipientCache = null;
				}
				if (this.recipientCacheFolder != null)
				{
					this.recipientCacheFolder.Dispose();
					this.recipientCacheFolder = null;
				}
				if (this.deletedItemsFolder != null)
				{
					this.deletedItemsFolder.Dispose();
					this.deletedItemsFolder = null;
				}
				if (this.bulkAutomaticLink != null)
				{
					this.bulkAutomaticLink.Dispose();
					this.bulkAutomaticLink = null;
				}
				this.Session = null;
			}

			// Token: 0x04000075 RID: 117
			private static readonly SortBy[] DeletedRecipientCacheContactsSortBy = new SortBy[]
			{
				new SortBy(ContactSchema.PartnerNetworkId, SortOrder.Descending)
			};

			// Token: 0x04000076 RID: 118
			private static readonly QueryFilter DeletedRecipientCacheContactsFilter = new ComparisonFilter(ComparisonOperator.Equal, ContactSchema.PartnerNetworkId, WellKnownNetworkNames.RecipientCache);

			// Token: 0x04000077 RID: 119
			private StoreObjectId recipientCacheFolderId;

			// Token: 0x04000078 RID: 120
			private Folder recipientCacheFolder;

			// Token: 0x04000079 RID: 121
			private StoreObjectId deletedItemsFolderId;

			// Token: 0x0400007A RID: 122
			private Folder deletedItemsFolder;

			// Token: 0x0400007B RID: 123
			private QueryResult sortedRecipientCache;

			// Token: 0x0400007C RID: 124
			private Dictionary<string, List<object[]>> sortedRecipientCacheData;

			// Token: 0x0400007D RID: 125
			private Dictionary<string, StoreId> deletedRecipientCacheItems;

			// Token: 0x0400007E RID: 126
			private BulkAutomaticLink bulkAutomaticLink;
		}
	}
}
