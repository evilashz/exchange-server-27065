using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200011E RID: 286
	[Serializable]
	public class RecipientIdParameter : ADIdParameter
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x0002214B File Offset: 0x0002034B
		public RecipientIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00022166 File Offset: 0x00020366
		public RecipientIdParameter()
		{
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00022180 File Offset: 0x00020380
		public RecipientIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002219B File Offset: 0x0002039B
		public RecipientIdParameter(ADObject recipient) : base(recipient.Id)
		{
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000221BB File Offset: 0x000203BB
		public RecipientIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x000221D6 File Offset: 0x000203D6
		internal virtual RecipientType[] RecipientTypes
		{
			get
			{
				return RecipientIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000221DD File Offset: 0x000203DD
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x000221E5 File Offset: 0x000203E5
		internal bool SearchWithDisplayName
		{
			get
			{
				return this.searchWithDisplayName;
			}
			set
			{
				this.searchWithDisplayName = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000221EE File Offset: 0x000203EE
		internal Guid RawMailboxGuidInvolvedInSearch
		{
			get
			{
				return this.rawMailboxGuidInvolvedInSearch;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000221F6 File Offset: 0x000203F6
		public static RecipientIdParameter Parse(string identity)
		{
			return new RecipientIdParameter(identity);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000221FE File Offset: 0x000203FE
		internal static QueryFilter GetRecipientTypeFilter(RecipientType[] recipientTypes)
		{
			return RecipientIdParameter.GetRecipientFilter<RecipientType>(recipientTypes, ADRecipientSchema.RecipientType);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002220B File Offset: 0x0002040B
		internal static QueryFilter GetRecipientTypeDetailsFilter(RecipientTypeDetails[] recipientTypeDetails)
		{
			return RecipientIdParameter.GetRecipientFilter<RecipientTypeDetails>(recipientTypeDetails, ADRecipientSchema.RecipientTypeDetails);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00022218 File Offset: 0x00020418
		internal override IEnumerableFilter<T> GetEnumerableFilter<T>()
		{
			return RecipientTypeFilter<T>.GetRecipientTypeFilter(this.RecipientTypes);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00022228 File Offset: 0x00020428
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			if (!typeof(ADRecipient).IsAssignableFrom(typeof(T)) && !typeof(ReducedRecipient).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (Globals.IsConsumerOrganization(session.SessionSettings.CurrentOrganizationId) && ADSessionFactory.UseAggregateSession(session.SessionSettings))
			{
				return ConsumerMailboxIdParameter.Parse(base.RawIdentity).GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			EnumerableWrapper<T> enumerableWrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (!enumerableWrapper.HasElements() && session is IRecipientSession)
			{
				enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, this.GetObjectsByAccountName<T>(base.RawIdentity, rootId, (IRecipientSession)session, optionalData));
			}
			if (enumerableWrapper.HasUnfilteredElements() && !enumerableWrapper.HasElements())
			{
				notFoundReason = new LocalizedString?(this.GetErrorMessageForWrongType(this.ToString()));
			}
			TaskLogger.LogExit();
			return enumerableWrapper;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002233C File Offset: 0x0002053C
		internal IEnumerable<T> GetObjectsByAccountName<T>(string accountName, ADObjectId rootId, IRecipientSession session, OptionalIdentityData optionalData) where T : IConfigurable, new()
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			QueryFilter queryFilter = RecipientIdParameter.GetRecipientTypeFilter(this.RecipientTypes);
			queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				queryFilter,
				this.AdditionalQueryFilter,
				(optionalData != null) ? optionalData.AdditionalFilter : null
			});
			return session.FindByAccountName<T>(null, accountName, rootId, queryFilter);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002239C File Offset: 0x0002059C
		internal T GetObjectInOrganization<T>(ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			T t = default(T);
			notFoundReason = null;
			IEnumerable<T> objectsInOrganization = this.GetObjectsInOrganization<T>(base.RawIdentity, rootId, session, optionalData);
			T result;
			using (IEnumerator<T> enumerator = objectsInOrganization.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
					result = default(T);
				}
				else
				{
					t = enumerator.Current;
					if (enumerator.MoveNext())
					{
						notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectAmbiguous(this.ToString()));
						result = default(T);
					}
					else
					{
						result = t;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00022450 File Offset: 0x00020650
		internal override IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData)
		{
			if (Globals.IsConsumerOrganization(session.SessionSettings.CurrentOrganizationId) && ADSessionFactory.UseAggregateSession(session.SessionSettings))
			{
				LocalizedString? localizedString;
				return ConsumerMailboxIdParameter.Parse(base.RawIdentity).GetObjects<T>(rootId, session, session, optionalData, out localizedString);
			}
			List<QueryFilter> list = new List<QueryFilter>();
			QueryFilter item = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ExternalDirectoryObjectId, identityString);
			list.Add(item);
			SmtpAddress smtpAddress = new SmtpAddress(identityString);
			if (smtpAddress.IsValidAddress)
			{
				QueryFilter item2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "SMTP:" + smtpAddress.ToString());
				list.Add(item2);
				QueryFilter item3 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ExternalEmailAddress, "SMTP:" + smtpAddress.ToString());
				list.Add(item3);
				QueryFilter item4 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.WindowsLiveID, smtpAddress.ToString());
				list.Add(item4);
			}
			QueryFilter item5 = base.CreateWildcardOrEqualFilter(ADUserSchema.UserPrincipalName, identityString);
			list.Add(item5);
			QueryFilter item6 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, identityString);
			list.Add(item6);
			QueryFilter item7 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "X500:" + identityString);
			list.Add(item7);
			QueryFilter queryFilter = base.CreateWildcardOrEqualFilter(ADRecipientSchema.Alias, identityString);
			if (queryFilter != null)
			{
				list.Add(queryFilter);
			}
			if (this.SearchWithDisplayName)
			{
				QueryFilter queryFilter2 = base.CreateWildcardOrEqualFilter(ADRecipientSchema.DisplayName, identityString);
				if (queryFilter2 != null)
				{
					list.Add(queryFilter2);
				}
			}
			NetID propertyValue;
			if (NetID.TryParse(identityString, out propertyValue))
			{
				QueryFilter item8 = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.NetID, propertyValue);
				list.Add(item8);
			}
			Guid guid = Guid.Empty;
			if (base.InternalADObjectId != null)
			{
				guid = base.InternalADObjectId.ObjectGuid;
			}
			if (Guid.Empty != guid || GuidHelper.TryParseGuid(identityString, out guid))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ExchangeGuid, guid));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveGuid, guid));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeObjectId, guid));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxGuidsRaw, guid.ToString()));
				this.rawMailboxGuidInvolvedInSearch = guid;
			}
			QueryFilter queryFilter3 = QueryFilter.OrTogether(list.ToArray());
			QueryFilter recipientTypeFilter = RecipientIdParameter.GetRecipientTypeFilter(this.RecipientTypes);
			queryFilter3 = QueryFilter.AndTogether(new QueryFilter[]
			{
				queryFilter3,
				recipientTypeFilter
			});
			EnumerableWrapper<T> enumerableWrapper = EnumerableWrapper<T>.GetWrapper(base.PerformPrimarySearch<T>(queryFilter3, rootId, session, true, optionalData));
			if (!enumerableWrapper.HasElements())
			{
				this.rawMailboxGuidInvolvedInSearch = Guid.Empty;
				OptionalIdentityData optionalIdentityData;
				if (optionalData == null)
				{
					optionalIdentityData = new OptionalIdentityData();
					optionalIdentityData.AdditionalFilter = recipientTypeFilter;
				}
				else
				{
					optionalIdentityData = optionalData.Clone();
					optionalIdentityData.AdditionalFilter = QueryFilter.AndTogether(new QueryFilter[]
					{
						optionalIdentityData.AdditionalFilter,
						recipientTypeFilter
					});
				}
				enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, base.GetObjectsInOrganization<T>(identityString, rootId, session, optionalIdentityData));
			}
			return enumerableWrapper;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00022740 File Offset: 0x00020940
		internal T GetObject<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, RecipientIdParameter.GetObjectsDelegate<T> getObjects, out LocalizedString? notFoundReason)
		{
			T t = default(T);
			IEnumerable<T> enumerable = getObjects(rootId, session, subTreeSession, optionalData, out notFoundReason);
			T result;
			using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
			{
				if (notFoundReason != null || !enumerator.MoveNext())
				{
					if (notFoundReason == null)
					{
						if (((IConfigDataProvider)session).Source != null)
						{
							notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFoundWithSource(this.ToString(), ((IConfigDataProvider)session).Source));
						}
						else
						{
							notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
						}
					}
					result = default(T);
				}
				else
				{
					t = enumerator.Current;
					if (enumerator.MoveNext())
					{
						notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectAmbiguous(this.ToString()));
						result = default(T);
					}
					else
					{
						result = t;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002282C File Offset: 0x00020A2C
		protected virtual LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeRecipientIdParamter(id);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00022834 File Offset: 0x00020A34
		private static QueryFilter GetRecipientFilter<TEnum>(TEnum[] recipientTypes, ADPropertyDefinition propDef)
		{
			QueryFilter result = null;
			if (recipientTypes != null && recipientTypes.Length > 0)
			{
				ComparisonFilter[] array = new ComparisonFilter[recipientTypes.Length];
				for (int i = 0; i < recipientTypes.Length; i++)
				{
					array[i] = new ComparisonFilter(ComparisonOperator.Equal, propDef, recipientTypes[i]);
				}
				if (recipientTypes.Length == 1)
				{
					result = array[0];
				}
				else
				{
					result = new OrFilter(array);
				}
			}
			return result;
		}

		// Token: 0x0400027F RID: 639
		internal static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.DynamicDistributionGroup,
			RecipientType.UserMailbox,
			RecipientType.MailContact,
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup,
			RecipientType.MailUser,
			RecipientType.PublicFolder
		};

		// Token: 0x04000280 RID: 640
		private bool searchWithDisplayName = true;

		// Token: 0x04000281 RID: 641
		private Guid rawMailboxGuidInvolvedInSearch = Guid.Empty;

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x06000A4C RID: 2636
		internal delegate IEnumerable<T> GetObjectsDelegate<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason);
	}
}
