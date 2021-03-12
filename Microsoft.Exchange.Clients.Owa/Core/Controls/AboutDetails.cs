using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002A4 RID: 676
	public class AboutDetails
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x0009688C File Offset: 0x00094A8C
		public AboutDetails(OwaContext owaContext)
		{
			this.details = new List<AboutDetails.Detail>();
			HttpRequest request = owaContext.HttpContext.Request;
			UserContext userContext = owaContext.UserContext;
			ITopologyConfigurationSession topologyConfigurationSession = Utilities.CreateADSystemConfigurationSessionScopedToFirstOrg(true, ConsistencyMode.IgnoreInvalid);
			Server server = topologyConfigurationSession.FindLocalServer();
			string format = "{0} [{1}]";
			string newValue = string.Format(format, userContext.MailboxIdentity.GetOWAMiniRecipient().DisplayName, userContext.MailboxIdentity.GetOWAMiniRecipient().PrimarySmtpAddress);
			this.details.Add(new AboutDetails.Detail(1932854157, newValue));
			if (userContext.MailboxIdentity != userContext.LogonIdentity)
			{
				newValue = string.Format(format, userContext.LogonIdentity.SafeGetRenderableName(), userContext.LogonIdentity.CreateADRecipientBySid().PrimarySmtpAddress);
				this.details.Add(new AboutDetails.Detail(-326055478, newValue));
			}
			this.details.Add(new AboutDetails.Detail(702028774, request.UserAgent));
			this.details.Add(new AboutDetails.Detail(1313983647, null, "spnSilverlight"));
			this.details.Add(new AboutDetails.Detail(-1008891981, "2.0.31005.0"));
			this.details.Add(new AboutDetails.Detail(287279379, userContext.Experiences[0].Name));
			this.details.Add(new AboutDetails.Detail(-580409853, AboutDetails.GetUserLanguage()));
			this.details.Add(new AboutDetails.Detail(-2080731678, AboutDetails.GetUserTimeZone(userContext)));
			this.details.Add(new AboutDetails.Detail(-2119715555, userContext.MailboxSession.MailboxOwner.LegacyDn));
			this.details.Add(new AboutDetails.Detail(1242327, owaContext.LocalHostName));
			this.details.Add(new AboutDetails.Detail(-443267801, Globals.ApplicationVersion));
			if (owaContext.IsProxyRequest)
			{
				this.details.Add(new AboutDetails.Detail(-115535529, owaContext.ProxyCasUri.ToString()));
				this.details.Add(new AboutDetails.Detail(1033866607, owaContext.ProxyCasVersion.ToString()));
				this.details.Add(new AboutDetails.Detail(1725379342, owaContext.ProxyCasUri.Host));
			}
			else
			{
				this.details.Add(new AboutDetails.Detail(-1140953118, request.ServerVariables["SERVER_NAME"]));
			}
			if (!userContext.IsBasicExperience && Utilities.IsSMimeFeatureUsable(owaContext))
			{
				this.details.Add(new AboutDetails.Detail(-536214884, null, "dvMmVer"));
				if (OwaRegistryKeys.AllowUserChoiceOfSigningCertificate)
				{
					string text;
					if (userContext.UserOptions.UseManuallyPickedSigningCertificate)
					{
						text = AboutDetails.FormatSigningCertificateId(userContext);
					}
					else
					{
						text = LocalizedStrings.GetNonEncoded(850704157);
					}
					if (!string.IsNullOrEmpty(text))
					{
						this.details.Add(new AboutDetails.Detail(2075380721, text));
					}
				}
			}
			this.details.Add(new AboutDetails.Detail(585827348, server.Fqdn));
			this.details.Add(new AboutDetails.Detail(1747010592, Environment.Version.ToString()));
			this.details.Add(new AboutDetails.Detail(-1210690029, Environment.OSVersion.ToString()));
			string text2 = CultureInfo.InstalledUICulture.IetfLanguageTag;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = string.Empty;
			}
			this.details.Add(new AboutDetails.Detail(1325823303, text2));
			ServerVersion serverVersion = ServerVersion.CreateFromVersionNumber(server.VersionNumber);
			this.details.Add(new AboutDetails.Detail(36884659, serverVersion.ToString()));
			string text3 = Globals.ServerCulture.IetfLanguageTag;
			if (string.IsNullOrEmpty(text3))
			{
				text3 = string.Empty;
			}
			this.details.Add(new AboutDetails.Detail(2002567151, text3));
			this.details.Add(new AboutDetails.Detail(-983285290, ExTimeZone.CurrentTimeZone.LocalizableDisplayName.ToString(Thread.CurrentThread.CurrentUICulture)));
			this.details.Add(new AboutDetails.Detail(-967424827, AboutDetails.GetCASFlavor()));
			this.details.Add(new AboutDetails.Detail(1832080745, userContext.MailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn));
			serverVersion = ServerVersion.CreateFromVersionNumber(userContext.MailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion);
			this.details.Add(new AboutDetails.Detail(-764368819, serverVersion.ToString()));
			this.details.Add(new AboutDetails.Detail(584845051, AboutDetails.GetCASRoles(server)));
			this.details.Add(new AboutDetails.Detail(1023146649, owaContext.UserContext.LogonIdentity.AuthenticationType));
			if (owaContext.IsProxyRequest)
			{
				this.details.Add(new AboutDetails.Detail(-1163209895, owaContext.HttpContext.User.Identity.AuthenticationType));
			}
			this.details.Add(new AboutDetails.Detail(33982382, owaContext.UserContext.IsPublicLogon ? LocalizedStrings.GetNonEncoded(-1273337393) : LocalizedStrings.GetNonEncoded(1496915101)));
			bool flag = !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.HideInternalUrls.Enabled;
			this.AddEndpointSettings(ServiceType.Pop3, true);
			if (flag)
			{
				this.AddEndpointSettings(ServiceType.Pop3, false);
			}
			this.AddEndpointSettings(ServiceType.Imap4, true);
			if (flag)
			{
				this.AddEndpointSettings(ServiceType.Imap4, false);
			}
			this.AddEndpointSettings(ServiceType.Smtp, true);
			if (flag)
			{
				this.AddEndpointSettings(ServiceType.Smtp, false);
			}
			if (!owaContext.UserContext.IsBasicExperience && FormsRegistryManager.HasCustomForm)
			{
				this.details.Add(new AboutDetails.Detail(-308367532, LocalizedStrings.GetNonEncoded(-761624927)));
			}
			this.details.Add(new AboutDetails.Detail(2084940277, userContext.SegmentationFlags.ToString("x")));
			this.details.Add(new AboutDetails.Detail(1383125876, userContext.RestrictedCapabilitiesFlags.ToString("x")));
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00096EA0 File Offset: 0x000950A0
		private static string FormatSigningCertificateId(UserContext userContext)
		{
			byte[] array;
			try
			{
				array = Convert.FromBase64String(userContext.UserOptions.SigningCertificateId);
			}
			catch (FormatException)
			{
				return null;
			}
			int num = 0;
			if (array.Length < num + 4)
			{
				return null;
			}
			int num2 = BitConverter.ToInt32(array, num);
			num += 4;
			if (num2 == 1)
			{
				if (array.Length < num + 4)
				{
					return null;
				}
				int num3 = BitConverter.ToInt32(array, num);
				num += 4;
				if (array.Length < num + num3)
				{
					return null;
				}
				byte[] array2 = new byte[num3];
				Array.Copy(array, num, array2, 0, num3);
				StringBuilder stringBuilder = new StringBuilder(num3);
				foreach (KeyValuePair<Oid, string> keyValuePair in X500DistinguishedNameDecoder.Decode(new X500DistinguishedName(array2)))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(keyValuePair.Value);
				}
				num += num3;
				if (array.Length < num + 4)
				{
					return null;
				}
				int num4 = BitConverter.ToInt32(array, num);
				num += 4;
				if (array.Length < num + num4)
				{
					return null;
				}
				StringBuilder stringBuilder2 = new StringBuilder(num4 * 3);
				for (int i = num4 - 1; i >= 0; i--)
				{
					stringBuilder2.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", new object[]
					{
						array[i + num]
					});
					if (i > 0)
					{
						stringBuilder2.Append(' ');
					}
				}
				num += num4;
				return string.Format(LocalizedStrings.GetNonEncoded(264112142), stringBuilder.ToString(), stringBuilder2.ToString());
			}
			else
			{
				if (num2 != 2)
				{
					return null;
				}
				if (array.Length < num + 4)
				{
					return null;
				}
				int num5 = BitConverter.ToInt32(array, num);
				num += 4;
				if (array.Length < num + num5)
				{
					return null;
				}
				byte[] array3 = new byte[num5];
				Array.Copy(array, num, array3, 0, num5);
				AsnEncodedData asnEncodedData = new AsnEncodedData(array3);
				num += num5;
				return string.Format(LocalizedStrings.GetNonEncoded(347942879), asnEncodedData.Format(false));
			}
			string result;
			return result;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000970A0 File Offset: 0x000952A0
		private static string GetUserTimeZone(UserContext userContext)
		{
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				if (string.Equals(exTimeZone.Id, userContext.UserOptions.TimeZone, StringComparison.OrdinalIgnoreCase))
				{
					return exTimeZone.LocalizableDisplayName.ToString(Thread.CurrentThread.CurrentUICulture);
				}
			}
			return string.Empty;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00097120 File Offset: 0x00095320
		private static string GetUserLanguage()
		{
			int lcid = Thread.CurrentThread.CurrentUICulture.LCID;
			CultureInfo[] supportedCultures = Culture.GetSupportedCultures();
			for (int i = 0; i < supportedCultures.Length; i++)
			{
				if (supportedCultures[i].LCID == lcid)
				{
					return supportedCultures[i].NativeName;
				}
			}
			return null;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00097166 File Offset: 0x00095366
		private static string GetCASFlavor()
		{
			if (IntPtr.Size == 4)
			{
				return LocalizedStrings.GetNonEncoded(1224242227);
			}
			if (IntPtr.Size == 8)
			{
				return LocalizedStrings.GetNonEncoded(455753820);
			}
			throw new FormatException();
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00097194 File Offset: 0x00095394
		private static string GetCASRoles(Server localServer)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			if (localServer.IsMailboxServer)
			{
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-1590140135));
			}
			if (localServer.IsHubTransportServer)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(1282493740));
			}
			if (localServer.IsEdgeServer)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(1132922861));
			}
			if (localServer.IsUnifiedMessagingServer)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(-590151333));
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString();
			}
			return LocalizedStrings.GetNonEncoded(223261762);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00097268 File Offset: 0x00095468
		private void AddEndpointSettings(ServiceType type, bool isExternal)
		{
			if (type == ServiceType.Pop3)
			{
				ProtocolConnectionSettings frontEndPop3SettingsForLocalServer = FrontEndLocator.GetFrontEndPop3SettingsForLocalServer();
				this.AddConnectionSettingDetails(type, isExternal, frontEndPop3SettingsForLocalServer);
				return;
			}
			if (type == ServiceType.Imap4)
			{
				ProtocolConnectionSettings frontEndImap4SettingsForLocalServer = FrontEndLocator.GetFrontEndImap4SettingsForLocalServer();
				this.AddConnectionSettingDetails(type, isExternal, frontEndImap4SettingsForLocalServer);
				return;
			}
			if (type == ServiceType.Smtp)
			{
				ProtocolConnectionSettings frontEndSmtpSettingsForLocalServer = FrontEndLocator.GetFrontEndSmtpSettingsForLocalServer();
				this.AddConnectionSettingDetails(type, isExternal, frontEndSmtpSettingsForLocalServer);
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000972B4 File Offset: 0x000954B4
		private void AddConnectionSettingDetails(ServiceType settingType, bool isExternal, ProtocolConnectionSettings settings)
		{
			if (this.details == null || settings == null)
			{
				return;
			}
			switch (settingType)
			{
			case ServiceType.Pop3:
				this.details.Add(new AboutDetails.Detail(isExternal ? 888502160 : -2138999282, null));
				break;
			case ServiceType.Imap4:
				this.details.Add(new AboutDetails.Detail(isExternal ? 463879924 : -839607014, null));
				break;
			case ServiceType.Smtp:
				this.details.Add(new AboutDetails.Detail(isExternal ? -969708705 : -487637651, null));
				break;
			}
			this.details.Add(new AboutDetails.Detail(1167375722, settings.Hostname.ToString(), true));
			this.details.Add(new AboutDetails.Detail(-2021978869, settings.Port.ToString(), true));
			if (settings.EncryptionType != null)
			{
				this.details.Add(new AboutDetails.Detail(-505859442, settings.EncryptionType.ToString(), true));
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000973CC File Offset: 0x000955CC
		public void GetDetails(int index, out Strings.IDs name, out string value, out string spanId, out bool shouldIndent)
		{
			name = this.details[index].Name;
			value = this.details[index].Value;
			spanId = this.details[index].SpanId;
			shouldIndent = this.details[index].Indent;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00097434 File Offset: 0x00095634
		public void GetDetails(int index, out Strings.IDs name, out string value, out bool shouldIndent)
		{
			name = this.details[index].Name;
			value = this.details[index].Value;
			shouldIndent = this.details[index].Indent;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x00097484 File Offset: 0x00095684
		public int Count
		{
			get
			{
				return this.details.Count;
			}
		}

		// Token: 0x040012D4 RID: 4820
		private List<AboutDetails.Detail> details;

		// Token: 0x020002A5 RID: 677
		private struct Detail
		{
			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00097491 File Offset: 0x00095691
			internal Strings.IDs Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00097499 File Offset: 0x00095699
			internal string Value
			{
				get
				{
					return this.detailValue;
				}
			}

			// Token: 0x170006DA RID: 1754
			// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000974A1 File Offset: 0x000956A1
			internal string SpanId
			{
				get
				{
					return this.spanId;
				}
			}

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000974A9 File Offset: 0x000956A9
			internal bool Indent
			{
				get
				{
					return this.indent;
				}
			}

			// Token: 0x06001A1C RID: 6684 RVA: 0x000974B1 File Offset: 0x000956B1
			internal Detail(Strings.IDs newName, string newValue)
			{
				this.name = newName;
				this.detailValue = newValue;
				this.spanId = null;
				this.indent = false;
			}

			// Token: 0x06001A1D RID: 6685 RVA: 0x000974CF File Offset: 0x000956CF
			internal Detail(Strings.IDs newName, string newValue, string spanId)
			{
				this = new AboutDetails.Detail(newName, newValue);
				this.spanId = spanId;
			}

			// Token: 0x06001A1E RID: 6686 RVA: 0x000974E0 File Offset: 0x000956E0
			internal Detail(Strings.IDs newName, string newValue, bool shouldIndent)
			{
				this = new AboutDetails.Detail(newName, newValue);
				this.indent = shouldIndent;
			}

			// Token: 0x040012D5 RID: 4821
			private Strings.IDs name;

			// Token: 0x040012D6 RID: 4822
			private string detailValue;

			// Token: 0x040012D7 RID: 4823
			private string spanId;

			// Token: 0x040012D8 RID: 4824
			private bool indent;
		}
	}
}
