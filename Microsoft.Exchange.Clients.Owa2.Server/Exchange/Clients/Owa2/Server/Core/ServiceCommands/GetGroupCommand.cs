using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000315 RID: 789
	internal class GetGroupCommand : ServiceCommand<GetGroupResponse>
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x0005F6B4 File Offset: 0x0005D8B4
		public GetGroupCommand(CallContext callContext, ItemId itemId, string adObjectId, EmailAddressWrapper emailAddress, IndexedPageView paging, GetGroupResultSet resultSet, TargetFolderId folderId = null) : base(callContext)
		{
			if (itemId == null && (emailAddress == null || string.IsNullOrEmpty(emailAddress.EmailAddress)) && string.IsNullOrEmpty(adObjectId))
			{
				throw new ArgumentException("Must specify itemId, adObjectId or legacyExchangeDN");
			}
			if (paging == null)
			{
				throw new ArgumentNullException("paging");
			}
			this.itemId = itemId;
			this.adObjectId = adObjectId;
			if (emailAddress != null && emailAddress.RoutingType != null)
			{
				if (emailAddress.RoutingType.Equals("EX", StringComparison.OrdinalIgnoreCase))
				{
					this.legacyExchangeDN = emailAddress.EmailAddress;
				}
				else if (emailAddress.RoutingType.Equals("SMTP", StringComparison.OrdinalIgnoreCase))
				{
					this.smtpAddress = emailAddress.EmailAddress;
				}
			}
			this.paging = paging;
			this.resultSet = resultSet;
			this.hashCode = this.GetParamsHashCode();
			this.parentFolderid = folderId;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0005F782 File Offset: 0x0005D982
		private PersonaComparerByDisplayName PersonaComparer
		{
			get
			{
				if (this.personaComparer == null)
				{
					this.personaComparer = new PersonaComparerByDisplayName(base.CallContext.OwaCulture);
				}
				return this.personaComparer;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0005F7A8 File Offset: 0x0005D9A8
		private DistributionListMemberComparerByDisplayName PDlMembersComparer
		{
			get
			{
				if (this.pdlMembersComparer == null)
				{
					this.pdlMembersComparer = new DistributionListMemberComparerByDisplayName(base.CallContext.OwaCulture);
				}
				return this.pdlMembersComparer;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0005F7CE File Offset: 0x0005D9CE
		private bool IsMembersInResultSet
		{
			get
			{
				return (this.resultSet & GetGroupResultSet.Members) == GetGroupResultSet.Members;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0005F7DB File Offset: 0x0005D9DB
		private bool IsGeneralInfoInResultSet
		{
			get
			{
				return (this.resultSet & GetGroupResultSet.GeneralInfo) == GetGroupResultSet.GeneralInfo;
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0005F7E8 File Offset: 0x0005D9E8
		protected override GetGroupResponse InternalExecute()
		{
			if (!string.IsNullOrEmpty(this.smtpAddress) || !string.IsNullOrEmpty(this.adObjectId) || !string.IsNullOrEmpty(this.legacyExchangeDN))
			{
				return this.GetDLData();
			}
			return this.GetPDLData();
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0005F820 File Offset: 0x0005DA20
		private static Persona CreatePersonaFromDistributionListMember(MailboxSession session, DistributionListMember member, out bool isADMember)
		{
			isADMember = false;
			Participant participant = member.Participant;
			Persona persona = new Persona();
			persona.DisplayName = participant.DisplayName;
			persona.ImAddress = participant.GetValueOrDefault<string>(ParticipantSchema.SipUri, null);
			persona.EmailAddress = new EmailAddressWrapper();
			persona.EmailAddress.RoutingType = participant.RoutingType;
			persona.EmailAddress.EmailAddress = participant.EmailAddress;
			persona.EmailAddress.Name = participant.DisplayName;
			StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
			bool flag = member.MainEntryId is ADParticipantEntryId;
			PersonType personType = PersonType.Unknown;
			if (storeParticipantOrigin != null)
			{
				personType = ((string.CompareOrdinal(participant.RoutingType, "MAPIPDL") == 0) ? PersonType.DistributionList : PersonType.Person);
				if (session != null)
				{
					persona.EmailAddress.ItemId = IdConverter.GetItemIdFromStoreId(storeParticipantOrigin.OriginItemId, new MailboxId(session));
				}
				persona.EmailAddress.EmailAddressIndex = storeParticipantOrigin.EmailAddressIndex.ToString();
				if (personType == PersonType.DistributionList)
				{
					persona.EmailAddress.MailboxType = MailboxHelper.MailboxTypeType.PrivateDL.ToString();
				}
				else if (personType == PersonType.Person)
				{
					persona.EmailAddress.MailboxType = MailboxHelper.MailboxTypeType.Contact.ToString();
				}
			}
			else if (flag)
			{
				isADMember = true;
			}
			else
			{
				persona.EmailAddress.MailboxType = MailboxHelper.MailboxTypeType.OneOff.ToString();
			}
			persona.PersonaType = PersonaTypeConverter.ToString(personType);
			return persona;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0005F978 File Offset: 0x0005DB78
		private static Persona GetPersonaFromADObject(ADRawEntry rawEntry)
		{
			if (rawEntry == null)
			{
				return null;
			}
			ADObjectId adobjectId = rawEntry[ADObjectSchema.Id] as ADObjectId;
			if (adobjectId == null)
			{
				return null;
			}
			Persona persona = new Persona();
			persona.PersonaId = IdConverter.PersonaIdFromADObjectId(adobjectId.ObjectGuid);
			RecipientType recipientType = (RecipientType)rawEntry[ADRecipientSchema.RecipientType];
			PersonType personType = ADRecipient.IsRecipientTypeDL(recipientType) ? PersonType.DistributionList : PersonType.Person;
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)rawEntry[ADRecipientSchema.RecipientTypeDetails];
			if (recipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				personType = PersonType.ModernGroup;
			}
			persona.PersonaType = PersonaTypeConverter.ToString(personType);
			object obj = rawEntry[ADRecipientSchema.DisplayName];
			if (obj != null)
			{
				persona.DisplayName = (obj as string);
			}
			object obj2 = rawEntry[ADRecipientSchema.PrimarySmtpAddress];
			if (obj2 != null)
			{
				persona.EmailAddress = new EmailAddressWrapper
				{
					Name = (persona.DisplayName ?? string.Empty),
					EmailAddress = obj2.ToString(),
					RoutingType = "SMTP",
					MailboxType = MailboxHelper.ConvertToMailboxType(personType).ToString()
				};
			}
			object obj3 = rawEntry[ADUserSchema.RTCSIPPrimaryUserAddress];
			if (obj3 != null)
			{
				persona.ImAddress = obj3.ToString();
			}
			return persona;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0005FAA8 File Offset: 0x0005DCA8
		private GetGroupResponse GetPDLData()
		{
			GetGroupResponse getGroupResponse = new GetGroupResponse();
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			ItemId sourceId = this.itemId;
			IdAndSession idAndSession = null;
			if (this.parentFolderid != null && this.parentFolderid.BaseFolderId != null)
			{
				IdAndSession idAndSession2 = base.GetIdAndSession(this.parentFolderid.BaseFolderId);
				if (idAndSession2 != null && idAndSession2.Session.IsPublicFolderSession)
				{
					idAndSession = idAndSession2;
				}
			}
			if (IdConverter.EwsIdIsConversationId(this.itemId.Id))
			{
				try
				{
					Persona persona;
					if (idAndSession != null)
					{
						persona = Persona.LoadFromPersonaId(idAndSession.Session, null, this.itemId, Persona.FullPersonaShape, null, idAndSession.Id);
					}
					else
					{
						persona = Persona.LoadFromPersonIdWithGalAggregation(mailboxIdentityMailboxSession, IdConverter.EwsIdToPersonId(this.itemId.Id), Persona.FullPersonaShape, null);
					}
					if (persona.Attributions.Length > 0)
					{
						sourceId = persona.Attributions[0].SourceId;
						for (int i = 1; i < persona.Attributions.Length; i++)
						{
							StoreId storeId = IdConverter.EwsIdToMessageStoreObjectId(persona.Attributions[i].SourceId.Id);
							using (Item item = Item.Bind(mailboxIdentityMailboxSession, storeId, new PropertyDefinition[]
							{
								ContactSchema.PersonId
							}))
							{
								item.OpenAsReadWrite();
								item[ContactSchema.PersonId] = PersonId.CreateNew();
								item.Save(SaveMode.NoConflictResolution);
							}
						}
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "UnlinkDupedPDLs", "Success");
					}
					else
					{
						ExTraceGlobals.GetGroupTracer.TraceError((long)this.hashCode, "ItemId is PersonaId but Persona has no linked contacts (attributions array is empty).");
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "UnlinkDupedPDLs", "Skipped");
					}
				}
				catch (LocalizedException arg)
				{
					ExTraceGlobals.GetGroupTracer.TraceError<LocalizedException>((long)this.hashCode, "Failed to unlink PDLs: {0}", arg);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "UnlinkDupedPDLs", "Failed");
				}
			}
			IdAndSession idAndSession3 = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(sourceId);
			StoreSession storeSession;
			if (idAndSession != null)
			{
				storeSession = idAndSession.Session;
			}
			else
			{
				storeSession = ((idAndSession3.Session as MailboxSession) ?? base.CallContext.SessionCache.GetMailboxIdentityMailboxSession());
			}
			using (DistributionList distributionList = DistributionList.Bind(storeSession, idAndSession3.Id))
			{
				if (distributionList == null)
				{
					this.WriteDebugTrace("No PDL was found");
					return getGroupResponse;
				}
				PersonId personId = (PersonId)distributionList[ContactSchema.PersonId];
				getGroupResponse.PersonaId = IdConverter.PersonaIdFromPersonId(storeSession.MailboxGuid, personId);
				if (!distributionList.GetValueOrDefault<bool>(ItemSchema.ConversationIndexTracking, false))
				{
					try
					{
						distributionList.OpenAsReadWrite();
						distributionList[ItemSchema.ConversationIndexTracking] = true;
						distributionList.Save(SaveMode.NoConflictResolution);
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "SetConversationIndexTracking", "Success");
					}
					catch (LocalizedException arg2)
					{
						ExTraceGlobals.GetGroupTracer.TraceError<LocalizedException>((long)this.hashCode, "Failed to set ConversationIndexTracking on PDL: {0}", arg2);
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "SetConversationIndexTracking", "Failed");
					}
				}
				if (!this.IsMembersInResultSet)
				{
					return getGroupResponse;
				}
				int count = distributionList.Count;
				getGroupResponse.MembersCount = count;
				this.WriteDebugTrace("Total PDL members count is " + count);
				if (this.paging.Offset < 0 || count <= this.paging.Offset)
				{
					this.WriteDebugTrace(string.Format("Provided offset is out of range - members count is {0}, offset is {1}.", count, this.paging.Offset));
					return getGroupResponse;
				}
				distributionList.Sort(this.PDlMembersComparer);
				List<Persona> list = new List<Persona>();
				List<string> list2 = new List<string>();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				int num = Math.Min(count, this.paging.Offset + this.paging.MaxRows);
				for (int j = this.paging.Offset; j < num; j++)
				{
					DistributionListMember distributionListMember = distributionList[j];
					if (distributionListMember.Participant == null)
					{
						if (num < count)
						{
							num++;
						}
						getGroupResponse.MembersCount--;
					}
					else
					{
						bool flag = false;
						Persona persona2 = GetGroupCommand.CreatePersonaFromDistributionListMember(mailboxIdentityMailboxSession, distributionListMember, out flag);
						if (flag)
						{
							list2.Add(persona2.EmailAddress.EmailAddress);
							dictionary.Add(persona2.EmailAddress.EmailAddress, list.Count);
						}
						list.Add(persona2);
					}
				}
				if (list2.Count > 0)
				{
					IRecipientSession galscopedADRecipientSession = base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext);
					Result<ADRawEntry>[] array = galscopedADRecipientSession.FindByLegacyExchangeDNs(list2.ToArray(), new PropertyDefinition[]
					{
						ADObjectSchema.Id,
						ADRecipientSchema.DisplayName,
						ADRecipientSchema.PrimarySmtpAddress,
						ADRecipientSchema.RecipientType,
						ADRecipientSchema.RecipientTypeDetails,
						ADRecipientSchema.LegacyExchangeDN
					});
					foreach (Result<ADRawEntry> result in array)
					{
						if (result.Data != null)
						{
							Persona personaFromADObject = GetGroupCommand.GetPersonaFromADObject(result.Data);
							string key = result.Data[ADRecipientSchema.LegacyExchangeDN] as string;
							int num2;
							if (dictionary.ContainsKey(key) && dictionary.TryGetValue(key, out num2) && num2 >= 0)
							{
								list[num2] = personaFromADObject;
							}
						}
					}
				}
				list.Sort(this.PersonaComparer);
				Persona[] array3 = new Persona[list.Count];
				list.CopyTo(0, array3, 0, list.Count);
				getGroupResponse.Members = array3;
				this.WriteDebugTrace("PDL members count loaded in the response is " + getGroupResponse.Members.Length);
			}
			return getGroupResponse;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000600B0 File Offset: 0x0005E2B0
		private GetGroupResponse GetDLData()
		{
			GetGroupResponse getGroupResponse = new GetGroupResponse();
			ADRecipient adrecipient = null;
			IRecipientSession galscopedADRecipientSession = base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext);
			if (!string.IsNullOrEmpty(this.smtpAddress))
			{
				Directory.TryFindRecipient(this.smtpAddress, galscopedADRecipientSession, out adrecipient);
			}
			if (adrecipient == null)
			{
				if (!string.IsNullOrEmpty(this.adObjectId))
				{
					Guid guid = new Guid(this.adObjectId);
					adrecipient = galscopedADRecipientSession.FindByObjectGuid(guid);
				}
				else if (!string.IsNullOrEmpty(this.legacyExchangeDN))
				{
					adrecipient = galscopedADRecipientSession.FindByLegacyExchangeDN(this.legacyExchangeDN);
				}
			}
			ADGroup adgroup = adrecipient as ADGroup;
			if (adgroup != null)
			{
				IADDistributionList iaddistributionList = adgroup;
				if (iaddistributionList != null)
				{
					if (this.IsGeneralInfoInResultSet)
					{
						getGroupResponse.Description = adgroup.Description;
						getGroupResponse.Notes = adrecipient.Notes;
						this.SetOwners(galscopedADRecipientSession, iaddistributionList, getGroupResponse);
					}
					if (this.IsMembersInResultSet)
					{
						getGroupResponse.MembersCount = ((iaddistributionList.Members != null) ? iaddistributionList.Members.Count : 0);
						this.WriteDebugTrace("Total DL members count is " + getGroupResponse.MembersCount);
						if (adrecipient.RecipientType != RecipientType.DynamicDistributionGroup)
						{
							if (getGroupResponse.MembersCount > 0)
							{
								this.SetMembers(galscopedADRecipientSession, iaddistributionList, getGroupResponse);
							}
						}
						else
						{
							this.WriteDebugTrace("DDL - Members information is not loaded");
						}
					}
				}
				else
				{
					this.WriteDebugTrace("No DL was found");
				}
			}
			else
			{
				this.WriteDebugTrace("The AD recipient is not a DL");
			}
			return getGroupResponse;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00060210 File Offset: 0x0005E410
		private void SetMembers(IRecipientSession adRecipientSession, IADDistributionList distributionList, GetGroupResponse response)
		{
			ADGroup adgroup = distributionList as ADGroup;
			if (adgroup == null)
			{
				throw new InvalidOperationException("AD DL object that is not dynamic DL must be ADGroup class");
			}
			if (adgroup.HiddenGroupMembershipEnabled)
			{
				this.WriteDebugTrace("HiddenGroupMembershipEnabled is true - Members information is not loaded");
				return;
			}
			if (this.paging.Offset < 0 || distributionList.Members.Count <= this.paging.Offset)
			{
				this.WriteDebugTrace(string.Format("Provided offset is out of range - members count is {0}, offset is {1}.", distributionList.Members.Count, this.paging.Offset));
				return;
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, adgroup.Id);
			SortBy sortBy = new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending);
			ADPagedReader<ADRawEntry> adpagedReader = adRecipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, filter, sortBy, 10000, GetGroupCommand.groupItemsProperties);
			using (IEnumerator<ADRawEntry> enumerator = adpagedReader.GetEnumerator())
			{
				List<Persona> list = new List<Persona>();
				int num = 0;
				int num2 = this.paging.Offset + this.paging.MaxRows;
				while (enumerator.MoveNext())
				{
					if (num < this.paging.Offset)
					{
						num++;
					}
					else
					{
						if (num == num2)
						{
							break;
						}
						Persona personaFromADObject = GetGroupCommand.GetPersonaFromADObject(enumerator.Current);
						if (personaFromADObject != null)
						{
							list.Add(personaFromADObject);
							num++;
						}
						else
						{
							response.MembersCount--;
						}
					}
				}
				response.Members = list.ToArray();
				this.WriteDebugTrace("DL members count loaded in the response is " + response.Members.Length);
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0006039C File Offset: 0x0005E59C
		private void SetOwners(IRecipientSession adRecipientSession, IADDistributionList distributionList, GetGroupResponse response)
		{
			List<Persona> list = new List<Persona>();
			if (distributionList is ADGroup)
			{
				ADGroup adgroup = distributionList as ADGroup;
				MultiValuedProperty<ADObjectId> managedBy = adgroup.ManagedBy;
				if (managedBy != null && managedBy.Count > 0)
				{
					ADObjectId[] entryIds = managedBy.ToArray();
					Result<ADRawEntry>[] array = adRecipientSession.ReadMultiple(entryIds, GetGroupCommand.groupItemsProperties);
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							Persona personaFromADObject = GetGroupCommand.GetPersonaFromADObject(array[i].Data);
							if (personaFromADObject != null)
							{
								list.Add(personaFromADObject);
							}
						}
						list.Sort(this.PersonaComparer);
						response.Owners = list.ToArray();
					}
				}
				else
				{
					this.WriteDebugTrace("No owners information was found for ADGroup DL");
				}
			}
			else
			{
				ADObjectId managedBy2 = distributionList.ManagedBy;
				if (managedBy2 != null)
				{
					ADRawEntry rawEntry = adRecipientSession.ReadADRawEntry(managedBy2, GetGroupCommand.groupItemsProperties);
					Persona personaFromADObject2 = GetGroupCommand.GetPersonaFromADObject(rawEntry);
					if (personaFromADObject2 != null)
					{
						response.Owners = new Persona[]
						{
							personaFromADObject2
						};
					}
				}
				else
				{
					this.WriteDebugTrace("No owner information was found for non-ADGroup DL");
				}
			}
			int num = (response.Owners != null) ? response.Owners.Length : 0;
			this.WriteDebugTrace("DL owners count loaded in the response is " + num);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000604C4 File Offset: 0x0005E6C4
		private void WriteDebugTrace(string message)
		{
			ExTraceGlobals.GetGroupTracer.TraceDebug((long)this.hashCode, message);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000604D8 File Offset: 0x0005E6D8
		private int GetParamsHashCode()
		{
			int result;
			if (!string.IsNullOrEmpty(this.adObjectId))
			{
				result = this.adObjectId.GetHashCode();
			}
			else if (!string.IsNullOrEmpty(this.legacyExchangeDN))
			{
				result = this.legacyExchangeDN.GetHashCode();
			}
			else
			{
				result = this.itemId.Id.GetHashCode();
			}
			return result;
		}

		// Token: 0x04000E84 RID: 3716
		private static readonly PropertyDefinition[] groupItemsProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientTypeDetails,
			ADUserSchema.RTCSIPPrimaryUserAddress
		};

		// Token: 0x04000E85 RID: 3717
		private readonly ItemId itemId;

		// Token: 0x04000E86 RID: 3718
		private readonly string adObjectId;

		// Token: 0x04000E87 RID: 3719
		private readonly string legacyExchangeDN;

		// Token: 0x04000E88 RID: 3720
		private readonly string smtpAddress;

		// Token: 0x04000E89 RID: 3721
		private readonly IndexedPageView paging;

		// Token: 0x04000E8A RID: 3722
		private readonly int hashCode;

		// Token: 0x04000E8B RID: 3723
		private PersonaComparerByDisplayName personaComparer;

		// Token: 0x04000E8C RID: 3724
		private DistributionListMemberComparerByDisplayName pdlMembersComparer;

		// Token: 0x04000E8D RID: 3725
		private GetGroupResultSet resultSet;

		// Token: 0x04000E8E RID: 3726
		private TargetFolderId parentFolderid;
	}
}
