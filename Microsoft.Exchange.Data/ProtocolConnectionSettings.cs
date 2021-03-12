using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000181 RID: 385
	[Serializable]
	public class ProtocolConnectionSettings
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x00026FA6 File Offset: 0x000251A6
		public ProtocolConnectionSettings(string settings)
		{
			this.ParseAndValidate(settings);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00026FB5 File Offset: 0x000251B5
		public ProtocolConnectionSettings(Hostname hostname, int port, EncryptionType? encryptionType)
		{
			this.hostname = hostname;
			this.port = port;
			this.encryptionType = encryptionType;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00026FD2 File Offset: 0x000251D2
		public Hostname Hostname
		{
			get
			{
				return this.hostname;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00026FDA File Offset: 0x000251DA
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00026FE2 File Offset: 0x000251E2
		public EncryptionType? EncryptionType
		{
			get
			{
				return this.encryptionType;
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00026FEA File Offset: 0x000251EA
		public static ProtocolConnectionSettings Parse(string settings)
		{
			return new ProtocolConnectionSettings(settings);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00026FF4 File Offset: 0x000251F4
		public override bool Equals(object obj)
		{
			ProtocolConnectionSettings protocolConnectionSettings = obj as ProtocolConnectionSettings;
			return protocolConnectionSettings != null && this.Equals(protocolConnectionSettings);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00027014 File Offset: 0x00025214
		public bool Equals(ProtocolConnectionSettings settings)
		{
			return settings != null && (this.hostname.Equals(settings.hostname) && this.port == settings.port) && this.encryptionType == settings.encryptionType;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00027079 File Offset: 0x00025279
		public override int GetHashCode()
		{
			if (this.hostname == null)
			{
				return 0;
			}
			return this.hostname.GetHashCode() ^ this.port ^ ((this.encryptionType != null) ? this.encryptionType.GetHashCode() : 0);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000270BC File Offset: 0x000252BC
		public override string ToString()
		{
			if (this.encryptionType != null)
			{
				return string.Format("{0}:{1}:{2}", this.hostname, this.port, this.encryptionType);
			}
			return string.Format("{0}:{1}", this.hostname, this.port);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00027118 File Offset: 0x00025318
		private void ParseAndValidate(string settings)
		{
			EncryptionType? encryptionType = null;
			string[] array = settings.Split(new char[]
			{
				':'
			});
			if (array.Length < 2 || array.Length > 3)
			{
				throw new FormatException(DataStrings.ExceptionProtocolConnectionSettingsInvalidFormat(settings));
			}
			Hostname hostname;
			if (!Hostname.TryParse(array[0], out hostname))
			{
				throw new FormatException(DataStrings.ExceptionProtocolConnectionSettingsInvalidHostname(settings));
			}
			int num;
			if (!int.TryParse(array[1], out num) || num < 0 || num > 65535)
			{
				throw new FormatException(DataStrings.ExceptionProtocolConnectionSettingsInvalidPort(settings, 0, 65535));
			}
			if (array.Length > 2)
			{
				try
				{
					encryptionType = new EncryptionType?((EncryptionType)Enum.Parse(typeof(EncryptionType), array[2], true));
				}
				catch (ArgumentException)
				{
					throw new FormatException(DataStrings.ExceptionProtocolConnectionSettingsInvalidEncryptionType(settings));
				}
			}
			this.hostname = hostname;
			this.port = num;
			this.encryptionType = encryptionType;
		}

		// Token: 0x04000794 RID: 1940
		private Hostname hostname;

		// Token: 0x04000795 RID: 1941
		private int port;

		// Token: 0x04000796 RID: 1942
		private EncryptionType? encryptionType;
	}
}
