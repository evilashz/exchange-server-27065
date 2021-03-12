using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E3 RID: 227
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationAutodiscoverGetUserSettingsRpcArgs : MigrationProxyRpcArgs
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x00033D4C File Offset: 0x00031F4C
		public MigrationAutodiscoverGetUserSettingsRpcArgs(string userName, string encryptedPassword, string userDomain, string autodiscoverDomain, Uri autodiscoverUrl, ExchangeVersion? exchangeVersion, string userSmtpAddress) : base(userName, encryptedPassword, userDomain, MigrationProxyRpcType.GetUserSettings)
		{
			this.AutodiscoverDomain = autodiscoverDomain;
			this.AutodiscoverUrl = autodiscoverUrl;
			this.ExchangeVersion = exchangeVersion;
			this.UserSmtpAddress = userSmtpAddress;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00033D78 File Offset: 0x00031F78
		public MigrationAutodiscoverGetUserSettingsRpcArgs(byte[] requestBlob) : base(requestBlob, MigrationProxyRpcType.GetUserSettings)
		{
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00033D82 File Offset: 0x00031F82
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00033D8F File Offset: 0x00031F8F
		public string AutodiscoverDomain
		{
			get
			{
				return base.GetProperty<string>(2416574495U);
			}
			set
			{
				base.SetPropertyAsString(2416574495U, value);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00033DA0 File Offset: 0x00031FA0
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00033DC8 File Offset: 0x00031FC8
		public Uri AutodiscoverUrl
		{
			get
			{
				string property = base.GetProperty<string>(2416640031U);
				Uri result;
				if (Uri.TryCreate(property, UriKind.Absolute, out result))
				{
					return result;
				}
				return null;
			}
			set
			{
				string value2 = null;
				if (value != null)
				{
					value2 = value.AbsoluteUri;
				}
				base.SetProperty(2416640031U, value2);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00033DF4 File Offset: 0x00031FF4
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00033E32 File Offset: 0x00032032
		public ExchangeVersion? ExchangeVersion
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2416705539U, out obj) && obj is int)
				{
					return new ExchangeVersion?((ExchangeVersion)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2416705539U] = value.Value;
					return;
				}
				this.PropertyCollection.Remove(2416705539U);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00033E6B File Offset: 0x0003206B
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00033E78 File Offset: 0x00032078
		public string UserSmtpAddress
		{
			get
			{
				return base.GetProperty<string>(2416508959U);
			}
			set
			{
				base.SetPropertyAsString(2416508959U, value);
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00033E86 File Offset: 0x00032086
		public override bool Validate(out string errorMsg)
		{
			if (!base.Validate(out errorMsg))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.AutodiscoverDomain))
			{
				errorMsg = "Autodiscover Domain cannot be null or empty.";
				return false;
			}
			if (string.IsNullOrEmpty(this.UserSmtpAddress))
			{
				errorMsg = "Autodiscover User Smtp Address cannot be null or empty.";
				return false;
			}
			errorMsg = null;
			return true;
		}
	}
}
