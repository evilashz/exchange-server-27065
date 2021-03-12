using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E7 RID: 231
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiGetRecipientRpcArgs : MigrationNspiRpcArgs
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x00034063 File Offset: 0x00032263
		public MigrationNspiGetRecipientRpcArgs(ExchangeOutlookAnywhereEndpoint endpoint, string recipientSmtpAddress, long[] longPropTags) : base(endpoint, MigrationProxyRpcType.GetRecipient)
		{
			this.RecipientSmtpAddress = recipientSmtpAddress;
			this.LongPropTags = longPropTags;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0003407B File Offset: 0x0003227B
		public MigrationNspiGetRecipientRpcArgs(byte[] requestBlob) : base(requestBlob, MigrationProxyRpcType.GetRecipient)
		{
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00034085 File Offset: 0x00032285
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x00034092 File Offset: 0x00032292
		public string RecipientSmtpAddress
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

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000340A0 File Offset: 0x000322A0
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x000340AD File Offset: 0x000322AD
		public long[] LongPropTags
		{
			private get
			{
				return base.GetProperty<long[]>(2416447508U);
			}
			set
			{
				base.SetProperty(2416447508U, value);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x000340BC File Offset: 0x000322BC
		public PropTag[] PropTags
		{
			get
			{
				long[] longPropTags = this.LongPropTags;
				if (longPropTags == null)
				{
					return null;
				}
				PropTag[] array = new PropTag[longPropTags.Length];
				for (int i = 0; i < longPropTags.Length; i++)
				{
					array[i] = (PropTag)longPropTags[i];
				}
				return array;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000340F4 File Offset: 0x000322F4
		public override bool Validate(out string errorMsg)
		{
			if (!base.Validate(out errorMsg))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.RecipientSmtpAddress))
			{
				errorMsg = "Recipient Smtp Address cannot be null or empty.";
				return false;
			}
			if (this.LongPropTags == null || this.LongPropTags.Length == 0)
			{
				errorMsg = "PropTags cannot be null.";
				return false;
			}
			errorMsg = null;
			return true;
		}
	}
}
