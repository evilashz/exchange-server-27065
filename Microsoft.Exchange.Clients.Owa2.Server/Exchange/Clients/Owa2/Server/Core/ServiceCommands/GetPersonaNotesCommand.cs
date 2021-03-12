using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000323 RID: 803
	internal class GetPersonaNotesCommand : ServiceCommand<GetPersonaNotesResponse>
	{
		// Token: 0x06001AB3 RID: 6835 RVA: 0x00064A44 File Offset: 0x00062C44
		public GetPersonaNotesCommand(CallContext callContext, string personaId, EmailAddressWrapper emailAddress, int maxBytesToFetch, TargetFolderId folderId = null) : base(callContext)
		{
			this.personaId = personaId;
			this.maxBytesToFetch = maxBytesToFetch;
			this.emailAddress = emailAddress;
			this.adRecipientSession = base.CallContext.ADRecipientSessionContext.GetGALScopedADRecipientSession(base.CallContext.EffectiveCaller.ClientSecurityContext);
			this.folderId = folderId;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00064A9C File Offset: 0x00062C9C
		protected override GetPersonaNotesResponse InternalExecute()
		{
			GetPersonaNotesResponse getPersonaNotesResponse = new GetPersonaNotesResponse();
			ADObjectId adobjectId = null;
			PersonId personId = null;
			if (string.IsNullOrEmpty(this.personaId))
			{
				if (this.emailAddress == null || string.IsNullOrEmpty(this.emailAddress.EmailAddress))
				{
					this.WriteTrace("Valid PersonaId or EmailAddress needs to be specified.", new object[0]);
					return getPersonaNotesResponse;
				}
				string text = this.emailAddress.EmailAddress;
				personId = Person.FindPersonIdByEmailAddress(base.CallContext.SessionCache.GetMailboxIdentityMailboxSession(), new XSOFactory(), text);
				if (personId == null)
				{
					ProxyAddress proxyAddress = null;
					if (!ProxyAddress.TryParse(text, out proxyAddress))
					{
						this.WriteTrace("EmailAddress is not valid - {0}", new object[]
						{
							text
						});
						return getPersonaNotesResponse;
					}
					PropertyDefinition[] array = new PropertyDefinition[]
					{
						ADObjectSchema.Id
					};
					ADRawEntry adrawEntry = this.adRecipientSession.FindByProxyAddress(proxyAddress, array);
					if (adrawEntry == null)
					{
						this.WriteTrace("Contact not found with specified EmailAddress - {0}", new object[]
						{
							text
						});
						return getPersonaNotesResponse;
					}
					object[] properties = adrawEntry.GetProperties(array);
					if (properties == null || properties.Length == 0)
					{
						this.WriteTrace("Contact not found with specified EmailAddress - {0}", new object[]
						{
							text
						});
						return getPersonaNotesResponse;
					}
					adobjectId = (ADObjectId)properties[0];
				}
			}
			else if (IdConverter.EwsIdIsActiveDirectoryObject(this.personaId))
			{
				adobjectId = IdConverter.EwsIdToADObjectId(this.personaId);
			}
			else
			{
				personId = IdConverter.EwsIdToPersonId(this.personaId);
			}
			List<BodyContentAttributedValue> list = null;
			if (adobjectId != null)
			{
				list = this.GetNotesFromActiveDirectory(adobjectId);
			}
			else if (personId != null)
			{
				list = this.GetNotesFromStore(personId);
			}
			if (list != null && list.Count > 0)
			{
				getPersonaNotesResponse.PersonaWithNotes = new Persona
				{
					PersonaId = new ItemId(),
					PersonaId = 
					{
						Id = this.personaId
					},
					Bodies = list.ToArray()
				};
			}
			return getPersonaNotesResponse;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00064C58 File Offset: 0x00062E58
		private List<BodyContentAttributedValue> GetNotesFromStore(PersonId personId)
		{
			StoreSession session = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			StoreId storeId = null;
			if (this.folderId != null && this.folderId.BaseFolderId != null)
			{
				IdAndSession idAndSession = base.GetIdAndSession(this.folderId.BaseFolderId);
				if (idAndSession.Session.IsPublicFolderSession)
				{
					session = idAndSession.Session;
					storeId = idAndSession.Id;
				}
			}
			Person person = Person.LoadNotes(session, personId, this.maxBytesToFetch, storeId);
			List<BodyContentAttributedValue> list = new List<BodyContentAttributedValue>();
			if (person != null && person.Bodies != null)
			{
				foreach (AttributedValue<PersonNotes> attributedValue in person.Bodies)
				{
					if (attributedValue.Value != null)
					{
						BodyContentAttributedValue item = new BodyContentAttributedValue(new BodyContentType
						{
							BodyType = BodyType.Text,
							Value = attributedValue.Value.NotesBody,
							IsTruncated = attributedValue.Value.IsTruncated
						}, attributedValue.Attributions);
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00064D7C File Offset: 0x00062F7C
		private List<BodyContentAttributedValue> GetNotesFromActiveDirectory(ADObjectId adObjectId)
		{
			PersonId personId = this.IsAdPersonLinkedInMailbox(adObjectId);
			if (personId != null)
			{
				return this.GetNotesFromStore(personId);
			}
			ADRecipient adrecipient = this.adRecipientSession.FindByObjectGuid(adObjectId.ObjectGuid);
			List<BodyContentAttributedValue> list = new List<BodyContentAttributedValue>();
			if (adrecipient != null)
			{
				IADOrgPerson iadorgPerson = adrecipient as IADOrgPerson;
				if (iadorgPerson != null && !string.IsNullOrWhiteSpace(iadorgPerson.Notes))
				{
					BodyContentType bodyContentType = new BodyContentType();
					bodyContentType.BodyType = BodyType.Text;
					if (iadorgPerson.Notes.Length > this.maxBytesToFetch)
					{
						bodyContentType.Value = iadorgPerson.Notes.Substring(0, this.maxBytesToFetch);
						bodyContentType.IsTruncated = true;
					}
					else
					{
						bodyContentType.Value = iadorgPerson.Notes;
						bodyContentType.IsTruncated = false;
					}
					BodyContentAttributedValue item = new BodyContentAttributedValue(bodyContentType, new string[]
					{
						WellKnownNetworkNames.GAL
					});
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00064E54 File Offset: 0x00063054
		private PersonId IsAdPersonLinkedInMailbox(ADObjectId adObjectId)
		{
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(Persona.FullPersonaShape);
			PropertyDefinition[] propertyDefinitions = propertyListForPersonaResponseShape.GetPropertyDefinitions();
			ADPersonToContactConverterSet adpersonToContactConverterSet = ADPersonToContactConverterSet.FromPersonProperties(propertyDefinitions, null);
			ADRawEntry adRawEntry = this.adRecipientSession.ReadADRawEntry(adObjectId, adpersonToContactConverterSet.ADProperties);
			Person person = Person.FindPersonLinkedToADEntry(base.MailboxIdentityMailboxSession, adRawEntry, propertyDefinitions);
			if (person != null)
			{
				return person.PersonId;
			}
			return null;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00064EAA File Offset: 0x000630AA
		private void WriteTrace(string message, params object[] args)
		{
			ExTraceGlobals.GetPersonaNotesTracer.TraceDebug((long)this.GetHashCode(), message, args);
		}

		// Token: 0x04000ECF RID: 3791
		private static readonly PropertyDefinition[] propertiesToFetch = new PropertyDefinition[]
		{
			PersonSchema.Bodies
		};

		// Token: 0x04000ED0 RID: 3792
		private readonly string personaId;

		// Token: 0x04000ED1 RID: 3793
		private readonly EmailAddressWrapper emailAddress;

		// Token: 0x04000ED2 RID: 3794
		private readonly IRecipientSession adRecipientSession;

		// Token: 0x04000ED3 RID: 3795
		private readonly int maxBytesToFetch;

		// Token: 0x04000ED4 RID: 3796
		private readonly TargetFolderId folderId;
	}
}
