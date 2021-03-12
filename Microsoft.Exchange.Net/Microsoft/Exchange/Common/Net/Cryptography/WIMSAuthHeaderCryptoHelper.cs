using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006BC RID: 1724
	public class WIMSAuthHeaderCryptoHelper : CryptoHelper
	{
		// Token: 0x06002015 RID: 8213 RVA: 0x0003E370 File Offset: 0x0003C570
		public WIMSAuthHeaderCryptoHelper(CryptoHelper helper, string partnerID)
		{
			this.helper = helper;
			this.partnerID = partnerID;
			this.partnerIDHint = helper.GenerateHint(partnerID);
			this.customProperties = new Dictionary<string, string>();
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x0003E39E File Offset: 0x0003C59E
		public string HeaderName
		{
			get
			{
				return "X-Message-Routing";
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0003E3A5 File Offset: 0x0003C5A5
		public string PartnerID
		{
			get
			{
				return this.partnerID;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0003E3AD File Offset: 0x0003C5AD
		// (set) Token: 0x06002019 RID: 8217 RVA: 0x0003E3B5 File Offset: 0x0003C5B5
		public string Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0003E3BE File Offset: 0x0003C5BE
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x0003E3C6 File Offset: 0x0003C5C6
		public DateTime ExpiredAt
		{
			get
			{
				return this.expiredAt;
			}
			set
			{
				this.expiredAt = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0003E3CF File Offset: 0x0003C5CF
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x0003E3D7 File Offset: 0x0003C5D7
		public int AuthHeaderType
		{
			get
			{
				return this.authHeaderType;
			}
			set
			{
				this.authHeaderType = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		public IDictionary<string, string> CustomProperties
		{
			get
			{
				return this.customProperties;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
		public string DecryptedHeader
		{
			get
			{
				return this.ConvertPropertiesToString();
			}
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0003E3F0 File Offset: 0x0003C5F0
		public override string Encrypt(string inputText)
		{
			return this.helper.Encrypt(inputText);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0003E3FE File Offset: 0x0003C5FE
		public override string Decrypt(string inputText)
		{
			return this.helper.Decrypt(inputText);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0003E40C File Offset: 0x0003C60C
		public string EncryptWithHintFromProperties()
		{
			string inputText = this.ConvertPropertiesToString();
			return this.helper.EncryptWithHint(inputText, this.partnerIDHint, WIMSAuthHeaderCryptoHelper.partnerIDOffset);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0003E438 File Offset: 0x0003C638
		public string DecryptWithHintToProperties(string inputText)
		{
			string text = this.helper.DecryptWithHint(inputText, this.partnerIDHint, WIMSAuthHeaderCryptoHelper.partnerIDOffset);
			if (text != null)
			{
				this.LoadPropertiesFromString(text);
			}
			return text;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0003E468 File Offset: 0x0003C668
		private string ConvertPropertiesToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("{0}={1};", "sender", this.sender ?? string.Empty));
			stringBuilder.Append(string.Format("{0}={1};", "expiredAt", this.expiredAt.ToFileTimeUtc()));
			stringBuilder.Append(string.Format("{0}={1}", "type", this.authHeaderType));
			foreach (KeyValuePair<string, string> keyValuePair in this.customProperties)
			{
				stringBuilder.Append(string.Format(";{0}={1}", keyValuePair.Key ?? string.Empty, keyValuePair.Value ?? string.Empty));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0003E558 File Offset: 0x0003C758
		private void LoadPropertiesFromString(string inputText)
		{
			if (inputText == null)
			{
				throw new ArgumentNullException("inputText");
			}
			this.sender = null;
			this.authHeaderType = 0;
			this.customProperties.Clear();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			string[] array = inputText.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length >= 1)
				{
					if (array2[0].Equals("sender"))
					{
						this.sender = ((array2.Length < 2 && array2[1].Length == 0) ? null : array2[1]);
						flag = true;
					}
					else if (array2[0].Equals("expiredAt"))
					{
						long fileTime;
						if (array2.Length >= 2 && long.TryParse(array2[1], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out fileTime))
						{
							this.expiredAt = DateTime.FromFileTimeUtc(fileTime);
						}
						flag3 = true;
					}
					else if (array2[0].Equals("type"))
					{
						int num;
						if (array2.Length >= 2 && int.TryParse(array2[1], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
						{
							this.authHeaderType = num;
						}
						flag2 = true;
					}
					else
					{
						this.customProperties[array2[0]] = ((array2.Length < 2 && array2[1].Length == 0) ? null : array2[1]);
					}
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException(string.Format("Missing required property: {0}", "sender"));
			}
			if (!flag3)
			{
				throw new InvalidOperationException(string.Format("Missing required property: {0}", "expiredAt"));
			}
			if (!flag2)
			{
				throw new InvalidOperationException(string.Format("Missing required property: {0}", "type"));
			}
		}

		// Token: 0x04001EFD RID: 7933
		public const string SenderName = "sender";

		// Token: 0x04001EFE RID: 7934
		public const string ExpiredAtName = "expiredAt";

		// Token: 0x04001EFF RID: 7935
		public const string HeaderTypeName = "type";

		// Token: 0x04001F00 RID: 7936
		private static int partnerIDOffset = 16;

		// Token: 0x04001F01 RID: 7937
		private CryptoHelper helper;

		// Token: 0x04001F02 RID: 7938
		private string partnerID;

		// Token: 0x04001F03 RID: 7939
		private string partnerIDHint;

		// Token: 0x04001F04 RID: 7940
		private string sender;

		// Token: 0x04001F05 RID: 7941
		private DateTime expiredAt;

		// Token: 0x04001F06 RID: 7942
		private int authHeaderType;

		// Token: 0x04001F07 RID: 7943
		private IDictionary<string, string> customProperties;

		// Token: 0x020006BD RID: 1725
		internal class HeaderType
		{
			// Token: 0x04001F08 RID: 7944
			public const int Unknown = 0;

			// Token: 0x04001F09 RID: 7945
			public const int IntraDomainDelivery = 1;

			// Token: 0x04001F0A RID: 7946
			public const int OIMTrxText = 2;

			// Token: 0x04001F0B RID: 7947
			public const int OIMTrxSMS = 3;

			// Token: 0x04001F0C RID: 7948
			public const int OIMTrxBubble = 4;

			// Token: 0x04001F0D RID: 7949
			public const int OIMNTrxText = 5;

			// Token: 0x04001F0E RID: 7950
			public const int OIMNTrxSMS = 6;

			// Token: 0x04001F0F RID: 7951
			public const int OIMNTrxBubble = 7;

			// Token: 0x04001F10 RID: 7952
			public const int NotUsed = 8;

			// Token: 0x04001F11 RID: 7953
			public const int NoBar = 9;

			// Token: 0x04001F12 RID: 7954
			public const int TrustedSender = 10;

			// Token: 0x04001F13 RID: 7955
			public const int FilterBypass = 11;

			// Token: 0x04001F14 RID: 7956
			public const int ExclusiveBypass = 12;

			// Token: 0x04001F15 RID: 7957
			public const int Unsubscribe = 13;
		}
	}
}
