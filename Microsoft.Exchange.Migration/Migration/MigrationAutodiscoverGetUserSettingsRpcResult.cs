using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Autodiscover;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000EB RID: 235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationAutodiscoverGetUserSettingsRpcResult : MigrationProxyRpcResult
	{
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00034868 File Offset: 0x00032A68
		public MigrationAutodiscoverGetUserSettingsRpcResult() : base(MigrationProxyRpcType.GetUserSettings)
		{
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00034871 File Offset: 0x00032A71
		public MigrationAutodiscoverGetUserSettingsRpcResult(byte[] resultBlob) : base(resultBlob, MigrationProxyRpcType.GetUserSettings)
		{
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0003487C File Offset: 0x00032A7C
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x000348BA File Offset: 0x00032ABA
		public ExchangeVersion? ExchangeVersion
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433024003U, out obj) && obj is int)
				{
					return new ExchangeVersion?((ExchangeVersion)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2433024003U] = value.Value;
					return;
				}
				this.PropertyCollection.Remove(2433024003U);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x000348F4 File Offset: 0x00032AF4
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00034932 File Offset: 0x00032B32
		public AutodiscoverClientStatus? Status
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2432958467U, out obj) && obj is int)
				{
					return new AutodiscoverClientStatus?((AutodiscoverClientStatus)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2432958467U] = (int)value.Value;
					return;
				}
				this.PropertyCollection.Remove(2432958467U);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0003496C File Offset: 0x00032B6C
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00034995 File Offset: 0x00032B95
		public string MailboxDN
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433089567U, out obj))
				{
					return obj as string;
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2433089567U] = value;
					return;
				}
				this.PropertyCollection.Remove(2433089567U);
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000349C4 File Offset: 0x00032BC4
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x000349ED File Offset: 0x00032BED
		public string ExchangeServerDN
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433155103U, out obj))
				{
					return obj as string;
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2433155103U] = value;
					return;
				}
				this.PropertyCollection.Remove(2433155103U);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00034A1C File Offset: 0x00032C1C
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x00034A4F File Offset: 0x00032C4F
		public string RpcProxyServer
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433220639U, out obj))
				{
					string text = obj as string;
					if (!string.IsNullOrEmpty(text))
					{
						return text;
					}
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2433220639U] = value;
					return;
				}
				this.PropertyCollection.Remove(2433220639U);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00034A7C File Offset: 0x00032C7C
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00034AAF File Offset: 0x00032CAF
		public string ExchangeServer
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433286175U, out obj))
				{
					string text = obj as string;
					if (!string.IsNullOrEmpty(text))
					{
						return text;
					}
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2433286175U] = value;
					return;
				}
				this.PropertyCollection.Remove(2433286175U);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00034ADC File Offset: 0x00032CDC
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x00034B1A File Offset: 0x00032D1A
		public AuthenticationMethod? AuthenticationMethod
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433351683U, out obj) && obj is int)
				{
					return new AuthenticationMethod?((AuthenticationMethod)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2433351683U] = (int)value.Value;
					return;
				}
				this.PropertyCollection.Remove(2433351683U);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00034B54 File Offset: 0x00032D54
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00034B7D File Offset: 0x00032D7D
		public string AutodiscoverUrl
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433417247U, out obj))
				{
					return obj as string;
				}
				return null;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.PropertyCollection[2433417247U] = value;
					return;
				}
				this.PropertyCollection.Remove(2433417247U);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00034BAC File Offset: 0x00032DAC
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00034BEA File Offset: 0x00032DEA
		public AutodiscoverErrorCode? AutodiscoverErrorCode
		{
			get
			{
				object obj;
				if (this.PropertyCollection.TryGetValue(2433482755U, out obj) && obj is int)
				{
					return new AutodiscoverErrorCode?((AutodiscoverErrorCode)obj);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.PropertyCollection[2433482755U] = value.Value;
					return;
				}
				this.PropertyCollection.Remove(2433482755U);
			}
		}
	}
}
