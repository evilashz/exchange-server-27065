using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002BC RID: 700
	internal sealed class DirectoryCommon
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00067DF0 File Offset: 0x00065FF0
		public static ReadOnlyCollection<string> MailboxReadAttrs
		{
			get
			{
				return DirectoryCommon.mailboxReadAttrs.AsReadOnly();
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x00067DFC File Offset: 0x00065FFC
		public static ReadOnlyCollection<string> MailboxWriteAttrs
		{
			get
			{
				return DirectoryCommon.mailboxWriteAttrs.AsReadOnly();
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00067E08 File Offset: 0x00066008
		public static ReadOnlyCollection<string> ExtraWriteAttrsForEOA
		{
			get
			{
				return DirectoryCommon.extraWriteAttrsForEOA.AsReadOnly();
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00067E14 File Offset: 0x00066014
		public static ReadOnlyCollection<string> ServerWriteAttrs
		{
			get
			{
				return DirectoryCommon.serverWriteAttrs.AsReadOnly();
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00067E20 File Offset: 0x00066020
		public static ReadOnlyCollection<string> TrustedSubsystemWriteAttrs
		{
			get
			{
				return DirectoryCommon.trustedSubsystemWriteAttrs.AsReadOnly();
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00067E2C File Offset: 0x0006602C
		public static ReadOnlyCollection<Guid> PasswordExtendedRights
		{
			get
			{
				return DirectoryCommon.passwordExtendedRights.AsReadOnly();
			}
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00067E38 File Offset: 0x00066038
		public static DNWithBinary FindWellKnownObjectEntry(IEnumerable<DNWithBinary> list, Guid wkGuid)
		{
			return TaskHelper.FindWellKnownObjectEntry(list, wkGuid);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00067E41 File Offset: 0x00066041
		public static ActiveDirectoryAccessRule FindAce(ActiveDirectoryAccessRule ace, ActiveDirectorySecurity acl)
		{
			return DirectoryCommon.FindAce(ace, acl, false, false);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00067E4C File Offset: 0x0006604C
		private static ActiveDirectoryAccessRule FindAce(ActiveDirectoryAccessRule ace, ActiveDirectorySecurity acl, bool includeInherited, bool subsetInsteadOfSuperset)
		{
			AuthorizationRuleCollection accessRules = acl.GetAccessRules(true, includeInherited, typeof(SecurityIdentifier));
			foreach (object obj in accessRules)
			{
				ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)obj;
				if (DirectoryCommon.AceMatches(ace, activeDirectoryAccessRule, subsetInsteadOfSuperset))
				{
					return activeDirectoryAccessRule;
				}
			}
			return null;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00067EC0 File Offset: 0x000660C0
		public static int CountAce(ActiveDirectoryAccessRule ace, ActiveDirectorySecurity acl)
		{
			int num = 0;
			AuthorizationRuleCollection accessRules = acl.GetAccessRules(true, false, typeof(SecurityIdentifier));
			foreach (object obj in accessRules)
			{
				ActiveDirectoryAccessRule ace2 = (ActiveDirectoryAccessRule)obj;
				if (DirectoryCommon.AceMatches(ace, ace2, false))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00067F34 File Offset: 0x00066134
		private static bool InheritsToAtLeastAsMany(ActiveDirectorySecurityInheritance inh1, ActiveDirectorySecurityInheritance inh2)
		{
			if (inh1 == inh2)
			{
				return true;
			}
			switch (inh2)
			{
			case ActiveDirectorySecurityInheritance.None:
				return false;
			case ActiveDirectorySecurityInheritance.All:
				return true;
			case ActiveDirectorySecurityInheritance.Descendents:
				return inh1 == ActiveDirectorySecurityInheritance.Children;
			case ActiveDirectorySecurityInheritance.SelfAndChildren:
				return inh1 == ActiveDirectorySecurityInheritance.None || inh1 == ActiveDirectorySecurityInheritance.Children;
			case ActiveDirectorySecurityInheritance.Children:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00067F7B File Offset: 0x0006617B
		private static bool ObjectTypeMatches(ObjectAceFlags flg1, Guid objType1, Guid inhObjType1, ObjectAceFlags flg2, Guid objType2, Guid inhObjType2)
		{
			return ((flg1 & flg2) != ObjectAceFlags.None || (flg1 == ObjectAceFlags.None && flg2 == ObjectAceFlags.None)) && (ObjectAceFlags.ObjectAceTypePresent != (ObjectAceFlags.ObjectAceTypePresent & flg1 & flg2) || !(objType1 != objType2)) && (ObjectAceFlags.InheritedObjectAceTypePresent != (ObjectAceFlags.InheritedObjectAceTypePresent & flg1 & flg2) || !(inhObjType1 != inhObjType2));
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00067FB4 File Offset: 0x000661B4
		private static bool AceMatches(ActiveDirectoryAccessRule ace1, ActiveDirectoryAccessRule ace2, bool subsetInsteadOfSuperset)
		{
			if (subsetInsteadOfSuperset)
			{
				return ace1.IdentityReference == ace2.IdentityReference && ace1.AccessControlType == ace2.AccessControlType && (ace1.ActiveDirectoryRights & ace2.ActiveDirectoryRights) != (ActiveDirectoryRights)0 && (ace1.InheritanceFlags & ace2.InheritanceFlags) == ace2.InheritanceFlags && (ace1.ObjectFlags & ace2.ObjectFlags) == ace1.ObjectFlags && DirectoryCommon.InheritsToAtLeastAsMany(ace2.InheritanceType, ace1.InheritanceType) && DirectoryCommon.ObjectTypeMatches(ace1.ObjectFlags, ace1.ObjectType, ace1.InheritedObjectType, ace2.ObjectFlags, ace2.ObjectType, ace2.InheritedObjectType);
			}
			return ace1.IdentityReference == ace2.IdentityReference && ace1.AccessControlType == ace2.AccessControlType && (ace1.ActiveDirectoryRights & ace2.ActiveDirectoryRights) == ace1.ActiveDirectoryRights && (ace1.InheritanceFlags & ace2.InheritanceFlags) == ace1.InheritanceFlags && (ace1.ObjectFlags & ace2.ObjectFlags) == ace1.ObjectFlags && DirectoryCommon.InheritsToAtLeastAsMany(ace1.InheritanceType, ace2.InheritanceType) && DirectoryCommon.ObjectTypeMatches(ace1.ObjectFlags, ace1.ObjectType, ace1.InheritedObjectType, ace2.ObjectFlags, ace2.ObjectType, ace2.InheritedObjectType);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00068104 File Offset: 0x00066304
		public static void RemoveAccessRule(ActiveDirectorySecurity acl, ActiveDirectoryAccessRule ace)
		{
			if (!acl.RemoveAccessRule(ace))
			{
				AuthorizationRuleCollection accessRules = acl.GetAccessRules(true, false, typeof(SecurityIdentifier));
				foreach (object obj in accessRules)
				{
					ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)obj;
					if (DirectoryCommon.AceMatches(ace, activeDirectoryAccessRule, false))
					{
						if ((~(ace.ActiveDirectoryRights != (ActiveDirectoryRights)0) & activeDirectoryAccessRule.ActiveDirectoryRights) == (ActiveDirectoryRights)0)
						{
							acl.RemoveAccessRuleSpecific(activeDirectoryAccessRule);
						}
						else
						{
							ActiveDirectoryAccessRule rule = new ActiveDirectoryAccessRule(activeDirectoryAccessRule.IdentityReference, ~ace.ActiveDirectoryRights & activeDirectoryAccessRule.ActiveDirectoryRights, activeDirectoryAccessRule.AccessControlType, activeDirectoryAccessRule.ObjectType, activeDirectoryAccessRule.InheritanceType, activeDirectoryAccessRule.InheritedObjectType);
							acl.RemoveAccessRuleSpecific(activeDirectoryAccessRule);
							acl.AddAccessRule(rule);
						}
					}
				}
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x000681D8 File Offset: 0x000663D8
		public static string AceToString(ActiveDirectoryAccessRule ace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ace.IdentityReference.ToString());
			stringBuilder.Append("; ");
			stringBuilder.Append(ace.AccessControlType.ToString());
			stringBuilder.Append("; ");
			stringBuilder.Append(ace.ActiveDirectoryRights.ToString());
			stringBuilder.Append("; ");
			stringBuilder.Append(ace.InheritanceFlags.ToString());
			stringBuilder.Append(ace.InheritanceType.ToString());
			if ((ace.ObjectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.InheritedObjectAceTypePresent)
			{
				stringBuilder.Append("; ");
				stringBuilder.Append(ace.InheritedObjectType.ToString());
			}
			if ((ace.ObjectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.ObjectAceTypePresent)
			{
				stringBuilder.Append("; ");
				stringBuilder.Append(ace.ObjectType.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x000682E7 File Offset: 0x000664E7
		public static bool FindAces(IDirectorySession session, ADObjectId id, params ActiveDirectoryAccessRule[] aces)
		{
			return DirectoryCommon.FindAces(id, session.ReadSecurityDescriptor(id), aces);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x000682F7 File Offset: 0x000664F7
		public static bool FindAces(ADObject obj, params ActiveDirectoryAccessRule[] aces)
		{
			return DirectoryCommon.FindAces(obj.Id, obj.ReadSecurityDescriptor(), aces);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0006830C File Offset: 0x0006650C
		private static bool FindAces(ADObjectId id, RawSecurityDescriptor rsd, params ActiveDirectoryAccessRule[] aces)
		{
			if (rsd == null)
			{
				throw new SecurityDescriptorAccessDeniedException(id.DistinguishedName);
			}
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			byte[] array = new byte[rsd.BinaryLength];
			rsd.GetBinaryForm(array, 0);
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array);
			foreach (ActiveDirectoryAccessRule ace in aces)
			{
				if (DirectoryCommon.FindAce(ace, activeDirectorySecurity) == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00068375 File Offset: 0x00066575
		public static void RemoveAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, ADObject obj, params ActiveDirectoryAccessRule[] aces)
		{
			DirectoryCommon.SetAces(verboseLogger, warningLogger, errorLogger, obj, true, aces);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00068383 File Offset: 0x00066583
		public static void RemoveAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.ErrorLoggerDelegate errorLogger, IDirectorySession session, ADObjectId id, params ActiveDirectoryAccessRule[] aces)
		{
			DirectoryCommon.SetAces(verboseLogger, null, errorLogger, session, id, true, aces);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00068392 File Offset: 0x00066592
		public static void RemoveAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, IDirectorySession session, ADObjectId id, params ActiveDirectoryAccessRule[] aces)
		{
			DirectoryCommon.SetAces(verboseLogger, warningLogger, errorLogger, session, id, true, aces);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000683A2 File Offset: 0x000665A2
		public static void SetAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, ADObject obj, params ActiveDirectoryAccessRule[] aces)
		{
			DirectoryCommon.SetAces(verboseLogger, warningLogger, null, obj, false, aces);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000683AF File Offset: 0x000665AF
		public static void SetAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, IDirectorySession session, ADObjectId id, params ActiveDirectoryAccessRule[] aces)
		{
			DirectoryCommon.SetAces(verboseLogger, warningLogger, null, session, id, false, aces);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000683BE File Offset: 0x000665BE
		public static void SetAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, IDirectorySession session, ADObjectId id, params ActiveDirectoryAccessRule[] aces)
		{
			verboseLogger(Strings.InfoSetAces(id.DistinguishedName));
			DirectoryCommon.SetAces(verboseLogger, warningLogger, errorLogger, session, id, false, aces);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000683E0 File Offset: 0x000665E0
		private static void SetAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, IDirectorySession session, ADObjectId id, bool remove, params ActiveDirectoryAccessRule[] aces)
		{
			if (verboseLogger != null)
			{
				verboseLogger(Strings.InfoSetAces(id.DistinguishedName));
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			RawSecurityDescriptor rawSecurityDescriptor = session.ReadSecurityDescriptor(id);
			rawSecurityDescriptor = DirectoryCommon.ApplyAcesOnSd(verboseLogger, warningLogger, errorLogger, id, rawSecurityDescriptor, remove, aces);
			if (rawSecurityDescriptor != null)
			{
				session.SaveSecurityDescriptor(id, rawSecurityDescriptor);
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00068444 File Offset: 0x00066644
		private static void SetAces(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, ADObject obj, bool remove, params ActiveDirectoryAccessRule[] aces)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			RawSecurityDescriptor rawSecurityDescriptor = obj.ReadSecurityDescriptor();
			rawSecurityDescriptor = DirectoryCommon.ApplyAcesOnSd(verboseLogger, warningLogger, errorLogger, obj.Id, rawSecurityDescriptor, remove, aces);
			if (rawSecurityDescriptor != null)
			{
				obj.SaveSecurityDescriptor(rawSecurityDescriptor);
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00068484 File Offset: 0x00066684
		private static RawSecurityDescriptor ApplyAcesOnSd(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, ADObjectId id, RawSecurityDescriptor rsd, bool remove, params ActiveDirectoryAccessRule[] aces)
		{
			if (rsd == null)
			{
				throw new SecurityDescriptorAccessDeniedException(id.DistinguishedName);
			}
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			byte[] array = new byte[rsd.BinaryLength];
			rsd.GetBinaryForm(array, 0);
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array);
			if (DirectoryCommon.ApplyAcesOnAcl(verboseLogger, warningLogger, errorLogger, id.DistinguishedName, activeDirectorySecurity, remove, aces))
			{
				return new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
			}
			return null;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000684E8 File Offset: 0x000666E8
		internal static bool ApplyAcesOnAcl(Task.TaskVerboseLoggingDelegate verboseLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.ErrorLoggerDelegate errorLogger, string objectIdentityString, ActiveDirectorySecurity acl, bool remove, params ActiveDirectoryAccessRule[] aces)
		{
			bool result = false;
			if (!acl.AreAccessRulesCanonical)
			{
				LocalizedString message = Strings.InfoAclNotCanonical(objectIdentityString);
				if (errorLogger != null)
				{
					errorLogger(new TaskInvalidOperationException(message), ExchangeErrorCategory.ServerOperation, null);
				}
				else if (warningLogger != null)
				{
					warningLogger(message);
				}
				else if (verboseLogger != null)
				{
					verboseLogger(message);
				}
				return false;
			}
			int i = 0;
			while (i < aces.Length)
			{
				ActiveDirectoryAccessRule activeDirectoryAccessRule = aces[i];
				bool flag = false;
				ActiveDirectoryAccessRule activeDirectoryAccessRule2 = DirectoryCommon.FindAce(activeDirectoryAccessRule, acl, true, remove);
				if (null != activeDirectoryAccessRule2 != remove && (activeDirectoryAccessRule2 == null || !activeDirectoryAccessRule2.IsInherited))
				{
					goto IL_13D;
				}
				if (!remove || !activeDirectoryAccessRule2.IsInherited)
				{
					if (verboseLogger != null)
					{
						if (remove)
						{
							verboseLogger(Strings.InfoRemovingAce(objectIdentityString, DirectoryCommon.AceToString(activeDirectoryAccessRule)));
						}
						else
						{
							verboseLogger(Strings.InfoAddingAce(objectIdentityString, DirectoryCommon.AceToString(activeDirectoryAccessRule)));
						}
					}
					if (remove)
					{
						DirectoryCommon.RemoveAccessRule(acl, activeDirectoryAccessRule);
					}
					else
					{
						acl.AddAccessRule(activeDirectoryAccessRule);
					}
					flag = (result = true);
					goto IL_13D;
				}
				LocalizedString message2 = Strings.ErrorWillNotPerformOnInheritedAce(activeDirectoryAccessRule2.ActiveDirectoryRights.ToString(), activeDirectoryAccessRule2.AccessControlType.ToString(), objectIdentityString);
				if (errorLogger != null)
				{
					errorLogger(new TaskInvalidOperationException(message2), ExchangeErrorCategory.ServerOperation, null);
				}
				else if (warningLogger != null)
				{
					warningLogger(message2);
				}
				else if (verboseLogger != null)
				{
					verboseLogger(message2);
				}
				IL_1DB:
				i++;
				continue;
				IL_13D:
				if ((flag && DirectoryCommon.FindAce(activeDirectoryAccessRule, acl, false, remove) == null == remove) || (verboseLogger == null && warningLogger == null && errorLogger == null))
				{
					goto IL_1DB;
				}
				LocalizedString message3;
				if (remove)
				{
					if (activeDirectoryAccessRule.ObjectFlags == ObjectAceFlags.ObjectAceTypePresent)
					{
						string attr = string.Format("{0} (ObjectType: {1})", activeDirectoryAccessRule.ActiveDirectoryRights, activeDirectoryAccessRule.ObjectType);
						message3 = Strings.InfoAttributeAceNotPresent(objectIdentityString, attr);
					}
					else
					{
						message3 = Strings.InfoAceNotPresent(objectIdentityString, SecurityPrincipalIdParameter.GetFriendlyUserName(activeDirectoryAccessRule.IdentityReference, verboseLogger));
					}
				}
				else
				{
					message3 = Strings.InfoAceAlreadyPresent(objectIdentityString, SecurityPrincipalIdParameter.GetFriendlyUserName(activeDirectoryAccessRule.IdentityReference, verboseLogger));
				}
				if (warningLogger != null)
				{
					warningLogger(message3);
					goto IL_1DB;
				}
				if (verboseLogger != null)
				{
					verboseLogger(message3);
					goto IL_1DB;
				}
				goto IL_1DB;
			}
			return result;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000686E4 File Offset: 0x000668E4
		public static void SetAclOnAlternateProperty(ADObject obj, GenericAce[] aces, PropertyDefinition sdProperty)
		{
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				SecurityIdentifier user = current.User;
				SecurityIdentifier group = user;
				DirectoryCommon.SetAclOnAlternateProperty(obj, aces, sdProperty, user, group);
			}
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00068728 File Offset: 0x00066928
		public static void SetAclOnAlternateProperty(ADObject obj, GenericAce[] aces, PropertyDefinition sdProperty, SecurityIdentifier owner, SecurityIdentifier group)
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(false, true, aces.Length);
			foreach (GenericAce genericAce in aces)
			{
				AccessControlType accessType;
				if (genericAce.AceType == AceType.AccessAllowed || genericAce.AceType == AceType.AccessAllowedObject)
				{
					accessType = AccessControlType.Allow;
				}
				else
				{
					if (genericAce.AceType != AceType.AccessDenied && genericAce.AceType != AceType.AccessDeniedObject)
					{
						throw new AceTypeHasUnsupportedValueException(genericAce.AceType.ToString());
					}
					accessType = AccessControlType.Deny;
				}
				if (genericAce is CommonAce)
				{
					CommonAce commonAce = genericAce as CommonAce;
					discretionaryAcl.AddAccess(accessType, commonAce.SecurityIdentifier, commonAce.AccessMask, commonAce.InheritanceFlags, commonAce.PropagationFlags);
				}
				else
				{
					if (!(genericAce is ObjectAce))
					{
						throw new AceIsUnsupportedTypeException(genericAce.GetType().ToString());
					}
					ObjectAce objectAce = genericAce as ObjectAce;
					discretionaryAcl.AddAccess(accessType, objectAce.SecurityIdentifier, objectAce.AccessMask, objectAce.InheritanceFlags, objectAce.PropagationFlags, objectAce.ObjectAceFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType);
				}
			}
			CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(false, true, ControlFlags.DiscretionaryAclPresent, owner, group, null, discretionaryAcl);
			byte[] binaryForm = new byte[commonSecurityDescriptor.BinaryLength];
			commonSecurityDescriptor.GetBinaryForm(binaryForm, 0);
			RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(binaryForm, 0);
			obj.SetProperties(new PropertyDefinition[]
			{
				sdProperty
			}, new object[]
			{
				rawSecurityDescriptor
			});
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00068888 File Offset: 0x00066A88
		public static void TakeOwnership(ADObjectId id, RawSecurityDescriptor sd, IConfigurationSession session)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (sd == null)
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(false, true, ControlFlags.DiscretionaryAclPresent, current.User, current.User, null, null);
					byte[] binaryForm = new byte[commonSecurityDescriptor.BinaryLength];
					commonSecurityDescriptor.GetBinaryForm(binaryForm, 0);
					sd = new RawSecurityDescriptor(binaryForm, 0);
				}
			}
			session.SaveSecurityDescriptor(id, sd, true);
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00068914 File Offset: 0x00066B14
		public static ADGroup GetDNSAdmins(ADDomain dom, IConfigurationSession session)
		{
			if (dom == null || session == null)
			{
				return null;
			}
			ADObjectId[] ids = new ADObjectId[]
			{
				new ADObjectId("CN=DnsAdmins,CN=Users," + dom.DistinguishedName)
			};
			return session.FindByADObjectIds<ADGroup>(ids)[0].Data;
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00068960 File Offset: 0x00066B60
		public static ADContainer GetAdminSDHolder(ADDomain dom, IConfigurationSession session)
		{
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = session.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.SystemWkGuid, dom.DistinguishedName);
			if (exchangeOrganizationalUnit == null)
			{
				throw new SystemContainerNotFoundException(dom.Fqdn, WellKnownGuid.SystemWkGuid);
			}
			ADContainer adcontainer = session.Read<ADContainer>(exchangeOrganizationalUnit.Id.GetChildId("AdminSDHolder"));
			if (adcontainer == null)
			{
				throw new AdminSDHolderNotFoundException(exchangeOrganizationalUnit.DistinguishedName);
			}
			return adcontainer;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000689BC File Offset: 0x00066BBC
		public static ADContainer GetAdminSDHolder(ADObjectId domainId, IConfigurationSession session)
		{
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = session.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.SystemWkGuid, domainId);
			if (exchangeOrganizationalUnit == null)
			{
				throw new SystemContainerNotFoundException(DNConvertor.FqdnFromDomainDistinguishedName(domainId.DistinguishedName), WellKnownGuid.SystemWkGuid);
			}
			ADContainer adcontainer = session.Read<ADContainer>(exchangeOrganizationalUnit.Id.GetChildId("AdminSDHolder"));
			if (adcontainer == null)
			{
				throw new AdminSDHolderNotFoundException(exchangeOrganizationalUnit.DistinguishedName);
			}
			return adcontainer;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00068A18 File Offset: 0x00066C18
		public static void GrantWriteMembershipPermissionToEOA(ADObject obj, ADGroup eoa, IConfigurationSession configurationSession, Task.TaskVerboseLoggingDelegate logWarning)
		{
			Guid schemaPropertyGuid = DirectoryCommon.GetSchemaPropertyGuid(configurationSession, "member");
			ActiveDirectoryAccessRule activeDirectoryAccessRule = new ActiveDirectoryAccessRule(eoa.Sid, ActiveDirectoryRights.WriteProperty, AccessControlType.Allow, schemaPropertyGuid, ActiveDirectorySecurityInheritance.All);
			DirectoryCommon.SetAces(logWarning, null, obj, new ActiveDirectoryAccessRule[]
			{
				activeDirectoryAccessRule
			});
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00068A58 File Offset: 0x00066C58
		public static Guid GetSchemaPropertyGuid(IConfigurationSession configSess, string prop)
		{
			ADSchemaAttributeObject[] array = configSess.Find<ADSchemaAttributeObject>(configSess.SchemaNamingContext, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADSchemaAttributeSchema.LdapDisplayName, prop), null, 1);
			if (array == null || array.Length == 0)
			{
				throw new CannotFindSchemaAttributeException(prop, configSess.SchemaNamingContext.DistinguishedName, null);
			}
			return array[0].SchemaIDGuid;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00068AA4 File Offset: 0x00066CA4
		public static Guid GetSchemaClassGuid(IConfigurationSession configSess, string className)
		{
			ADSchemaClassObject[] array = configSess.Find<ADSchemaClassObject>(configSess.SchemaNamingContext, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADSchemaObjectSchema.DisplayName, className), null, 1);
			if (array == null || array.Length == 0)
			{
				throw new CannotFindSchemaClassException(className, configSess.SchemaNamingContext.DistinguishedName, null);
			}
			return array[0].SchemaIDGuid;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00068AF0 File Offset: 0x00066CF0
		public static ADGroup FindE12DomainServersGroup(IRecipientSession recipSession, MesoContainer meso)
		{
			return recipSession.Read(meso.Id.GetChildId("Exchange Install Domain Servers")) as ADGroup;
		}

		// Token: 0x04000AAE RID: 2734
		public const int READ_CONTROL = 131072;

		// Token: 0x04000AAF RID: 2735
		public const int CONTROL_ACCESS = 256;

		// Token: 0x04000AB0 RID: 2736
		public const int READ_PROP = 16;

		// Token: 0x04000AB1 RID: 2737
		public const int WRITE_PROP = 32;

		// Token: 0x04000AB2 RID: 2738
		public const int fsdpermUserMailboxOwner = 1;

		// Token: 0x04000AB3 RID: 2739
		public const int fsdpermUserSendAs = 2;

		// Token: 0x04000AB4 RID: 2740
		public const int fsdpermUserPrimaryUser = 4;

		// Token: 0x04000AB5 RID: 2741
		public const string E12DomainServersCN = "Exchange Install Domain Servers";

		// Token: 0x04000AB6 RID: 2742
		private static List<string> mailboxReadAttrs = new List<string>(new string[]
		{
			"garbageCollPeriod",
			"userAccountControl",
			"canonicalName"
		});

		// Token: 0x04000AB7 RID: 2743
		private static List<string> mailboxWriteAttrs = new List<string>(new string[]
		{
			"groupType",
			"msExchUserCulture",
			"publicDelegates",
			"userCertificate",
			"msExchUMPinChecksum",
			"msExchUMSpokenName",
			"msExchMobileMailboxFlags",
			"msExchMailboxSecurityDescriptor",
			"msExchUMServerWritableFlags",
			"msExchUMDtmfMap",
			"msExchSafeSendersHash",
			"msExchSafeRecipientsHash",
			"msExchBlockedSendersHash",
			"thumbnailPhoto"
		});

		// Token: 0x04000AB8 RID: 2744
		private static List<string> extraWriteAttrsForEOA = new List<string>(new string[]
		{
			"adminDisplayName",
			"displayName",
			"legacyExchangeDN",
			"mail",
			"proxyAddresses",
			"showInAddressBook",
			"textEncodedORAddress",
			"garbageCollPeriod",
			"publicDelegates",
			"displayNamePrintable"
		});

		// Token: 0x04000AB9 RID: 2745
		private static List<string> serverWriteAttrs = new List<string>(new string[]
		{
			"msExchServerSite",
			"msExchEdgeSyncCredential"
		});

		// Token: 0x04000ABA RID: 2746
		private static List<string> trustedSubsystemWriteAttrs = new List<string>(new string[]
		{
			"countryCode",
			"sAMAccountName",
			"wWWHomePage",
			"member",
			"managedBy",
			"pwdLastSet",
			"userAccountControl"
		});

		// Token: 0x04000ABB RID: 2747
		private static List<Guid> passwordExtendedRights = new List<Guid>(new Guid[]
		{
			WellKnownGuid.ChangePasswordExtendedRightGuid,
			WellKnownGuid.ResetPasswordOnNextLogonExtendedRightGuid
		});
	}
}
