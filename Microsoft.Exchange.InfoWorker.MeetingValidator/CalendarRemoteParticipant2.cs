using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarRemoteParticipant2 : CalendarParticipant
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00009908 File Offset: 0x00007B08
		internal CalendarRemoteParticipant2(UserObject userObject, ExDateTime validateFrom, ExDateTime validateUntil, MailboxSession session, Uri endpoint) : base(userObject, validateFrom, validateUntil)
		{
			if (userObject.ExchangePrincipal == null)
			{
				throw new ArgumentNullException("userObject.ExchangePrincipal");
			}
			this.localSession = session;
			this.ExchangePrincipal = userObject.ExchangePrincipal;
			this.calendarConverter = new CalendarItemConverter();
			this.binding = new MeetingValidatorEwsBinding(this.ExchangePrincipal, endpoint);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009963 File Offset: 0x00007B63
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000996B File Offset: 0x00007B6B
		internal ExchangePrincipal ExchangePrincipal { get; private set; }

		// Token: 0x0600012E RID: 302 RVA: 0x00009974 File Offset: 0x00007B74
		public override void Dispose()
		{
			if (this.binding != null)
			{
				this.binding.Dispose();
				this.binding = null;
			}
			base.Dispose();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009998 File Offset: 0x00007B98
		internal override void ValidateMeetings(ref Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent, Action<long> onItemRepaired)
		{
			bool shouldProcessMailbox = CalendarParticipant.InternalShouldProcessMailbox(this.ExchangePrincipal);
			try
			{
				List<SearchExpressionType> list = new List<SearchExpressionType>();
				foreach (CalendarInstanceContext calendarInstanceContext in base.ItemList.Values)
				{
					calendarInstanceContext.ValidationContext.CalendarInstance = new CalendarRemoteItem(this.ExchangePrincipal, this.binding);
					calendarInstanceContext.ValidationContext.CalendarInstance.ShouldProcessMailbox = shouldProcessMailbox;
					GlobalObjectId globalObjectId = calendarInstanceContext.ValidationContext.BaseItem.GlobalObjectId;
					string value = Convert.ToBase64String(globalObjectId.CleanGlobalObjectIdBytes);
					SearchExpressionType item = new IsEqualToType
					{
						Item = CalendarItemFields.CleanGlobalObjectIdProp,
						FieldURIOrConstant = new FieldURIOrConstantType
						{
							Item = new ConstantValueType
							{
								Value = value
							}
						}
					};
					list.Add(item);
				}
				ItemType[] remoteCalendarItems = this.GetRemoteCalendarItems(list);
				if (remoteCalendarItems != null)
				{
					Dictionary<GlobalObjectId, CalendarItemType> dictionary = new Dictionary<GlobalObjectId, CalendarItemType>();
					foreach (ItemType itemType in remoteCalendarItems)
					{
						CalendarItemType calendarItemType = itemType as CalendarItemType;
						GlobalObjectId globalObjectId2 = CalendarItemFields.GetGlobalObjectId(calendarItemType);
						dictionary.Add(globalObjectId2, calendarItemType);
					}
					foreach (KeyValuePair<GlobalObjectId, CalendarInstanceContext> keyValuePair in base.ItemList)
					{
						if (dictionary.ContainsKey(keyValuePair.Key))
						{
							CalendarItemType remoteItem = dictionary[keyValuePair.Key];
							CalendarInstanceContext value2 = keyValuePair.Value;
							try
							{
								try
								{
									CalendarItemBase calendarItemBase = CalendarItem.Create(this.localSession, this.localSession.GetDefaultFolderId(DefaultFolderType.Calendar));
									Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "Converting the EWS item to XSO.");
									this.calendarConverter.ConvertItem(calendarItemBase, remoteItem);
									value2.ValidationContext.OppositeItem = calendarItemBase;
								}
								catch (FormatException ex)
								{
									string text = string.Format("Could not convert the remote item, exception = {0}", ex.GetType());
									Globals.ConsistencyChecksTracer.TraceError((long)this.GetHashCode(), text);
									value2.ValidationContext.CalendarInstance.LoadInconsistency = Inconsistency.CreateInstance(value2.ValidationContext.OppositeRole, text, CalendarInconsistencyFlag.StorageException, value2.ValidationContext);
								}
								catch (CorruptDataException ex2)
								{
									string text2 = string.Format("Could not convert the remote item, exception = {0}", ex2.GetType());
									Globals.ConsistencyChecksTracer.TraceError((long)this.GetHashCode(), text2);
									value2.ValidationContext.CalendarInstance.LoadInconsistency = Inconsistency.CreateInstance(value2.ValidationContext.OppositeRole, text2, CalendarInconsistencyFlag.StorageException, value2.ValidationContext);
								}
								catch (StorageTransientException ex3)
								{
									string text3 = string.Format("Could not convert the remote item, exception = {0}", ex3.GetType());
									Globals.ConsistencyChecksTracer.TraceError((long)this.GetHashCode(), text3);
									value2.ValidationContext.CalendarInstance.LoadInconsistency = Inconsistency.CreateInstance(value2.ValidationContext.OppositeRole, text3, CalendarInconsistencyFlag.StorageException, value2.ValidationContext);
								}
								continue;
							}
							finally
							{
								base.ValidateInstance(value2, organizerRumsSent, onItemRepaired);
								if (value2.ValidationContext.OppositeItem != null)
								{
									value2.ValidationContext.OppositeItem.Dispose();
									value2.ValidationContext.OppositeItem = null;
								}
							}
						}
						Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "GetItem didn't return an expected GlobalObjectId.");
					}
				}
				foreach (CalendarInstanceContext calendarInstanceContext2 in base.ItemList.Values)
				{
					if (!calendarInstanceContext2.IsValidationDone)
					{
						if (calendarInstanceContext2.ValidationContext.OppositeRole == RoleType.Organizer && calendarInstanceContext2.ValidationContext.OppositeItem == null)
						{
							calendarInstanceContext2.ValidationContext.OppositeRoleOrganizerIsValid = true;
						}
						base.ValidateInstance(calendarInstanceContext2, organizerRumsSent, onItemRepaired);
					}
				}
			}
			catch (ProtocolViolationException exception)
			{
				this.HandleRemoteException(exception);
			}
			catch (SecurityException exception2)
			{
				this.HandleRemoteException(exception2);
			}
			catch (ArgumentException exception3)
			{
				this.HandleRemoteException(exception3);
			}
			catch (InvalidOperationException exception4)
			{
				this.HandleRemoteException(exception4);
			}
			catch (NotSupportedException exception5)
			{
				this.HandleRemoteException(exception5);
			}
			catch (XmlException exception6)
			{
				this.HandleRemoteException(exception6);
			}
			catch (XPathException exception7)
			{
				this.HandleRemoteException(exception7);
			}
			catch (SoapException exception8)
			{
				this.HandleRemoteException(exception8);
			}
			catch (IOException exception9)
			{
				this.HandleRemoteException(exception9);
			}
			catch (IWTransientException exception10)
			{
				this.HandleRemoteException(exception10);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009F6C File Offset: 0x0000816C
		private ItemType[] GetRemoteCalendarItems(List<SearchExpressionType> searchItems)
		{
			FindItemType findItemType = new FindItemType();
			findItemType.ItemShape = CalendarItemFields.CalendarQueryShape;
			findItemType.ParentFolderIds = new BaseFolderIdType[]
			{
				new DistinguishedFolderIdType
				{
					Id = DistinguishedFolderIdNameType.calendar
				}
			};
			findItemType.Restriction = new RestrictionType
			{
				Item = new OrType
				{
					Items = searchItems.ToArray()
				}
			};
			FindItemResponseType response = this.binding.FindItem(findItemType);
			ItemType[] array = this.HandleFindItemResponse(response);
			if (array == null)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "FindItem returned NULL ArrayOfRealItemsType.");
				return null;
			}
			List<ItemIdType> list = new List<ItemIdType>();
			foreach (ItemType itemType in array)
			{
				if (itemType is CalendarItemType)
				{
					CalendarItemType calendarItemType = itemType as CalendarItemType;
					if (calendarItemType.CalendarItemType1 == CalendarItemTypeType.Single)
					{
						list.Add(itemType.ItemId);
					}
					else
					{
						OccurrencesRangeType occurrencesRangeType = new OccurrencesRangeType
						{
							Start = base.ValidateFrom.UniversalTime,
							StartSpecified = true,
							End = base.ValidateUntil.UniversalTime,
							EndSpecified = true,
							Count = 100,
							CountSpecified = true
						};
						RecurringMasterItemIdRangesType item = new RecurringMasterItemIdRangesType
						{
							Id = itemType.ItemId.Id,
							ChangeKey = itemType.ItemId.ChangeKey,
							Ranges = new OccurrencesRangeType[]
							{
								occurrencesRangeType
							}
						};
						list.Add(item);
					}
				}
				else
				{
					Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "FindItem returned an item which is not a CalendarItemType. Skipping it.");
				}
			}
			if (list.Count < 1)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "FindItem didn't return valid items.");
				return null;
			}
			GetItemType getItem = new GetItemType
			{
				ItemShape = CalendarItemFields.CalendarItemShape,
				ItemIds = list.ToArray()
			};
			GetItemResponseType item2 = this.binding.GetItem(getItem);
			ItemType[] array3 = this.HandleGetItemResponse(item2);
			if (array3 == null)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "GetItem returned NULL ItemType[].");
			}
			return array3;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A19E File Offset: 0x0000839E
		private ResponseMessageType HandleBaseResponseMessage(BaseResponseMessageType response)
		{
			return null;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A1A4 File Offset: 0x000083A4
		private ItemType[] HandleFindItemResponse(BaseResponseMessageType response)
		{
			ResponseMessageType responseMessageType = this.HandleBaseResponseMessage(response);
			FindItemResponseMessageType findItemResponseMessageType = responseMessageType as FindItemResponseMessageType;
			if (findItemResponseMessageType == null)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "FindItem web request returned NULL FindItemResponseMessageType.");
				return null;
			}
			if (findItemResponseMessageType.RootFolder == null || findItemResponseMessageType.RootFolder.Item == null)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "FindItem web request returned NULL RootFolder.");
				return null;
			}
			ArrayOfRealItemsType arrayOfRealItemsType = findItemResponseMessageType.RootFolder.Item as ArrayOfRealItemsType;
			if (arrayOfRealItemsType == null || arrayOfRealItemsType.Items == null || arrayOfRealItemsType.Items[0] == null)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "FindItem web request returned NULL ItemType.");
				return null;
			}
			return arrayOfRealItemsType.Items;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A24C File Offset: 0x0000844C
		private ItemType[] HandleGetItemResponse(BaseResponseMessageType response)
		{
			ResponseMessageType responseMessageType = this.HandleBaseResponseMessage(response);
			ItemInfoResponseMessageType itemInfoResponseMessageType = responseMessageType as ItemInfoResponseMessageType;
			if (itemInfoResponseMessageType == null || itemInfoResponseMessageType.Items == null || itemInfoResponseMessageType.Items.Items == null)
			{
				Globals.ConsistencyChecksTracer.TraceDebug((long)this.GetHashCode(), "GetItem web request returned NULL ItemType.");
				return null;
			}
			return itemInfoResponseMessageType.Items.Items;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A2A4 File Offset: 0x000084A4
		private void HandleRemoteException(Exception exception)
		{
			Globals.ConsistencyChecksTracer.TraceError<Exception, SmtpAddress>((long)this.GetHashCode(), "{0}: Could not access remote server to open mailbox {1}.", exception, this.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress);
			Globals.CalendarRepairLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_ErrorAccessingRemoteMailbox, this.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), new object[]
			{
				this.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(),
				exception
			});
		}

		// Token: 0x040000A8 RID: 168
		private MailboxSession localSession;

		// Token: 0x040000A9 RID: 169
		private MeetingValidatorEwsBinding binding;

		// Token: 0x040000AA RID: 170
		private CalendarItemConverter calendarConverter;
	}
}
