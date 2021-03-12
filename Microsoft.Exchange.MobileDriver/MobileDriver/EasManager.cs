using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200001D RID: 29
	internal class EasManager : IMobileServiceManager
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000043A6 File Offset: 0x000025A6
		public EasManager(EasSelector selector)
		{
			this.Selector = selector;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000043B5 File Offset: 0x000025B5
		IMobileServiceSelector IMobileServiceManager.Selector
		{
			get
			{
				return this.Selector;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000043BD File Offset: 0x000025BD
		public bool CapabilityPerRecipientSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000043C0 File Offset: 0x000025C0
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000043C8 File Offset: 0x000025C8
		public EasSelector Selector { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000043D1 File Offset: 0x000025D1
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000043D8 File Offset: 0x000025D8
		private static EasCapability Capability { get; set; } = new EasCapability(PartType.Short | PartType.Concatenated, 255, new CodingSupportability[]
		{
			new CodingSupportability(CodingScheme.GsmDefault, 160, 153),
			new CodingSupportability(CodingScheme.Unicode, 70, 67)
		}, FeatureSupportability.None);

		// Token: 0x060000A2 RID: 162 RVA: 0x000043E0 File Offset: 0x000025E0
		public EasCapability GetCapabilityForRecipient(MobileRecipient recipient)
		{
			if (recipient != null)
			{
				throw new ArgumentException("recipient");
			}
			return EasManager.Capability;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043F5 File Offset: 0x000025F5
		MobileServiceCapability IMobileServiceManager.GetCapabilityForRecipient(MobileRecipient recipient)
		{
			return this.GetCapabilityForRecipient(recipient);
		}
	}
}
