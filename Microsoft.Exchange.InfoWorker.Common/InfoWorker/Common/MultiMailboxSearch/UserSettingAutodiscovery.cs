using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200021B RID: 539
	internal class UserSettingAutodiscovery : DisposeTrackableBase, IAutodiscoveryClient
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00040AE4 File Offset: 0x0003ECE4
		public UserSettingAutodiscovery(List<MailboxInfo> mailboxes, Uri autodiscoverEndpoint, ICredentials credentials, CallerInfo callerInfo)
		{
			this.mailboxes = mailboxes;
			this.autodiscoveryEndpoint = autodiscoverEndpoint;
			this.client = new DefaultBinding_Autodiscover(base.GetType().FullName, new RemoteCertificateValidationCallback(CertificateValidation.CertificateErrorHandler));
			this.client.Url = autodiscoverEndpoint.ToString();
			this.client.UserAgent = base.GetType().ToString();
			this.client.RequestedServerVersionValue = UserSettingAutodiscovery.Exchange2013RequestedServerVersion;
			this.client.PreAuthenticate = true;
			this.client.Credentials = credentials;
			this.callerInfo = callerInfo;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00040B80 File Offset: 0x0003ED80
		public IAsyncResult BeginAutodiscover(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.asyncResult = new AsyncResult(callback, state);
			User[] array = new User[this.mailboxes.Count];
			for (int i = 0; i < this.mailboxes.Count; i++)
			{
				array[i] = UserSettingAutodiscovery.CreateUserFromMailboxInfo(this.mailboxes[i]);
			}
			this.request = new GetUserSettingsRequest
			{
				Users = array,
				RequestedSettings = UserSettingAutodiscovery.RequestedSettings,
				RequestedVersion = new ExchangeVersion?(ExchangeVersion.Exchange2013)
			};
			this.client.BeginGetUserSettings(this.request, new AsyncCallback(this.UsersettingsDiscoveryCompleted), null);
			return this.asyncResult;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00040C2C File Offset: 0x0003EE2C
		public Dictionary<GroupId, List<MailboxInfo>> EndAutodiscover(IAsyncResult asyncResult)
		{
			this.redirects = 0;
			this.asyncResult.AsyncWaitHandle.WaitOne();
			return this.groups;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00040C4C File Offset: 0x0003EE4C
		public void CancelAutodiscover()
		{
			lock (this)
			{
				if (this.groups != null && !this.cancelled)
				{
					this.cancelled = true;
					this.groups = new Dictionary<GroupId, List<MailboxInfo>>(1);
					this.groups.Add(new GroupId(new MultiMailboxSearchException(Strings.AutodiscoverTimedOut)), this.mailboxes);
					this.ReportCompletion();
				}
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00040CCC File Offset: 0x0003EECC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.asyncResult != null)
				{
					this.asyncResult.Dispose();
					this.asyncResult = null;
				}
				if (this.client != null)
				{
					this.client.Dispose();
					this.client = null;
				}
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00040D05 File Offset: 0x0003EF05
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UserSettingAutodiscovery>(this);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00040D10 File Offset: 0x0003EF10
		private void UsersettingsDiscoveryCompleted(IAsyncResult result)
		{
			lock (this)
			{
				if (!this.cancelled)
				{
					Exception ex = null;
					bool flag2 = false;
					try
					{
						GetUserSettingsResponse response = this.client.EndGetUserSettings(result);
						this.groups = this.CreateGroupIdFromAutoDiscoverResponse(response);
						flag2 = true;
					}
					catch (LocalizedException ex2)
					{
						ex = ex2;
					}
					catch (IOException ex3)
					{
						ex = ex3;
					}
					catch (WebException ex4)
					{
						if (!this.ShouldHandleRedirect(ex4.Response as HttpWebResponse))
						{
							ex = ex4;
						}
					}
					catch (SoapException ex5)
					{
						ex = ex5;
					}
					catch (InvalidOperationException ex6)
					{
						ex = ex6;
					}
					if (ex != null)
					{
						Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryAutodiscoverError, null, new object[]
						{
							this.autodiscoveryEndpoint.ToString(),
							ex.ToString(),
							this.callerInfo.QueryCorrelationId.ToString()
						});
						this.CreateErrorGroup(ex);
						flag2 = true;
					}
					if (flag2)
					{
						this.ReportCompletion();
					}
					else
					{
						this.client.BeginGetUserSettings(this.request, new AsyncCallback(this.UsersettingsDiscoveryCompleted), null);
					}
				}
			}
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00040E78 File Offset: 0x0003F078
		private void CreateErrorGroup(Exception exception)
		{
			Factory.Current.AutodiscoverTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Request '{1}' failed with exception {2}.", this.callerInfo.QueryCorrelationId, this.autodiscoveryEndpoint.ToString(), exception.ToString());
			this.groups = new Dictionary<GroupId, List<MailboxInfo>>();
			this.groups.Add(new GroupId(exception), this.mailboxes);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00040EDE File Offset: 0x0003F0DE
		private void ReportCompletion()
		{
			this.asyncResult.ReportCompletion();
			if (this.callback != null)
			{
				this.callback(this.asyncResult);
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00040F04 File Offset: 0x0003F104
		private bool ShouldHandleRedirect(HttpWebResponse webResponse)
		{
			if (webResponse.StatusCode != HttpStatusCode.Found && webResponse.StatusCode != HttpStatusCode.MovedPermanently)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, HttpStatusCode>((long)this.GetHashCode(), "Correlation Id:{0}. The StatusCode in WebException is not an redirect: {1}", this.callerInfo.QueryCorrelationId, webResponse.StatusCode);
				return false;
			}
			this.redirects++;
			if (this.redirects > 5)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, int>((long)this.GetHashCode(), "Correlation Id:{0}. Stopped following redirects because it exceeded maximum {1}", this.callerInfo.QueryCorrelationId, 5);
				return false;
			}
			string text = webResponse.Headers[HttpResponseHeader.Location];
			if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Not a valid redirect URL: {1}", this.callerInfo.QueryCorrelationId, text);
				return false;
			}
			Uri uri = new Uri(text, UriKind.Absolute);
			if (uri.Scheme != Uri.UriSchemeHttps)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Not a secure redirect URL: {1}", this.callerInfo.QueryCorrelationId, text);
				return false;
			}
			UriBuilder uriBuilder = new UriBuilder
			{
				Scheme = Uri.UriSchemeHttps,
				Host = uri.Host,
				Path = this.autodiscoveryEndpoint.PathAndQuery
			};
			this.autodiscoveryEndpoint = uriBuilder.Uri;
			this.client.Url = this.autodiscoveryEndpoint.ToString();
			return true;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00041070 File Offset: 0x0003F270
		private static User CreateUserFromMailboxInfo(MailboxInfo mailbox)
		{
			SmtpAddress primarySmtpAddress;
			if (mailbox.IsArchive)
			{
				string text = (mailbox.ArchiveDomain != null) ? mailbox.ArchiveDomain.Domain : null;
				if (text == null)
				{
					text = mailbox.PrimarySmtpAddress.Domain;
				}
				primarySmtpAddress = new SmtpAddress(SmtpProxyAddress.EncapsulateExchangeGuid(text, mailbox.ArchiveGuid));
			}
			else if (mailbox.ExternalEmailAddress != null)
			{
				primarySmtpAddress = new SmtpAddress(mailbox.ExternalEmailAddress.AddressString);
			}
			else
			{
				primarySmtpAddress = mailbox.PrimarySmtpAddress;
			}
			return new User
			{
				Mailbox = primarySmtpAddress.ToString()
			};
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00041108 File Offset: 0x0003F308
		private int ParseVersionString(string serverVersion)
		{
			if (serverVersion == null)
			{
				return Server.E15MinVersion;
			}
			string[] array = serverVersion.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				return Server.E15MinVersion;
			}
			int result;
			try
			{
				int major = int.Parse(array[0]);
				int minor = int.Parse(array[1]);
				int build = int.Parse(array[2]);
				int revision = int.Parse(array[3]);
				result = new ServerVersion(major, minor, build, revision, null).ToInt();
			}
			catch (FormatException)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, object, string>((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Server version string was invalid {2}", this.callerInfo.QueryCorrelationId, TraceContext.Get(), serverVersion);
				result = Server.E15MinVersion;
			}
			catch (OverflowException)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, object, string>((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Server version string was invalid {2}", this.callerInfo.QueryCorrelationId, TraceContext.Get(), serverVersion);
				result = Server.E15MinVersion;
			}
			catch (ArgumentNullException)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, object, string>((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Server version string was invalid {2}", this.callerInfo.QueryCorrelationId, TraceContext.Get(), serverVersion);
				result = Server.E15MinVersion;
			}
			return result;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0004124C File Offset: 0x0003F44C
		private Dictionary<GroupId, List<MailboxInfo>> CreateGroupIdFromAutoDiscoverResponse(GetUserSettingsResponse response)
		{
			Dictionary<GroupId, List<MailboxInfo>> dictionary = new Dictionary<GroupId, List<MailboxInfo>>(2);
			Dictionary<Uri, List<MailboxInfo>> dictionary2 = new Dictionary<Uri, List<MailboxInfo>>();
			for (int i = 0; i < this.mailboxes.Count; i++)
			{
				GroupId groupId = this.GetGroupId(this.mailboxes[i], response.UserResponses[i]);
				List<MailboxInfo> list;
				if (!dictionary2.TryGetValue(groupId.Uri, out list))
				{
					list = new List<MailboxInfo>();
					dictionary2.Add(groupId.Uri, list);
					dictionary.Add(groupId, list);
				}
				list.Add(this.mailboxes[i]);
			}
			return dictionary;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000412DC File Offset: 0x0003F4DC
		private GroupId GetGroupId(MailboxInfo mailbox, UserResponse userResponse)
		{
			if (userResponse == null)
			{
				return new GroupId(new MultiMailboxSearchException(Strings.descSoapAutoDiscoverInvalidResponseError(this.autodiscoveryEndpoint.ToString())));
			}
			GroupId result = null;
			if (userResponse.ErrorCodeSpecified && this.HasResponseErrorCode(mailbox, userResponse, out result))
			{
				return result;
			}
			if (this.HasSettingErrorInResponse(mailbox, userResponse, out result))
			{
				return result;
			}
			string stringSettingFromResponse = this.GetStringSettingFromResponse(userResponse, mailbox, UserSettingAutodiscovery.ExternalEwsVersion);
			string stringSettingFromResponse2 = this.GetStringSettingFromResponse(userResponse, mailbox, UserSettingAutodiscovery.ExternalEwsUrl);
			if (stringSettingFromResponse2 == null)
			{
				Factory.Current.AutodiscoverTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Request '{1}' for user {2} got no URL value.", this.callerInfo.QueryCorrelationId, this.autodiscoveryEndpoint.ToString(), mailbox.ToString());
				return new GroupId(new MultiMailboxSearchException(Strings.descSoapAutoDiscoverRequestUserSettingInvalidError(this.autodiscoveryEndpoint.ToString(), UserSettingAutodiscovery.ExternalEwsUrl)));
			}
			return new GroupId(GroupType.CrossPremise, new Uri(stringSettingFromResponse2), this.ParseVersionString(stringSettingFromResponse), null)
			{
				Domain = mailbox.GetDomain()
			};
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000413C8 File Offset: 0x0003F5C8
		private bool HasResponseErrorCode(MailboxInfo mailbox, UserResponse userResponse, out GroupId groupId)
		{
			groupId = null;
			if (!userResponse.ErrorCodeSpecified)
			{
				return false;
			}
			switch (userResponse.ErrorCode)
			{
			case ErrorCode.NoError:
				return false;
			case ErrorCode.RedirectAddress:
				Factory.Current.AutodiscoverTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Request '{2}' got address redirect response for user {3} to {4}", new object[]
				{
					this.callerInfo.QueryCorrelationId,
					TraceContext.Get(),
					this,
					mailbox.ToString(),
					userResponse.RedirectTarget
				});
				groupId = new GroupId(new MultiMailboxSearchException(new LocalizedString("Redirect address for autodiscovery is not supported")));
				return true;
			case ErrorCode.RedirectUrl:
				Factory.Current.AutodiscoverTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Request '{2}' got URL redirect response for user {3} but the redirect value is not valid: {4}", new object[]
				{
					this.callerInfo.QueryCorrelationId,
					TraceContext.Get(),
					this,
					mailbox.ToString(),
					userResponse.RedirectTarget
				});
				groupId = new GroupId(new MultiMailboxSearchException(new LocalizedString("Redirect urls for autodiscovery is not supported")));
				return true;
			default:
				groupId = new GroupId(new MultiMailboxSearchException(Strings.descSoapAutoDiscoverRequestUserSettingError(this.autodiscoveryEndpoint.ToString(), UserSettingAutodiscovery.ExternalEwsUrl, userResponse.ErrorCode.ToString())));
				return true;
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0004150C File Offset: 0x0003F70C
		private bool HasSettingErrorInResponse(MailboxInfo mailbox, UserResponse userResponse, out GroupId groupId)
		{
			groupId = null;
			UserSettingError settingErrorFromResponse = this.GetSettingErrorFromResponse(userResponse, UserSettingAutodiscovery.ExternalEwsUrl);
			if (settingErrorFromResponse != null)
			{
				Factory.Current.AutodiscoverTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. Request '{1}' got error response for user {2}. Error: {3}:{4}:{5}", new object[]
				{
					this.callerInfo.QueryCorrelationId,
					this,
					mailbox.ToString(),
					settingErrorFromResponse.SettingName,
					settingErrorFromResponse.ErrorCode,
					settingErrorFromResponse.ErrorMessage
				});
				groupId = new GroupId(new MultiMailboxSearchException(Strings.descSoapAutoDiscoverRequestUserSettingError(this.autodiscoveryEndpoint.ToString(), settingErrorFromResponse.SettingName, settingErrorFromResponse.ErrorMessage)));
				return true;
			}
			return false;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000415C0 File Offset: 0x0003F7C0
		private UserSettingError GetSettingErrorFromResponse(UserResponse userResponse, string settingName)
		{
			if (userResponse.UserSettingErrors != null)
			{
				UserSettingError[] userSettingErrors = userResponse.UserSettingErrors;
				int i = 0;
				while (i < userSettingErrors.Length)
				{
					UserSettingError userSettingError = userSettingErrors[i];
					if (userSettingError != null && StringComparer.InvariantCulture.Equals(userSettingError.SettingName, settingName))
					{
						if (userSettingError.ErrorCode != ErrorCode.NoError)
						{
							return userSettingError;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return null;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00041614 File Offset: 0x0003F814
		private string GetStringSettingFromResponse(UserResponse userResponse, MailboxInfo mailbox, string settingName)
		{
			if (userResponse.UserSettings == null)
			{
				return null;
			}
			UserSetting userSetting = null;
			foreach (UserSetting userSetting2 in userResponse.UserSettings)
			{
				if (StringComparer.InvariantCulture.Equals(userSetting2.Name, settingName))
				{
					userSetting = userSetting2;
					break;
				}
			}
			if (userSetting == null)
			{
				Factory.Current.AutodiscoverTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Request '{2}' for user {3} got response without setting {4}.", new object[]
				{
					this.callerInfo.QueryCorrelationId,
					TraceContext.Get(),
					this,
					mailbox.ToString(),
					settingName
				});
				return null;
			}
			StringSetting stringSetting = userSetting as StringSetting;
			if (stringSetting == null)
			{
				Factory.Current.AutodiscoverTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Request '{2}' for user {3} got response for setting {4} of unexpected type: {5}", new object[]
				{
					this.callerInfo.QueryCorrelationId,
					TraceContext.Get(),
					this,
					mailbox.ToString(),
					settingName,
					userSetting.GetType().Name
				});
				return null;
			}
			if (string.IsNullOrEmpty(stringSetting.Value))
			{
				Factory.Current.AutodiscoverTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Request '{2}' for user {3} got response with empty value for setting {4}", new object[]
				{
					this.callerInfo.QueryCorrelationId,
					TraceContext.Get(),
					this,
					mailbox.ToString(),
					settingName
				});
				return null;
			}
			Factory.Current.AutodiscoverTracer.TraceDebug((long)this.GetHashCode(), "Correlation Id:{0}. {1}: Request '{2}' for user {3} got response for setting {4} with value: {5}", new object[]
			{
				this.callerInfo.QueryCorrelationId,
				TraceContext.Get(),
				this,
				mailbox.ToString(),
				settingName,
				stringSetting.Value
			});
			return stringSetting.Value;
		}

		// Token: 0x040009F5 RID: 2549
		private const int MaximumRedirects = 5;

		// Token: 0x040009F6 RID: 2550
		private static string ExternalEwsUrl = "ExternalEwsUrl";

		// Token: 0x040009F7 RID: 2551
		private static string ExternalEwsVersion = "ExternalEwsVersion";

		// Token: 0x040009F8 RID: 2552
		private static readonly string[] RequestedSettings = new string[]
		{
			UserSettingAutodiscovery.ExternalEwsUrl,
			UserSettingAutodiscovery.ExternalEwsVersion
		};

		// Token: 0x040009F9 RID: 2553
		private static readonly RequestedServerVersion Exchange2013RequestedServerVersion = new RequestedServerVersion
		{
			Text = new string[]
			{
				"Exchange2013"
			}
		};

		// Token: 0x040009FA RID: 2554
		private readonly List<MailboxInfo> mailboxes;

		// Token: 0x040009FB RID: 2555
		private readonly CallerInfo callerInfo;

		// Token: 0x040009FC RID: 2556
		private Uri autodiscoveryEndpoint;

		// Token: 0x040009FD RID: 2557
		private DefaultBinding_Autodiscover client;

		// Token: 0x040009FE RID: 2558
		private Dictionary<GroupId, List<MailboxInfo>> groups;

		// Token: 0x040009FF RID: 2559
		private AsyncCallback callback;

		// Token: 0x04000A00 RID: 2560
		private AsyncResult asyncResult;

		// Token: 0x04000A01 RID: 2561
		private GetUserSettingsRequest request;

		// Token: 0x04000A02 RID: 2562
		private int redirects;

		// Token: 0x04000A03 RID: 2563
		private bool cancelled;
	}
}
