using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002DB RID: 731
	[DataContract]
	public class ProtocolSettingsData : BaseRow
	{
		// Token: 0x06002CC9 RID: 11465 RVA: 0x00089B8F File Offset: 0x00087D8F
		public ProtocolSettingsData(CASMailbox casMailbox) : base(casMailbox.ToIdentity(), casMailbox)
		{
			this.casMailbox = casMailbox;
		}

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x00089BA8 File Offset: 0x00087DA8
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x00089BED File Offset: 0x00087DED
		[DataMember]
		public string ExternalPopSetting
		{
			get
			{
				string text = this.casMailbox.ExternalPopSettings;
				if (string.IsNullOrEmpty(text))
				{
					text = OwaOptionStrings.SettingNotAvailable;
				}
				if (!this.casMailbox.PopEnabled)
				{
					text = OwaOptionStrings.SettingAccessDisabled;
				}
				return text;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x00089BF4 File Offset: 0x00087DF4
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x00089C39 File Offset: 0x00087E39
		[DataMember]
		public string ExternalImapSetting
		{
			get
			{
				string text = this.casMailbox.ExternalImapSettings;
				if (string.IsNullOrEmpty(text))
				{
					text = OwaOptionStrings.SettingNotAvailable;
				}
				if (!this.casMailbox.ImapEnabled)
				{
					text = OwaOptionStrings.SettingAccessDisabled;
				}
				return text;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00089C40 File Offset: 0x00087E40
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x00089C6D File Offset: 0x00087E6D
		[DataMember]
		public string ExternalSmtpSetting
		{
			get
			{
				string text = this.casMailbox.ExternalSmtpSettings;
				if (string.IsNullOrEmpty(text))
				{
					text = OwaOptionStrings.SettingNotAvailable;
				}
				return text;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002211 RID: 8721
		private CASMailbox casMailbox;
	}
}
