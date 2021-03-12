using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200051A RID: 1306
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PeopleIKnowEmailAddressCollectionFolderProperty : IPeopleIKnowPublisher
	{
		// Token: 0x06003806 RID: 14342 RVA: 0x000E591B File Offset: 0x000E3B1B
		public PeopleIKnowEmailAddressCollectionFolderProperty(IXSOFactory xsoFactory, ITracer tracer, int traceId)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.xsoFactory = xsoFactory;
			this.tracer = tracer;
			this.traceId = traceId;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000E5950 File Offset: 0x000E3B50
		public void Publish(IMailboxSession session)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			try
			{
				IDictionary<string, PeopleIKnowMetadata> systemFavorites = this.GetSystemFavorites(session);
				if (systemFavorites.Count > 0)
				{
					PeopleIKnowEmailAddressCollection peopleIKnowEmailAddressCollection = PeopleIKnowEmailAddressCollection.CreateFromStringCollection(systemFavorites, this.tracer, this.traceId, 1);
					byte[] data = peopleIKnowEmailAddressCollection.Data;
					PeopleIKnowEmailAddressCollection peopleIKnowEmailAddressCollection2 = PeopleIKnowEmailAddressCollection.CreateFromStringCollection(systemFavorites, this.tracer, this.traceId, 2);
					byte[] data2 = peopleIKnowEmailAddressCollection2.Data;
					using (IFolder folder = this.xsoFactory.BindToFolder(session, DefaultFolderType.Inbox, PeopleIKnowEmailAddressCollectionFolderProperty.PeopleIKnowEmailAddressCollectionPropertyArray))
					{
						folder[FolderSchema.PeopleIKnowEmailAddressCollection] = data;
						folder[FolderSchema.PeopleIKnowEmailAddressRelevanceScoreCollection] = data2;
						folder.Save();
						goto IL_DA;
					}
				}
				using (IFolder folder2 = this.xsoFactory.BindToFolder(session, DefaultFolderType.Inbox, PeopleIKnowEmailAddressCollectionFolderProperty.PeopleIKnowEmailAddressCollectionPropertyArray))
				{
					folder2.Delete(FolderSchema.PeopleIKnowEmailAddressCollection);
					folder2.Delete(FolderSchema.PeopleIKnowEmailAddressRelevanceScoreCollection);
					folder2.Save();
				}
				IL_DA:;
			}
			catch (ObjectNotFoundException arg)
			{
				this.tracer.TraceDebug<IMailboxSession, ObjectNotFoundException>((long)this.GetHashCode(), "People I Know email addresses container has not been initialized or has been deleted for mailbox '{0}'.  Exception: {1}", session, arg);
			}
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000E5A80 File Offset: 0x000E3C80
		private IDictionary<string, PeopleIKnowMetadata> GetSystemFavorites(IMailboxSession session)
		{
			Dictionary<string, PeopleIKnowMetadata> dictionary = new Dictionary<string, PeopleIKnowMetadata>();
			using (IFolder folder = this.xsoFactory.BindToFolder(session, DefaultFolderType.RecipientCache))
			{
				using (IQueryResult queryResult = folder.PersonItemQuery(PeopleIKnowEmailAddressCollectionFolderProperty.EmptyFilter, PeopleIKnowEmailAddressCollectionFolderProperty.EmptyFilter, PeopleIKnowEmailAddressCollectionFolderProperty.AnySort, PeopleIKnowEmailAddressCollectionFolderProperty.RecipientCacheProperties))
				{
					IStorePropertyBag[] propertyBags;
					do
					{
						propertyBags = queryResult.GetPropertyBags(10000);
						if (propertyBags != null && propertyBags.Length > 0)
						{
							foreach (IStorePropertyBag storePropertyBag in propertyBags)
							{
								PersonType valueOrDefault = storePropertyBag.GetValueOrDefault<PersonType>(PersonSchema.PersonType, PersonType.Unknown);
								if (valueOrDefault == PersonType.Person)
								{
									Participant[] valueOrDefault2 = storePropertyBag.GetValueOrDefault<Participant[]>(PersonSchema.EmailAddresses, null);
									int valueOrDefault3 = storePropertyBag.GetValueOrDefault<int>(PersonSchema.RelevanceScore, int.MaxValue);
									if (valueOrDefault2 != null && valueOrDefault2.Length > 0)
									{
										foreach (Participant participant in valueOrDefault2)
										{
											if (!string.IsNullOrEmpty(participant.EmailAddress) && !this.DoesEmailAddressMatchMailboxOwner(participant.EmailAddress, session.MailboxOwner))
											{
												dictionary[participant.EmailAddress] = new PeopleIKnowMetadata
												{
													RelevanceScore = valueOrDefault3
												};
											}
										}
									}
								}
							}
						}
					}
					while (propertyBags.Length > 0);
				}
			}
			return dictionary;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000E5BF8 File Offset: 0x000E3DF8
		private bool DoesEmailAddressMatchMailboxOwner(string emailAddress, IExchangePrincipal exchangePrincipal)
		{
			foreach (string value in exchangePrincipal.GetAllEmailAddresses())
			{
				if (emailAddress.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001DEB RID: 7659
		private static readonly QueryFilter EmptyFilter = null;

		// Token: 0x04001DEC RID: 7660
		private static readonly SortBy[] AnySort = null;

		// Token: 0x04001DED RID: 7661
		private static readonly PropertyDefinition[] PeopleIKnowEmailAddressCollectionPropertyArray = new PropertyDefinition[]
		{
			FolderSchema.PeopleIKnowEmailAddressCollection,
			FolderSchema.PeopleIKnowEmailAddressRelevanceScoreCollection
		};

		// Token: 0x04001DEE RID: 7662
		private static readonly PropertyDefinition[] RecipientCacheProperties = new PropertyDefinition[]
		{
			PersonSchema.PersonType,
			PersonSchema.EmailAddresses,
			PersonSchema.RelevanceScore
		};

		// Token: 0x04001DEF RID: 7663
		private readonly ITracer tracer;

		// Token: 0x04001DF0 RID: 7664
		private readonly int traceId;

		// Token: 0x04001DF1 RID: 7665
		private readonly IXSOFactory xsoFactory;
	}
}
