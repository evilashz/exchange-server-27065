using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001A3 RID: 419
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecipientInfoCacheEntry : IComparable<RecipientInfoCacheEntry>
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x00040CD4 File Offset: 0x0003EED4
		public RecipientInfoCacheEntry(string displayName, string smtpAddress, string routingAddress, string alias, string routingType, AddressOrigin addressOrigin, int recipientFlags, string itemId, EmailAddressIndex emailAddressIndex, string sipUri, string mobilePhoneNumber) : this(displayName, smtpAddress, routingAddress, alias, routingType, addressOrigin, recipientFlags, itemId, emailAddressIndex, sipUri, mobilePhoneNumber, 6, DateTime.UtcNow.Ticks, 0, 0, 0)
		{
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00040D0C File Offset: 0x0003EF0C
		private RecipientInfoCacheEntry(string displayName, string smtpAddress, string routingAddress, string alias, string routingType, AddressOrigin addressOrigin, int recipientFlags, string itemId, EmailAddressIndex emailAddressIndex, string sipUri, string mobilePhoneNumber, short usage, long dateTimeTicks, short sessionCount, short sessionFlag, int cacheEntryId)
		{
			EnumValidator.ThrowIfInvalid<AddressOrigin>(addressOrigin);
			EnumValidator.ThrowIfInvalid<EmailAddressIndex>(emailAddressIndex);
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.routingAddress = routingAddress;
			this.alias = alias;
			if (string.IsNullOrEmpty(routingType))
			{
				this.routingType = "SMTP";
			}
			else
			{
				this.routingType = routingType;
			}
			this.addressOrigin = addressOrigin;
			this.recipientFlags = recipientFlags;
			this.itemId = itemId;
			this.usage = usage;
			this.dateTimeTicks = dateTimeTicks;
			this.sessionCount = sessionCount;
			this.sessionFlag = sessionFlag;
			this.emailAddressIndex = emailAddressIndex;
			this.sipUri = sipUri;
			this.mobilePhoneNumber = mobilePhoneNumber;
			this.cacheEntryId = cacheEntryId;
			if (this.cacheEntryId == 0)
			{
				this.GetNewCacheEntryId();
				this.isDirty = false;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00040DD5 File Offset: 0x0003EFD5
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00040DDD File Offset: 0x0003EFDD
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.isDirty = true;
				this.displayName = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00040DED File Offset: 0x0003EFED
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00040DF5 File Offset: 0x0003EFF5
		public string RoutingAddress
		{
			get
			{
				return this.routingAddress;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00040DFD File Offset: 0x0003EFFD
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x00040E05 File Offset: 0x0003F005
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.isDirty = true;
				this.alias = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00040E15 File Offset: 0x0003F015
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x00040E1D File Offset: 0x0003F01D
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("RoutingType", "Routing Type cannot be empty or null");
				}
				this.isDirty = true;
				this.routingType = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00040E45 File Offset: 0x0003F045
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x00040E4D File Offset: 0x0003F04D
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.isDirty = true;
				this.itemId = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00040E5D File Offset: 0x0003F05D
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x00040E65 File Offset: 0x0003F065
		public EmailAddressIndex EmailAddressIndex
		{
			get
			{
				return this.emailAddressIndex;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<EmailAddressIndex>(value);
				this.isDirty = true;
				this.emailAddressIndex = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00040E7B File Offset: 0x0003F07B
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00040E83 File Offset: 0x0003F083
		public AddressOrigin AddressOrigin
		{
			get
			{
				return this.addressOrigin;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<AddressOrigin>(value);
				this.isDirty = true;
				this.addressOrigin = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00040E99 File Offset: 0x0003F099
		public int RecipientFlags
		{
			get
			{
				return this.recipientFlags;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00040EA1 File Offset: 0x0003F0A1
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x00040EA9 File Offset: 0x0003F0A9
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
			set
			{
				this.isDirty = true;
				this.sipUri = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00040EB9 File Offset: 0x0003F0B9
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x00040EC1 File Offset: 0x0003F0C1
		public string MobilePhoneNumber
		{
			get
			{
				return this.mobilePhoneNumber;
			}
			set
			{
				this.isDirty = true;
				this.mobilePhoneNumber = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00040ED1 File Offset: 0x0003F0D1
		public short Usage
		{
			get
			{
				return this.usage;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00040ED9 File Offset: 0x0003F0D9
		public long DateTimeTicks
		{
			get
			{
				return this.dateTimeTicks;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00040EE1 File Offset: 0x0003F0E1
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x00040EE9 File Offset: 0x0003F0E9
		public short SessionCount
		{
			get
			{
				return this.sessionCount;
			}
			set
			{
				this.isDirty = true;
				this.sessionCount = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00040EF9 File Offset: 0x0003F0F9
		public int CacheEntryId
		{
			get
			{
				return this.cacheEntryId;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00040F01 File Offset: 0x0003F101
		internal bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00040F0C File Offset: 0x0003F10C
		public void UpdateTimeStamp()
		{
			this.isDirty = true;
			this.dateTimeTicks = ExDateTime.UtcNow.UtcTicks;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00040F34 File Offset: 0x0003F134
		public void Decay()
		{
			this.isDirty = true;
			short num = -1;
			if (!this.IsSessionFlagSet())
			{
				this.IncrementSessionCount();
			}
			else
			{
				this.ResetSessionCount();
			}
			short num2 = this.SessionCount;
			if (num2 <= 12)
			{
				if (num2 <= 6)
				{
					if (num2 != 3 && num2 != 6)
					{
						goto IL_C0;
					}
				}
				else if (num2 != 9)
				{
					if (num2 != 12)
					{
						goto IL_C0;
					}
					num = this.ComputeDecay(0.25, 2);
					goto IL_D1;
				}
				num = 1;
				goto IL_D1;
			}
			if (num2 <= 23)
			{
				if (num2 == 17)
				{
					num = this.ComputeDecay(0.25, 3);
					goto IL_D1;
				}
				if (num2 == 23)
				{
					num = this.ComputeDecay(0.5, 4);
					goto IL_D1;
				}
			}
			else
			{
				if (num2 == 26)
				{
					num = this.ComputeDecay(0.75, 5);
					goto IL_D1;
				}
				if (num2 == 31)
				{
					num = 0;
					this.usage = 0;
					goto IL_D1;
				}
			}
			IL_C0:
			if (this.SessionCount > 31)
			{
				this.usage = 0;
			}
			IL_D1:
			if (num > 0)
			{
				this.usage -= num;
				if (this.usage < 0)
				{
					this.usage = 0;
				}
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00041035 File Offset: 0x0003F235
		public void IncrementUsage()
		{
			this.isDirty = true;
			this.usage += 1;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0004104D File Offset: 0x0003F24D
		public void IncrementSessionCount()
		{
			this.isDirty = true;
			this.sessionCount += 1;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00041065 File Offset: 0x0003F265
		public void ResetSessionCount()
		{
			this.isDirty = true;
			this.sessionCount = 0;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00041075 File Offset: 0x0003F275
		public void SetSessionFlag()
		{
			this.isDirty = true;
			this.sessionFlag = 1;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00041085 File Offset: 0x0003F285
		public void ClearSessionFlag()
		{
			this.isDirty = true;
			this.sessionFlag = 0;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00041095 File Offset: 0x0003F295
		public bool IsSessionFlagSet()
		{
			return this.sessionFlag == 1;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000410A0 File Offset: 0x0003F2A0
		public int CompareTo(RecipientInfoCacheEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			int num = this.Usage.CompareTo(entry.Usage);
			if (num == 0)
			{
				num = this.DateTimeTicks.CompareTo(entry.DateTimeTicks);
			}
			return num;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000410EC File Offset: 0x0003F2EC
		internal static RecipientInfoCacheEntry ParseEntry(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (!string.Equals(reader.Name, "entry", StringComparison.OrdinalIgnoreCase))
			{
				throw new CorruptDataException(ServerStrings.InvalidTagName("entry", reader.Name));
			}
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			AddressOrigin addressOrigin = AddressOrigin.Unknown;
			string text6 = string.Empty;
			short num = 0;
			long num2 = 0L;
			short num3 = 0;
			short num4 = 0;
			int num5 = 0;
			EmailAddressIndex emailAddressIndex = EmailAddressIndex.None;
			string text7 = string.Empty;
			int num6 = 0;
			string text8 = string.Empty;
			try
			{
				if (reader.HasValue)
				{
					throw new CorruptDataException(ServerStrings.ElementHasUnsupportedValue(reader.Name));
				}
				if (reader.HasAttributes)
				{
					int i = 0;
					while (i < reader.AttributeCount)
					{
						reader.MoveToAttribute(i);
						string name;
						if ((name = reader.Name) != null)
						{
							if (<PrivateImplementationDetails>{85358D61-4A90-46D7-A75A-D3426C8257C6}.$$method0x6000f59-1 == null)
							{
								<PrivateImplementationDetails>{85358D61-4A90-46D7-A75A-D3426C8257C6}.$$method0x6000f59-1 = new Dictionary<string, int>(16)
								{
									{
										"displayName",
										0
									},
									{
										"smtpAddr",
										1
									},
									{
										"routAddr",
										2
									},
									{
										"alias",
										3
									},
									{
										"routType",
										4
									},
									{
										"addrOrig",
										5
									},
									{
										"recipFlags",
										6
									},
									{
										"itemId",
										7
									},
									{
										"emailAddressIndex",
										8
									},
									{
										"sipUri",
										9
									},
									{
										"usage",
										10
									},
									{
										"dateTimeTicks",
										11
									},
									{
										"session",
										12
									},
									{
										"sessionFlag",
										13
									},
									{
										"cacheEntryId",
										14
									},
									{
										"mobilePhoneNumber",
										15
									}
								};
							}
							int num7;
							if (<PrivateImplementationDetails>{85358D61-4A90-46D7-A75A-D3426C8257C6}.$$method0x6000f59-1.TryGetValue(name, out num7))
							{
								switch (num7)
								{
								case 0:
									text = reader.Value;
									break;
								case 1:
									text2 = reader.Value;
									break;
								case 2:
									text3 = reader.Value;
									break;
								case 3:
									text4 = reader.Value;
									break;
								case 4:
									text5 = reader.Value;
									break;
								case 5:
									addressOrigin = (AddressOrigin)int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 6:
									num5 = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 7:
									text6 = reader.Value;
									break;
								case 8:
									emailAddressIndex = (EmailAddressIndex)int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 9:
									text7 = reader.Value;
									break;
								case 10:
									num = short.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 11:
									num2 = long.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 12:
									num3 = short.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 13:
									num4 = short.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 14:
									num6 = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
									break;
								case 15:
									text8 = reader.Value;
									break;
								default:
									goto IL_30F;
								}
								i++;
								continue;
							}
						}
						IL_30F:
						throw new CorruptDataException(ServerStrings.ValueNotRecognizedForAttribute(reader.Name));
					}
					if (!reader.Read())
					{
						throw new CorruptDataException(ServerStrings.ClosingTagExpectedNoneFound);
					}
					if (reader.NodeType != XmlNodeType.EndElement || !string.Equals("entry", reader.Name, StringComparison.OrdinalIgnoreCase))
					{
						throw new CorruptDataException(ServerStrings.ClosingTagExpected(reader.Name));
					}
				}
			}
			catch (FormatException innerException)
			{
				throw new CorruptDataException(ServerStrings.FailedToParseValue, innerException);
			}
			catch (OverflowException innerException2)
			{
				throw new CorruptDataException(ServerStrings.FailedToParseValue, innerException2);
			}
			if (num6 <= 0)
			{
				throw new CorruptDataException(ServerStrings.InvalidCacheEntryId(num6));
			}
			return new RecipientInfoCacheEntry(text, text2, text3, text4, text5, addressOrigin, num5, text6, emailAddressIndex, text7, text8, num, num2, num3, num4, num6);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000414F4 File Offset: 0x0003F6F4
		internal void Serialize(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			xmlWriter.WriteStartElement("entry");
			if (!string.IsNullOrEmpty(this.displayName))
			{
				xmlWriter.WriteAttributeString("displayName", this.displayName);
			}
			if (!string.IsNullOrEmpty(this.smtpAddress))
			{
				xmlWriter.WriteAttributeString("smtpAddr", this.smtpAddress);
			}
			if (!string.IsNullOrEmpty(this.routingAddress))
			{
				xmlWriter.WriteAttributeString("routAddr", this.routingAddress);
			}
			if (!string.IsNullOrEmpty(this.alias))
			{
				xmlWriter.WriteAttributeString("alias", this.alias);
			}
			if (!string.IsNullOrEmpty(this.routingType))
			{
				xmlWriter.WriteAttributeString("routType", this.routingType);
			}
			string localName = "addrOrig";
			int num = (int)this.addressOrigin;
			xmlWriter.WriteAttributeString(localName, num.ToString(CultureInfo.InvariantCulture));
			xmlWriter.WriteAttributeString("recipFlags", this.recipientFlags.ToString(CultureInfo.InvariantCulture));
			if (!string.IsNullOrEmpty(this.itemId))
			{
				xmlWriter.WriteAttributeString("itemId", this.itemId);
			}
			if (this.emailAddressIndex != EmailAddressIndex.None)
			{
				string localName2 = "emailAddressIndex";
				int num2 = (int)this.emailAddressIndex;
				xmlWriter.WriteAttributeString(localName2, num2.ToString(CultureInfo.InvariantCulture));
			}
			if (!string.IsNullOrEmpty(this.sipUri))
			{
				xmlWriter.WriteAttributeString("sipUri", this.sipUri);
			}
			if (this.usage >= 0)
			{
				xmlWriter.WriteAttributeString("usage", this.usage.ToString(CultureInfo.InvariantCulture));
			}
			if (this.dateTimeTicks >= 0L)
			{
				xmlWriter.WriteAttributeString("dateTimeTicks", this.dateTimeTicks.ToString(CultureInfo.InvariantCulture));
			}
			if (this.sessionCount >= 0)
			{
				xmlWriter.WriteAttributeString("session", this.sessionCount.ToString(CultureInfo.InvariantCulture));
			}
			if (this.sessionFlag >= 0)
			{
				xmlWriter.WriteAttributeString("sessionFlag", this.sessionFlag.ToString(CultureInfo.InvariantCulture));
			}
			if (this.cacheEntryId == 0)
			{
				this.GetNewCacheEntryId();
			}
			xmlWriter.WriteAttributeString("cacheEntryId", this.cacheEntryId.ToString(CultureInfo.InvariantCulture));
			if (!string.IsNullOrEmpty(this.mobilePhoneNumber))
			{
				xmlWriter.WriteAttributeString("mobilePhoneNumber", this.mobilePhoneNumber);
			}
			xmlWriter.WriteFullEndElement();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0004172C File Offset: 0x0003F92C
		internal void GetNewCacheEntryId()
		{
			this.isDirty = true;
			lock (RecipientInfoCacheEntry.randomGenerator)
			{
				int num = this.cacheEntryId;
				do
				{
					this.cacheEntryId = RecipientInfoCacheEntry.randomGenerator.Next();
				}
				while (this.cacheEntryId == 0 || this.cacheEntryId == num);
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00041794 File Offset: 0x0003F994
		private short ComputeDecay(double percent, short setDecay)
		{
			short num = (short)Math.Round(percent * (double)this.usage);
			short result;
			if (num > setDecay)
			{
				result = num;
			}
			else
			{
				result = setDecay;
			}
			return result;
		}

		// Token: 0x04000874 RID: 2164
		private const string AutoCompleteEntryName = "entry";

		// Token: 0x04000875 RID: 2165
		private static readonly Random randomGenerator = new Random();

		// Token: 0x04000876 RID: 2166
		private string displayName;

		// Token: 0x04000877 RID: 2167
		private readonly string smtpAddress;

		// Token: 0x04000878 RID: 2168
		private readonly string routingAddress;

		// Token: 0x04000879 RID: 2169
		private string alias;

		// Token: 0x0400087A RID: 2170
		private string routingType;

		// Token: 0x0400087B RID: 2171
		private AddressOrigin addressOrigin;

		// Token: 0x0400087C RID: 2172
		private string itemId;

		// Token: 0x0400087D RID: 2173
		private short usage;

		// Token: 0x0400087E RID: 2174
		private long dateTimeTicks;

		// Token: 0x0400087F RID: 2175
		private short sessionCount;

		// Token: 0x04000880 RID: 2176
		private short sessionFlag;

		// Token: 0x04000881 RID: 2177
		private readonly int recipientFlags;

		// Token: 0x04000882 RID: 2178
		private EmailAddressIndex emailAddressIndex;

		// Token: 0x04000883 RID: 2179
		private int cacheEntryId;

		// Token: 0x04000884 RID: 2180
		private string sipUri;

		// Token: 0x04000885 RID: 2181
		private string mobilePhoneNumber;

		// Token: 0x04000886 RID: 2182
		private bool isDirty;
	}
}
