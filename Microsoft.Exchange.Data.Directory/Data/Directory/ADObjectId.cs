using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public sealed class ADObjectId : ObjectId, IDnFormattable, ITraceable, INonOrgHierarchy
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x00015AF8 File Offset: 0x00013CF8
		public ADObjectId() : this(string.Empty, Guid.Empty)
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00015B0A File Offset: 0x00013D0A
		public ADObjectId(AdName distinguishedName) : this((distinguishedName != null) ? distinguishedName.ToString() : string.Empty)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00015B28 File Offset: 0x00013D28
		public ADObjectId(string distinguishedName) : this(distinguishedName, Guid.Empty)
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00015B36 File Offset: 0x00013D36
		public ADObjectId(Guid partitionGuid, Guid guid) : this(string.Empty, partitionGuid, guid)
		{
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00015B45 File Offset: 0x00013D45
		public ADObjectId(Guid guid, string partitionFQDN) : this(string.Empty, Guid.Empty, partitionFQDN, guid, false)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00015B5A File Offset: 0x00013D5A
		public ADObjectId(Guid guid) : this(string.Empty, guid)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00015B68 File Offset: 0x00013D68
		public ADObjectId(string distinguishedName, Guid objectGuid) : this(distinguishedName, Guid.Empty, null, objectGuid, true)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00015B79 File Offset: 0x00013D79
		public ADObjectId(string distinguishedName, Guid partitionGuid, Guid objectGuid) : this(distinguishedName, partitionGuid, null, objectGuid, true)
		{
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015B86 File Offset: 0x00013D86
		public ADObjectId(string distinguishedName, string partitionFQDN, Guid objectGuid) : this(distinguishedName, Guid.Empty, partitionFQDN, objectGuid, false)
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015B97 File Offset: 0x00013D97
		public ADObjectId(byte[] bytes) : this(bytes, Encoding.Unicode)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00015BA5 File Offset: 0x00013DA5
		public ADObjectId(byte[] bytes, Encoding encoding) : this(bytes, encoding, 0)
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00015BB0 File Offset: 0x00013DB0
		private ADObjectId(string distinguishedName, Guid partitionGuid, string partitionFQDN, Guid objectGuid, bool validateDN)
		{
			this.depth = -1;
			base..ctor();
			if (string.IsNullOrEmpty(distinguishedName))
			{
				this.distinguishedName = string.Empty;
			}
			else if (validateDN)
			{
				this.distinguishedName = ADObjectId.FormatDN(distinguishedName, true, out this.depth);
			}
			else
			{
				this.distinguishedName = distinguishedName;
			}
			this.partitionGuid = partitionGuid;
			this.objectGuid = objectGuid;
			this.securityIdentifierString = null;
			if (!string.IsNullOrEmpty(partitionFQDN))
			{
				this.partitionFqdn = partitionFQDN;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00015C24 File Offset: 0x00013E24
		private ADObjectId(string dn, Guid partitionGuid, string partitionFQDN, Guid guid, int depth, OrganizationId orgId, string sid)
		{
			this.depth = -1;
			base..ctor();
			this.distinguishedName = dn;
			this.partitionGuid = partitionGuid;
			this.partitionFqdn = partitionFQDN;
			this.objectGuid = guid;
			this.depth = depth;
			this.executingUserOrganization = orgId;
			this.securityIdentifierString = sid;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00015C73 File Offset: 0x00013E73
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00015C7B File Offset: 0x00013E7B
		public OrganizationId OrgHierarchyToIgnore
		{
			get
			{
				return this.orgHierarchyToIgnore;
			}
			set
			{
				this.orgHierarchyToIgnore = value;
				this.toStringVal = null;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00015C8C File Offset: 0x00013E8C
		public static bool IsValidDistinguishedName(string distinguishedName)
		{
			if (distinguishedName == null)
			{
				return false;
			}
			string text = null;
			int num = 0;
			return ADObjectId.TryFormatDN(distinguishedName, true, out text, out num);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00015CB0 File Offset: 0x00013EB0
		public static ADObjectId ParseDnOrGuid(string input)
		{
			ADObjectId result = null;
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (!ADObjectId.TryParseDnOrGuid(input, out result))
			{
				throw new FormatException(DirectoryStrings.InvalidIdFormat(input));
			}
			return result;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00015CEC File Offset: 0x00013EEC
		public static bool Equals(ADObjectId x, ADObjectId y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.ObjectGuid == y.ObjectGuid)
			{
				return x.ObjectGuid != Guid.Empty || string.Equals(x.DistinguishedName, y.DistinguishedName, StringComparison.OrdinalIgnoreCase);
			}
			return (!(x.ObjectGuid != Guid.Empty) || !(y.ObjectGuid != Guid.Empty)) && !string.IsNullOrEmpty(x.DistinguishedName) && !string.IsNullOrEmpty(y.DistinguishedName) && string.Equals(x.DistinguishedName, y.DistinguishedName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00015D95 File Offset: 0x00013F95
		public override byte[] GetBytes()
		{
			return this.GetBytes(Encoding.Unicode);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00015DA4 File Offset: 0x00013FA4
		public byte[] GetBytes(Encoding encoding)
		{
			int byteCount = this.GetByteCount(encoding);
			byte[] array = new byte[byteCount];
			this.GetBytes(encoding, array, 0);
			return array;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00015DCA File Offset: 0x00013FCA
		public string ToDNString()
		{
			if (!string.IsNullOrEmpty(this.DistinguishedName))
			{
				return this.DistinguishedName;
			}
			if (this.objectGuid != Guid.Empty)
			{
				return ADObjectId.ToGuidString(this.objectGuid);
			}
			return string.Empty;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00015E03 File Offset: 0x00014003
		public string ToGuidOrDNString()
		{
			if (this.objectGuid != Guid.Empty)
			{
				return ADObjectId.ToGuidString(this.objectGuid);
			}
			if (!string.IsNullOrEmpty(this.DistinguishedName))
			{
				return this.DistinguishedName;
			}
			return string.Empty;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00015E3C File Offset: 0x0001403C
		public string ToExtendedDN()
		{
			string text = string.Empty;
			if (this.objectGuid != Guid.Empty)
			{
				text = ADObjectId.ToGuidString(this.objectGuid);
			}
			if (!string.IsNullOrEmpty(this.DistinguishedName))
			{
				if (this.objectGuid != Guid.Empty)
				{
					text = text + ";" + this.DistinguishedName;
				}
				else
				{
					text = this.DistinguishedName;
				}
			}
			return text;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00015EA8 File Offset: 0x000140A8
		public void TraceTo(ITraceBuilder traceBuilder)
		{
			traceBuilder.AddArgument(string.Format("ADObjectId({0},{1})", string.IsNullOrEmpty(this.DistinguishedName) ? "<null/empty>" : this.DistinguishedName, (this.objectGuid == Guid.Empty) ? "<empty>" : this.objectGuid.ToString()));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00015F0C File Offset: 0x0001410C
		public override string ToString()
		{
			if (this.toStringVal != null)
			{
				return this.toStringVal;
			}
			if (string.IsNullOrEmpty(this.DistinguishedName) && this.ObjectGuid != Guid.Empty)
			{
				return this.ObjectGuid.ToString();
			}
			if (string.IsNullOrEmpty(this.DistinguishedName))
			{
				return this.toStringVal;
			}
			string text = this.DistinguishedName.ToLower();
			OrganizationId organizationId = (this.OrgHierarchyToIgnore != null && (this.executingUserOrganization == null || OrganizationId.ForestWideOrgId.Equals(this.executingUserOrganization))) ? this.OrgHierarchyToIgnore : this.executingUserOrganization;
			if (!this.IsTenantId(this, organizationId))
			{
				if (!this.IsIdUnderConfigurationContainer(text))
				{
					this.toStringVal = this.ResolveDomainNCToString(this.DistinguishedName);
				}
				else
				{
					this.toStringVal = this.ResolveConfigNCToString(this.DistinguishedName);
					if (text.Contains(",cn=configurationunits,"))
					{
						string text2 = this.DistinguishedName.Substring(0, this.DistinguishedName.LastIndexOf("CN=ConfigurationUnits", StringComparison.OrdinalIgnoreCase) - 1);
						if (text2.Contains("CN=Configuration"))
						{
							if (text.IndexOf("CN=Configuration", StringComparison.OrdinalIgnoreCase) == 0)
							{
								this.toStringVal = string.Empty;
							}
							text2 = text2.Substring(text2.LastIndexOf("CN=Configuration", StringComparison.OrdinalIgnoreCase) + "CN=Configuration".Length);
							this.toStringVal = this.AppendHierarchicalIdentity(this.toStringVal, this.BuildHierarchicalIdentity(text2));
						}
					}
				}
			}
			else if (!this.IsIdUnderConfigurationContainer(text))
			{
				if (this.DistinguishedName.Equals(organizationId.OrganizationalUnit.DistinguishedName))
				{
					string text3 = organizationId.OrganizationalUnit.Rdn.ToString();
					this.toStringVal = AdName.Unescape(text3.Substring(text3.IndexOf('=') + 1));
				}
				else
				{
					string partialDn = this.DistinguishedName.Replace(organizationId.OrganizationalUnit.DistinguishedName, string.Empty);
					this.toStringVal = this.BuildHierarchicalIdentity(partialDn);
				}
			}
			else
			{
				string text4 = organizationId.ConfigurationUnit.DistinguishedName;
				int length = "CN=ConfigurationUnits".Length;
				int length2 = "CN=Configuration".Length;
				int num = text4.LastIndexOf("CN=ConfigurationUnits", StringComparison.OrdinalIgnoreCase);
				string text5 = "CN=First Organization" + text4.Substring(num + length);
				text5 = this.DistinguishedName.Replace(text4, text5);
				string identity = this.ResolveConfigNCToString(text5);
				string text6 = this.DistinguishedName.Replace(organizationId.ConfigurationUnit.Parent.DistinguishedName, string.Empty);
				text6 = text6.Substring(text6.IndexOf("CN=Configuration") + length2);
				this.toStringVal = this.AppendHierarchicalIdentity(identity, this.BuildHierarchicalIdentity(text6));
			}
			return this.toStringVal;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000161D8 File Offset: 0x000143D8
		private static string ToGuidString(Guid guid)
		{
			if (guid != Guid.Empty)
			{
				return "<GUID=" + guid.ToString() + ">";
			}
			return string.Empty;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001620C File Offset: 0x0001440C
		private static int GetAddressListContainerIndex(string[] splitDn)
		{
			for (int i = 0; i < splitDn.Length; i++)
			{
				if ("cn=address lists container".Equals(splitDn[i], StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001623C File Offset: 0x0001443C
		private static string ResolveLanguageCode2ISOFormat(string languageCode)
		{
			if (string.IsNullOrEmpty(languageCode))
			{
				return null;
			}
			int lcid;
			if (int.TryParse(languageCode, out lcid))
			{
				try
				{
					CultureInfo cultureFromLcid = LocaleMap.GetCultureFromLcid(lcid);
					return cultureFromLcid.Name;
				}
				catch (ArgumentException)
				{
				}
			}
			return null;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00016284 File Offset: 0x00014484
		private static bool TryFormatDN(string distinguishedName, bool validate, out string formattedDN, out int depth)
		{
			formattedDN = string.Empty;
			depth = 0;
			if (!string.IsNullOrEmpty(distinguishedName))
			{
				StringBuilder stringBuilder = null;
				int num = 0;
				int i = 0;
				while (i < distinguishedName.Length)
				{
					if (validate)
					{
						string input = distinguishedName.Substring(i);
						if (!ADObjectId.keycharRdnRegex.IsMatch(input) && !ADObjectId.oidRdnRegex.IsMatch(input))
						{
							return false;
						}
					}
					int num2 = DNConvertor.IndexOfUnescapedChar(distinguishedName, i, ',');
					if (num2 == -1)
					{
						num2 = distinguishedName.Length;
					}
					int num3 = distinguishedName.IndexOf('=', i, num2 - i);
					if (num3 < i + 1 || num3 >= num2 - 1)
					{
						return false;
					}
					num3++;
					string text;
					if (!AdName.TryConvertTo(AdName.ConvertOption.Format, distinguishedName, num3, num2 - num3, out text))
					{
						return false;
					}
					if (text != null)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(distinguishedName, 0, num3, distinguishedName.Length);
						}
						else
						{
							stringBuilder.Append(distinguishedName, num, num3 - num);
						}
						stringBuilder.Append(text);
						num = num2;
					}
					i = num2;
					i++;
					depth++;
				}
				if (stringBuilder != null)
				{
					if (num < distinguishedName.Length)
					{
						stringBuilder.Append(distinguishedName, num, distinguishedName.Length - num);
					}
					distinguishedName = stringBuilder.ToString();
				}
				depth--;
			}
			formattedDN = distinguishedName;
			return true;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000163A4 File Offset: 0x000145A4
		private static string FormatDN(string distinguishedName, bool validate, out int depth)
		{
			string result = null;
			if (ADObjectId.TryFormatDN(distinguishedName, validate, out result, out depth))
			{
				return result;
			}
			throw new FormatException(DirectoryStrings.InvalidDNFormat(distinguishedName));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000163D4 File Offset: 0x000145D4
		private static bool TryParseBytes(byte[] bytes, int offset, int length, Encoding encoding, out string formattedDN, out Guid partitionGuid, out string partitionFQDN, out Guid objectGuid, out int depth)
		{
			formattedDN = string.Empty;
			objectGuid = Guid.Empty;
			partitionGuid = Guid.Empty;
			partitionFQDN = null;
			depth = -1;
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "offset cannot be negative");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "length cannot be negative");
			}
			if (bytes.Length < offset + length)
			{
				throw new ArgumentOutOfRangeException("bytes", "not enough bytes for provided offset/length");
			}
			if (length < 16)
			{
				return false;
			}
			objectGuid = ExBitConverter.ReadGuid(bytes, offset);
			int num = 16 + offset;
			if (length == num - offset)
			{
				return true;
			}
			if (bytes[num] != 1)
			{
				int count = length - (num - offset);
				string text = string.Empty;
				try
				{
					text = encoding.GetString(bytes, num, count);
				}
				catch (DecoderFallbackException)
				{
					return false;
				}
				catch (ArgumentException)
				{
					return false;
				}
				string text2;
				int num2;
				if (!ADObjectId.TryFormatDN(text, false, out text2, out num2))
				{
					return false;
				}
				formattedDN = text2;
				depth = num2;
				return true;
			}
			num++;
			partitionFQDN = encoding.GetString(bytes, num, length - num - offset);
			return true;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001650C File Offset: 0x0001470C
		private string AppendHierarchicalIdentity(string identity, string hierarchicalId)
		{
			if (string.IsNullOrEmpty(identity))
			{
				return hierarchicalId;
			}
			if (!string.IsNullOrEmpty(hierarchicalId) && !hierarchicalId.EndsWith("\\") && !identity.StartsWith("\\"))
			{
				return hierarchicalId + "\\" + identity;
			}
			if (!string.IsNullOrEmpty(hierarchicalId))
			{
				return hierarchicalId + identity;
			}
			return identity;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00016564 File Offset: 0x00014764
		internal static bool TryCreateFromBytes(byte[] bytes, int offset, int length, Encoding encoding, out ADObjectId objectId)
		{
			objectId = null;
			string text;
			Guid guid;
			string partitionFQDN;
			Guid guid2;
			int num;
			if (!ADObjectId.TryParseBytes(bytes, offset, length, encoding, out text, out guid, out partitionFQDN, out guid2, out num))
			{
				return false;
			}
			objectId = new ADObjectId(text, guid, partitionFQDN, guid2, false);
			objectId.depth = num;
			return true;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000165A4 File Offset: 0x000147A4
		internal ADObjectId(byte[] bytes, Encoding encoding, int offset)
		{
			this.depth = -1;
			base..ctor();
			if (!ADObjectId.TryParseBytes(bytes, offset, bytes.Length - offset, encoding, out this.distinguishedName, out this.partitionGuid, out this.partitionFqdn, out this.objectGuid, out this.depth))
			{
				throw new FormatException("Unable to interpret provided bytes");
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000165F8 File Offset: 0x000147F8
		private string BuildHierarchicalIdentity(string partialDn)
		{
			if (string.IsNullOrEmpty(partialDn))
			{
				return partialDn;
			}
			string[] array = DNConvertor.SplitDistinguishedName(partialDn, ',');
			StringBuilder stringBuilder = new StringBuilder(partialDn.Length);
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Insert(0, AdName.Unescape(array[i].Substring(array[i].IndexOf('=') + 1)));
				if (i + 1 < array.Length)
				{
					stringBuilder.Insert(0, '\\');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001666C File Offset: 0x0001486C
		private bool IsTenantId(ADObjectId id, OrganizationId orgId)
		{
			return id != null && !(orgId == null) && !orgId.Equals(OrganizationId.ForestWideOrgId) && (id.IsDescendantOf(orgId.OrganizationalUnit) || id.IsDescendantOf(orgId.ConfigurationUnit.Parent) || id.DistinguishedName.ToLower().Contains(",cn=configurationunits,"));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000166D0 File Offset: 0x000148D0
		private string ResolveConfigNCToString(string originalDN)
		{
			StringBuilder stringBuilder = new StringBuilder(originalDN.Length);
			string[] array = DNConvertor.SplitDistinguishedName(originalDN, ',');
			string text = originalDN.ToLower();
			stringBuilder.Append((array.Length > 0) ? AdName.Unescape(array[0].Substring(array[0].IndexOf('=') + 1)) : originalDN);
			if (array.Length < 5)
			{
				return stringBuilder.ToString();
			}
			string text2 = AdName.Unescape(array[1].Substring(array[1].IndexOf('=') + 1));
			string text3 = AdName.Unescape(array[2].Substring(array[2].IndexOf('=') + 1));
			string value = AdName.Unescape(array[3].Substring(array[3].IndexOf('=') + 1));
			string value2 = AdName.Unescape(array[4].Substring(array[4].IndexOf('=') + 1));
			if (text.Contains("cn=servers,") && text.Contains("cn=administrative groups,") && !text.Contains("cn=address lists container"))
			{
				if ("informationstore".Equals(text2, StringComparison.OrdinalIgnoreCase) && "servers".Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder.Insert(0, "\\");
					stringBuilder.Insert(0, text3);
				}
				else if ("informationstore".Equals(text3, StringComparison.OrdinalIgnoreCase) && "servers".Equals(value2, StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder.Insert(0, "\\");
					stringBuilder.Insert(0, text2);
					stringBuilder.Insert(0, "\\");
					stringBuilder.Insert(0, value);
				}
				else if ("protocols".Equals(text3, StringComparison.OrdinalIgnoreCase) && "servers".Equals(value2, StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder.Insert(0, "\\");
					stringBuilder.Insert(0, value);
				}
			}
			if ("databases".Equals(text3, StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, text2);
			}
			else if (text.Contains("cn=address lists container"))
			{
				int addressListContainerIndex = ADObjectId.GetAddressListContainerIndex(array);
				if (0 < addressListContainerIndex)
				{
					StringBuilder stringBuilder2 = new StringBuilder("\\");
					for (int i = addressListContainerIndex - 2; i >= 0; i--)
					{
						stringBuilder2.Append(AdName.Unescape(array[i].Substring(array[i].IndexOf('=') + 1)));
						if (i != 0)
						{
							stringBuilder2.Append("\\");
						}
					}
					return stringBuilder2.ToString();
				}
				return originalDN;
			}
			else if ("message classifications".Equals(text3, StringComparison.OrdinalIgnoreCase) && "transport settings".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, text2);
			}
			else if ("dsn customization".Equals(value, StringComparison.OrdinalIgnoreCase) && "transport settings".Equals(value2, StringComparison.OrdinalIgnoreCase))
			{
				string value3 = ADObjectId.ResolveLanguageCode2ISOFormat(text3);
				if (string.IsNullOrEmpty(value3))
				{
					return originalDN;
				}
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, text2);
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, value3);
			}
			else if ("dsn customization".Equals(text3, StringComparison.OrdinalIgnoreCase) && "transport settings".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				string value4 = ADObjectId.ResolveLanguageCode2ISOFormat(text2);
				if (string.IsNullOrEmpty(value4))
				{
					return originalDN;
				}
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, value4);
			}
			else if ("Display-Templates".Equals(text3, StringComparison.OrdinalIgnoreCase))
			{
				Culture culture = null;
				if (!Culture.TryGetCulture(int.Parse(text2, NumberStyles.HexNumber), out culture))
				{
					return originalDN;
				}
				stringBuilder = new StringBuilder(originalDN.Length);
				stringBuilder.Insert(0, this.InternalGetDetailsTemplateName(array[0]));
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, culture.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00016A76 File Offset: 0x00014C76
		private bool IsIdUnderConfigurationContainer(string dnLower)
		{
			return dnLower.Contains("cn=microsoft exchange,cn=services,cn=configuration,dc=") || dnLower.Contains("cn=microsoft exchange,cn=services,cn=configuration,ou=") || dnLower.Contains(",cn=configurationunits,dc=") || this.InternalIsUnderAdamConfigurationContainer(dnLower);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00016AA8 File Offset: 0x00014CA8
		internal static ADObjectId ParseExtendedDN(string extendedDN)
		{
			return ADObjectId.ParseExtendedDN(extendedDN, Guid.Empty, null);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00016AB6 File Offset: 0x00014CB6
		internal static ADObjectId ParseExtendedDN(string extendedDN, OrganizationId orgId)
		{
			return ADObjectId.ParseExtendedDN(extendedDN, Guid.Empty, orgId);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016AC4 File Offset: 0x00014CC4
		internal static ADObjectId ParseExtendedDN(string extendedDN, Guid partitionGuid, OrganizationId orgId)
		{
			string sid = null;
			Guid guid = Guid.Empty;
			int num = -1;
			if (string.IsNullOrEmpty(extendedDN) || !extendedDN.StartsWith("<GUID=", StringComparison.Ordinal))
			{
				guid = Guid.Empty;
			}
			else
			{
				guid = GuidFactory.Parse(extendedDN, "<GUID=".Length);
				int num2 = "<GUID=".Length + "098f2470-bae0-11cd-b579-08002b30bfeb".Length + 2;
				if (extendedDN.Length > num2)
				{
					int num3 = num2;
					if (extendedDN[num2] == '<' && extendedDN.Substring(num2, "<SID=".Length) == "<SID=")
					{
						int num4 = num2 + "<SID=".Length;
						int num5 = extendedDN.IndexOf(';', num2) - 2;
						sid = extendedDN.Substring(num4, num5 - num4 + 1);
						num3 = extendedDN.IndexOf(';', num2) + 1;
					}
					if (extendedDN.Length > num3)
					{
						extendedDN = extendedDN.Substring(num3);
					}
				}
				else
				{
					extendedDN = string.Empty;
				}
			}
			string dn;
			if (string.IsNullOrEmpty(extendedDN))
			{
				dn = string.Empty;
			}
			else
			{
				dn = ADObjectId.FormatDN(extendedDN, false, out num);
			}
			ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "ADObjectId.ParseExtendedDN - Initialized using DN {0}", extendedDN);
			return new ADObjectId(dn, partitionGuid, null, guid, num, orgId, sid);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016BF0 File Offset: 0x00014DF0
		internal static SecurityIdentifier GetSecurityIdentifier(ADObjectId instance)
		{
			if (instance.securityIdentifierString == null)
			{
				return null;
			}
			return new SecurityIdentifier(instance.securityIdentifierString);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00016C08 File Offset: 0x00014E08
		internal static bool TryParseDnOrGuid(string input, out ADObjectId instance)
		{
			instance = null;
			if (input == null || input.Length < 3)
			{
				return false;
			}
			string text = null;
			int num = 0;
			if (ADObjectId.TryFormatDN(input, true, out text, out num))
			{
				instance = new ADObjectId(text, Guid.Empty, null, Guid.Empty, false);
				return true;
			}
			Guid guid;
			if (GuidHelper.TryParseGuid(input, out guid))
			{
				instance = new ADObjectId(string.Empty, Guid.Empty, null, guid, false);
				return true;
			}
			return false;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00016C6E File Offset: 0x00014E6E
		public ADObjectId GetChildId(string unescapedCommonName)
		{
			return this.GetChildId("CN", unescapedCommonName);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00016C7C File Offset: 0x00014E7C
		public ADObjectId GetChildId(string prefix, string unescapedCommonName)
		{
			if (string.IsNullOrEmpty(unescapedCommonName))
			{
				throw new ArgumentNullException(DirectoryStrings.CannotGetChild(" unescapedCommonName is null/empty").ToString());
			}
			if (string.IsNullOrEmpty(prefix))
			{
				throw new ArgumentNullException(DirectoryStrings.CannotGetChild("prefix is null/empty").ToString());
			}
			if (this.Rdn == null)
			{
				throw new ArgumentNullException(DirectoryStrings.CannotGetChild("this is a GUID based ADObjectId").ToString());
			}
			AdName adName = new AdName(prefix, unescapedCommonName);
			return new ADObjectId(adName.ToString() + "," + this.DistinguishedName, Guid.Empty, this.partitionFqdn, Guid.Empty, false)
			{
				currentRdn = adName,
				parent = this,
				domainInfo = this.domainInfo
			};
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00016D54 File Offset: 0x00014F54
		public ADObjectId GetDescendantId(string unescapedChildName, string unescapedGrandChildName, params string[] unescapedDescendants)
		{
			ADObjectId childId = this.GetChildId(unescapedChildName).GetChildId(unescapedGrandChildName);
			foreach (string unescapedCommonName in unescapedDescendants)
			{
				childId = childId.GetChildId(unescapedCommonName);
			}
			return childId;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00016D8C File Offset: 0x00014F8C
		public ADObjectId GetDescendantId(ADObjectId relativePath)
		{
			if (string.IsNullOrEmpty(this.DistinguishedName))
			{
				return null;
			}
			if (relativePath == null)
			{
				return this;
			}
			if (string.IsNullOrEmpty(relativePath.DistinguishedName))
			{
				return null;
			}
			return new ADObjectId(relativePath.DistinguishedName + "," + this.DistinguishedName, Guid.Empty, this.partitionFqdn, Guid.Empty, false)
			{
				domainInfo = this.domainInfo
			};
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00016DF8 File Offset: 0x00014FF8
		public AdName GetAdNameAtDepth(int depth)
		{
			if (string.IsNullOrEmpty(this.DistinguishedName))
			{
				throw new InvalidOperationException(DirectoryStrings.CannotGetDnFromGuid(this.objectGuid));
			}
			if (depth < 0 || depth > this.Depth)
			{
				throw new InvalidOperationException(DirectoryStrings.CannotGetDnAtDepth(this.DistinguishedName, depth));
			}
			ADObjectId adobjectId = this;
			for (int i = 0; i < this.Depth - depth; i++)
			{
				adobjectId = adobjectId.Parent;
			}
			return adobjectId.Rdn;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00016E70 File Offset: 0x00015070
		public ADObjectId DescendantDN(int depth)
		{
			if (depth < 0)
			{
				throw new InvalidOperationException(DirectoryStrings.CannotGetDnAtDepth(this.DistinguishedName, depth));
			}
			int num;
			if (this.DomainId == null)
			{
				num = this.Depth;
			}
			else
			{
				num = this.Depth - this.DomainId.Depth;
			}
			num -= depth;
			if (num > this.Depth || num < 0)
			{
				throw new InvalidOperationException(DirectoryStrings.CannotGetDnAtDepth(this.DistinguishedName, num));
			}
			ADObjectId adobjectId = this;
			for (int i = 0; i < num; i++)
			{
				adobjectId = adobjectId.Parent;
			}
			return adobjectId;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00016EFC File Offset: 0x000150FC
		public ADObjectId AncestorDN(int generation)
		{
			ADObjectId adobjectId = this;
			int num = generation;
			while (adobjectId != null && num > 0)
			{
				num--;
				adobjectId = adobjectId.Parent;
			}
			if (adobjectId == null || num != 0)
			{
				throw new InvalidOperationException(DirectoryStrings.CannotGetDnAtDepth(this.DistinguishedName, generation));
			}
			return adobjectId;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00016F3F File Offset: 0x0001513F
		public bool IsDescendantOf(ADObjectId rootId)
		{
			return rootId != null && !string.IsNullOrEmpty(rootId.DistinguishedName) && !string.IsNullOrEmpty(this.DistinguishedName) && this.DistinguishedName.EndsWith(rootId.DistinguishedName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00016F74 File Offset: 0x00015174
		public ADObjectId GetFirstGenerationDecendantOf(ADObjectId rootId)
		{
			if (rootId == null)
			{
				throw new ArgumentNullException("rootId");
			}
			ADObjectId adobjectId = this;
			int i = 0;
			while (i < 256)
			{
				ADObjectId adobjectId2 = adobjectId.Parent;
				i++;
				if (adobjectId2 == null)
				{
					break;
				}
				if (adobjectId2.DistinguishedName.Equals(rootId.DistinguishedName, StringComparison.OrdinalIgnoreCase))
				{
					return adobjectId;
				}
				adobjectId = adobjectId2;
			}
			throw new InvalidOperationException(DirectoryStrings.CannotGetDnAtDepth(this.DistinguishedName, i));
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00016FDC File Offset: 0x000151DC
		public bool IsDeleted
		{
			get
			{
				AdName rdn = this.Rdn;
				return !(rdn == null) && rdn.UnescapedName.Contains("\nDEL:");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001700C File Offset: 0x0001520C
		public AdName Rdn
		{
			get
			{
				if (null == this.currentRdn && !string.IsNullOrEmpty(this.DistinguishedName))
				{
					int num = DNConvertor.IndexOfUnescapedChar(this.DistinguishedName, 0, ',');
					if (num == -1)
					{
						this.currentRdn = AdName.ParseRdn(this.DistinguishedName, 0, this.DistinguishedName.Length, true);
					}
					else
					{
						this.currentRdn = AdName.ParseRdn(this.DistinguishedName, 0, num, true);
					}
				}
				return this.currentRdn;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00017084 File Offset: 0x00015284
		public ADObjectId Parent
		{
			get
			{
				if (this.parent == null && this.Depth > 0)
				{
					int num = DNConvertor.IndexOfUnescapedChar(this.DistinguishedName, 0, ',');
					this.parent = new ADObjectId(this.DistinguishedName.Substring(num + 1), Guid.Empty, null, Guid.Empty, false);
					if (this.depth != -1)
					{
						this.parent.depth = this.depth - 1;
					}
				}
				return this.parent;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000170FC File Offset: 0x000152FC
		public int Depth
		{
			get
			{
				if (this.depth == -1)
				{
					int num = 0;
					if (!string.IsNullOrEmpty(this.DistinguishedName))
					{
						if (this.parent != null)
						{
							num = this.parent.Depth + 1;
						}
						else
						{
							int num2 = -1;
							while ((num2 = DNConvertor.IndexOfUnescapedChar(this.DistinguishedName, num2 + 1, ',')) != -1)
							{
								num++;
							}
						}
					}
					this.depth = num;
				}
				return this.depth;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00017165 File Offset: 0x00015365
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00017170 File Offset: 0x00015370
		private bool InitializeDomainInfo()
		{
			if (this.domainInfo != null)
			{
				return !this.domainInfo.IsRelativeDn;
			}
			if (string.IsNullOrEmpty(this.DistinguishedName))
			{
				this.domainInfo = ADObjectId.DomainInfo.NullDomainFalseRelative;
				return true;
			}
			ADObjectId adobjectId = this;
			while (adobjectId.parent != null)
			{
				if (adobjectId.DistinguishedName.StartsWith("DC=", StringComparison.OrdinalIgnoreCase))
				{
					this.domainInfo = ADObjectId.DomainInfo.GetDomainInfo(adobjectId, false);
					return true;
				}
				adobjectId = adobjectId.parent;
			}
			int num = adobjectId.DistinguishedName.IndexOf("DC=", 0, adobjectId.DistinguishedName.Length, StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				num = adobjectId.DistinguishedName.LastIndexOf("CN={", StringComparison.OrdinalIgnoreCase);
				if (num == -1)
				{
					num = adobjectId.DistinguishedName.LastIndexOf("OU=MSExchangeGateway", StringComparison.OrdinalIgnoreCase);
					if (num == -1)
					{
						this.domainInfo = ADObjectId.DomainInfo.NullDomainTrueRelative;
						return false;
					}
				}
			}
			this.domainInfo = ADObjectId.DomainInfo.GetDomainInfo(new ADObjectId(adobjectId.DistinguishedName.Substring(num), Guid.Empty, null, Guid.Empty, false), false);
			return true;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00017268 File Offset: 0x00015468
		public bool IsRelativeDn
		{
			get
			{
				if (this.domainInfo == null)
				{
					this.InitializeDomainInfo();
				}
				return this.domainInfo.IsRelativeDn;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00017284 File Offset: 0x00015484
		public ADObjectId DomainId
		{
			get
			{
				if (this.domainInfo == null && !this.InitializeDomainInfo())
				{
					throw new InvalidOperationException(DirectoryStrings.CannotGetDomainFromDN(this.DistinguishedName));
				}
				return this.domainInfo.DomainId;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000172B7 File Offset: 0x000154B7
		public Guid PartitionGuid
		{
			get
			{
				return this.partitionGuid;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000172C0 File Offset: 0x000154C0
		public string PartitionFQDN
		{
			get
			{
				if (this.partitionFqdn == null)
				{
					if (this.domainInfo == null)
					{
						this.InitializeDomainInfo();
					}
					this.partitionFqdn = ((this.domainInfo.DomainId != null) ? this.domainInfo.DomainId.ToString() : string.Empty);
				}
				return this.partitionFqdn;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00017314 File Offset: 0x00015514
		public Guid ObjectGuid
		{
			get
			{
				return this.objectGuid;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001731C File Offset: 0x0001551C
		public string Name
		{
			get
			{
				if (!(this.Rdn != null))
				{
					return string.Empty;
				}
				return this.Rdn.UnescapedName;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00017340 File Offset: 0x00015540
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ADObjectId adobjectId = obj as ADObjectId;
			if (adobjectId != null)
			{
				return this.Equals(adobjectId);
			}
			string text = obj as string;
			return text != null && this.Equals(text);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017378 File Offset: 0x00015578
		public bool Equals(string objString)
		{
			if (string.IsNullOrEmpty(objString))
			{
				return false;
			}
			ADObjectId y = null;
			if (ADObjectId.TryParseDnOrGuid(objString, out y))
			{
				return ADObjectId.Equals(this, y);
			}
			return this.ToString().Equals(objString);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000173B0 File Offset: 0x000155B0
		public bool Equals(ADObjectId id)
		{
			if (id == null)
			{
				return false;
			}
			bool result;
			if (!this.ObjectGuid.Equals(Guid.Empty) && !id.ObjectGuid.Equals(Guid.Empty))
			{
				result = this.ObjectGuid.Equals(id.ObjectGuid);
			}
			else if (this.DistinguishedName == null)
			{
				result = (id.DistinguishedName == null);
			}
			else
			{
				result = this.DistinguishedName.Equals(id.DistinguishedName, StringComparison.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00017430 File Offset: 0x00015630
		public override int GetHashCode()
		{
			int hashCode = this.ObjectGuid.GetHashCode();
			if (this.ObjectGuid.Equals(Guid.Empty) && !string.IsNullOrEmpty(this.DistinguishedName))
			{
				hashCode = this.DistinguishedName.ToLowerInvariant().GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017488 File Offset: 0x00015688
		internal static bool IsNullOrEmpty(ADObjectId adobjectId)
		{
			return adobjectId == null || (string.IsNullOrEmpty(adobjectId.DistinguishedName) && adobjectId.ObjectGuid == Guid.Empty);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000174AE File Offset: 0x000156AE
		internal PartitionId GetPartitionId()
		{
			return new PartitionId(this);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000174B6 File Offset: 0x000156B6
		public string ToCanonicalName()
		{
			if (!string.IsNullOrEmpty(this.DistinguishedName))
			{
				return NativeHelpers.CanonicalNameFromDistinguishedName(this.DistinguishedName);
			}
			return string.Empty;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000174D8 File Offset: 0x000156D8
		public int GetByteCount(Encoding encoding)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			int num = 16;
			if (!string.IsNullOrEmpty(this.DistinguishedName))
			{
				num += encoding.GetByteCount(this.DistinguishedName);
			}
			else if (!string.IsNullOrEmpty(this.PartitionFQDN) && ConfigBase<AdDriverConfigSchema>.GetConfig<int>("SoftLinkFormatVersion") == 2)
			{
				num += encoding.GetByteCount(this.PartitionFQDN) + 1;
			}
			return num;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00017540 File Offset: 0x00015740
		internal void GetBytes(Encoding encoding, byte[] byteArray, int offset)
		{
			offset += ExBitConverter.Write(this.objectGuid, byteArray, offset);
			if (!string.IsNullOrEmpty(this.DistinguishedName))
			{
				encoding.GetBytes(this.DistinguishedName, 0, this.DistinguishedName.Length, byteArray, offset);
				return;
			}
			if (!string.IsNullOrEmpty(this.PartitionFQDN) && ConfigBase<AdDriverConfigSchema>.GetConfig<int>("SoftLinkFormatVersion") == 2)
			{
				byteArray[offset++] = 1;
				encoding.GetBytes(this.PartitionFQDN, 0, this.PartitionFQDN.Length, byteArray, offset);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000175C8 File Offset: 0x000157C8
		public byte[] ToSoftLinkValue()
		{
			byte b = (byte)ConfigBase<AdDriverConfigSchema>.GetConfig<int>("SoftLinkFormatVersion");
			if (this.objectGuid == Guid.Empty || (b == 2 && string.IsNullOrEmpty(this.PartitionFQDN)))
			{
				throw new FormatException(DirectoryStrings.InvalidIdFormat(this.ToExtendedDN()));
			}
			if (b == 1)
			{
				Guid resourcePartitionGuid = this.partitionGuid;
				if (this.partitionGuid == Guid.Empty)
				{
					resourcePartitionGuid = ADObjectId.ResourcePartitionGuid;
				}
				byte[] array = new byte[33];
				array[0] = b;
				byte[] sourceArray = this.objectGuid.ToByteArray();
				Array.Copy(sourceArray, 0, array, 1, 16);
				sourceArray = resourcePartitionGuid.ToByteArray();
				Array.Copy(sourceArray, 0, array, 17, 16);
				return array;
			}
			if (b == 2)
			{
				int num = 16 + Encoding.UTF8.GetByteCount(this.PartitionFQDN) + 1;
				byte[] array2 = new byte[num];
				array2[0] = b;
				byte[] array3 = this.objectGuid.ToByteArray();
				Array.Copy(array3, 0, array2, 1, 16);
				array3 = Encoding.UTF8.GetBytes(this.PartitionFQDN);
				Array.Copy(array3, 0, array2, 17, array3.Length);
				return array2;
			}
			throw new FormatException(string.Format("Invalid soft link format version: {0}", b));
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000176F4 File Offset: 0x000158F4
		internal byte[] ToSoftLinkLdapQueryValue(byte prefix)
		{
			if (this.objectGuid == Guid.Empty)
			{
				throw new FormatException(DirectoryStrings.InvalidIdFormat(this.ToExtendedDN()));
			}
			if (prefix != 1 && prefix != 2)
			{
				throw new ArgumentException(string.Format("Invalid soft link format version: {0}", prefix), "prefix");
			}
			byte[] array = new byte[17];
			array[0] = prefix;
			byte[] sourceArray = this.objectGuid.ToByteArray();
			Array.Copy(sourceArray, 0, array, 1, 16);
			return array;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00017770 File Offset: 0x00015970
		internal static ADObjectId FromSoftLinkValue(byte[] input, ADObjectId objectId, OrganizationId executingUserOrgId)
		{
			Guid empty = Guid.Empty;
			Guid empty2 = Guid.Empty;
			byte b = input[0];
			if ((b != 1 && b != 2) || (b == 1 && input.Length != 33) || (b == 2 && input.Length < 38))
			{
				throw new FormatException(DirectoryStrings.InvalidIdFormat(input.ToString()));
			}
			byte[] array = new byte[16];
			Array.Copy(input, 1, array, 0, 16);
			empty = new Guid(array);
			string partitionFQDN;
			if (b == 1)
			{
				Array.Copy(input, 17, array, 0, 16);
				empty2 = new Guid(array);
				if (objectId != null)
				{
					partitionFQDN = ADResourceForestLocator.InferResourceForestFromAccountForestIdentity(objectId);
				}
				else
				{
					partitionFQDN = PartitionId.LocalForest.ForestFQDN;
				}
			}
			else
			{
				partitionFQDN = Encoding.UTF8.GetString(input, 17, input.Length - 17);
			}
			return new ADObjectId(string.Empty, empty2, partitionFQDN, empty, 0, executingUserOrgId, null);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00017838 File Offset: 0x00015A38
		private string ResolveDomainNCToString(string originalDn)
		{
			try
			{
				return NativeHelpers.CanonicalNameFromDistinguishedName(originalDn);
			}
			catch (NameConversionException)
			{
			}
			return originalDn;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00017864 File Offset: 0x00015A64
		private string InternalGetDetailsTemplateName(string detailsTemplateId)
		{
			return DetailsTemplate.TranslateTemplateIDToName(AdName.Unescape(detailsTemplateId.Substring(detailsTemplateId.IndexOf('=') + 1)));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00017880 File Offset: 0x00015A80
		private bool InternalIsUnderAdamConfigurationContainer(string dnLower)
		{
			bool result = false;
			if (ADSession.IsBoundToAdam && dnLower.LastIndexOf("{") > 0)
			{
				int num = dnLower.LastIndexOf("{") - "cn=microsoft exchange,cn=services,cn=configuration,cn=".Length;
				if (num > 0)
				{
					result = dnLower.Substring(num).StartsWith("cn=microsoft exchange,cn=services,cn=configuration,cn={");
				}
			}
			return result;
		}

		// Token: 0x04000141 RID: 321
		internal const int MaxRdnLength = 64;

		// Token: 0x04000142 RID: 322
		private const string GuidDNPrefix = "<GUID=";

		// Token: 0x04000143 RID: 323
		private const string SIDPrefix = "<SID=";

		// Token: 0x04000144 RID: 324
		private const string GuidStringRepresentation = "098f2470-bae0-11cd-b579-08002b30bfeb";

		// Token: 0x04000145 RID: 325
		private const int BytesForGuid = 16;

		// Token: 0x04000146 RID: 326
		private const byte SoftLinkValueLengthV1 = 33;

		// Token: 0x04000147 RID: 327
		private const byte MinSoftLinkValueLengthV2 = 38;

		// Token: 0x04000148 RID: 328
		private readonly Guid partitionGuid;

		// Token: 0x04000149 RID: 329
		private string partitionFqdn;

		// Token: 0x0400014A RID: 330
		private Guid objectGuid;

		// Token: 0x0400014B RID: 331
		private string distinguishedName;

		// Token: 0x0400014C RID: 332
		private string securityIdentifierString;

		// Token: 0x0400014D RID: 333
		private int depth;

		// Token: 0x0400014E RID: 334
		private string toStringVal;

		// Token: 0x0400014F RID: 335
		private OrganizationId executingUserOrganization;

		// Token: 0x04000150 RID: 336
		[NonSerialized]
		private ADObjectId parent;

		// Token: 0x04000151 RID: 337
		[NonSerialized]
		private ADObjectId.DomainInfo domainInfo;

		// Token: 0x04000152 RID: 338
		[NonSerialized]
		private AdName currentRdn;

		// Token: 0x04000153 RID: 339
		[NonSerialized]
		private OrganizationId orgHierarchyToIgnore;

		// Token: 0x04000154 RID: 340
		public static Guid ResourcePartitionGuid = new Guid("59ce2f71-eaa2-4ddf-a4fa-f25069d0b324");

		// Token: 0x04000155 RID: 341
		private static readonly Regex keycharRdnRegex = new Regex("^[a-zA-Z][a-zA-Z0-9\\-]*=", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000156 RID: 342
		private static readonly Regex oidRdnRegex = new Regex("^[0-9]+(\\.[0-9]+)*=", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0200004D RID: 77
		private class DomainInfo
		{
			// Token: 0x06000410 RID: 1040 RVA: 0x0001790B File Offset: 0x00015B0B
			private DomainInfo(ADObjectId domainId, bool isRelativeDn)
			{
				this.domainId = domainId;
				this.isRelativeDn = isRelativeDn;
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x00017921 File Offset: 0x00015B21
			public ADObjectId DomainId
			{
				get
				{
					return this.domainId;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x00017929 File Offset: 0x00015B29
			public bool IsRelativeDn
			{
				get
				{
					return this.isRelativeDn;
				}
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x00017934 File Offset: 0x00015B34
			public static ADObjectId.DomainInfo GetDomainInfo(ADObjectId domainId, bool isRelativeDn)
			{
				ADObjectId.DomainInfo domainInfo;
				if (ADObjectId.DomainInfo.domains != null)
				{
					domainInfo = ADObjectId.DomainInfo.GetDomainInfoFromList(domainId, isRelativeDn);
					if (domainInfo != null)
					{
						return domainInfo;
					}
					lock (ADObjectId.DomainInfo.domainLock)
					{
						domainInfo = ADObjectId.DomainInfo.GetDomainInfoFromList(domainId, isRelativeDn);
						if (domainInfo == null)
						{
							domainInfo = new ADObjectId.DomainInfo(domainId, isRelativeDn);
							ADObjectId.DomainInfo.domains = new List<ADObjectId.DomainInfo>(ADObjectId.DomainInfo.domains)
							{
								domainInfo
							};
						}
						return domainInfo;
					}
				}
				domainInfo = new ADObjectId.DomainInfo(domainId, isRelativeDn);
				return domainInfo;
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x000179B4 File Offset: 0x00015BB4
			private static ADObjectId.DomainInfo GetDomainInfoFromList(ADObjectId domainId, bool isRelativeDn)
			{
				foreach (ADObjectId.DomainInfo domainInfo in ADObjectId.DomainInfo.domains)
				{
					if (domainInfo.isRelativeDn.Equals(isRelativeDn) && ADObjectIdEqualityComparer.Instance.Equals(domainInfo.domainId, domainId))
					{
						return domainInfo;
					}
				}
				return null;
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x00017A28 File Offset: 0x00015C28
			static DomainInfo()
			{
				if (Globals.IsDatacenter)
				{
					ADObjectId.DomainInfo.domains = new List<ADObjectId.DomainInfo>();
				}
			}

			// Token: 0x04000157 RID: 343
			private static List<ADObjectId.DomainInfo> domains;

			// Token: 0x04000158 RID: 344
			private static object domainLock = new object();

			// Token: 0x04000159 RID: 345
			public static ADObjectId.DomainInfo NullDomainTrueRelative = new ADObjectId.DomainInfo(null, true);

			// Token: 0x0400015A RID: 346
			public static ADObjectId.DomainInfo NullDomainFalseRelative = new ADObjectId.DomainInfo(null, false);

			// Token: 0x0400015B RID: 347
			private ADObjectId domainId;

			// Token: 0x0400015C RID: 348
			private bool isRelativeDn;
		}
	}
}
