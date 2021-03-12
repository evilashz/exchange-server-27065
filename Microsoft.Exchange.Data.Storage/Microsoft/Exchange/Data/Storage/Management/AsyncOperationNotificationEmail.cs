using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B9 RID: 2489
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AsyncOperationNotificationEmail
	{
		// Token: 0x06005BE8 RID: 23528 RVA: 0x0017F950 File Offset: 0x0017DB50
		static AsyncOperationNotificationEmail()
		{
			AsyncOperationNotificationEmail.bodyTable = new Dictionary<AsyncOperationType, Dictionary<AsyncOperationNotificationEmailType, string>>
			{
				{
					AsyncOperationType.CertExpiry,
					new Dictionary<AsyncOperationNotificationEmailType, string>
					{
						{
							AsyncOperationNotificationEmailType.CertExpiring,
							ServerStrings.NotificationEmailBodyCertExpiring
						},
						{
							AsyncOperationNotificationEmailType.CertExpired,
							ServerStrings.NotificationEmailBodyCertExpired
						}
					}
				},
				{
					AsyncOperationType.ExportPST,
					new Dictionary<AsyncOperationNotificationEmailType, string>
					{
						{
							AsyncOperationNotificationEmailType.Created,
							ServerStrings.NotificationEmailBodyExportPSTCreated
						},
						{
							AsyncOperationNotificationEmailType.Completed,
							ServerStrings.NotificationEmailBodyExportPSTCompleted
						},
						{
							AsyncOperationNotificationEmailType.Failed,
							ServerStrings.NotificationEmailBodyExportPSTFailed
						}
					}
				},
				{
					AsyncOperationType.ImportPST,
					new Dictionary<AsyncOperationNotificationEmailType, string>
					{
						{
							AsyncOperationNotificationEmailType.Created,
							ServerStrings.NotificationEmailBodyImportPSTCreated
						},
						{
							AsyncOperationNotificationEmailType.Completed,
							ServerStrings.NotificationEmailBodyImportPSTCompleted
						},
						{
							AsyncOperationNotificationEmailType.Failed,
							ServerStrings.NotificationEmailBodyImportPSTFailed
						}
					}
				}
			};
			Dictionary<string, Func<AsyncOperationNotificationEmail, string, string>> dictionary = new Dictionary<string, Func<AsyncOperationNotificationEmail, string, string>>();
			dictionary.Add("ExpireDate", (AsyncOperationNotificationEmail email, string key) => ExDateTime.FromFileTimeUtc(long.Parse(email.notification.GetExtendedAttributeValue(key))).ToShortDateString());
			dictionary.Add("StartedBy", delegate(AsyncOperationNotificationEmail email, string key)
			{
				if (email.notification.StartedBy != null)
				{
					return email.notification.StartedBy.ToString();
				}
				return string.Empty;
			});
			dictionary.Add("StartTime", (AsyncOperationNotificationEmail email, string key) => email.notification.StartTime.ToString());
			dictionary.Add("RunTime", delegate(AsyncOperationNotificationEmail email, string key)
			{
				if (email.notification.LastModified == null || email.notification.StartTime == null)
				{
					return string.Empty;
				}
				return email.notification.LastModified.Value.Subtract(email.notification.StartTime.Value).ToString();
			});
			dictionary.Add("DisplayName", (AsyncOperationNotificationEmail email, string key) => email.notification.DisplayName.ToString());
			dictionary.Add("EcpUrl", (AsyncOperationNotificationEmail email, string key) => email.GetEcpUrl());
			AsyncOperationNotificationEmail.bodyVariableGetters = dictionary;
			AsyncOperationNotificationEmail.bodyFormatRegex = new Regex("\\$_\\.(?<key>\\w+)", RegexOptions.Multiline | RegexOptions.Compiled);
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x0017FB38 File Offset: 0x0017DD38
		public AsyncOperationNotificationEmail(AsyncOperationNotificationDataProvider provider, AsyncOperationNotification notification, bool forceSendCreatedMail)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			this.provider = provider;
			this.notification = notification;
			if (forceSendCreatedMail)
			{
				this.emailType = AsyncOperationNotificationEmailType.Created;
			}
			else
			{
				switch (notification.Status)
				{
				case AsyncOperationStatus.Completed:
					this.emailType = AsyncOperationNotificationEmailType.Completed;
					break;
				case AsyncOperationStatus.Failed:
					this.emailType = AsyncOperationNotificationEmailType.Failed;
					break;
				case AsyncOperationStatus.CertExpiring:
					this.emailType = AsyncOperationNotificationEmailType.CertExpiring;
					break;
				case AsyncOperationStatus.CertExpired:
					this.emailType = AsyncOperationNotificationEmailType.CertExpired;
					break;
				default:
					this.emailType = AsyncOperationNotificationEmailType.Created;
					break;
				}
			}
			this.emailMessage = new EmailMessage(this.provider.Service);
			this.emailMessage.Subject = this.GetSubject();
			this.emailMessage.Body = this.GetBody();
			this.AppendRecipients(notification.NotificationEmails);
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06005BEA RID: 23530 RVA: 0x0017FC11 File Offset: 0x0017DE11
		public string Subject
		{
			get
			{
				return this.emailMessage.Subject;
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06005BEB RID: 23531 RVA: 0x0017FC1E File Offset: 0x0017DE1E
		public MessageBody Body
		{
			get
			{
				return this.emailMessage.Body;
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06005BEC RID: 23532 RVA: 0x0017FC2B File Offset: 0x0017DE2B
		public EmailAddressCollection ToRecipients
		{
			get
			{
				return this.emailMessage.ToRecipients;
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06005BED RID: 23533 RVA: 0x0017FC38 File Offset: 0x0017DE38
		public AttachmentCollection Attachments
		{
			get
			{
				return this.emailMessage.Attachments;
			}
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x0017FC48 File Offset: 0x0017DE48
		public void AppendRecipients(IEnumerable<ADRecipientOrAddress> recipients)
		{
			if (recipients != null)
			{
				foreach (ADRecipientOrAddress adrecipientOrAddress in recipients)
				{
					EmailAddress emailAddress = new EmailAddress(adrecipientOrAddress.DisplayName, adrecipientOrAddress.Address, adrecipientOrAddress.RoutingType);
					if (!this.emailMessage.ToRecipients.Contains(emailAddress))
					{
						this.emailMessage.ToRecipients.Add(emailAddress);
					}
				}
			}
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x0017FCC8 File Offset: 0x0017DEC8
		public bool Send()
		{
			if (this.ToRecipients.Count == 0)
			{
				throw new InvalidOperationException("There is no Notification Email in the given notification object");
			}
			bool result = false;
			try
			{
				this.emailMessage.Send();
				result = true;
			}
			catch (ServiceRequestException)
			{
			}
			catch (ServiceResponseException)
			{
			}
			catch (WebException)
			{
			}
			return result;
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x0017FD30 File Offset: 0x0017DF30
		private string GetSubject()
		{
			string text = string.Empty;
			switch (this.notification.Type)
			{
			case AsyncOperationType.ImportPST:
				text = ServerStrings.NotificationEmailSubjectImportPst;
				break;
			case AsyncOperationType.ExportPST:
				text = ServerStrings.NotificationEmailSubjectExportPst;
				break;
			case AsyncOperationType.Migration:
				text = ServerStrings.NotificationEmailSubjectMoveMailbox;
				break;
			}
			switch (this.emailType)
			{
			case AsyncOperationNotificationEmailType.Created:
				text = ServerStrings.NotificationEmailSubjectCreated(text);
				break;
			case AsyncOperationNotificationEmailType.Completed:
				text = ServerStrings.NotificationEmailSubjectCompleted(text);
				break;
			case AsyncOperationNotificationEmailType.Failed:
				text = ServerStrings.NotificationEmailSubjectFailed(text);
				break;
			case AsyncOperationNotificationEmailType.CertExpiring:
				text = ServerStrings.NotificationEmailSubjectCertExpiring;
				break;
			case AsyncOperationNotificationEmailType.CertExpired:
				text = ServerStrings.NotificationEmailSubjectCertExpired;
				break;
			}
			return text;
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x0017FDF4 File Offset: 0x0017DFF4
		private MessageBody GetBody()
		{
			string text = string.Empty;
			Dictionary<AsyncOperationNotificationEmailType, string> dictionary;
			if (AsyncOperationNotificationEmail.bodyTable.TryGetValue(this.notification.Type, out dictionary) && dictionary.TryGetValue(this.emailType, out text))
			{
				text = AsyncOperationNotificationEmail.bodyFormatRegex.Replace(text, new MatchEvaluator(this.GetBodyVariable));
			}
			return "<html>\r\n            <head>\r\n            <style>\r\n            body\r\n            {\r\n                font-family: Tahoma;\r\n                background-color: #FFFFFF;\r\n                color: #000000; font-size:x-small;\r\n                width: 600px;\r\n            }\r\n            p\r\n            {\r\n                margin-left:0px ;\r\n                margin-bottom:8px\r\n            }\r\n            table\r\n            {\r\n                font-family: Tahoma;\r\n                background-color: #FFFFFF;\r\n                color: #000000;\r\n                font-size:x-small;\r\n                border:0px ;\r\n                text-align:left;\r\n                margin-left:20px\r\n            }\r\n            </style>\r\n            </head>\r\n            <body>\r\n            " + text + "\r\n            </body>\r\n            </html>";
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x0017FE60 File Offset: 0x0017E060
		private string GetBodyVariable(Match match)
		{
			string value = match.Groups["key"].Value;
			Func<AsyncOperationNotificationEmail, string, string> func;
			string result;
			LocalizedString value2;
			if (AsyncOperationNotificationEmail.bodyVariableGetters.TryGetValue(value, out func))
			{
				result = func(this, value);
			}
			else if (this.notification.TryGetExtendedAttributeValue(value, out value2))
			{
				result = value2;
			}
			else
			{
				result = match.Value;
			}
			return result;
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0017FEC0 File Offset: 0x0017E0C0
		private string GetEcpUrl()
		{
			if (AsyncOperationNotificationEmail.ecpUrl == null)
			{
				Uri uri = null;
				IExchangePrincipal mailbox = this.provider.Mailbox;
				try
				{
					if (AsyncOperationNotificationEmail.discoveryEcpExternalUrl == null)
					{
						AsyncOperationNotificationEmail.discoveryEcpExternalUrl = (Func<IExchangePrincipal, Uri>)Delegate.CreateDelegate(typeof(Func<IExchangePrincipal, Uri>), Type.GetType("Microsoft.Exchange.Data.ApplicationLogic.Cafe.FrontEndLocator, Microsoft.Exchange.Data.ApplicationLogic").GetMethod("GetFrontEndEcpUrl", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(IExchangePrincipal)
						}, null));
					}
					uri = AsyncOperationNotificationEmail.discoveryEcpExternalUrl(mailbox);
				}
				catch (Exception)
				{
				}
				if (uri != null && uri.IsAbsoluteUri)
				{
					AsyncOperationNotificationEmail.ecpUrl = uri.AbsoluteUri;
				}
			}
			return AsyncOperationNotificationEmail.ecpUrl ?? string.Empty;
		}

		// Token: 0x04003291 RID: 12945
		public const string StartedByKey = "StartedBy";

		// Token: 0x04003292 RID: 12946
		public const string StartTimeKey = "StartTime";

		// Token: 0x04003293 RID: 12947
		public const string RunTimeKey = "RunTime";

		// Token: 0x04003294 RID: 12948
		public const string DisplayNameKey = "DisplayName";

		// Token: 0x04003295 RID: 12949
		public const string EcpUrlKey = "EcpUrl";

		// Token: 0x04003296 RID: 12950
		public const string ExpireDateKey = "ExpireDate";

		// Token: 0x04003297 RID: 12951
		private const string BodyBeginPart = "<html>\r\n            <head>\r\n            <style>\r\n            body\r\n            {\r\n                font-family: Tahoma;\r\n                background-color: #FFFFFF;\r\n                color: #000000; font-size:x-small;\r\n                width: 600px;\r\n            }\r\n            p\r\n            {\r\n                margin-left:0px ;\r\n                margin-bottom:8px\r\n            }\r\n            table\r\n            {\r\n                font-family: Tahoma;\r\n                background-color: #FFFFFF;\r\n                color: #000000;\r\n                font-size:x-small;\r\n                border:0px ;\r\n                text-align:left;\r\n                margin-left:20px\r\n            }\r\n            </style>\r\n            </head>\r\n            <body>\r\n            ";

		// Token: 0x04003298 RID: 12952
		private const string BodyEndPart = "\r\n            </body>\r\n            </html>";

		// Token: 0x04003299 RID: 12953
		private static Func<IExchangePrincipal, Uri> discoveryEcpExternalUrl;

		// Token: 0x0400329A RID: 12954
		private static readonly Dictionary<AsyncOperationType, Dictionary<AsyncOperationNotificationEmailType, string>> bodyTable;

		// Token: 0x0400329B RID: 12955
		private static readonly Dictionary<string, Func<AsyncOperationNotificationEmail, string, string>> bodyVariableGetters;

		// Token: 0x0400329C RID: 12956
		private static readonly Regex bodyFormatRegex;

		// Token: 0x0400329D RID: 12957
		private static string ecpUrl = null;

		// Token: 0x0400329E RID: 12958
		private AsyncOperationNotificationDataProvider provider;

		// Token: 0x0400329F RID: 12959
		private AsyncOperationNotification notification;

		// Token: 0x040032A0 RID: 12960
		private EmailMessage emailMessage;

		// Token: 0x040032A1 RID: 12961
		private AsyncOperationNotificationEmailType emailType;
	}
}
