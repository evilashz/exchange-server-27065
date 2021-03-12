using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A09 RID: 2569
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxJunkEmailConfigurationDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06005E4E RID: 24142 RVA: 0x0018E494 File Offset: 0x0018C694
		// (set) Token: 0x06005E4F RID: 24143 RVA: 0x0018E49C File Offset: 0x0018C69C
		private IRecipientSession Session { get; set; }

		// Token: 0x06005E50 RID: 24144 RVA: 0x0018E4A5 File Offset: 0x0018C6A5
		public MailboxJunkEmailConfigurationDataProvider(ExchangePrincipal mailboxOwner, IRecipientSession session, string action) : base(mailboxOwner, action)
		{
			this.Session = session;
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x0018E4B6 File Offset: 0x0018C6B6
		public MailboxJunkEmailConfigurationDataProvider(ExchangePrincipal mailboxOwner, string action) : base(mailboxOwner, action)
		{
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x0018E4C0 File Offset: 0x0018C6C0
		public MailboxJunkEmailConfigurationDataProvider(MailboxSession session) : base(session)
		{
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x0018E4C9 File Offset: 0x0018C6C9
		internal MailboxJunkEmailConfigurationDataProvider()
		{
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x0018E6C4 File Offset: 0x0018C8C4
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			this.TestArguments<T>(filter, rootId);
			JunkEmailRule rule = base.MailboxSession.JunkEmailRule;
			MailboxJunkEmailConfiguration configuration = (MailboxJunkEmailConfiguration)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			configuration.MailboxOwnerId = base.MailboxSession.MailboxOwner.ObjectId;
			configuration.Enabled = rule.IsEnabled;
			configuration.ContactsTrusted = rule.IsContactsFolderTrusted;
			configuration.TrustedListsOnly = rule.TrustedListsOnly;
			configuration.TrustedSendersAndDomains = this.CompileTrusted(rule);
			configuration.BlockedSendersAndDomains = this.CompileBlocked(rule);
			yield return (T)((object)configuration);
			yield break;
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x0018E6F0 File Offset: 0x0018C8F0
		protected override void InternalSave(ConfigurableObject instance)
		{
			if (this.Session == null)
			{
				throw new DataSourceOperationException(ServerStrings.JunkEmailInvalidConstructionException);
			}
			this.TestForJunkEmailFolder();
			MailboxJunkEmailConfiguration o = (MailboxJunkEmailConfiguration)instance;
			JunkEmailRule junkEmailRule = base.MailboxSession.JunkEmailRule;
			this.PrepareJunkEmailRule(o, junkEmailRule);
			this.SaveRule(junkEmailRule);
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x0018E738 File Offset: 0x0018C938
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxJunkEmailConfigurationDataProvider>(this);
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x0018E740 File Offset: 0x0018C940
		private void TestForJunkEmailFolder()
		{
			try
			{
				if (base.MailboxSession.GetDefaultFolderId(DefaultFolderType.JunkEmail) == null)
				{
					throw new DataSourceOperationException(ServerStrings.JunkEmailFolderNotFoundException);
				}
			}
			catch (ObjectNotFoundException)
			{
				throw new DataSourceOperationException(ServerStrings.JunkEmailFolderNotFoundException);
			}
		}

		// Token: 0x06005E58 RID: 24152 RVA: 0x0018E784 File Offset: 0x0018C984
		private void TestArguments<T>(QueryFilter filter, ObjectId rootId)
		{
			if (filter != null && !(filter is FalseFilter))
			{
				throw new NotSupportedException("filter");
			}
			if (rootId != null && rootId is ADObjectId && !ADObjectId.Equals((ADObjectId)rootId, base.MailboxSession.MailboxOwner.ObjectId))
			{
				throw new NotSupportedException("rootId");
			}
			if (!typeof(MailboxJunkEmailConfiguration).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x0018E840 File Offset: 0x0018CA40
		private string[] CompileTrusted(JunkEmailRule rule)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			hashSet.UnionWith(rule.TrustedSenderEmailCollection);
			hashSet.UnionWith(rule.TrustedSenderDomainCollection);
			hashSet.UnionWith(rule.TrustedRecipientEmailCollection);
			hashSet.UnionWith(rule.TrustedRecipientDomainCollection);
			return Array.ConvertAll<string, string>(hashSet.ToArray<string>(), (string s) => s.TrimStart(new char[]
			{
				'@'
			}));
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x0018E8D0 File Offset: 0x0018CAD0
		private string[] CompileBlocked(JunkEmailRule rule)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			hashSet.UnionWith(rule.BlockedSenderEmailCollection);
			hashSet.UnionWith(rule.BlockedSenderDomainCollection);
			return Array.ConvertAll<string, string>(hashSet.ToArray<string>(), (string s) => s.TrimStart(new char[]
			{
				'@'
			}));
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x0018E928 File Offset: 0x0018CB28
		private void SaveRule(JunkEmailRule rule)
		{
			try
			{
				rule.Save();
			}
			catch (ObjectDisposedException)
			{
				throw new DataSourceTransientException(ServerStrings.JunkEmailObjectDisposedException);
			}
			catch (InvalidOperationException)
			{
				throw new DataSourceTransientException(ServerStrings.JunkEmailInvalidOperationException);
			}
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x0018E974 File Offset: 0x0018CB74
		private void PrepareJunkEmailRule(MailboxJunkEmailConfiguration o, JunkEmailRule rule)
		{
			rule.IsEnabled = o.Enabled;
			rule.TrustedListsOnly = o.TrustedListsOnly;
			if (o.ContactsTrusted)
			{
				rule.SynchronizeContactsCache();
			}
			else
			{
				rule.ClearContactsCache();
			}
			this.SynchronizeTrustedLists(rule);
			this.SetBlockedList(o, rule);
			this.SetTrustedList(o, rule);
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0018E9C8 File Offset: 0x0018CBC8
		private void SynchronizeTrustedLists(JunkEmailRule rule)
		{
			HashSet<string> hashSet = new HashSet<string>(rule.TrustedRecipientEmailCollection, StringComparer.OrdinalIgnoreCase);
			hashSet.UnionWith(rule.TrustedSenderEmailCollection);
			this.SetDelta(hashSet.ToArray<string>(), rule.TrustedRecipientEmailCollection, new MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy(this.SetListWithoutValidation));
			this.SetDelta(hashSet.ToArray<string>(), rule.TrustedSenderEmailCollection, new MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy(this.SetListWithoutValidation));
			HashSet<string> hashSet2 = new HashSet<string>(rule.TrustedRecipientDomainCollection, StringComparer.OrdinalIgnoreCase);
			hashSet2.UnionWith(rule.TrustedSenderDomainCollection);
			this.SetDelta(hashSet2.ToArray<string>(), rule.TrustedRecipientDomainCollection, new MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy(this.SetListWithoutValidation));
			this.SetDelta(hashSet2.ToArray<string>(), rule.TrustedSenderDomainCollection, new MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy(this.SetListWithoutValidation));
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x0018EA8C File Offset: 0x0018CC8C
		private void SetBlockedList(MailboxJunkEmailConfiguration o, JunkEmailRule rule)
		{
			MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple junkEmailValidationTuple = this.SetList(o.BlockedSendersAndDomains, rule.BlockedSenderEmailCollection, rule.BlockedSenderDomainCollection);
			LocalizedString localizedString = LocalizedString.Empty;
			switch (junkEmailValidationTuple.Problem)
			{
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsUsersEmailOrDomain:
				localizedString = ServerStrings.JunkEmailBlockedListOwnersEmailAddressException(junkEmailValidationTuple.Address);
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsInternalToOrganization:
				localizedString = ServerStrings.JunkEmailBlockedListInternalToOrganizationException(junkEmailValidationTuple.Address);
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsDuplicate:
				localizedString = ServerStrings.JunkEmailBlockedListXsoDuplicateException(junkEmailValidationTuple.Address);
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsEmpty:
				localizedString = ServerStrings.JunkEmailBlockedListXsoEmptyException;
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsMalformatted:
				localizedString = ServerStrings.JunkEmailBlockedListXsoFormatException(junkEmailValidationTuple.Address);
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsNull:
				localizedString = ServerStrings.JunkEmailBlockedListXsoNullException;
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsTooBig:
				localizedString = ServerStrings.JunkEmailBlockedListXsoTooBigException(junkEmailValidationTuple.Address);
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsFull:
				localizedString = ServerStrings.JunkEmailBlockedListXsoTooManyException;
				goto IL_C7;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood:
				goto IL_C7;
			}
			localizedString = ServerStrings.JunkEmailBlockedListXsoGenericException(junkEmailValidationTuple.Address);
			IL_C7:
			if (localizedString != LocalizedString.Empty)
			{
				PropertyValidationError propertyValidationError = new PropertyValidationError(localizedString, MailboxJunkEmailConfigurationSchema.BlockedSendersAndDomains, o.BlockedSendersAndDomains);
				throw new PropertyValidationException(localizedString.ToString(), propertyValidationError.PropertyDefinition, new PropertyValidationError[]
				{
					propertyValidationError
				});
			}
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x0018EBA8 File Offset: 0x0018CDA8
		private void SetTrustedList(MailboxJunkEmailConfiguration o, JunkEmailRule rule)
		{
			MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple junkEmailValidationTuple = this.SetList(o.TrustedSendersAndDomains, rule.TrustedSenderEmailCollection, rule.TrustedSenderDomainCollection);
			if (junkEmailValidationTuple.Problem == MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood)
			{
				junkEmailValidationTuple = this.SetList(o.TrustedSendersAndDomains, rule.TrustedRecipientEmailCollection, rule.TrustedRecipientDomainCollection);
			}
			LocalizedString localizedString = LocalizedString.Empty;
			switch (junkEmailValidationTuple.Problem)
			{
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsUsersEmailOrDomain:
				localizedString = ServerStrings.JunkEmailTrustedListOwnersEmailAddressException(junkEmailValidationTuple.Address);
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsInternalToOrganization:
				localizedString = ServerStrings.JunkEmailTrustedListInternalToOrganizationException(junkEmailValidationTuple.Address);
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsDuplicate:
				localizedString = ServerStrings.JunkEmailTrustedListXsoDuplicateException(junkEmailValidationTuple.Address);
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsEmpty:
				localizedString = ServerStrings.JunkEmailTrustedListXsoEmptyException;
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsMalformatted:
				localizedString = ServerStrings.JunkEmailTrustedListXsoFormatException(junkEmailValidationTuple.Address);
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsNull:
				localizedString = ServerStrings.JunkEmailTrustedListXsoNullException;
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsTooBig:
				localizedString = ServerStrings.JunkEmailTrustedListXsoTooBigException(junkEmailValidationTuple.Address);
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsFull:
				localizedString = ServerStrings.JunkEmailTrustedListXsoTooManyException;
				goto IL_EB;
			case MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood:
				goto IL_EB;
			}
			localizedString = ServerStrings.JunkEmailTrustedListXsoGenericException(junkEmailValidationTuple.Address);
			IL_EB:
			if (localizedString != LocalizedString.Empty)
			{
				PropertyValidationError propertyValidationError = new PropertyValidationError(localizedString, MailboxJunkEmailConfigurationSchema.TrustedSendersAndDomains, o.TrustedSendersAndDomains);
				throw new PropertyValidationException(localizedString.ToString(), propertyValidationError.PropertyDefinition, new PropertyValidationError[]
				{
					propertyValidationError
				});
			}
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x0018ECE8 File Offset: 0x0018CEE8
		private MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple SetList(MultiValuedProperty<string> addresses, JunkEmailCollection junkEmails, JunkEmailCollection junkDomains)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string[] array = this.CullEmailsAndDomains(addresses, list, list2);
			foreach (string text in array)
			{
				if (junkEmails.Contains(text))
				{
					list.Add(text);
				}
				else
				{
					if (!junkDomains.Contains(text))
					{
						return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsMalformatted, text);
					}
					list2.Add(text);
				}
			}
			MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple result = this.SetDelta(list.ToArray(), junkEmails, new MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy(this.SetEmailsList));
			if (result.Problem != MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood)
			{
				return result;
			}
			return this.SetDelta(list2.ToArray(), junkDomains, new MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy(this.SetDomainsList));
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x0018EDA0 File Offset: 0x0018CFA0
		private MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple SetDelta(string[] addresses, JunkEmailCollection junk, MailboxJunkEmailConfigurationDataProvider.JunkEmailAdditionStrategy addToRule)
		{
			HashSet<string> hashSet = new HashSet<string>(addresses, StringComparer.OrdinalIgnoreCase);
			hashSet.UnionWith(junk);
			ICollection<string> collection = this.Subtract(hashSet, addresses);
			ICollection<string> collection2 = this.Subtract(hashSet, junk);
			foreach (string item in collection)
			{
				junk.Remove(item);
			}
			if (collection2.Count == 0)
			{
				return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood, string.Empty);
			}
			return addToRule(collection2, junk);
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x0018EE30 File Offset: 0x0018D030
		private MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple SetEmailsList(ICollection<string> emails, JunkEmailCollection junk)
		{
			new List<MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple>();
			foreach (string text in emails)
			{
				if (this.IsUsersEmailOrDomain(text, false))
				{
					return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsUsersEmailOrDomain, text);
				}
				if (this.IsInternalToOrganization(text))
				{
					return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsInternalToOrganization, text);
				}
			}
			try
			{
				junk.AddRange(emails.ToArray<string>());
			}
			catch (JunkEmailValidationException ex)
			{
				return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(ex.Problem, (string)ex.StringFormatParameters[0]);
			}
			return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood, string.Empty);
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x0018EEE8 File Offset: 0x0018D0E8
		private MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple SetDomainsList(ICollection<string> domains, JunkEmailCollection junk)
		{
			foreach (string text in domains)
			{
				if (this.IsUsersEmailOrDomain(text, true))
				{
					return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsUsersEmailOrDomain, text);
				}
			}
			try
			{
				junk.AddRange(domains.ToArray<string>());
			}
			catch (JunkEmailValidationException ex)
			{
				return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(ex.Problem, (string)ex.StringFormatParameters[0]);
			}
			return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood, string.Empty);
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x0018EF88 File Offset: 0x0018D188
		private MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple SetListWithoutValidation(ICollection<string> addresses, JunkEmailCollection junk)
		{
			bool validating = junk.Validating;
			junk.Validating = false;
			junk.AddRange(addresses.ToArray<string>());
			junk.Validating = validating;
			return new MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood, string.Empty);
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x0018EFC4 File Offset: 0x0018D1C4
		private string[] CullEmailsAndDomains(ICollection<string> addresses, List<string> emails, List<string> domains)
		{
			List<string> list = new List<string>();
			foreach (string text in addresses)
			{
				string item = null;
				if (this.TryParseEmail(text, out item))
				{
					emails.Add(item);
				}
				else if (this.TryParseDomain(text, out item))
				{
					domains.Add(item);
				}
				else
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x0018F044 File Offset: 0x0018D244
		private bool TryParseEmail(string address, out string result)
		{
			if (string.IsNullOrEmpty(address))
			{
				result = null;
				return false;
			}
			if (!RoutingAddress.IsValidAddress(address))
			{
				result = null;
				return false;
			}
			result = RoutingAddress.Parse(address).ToString();
			return true;
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x0018F084 File Offset: 0x0018D284
		private bool TryParseDomain(string address, out string result)
		{
			if (string.IsNullOrEmpty(address))
			{
				result = null;
				return false;
			}
			switch (address.IndexOf('@'))
			{
			case -1:
				if (SmtpAddress.IsValidDomain(address))
				{
					result = "@" + address;
					return true;
				}
				result = null;
				return false;
			case 0:
				if (SmtpAddress.IsValidDomain(address.Substring(1)))
				{
					result = address;
					return true;
				}
				result = null;
				return false;
			default:
				result = null;
				return false;
			}
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x0018F0F4 File Offset: 0x0018D2F4
		private bool IsUsersEmailOrDomain(string email, bool isDomain)
		{
			ADRecipient adrecipient = this.Session.Read(base.MailboxSession.MailboxOwner.ObjectId);
			if (adrecipient == null)
			{
				return false;
			}
			foreach (ProxyAddress proxyAddress in adrecipient.EmailAddresses)
			{
				if (proxyAddress != null && proxyAddress.Prefix == ProxyAddressPrefix.Smtp && SmtpAddress.IsValidSmtpAddress(proxyAddress.AddressString))
				{
					SmtpAddress smtpAddress = (SmtpAddress)((SmtpProxyAddress)proxyAddress);
					string b = isDomain ? smtpAddress.Domain.ToString() : smtpAddress.ToString();
					string a = isDomain ? email.Substring(1) : email;
					if (string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x0018F1DC File Offset: 0x0018D3DC
		private bool IsInternalToOrganization(string email)
		{
			return this.Session.IsRecipientInOrg(ProxyAddress.Parse(email));
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0018F1F0 File Offset: 0x0018D3F0
		private ICollection<string> Subtract(HashSet<string> subject, ICollection<string> target)
		{
			HashSet<string> hashSet = new HashSet<string>(subject, StringComparer.OrdinalIgnoreCase);
			foreach (string item in target)
			{
				hashSet.Remove(item);
			}
			return hashSet;
		}

		// Token: 0x02000A0A RID: 2570
		private enum JunkEmailValidationProblem
		{
			// Token: 0x04003494 RID: 13460
			IsUsersEmailOrDomain,
			// Token: 0x04003495 RID: 13461
			IsInternalToOrganization,
			// Token: 0x04003496 RID: 13462
			IsDuplicate,
			// Token: 0x04003497 RID: 13463
			IsEmpty,
			// Token: 0x04003498 RID: 13464
			IsMalformatted,
			// Token: 0x04003499 RID: 13465
			IsNull,
			// Token: 0x0400349A RID: 13466
			IsTooBig,
			// Token: 0x0400349B RID: 13467
			IsFull,
			// Token: 0x0400349C RID: 13468
			IsInvalid,
			// Token: 0x0400349D RID: 13469
			IsGood
		}

		// Token: 0x02000A0B RID: 2571
		// (Invoke) Token: 0x06005E6E RID: 24174
		private delegate MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationTuple JunkEmailAdditionStrategy(ICollection<string> addresses, JunkEmailCollection junk);

		// Token: 0x02000A0C RID: 2572
		private struct JunkEmailValidationTuple
		{
			// Token: 0x06005E71 RID: 24177 RVA: 0x0018F248 File Offset: 0x0018D448
			public JunkEmailValidationTuple(MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem problem, string address)
			{
				this.Problem = problem;
				this.Address = address;
			}

			// Token: 0x06005E72 RID: 24178 RVA: 0x0018F258 File Offset: 0x0018D458
			public JunkEmailValidationTuple(JunkEmailCollection.ValidationProblem problem, string address)
			{
				this.Address = address;
				switch (problem)
				{
				case JunkEmailCollection.ValidationProblem.Null:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsNull;
					return;
				case JunkEmailCollection.ValidationProblem.Duplicate:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsDuplicate;
					return;
				case JunkEmailCollection.ValidationProblem.FormatError:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsMalformatted;
					return;
				case JunkEmailCollection.ValidationProblem.Empty:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsEmpty;
					return;
				case JunkEmailCollection.ValidationProblem.TooBig:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsTooBig;
					return;
				case JunkEmailCollection.ValidationProblem.TooManyEntries:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsFull;
					return;
				case JunkEmailCollection.ValidationProblem.EntryInInvalidEntriesList:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsInvalid;
					return;
				default:
					this.Problem = MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem.IsGood;
					return;
				}
			}

			// Token: 0x0400349E RID: 13470
			public readonly MailboxJunkEmailConfigurationDataProvider.JunkEmailValidationProblem Problem;

			// Token: 0x0400349F RID: 13471
			public readonly string Address;
		}
	}
}
