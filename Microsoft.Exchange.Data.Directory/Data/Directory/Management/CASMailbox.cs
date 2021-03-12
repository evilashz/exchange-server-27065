using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E4 RID: 1764
	[Serializable]
	public class CASMailbox : ADPresentationObject
	{
		// Token: 0x0600517B RID: 20859 RVA: 0x0012DA34 File Offset: 0x0012BC34
		internal static bool IsMailboxProtocolLoggingEnabled(ADObject adObject, ADPropertyDefinition property)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			int? num = (int?)adObject[property];
			if (num == null)
			{
				return false;
			}
			double num2 = (ExDateTime.UtcNow - CASMailbox.MailboxProtocolLoggingInitialTime).TotalMinutes - (double)num.Value;
			return num2 >= 0.0 && num2 < (double)CASMailbox.MailboxProtocolLoggingLength;
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x0012DAB0 File Offset: 0x0012BCB0
		internal static void SetMailboxProtocolLoggingEnabled(ADObject adObject, ADPropertyDefinition property, bool value)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (!value)
			{
				adObject[property] = null;
				return;
			}
			double totalMinutes = (ExDateTime.UtcNow - CASMailbox.MailboxProtocolLoggingInitialTime).TotalMinutes;
			if (totalMinutes < -2147483648.0 || 2147483647.0 < totalMinutes)
			{
				adObject[property] = null;
				return;
			}
			adObject[property] = (int)totalMinutes;
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x0600517D RID: 20861 RVA: 0x0012DB2C File Offset: 0x0012BD2C
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return CASMailbox.schema;
			}
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x0012DB33 File Offset: 0x0012BD33
		public CASMailbox()
		{
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x0012DB47 File Offset: 0x0012BD47
		public CASMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x0012DB5C File Offset: 0x0012BD5C
		internal static CASMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new CASMailbox(dataObject);
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06005181 RID: 20865 RVA: 0x0012DB69 File Offset: 0x0012BD69
		protected override IEnumerable<PropertyInfo> CloneableProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = CASMailbox.cloneableProps) == null)
				{
					result = (CASMailbox.cloneableProps = ADPresentationObject.GetCloneableProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06005182 RID: 20866 RVA: 0x0012DB80 File Offset: 0x0012BD80
		protected override IEnumerable<PropertyInfo> CloneableOnceProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = CASMailbox.cloneableOnceProps) == null)
				{
					result = (CASMailbox.cloneableOnceProps = ADPresentationObject.GetCloneableOnceProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x0012DB97 File Offset: 0x0012BD97
		protected override IEnumerable<PropertyInfo> CloneableEnabledStateProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = CASMailbox.cloneableEnabledStateProps) == null)
				{
					result = (CASMailbox.cloneableEnabledStateProps = ADPresentationObject.GetCloneableEnabledStateProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x0012DBAE File Offset: 0x0012BDAE
		// (set) Token: 0x06005185 RID: 20869 RVA: 0x0012DBC0 File Offset: 0x0012BDC0
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[CASMailboxSchema.EmailAddresses];
			}
			set
			{
				this[CASMailboxSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x0012DBCE File Offset: 0x0012BDCE
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[CASMailboxSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x0012DBE0 File Offset: 0x0012BDE0
		public string LinkedMasterAccount
		{
			get
			{
				return (string)this[CASMailboxSchema.LinkedMasterAccount];
			}
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x0012DBF2 File Offset: 0x0012BDF2
		// (set) Token: 0x06005189 RID: 20873 RVA: 0x0012DC04 File Offset: 0x0012BE04
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[CASMailboxSchema.PrimarySmtpAddress];
			}
			set
			{
				this[CASMailboxSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x0012DC17 File Offset: 0x0012BE17
		// (set) Token: 0x0600518B RID: 20875 RVA: 0x0012DC29 File Offset: 0x0012BE29
		[Parameter(Mandatory = false)]
		public string SamAccountName
		{
			get
			{
				return (string)this[CASMailboxSchema.SamAccountName];
			}
			set
			{
				this[CASMailboxSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x0600518C RID: 20876 RVA: 0x0012DC37 File Offset: 0x0012BE37
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[CASMailboxSchema.ServerLegacyDN];
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x0600518D RID: 20877 RVA: 0x0012DC49 File Offset: 0x0012BE49
		public string ServerName
		{
			get
			{
				return (string)this[CASMailboxSchema.ServerName];
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x0600518E RID: 20878 RVA: 0x0012DC5B File Offset: 0x0012BE5B
		// (set) Token: 0x0600518F RID: 20879 RVA: 0x0012DC6D File Offset: 0x0012BE6D
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[CASMailboxSchema.DisplayName];
			}
			set
			{
				this[CASMailboxSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x0012DC7B File Offset: 0x0012BE7B
		// (set) Token: 0x06005191 RID: 20881 RVA: 0x0012DC8D File Offset: 0x0012BE8D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ActiveSyncAllowedDeviceIDs
		{
			get
			{
				return (MultiValuedProperty<string>)this[CASMailboxSchema.ActiveSyncAllowedDeviceIDs];
			}
			set
			{
				this[CASMailboxSchema.ActiveSyncAllowedDeviceIDs] = value;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06005192 RID: 20882 RVA: 0x0012DC9B File Offset: 0x0012BE9B
		// (set) Token: 0x06005193 RID: 20883 RVA: 0x0012DCAD File Offset: 0x0012BEAD
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ActiveSyncBlockedDeviceIDs
		{
			get
			{
				return (MultiValuedProperty<string>)this[CASMailboxSchema.ActiveSyncBlockedDeviceIDs];
			}
			set
			{
				this[CASMailboxSchema.ActiveSyncBlockedDeviceIDs] = value;
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x0012DCBB File Offset: 0x0012BEBB
		// (set) Token: 0x06005195 RID: 20885 RVA: 0x0012DCE1 File Offset: 0x0012BEE1
		public ADObjectId ActiveSyncMailboxPolicy
		{
			get
			{
				if (this[CASMailboxSchema.ActiveSyncMailboxPolicy] != null)
				{
					return (ADObjectId)this[CASMailboxSchema.ActiveSyncMailboxPolicy];
				}
				return this.activeSyncMailboxPolicy;
			}
			set
			{
				this[CASMailboxSchema.ActiveSyncMailboxPolicy] = value;
			}
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x0012DCEF File Offset: 0x0012BEEF
		internal void SetActiveSyncMailboxPolicyLocally(ADObjectId activeSyncMailboxPolicy)
		{
			this.activeSyncMailboxPolicy = activeSyncMailboxPolicy;
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x0012DCF8 File Offset: 0x0012BEF8
		// (set) Token: 0x06005198 RID: 20888 RVA: 0x0012DD14 File Offset: 0x0012BF14
		public bool ActiveSyncMailboxPolicyIsDefaulted
		{
			get
			{
				return (bool)(this[CASMailboxSchema.ActiveSyncMailboxPolicyIsDefaulted] ?? false);
			}
			internal set
			{
				this[CASMailboxSchema.ActiveSyncMailboxPolicyIsDefaulted] = value;
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x0012DD28 File Offset: 0x0012BF28
		// (set) Token: 0x0600519A RID: 20890 RVA: 0x0012DD4E File Offset: 0x0012BF4E
		[Parameter(Mandatory = false)]
		public bool ActiveSyncDebugLogging
		{
			get
			{
				return this.activeSyncDebugLogging ?? false;
			}
			set
			{
				this.activeSyncDebugLogging = new bool?(value);
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x0012DD5C File Offset: 0x0012BF5C
		internal bool ActiveSyncDebugLoggingSpecified
		{
			get
			{
				return this.activeSyncDebugLogging != null;
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x0600519C RID: 20892 RVA: 0x0012DD69 File Offset: 0x0012BF69
		// (set) Token: 0x0600519D RID: 20893 RVA: 0x0012DD7B File Offset: 0x0012BF7B
		[Parameter]
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public bool ActiveSyncEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.ActiveSyncEnabled];
			}
			set
			{
				this[CASMailboxSchema.ActiveSyncEnabled] = value;
			}
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x0600519E RID: 20894 RVA: 0x0012DD8E File Offset: 0x0012BF8E
		public bool HasActiveSyncDevicePartnership
		{
			get
			{
				return (bool)this[CASMailboxSchema.HasActiveSyncDevicePartnership];
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x0012DDA0 File Offset: 0x0012BFA0
		// (set) Token: 0x060051A0 RID: 20896 RVA: 0x0012DDA8 File Offset: 0x0012BFA8
		public string ExternalImapSettings { get; internal set; }

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x060051A1 RID: 20897 RVA: 0x0012DDB1 File Offset: 0x0012BFB1
		// (set) Token: 0x060051A2 RID: 20898 RVA: 0x0012DDB9 File Offset: 0x0012BFB9
		public string InternalImapSettings { get; internal set; }

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x060051A3 RID: 20899 RVA: 0x0012DDC2 File Offset: 0x0012BFC2
		// (set) Token: 0x060051A4 RID: 20900 RVA: 0x0012DDCA File Offset: 0x0012BFCA
		public string ExternalPopSettings { get; internal set; }

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x060051A5 RID: 20901 RVA: 0x0012DDD3 File Offset: 0x0012BFD3
		// (set) Token: 0x060051A6 RID: 20902 RVA: 0x0012DDDB File Offset: 0x0012BFDB
		public string InternalPopSettings { get; internal set; }

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0012DDE4 File Offset: 0x0012BFE4
		// (set) Token: 0x060051A8 RID: 20904 RVA: 0x0012DDEC File Offset: 0x0012BFEC
		public string ExternalSmtpSettings { get; internal set; }

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0012DDF5 File Offset: 0x0012BFF5
		// (set) Token: 0x060051AA RID: 20906 RVA: 0x0012DDFD File Offset: 0x0012BFFD
		public string InternalSmtpSettings { get; internal set; }

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x0012DE06 File Offset: 0x0012C006
		// (set) Token: 0x060051AC RID: 20908 RVA: 0x0012DE18 File Offset: 0x0012C018
		[ProvisionalCloneOnce(CloneSet.CloneExtendedSet)]
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[CASMailboxSchema.OwaMailboxPolicy];
			}
			set
			{
				this[CASMailboxSchema.OwaMailboxPolicy] = value;
			}
		}

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x060051AD RID: 20909 RVA: 0x0012DE26 File Offset: 0x0012C026
		// (set) Token: 0x060051AE RID: 20910 RVA: 0x0012DE38 File Offset: 0x0012C038
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter]
		public bool OWAEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.OWAEnabled];
			}
			set
			{
				this[CASMailboxSchema.OWAEnabled] = value;
			}
		}

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x0012DE4B File Offset: 0x0012C04B
		// (set) Token: 0x060051B0 RID: 20912 RVA: 0x0012DE5D File Offset: 0x0012C05D
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public bool OWAforDevicesEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.OWAforDevicesEnabled];
			}
			set
			{
				this[CASMailboxSchema.OWAforDevicesEnabled] = value;
			}
		}

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x060051B1 RID: 20913 RVA: 0x0012DE70 File Offset: 0x0012C070
		// (set) Token: 0x060051B2 RID: 20914 RVA: 0x0012DE82 File Offset: 0x0012C082
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public bool ECPEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.ECPEnabled];
			}
			set
			{
				this[CASMailboxSchema.ECPEnabled] = value;
			}
		}

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x0012DE95 File Offset: 0x0012C095
		// (set) Token: 0x060051B4 RID: 20916 RVA: 0x0012DEA7 File Offset: 0x0012C0A7
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public bool PopEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.PopEnabled];
			}
			set
			{
				this[CASMailboxSchema.PopEnabled] = value;
			}
		}

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x060051B5 RID: 20917 RVA: 0x0012DEBA File Offset: 0x0012C0BA
		// (set) Token: 0x060051B6 RID: 20918 RVA: 0x0012DECC File Offset: 0x0012C0CC
		[Parameter(Mandatory = false)]
		public bool PopUseProtocolDefaults
		{
			get
			{
				return (bool)this[CASMailboxSchema.PopUseProtocolDefaults];
			}
			set
			{
				this[CASMailboxSchema.PopUseProtocolDefaults] = value;
			}
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x060051B7 RID: 20919 RVA: 0x0012DEDF File Offset: 0x0012C0DF
		// (set) Token: 0x060051B8 RID: 20920 RVA: 0x0012DEF1 File Offset: 0x0012C0F1
		[Parameter(Mandatory = false)]
		public MimeTextFormat PopMessagesRetrievalMimeFormat
		{
			get
			{
				return (MimeTextFormat)this[CASMailboxSchema.PopMessagesRetrievalMimeFormat];
			}
			set
			{
				this[CASMailboxSchema.PopMessagesRetrievalMimeFormat] = value;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x060051B9 RID: 20921 RVA: 0x0012DF04 File Offset: 0x0012C104
		// (set) Token: 0x060051BA RID: 20922 RVA: 0x0012DF16 File Offset: 0x0012C116
		[Parameter(Mandatory = false)]
		public bool PopEnableExactRFC822Size
		{
			get
			{
				return (bool)this[CASMailboxSchema.PopEnableExactRFC822Size];
			}
			set
			{
				this[CASMailboxSchema.PopEnableExactRFC822Size] = value;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x0012DF29 File Offset: 0x0012C129
		// (set) Token: 0x060051BC RID: 20924 RVA: 0x0012DF36 File Offset: 0x0012C136
		internal bool PopProtocolLoggingEnabled
		{
			get
			{
				return CASMailbox.IsMailboxProtocolLoggingEnabled(this, CASMailboxSchema.PopProtocolLoggingEnabled);
			}
			set
			{
				CASMailbox.SetMailboxProtocolLoggingEnabled(this, CASMailboxSchema.PopProtocolLoggingEnabled, value);
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x060051BD RID: 20925 RVA: 0x0012DF44 File Offset: 0x0012C144
		// (set) Token: 0x060051BE RID: 20926 RVA: 0x0012DF56 File Offset: 0x0012C156
		[Parameter(Mandatory = false)]
		public bool PopSuppressReadReceipt
		{
			get
			{
				return (bool)this[CASMailboxSchema.PopSuppressReadReceipt];
			}
			set
			{
				this[CASMailboxSchema.PopSuppressReadReceipt] = value;
			}
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x0012DF69 File Offset: 0x0012C169
		// (set) Token: 0x060051C0 RID: 20928 RVA: 0x0012DF7B File Offset: 0x0012C17B
		[Parameter(Mandatory = false)]
		public bool PopForceICalForCalendarRetrievalOption
		{
			get
			{
				return (bool)this[CASMailboxSchema.PopForceICalForCalendarRetrievalOption];
			}
			set
			{
				this[CASMailboxSchema.PopForceICalForCalendarRetrievalOption] = value;
			}
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x0012DF8E File Offset: 0x0012C18E
		// (set) Token: 0x060051C2 RID: 20930 RVA: 0x0012DFA0 File Offset: 0x0012C1A0
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public bool ImapEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.ImapEnabled];
			}
			set
			{
				this[CASMailboxSchema.ImapEnabled] = value;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x0012DFB3 File Offset: 0x0012C1B3
		// (set) Token: 0x060051C4 RID: 20932 RVA: 0x0012DFC5 File Offset: 0x0012C1C5
		[Parameter(Mandatory = false)]
		public bool ImapUseProtocolDefaults
		{
			get
			{
				return (bool)this[CASMailboxSchema.ImapUseProtocolDefaults];
			}
			set
			{
				this[CASMailboxSchema.ImapUseProtocolDefaults] = value;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x0012DFD8 File Offset: 0x0012C1D8
		// (set) Token: 0x060051C6 RID: 20934 RVA: 0x0012DFEA File Offset: 0x0012C1EA
		[Parameter(Mandatory = false)]
		public MimeTextFormat ImapMessagesRetrievalMimeFormat
		{
			get
			{
				return (MimeTextFormat)this[CASMailboxSchema.ImapMessagesRetrievalMimeFormat];
			}
			set
			{
				this[CASMailboxSchema.ImapMessagesRetrievalMimeFormat] = value;
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x060051C7 RID: 20935 RVA: 0x0012DFFD File Offset: 0x0012C1FD
		// (set) Token: 0x060051C8 RID: 20936 RVA: 0x0012E00F File Offset: 0x0012C20F
		[Parameter(Mandatory = false)]
		public bool ImapEnableExactRFC822Size
		{
			get
			{
				return (bool)this[CASMailboxSchema.ImapEnableExactRFC822Size];
			}
			set
			{
				this[CASMailboxSchema.ImapEnableExactRFC822Size] = value;
			}
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x0012E022 File Offset: 0x0012C222
		// (set) Token: 0x060051CA RID: 20938 RVA: 0x0012E02F File Offset: 0x0012C22F
		internal bool ImapProtocolLoggingEnabled
		{
			get
			{
				return CASMailbox.IsMailboxProtocolLoggingEnabled(this, CASMailboxSchema.ImapProtocolLoggingEnabled);
			}
			set
			{
				CASMailbox.SetMailboxProtocolLoggingEnabled(this, CASMailboxSchema.ImapProtocolLoggingEnabled, value);
			}
		}

		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x060051CB RID: 20939 RVA: 0x0012E03D File Offset: 0x0012C23D
		// (set) Token: 0x060051CC RID: 20940 RVA: 0x0012E04F File Offset: 0x0012C24F
		[Parameter(Mandatory = false)]
		public bool ImapSuppressReadReceipt
		{
			get
			{
				return (bool)this[CASMailboxSchema.ImapSuppressReadReceipt];
			}
			set
			{
				this[CASMailboxSchema.ImapSuppressReadReceipt] = value;
			}
		}

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x0012E062 File Offset: 0x0012C262
		// (set) Token: 0x060051CE RID: 20942 RVA: 0x0012E074 File Offset: 0x0012C274
		[Parameter(Mandatory = false)]
		public bool ImapForceICalForCalendarRetrievalOption
		{
			get
			{
				return (bool)this[CASMailboxSchema.ImapForceICalForCalendarRetrievalOption];
			}
			set
			{
				this[CASMailboxSchema.ImapForceICalForCalendarRetrievalOption] = value;
			}
		}

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x0012E087 File Offset: 0x0012C287
		// (set) Token: 0x060051D0 RID: 20944 RVA: 0x0012E099 File Offset: 0x0012C299
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public bool MAPIEnabled
		{
			get
			{
				return (bool)this[CASMailboxSchema.MAPIEnabled];
			}
			set
			{
				this[CASMailboxSchema.MAPIEnabled] = value;
			}
		}

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x0012E0AC File Offset: 0x0012C2AC
		// (set) Token: 0x060051D2 RID: 20946 RVA: 0x0012E0BE File Offset: 0x0012C2BE
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public bool? MapiHttpEnabled
		{
			get
			{
				return (bool?)this[CASMailboxSchema.MapiHttpEnabled];
			}
			set
			{
				this[CASMailboxSchema.MapiHttpEnabled] = value;
			}
		}

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x0012E0D1 File Offset: 0x0012C2D1
		// (set) Token: 0x060051D4 RID: 20948 RVA: 0x0012E0E3 File Offset: 0x0012C2E3
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public bool MAPIBlockOutlookNonCachedMode
		{
			get
			{
				return (bool)this[CASMailboxSchema.MAPIBlockOutlookNonCachedMode];
			}
			set
			{
				this[CASMailboxSchema.MAPIBlockOutlookNonCachedMode] = value;
			}
		}

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x060051D5 RID: 20949 RVA: 0x0012E0F6 File Offset: 0x0012C2F6
		// (set) Token: 0x060051D6 RID: 20950 RVA: 0x0012E108 File Offset: 0x0012C308
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public string MAPIBlockOutlookVersions
		{
			get
			{
				return (string)this[CASMailboxSchema.MAPIBlockOutlookVersions];
			}
			set
			{
				this[CASMailboxSchema.MAPIBlockOutlookVersions] = value;
			}
		}

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x0012E116 File Offset: 0x0012C316
		// (set) Token: 0x060051D8 RID: 20952 RVA: 0x0012E128 File Offset: 0x0012C328
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public bool MAPIBlockOutlookRpcHttp
		{
			get
			{
				return (bool)this[CASMailboxSchema.MAPIBlockOutlookRpcHttp];
			}
			set
			{
				this[CASMailboxSchema.MAPIBlockOutlookRpcHttp] = value;
			}
		}

		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x0012E13B File Offset: 0x0012C33B
		// (set) Token: 0x060051DA RID: 20954 RVA: 0x0012E14D File Offset: 0x0012C34D
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public bool MAPIBlockOutlookExternalConnectivity
		{
			get
			{
				return (bool)this[CASMailboxSchema.MAPIBlockOutlookExternalConnectivity];
			}
			set
			{
				this[CASMailboxSchema.MAPIBlockOutlookExternalConnectivity] = value;
			}
		}

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x0012E160 File Offset: 0x0012C360
		// (set) Token: 0x060051DC RID: 20956 RVA: 0x0012E177 File Offset: 0x0012C377
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public bool? EwsEnabled
		{
			get
			{
				return CASMailboxHelper.ToBooleanNullable((int?)this[CASMailboxSchema.EwsEnabled]);
			}
			set
			{
				this[CASMailboxSchema.EwsEnabled] = CASMailboxHelper.ToInt32Nullable(value);
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x060051DD RID: 20957 RVA: 0x0012E18F File Offset: 0x0012C38F
		// (set) Token: 0x060051DE RID: 20958 RVA: 0x0012E1A1 File Offset: 0x0012C3A1
		[Parameter(Mandatory = false)]
		public bool? EwsAllowOutlook
		{
			get
			{
				return (bool?)this[CASMailboxSchema.EwsAllowOutlook];
			}
			set
			{
				this[CASMailboxSchema.EwsAllowOutlook] = value;
			}
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x0012E1B4 File Offset: 0x0012C3B4
		// (set) Token: 0x060051E0 RID: 20960 RVA: 0x0012E1C6 File Offset: 0x0012C3C6
		[Parameter(Mandatory = false)]
		public bool? EwsAllowMacOutlook
		{
			get
			{
				return (bool?)this[CASMailboxSchema.EwsAllowMacOutlook];
			}
			set
			{
				this[CASMailboxSchema.EwsAllowMacOutlook] = value;
			}
		}

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x0012E1D9 File Offset: 0x0012C3D9
		// (set) Token: 0x060051E2 RID: 20962 RVA: 0x0012E1EB File Offset: 0x0012C3EB
		[Parameter(Mandatory = false)]
		public bool? EwsAllowEntourage
		{
			get
			{
				return (bool?)this[CASMailboxSchema.EwsAllowEntourage];
			}
			set
			{
				this[CASMailboxSchema.EwsAllowEntourage] = value;
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x060051E3 RID: 20963 RVA: 0x0012E1FE File Offset: 0x0012C3FE
		// (set) Token: 0x060051E4 RID: 20964 RVA: 0x0012E210 File Offset: 0x0012C410
		[Parameter(Mandatory = false)]
		public EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
		{
			get
			{
				return (EwsApplicationAccessPolicy?)this[CASMailboxSchema.EwsApplicationAccessPolicy];
			}
			set
			{
				this[CASMailboxSchema.EwsApplicationAccessPolicy] = value;
			}
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x0012E224 File Offset: 0x0012C424
		// (set) Token: 0x060051E6 RID: 20966 RVA: 0x0012E26B File Offset: 0x0012C46B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EwsAllowList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[CASMailboxSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceAllowList)
				{
					return (MultiValuedProperty<string>)this[CASMailboxSchema.EwsExceptions];
				}
				return null;
			}
			set
			{
				this[CASMailboxSchema.EwsExceptions] = value;
				this.ewsAllowListSpecified = true;
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x060051E7 RID: 20967 RVA: 0x0012E280 File Offset: 0x0012C480
		// (set) Token: 0x060051E8 RID: 20968 RVA: 0x0012E2C8 File Offset: 0x0012C4C8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EwsBlockList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[CASMailboxSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceBlockList)
				{
					return (MultiValuedProperty<string>)this[CASMailboxSchema.EwsExceptions];
				}
				return null;
			}
			set
			{
				this[CASMailboxSchema.EwsExceptions] = value;
				this.ewsBlockListSpecified = true;
			}
		}

		// Token: 0x17001AFD RID: 6909
		// (set) Token: 0x060051E9 RID: 20969 RVA: 0x0012E2DD File Offset: 0x0012C4DD
		internal MultiValuedProperty<string> EwsExceptions
		{
			set
			{
				this[CASMailboxSchema.EwsExceptions] = value;
			}
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x0012E2EB File Offset: 0x0012C4EB
		internal bool EwsAllowListSpecified
		{
			get
			{
				return this.ewsAllowListSpecified;
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x060051EB RID: 20971 RVA: 0x0012E2F3 File Offset: 0x0012C4F3
		internal bool EwsBlockListSpecified
		{
			get
			{
				return this.ewsBlockListSpecified;
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x0012E2FB File Offset: 0x0012C4FB
		// (set) Token: 0x060051ED RID: 20973 RVA: 0x0012E30D File Offset: 0x0012C50D
		[Parameter(Mandatory = false)]
		public bool ShowGalAsDefaultView
		{
			get
			{
				return Convert.ToBoolean(this[CASMailboxSchema.AddressBookFlags]);
			}
			set
			{
				this[CASMailboxSchema.AddressBookFlags] = Convert.ToInt32(value);
			}
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x060051EE RID: 20974 RVA: 0x0012E325 File Offset: 0x0012C525
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04003762 RID: 14178
		private static IEnumerable<PropertyInfo> cloneableProps;

		// Token: 0x04003763 RID: 14179
		private static IEnumerable<PropertyInfo> cloneableOnceProps;

		// Token: 0x04003764 RID: 14180
		private static IEnumerable<PropertyInfo> cloneableEnabledStateProps;

		// Token: 0x04003765 RID: 14181
		private static CASMailboxSchema schema = ObjectSchema.GetInstance<CASMailboxSchema>();

		// Token: 0x04003766 RID: 14182
		public static ExDateTime MailboxProtocolLoggingInitialTime = new ExDateTime(ExTimeZone.UtcTimeZone, 2009, 1, 1);

		// Token: 0x04003767 RID: 14183
		public static int MailboxProtocolLoggingLength = 4320;

		// Token: 0x04003768 RID: 14184
		private bool ewsAllowListSpecified;

		// Token: 0x04003769 RID: 14185
		private bool ewsBlockListSpecified;

		// Token: 0x0400376A RID: 14186
		private ADObjectId activeSyncMailboxPolicy;

		// Token: 0x0400376B RID: 14187
		private bool? activeSyncDebugLogging = null;
	}
}
