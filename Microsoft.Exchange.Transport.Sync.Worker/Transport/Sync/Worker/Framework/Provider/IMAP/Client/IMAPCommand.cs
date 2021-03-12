using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.IMAP.Client
{
	// Token: 0x020001D1 RID: 465
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPCommand
	{
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00022E58 File Offset: 0x00021058
		internal IMAPCommand()
		{
			this.cachedStringBuilder = new StringBuilder(128);
			this.commandParameters = new List<object>(5);
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00022E7C File Offset: 0x0002107C
		internal string CommandId
		{
			get
			{
				return this.commandId;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00022E84 File Offset: 0x00021084
		internal IMAPCommandType CommandType
		{
			get
			{
				return this.commandType;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00022E8C File Offset: 0x0002108C
		internal IList<object> CommandParameters
		{
			get
			{
				return this.commandParameters;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00022E94 File Offset: 0x00021094
		private StringBuilder CachedStringBuilder
		{
			get
			{
				return this.cachedStringBuilder;
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00022F04 File Offset: 0x00021104
		internal void ResetAsLogin(string newCommandId, string user, SecureString password)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("user", user);
			SyncUtilities.ThrowIfArgumentNull("password", password);
			this.Reset(IMAPCommandType.Login, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} LOGIN \"{1}\" \"{2}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0]),
				cmd.ConvertToQuotableString(SyncUtilities.SecureStringToString((SecureString)cmd.CommandParameters[1]))
			}), new object[]
			{
				user,
				password
			});
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00022FB0 File Offset: 0x000211B0
		internal void ResetAsId(string newCommandId, SecureString clientToken)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNull("clientToken", clientToken);
			this.Reset(IMAPCommandType.Id, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} ID {1}\r\n", new object[]
			{
				cmd.CommandId,
				SyncUtilities.SecureStringToString((SecureString)cmd.CommandParameters[0])
			}), new object[]
			{
				clientToken
			});
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00023035 File Offset: 0x00021235
		internal void ResetAsLogout(string newCommandId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			this.Reset(IMAPCommandType.Logout, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} LOGOUT\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002309D File Offset: 0x0002129D
		internal void ResetAsStarttls(string newCommandId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			this.Reset(IMAPCommandType.Starttls, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} STARTTLS\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00023168 File Offset: 0x00021368
		internal void ResetAsAuthenticate(string newCommandId, IMAPAuthenticationMechanism authMechanism, string user, SecureString password, AuthenticationContext authContext)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNull("user", user);
			SyncUtilities.ThrowIfArgumentNull("password", password);
			this.Reset(IMAPCommandType.Authenticate, newCommandId, delegate(IMAPCommand cmd)
			{
				StringBuilder stringBuilder = cmd.cachedStringBuilder;
				stringBuilder.Length = 0;
				stringBuilder.Append(cmd.CommandId);
				stringBuilder.Append(" AUTHENTICATE");
				IMAPAuthenticationMechanism authMechanism2 = authMechanism;
				if (authMechanism2 != IMAPAuthenticationMechanism.Basic)
				{
					if (authMechanism2 != IMAPAuthenticationMechanism.Ntlm)
					{
						throw new InvalidOperationException("Unexpected authentication mechanism " + authMechanism);
					}
					stringBuilder.Append(" NTLM\r\n");
				}
				else
				{
					stringBuilder.Append(" PLAIN\r\n");
				}
				return stringBuilder.ToString();
			}, new object[]
			{
				authMechanism,
				user,
				password,
				authContext
			});
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002320D File Offset: 0x0002140D
		internal void ResetAsCapability(string newCommandId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			this.Reset(IMAPCommandType.Capability, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} CAPABILITY\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00023275 File Offset: 0x00021475
		internal void ResetAsExpunge(string newCommandId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			this.Reset(IMAPCommandType.Expunge, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} EXPUNGE\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00023300 File Offset: 0x00021500
		internal void ResetAsSelect(string newCommandId, IMAPMailbox imapMailbox)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNull("imapMailbox", imapMailbox);
			this.Reset(IMAPCommandType.Select, newCommandId, delegate(IMAPCommand cmd)
			{
				string text = cmd.ConvertToQuotableString(((IMAPMailbox)cmd.CommandParameters[0]).NameOnTheWire);
				return string.Format(CultureInfo.InvariantCulture, "{0} SELECT \"{1}\"\r\n", new object[]
				{
					cmd.CommandId,
					text
				});
			}, new object[]
			{
				imapMailbox
			});
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000233A4 File Offset: 0x000215A4
		internal void ResetAsStatus(string newCommandId, IMAPMailbox imapMailbox)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNull("imapMailbox", imapMailbox);
			this.Reset(IMAPCommandType.Status, newCommandId, delegate(IMAPCommand cmd)
			{
				string text = cmd.ConvertToQuotableString(((IMAPMailbox)cmd.CommandParameters[0]).NameOnTheWire);
				return string.Format(CultureInfo.InvariantCulture, "{0} STATUS \"{1}\" (UIDNEXT)\r\n", new object[]
				{
					cmd.CommandId,
					text
				});
			}, new object[]
			{
				imapMailbox
			});
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00023514 File Offset: 0x00021714
		internal void ResetAsFetch(string newCommandId, string start, string end, bool uidFetch, IList<string> dataItemSet)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("start", start);
			SyncUtilities.ThrowIfArgumentNull("dataItemSet", dataItemSet);
			if (dataItemSet.Count == 0)
			{
				throw new ArgumentException("dataItemSet cannot be empty");
			}
			this.Reset(IMAPCommandType.Fetch, newCommandId, delegate(IMAPCommand cmd)
			{
				bool flag = (bool)cmd.CommandParameters[2];
				StringBuilder stringBuilder = cmd.cachedStringBuilder;
				stringBuilder.Length = 0;
				stringBuilder.Append(cmd.CommandId);
				if (flag)
				{
					stringBuilder.Append(" UID");
				}
				stringBuilder.Append(" FETCH ");
				stringBuilder.Append(cmd.CommandParameters[0]);
				if (cmd.CommandParameters[1] != null)
				{
					stringBuilder.Append(':');
					stringBuilder.Append(cmd.CommandParameters[1]);
				}
				string value = " (";
				IList<string> list = (IList<string>)cmd.CommandParameters[3];
				foreach (string value2 in list)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(value2);
					value = " ";
				}
				stringBuilder.Append(')');
				stringBuilder.Append("\r\n");
				return stringBuilder.ToString();
			}, new object[]
			{
				start,
				end,
				uidFetch,
				dataItemSet
			});
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0002364C File Offset: 0x0002184C
		internal void ResetAsAppend(string newCommandId, string mailboxName, IMAPMailFlags messageFlags, Stream messageBody)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxName", mailboxName);
			SyncUtilities.ThrowIfArgumentNull("messageFlags", messageFlags);
			SyncUtilities.ThrowIfArgumentNull("messageBody", messageBody);
			this.Reset(IMAPCommandType.Append, newCommandId, delegate(IMAPCommand cmd)
			{
				string value = cmd.ConvertToQuotableString((string)cmd.CommandParameters[0]);
				StringBuilder stringBuilder = cmd.CachedStringBuilder;
				stringBuilder.Length = 0;
				stringBuilder.Append(cmd.CommandId);
				stringBuilder.Append(" APPEND \"");
				stringBuilder.Append(value);
				stringBuilder.Append("\" ");
				IMAPUtils.AppendStringBuilderIMAPFlags((IMAPMailFlags)cmd.CommandParameters[1], stringBuilder);
				stringBuilder.Append(" {");
				stringBuilder.Append((long)cmd.CommandParameters[3]);
				stringBuilder.Append("}\r\n");
				return stringBuilder.ToString();
			}, new object[]
			{
				mailboxName,
				messageFlags,
				messageBody,
				messageBody.Length
			});
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002377C File Offset: 0x0002197C
		internal void ResetAsUidStore(string newCommandId, string uid, IMAPMailFlags flags, bool addFlags)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("uid", uid);
			this.Reset(IMAPCommandType.Store, newCommandId, delegate(IMAPCommand cmd)
			{
				StringBuilder stringBuilder = cmd.cachedStringBuilder;
				stringBuilder.Length = 0;
				stringBuilder.Append(cmd.CommandId);
				stringBuilder.Append(" UID STORE ");
				stringBuilder.Append((string)cmd.CommandParameters[0]);
				if (addFlags)
				{
					stringBuilder.Append(" +FLAGS.SILENT ");
				}
				else
				{
					stringBuilder.Append(" -FLAGS.SILENT ");
				}
				IMAPUtils.AppendStringBuilderIMAPFlags((IMAPMailFlags)cmd.CommandParameters[1], stringBuilder);
				stringBuilder.Append("\r\n");
				return stringBuilder.ToString();
			}, new object[]
			{
				uid,
				flags
			});
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00023805 File Offset: 0x00021A05
		internal void ResetAsNoop(string newCommandId)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			this.Reset(IMAPCommandType.Noop, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} NOOP\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000238D4 File Offset: 0x00021AD4
		internal void ResetAsSearch(string newCommandId, params string[] queryTerms)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNull("queryTerms", queryTerms);
			this.Reset(IMAPCommandType.Search, newCommandId, delegate(IMAPCommand cmd)
			{
				StringBuilder stringBuilder = cmd.CachedStringBuilder;
				stringBuilder.Length = 0;
				stringBuilder.Append(cmd.CommandId);
				stringBuilder.Append(" UID SEARCH");
				foreach (object value in cmd.CommandParameters)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(value);
				}
				stringBuilder.Append("\r\n");
				return stringBuilder.ToString();
			}, queryTerms);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000239DC File Offset: 0x00021BDC
		internal void ResetAsList(string newCommandId, char separator, int? level, string rootFolderPath)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			this.Reset(IMAPCommandType.List, newCommandId, delegate(IMAPCommand cmd)
			{
				int? num = (int?)cmd.CommandParameters[0];
				string text = "*";
				if (num != null)
				{
					StringBuilder stringBuilder = cmd.CachedStringBuilder;
					stringBuilder.Length = 0;
					stringBuilder.Append("%");
					for (int i = 1; i < num.Value; i++)
					{
						stringBuilder.Append(separator);
						stringBuilder.Append('%');
					}
					text = stringBuilder.ToString();
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} LIST \"{1}\" {2}\r\n", new object[]
				{
					cmd.CommandId,
					string.IsNullOrEmpty(rootFolderPath) ? string.Empty : rootFolderPath,
					text
				});
			}, new object[]
			{
				level,
				separator
			});
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00023A84 File Offset: 0x00021C84
		internal void ResetAsCreate(string newCommandId, string mailboxName)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxName", mailboxName);
			this.Reset(IMAPCommandType.CreateMailbox, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} CREATE \"{1}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0])
			}), new object[]
			{
				mailboxName
			});
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00023B24 File Offset: 0x00021D24
		internal void ResetAsDelete(string newCommandId, string mailboxName)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxName", mailboxName);
			this.Reset(IMAPCommandType.DeleteMailbox, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} DELETE \"{1}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0])
			}), new object[]
			{
				mailboxName
			});
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00023BE0 File Offset: 0x00021DE0
		internal void ResetAsRename(string newCommandId, string oldMailboxName, string newMailboxName)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newCommandId", newCommandId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("oldMailboxName", oldMailboxName);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("newMailboxName", newMailboxName);
			this.Reset(IMAPCommandType.RenameMailbox, newCommandId, (IMAPCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} RENAME \"{1}\" \"{2}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0]),
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[1])
			}), new object[]
			{
				oldMailboxName,
				newMailboxName
			});
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00023C44 File Offset: 0x00021E44
		internal byte[] ToBytes()
		{
			return Encoding.ASCII.GetBytes(this.builder(this));
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00023C5C File Offset: 0x00021E5C
		internal string ToPiiCleanString()
		{
			IMAPCommandType imapcommandType = this.commandType;
			if (imapcommandType == IMAPCommandType.Login)
			{
				return this.commandId + " LOGIN ...";
			}
			if (imapcommandType != IMAPCommandType.Id)
			{
				return this.builder(this);
			}
			return this.commandId + " ID ...";
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00023CAC File Offset: 0x00021EAC
		internal Stream GetCommandParameterStream(Fqdn targetHost, string responseLine, out Exception failureException)
		{
			failureException = null;
			if (this.commandType == IMAPCommandType.Append)
			{
				return this.CommandParameters[2] as Stream;
			}
			if (this.commandType == IMAPCommandType.Authenticate)
			{
				byte[] inputBuffer = null;
				MemoryStream result = null;
				IMAPAuthenticationMechanism imapauthenticationMechanism = (IMAPAuthenticationMechanism)this.CommandParameters[0];
				string text = (string)this.CommandParameters[1];
				SecureString password = (SecureString)this.CommandParameters[2];
				AuthenticationContext authenticationContext = (AuthenticationContext)this.CommandParameters[3];
				string text2 = null;
				if (responseLine != null && responseLine.Length > 2)
				{
					inputBuffer = Encoding.ASCII.GetBytes(responseLine.Substring(2));
				}
				byte[] buffer = null;
				IMAPAuthenticationMechanism imapauthenticationMechanism2 = imapauthenticationMechanism;
				if (imapauthenticationMechanism2 != IMAPAuthenticationMechanism.Basic)
				{
					if (imapauthenticationMechanism2 == IMAPAuthenticationMechanism.Ntlm)
					{
						SecurityStatus securityStatus;
						if (authenticationContext == null)
						{
							authenticationContext = new AuthenticationContext();
							this.CommandParameters[3] = authenticationContext;
							string spn = "IMAP/" + targetHost;
							securityStatus = authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Ntlm, spn, text, password);
							if (securityStatus != SecurityStatus.OK)
							{
								failureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new IMAPException("Failure in NTLM Authentication"), true);
								return null;
							}
						}
						securityStatus = authenticationContext.NegotiateSecurityContext(inputBuffer, out buffer);
						SecurityStatus securityStatus2 = securityStatus;
						if (securityStatus2 != SecurityStatus.OK && securityStatus2 != SecurityStatus.ContinueNeeded)
						{
							failureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new IMAPException("Failure in NTLM Authentication"), true);
							return null;
						}
						result = new MemoryStream(buffer);
					}
					else
					{
						failureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.ConnectionError, new IMAPException("Unsupported Authentication Mechanism"), true);
					}
				}
				else
				{
					SecurityStatus securityStatus;
					if (authenticationContext == null)
					{
						authenticationContext = new AuthenticationContext();
						this.CommandParameters[3] = authenticationContext;
						Match match = IMAPCommand.UserNameWithAuthorizationId.Match(text);
						if (match != null && match.Success && match.Groups.Count == 3)
						{
							text2 = match.Groups[1].Value;
							text = match.Groups[2].Value;
						}
						securityStatus = authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Plain, null, text, password);
						if (securityStatus != SecurityStatus.OK)
						{
							failureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new IMAPException("Failure in PLAIN Authentication"), true);
							return null;
						}
						if (text2 != null)
						{
							authenticationContext.AuthorizationIdentity = Encoding.ASCII.GetBytes(text2);
						}
					}
					securityStatus = authenticationContext.NegotiateSecurityContext(inputBuffer, out buffer);
					SecurityStatus securityStatus3 = securityStatus;
					if (securityStatus3 != SecurityStatus.OK)
					{
						failureException = SyncTransientException.CreateOperationLevelException(DetailedAggregationStatus.AuthenticationError, new IMAPException("Failure in PLAIN Authentication"), true);
						return null;
					}
					result = new MemoryStream(buffer);
				}
				return result;
			}
			return null;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00023EF8 File Offset: 0x000220F8
		private void Reset(IMAPCommandType incomingCommandType, string incomingCommandId, IMAPCommand.CommandStringBuilder incomingCommandBuilder, params object[] incomingCommandParameters)
		{
			this.commandType = incomingCommandType;
			this.commandId = incomingCommandId;
			this.commandParameters.Clear();
			this.cachedStringBuilder.Length = 0;
			this.builder = incomingCommandBuilder;
			if (incomingCommandParameters != null)
			{
				foreach (object item in incomingCommandParameters)
				{
					this.commandParameters.Add(item);
				}
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00023F58 File Offset: 0x00022158
		private string ConvertToQuotableString(string incoming)
		{
			StringBuilder stringBuilder = this.cachedStringBuilder;
			stringBuilder.Length = 0;
			for (int i = 0; i < incoming.Length; i++)
			{
				if (incoming[i] == '\\' || incoming[i] == '"')
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(incoming[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000787 RID: 1927
		internal const int MailboxToSelect = 0;

		// Token: 0x04000788 RID: 1928
		internal const int MailboxToCreate = 0;

		// Token: 0x04000789 RID: 1929
		internal const int FetchStartIndex = 0;

		// Token: 0x0400078A RID: 1930
		internal const int FetchEndIndex = 1;

		// Token: 0x0400078B RID: 1931
		internal const int FetchByUid = 2;

		// Token: 0x0400078C RID: 1932
		internal const int FetchDataItems = 3;

		// Token: 0x0400078D RID: 1933
		internal const int StoreUidIndex = 0;

		// Token: 0x0400078E RID: 1934
		internal const string PrefixTag = "E";

		// Token: 0x0400078F RID: 1935
		private const string ImapSpnPrefix = "IMAP/";

		// Token: 0x04000790 RID: 1936
		private const int DefaultCommandLength = 128;

		// Token: 0x04000791 RID: 1937
		private const int DefaultNumberOfCommandParameters = 5;

		// Token: 0x04000792 RID: 1938
		private const int MessageBodyIndex = 2;

		// Token: 0x04000793 RID: 1939
		private const int AuthMechanismIndex = 0;

		// Token: 0x04000794 RID: 1940
		private const int UsernameIndex = 1;

		// Token: 0x04000795 RID: 1941
		private const int PasswordIndex = 2;

		// Token: 0x04000796 RID: 1942
		private const int AuthContextIndex = 3;

		// Token: 0x04000797 RID: 1943
		private static readonly Regex UserNameWithAuthorizationId = new Regex("^#(.+)#([^#\n]+)#$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000798 RID: 1944
		private IMAPCommandType commandType;

		// Token: 0x04000799 RID: 1945
		private string commandId;

		// Token: 0x0400079A RID: 1946
		private IList<object> commandParameters;

		// Token: 0x0400079B RID: 1947
		private IMAPCommand.CommandStringBuilder builder;

		// Token: 0x0400079C RID: 1948
		private StringBuilder cachedStringBuilder;

		// Token: 0x020001D2 RID: 466
		// (Invoke) Token: 0x06000E0E RID: 3598
		private delegate string CommandStringBuilder(IMAPCommand command);
	}
}
