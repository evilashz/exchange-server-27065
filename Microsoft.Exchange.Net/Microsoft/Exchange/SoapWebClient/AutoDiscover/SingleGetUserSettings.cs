using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x020006D4 RID: 1748
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SingleGetUserSettings
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0004080B File Offset: 0x0003EA0B
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x00040813 File Offset: 0x0003EA13
		public bool UseWSSecurityUrl { get; set; }

		// Token: 0x060020A1 RID: 8353 RVA: 0x0004081C File Offset: 0x0003EA1C
		public SingleGetUserSettings(params string[] requestedSettings)
		{
			this.requestedSettings = requestedSettings;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0004082B File Offset: 0x0003EA2B
		public UserSettings Discover(AutodiscoverClient client, string user)
		{
			return this.Discover(client, user, null);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00040838 File Offset: 0x0003EA38
		public UserSettings Discover(AutodiscoverClient client, string user, Uri url)
		{
			InvokeDelegate invokeDelegate = this.GetInvokeDelegate(user);
			SingleGetUserSettings.DiscoveryResultData discoveryResultData = null;
			for (int i = 0; i < 3; i++)
			{
				if (url != null)
				{
					SingleGetUserSettings.Tracer.TraceDebug<SingleGetUserSettings, string, Uri>((long)this.GetHashCode(), "{0}: Discover user {1} at {2}", this, user, url);
					discoveryResultData = this.HandleResult(client.InvokeWithEndpoint(invokeDelegate, url));
					url = null;
				}
				else
				{
					SingleGetUserSettings.Tracer.TraceDebug<SingleGetUserSettings, string>((long)this.GetHashCode(), "{0}: Discover user {1}", this, user);
					discoveryResultData = this.HandleResults(client.InvokeWithDiscovery(invokeDelegate, SingleGetUserSettings.GetUserDomain(user)));
				}
				if (discoveryResultData.Result == SingleGetUserSettings.DiscoveryResult.UrlRedirect)
				{
					SingleGetUserSettings.Tracer.TraceDebug<SingleGetUserSettings, Uri>((long)this.GetHashCode(), "{0}: Following URL redirection to {1}", this, discoveryResultData.RedirectUrl);
					if (this.UseWSSecurityUrl)
					{
						url = EwsWsSecurityUrl.Fix(discoveryResultData.RedirectUrl);
					}
					else
					{
						url = discoveryResultData.RedirectUrl;
					}
				}
				else
				{
					if (discoveryResultData.Result != SingleGetUserSettings.DiscoveryResult.AddressRedirect)
					{
						break;
					}
					SingleGetUserSettings.Tracer.TraceDebug<SingleGetUserSettings, string>((long)this.GetHashCode(), "{0}: Following UserAddress redirection to {1}", this, discoveryResultData.RedirectUserAddress);
					user = discoveryResultData.RedirectUserAddress;
					invokeDelegate = this.GetInvokeDelegate(user);
				}
			}
			if (discoveryResultData.Result == SingleGetUserSettings.DiscoveryResult.Success)
			{
				UserSettings userSettings = new UserSettings(discoveryResultData.UserResponse);
				SingleGetUserSettings.Tracer.TraceDebug<SingleGetUserSettings, UserSettings>((long)this.GetHashCode(), "{0}: Received: {1}", this, userSettings);
				return userSettings;
			}
			if (discoveryResultData.Exception != null)
			{
				throw discoveryResultData.Exception;
			}
			throw new GetUserSettingsException(NetException.GetUserSettingsGeneralFailure);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00040988 File Offset: 0x0003EB88
		private static string GetUserDomain(string user)
		{
			int num = user.IndexOf("@");
			if (num != -1)
			{
				string text = user.Substring(num + 1).Trim();
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			throw new GetUserSettingsException(NetException.InvalidUserForGetUserSettings(user));
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00040A00 File Offset: 0x0003EC00
		private InvokeDelegate GetInvokeDelegate(string user)
		{
			GetUserSettingsRequest request = new GetUserSettingsRequest
			{
				RequestedSettings = this.requestedSettings,
				Users = new User[]
				{
					new User
					{
						Mailbox = user
					}
				}
			};
			return delegate(DefaultBinding_Autodiscover binding)
			{
				if (this.UseWSSecurityUrl)
				{
					binding.Url = EwsWsSecurityUrl.Fix(binding.Url);
				}
				return binding.GetUserSettings(request);
			};
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00040A64 File Offset: 0x0003EC64
		private SingleGetUserSettings.DiscoveryResultData HandleResults(IEnumerable<AutodiscoverResultData> results)
		{
			SingleGetUserSettings.DiscoveryResultData discoveryResultData = null;
			foreach (AutodiscoverResultData result in results)
			{
				SingleGetUserSettings.DiscoveryResultData discoveryResultData2 = this.HandleResult(result);
				if (discoveryResultData2.Result != SingleGetUserSettings.DiscoveryResult.Failure)
				{
					return discoveryResultData2;
				}
				if (discoveryResultData == null)
				{
					discoveryResultData = discoveryResultData2;
				}
			}
			return discoveryResultData;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00040ACC File Offset: 0x0003ECCC
		private SingleGetUserSettings.DiscoveryResultData HandleResult(AutodiscoverResultData result)
		{
			switch (result.Type)
			{
			case AutodiscoverResult.Failure:
				return new SingleGetUserSettings.DiscoveryResultData
				{
					Result = SingleGetUserSettings.DiscoveryResult.Failure,
					Exception = new GetUserSettingsException(NetException.GetUserSettingsGeneralFailure, result.Exception)
				};
			case AutodiscoverResult.UnsecuredRedirect:
			case AutodiscoverResult.InvalidSslHostname:
				return new SingleGetUserSettings.DiscoveryResultData
				{
					Result = SingleGetUserSettings.DiscoveryResult.Failure,
					Exception = new GetUserSettingsException(NetException.CannotHandleUnsecuredRedirection)
				};
			}
			GetUserSettingsResponse getUserSettingsResponse = result.Response as GetUserSettingsResponse;
			if (getUserSettingsResponse == null)
			{
				SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings, string>((long)this.GetHashCode(), "{0}: Unexpected response type {1}", this, result.Response.GetType().Name);
				return new SingleGetUserSettings.DiscoveryResultData
				{
					Result = SingleGetUserSettings.DiscoveryResult.Failure,
					Exception = new GetUserSettingsException(NetException.UnexpectedAutodiscoverResult(result.Response.GetType().Name))
				};
			}
			if (getUserSettingsResponse.UserResponses == null)
			{
				SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings>((long)this.GetHashCode(), "{0}: Response with no UserResponses", this);
				return new SingleGetUserSettings.DiscoveryResultData
				{
					Result = SingleGetUserSettings.DiscoveryResult.Failure,
					Exception = new GetUserSettingsException(NetException.UnexpectedUserResponses)
				};
			}
			if (getUserSettingsResponse.UserResponses.Length != 1)
			{
				SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings, int>((long)this.GetHashCode(), "{0}: Response with unexpected number of UserResponses: {1}", this, getUserSettingsResponse.UserResponses.Length);
				return new SingleGetUserSettings.DiscoveryResultData
				{
					Result = SingleGetUserSettings.DiscoveryResult.Failure,
					Exception = new GetUserSettingsException(NetException.UnexpectedUserResponses)
				};
			}
			UserResponse userResponse = getUserSettingsResponse.UserResponses[0];
			if (userResponse == null)
			{
				SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings>((long)this.GetHashCode(), "{0}: No response", this);
				return new SingleGetUserSettings.DiscoveryResultData
				{
					Result = SingleGetUserSettings.DiscoveryResult.Failure,
					Exception = new GetUserSettingsException(NetException.UnexpectedUserResponses)
				};
			}
			if (userResponse.ErrorCodeSpecified && userResponse.ErrorCode != ErrorCode.NoError)
			{
				return this.HandleErrorResponse(userResponse);
			}
			return new SingleGetUserSettings.DiscoveryResultData
			{
				Result = SingleGetUserSettings.DiscoveryResult.Success,
				UserResponse = userResponse
			};
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00040CB0 File Offset: 0x0003EEB0
		private SingleGetUserSettings.DiscoveryResultData HandleErrorResponse(UserResponse userResponse)
		{
			if (userResponse.ErrorCode == ErrorCode.RedirectUrl)
			{
				if (!string.IsNullOrEmpty(userResponse.RedirectTarget))
				{
					if (Uri.IsWellFormedUriString(userResponse.RedirectTarget, UriKind.Absolute))
					{
						Uri uri = new Uri(userResponse.RedirectTarget);
						if (uri.Scheme == Uri.UriSchemeHttps)
						{
							SingleGetUserSettings.Tracer.TraceDebug<SingleGetUserSettings, string>((long)this.GetHashCode(), "{0}: Response has RedirectTarget: {1}", this, userResponse.RedirectTarget);
							return new SingleGetUserSettings.DiscoveryResultData
							{
								Result = SingleGetUserSettings.DiscoveryResult.UrlRedirect,
								RedirectUrl = new Uri(userResponse.RedirectTarget)
							};
						}
						SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings, string>((long)this.GetHashCode(), "{0}: Response has non-HTTPS RedirectTarget: {1}", this, userResponse.RedirectTarget);
					}
					else
					{
						SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings, string>((long)this.GetHashCode(), "{0}: Response has bad RedirectTarget: {1}", this, userResponse.RedirectTarget);
					}
				}
				else
				{
					SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings>((long)this.GetHashCode(), "{0}: Response missing RedirectTarget", this);
				}
			}
			if (userResponse.ErrorCode == ErrorCode.RedirectAddress)
			{
				if (!string.IsNullOrEmpty(userResponse.RedirectTarget))
				{
					return new SingleGetUserSettings.DiscoveryResultData
					{
						Result = SingleGetUserSettings.DiscoveryResult.AddressRedirect,
						RedirectUserAddress = userResponse.RedirectTarget
					};
				}
				SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings>((long)this.GetHashCode(), "{0}: Response missing RedirectTarget", this);
			}
			SingleGetUserSettings.Tracer.TraceError<SingleGetUserSettings, ErrorCode>((long)this.GetHashCode(), "{0}: Response has error: {1}", this, userResponse.ErrorCode);
			return new SingleGetUserSettings.DiscoveryResultData
			{
				Result = SingleGetUserSettings.DiscoveryResult.Failure,
				Exception = new GetUserSettingsException(NetException.UnexpectedUserResponses)
			};
		}

		// Token: 0x04001F5F RID: 8031
		private const int MaximumRedirections = 3;

		// Token: 0x04001F60 RID: 8032
		private static readonly Trace Tracer = ExTraceGlobals.EwsClientTracer;

		// Token: 0x04001F61 RID: 8033
		private string[] requestedSettings;

		// Token: 0x020006D5 RID: 1749
		private enum DiscoveryResult
		{
			// Token: 0x04001F64 RID: 8036
			Success,
			// Token: 0x04001F65 RID: 8037
			Failure,
			// Token: 0x04001F66 RID: 8038
			UrlRedirect,
			// Token: 0x04001F67 RID: 8039
			AddressRedirect
		}

		// Token: 0x020006D6 RID: 1750
		private sealed class DiscoveryResultData
		{
			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x060020AA RID: 8362 RVA: 0x00040E26 File Offset: 0x0003F026
			// (set) Token: 0x060020AB RID: 8363 RVA: 0x00040E2E File Offset: 0x0003F02E
			public SingleGetUserSettings.DiscoveryResult Result { get; set; }

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x060020AC RID: 8364 RVA: 0x00040E37 File Offset: 0x0003F037
			// (set) Token: 0x060020AD RID: 8365 RVA: 0x00040E3F File Offset: 0x0003F03F
			public LocalizedException Exception { get; set; }

			// Token: 0x17000883 RID: 2179
			// (get) Token: 0x060020AE RID: 8366 RVA: 0x00040E48 File Offset: 0x0003F048
			// (set) Token: 0x060020AF RID: 8367 RVA: 0x00040E50 File Offset: 0x0003F050
			public string RedirectUserAddress { get; set; }

			// Token: 0x17000884 RID: 2180
			// (get) Token: 0x060020B0 RID: 8368 RVA: 0x00040E59 File Offset: 0x0003F059
			// (set) Token: 0x060020B1 RID: 8369 RVA: 0x00040E61 File Offset: 0x0003F061
			public Uri RedirectUrl { get; set; }

			// Token: 0x17000885 RID: 2181
			// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00040E6A File Offset: 0x0003F06A
			// (set) Token: 0x060020B3 RID: 8371 RVA: 0x00040E72 File Offset: 0x0003F072
			public UserResponse UserResponse { get; set; }

			// Token: 0x060020B4 RID: 8372 RVA: 0x00040E7C File Offset: 0x0003F07C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(400);
				stringBuilder.Append("Result=" + this.Result + ";");
				if (this.Exception != null)
				{
					stringBuilder.Append("Exception=" + this.Exception.Message + ";");
				}
				if (this.RedirectUserAddress != null)
				{
					stringBuilder.Append("UserAddress=" + this.RedirectUserAddress + ";");
				}
				if (this.RedirectUrl != null)
				{
					stringBuilder.Append("RedirectUrl=" + this.RedirectUrl.ToString() + ";");
				}
				return stringBuilder.ToString();
			}
		}
	}
}
