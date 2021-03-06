using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB5 RID: 3765
	internal class UserProvider
	{
		// Token: 0x06006200 RID: 25088 RVA: 0x00133226 File Offset: 0x00131426
		public UserProvider(IRecipientSession recipientSession)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			this.RecipientSession = recipientSession;
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06006201 RID: 25089 RVA: 0x00133240 File Offset: 0x00131440
		// (set) Token: 0x06006202 RID: 25090 RVA: 0x00133248 File Offset: 0x00131448
		public IRecipientSession RecipientSession { get; private set; }

		// Token: 0x06006203 RID: 25091 RVA: 0x00133254 File Offset: 0x00131454
		public static User ADUserToEntity(ADRawEntry user, IList<PropertyDefinition> properties)
		{
			ArgumentValidator.ThrowIfNull("user", user);
			ArgumentValidator.ThrowIfNull("properties", properties);
			User user2 = new User();
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				propertyDefinition.ADDriverPropertyProvider.GetPropertyFromDataSource(user2, propertyDefinition, user);
			}
			return user2;
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x001332C0 File Offset: 0x001314C0
		public User Read(string id, UserQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			SmtpProxyAddress proxyAddress = null;
			try
			{
				proxyAddress = new SmtpProxyAddress(id, false);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new InvalidUserException(id);
			}
			ADUser aduser = this.RecipientSession.FindByProxyAddress<ADUser>(proxyAddress);
			if (aduser == null)
			{
				throw new InvalidUserException(id);
			}
			return UserProvider.ADUserToEntity(aduser, queryAdapter.RequestedProperties);
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x00133320 File Offset: 0x00131520
		public IFindEntitiesResult<User> Find(UserQueryAdapter queryAdapter)
		{
			ArgumentValidator.ThrowIfNull("queryAdapter", queryAdapter);
			QueryFilter queryFilter = queryAdapter.GetQueryFilter();
			ADPagedReader<ADRawEntry> source = this.RecipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
				queryFilter
			}), queryAdapter.GetSortBy(), 0, queryAdapter.GetRequestedADProperties());
			IEnumerable<ADRawEntry> enumerable = source.Skip(queryAdapter.GetSkipCount()).Take(queryAdapter.GetPageSize());
			List<User> list = new List<User>();
			foreach (ADRawEntry user in enumerable)
			{
				User item = UserProvider.ADUserToEntity(user, queryAdapter.RequestedProperties);
				list.Add(item);
			}
			return new FindEntitiesResult<User>(list, -1);
		}
	}
}
