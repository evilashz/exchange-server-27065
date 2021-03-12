using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E9 RID: 233
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationNspiSetRecipientRpcArgs : MigrationNspiRpcArgs
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x0003433B File Offset: 0x0003253B
		public MigrationNspiSetRecipientRpcArgs(ExchangeOutlookAnywhereEndpoint endpoint, string recipientSmtpAddress, string recipientLegDN, string[] propTagValues, long[] longPropTags) : base(endpoint, MigrationProxyRpcType.SetRecipient)
		{
			this.RecipientSmtpAddress = recipientSmtpAddress;
			this.RecipientLegDN = recipientLegDN;
			this.PropTagValues = propTagValues;
			this.LongPropTags = longPropTags;
			this.ExchangeServer = endpoint.ExchangeServer;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003436F File Offset: 0x0003256F
		public MigrationNspiSetRecipientRpcArgs(byte[] requestBlob) : base(requestBlob, MigrationProxyRpcType.SetRecipient)
		{
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00034379 File Offset: 0x00032579
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00034386 File Offset: 0x00032586
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

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00034394 File Offset: 0x00032594
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x000343A1 File Offset: 0x000325A1
		public string RecipientLegDN
		{
			get
			{
				return base.GetProperty<string>(2416967711U);
			}
			set
			{
				base.SetPropertyAsString(2416967711U, value);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000343AF File Offset: 0x000325AF
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x000343BC File Offset: 0x000325BC
		public string ExchangeServer
		{
			get
			{
				return base.GetProperty<string>(2416902175U);
			}
			set
			{
				base.SetPropertyAsString(2416902175U, value);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000343CA File Offset: 0x000325CA
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x000343D7 File Offset: 0x000325D7
		public string[] PropTagValues
		{
			get
			{
				return base.GetProperty<string[]>(2416775199U);
			}
			set
			{
				base.SetProperty(2416775199U, value);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000343E5 File Offset: 0x000325E5
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x000343F2 File Offset: 0x000325F2
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00034400 File Offset: 0x00032600
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

		// Token: 0x06000BEC RID: 3052 RVA: 0x00034438 File Offset: 0x00032638
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
			if (string.IsNullOrEmpty(this.ExchangeServer))
			{
				errorMsg = "Exchange Server cannot be null or empty.";
				return false;
			}
			if (this.LongPropTags == null || this.LongPropTags.Length == 0)
			{
				errorMsg = "PropTags cannot be null.";
				return false;
			}
			if (this.PropTagValues == null || this.PropTagValues.Length == 0)
			{
				errorMsg = "PropTagValues cannot be null.";
				return false;
			}
			if (this.PropTagValues.Length != this.LongPropTags.Length)
			{
				errorMsg = "PropTagValues has to be same in size as PropTags.";
				return false;
			}
			errorMsg = null;
			return true;
		}
	}
}
