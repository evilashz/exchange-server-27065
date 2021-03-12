using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A7 RID: 679
	internal abstract class FindPeopleImplementation
	{
		// Token: 0x0600122D RID: 4653 RVA: 0x00059160 File Offset: 0x00057360
		protected FindPeopleImplementation(FindPeopleParameters parameters, HashSet<PropertyPath> additionalSupportedProperties, bool pagingSupported)
		{
			ServiceCommandBase.ThrowIfNull(parameters.Logger, "logger", "FindPeopleImplementation::FindPeopleImplementation");
			this.parameters = parameters;
			this.additionalSupportedProperties = (additionalSupportedProperties ?? FindPeopleImplementation.EmptyPropertyHashSet);
			if (this.parameters.PersonaShape == null)
			{
				this.parameters.PersonaShape = Persona.DefaultPersonaShape;
			}
			if (this.parameters.QueryString != null && this.parameters.QueryString.Length > FindPeopleConfiguration.MaxQueryStringLength)
			{
				this.parameters.QueryString = this.parameters.QueryString.Substring(0, FindPeopleConfiguration.MaxQueryStringLength);
			}
			this.pagingSupported = pagingSupported;
			this.Log(FindPeopleMetadata.QueryString, this.parameters.QueryString);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0005921A File Offset: 0x0005741A
		public string QueryString
		{
			get
			{
				return this.parameters.QueryString;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00059227 File Offset: 0x00057427
		public SortResults[] SortResults
		{
			get
			{
				return this.parameters.SortResults;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00059234 File Offset: 0x00057434
		public BasePagingType Paging
		{
			get
			{
				return this.parameters.Paging;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00059241 File Offset: 0x00057441
		public RestrictionType Restriction
		{
			get
			{
				return this.parameters.Restriction;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0005924E File Offset: 0x0005744E
		public RestrictionType AggregationRestriction
		{
			get
			{
				return this.parameters.AggregationRestriction;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0005925B File Offset: 0x0005745B
		public PersonaResponseShape PersonaShape
		{
			get
			{
				return this.parameters.PersonaShape;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00059268 File Offset: 0x00057468
		public CultureInfo CultureInfo
		{
			get
			{
				return this.parameters.CultureInfo;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00059275 File Offset: 0x00057475
		public RequestDetailsLogger Logger
		{
			get
			{
				return this.parameters.Logger;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00059284 File Offset: 0x00057484
		protected int MaxRows
		{
			get
			{
				int result = FindPeopleConfiguration.MaxRowsDefault;
				if (this.parameters.Paging != null && this.parameters.Paging.MaxRowsSpecified)
				{
					result = this.parameters.Paging.MaxRows;
				}
				return result;
			}
		}

		// Token: 0x06001237 RID: 4663
		public abstract FindPeopleResult Execute();

		// Token: 0x06001238 RID: 4664 RVA: 0x000592C8 File Offset: 0x000574C8
		public virtual void Validate()
		{
			this.ValidatePaging();
			this.ValidatePersonaShape();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000592D8 File Offset: 0x000574D8
		protected QueryFilter GetAndValidateRestrictionFilter()
		{
			QueryFilter queryFilter = this.GetRestrictionFilter();
			if (queryFilter != null)
			{
				this.ValidateSupportedProperties(queryFilter.FilterProperties(), FindPeopleProperties.SupportedRestrictionProperties, "FindPeople Restriction");
				queryFilter = BasePagingType.ApplyQueryAppend(queryFilter, this.Paging);
			}
			return queryFilter;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00059314 File Offset: 0x00057514
		protected QueryFilter GetRestrictionFilter()
		{
			QueryFilter result = null;
			if (this.Restriction != null && this.Restriction.Item != null)
			{
				ServiceObjectToFilterConverter serviceObjectToFilterConverter = new ServiceObjectToFilterConverter();
				result = serviceObjectToFilterConverter.Convert(this.Restriction.Item);
			}
			return result;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00059354 File Offset: 0x00057554
		protected QueryFilter GetAggregationRestrictionFilter()
		{
			QueryFilter queryFilter = null;
			if (this.AggregationRestriction != null && this.AggregationRestriction.Item != null)
			{
				ServiceObjectToFilterConverter serviceObjectToFilterConverter = new ServiceObjectToFilterConverter();
				queryFilter = serviceObjectToFilterConverter.Convert(this.AggregationRestriction.Item);
				foreach (PropertyDefinition propertyDefinition in queryFilter.FilterProperties())
				{
					if (!PersonSchema.Instance.AllProperties.Contains(propertyDefinition))
					{
						throw new UnsupportedPathForQueryException(propertyDefinition, new NotSupportedException(string.Format("Unsupported aggregation property {0}", propertyDefinition.ToString())));
					}
				}
			}
			return queryFilter;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000593F8 File Offset: 0x000575F8
		protected void Log(FindPeopleMetadata metadata, object value)
		{
			this.Logger.Set(metadata, value);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00059410 File Offset: 0x00057610
		protected void ValidateSupportedProperties(IEnumerable<PropertyDefinition> properties, ICollection<PropertyDefinition> supportedProperties, string usage)
		{
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				if (!supportedProperties.Contains(propertyDefinition))
				{
					throw new UnsupportedPathForQueryException(propertyDefinition, new NotSupportedException(string.Format("Unsupported property {0} in {1}", propertyDefinition.ToString(), usage)));
				}
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00059478 File Offset: 0x00057678
		protected virtual void ValidatePaging()
		{
			BasePagingType.Validate(this.Paging);
			if (this.pagingSupported)
			{
				if (!this.Paging.MaxRowsSpecified)
				{
					throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidIndexedPagingParameters);
				}
			}
			else if (((IndexedPageView)this.Paging).Offset != 0)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidIndexedPagingParameters);
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000594D8 File Offset: 0x000576D8
		protected void ValidatePersonaShape()
		{
			if (this.PersonaShape.BaseShape == ShapeEnum.AllProperties)
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidShape);
			}
			if (this.PersonaShape.AdditionalProperties != null)
			{
				foreach (PropertyPath item in this.PersonaShape.AdditionalProperties)
				{
					if (!FindPeopleProperties.SupportedRequestProperties.Contains(item) && !this.additionalSupportedProperties.Contains(item))
					{
						throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidShape);
					}
				}
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0005955C File Offset: 0x0005775C
		public static Persona GetPersona(StoreSession storeSession, IStorePropertyBag propertyBag, int unreadCount = 0, PersonType personType = PersonType.Person)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(ContactSchema.Email1EmailAddress, null);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(StoreObjectSchema.DisplayName, null);
			PersonId valueOrDefault3 = propertyBag.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
			EmailAddressWrapper emailAddressWrapper = null;
			EmailAddressWrapper[] emailAddresses = null;
			if (personType == PersonType.DistributionList)
			{
				emailAddressWrapper = new EmailAddressWrapper
				{
					Name = valueOrDefault2,
					RoutingType = "MAPIPDL"
				};
				emailAddresses = new EmailAddressWrapper[]
				{
					emailAddressWrapper
				};
			}
			else if (valueOrDefault != null)
			{
				emailAddressWrapper = new EmailAddressWrapper
				{
					Name = valueOrDefault2,
					EmailAddress = valueOrDefault,
					RoutingType = propertyBag.GetValueOrDefault<string>(ContactSchema.Email1AddrType, null)
				};
				emailAddresses = new EmailAddressWrapper[]
				{
					emailAddressWrapper
				};
			}
			return new Persona
			{
				PersonaId = IdConverter.PersonaIdFromPersonId(storeSession.MailboxGuid, valueOrDefault3),
				DisplayName = valueOrDefault2,
				EmailAddress = emailAddressWrapper,
				EmailAddresses = emailAddresses,
				ImAddress = propertyBag.GetValueOrDefault<string>(ContactSchema.IMAddress, null),
				UnreadCount = unreadCount,
				RelevanceScore = propertyBag.GetValueOrDefault<int>(ContactSchema.RelevanceScore, int.MaxValue),
				PersonaType = personType.ToString("g")
			};
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0005968C File Offset: 0x0005788C
		public static FindPeopleResult QueryContactsInPublicFolder(PublicFolderSession session, Folder folder, SortBy[] sortBy, IndexedPageView paging, QueryFilter queryFilter = null)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("folder", folder);
			Persona[] array = null;
			int estimatedRowCount;
			using (IQueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, sortBy, FindPeopleImplementation.PublicFolderListContactProperties))
			{
				estimatedRowCount = queryResult.EstimatedRowCount;
				queryResult.SeekToOffset(SeekReference.OriginBeginning, paging.Offset);
				IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(paging.MaxRows);
				array = new Persona[propertyBags.Length];
				for (int i = 0; i < propertyBags.Length; i++)
				{
					PersonType valueOrDefault = propertyBags[i].GetValueOrDefault<PersonType>(ContactSchema.PersonType, PersonType.Person);
					array[i] = FindPeopleImplementation.GetPersona(session, propertyBags[i], 0, valueOrDefault);
					array[i].DisplayNameFirstLast = propertyBags[i].GetValueOrDefault<string>(ContactBaseSchema.DisplayNameFirstLast, null);
					array[i].DisplayNameLastFirst = propertyBags[i].GetValueOrDefault<string>(ContactBaseSchema.DisplayNameLastFirst, null);
				}
			}
			return FindPeopleResult.CreateMailboxBrowseResult(array, estimatedRowCount);
		}

		// Token: 0x04000CF1 RID: 3313
		private static readonly HashSet<PropertyPath> EmptyPropertyHashSet = new HashSet<PropertyPath>();

		// Token: 0x04000CF2 RID: 3314
		private readonly FindPeopleParameters parameters;

		// Token: 0x04000CF3 RID: 3315
		private readonly bool pagingSupported;

		// Token: 0x04000CF4 RID: 3316
		private readonly HashSet<PropertyPath> additionalSupportedProperties;

		// Token: 0x04000CF5 RID: 3317
		private static readonly string PersonaTypePerson = PersonaTypeConverter.ToString(PersonType.Person);

		// Token: 0x04000CF6 RID: 3318
		private static readonly PropertyDefinition[] PublicFolderListContactProperties = new PropertyDefinition[]
		{
			StoreObjectSchema.DisplayName,
			ContactBaseSchema.DisplayNameFirstLast,
			ContactBaseSchema.DisplayNameLastFirst,
			ContactSchema.Email1AddrType,
			ContactSchema.Email1EmailAddress,
			ContactSchema.IMAddress,
			ContactSchema.PersonId,
			ContactSchema.PersonType,
			ContactSchema.RelevanceScore
		};
	}
}
