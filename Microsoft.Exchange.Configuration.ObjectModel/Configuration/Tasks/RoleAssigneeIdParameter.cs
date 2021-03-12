using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000143 RID: 323
	[Serializable]
	public class RoleAssigneeIdParameter : ADIdParameter
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x000247CD File Offset: 0x000229CD
		public RoleAssigneeIdParameter()
		{
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000247D5 File Offset: 0x000229D5
		public RoleAssigneeIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000247DE File Offset: 0x000229DE
		public RoleAssigneeIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000247E7 File Offset: 0x000229E7
		public RoleAssigneeIdParameter(Mailbox mailbox) : base(mailbox.Id)
		{
			this.spParameter = new SecurityPrincipalIdParameter(mailbox);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00024801 File Offset: 0x00022A01
		public RoleAssigneeIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002480A File Offset: 0x00022A0A
		public RoleAssigneeIdParameter(SecurityIdentifier sid) : base(sid.ToString())
		{
			this.spParameter = new SecurityPrincipalIdParameter(sid);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00024824 File Offset: 0x00022A24
		public RoleAssigneeIdParameter(RoleGroup group) : base(group.Id)
		{
			this.spParameter = new SecurityPrincipalIdParameter(group.Id);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00024843 File Offset: 0x00022A43
		public RoleAssigneeIdParameter(RoleAssignmentPolicy policy) : base(policy.Id)
		{
			this.policyParameter = new MailboxPolicyIdParameter(policy);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002485D File Offset: 0x00022A5D
		public static RoleAssigneeIdParameter Parse(string identity)
		{
			return new RoleAssigneeIdParameter(identity);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00024868 File Offset: 0x00022A68
		internal static ADObject GetRawRoleAssignee(RoleAssigneeIdParameter user, IConfigurationSession configSession, IRecipientSession recipientSession)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			IEnumerable<ADObject> enumerable = user.GetObjects<RoleAssignmentPolicy>(null, configSession).Cast<ADObject>();
			EnumerableWrapper<ADObject> wrapper = EnumerableWrapper<ADObject>.GetWrapper(enumerable);
			if (!wrapper.HasElements())
			{
				SecurityIdentifier securityIdentifier = SecurityPrincipalIdParameter.TryParseToSID(user.RawIdentity);
				if (null != securityIdentifier)
				{
					ADRecipient adrecipient = recipientSession.FindBySid(securityIdentifier);
					if (adrecipient != null)
					{
						enumerable = new ADObject[]
						{
							adrecipient
						};
						wrapper = EnumerableWrapper<ADObject>.GetWrapper(enumerable);
					}
				}
				else
				{
					enumerable = user.GetObjects<ADRecipient>(null, recipientSession).Cast<ADObject>();
					wrapper = EnumerableWrapper<ADObject>.GetWrapper(enumerable);
				}
			}
			ADObject result = null;
			using (IEnumerator<ADObject> enumerator = wrapper.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorPolicyUserOrSecurityGroupNotFound(user.ToString()));
				}
				result = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorPolicyUserOrSecurityGroupNotUnique(user.ToString()));
				}
			}
			return result;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00024974 File Offset: 0x00022B74
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (subTreeSession == null)
			{
				throw new ArgumentNullException("subTreeSession");
			}
			if (!typeof(T).IsAssignableFrom(typeof(ADRecipient)) && !typeof(T).IsAssignableFrom(typeof(RoleAssignmentPolicy)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			bool flag = session is IConfigurationSession && subTreeSession is IConfigurationSession;
			bool flag2 = session is IRecipientSession && subTreeSession is IRecipientSession;
			if (flag)
			{
				if (typeof(T).IsAssignableFrom(typeof(RoleAssignmentPolicy)))
				{
					if (this.policyParameter == null)
					{
						this.policyParameter = ((base.InternalADObjectId != null) ? new MailboxPolicyIdParameter(base.InternalADObjectId) : new MailboxPolicyIdParameter(base.RawIdentity));
					}
					return this.policyParameter.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
				}
				throw new ArgumentException("Argument Mismatch. Type T is located on Config NC and sessions aren't of IConfigurationSession type.");
			}
			else
			{
				if (!flag2)
				{
					throw new ArgumentException(string.Format("Invalid Session Type. Session isn't of type 'RecipientSession' or 'SystemConfigurationSession'. Session type is '{0}'", session.GetType().Name), "session");
				}
				if (typeof(T).IsAssignableFrom(typeof(ADRecipient)))
				{
					if (this.spParameter == null)
					{
						this.spParameter = ((base.InternalADObjectId != null) ? new SecurityPrincipalIdParameter(base.InternalADObjectId) : new SecurityPrincipalIdParameter(base.RawIdentity));
					}
					return this.spParameter.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
				}
				throw new ArgumentException("Argument Mismatch. Type T is located on Domain NC and sessions aren't of IRecipientSession type.");
			}
		}

		// Token: 0x040002A4 RID: 676
		private SecurityPrincipalIdParameter spParameter;

		// Token: 0x040002A5 RID: 677
		private MailboxPolicyIdParameter policyParameter;
	}
}
