using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200009E RID: 158
	internal static class SuppressingPiiData
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00016B58 File Offset: 0x00014D58
		static SuppressingPiiData()
		{
			try
			{
				SuppressingPiiData.hasher = new SHA256Cng();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00016BD0 File Offset: 0x00014DD0
		public static ProxyAddress Redact(ProxyAddress proxyAddr, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (proxyAddr == null)
			{
				return proxyAddr;
			}
			ProxyAddressPrefix prefix = proxyAddr.Prefix;
			PiiRedactor<string> redactorForProxyAddress = SuppressingPiiData.GetRedactorForProxyAddress(prefix);
			if (redactorForProxyAddress != null)
			{
				return prefix.GetProxyAddress(redactorForProxyAddress(proxyAddr.AddressString, out raw, out redacted), proxyAddr.IsPrimaryAddress);
			}
			return proxyAddr;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00016C1B File Offset: 0x00014E1B
		public static CountryInfo Redact(CountryInfo countryInfo, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			return null;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00016C24 File Offset: 0x00014E24
		public static string RedactLegacyDN(string legacyDN, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(legacyDN))
			{
				return legacyDN;
			}
			int num = DNConvertor.LastIndexOfUnescapedChar(legacyDN, legacyDN.Length - 1, '/') + 1;
			if (string.Compare(legacyDN, num, "cn=", 0, "cn=".Length, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return legacyDN;
			}
			num += "cn=".Length;
			return legacyDN.Substring(0, num) + SuppressingPiiData.Redact(legacyDN.Substring(num), out raw, out redacted);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00016C98 File Offset: 0x00014E98
		public static string RedactDN(string dn, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(dn))
			{
				return dn;
			}
			if (dn.StartsWith("cn=", StringComparison.OrdinalIgnoreCase))
			{
				int num = DNConvertor.IndexOfUnescapedChar(dn, 0, ',');
				if (num < 0)
				{
					num = dn.Length;
				}
				return dn.Substring(0, "cn=".Length) + SuppressingPiiData.Redact(dn.Substring("cn=".Length, num - "cn=".Length), out raw, out redacted) + dn.Substring(num);
			}
			return dn;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00016D1C File Offset: 0x00014F1C
		public static string RedactSmtpAddress(string smtp)
		{
			string text;
			string text2;
			return SuppressingPiiData.RedactSmtpAddress(smtp, out text, out text2);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00016D34 File Offset: 0x00014F34
		public static string RedactSmtpAddress(string smtp, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(smtp))
			{
				return smtp;
			}
			if (SmtpAddress.IsValidSmtpAddress(smtp))
			{
				return SuppressingPiiData.Redact(new SmtpAddress(smtp), out raw, out redacted).ToString();
			}
			return SuppressingPiiData.Redact(smtp, out raw, out redacted);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00016D80 File Offset: 0x00014F80
		public static SmtpAddress? Redact(SmtpAddress? smtp)
		{
			string text;
			string text2;
			return SuppressingPiiData.Redact(smtp, out text, out text2);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00016D98 File Offset: 0x00014F98
		public static SmtpAddress? Redact(SmtpAddress? smtp, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (smtp == null || string.IsNullOrWhiteSpace(smtp.Value.Local) || string.IsNullOrWhiteSpace(smtp.Value.Domain))
			{
				return smtp;
			}
			return new SmtpAddress?(new SmtpAddress(SuppressingPiiData.Redact(smtp.Value.Local.ToUpperInvariant(), out raw, out redacted), smtp.Value.Domain));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00016E18 File Offset: 0x00015018
		public static SmtpAddress Redact(SmtpAddress smtp, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(smtp.Local) || string.IsNullOrWhiteSpace(smtp.Domain))
			{
				return smtp;
			}
			return new SmtpAddress(SuppressingPiiData.Redact(smtp.Local.ToUpperInvariant(), out raw, out redacted), smtp.Domain);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00016E68 File Offset: 0x00015068
		public static RoutingAddress Redact(RoutingAddress address)
		{
			if (string.IsNullOrWhiteSpace(address.LocalPart) || string.IsNullOrWhiteSpace(address.DomainPart))
			{
				return address;
			}
			string text;
			string text2;
			return new RoutingAddress(SuppressingPiiData.Redact(address.LocalPart.ToUpperInvariant(), out text, out text2), address.DomainPart);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00016EB4 File Offset: 0x000150B4
		public static string ConvertToUpperInvariantAndRedact(string value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
			return SuppressingPiiData.Redact(value.ToUpperInvariant(), out raw, out redacted);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00016ED4 File Offset: 0x000150D4
		public static RecipientInfo[] Redact(RecipientInfo[] recipients, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (recipients != null && recipients.Length > 0)
			{
				raw = new string[recipients.Length];
				redacted = new string[recipients.Length];
				for (int i = 0; i < recipients.Length; i++)
				{
					recipients[i].Address = SuppressingPiiData.RedactSmtpAddress(recipients[i].Address, out raw[i], out redacted[i]);
				}
			}
			return recipients;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00016F38 File Offset: 0x00015138
		public static ADObjectId Redact(ADObjectId adObjectId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (adObjectId == null || string.IsNullOrWhiteSpace(adObjectId.DistinguishedName))
			{
				return adObjectId;
			}
			string distinguishedName = null;
			if (adObjectId.ObjectGuid == Guid.Empty)
			{
				distinguishedName = SuppressingPiiData.RedactDN(adObjectId.DistinguishedName, out raw, out redacted);
			}
			return new ADObjectId(distinguishedName, adObjectId.PartitionGuid, adObjectId.ObjectGuid);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00016F94 File Offset: 0x00015194
		public static ADObjectId[] Redact(ADObjectId[] values)
		{
			if (values != null && values.Length > 0)
			{
				string text = null;
				string text2 = null;
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = SuppressingPiiData.Redact(values[i], out text, out text2);
				}
			}
			return values;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00016FCC File Offset: 0x000151CC
		public static ObjectId RedactFolderId(ObjectId folderId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (folderId is MailboxFolderId)
			{
				MailboxFolderId mailboxFolderId = (MailboxFolderId)folderId;
				folderId = new MailboxFolderId(SuppressingPiiData.Redact(mailboxFolderId.MailboxOwnerId, out raw, out redacted), mailboxFolderId.StoreObjectIdValue, mailboxFolderId.MailboxFolderPath);
			}
			if (folderId is Microsoft.Exchange.Data.Storage.Management.PublicFolderId)
			{
				Microsoft.Exchange.Data.Storage.Management.PublicFolderId publicFolderId = (Microsoft.Exchange.Data.Storage.Management.PublicFolderId)folderId;
				folderId = new Microsoft.Exchange.Data.Storage.Management.PublicFolderId(publicFolderId.OrganizationId, publicFolderId.StoreObjectId, SuppressingPiiData.Redact(publicFolderId.MapiFolderPath, out raw, out redacted));
			}
			return folderId;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001703E File Offset: 0x0001523E
		public static InboxRuleId Redact(InboxRuleId inboxRuleId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			return new InboxRuleId(SuppressingPiiData.Redact(inboxRuleId.MailboxOwnerId, out raw, out redacted), inboxRuleId.Name, inboxRuleId.RuleId);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00017064 File Offset: 0x00015264
		public static Uri Redact(Uri uri, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (uri != null)
			{
				raw = uri.OriginalString;
				redacted = SuppressingPiiData.Redact(uri.OriginalString);
				uri = new Uri(redacted, UriKind.RelativeOrAbsolute);
			}
			return uri;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017098 File Offset: 0x00015298
		public static ObjectId RedactMailboxStoreObjectId(ObjectId objectId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			MailboxStoreObjectId mailboxStoreObjectId = objectId as MailboxStoreObjectId;
			if (mailboxStoreObjectId != null)
			{
				return new MailboxStoreObjectId(SuppressingPiiData.Redact(mailboxStoreObjectId.MailboxOwnerId, out raw, out redacted), mailboxStoreObjectId.StoreObjectId);
			}
			return objectId;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000170D8 File Offset: 0x000152D8
		public static ObjectId RedactMailboxStoreObjectId(ObjectId objectId)
		{
			string text;
			string text2;
			return SuppressingPiiData.RedactMailboxStoreObjectId(objectId, out text, out text2);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000170EF File Offset: 0x000152EF
		public static MapiFolderPath Redact(MapiFolderPath folderPath, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (folderPath != null)
			{
				raw = folderPath.ToString();
				redacted = SuppressingPiiData.Redact(raw);
				folderPath = new MapiFolderPath('\\' + redacted);
			}
			return folderPath;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017128 File Offset: 0x00015328
		public static MailboxFolder Redact(MailboxFolder folder, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (folder != null)
			{
				folder.Name = SuppressingPiiData.Redact(folder.Name);
				folder.FolderPath = SuppressingPiiData.Redact(folder.FolderPath, out raw, out redacted);
				folder.MailboxOwnerId = SuppressingPiiData.Redact(folder.MailboxOwnerId, out raw, out redacted);
				return folder;
			}
			return folder;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00017178 File Offset: 0x00015378
		public static NetID Redact(NetID netID, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			return null;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00017181 File Offset: 0x00015381
		public static string Redact(SecurityIdentifier sid, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (sid == null)
			{
				return null;
			}
			return SuppressingPiiData.Redact(sid.Value, out raw, out redacted);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000171A1 File Offset: 0x000153A1
		public static E164Number Redact(E164Number e164Number, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			return null;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000171AC File Offset: 0x000153AC
		public static string RedactAltSecurityId(string altSecurityId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(altSecurityId))
			{
				return altSecurityId;
			}
			int num = altSecurityId.IndexOf(':') + 1;
			string arg = altSecurityId.Substring(0, num);
			NetID netID;
			string text;
			if (ADUser.ParseSecIDValue(altSecurityId, out netID, out text) || ADUser.ParseConsumerSecIDValue(altSecurityId, out netID))
			{
				return string.Format("{0}{1}{2}", arg, SuppressingPiiData.Redact(netID.ToString(), out raw, out redacted), altSecurityId.Substring(num + 16));
			}
			return altSecurityId;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017217 File Offset: 0x00015417
		public static byte[] Redact(byte[] value, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			return null;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00017220 File Offset: 0x00015420
		public static object RedactCertificate(object value, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			MultiValuedProperty<byte[]> multiValuedProperty = value as MultiValuedProperty<byte[]>;
			if (multiValuedProperty == null)
			{
				return null;
			}
			MultiValuedProperty<byte[]> multiValuedProperty2 = new MultiValuedProperty<byte[]>();
			if (multiValuedProperty.Count == 0 || multiValuedProperty.FirstOrDefault<byte[]>() == null)
			{
				return multiValuedProperty2;
			}
			multiValuedProperty2.Add(new byte[128]);
			return multiValuedProperty2;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00017268 File Offset: 0x00015468
		public static string RedactUMDtmfMap(string value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(value))
			{
				return value;
			}
			int num = value.IndexOf(':');
			if (num < 0)
			{
				return SuppressingPiiData.Redact(value, out raw, out redacted);
			}
			return value.Substring(0, num + 1) + SuppressingPiiData.Redact(value.Substring(num + 1), out raw, out redacted);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000172BC File Offset: 0x000154BC
		public static string Redact(string value)
		{
			string text;
			string text2;
			return SuppressingPiiData.Redact(value, out text, out text2);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000172D4 File Offset: 0x000154D4
		public static string Redact(string value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(value) || SuppressingPiiData.redactedString.IsMatch(value))
			{
				return value;
			}
			if (ADObjectNameHelper.ReservedADNameStringRegex.IsMatch(value))
			{
				return value;
			}
			if (SmtpAddress.IsValidSmtpAddress(value))
			{
				return SuppressingPiiData.RedactSmtpAddress(value, out raw, out redacted);
			}
			raw = value;
			try
			{
				if (SuppressingPiiData.hasher == null)
				{
					redacted = string.Empty;
				}
				else
				{
					redacted = BitConverter.ToString(SuppressingPiiData.hasher.ComputeHash(Encoding.Unicode.GetBytes(value))).Replace("-", string.Empty).ToLower().Substring(0, 32);
				}
			}
			catch (Exception)
			{
				redacted = string.Empty;
			}
			return SuppressingPiiConfig.RedactedDataPrefix + redacted;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00017394 File Offset: 0x00015594
		public static string RedactWithoutHashing(string value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			return "REDACTED";
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000173A4 File Offset: 0x000155A4
		public static LocalizedString Redact(LocalizedString value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			LocalizedString result;
			if (!SuppressingPiiData.TryRedactPiiLocString(value, SuppressingPiiContext.PiiMap, out result))
			{
				result = new LocalizedString(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted));
			}
			return result;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000173E8 File Offset: 0x000155E8
		public static LocalizedString Redact(LocalizedString original, PiiMap piiMap)
		{
			LocalizedString result;
			SuppressingPiiData.TryRedactPiiLocString(original, piiMap, out result);
			return result;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00017400 File Offset: 0x00015600
		public static KeyValuePair<string, LocalizedString>[] Redact(KeyValuePair<string, LocalizedString>[] value, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			for (int i = 0; i < value.Length; i++)
			{
				string text;
				string text2;
				value[i] = new KeyValuePair<string, LocalizedString>(value[i].Key, SuppressingPiiData.Redact(value[i].Value, out text, out text2));
			}
			return value;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00017459 File Offset: 0x00015659
		public static ADRecipientOrAddress Redact(ADRecipientOrAddress value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new ADRecipientOrAddress(new Participant(SuppressingPiiData.Redact(value.DisplayName, out raw, out redacted), SuppressingPiiData.Redact(value.Address, out raw, out redacted), null));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001748C File Offset: 0x0001568C
		public static ADRecipientOrAddress[] Redact(ADRecipientOrAddress[] value, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			raw = new string[value.Length];
			redacted = new string[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				value[i] = SuppressingPiiData.Redact(value[i], out raw[i], out redacted[i]);
			}
			return value;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000174E2 File Offset: 0x000156E2
		public static bool ContainsRedactedValue(string value)
		{
			return !string.IsNullOrEmpty(value) && SuppressingPiiData.containsRedactedValue.IsMatch(value);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000174FC File Offset: 0x000156FC
		private static string RedactX400Address(string x400Addr, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(x400Addr))
			{
				return x400Addr;
			}
			IList<string> list;
			if (!X400AddressParser.TryParse(x400Addr, out list))
			{
				return x400Addr;
			}
			foreach (int num in SuppressingPiiData.typesNeedRedact)
			{
				if (num < list.Count)
				{
					string text = SuppressingPiiData.Redact(list[num], out raw, out redacted);
					if (text != null && X400AddressParser.MaxComponentLengths[num] < text.Length)
					{
						text = text.Substring(0, X400AddressParser.MaxComponentLengths[num]);
					}
					list[num] = text;
				}
			}
			return X400AddressParser.ToCanonicalString(list);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001758C File Offset: 0x0001578C
		private static string RedactEum(string eum, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (string.IsNullOrWhiteSpace(eum))
			{
				return eum;
			}
			int num = eum.IndexOf(';');
			if (num <= 0)
			{
				return eum;
			}
			string text = eum.Substring(0, num);
			if (SmtpAddress.IsValidSmtpAddress(text))
			{
				return SuppressingPiiData.RedactSmtpAddress(text, out raw, out redacted) + eum.Substring(num);
			}
			return SuppressingPiiData.Redact(text, out raw, out redacted) + eum.Substring(num);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000175F4 File Offset: 0x000157F4
		private static PiiRedactor<string> GetRedactorForProxyAddress(ProxyAddressPrefix prefix)
		{
			PiiRedactor<string> result = null;
			if (prefix.PrimaryPrefix == ProxyAddressPrefix.LegacyDN.PrimaryPrefix || prefix.PrimaryPrefix == ProxyAddressPrefix.X500.PrimaryPrefix)
			{
				result = new PiiRedactor<string>(SuppressingPiiData.RedactLegacyDN);
			}
			else if (prefix.PrimaryPrefix == ProxyAddressPrefix.Notes.PrimaryPrefix || prefix.PrimaryPrefix == ProxyAddressPrefix.SIP.PrimaryPrefix || prefix is SmtpProxyAddressPrefix)
			{
				result = new PiiRedactor<string>(SuppressingPiiData.RedactSmtpAddress);
			}
			else if (prefix is X400ProxyAddressPrefix)
			{
				result = new PiiRedactor<string>(SuppressingPiiData.RedactX400Address);
			}
			else if (prefix is EumProxyAddressPrefix)
			{
				result = new PiiRedactor<string>(SuppressingPiiData.RedactEum);
			}
			else if (prefix.PrimaryPrefix == ProxyAddressPrefix.CcMail.PrimaryPrefix)
			{
				result = new PiiRedactor<string>(SuppressingPiiData.Redact);
			}
			else if (!(prefix.PrimaryPrefix == ProxyAddressPrefix.MsMail.PrimaryPrefix) && !(prefix.PrimaryPrefix == ProxyAddressPrefix.GroupWise.PrimaryPrefix) && !(prefix is MeumProxyAddressPrefix))
			{
				result = new PiiRedactor<string>(SuppressingPiiData.Redact);
			}
			return result;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00017728 File Offset: 0x00015928
		public static string[] Redact(string[] values, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (values != null && values.Length > 0)
			{
				raw = new string[values.Length];
				redacted = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = SuppressingPiiData.Redact(values[i], out raw[i], out redacted[i]);
				}
			}
			return values;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00017784 File Offset: 0x00015984
		public static string[] Redact(string[] values)
		{
			string[] array;
			string[] array2;
			return SuppressingPiiData.Redact(values, out array, out array2);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001779C File Offset: 0x0001599C
		public static SmtpAddress[] Redact(SmtpAddress[] values, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (values != null && values.Length > 0)
			{
				raw = new string[values.Length];
				redacted = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					values[i] = SuppressingPiiData.Redact(values[i], out raw[i], out redacted[i]);
				}
			}
			return values;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00017808 File Offset: 0x00015A08
		public static DisclaimerText? Redact(DisclaimerText? value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new DisclaimerText?(new DisclaimerText(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted)));
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001784C File Offset: 0x00015A4C
		public static DsnText? Redact(DsnText? value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new DsnText?(new DsnText(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted)));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00017890 File Offset: 0x00015A90
		public static EventLogText? Redact(EventLogText? value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new EventLogText?(new EventLogText(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted)));
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000178D4 File Offset: 0x00015AD4
		public static HeaderName? Redact(HeaderName? value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new HeaderName?(new HeaderName(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted)));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00017918 File Offset: 0x00015B18
		public static HeaderValue? Redact(HeaderValue? value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new HeaderValue?(new HeaderValue(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted)));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001795C File Offset: 0x00015B5C
		public static RecipientIdParameter Redact(RecipientIdParameter value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new RecipientIdParameter(SuppressingPiiData.RedactSmtpAddress(value.RawIdentity, out raw, out redacted));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001797C File Offset: 0x00015B7C
		public static RecipientIdParameter[] Redact(RecipientIdParameter[] values, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (values != null && values.Any<RecipientIdParameter>())
			{
				raw = new string[values.Length];
				redacted = new string[values.Length];
				RecipientIdParameter[] array = new RecipientIdParameter[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = SuppressingPiiData.Redact(values[i], out raw[i], out redacted[i]);
				}
				values = array;
			}
			return values;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000179E4 File Offset: 0x00015BE4
		public static RejectText? Redact(RejectText? value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new RejectText?(new RejectText(SuppressingPiiData.Redact(value.ToString(), out raw, out redacted)));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00017A28 File Offset: 0x00015C28
		public static RmsTemplateIdentity Redact(RmsTemplateIdentity value, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (value == null)
			{
				return null;
			}
			return new RmsTemplateIdentity(value.TemplateId, SuppressingPiiData.Redact(value.TemplateName, out raw, out redacted), value.Type);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00017A54 File Offset: 0x00015C54
		public static Word[] Redact(Word[] values, out string[] raw, out string[] redacted)
		{
			raw = null;
			redacted = null;
			if (values != null && values.Any<Word>())
			{
				raw = new string[values.Length];
				redacted = new string[values.Length];
				Word[] array = new Word[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = new Word(SuppressingPiiData.Redact(values[i].ToString(), out raw[i], out redacted[i]));
				}
				values = array;
			}
			return values;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00017AD9 File Offset: 0x00015CD9
		public static ObjectId RedactMailboxId(ObjectId mailboxId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (mailboxId is MailboxId)
			{
				mailboxId = new MailboxId(SuppressingPiiData.RedactLegacyDN(((MailboxId)mailboxId).MailboxExchangeLegacyDn, out raw, out redacted));
			}
			return mailboxId;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00017B04 File Offset: 0x00015D04
		public static MigrationUserId Redact(MigrationUserId migUserId, out string raw, out string redacted)
		{
			raw = null;
			redacted = null;
			if (migUserId == null)
			{
				return null;
			}
			if (migUserId.JobItemGuid != Guid.Empty)
			{
				return new MigrationUserId(string.Empty, migUserId.JobItemGuid);
			}
			return new MigrationUserId(SuppressingPiiData.Redact(migUserId.Id, out raw, out redacted), migUserId.JobItemGuid);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00017B58 File Offset: 0x00015D58
		public static bool TryRedactPiiLocString(LocalizedString original, PiiMap piiMap, out LocalizedString redactedLocString)
		{
			if (original.FormatParameters != null)
			{
				object[] array = original.FormatParameters.ToArray<object>();
				IEnumerable<int> piiParams = SuppressingPiiData.GetPiiParams(original.FullId, array);
				bool flag = false;
				foreach (int num in piiParams)
				{
					string value = null;
					string key = null;
					if (array[num] is LocalizedString)
					{
						flag |= SuppressingPiiData.TryRedactPiiLocString((LocalizedString)array[num], piiMap, out redactedLocString);
						array[num] = redactedLocString;
					}
					else if (array[num] is ADObjectId)
					{
						flag = true;
						array[num] = SuppressingPiiData.Redact((ADObjectId)array[num], out value, out key);
					}
					else if (array[num] != null)
					{
						flag = true;
						array[num] = SuppressingPiiData.Redact(array[num].ToString(), out value, out key);
					}
					if (piiMap != null && !string.IsNullOrEmpty(value))
					{
						piiMap[key] = value;
					}
				}
				redactedLocString = (flag ? original.RecreateWithNewParams(array) : original);
				return flag;
			}
			redactedLocString = original;
			return false;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00017DD8 File Offset: 0x00015FD8
		private static IEnumerable<int> GetPiiParams(string fullId, object[] parameters)
		{
			int[] piiParamIndexes = SuppressingPiiProperty.GetPiiStringParams(fullId);
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i] is LocalizedString)
				{
					yield return i;
				}
				else if (piiParamIndexes != null && piiParamIndexes.Contains(i))
				{
					yield return i;
				}
			}
			yield break;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00017DFC File Offset: 0x00015FFC
		public static IdentityDetails Redact(IdentityDetails identityDetails)
		{
			string text;
			string text2;
			ADObjectId identity = SuppressingPiiData.Redact(identityDetails.Identity, out text, out text2);
			string name = SuppressingPiiData.Redact(identityDetails.Name);
			string displayName = SuppressingPiiData.Redact(identityDetails.DisplayName);
			return new IdentityDetails(identity, name, displayName, identityDetails.ExternalDirectoryObjectId);
		}

		// Token: 0x0400013A RID: 314
		private const string CnPrefix = "cn=";

		// Token: 0x0400013B RID: 315
		private static readonly Regex redactedString = new Regex(string.Format("^{0}[0-9A-Fa-f]{{32}}$", SuppressingPiiConfig.RedactedDataPrefix), RegexOptions.Compiled);

		// Token: 0x0400013C RID: 316
		private static readonly Regex containsRedactedValue = new Regex(string.Format("{0}[0-9A-Fa-f]{{32}}", SuppressingPiiConfig.RedactedDataPrefix), RegexOptions.Compiled);

		// Token: 0x0400013D RID: 317
		private static readonly int[] typesNeedRedact = new int[]
		{
			10,
			9,
			11,
			8
		};

		// Token: 0x0400013E RID: 318
		private static readonly HashAlgorithm hasher;
	}
}
