using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	public class SecurityPrincipalIdParameter : RecipientIdParameter, IUrlTokenEncode
	{
		// Token: 0x06000DDA RID: 3546 RVA: 0x00029783 File Offset: 0x00027983
		public SecurityPrincipalIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0002978C File Offset: 0x0002798C
		public SecurityPrincipalIdParameter()
		{
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00029794 File Offset: 0x00027994
		public SecurityPrincipalIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0002979D File Offset: 0x0002799D
		public SecurityPrincipalIdParameter(Mailbox mailbox) : base(mailbox.Id)
		{
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000297AB File Offset: 0x000279AB
		public SecurityPrincipalIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000297B4 File Offset: 0x000279B4
		public SecurityPrincipalIdParameter(SecurityIdentifier sid) : this(SecurityPrincipalIdParameter.GetFriendlyUserName(sid, null))
		{
			this.securityIdentifier = sid;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000297CA File Offset: 0x000279CA
		public SecurityPrincipalIdParameter(SecurityIdentifier sid, string friendlyName) : this(friendlyName)
		{
			this.securityIdentifier = sid;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000297DA File Offset: 0x000279DA
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x000297E2 File Offset: 0x000279E2
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.securityIdentifier;
			}
			internal set
			{
				this.securityIdentifier = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000297EB File Offset: 0x000279EB
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return SecurityPrincipalIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000297F2 File Offset: 0x000279F2
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000297FA File Offset: 0x000279FA
		public bool ReturnUrlTokenEncodedString
		{
			get
			{
				return this.returnUrlTokenEncodedString;
			}
			set
			{
				this.returnUrlTokenEncodedString = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00029804 File Offset: 0x00027A04
		private static Dictionary<string, string> SidToAliasMap
		{
			get
			{
				if (SecurityPrincipalIdParameter.sidToAlias == null)
				{
					lock (SecurityPrincipalIdParameter.syncLock)
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
						Type typeFromHandle = typeof(WellKnownSids);
						Type typeFromHandle2 = typeof(DescriptionAttribute);
						BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
						foreach (FieldInfo fieldInfo in typeFromHandle.GetFields(bindingAttr))
						{
							SecurityIdentifier securityIdentifier = fieldInfo.GetValue(null) as SecurityIdentifier;
							if (!(securityIdentifier == null))
							{
								DescriptionAttribute[] array = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeFromHandle2, true);
								if (array != null)
								{
									try
									{
										dictionary.Add(securityIdentifier.ToString(), array[0].Description);
									}
									catch (ArgumentException)
									{
									}
									SecurityPrincipalIdParameter.sidToAlias = dictionary;
								}
							}
						}
					}
				}
				return SecurityPrincipalIdParameter.sidToAlias;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000298F4 File Offset: 0x00027AF4
		public new static SecurityPrincipalIdParameter Parse(string identity)
		{
			return new SecurityPrincipalIdParameter(identity);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x000298FC File Offset: 0x00027AFC
		public override string ToString()
		{
			string text = SecurityPrincipalIdParameter.MapSidToAlias(base.RawIdentity);
			string text2 = (!string.IsNullOrEmpty(text)) ? text : base.ToString();
			if (this.ReturnUrlTokenEncodedString && text2 != null)
			{
				text2 = UrlTokenConverter.UrlTokenEncode(text2);
			}
			return text2;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002993C File Offset: 0x00027B3C
		internal static string GetFriendlyUserName(IdentityReference sid, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			if (null == sid)
			{
				throw new ArgumentNullException("sid");
			}
			string result;
			try
			{
				result = sid.Translate(typeof(NTAccount)).ToString();
			}
			catch (IdentityNotMappedException ex)
			{
				TaskLogger.Trace("Couldn't resolve the following sid '{0}': {1}", new object[]
				{
					sid.ToString(),
					ex.Message
				});
				if (verboseLogger != null)
				{
					verboseLogger(Strings.VerboseCannotResolveSid(sid.ToString(), ex.Message));
				}
				result = sid.ToString();
			}
			catch (SystemException ex2)
			{
				TaskLogger.Trace("Couldn't resolve the following sid '{0}': {1}", new object[]
				{
					sid.ToString(),
					ex2.Message
				});
				if (verboseLogger != null)
				{
					verboseLogger(Strings.VerboseCannotResolveSid(sid.ToString(), ex2.Message));
				}
				result = sid.ToString();
			}
			return result;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00029A2C File Offset: 0x00027C2C
		internal static IADSecurityPrincipal GetSecurityPrincipal(IRecipientSession session, SecurityPrincipalIdParameter user, Task.TaskErrorLoggingDelegate logError, Task.TaskVerboseLoggingDelegate logVerbose)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (logError == null)
			{
				throw new ArgumentNullException("logError");
			}
			if (logVerbose == null)
			{
				throw new ArgumentNullException("logVerbose");
			}
			ADRecipient adrecipient = null;
			logVerbose(Strings.CheckIfUserIsASID(user.ToString()));
			SecurityIdentifier securityIdentifier = SecurityPrincipalIdParameter.TryParseToSID(user.RawIdentity);
			if (null == securityIdentifier)
			{
				securityIdentifier = SecurityPrincipalIdParameter.GetUserSidAsSAMAccount(user, logError, logVerbose);
			}
			IEnumerable<ADRecipient> objects = user.GetObjects<ADRecipient>(null, session);
			foreach (ADRecipient adrecipient2 in objects)
			{
				if (adrecipient == null)
				{
					adrecipient = adrecipient2;
				}
				else
				{
					logError(new ManagementObjectAmbiguousException(Strings.ErrorUserNotUnique(user.ToString())), ErrorCategory.InvalidData, null);
				}
			}
			if (adrecipient == null && null != securityIdentifier)
			{
				adrecipient = new ADUser();
				adrecipient.propertyBag.SetField(IADSecurityPrincipalSchema.Sid, securityIdentifier);
			}
			if (adrecipient == null)
			{
				logError(new ManagementObjectNotFoundException(Strings.ErrorUserNotFound(user.ToString())), ErrorCategory.InvalidData, null);
			}
			return (IADSecurityPrincipal)adrecipient;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00029B48 File Offset: 0x00027D48
		internal static SecurityIdentifier GetUserSid(IRecipientSession session, SecurityPrincipalIdParameter user, Task.TaskErrorLoggingDelegate logError, Task.TaskVerboseLoggingDelegate logVerbose)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (logError == null)
			{
				throw new ArgumentNullException("logError");
			}
			if (logVerbose == null)
			{
				throw new ArgumentNullException("logVerbose");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			logVerbose(Strings.CheckIfUserIsASID(user.ToString()));
			SecurityIdentifier securityIdentifier = SecurityPrincipalIdParameter.TryParseToSID(user.RawIdentity);
			if (null != securityIdentifier)
			{
				return securityIdentifier;
			}
			logVerbose(Strings.LookupUserAsDomainUser(user.ToString()));
			IEnumerable<ADRecipient> objects = user.GetObjects<ADRecipient>(null, session);
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					securityIdentifier = ((IADSecurityPrincipal)enumerator.Current).Sid;
					if (enumerator.MoveNext())
					{
						logError(new ManagementObjectAmbiguousException(Strings.ErrorUserNotUnique(user.ToString())), ErrorCategory.InvalidData, null);
					}
					return securityIdentifier;
				}
			}
			securityIdentifier = SecurityPrincipalIdParameter.GetUserSidAsSAMAccount(user, logError, logVerbose);
			if (null == securityIdentifier)
			{
				logError(new ManagementObjectNotFoundException(Strings.ErrorUserNotFound(user.ToString())), ErrorCategory.InvalidData, null);
			}
			return securityIdentifier;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00029C5C File Offset: 0x00027E5C
		internal static SecurityIdentifier TryParseToSID(string sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			SecurityIdentifier result;
			try
			{
				result = new SecurityIdentifier(sid);
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00029C98 File Offset: 0x00027E98
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			notFoundReason = new LocalizedString?(LocalizedString.Empty);
			EnumerableWrapper<T> enumerableWrapper = EnumerableWrapper<T>.Empty;
			SecurityIdentifier sid = SecurityPrincipalIdParameter.TryParseToSID(base.RawIdentity);
			string userAccountNameFromSid = SecurityPrincipalIdParameter.GetUserAccountNameFromSid(sid, this.ToString(), null);
			if (!string.IsNullOrEmpty(userAccountNameFromSid))
			{
				enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, base.GetObjectsByAccountName<T>(userAccountNameFromSid, rootId, (IRecipientSession)session, optionalData));
				if (enumerableWrapper.HasElements())
				{
					return enumerableWrapper;
				}
			}
			enumerableWrapper = base.GetEnumerableWrapper<T>(enumerableWrapper, base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (enumerableWrapper.HasElements())
			{
				return enumerableWrapper;
			}
			sid = SecurityPrincipalIdParameter.GetUserSidAsSAMAccount(this, null, null);
			userAccountNameFromSid = SecurityPrincipalIdParameter.GetUserAccountNameFromSid(sid, this.ToString(), null);
			if (!string.IsNullOrEmpty(userAccountNameFromSid))
			{
				enumerableWrapper = base.GetEnumerableWrapper<T>(EnumerableWrapper<T>.Empty, base.GetObjectsByAccountName<T>(userAccountNameFromSid, rootId, (IRecipientSession)session, optionalData));
			}
			return enumerableWrapper;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00029D5F File Offset: 0x00027F5F
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeSecurityPrincipal(id);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00029D67 File Offset: 0x00027F67
		protected virtual SecurityPrincipalIdParameter CreateSidParameter(string identity)
		{
			return new SecurityPrincipalIdParameter(identity);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00029D70 File Offset: 0x00027F70
		internal static SecurityIdentifier GetUserSidAsSAMAccount(SecurityPrincipalIdParameter user, Task.TaskErrorLoggingDelegate logError, Task.TaskVerboseLoggingDelegate logVerbose)
		{
			SecurityIdentifier securityIdentifier = null;
			if (logVerbose != null)
			{
				logVerbose(Strings.LookupUserAsSAMAccount(user.ToString()));
			}
			NTAccount ntaccount;
			try
			{
				ntaccount = new NTAccount(user.RawIdentity);
			}
			catch (ArgumentException)
			{
				if (logVerbose != null)
				{
					logVerbose(Strings.UserNotSAMAccount(user.ToString()));
				}
				return null;
			}
			try
			{
				securityIdentifier = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
			}
			catch (IdentityNotMappedException)
			{
			}
			catch (SystemException innerException)
			{
				if (logError != null)
				{
					logError(new LocalizedException(Strings.ForeignForestTrustFailedException(user.ToString()), innerException), ErrorCategory.InvalidOperation, null);
				}
				return null;
			}
			if (securityIdentifier == null)
			{
				securityIdentifier = SecurityPrincipalIdParameter.MapAliasToSid(user.RawIdentity);
			}
			return securityIdentifier;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00029E38 File Offset: 0x00028038
		private static string MapSidToAlias(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			string result;
			if (!SecurityPrincipalIdParameter.SidToAliasMap.TryGetValue(key, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00029E64 File Offset: 0x00028064
		private static SecurityIdentifier MapAliasToSid(string alias)
		{
			foreach (KeyValuePair<string, string> keyValuePair in SecurityPrincipalIdParameter.SidToAliasMap)
			{
				if (string.Equals(alias, keyValuePair.Value, StringComparison.OrdinalIgnoreCase))
				{
					return new SecurityIdentifier(keyValuePair.Key);
				}
			}
			return null;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00029ED4 File Offset: 0x000280D4
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			if (null != this.securityIdentifier)
			{
				this.binarySecurityIdentifier = ValueConvertor.ConvertValueToBinary(this.securityIdentifier, null);
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00029EF6 File Offset: 0x000280F6
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			this.binarySecurityIdentifier = null;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00029EFF File Offset: 0x000280FF
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.binarySecurityIdentifier != null)
			{
				this.securityIdentifier = (SecurityIdentifier)ValueConvertor.ConvertValueFromBinary(this.binarySecurityIdentifier, typeof(SecurityIdentifier), null);
				this.binarySecurityIdentifier = null;
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00029F34 File Offset: 0x00028134
		private static string GetUserAccountNameFromSid(SecurityIdentifier sid, string user, Task.TaskErrorLoggingDelegate logError)
		{
			string result = null;
			if (sid != null && sid.IsAccountSid())
			{
				try
				{
					string text = sid.Translate(typeof(NTAccount)).ToString();
					string[] array = text.Split(new char[]
					{
						'\\'
					});
					if (array.Length == 2 && string.Compare(array[0], Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0 && !ADSession.IsBoundToAdam && logError != null)
					{
						logError(new CannotHaveLocalAccountException(user), ErrorCategory.InvalidData, null);
					}
					result = text;
				}
				catch (IdentityNotMappedException)
				{
				}
				catch (SystemException innerException)
				{
					if (logError != null)
					{
						logError(new LocalizedException(Strings.ForeignForestTrustFailedException(user), innerException), ErrorCategory.InvalidOperation, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00029FF0 File Offset: 0x000281F0
		private IEnumerable<T> GetUserAccountFromSid<T>(SecurityIdentifier sid, string user, Task.TaskErrorLoggingDelegate logError, ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			string userAccountNameFromSid = SecurityPrincipalIdParameter.GetUserAccountNameFromSid(sid, user, logError);
			if (!string.IsNullOrEmpty(userAccountNameFromSid))
			{
				SecurityPrincipalIdParameter securityPrincipalIdParameter = this.CreateSidParameter(userAccountNameFromSid);
				return securityPrincipalIdParameter.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			return null;
		}

		// Token: 0x040002FF RID: 767
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.User,
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.Group,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup,
			RecipientType.Computer
		};

		// Token: 0x04000300 RID: 768
		private static object syncLock = new object();

		// Token: 0x04000301 RID: 769
		private static Dictionary<string, string> sidToAlias = null;

		// Token: 0x04000302 RID: 770
		[NonSerialized]
		private SecurityIdentifier securityIdentifier;

		// Token: 0x04000303 RID: 771
		private byte[] binarySecurityIdentifier;

		// Token: 0x04000304 RID: 772
		[NonSerialized]
		private bool returnUrlTokenEncodedString;
	}
}
