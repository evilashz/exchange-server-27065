using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	public class UMMailboxConfiguration : ConfigurableObject
	{
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002BCA3 File Offset: 0x00029EA3
		public UMMailboxConfiguration(ObjectId identity) : base(new SimpleProviderPropertyBag())
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.propertyBag.SetField(SimpleProviderObjectSchema.Identity, identity);
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002BCD0 File Offset: 0x00029ED0
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x0002BCE2 File Offset: 0x00029EE2
		public MailboxGreetingEnum Greeting
		{
			get
			{
				return (MailboxGreetingEnum)this[UMMailboxConfigurationSchema.Greeting];
			}
			internal set
			{
				this[UMMailboxConfigurationSchema.Greeting] = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0002BCF5 File Offset: 0x00029EF5
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0002BD07 File Offset: 0x00029F07
		public MailboxFolder FolderToReadEmailsFrom
		{
			get
			{
				return (MailboxFolder)this[UMMailboxConfigurationSchema.FolderToReadEmailsFrom];
			}
			internal set
			{
				this[UMMailboxConfigurationSchema.FolderToReadEmailsFrom] = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002BD15 File Offset: 0x00029F15
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0002BD27 File Offset: 0x00029F27
		public bool ReadOldestUnreadVoiceMessagesFirst
		{
			get
			{
				return (bool)this[UMMailboxConfigurationSchema.ReadOldestUnreadVoiceMessagesFirst];
			}
			internal set
			{
				this[UMMailboxConfigurationSchema.ReadOldestUnreadVoiceMessagesFirst] = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002BD3A File Offset: 0x00029F3A
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0002BD4C File Offset: 0x00029F4C
		public string DefaultPlayOnPhoneNumber
		{
			get
			{
				return (string)this[UMMailboxConfigurationSchema.DefaultPlayOnPhoneNumber];
			}
			internal set
			{
				this[UMMailboxConfigurationSchema.DefaultPlayOnPhoneNumber] = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002BD5A File Offset: 0x00029F5A
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0002BD6C File Offset: 0x00029F6C
		public bool ReceivedVoiceMailPreviewEnabled
		{
			get
			{
				return (bool)this[UMMailboxConfigurationSchema.ReceivedVoiceMailPreviewEnabled];
			}
			internal set
			{
				this[UMMailboxConfigurationSchema.ReceivedVoiceMailPreviewEnabled] = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002BD7F File Offset: 0x00029F7F
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x0002BD91 File Offset: 0x00029F91
		public bool SentVoiceMailPreviewEnabled
		{
			get
			{
				return (bool)this[UMMailboxConfigurationSchema.SentVoiceMailPreviewEnabled];
			}
			internal set
			{
				this[UMMailboxConfigurationSchema.SentVoiceMailPreviewEnabled] = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002BDA4 File Offset: 0x00029FA4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMMailboxConfiguration.schema;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0002BDAB File Offset: 0x00029FAB
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x0400067A RID: 1658
		public const string GreetingParameterName = "Greeting";

		// Token: 0x0400067B RID: 1659
		public const string FolderToReadEmailsFromParameterName = "FolderToReadEmailsFrom";

		// Token: 0x0400067C RID: 1660
		public const string ReadOldestUnreadVoiceMessageFirstParameterName = "ReadOldestUnreadVoiceMessageFirst";

		// Token: 0x0400067D RID: 1661
		public const string DefaultPlayOnPhoneNumberParameterName = "DefaultPlayOnPhoneNumber";

		// Token: 0x0400067E RID: 1662
		public const string ReceivedVoiceMailPreviewEnabledParameterName = "ReceivedVoiceMailPreviewEnabled";

		// Token: 0x0400067F RID: 1663
		public const string SentVoiceMailPreviewEnabledParameterName = "SentVoiceMailPreviewEnabled";

		// Token: 0x04000680 RID: 1664
		private static UMMailboxConfigurationSchema schema = ObjectSchema.GetInstance<UMMailboxConfigurationSchema>();
	}
}
