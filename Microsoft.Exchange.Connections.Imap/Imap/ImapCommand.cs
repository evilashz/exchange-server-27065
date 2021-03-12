using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ImapCommand
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal ImapCommand()
		{
			this.CachedStringBuilder = new StringBuilder(128);
			this.CommandParameters = new List<object>(5);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FC File Offset: 0x000002FC
		internal string CommandId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000210D File Offset: 0x0000030D
		internal ImapCommandType CommandType { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002116 File Offset: 0x00000316
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000211E File Offset: 0x0000031E
		internal IList<object> CommandParameters { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002127 File Offset: 0x00000327
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000212F File Offset: 0x0000032F
		private ImapCommand.CommandStringBuilder Builder { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002140 File Offset: 0x00000340
		private StringBuilder CachedStringBuilder { get; set; }

		// Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		internal void ResetAsLogin(string newCommandId, string user, SecureString password)
		{
			this.Reset(ImapCommandType.Login, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} LOGIN \"{1}\" \"{2}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0]),
				cmd.ConvertToQuotableString(((SecureString)cmd.CommandParameters[1]).AsUnsecureString())
			}), new object[]
			{
				user,
				password
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002240 File Offset: 0x00000440
		internal void ResetAsId(string newCommandId, SecureString clientToken)
		{
			this.Reset(ImapCommandType.Id, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} ID {1}\r\n", new object[]
			{
				cmd.CommandId,
				((SecureString)cmd.CommandParameters[0]).AsUnsecureString()
			}), new object[]
			{
				clientToken
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022AD File Offset: 0x000004AD
		internal void ResetAsLogout(string newCommandId)
		{
			this.Reset(ImapCommandType.Logout, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} LOGOUT\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002309 File Offset: 0x00000509
		internal void ResetAsStarttls(string newCommandId)
		{
			this.Reset(ImapCommandType.Starttls, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} STARTTLS\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C8 File Offset: 0x000005C8
		internal void ResetAsAuthenticate(string newCommandId, ImapAuthenticationMechanism authMechanism, string user, SecureString password, AuthenticationContext authContext)
		{
			this.Reset(ImapCommandType.Authenticate, newCommandId, delegate(ImapCommand cmd)
			{
				StringBuilder cachedStringBuilder = cmd.CachedStringBuilder;
				cachedStringBuilder.Length = 0;
				cachedStringBuilder.Append(cmd.CommandId);
				cachedStringBuilder.Append(" AUTHENTICATE");
				ImapAuthenticationMechanism authMechanism2 = authMechanism;
				if (authMechanism2 != ImapAuthenticationMechanism.Basic)
				{
					if (authMechanism2 != ImapAuthenticationMechanism.Ntlm)
					{
						string message = "Unexpected authentication mechanism " + authMechanism;
						throw new InvalidOperationException(message);
					}
					cachedStringBuilder.Append(" NTLM\r\n");
				}
				else
				{
					cachedStringBuilder.Append(" PLAIN\r\n");
				}
				return cachedStringBuilder.ToString();
			}, new object[]
			{
				authMechanism,
				user,
				password,
				authContext
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002449 File Offset: 0x00000649
		internal void ResetAsCapability(string newCommandId)
		{
			this.Reset(ImapCommandType.Capability, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} CAPABILITY\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024A5 File Offset: 0x000006A5
		internal void ResetAsExpunge(string newCommandId)
		{
			this.Reset(ImapCommandType.Expunge, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} EXPUNGE\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002524 File Offset: 0x00000724
		internal void ResetAsSelect(string newCommandId, ImapMailbox imapMailbox)
		{
			this.Reset(ImapCommandType.Select, newCommandId, delegate(ImapCommand cmd)
			{
				string text = cmd.ConvertToQuotableString(((ImapMailbox)cmd.CommandParameters[0]).NameOnTheWire);
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

		// Token: 0x06000014 RID: 20 RVA: 0x000025B4 File Offset: 0x000007B4
		internal void ResetAsStatus(string newCommandId, ImapMailbox imapMailbox)
		{
			this.Reset(ImapCommandType.Status, newCommandId, delegate(ImapCommand cmd)
			{
				string text = cmd.ConvertToQuotableString(((ImapMailbox)cmd.CommandParameters[0]).NameOnTheWire);
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

		// Token: 0x06000015 RID: 21 RVA: 0x0000270C File Offset: 0x0000090C
		internal void ResetAsFetch(string newCommandId, string start, string end, bool uidFetch, IList<string> dataItemSet)
		{
			if (dataItemSet.Count == 0)
			{
				throw new ArgumentException("dataItemSet cannot be empty");
			}
			this.Reset(ImapCommandType.Fetch, newCommandId, delegate(ImapCommand cmd)
			{
				bool flag = (bool)cmd.CommandParameters[2];
				StringBuilder cachedStringBuilder = cmd.CachedStringBuilder;
				cachedStringBuilder.Length = 0;
				cachedStringBuilder.Append(cmd.CommandId);
				if (flag)
				{
					cachedStringBuilder.Append(" UID");
				}
				cachedStringBuilder.Append(" FETCH ");
				cachedStringBuilder.Append(cmd.CommandParameters[0]);
				if (cmd.CommandParameters[1] != null)
				{
					cachedStringBuilder.Append(':');
					cachedStringBuilder.Append(cmd.CommandParameters[1]);
				}
				string value = " (";
				IList<string> list = (IList<string>)cmd.CommandParameters[3];
				foreach (string value2 in list)
				{
					cachedStringBuilder.Append(value);
					cachedStringBuilder.Append(value2);
					value = " ";
				}
				cachedStringBuilder.Append(')');
				cachedStringBuilder.Append("\r\n");
				return cachedStringBuilder.ToString();
			}, new object[]
			{
				start,
				end,
				uidFetch,
				dataItemSet
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002824 File Offset: 0x00000A24
		internal void ResetAsAppend(string newCommandId, string mailboxName, ImapMailFlags messageFlags, Stream messageBody)
		{
			this.Reset(ImapCommandType.Append, newCommandId, delegate(ImapCommand cmd)
			{
				string value = cmd.ConvertToQuotableString((string)cmd.CommandParameters[0]);
				StringBuilder cachedStringBuilder = cmd.CachedStringBuilder;
				cachedStringBuilder.Length = 0;
				cachedStringBuilder.Append(cmd.CommandId);
				cachedStringBuilder.Append(" APPEND \"");
				cachedStringBuilder.Append(value);
				cachedStringBuilder.Append("\" ");
				ImapUtilities.AppendStringBuilderImapFlags((ImapMailFlags)cmd.CommandParameters[1], cachedStringBuilder);
				cachedStringBuilder.Append(" {");
				cachedStringBuilder.Append((long)cmd.CommandParameters[3]);
				cachedStringBuilder.Append("}\r\n");
				return cachedStringBuilder.ToString();
			}, new object[]
			{
				mailboxName,
				messageFlags,
				messageBody,
				messageBody.Length
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002920 File Offset: 0x00000B20
		internal void ResetAsUidStore(string newCommandId, string uid, ImapMailFlags flags, bool addFlags)
		{
			this.Reset(ImapCommandType.Store, newCommandId, delegate(ImapCommand cmd)
			{
				StringBuilder cachedStringBuilder = cmd.CachedStringBuilder;
				cachedStringBuilder.Length = 0;
				cachedStringBuilder.Append(cmd.CommandId);
				cachedStringBuilder.Append(" UID STORE ");
				cachedStringBuilder.Append((string)cmd.CommandParameters[0]);
				if (addFlags)
				{
					cachedStringBuilder.Append(" +FLAGS.SILENT ");
				}
				else
				{
					cachedStringBuilder.Append(" -FLAGS.SILENT ");
				}
				ImapUtilities.AppendStringBuilderImapFlags((ImapMailFlags)cmd.CommandParameters[1], cachedStringBuilder);
				cachedStringBuilder.Append("\r\n");
				return cachedStringBuilder.ToString();
			}, new object[]
			{
				uid,
				flags
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002995 File Offset: 0x00000B95
		internal void ResetAsNoop(string newCommandId)
		{
			this.Reset(ImapCommandType.Noop, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} NOOP\r\n", new object[]
			{
				cmd.CommandId
			}), new object[0]);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A58 File Offset: 0x00000C58
		internal void ResetAsSearch(string newCommandId, params string[] queryTerms)
		{
			this.Reset(ImapCommandType.Search, newCommandId, delegate(ImapCommand cmd)
			{
				StringBuilder cachedStringBuilder = cmd.CachedStringBuilder;
				cachedStringBuilder.Length = 0;
				cachedStringBuilder.Append(cmd.CommandId);
				cachedStringBuilder.Append(" UID SEARCH");
				foreach (object value in cmd.CommandParameters)
				{
					cachedStringBuilder.Append(" ");
					cachedStringBuilder.Append(value);
				}
				cachedStringBuilder.Append("\r\n");
				return cachedStringBuilder.ToString();
			}, queryTerms);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B48 File Offset: 0x00000D48
		internal void ResetAsList(string newCommandId, char separator, int? level, string rootFolderPath)
		{
			this.Reset(ImapCommandType.List, newCommandId, delegate(ImapCommand cmd)
			{
				int? num = (int?)cmd.CommandParameters[0];
				string text = "*";
				if (num != null)
				{
					StringBuilder cachedStringBuilder = cmd.CachedStringBuilder;
					cachedStringBuilder.Length = 0;
					cachedStringBuilder.Append("%");
					for (int i = 1; i < num.Value; i++)
					{
						cachedStringBuilder.Append(separator);
						cachedStringBuilder.Append('%');
					}
					text = cachedStringBuilder.ToString();
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002BE8 File Offset: 0x00000DE8
		internal void ResetAsCreate(string newCommandId, string mailboxName)
		{
			this.Reset(ImapCommandType.CreateMailbox, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} CREATE \"{1}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0])
			}), new object[]
			{
				mailboxName
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002C70 File Offset: 0x00000E70
		internal void ResetAsDelete(string newCommandId, string mailboxName)
		{
			this.Reset(ImapCommandType.DeleteMailbox, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} DELETE \"{1}\"\r\n", new object[]
			{
				cmd.CommandId,
				cmd.ConvertToQuotableString((string)cmd.CommandParameters[0])
			}), new object[]
			{
				mailboxName
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002D14 File Offset: 0x00000F14
		internal void ResetAsRename(string newCommandId, string oldMailboxName, string newMailboxName)
		{
			this.Reset(ImapCommandType.RenameMailbox, newCommandId, (ImapCommand cmd) => string.Format(CultureInfo.InvariantCulture, "{0} RENAME \"{1}\" \"{2}\"\r\n", new object[]
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002D57 File Offset: 0x00000F57
		internal byte[] ToBytes()
		{
			return Encoding.ASCII.GetBytes(this.Builder(this));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002D70 File Offset: 0x00000F70
		internal string ToPiiCleanString()
		{
			ImapCommandType commandType = this.CommandType;
			if (commandType == ImapCommandType.Login)
			{
				return this.CommandId + " LOGIN ...";
			}
			if (commandType != ImapCommandType.Id)
			{
				return this.Builder(this);
			}
			return this.CommandId + " ID ...";
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002DC0 File Offset: 0x00000FC0
		internal Stream GetCommandParameterStream(string targetHost, string responseLine, out Exception failureException)
		{
			failureException = null;
			if (this.CommandType == ImapCommandType.Append)
			{
				return this.CommandParameters[2] as Stream;
			}
			if (this.CommandType == ImapCommandType.Authenticate)
			{
				byte[] inputBuffer = null;
				MemoryStream result = null;
				ImapAuthenticationMechanism imapAuthenticationMechanism = (ImapAuthenticationMechanism)this.CommandParameters[0];
				string text = (string)this.CommandParameters[1];
				SecureString password = (SecureString)this.CommandParameters[2];
				AuthenticationContext authenticationContext = (AuthenticationContext)this.CommandParameters[3];
				string text2 = null;
				if (responseLine != null && responseLine.Length > 2)
				{
					inputBuffer = Encoding.ASCII.GetBytes(responseLine.Substring(2));
				}
				byte[] buffer = null;
				ImapAuthenticationMechanism imapAuthenticationMechanism2 = imapAuthenticationMechanism;
				if (imapAuthenticationMechanism2 != ImapAuthenticationMechanism.Basic)
				{
					if (imapAuthenticationMechanism2 == ImapAuthenticationMechanism.Ntlm)
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
								failureException = new ImapAuthenticationException(targetHost, imapAuthenticationMechanism.ToString(), RetryPolicy.Backoff);
								return null;
							}
						}
						securityStatus = authenticationContext.NegotiateSecurityContext(inputBuffer, out buffer);
						SecurityStatus securityStatus2 = securityStatus;
						if (securityStatus2 != SecurityStatus.OK && securityStatus2 != SecurityStatus.ContinueNeeded)
						{
							failureException = new ImapAuthenticationException(targetHost, imapAuthenticationMechanism.ToString(), RetryPolicy.Backoff);
							return null;
						}
						result = new MemoryStream(buffer);
					}
					else
					{
						failureException = new ImapUnsupportedAuthenticationException(targetHost, imapAuthenticationMechanism.ToString(), RetryPolicy.Backoff);
					}
				}
				else
				{
					SecurityStatus securityStatus;
					if (authenticationContext == null)
					{
						authenticationContext = new AuthenticationContext();
						this.CommandParameters[3] = authenticationContext;
						Match match = ImapCommand.UserNameWithAuthorizationId.Match(text);
						if (match != null && match.Success && match.Groups.Count == 3)
						{
							text2 = match.Groups[1].Value;
							text = match.Groups[2].Value;
						}
						securityStatus = authenticationContext.InitializeForOutboundNegotiate(AuthenticationMechanism.Plain, null, text, password);
						if (securityStatus != SecurityStatus.OK)
						{
							failureException = new ImapAuthenticationException(targetHost, imapAuthenticationMechanism.ToString(), RetryPolicy.Backoff);
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
						failureException = new ImapAuthenticationException(targetHost, imapAuthenticationMechanism.ToString(), RetryPolicy.Backoff);
						return null;
					}
					result = new MemoryStream(buffer);
				}
				return result;
			}
			return null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003014 File Offset: 0x00001214
		private void Reset(ImapCommandType incomingCommandType, string incomingCommandId, ImapCommand.CommandStringBuilder incomingCommandBuilder, params object[] incomingCommandParameters)
		{
			this.CommandType = incomingCommandType;
			this.CommandId = incomingCommandId;
			this.CommandParameters.Clear();
			this.CachedStringBuilder.Length = 0;
			this.Builder = incomingCommandBuilder;
			if (incomingCommandParameters != null)
			{
				foreach (object item in incomingCommandParameters)
				{
					this.CommandParameters.Add(item);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003074 File Offset: 0x00001274
		private string ConvertToQuotableString(string incoming)
		{
			StringBuilder cachedStringBuilder = this.CachedStringBuilder;
			cachedStringBuilder.Length = 0;
			foreach (char c in incoming)
			{
				if (c == '\\' || c == '"')
				{
					cachedStringBuilder.Append('\\');
				}
				cachedStringBuilder.Append(c);
			}
			return cachedStringBuilder.ToString();
		}

		// Token: 0x04000004 RID: 4
		public const int MailboxToSelect = 0;

		// Token: 0x04000005 RID: 5
		public const int MailboxToCreate = 0;

		// Token: 0x04000006 RID: 6
		public const int FetchStartIndex = 0;

		// Token: 0x04000007 RID: 7
		public const int FetchEndIndex = 1;

		// Token: 0x04000008 RID: 8
		public const int FetchByUid = 2;

		// Token: 0x04000009 RID: 9
		public const int FetchDataItems = 3;

		// Token: 0x0400000A RID: 10
		public const int StoreUidIndex = 0;

		// Token: 0x0400000B RID: 11
		public const string PrefixTag = "E";

		// Token: 0x0400000C RID: 12
		private const string ImapSpnPrefix = "IMAP/";

		// Token: 0x0400000D RID: 13
		private const int DefaultCommandLength = 128;

		// Token: 0x0400000E RID: 14
		private const int DefaultNumberOfCommandParameters = 5;

		// Token: 0x0400000F RID: 15
		private const int MessageBodyIndex = 2;

		// Token: 0x04000010 RID: 16
		private const int AuthMechanismIndex = 0;

		// Token: 0x04000011 RID: 17
		private const int UsernameIndex = 1;

		// Token: 0x04000012 RID: 18
		private const int PasswordIndex = 2;

		// Token: 0x04000013 RID: 19
		private const int AuthContextIndex = 3;

		// Token: 0x04000014 RID: 20
		private static readonly Regex UserNameWithAuthorizationId = new Regex("^#(.+)#([^#\n]+)#$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x02000004 RID: 4
		// (Invoke) Token: 0x06000034 RID: 52
		private delegate string CommandStringBuilder(ImapCommand command);
	}
}
