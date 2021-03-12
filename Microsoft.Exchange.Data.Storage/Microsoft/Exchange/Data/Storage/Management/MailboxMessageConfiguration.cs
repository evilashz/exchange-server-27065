using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A7A RID: 2682
	[Serializable]
	public class MailboxMessageConfiguration : XsoMailboxConfigurationObject
	{
		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x060061FD RID: 25085 RVA: 0x0019EF05 File Offset: 0x0019D105
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return MailboxMessageConfiguration.schema;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x060061FE RID: 25086 RVA: 0x0019EF0C File Offset: 0x0019D10C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06006200 RID: 25088 RVA: 0x0019EF1B File Offset: 0x0019D11B
		// (set) Token: 0x06006201 RID: 25089 RVA: 0x0019EF2D File Offset: 0x0019D12D
		[Parameter(Mandatory = false)]
		public AfterMoveOrDeleteBehavior AfterMoveOrDeleteBehavior
		{
			get
			{
				return (AfterMoveOrDeleteBehavior)this[MailboxMessageConfigurationSchema.AfterMoveOrDeleteBehavior];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.AfterMoveOrDeleteBehavior] = value;
			}
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x06006202 RID: 25090 RVA: 0x0019EF40 File Offset: 0x0019D140
		// (set) Token: 0x06006203 RID: 25091 RVA: 0x0019EF52 File Offset: 0x0019D152
		[Parameter(Mandatory = false)]
		public NewItemNotification NewItemNotification
		{
			get
			{
				return (NewItemNotification)this[MailboxMessageConfigurationSchema.NewItemNotification];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.NewItemNotification] = value;
			}
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06006204 RID: 25092 RVA: 0x0019EF65 File Offset: 0x0019D165
		// (set) Token: 0x06006205 RID: 25093 RVA: 0x0019EF77 File Offset: 0x0019D177
		[Parameter(Mandatory = false)]
		public bool EmptyDeletedItemsOnLogoff
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.EmptyDeletedItemsOnLogoff];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.EmptyDeletedItemsOnLogoff] = value;
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06006206 RID: 25094 RVA: 0x0019EF8A File Offset: 0x0019D18A
		// (set) Token: 0x06006207 RID: 25095 RVA: 0x0019EF9C File Offset: 0x0019D19C
		[Parameter(Mandatory = false)]
		public bool AutoAddSignature
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.AutoAddSignature];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.AutoAddSignature] = value;
			}
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x06006208 RID: 25096 RVA: 0x0019EFAF File Offset: 0x0019D1AF
		// (set) Token: 0x06006209 RID: 25097 RVA: 0x0019EFC1 File Offset: 0x0019D1C1
		[Parameter(Mandatory = false)]
		public string SignatureText
		{
			get
			{
				return (string)this[MailboxMessageConfigurationSchema.SignatureText];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.SignatureText] = value;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x0600620A RID: 25098 RVA: 0x0019EFCF File Offset: 0x0019D1CF
		// (set) Token: 0x0600620B RID: 25099 RVA: 0x0019EFE1 File Offset: 0x0019D1E1
		[Parameter(Mandatory = false)]
		public string SignatureHtml
		{
			get
			{
				return (string)this[MailboxMessageConfigurationSchema.SignatureHtml];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.SignatureHtml] = value;
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x0019EFEF File Offset: 0x0019D1EF
		// (set) Token: 0x0600620D RID: 25101 RVA: 0x0019F001 File Offset: 0x0019D201
		[Parameter(Mandatory = false)]
		public bool AutoAddSignatureOnMobile
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.AutoAddSignatureOnMobile];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.AutoAddSignatureOnMobile] = value;
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x0600620E RID: 25102 RVA: 0x0019F014 File Offset: 0x0019D214
		// (set) Token: 0x0600620F RID: 25103 RVA: 0x0019F026 File Offset: 0x0019D226
		[Parameter(Mandatory = false)]
		public string SignatureTextOnMobile
		{
			get
			{
				return (string)this[MailboxMessageConfigurationSchema.SignatureTextOnMobile];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.SignatureTextOnMobile] = value;
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06006210 RID: 25104 RVA: 0x0019F034 File Offset: 0x0019D234
		// (set) Token: 0x06006211 RID: 25105 RVA: 0x0019F046 File Offset: 0x0019D246
		[Parameter(Mandatory = false)]
		public bool UseDefaultSignatureOnMobile
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.UseDesktopSignature];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.UseDesktopSignature] = value;
			}
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06006212 RID: 25106 RVA: 0x0019F059 File Offset: 0x0019D259
		// (set) Token: 0x06006213 RID: 25107 RVA: 0x0019F06B File Offset: 0x0019D26B
		[Parameter(Mandatory = false)]
		public string DefaultFontName
		{
			get
			{
				return (string)this[MailboxMessageConfigurationSchema.DefaultFontName];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.DefaultFontName] = value;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x0019F079 File Offset: 0x0019D279
		// (set) Token: 0x06006215 RID: 25109 RVA: 0x0019F08B File Offset: 0x0019D28B
		[Parameter(Mandatory = false)]
		public int DefaultFontSize
		{
			get
			{
				return (int)this[MailboxMessageConfigurationSchema.DefaultFontSize];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.DefaultFontSize] = value;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x0019F09E File Offset: 0x0019D29E
		// (set) Token: 0x06006217 RID: 25111 RVA: 0x0019F0B0 File Offset: 0x0019D2B0
		[Parameter(Mandatory = false)]
		public string DefaultFontColor
		{
			get
			{
				return (string)this[MailboxMessageConfigurationSchema.DefaultFontColor];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.DefaultFontColor] = value;
			}
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x0019F0BE File Offset: 0x0019D2BE
		// (set) Token: 0x06006219 RID: 25113 RVA: 0x0019F0D0 File Offset: 0x0019D2D0
		[Parameter(Mandatory = false)]
		public FontFlags DefaultFontFlags
		{
			get
			{
				return (FontFlags)this[MailboxMessageConfigurationSchema.DefaultFontFlags];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.DefaultFontFlags] = value;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x0600621A RID: 25114 RVA: 0x0019F0E3 File Offset: 0x0019D2E3
		// (set) Token: 0x0600621B RID: 25115 RVA: 0x0019F0F5 File Offset: 0x0019D2F5
		[Parameter(Mandatory = false)]
		public bool AlwaysShowBcc
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.AlwaysShowBcc];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.AlwaysShowBcc] = value;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x0019F108 File Offset: 0x0019D308
		// (set) Token: 0x0600621D RID: 25117 RVA: 0x0019F11A File Offset: 0x0019D31A
		[Parameter(Mandatory = false)]
		public bool AlwaysShowFrom
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.AlwaysShowFrom];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.AlwaysShowFrom] = value;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x0600621E RID: 25118 RVA: 0x0019F12D File Offset: 0x0019D32D
		// (set) Token: 0x0600621F RID: 25119 RVA: 0x0019F13F File Offset: 0x0019D33F
		[Parameter(Mandatory = false)]
		public MailFormat DefaultFormat
		{
			get
			{
				return (MailFormat)this[MailboxMessageConfigurationSchema.DefaultFormat];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.DefaultFormat] = value;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06006220 RID: 25120 RVA: 0x0019F152 File Offset: 0x0019D352
		// (set) Token: 0x06006221 RID: 25121 RVA: 0x0019F164 File Offset: 0x0019D364
		[Parameter(Mandatory = false)]
		public ReadReceiptResponse ReadReceiptResponse
		{
			get
			{
				return (ReadReceiptResponse)this[MailboxMessageConfigurationSchema.ReadReceiptResponse];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.ReadReceiptResponse] = value;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x06006222 RID: 25122 RVA: 0x0019F177 File Offset: 0x0019D377
		// (set) Token: 0x06006223 RID: 25123 RVA: 0x0019F189 File Offset: 0x0019D389
		[Parameter(Mandatory = false)]
		public PreviewMarkAsReadBehavior PreviewMarkAsReadBehavior
		{
			get
			{
				return (PreviewMarkAsReadBehavior)this[MailboxMessageConfigurationSchema.PreviewMarkAsReadBehavior];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.PreviewMarkAsReadBehavior] = value;
			}
		}

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x0019F19C File Offset: 0x0019D39C
		// (set) Token: 0x06006225 RID: 25125 RVA: 0x0019F1AE File Offset: 0x0019D3AE
		[Parameter(Mandatory = false)]
		public int PreviewMarkAsReadDelaytime
		{
			get
			{
				return (int)this[MailboxMessageConfigurationSchema.PreviewMarkAsReadDelaytime];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.PreviewMarkAsReadDelaytime] = value;
			}
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x0019F1C1 File Offset: 0x0019D3C1
		// (set) Token: 0x06006227 RID: 25127 RVA: 0x0019F1D3 File Offset: 0x0019D3D3
		[Parameter(Mandatory = false)]
		public ConversationSortOrder ConversationSortOrder
		{
			get
			{
				return (ConversationSortOrder)this[MailboxMessageConfigurationSchema.ConversationSortOrder];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.ConversationSortOrder] = value;
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x0019F1E6 File Offset: 0x0019D3E6
		// (set) Token: 0x06006229 RID: 25129 RVA: 0x0019F1F8 File Offset: 0x0019D3F8
		[Parameter(Mandatory = false)]
		public bool ShowConversationAsTree
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.ShowConversationAsTree];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.ShowConversationAsTree] = value;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x0600622A RID: 25130 RVA: 0x0019F20B File Offset: 0x0019D40B
		// (set) Token: 0x0600622B RID: 25131 RVA: 0x0019F21D File Offset: 0x0019D41D
		[Parameter(Mandatory = false)]
		public bool HideDeletedItems
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.HideDeletedItems];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.HideDeletedItems] = value;
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x0019F230 File Offset: 0x0019D430
		// (set) Token: 0x0600622D RID: 25133 RVA: 0x0019F242 File Offset: 0x0019D442
		[Parameter(Mandatory = false)]
		public string SendAddressDefault
		{
			get
			{
				return (string)this[MailboxMessageConfigurationSchema.SendAddressDefault];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.SendAddressDefault] = value;
			}
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x0600622E RID: 25134 RVA: 0x0019F250 File Offset: 0x0019D450
		// (set) Token: 0x0600622F RID: 25135 RVA: 0x0019F262 File Offset: 0x0019D462
		[Parameter(Mandatory = false)]
		public EmailComposeMode EmailComposeMode
		{
			get
			{
				return (EmailComposeMode)this[MailboxMessageConfigurationSchema.EmailComposeMode];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.EmailComposeMode] = value;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x0019F275 File Offset: 0x0019D475
		// (set) Token: 0x06006231 RID: 25137 RVA: 0x0019F287 File Offset: 0x0019D487
		[Parameter(Mandatory = false)]
		public bool CheckForForgottenAttachments
		{
			get
			{
				return (bool)this[MailboxMessageConfigurationSchema.CheckForForgottenAttachments];
			}
			set
			{
				this[MailboxMessageConfigurationSchema.CheckForForgottenAttachments] = value;
			}
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x0019F29C File Offset: 0x0019D49C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			int num = (this.SignatureHtml == null) ? 0 : this.SignatureHtml.Length;
			num += ((this.SignatureText == null) ? 0 : this.SignatureText.Length);
			if (num > 8000)
			{
				errors.Add(new ObjectValidationError(ServerStrings.ErrorSigntureTooLarge, this.Identity, num.ToString()));
			}
		}

		// Token: 0x040037B3 RID: 14259
		internal const int MaxSignatureSize = 8000;

		// Token: 0x040037B4 RID: 14260
		internal const int MaxSignatureSizeOnMobile = 512;

		// Token: 0x040037B5 RID: 14261
		private static MailboxMessageConfigurationSchema schema = ObjectSchema.GetInstance<MailboxMessageConfigurationSchema>();
	}
}
