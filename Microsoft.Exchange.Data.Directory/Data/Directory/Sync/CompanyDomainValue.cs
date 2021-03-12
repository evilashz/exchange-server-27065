using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000820 RID: 2080
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(CompanyVerifiedDomainValue))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class CompanyDomainValue : IComparable
	{
		// Token: 0x060066DA RID: 26330 RVA: 0x0016BD6E File Offset: 0x00169F6E
		public override string ToString()
		{
			return string.Format("nameField={0} liveTypeField={1} capabilitiesField={2}", this.nameField, this.liveTypeField, this.capabilitiesField);
		}

		// Token: 0x1700244D RID: 9293
		// (get) Token: 0x060066DB RID: 26331 RVA: 0x0016BD96 File Offset: 0x00169F96
		public AcceptedDomainType AcceptedDomainType
		{
			get
			{
				if ((this.Capabilities & 4) != 4)
				{
					return AcceptedDomainType.Authoritative;
				}
				return AcceptedDomainType.InternalRelay;
			}
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x0016BDA8 File Offset: 0x00169FA8
		public int CompareTo(object other)
		{
			if (other is CompanyDomainValue)
			{
				CompanyDomainValue companyDomainValue = other as CompanyDomainValue;
				int num = string.IsNullOrEmpty(this.Name) ? 0 : this.Name.Length;
				int num2 = string.IsNullOrEmpty(companyDomainValue.Name) ? 0 : companyDomainValue.Name.Length;
				return num - num2;
			}
			throw new ArgumentException("other is not a CompanyDomainValue");
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x0016BE0A File Offset: 0x0016A00A
		public CompanyDomainValue()
		{
			this.liveTypeField = LiveNamespaceType.None;
			this.capabilitiesField = 0;
		}

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x0016BE20 File Offset: 0x0016A020
		// (set) Token: 0x060066DF RID: 26335 RVA: 0x0016BE28 File Offset: 0x0016A028
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x0016BE31 File Offset: 0x0016A031
		// (set) Token: 0x060066E1 RID: 26337 RVA: 0x0016BE39 File Offset: 0x0016A039
		[XmlAttribute]
		[DefaultValue(LiveNamespaceType.None)]
		public LiveNamespaceType LiveType
		{
			get
			{
				return this.liveTypeField;
			}
			set
			{
				this.liveTypeField = value;
			}
		}

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x060066E2 RID: 26338 RVA: 0x0016BE42 File Offset: 0x0016A042
		// (set) Token: 0x060066E3 RID: 26339 RVA: 0x0016BE4A File Offset: 0x0016A04A
		[XmlAttribute(DataType = "hexBinary")]
		public byte[] LiveNetId
		{
			get
			{
				return this.liveNetIdField;
			}
			set
			{
				this.liveNetIdField = value;
			}
		}

		// Token: 0x17002451 RID: 9297
		// (get) Token: 0x060066E4 RID: 26340 RVA: 0x0016BE53 File Offset: 0x0016A053
		// (set) Token: 0x060066E5 RID: 26341 RVA: 0x0016BE5B File Offset: 0x0016A05B
		[XmlAttribute]
		[DefaultValue(0)]
		public int Capabilities
		{
			get
			{
				return this.capabilitiesField;
			}
			set
			{
				this.capabilitiesField = value;
			}
		}

		// Token: 0x17002452 RID: 9298
		// (get) Token: 0x060066E6 RID: 26342 RVA: 0x0016BE64 File Offset: 0x0016A064
		// (set) Token: 0x060066E7 RID: 26343 RVA: 0x0016BE6C File Offset: 0x0016A06C
		[XmlAttribute]
		public string MailTargetKey
		{
			get
			{
				return this.mailTargetKeyField;
			}
			set
			{
				this.mailTargetKeyField = value;
			}
		}

		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x060066E8 RID: 26344 RVA: 0x0016BE75 File Offset: 0x0016A075
		// (set) Token: 0x060066E9 RID: 26345 RVA: 0x0016BE7D File Offset: 0x0016A07D
		[XmlAttribute]
		public int PasswordValidityPeriodDays
		{
			get
			{
				return this.passwordValidityPeriodDaysField;
			}
			set
			{
				this.passwordValidityPeriodDaysField = value;
			}
		}

		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x060066EA RID: 26346 RVA: 0x0016BE86 File Offset: 0x0016A086
		// (set) Token: 0x060066EB RID: 26347 RVA: 0x0016BE8E File Offset: 0x0016A08E
		[XmlIgnore]
		public bool PasswordValidityPeriodDaysSpecified
		{
			get
			{
				return this.passwordValidityPeriodDaysFieldSpecified;
			}
			set
			{
				this.passwordValidityPeriodDaysFieldSpecified = value;
			}
		}

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x060066EC RID: 26348 RVA: 0x0016BE97 File Offset: 0x0016A097
		// (set) Token: 0x060066ED RID: 26349 RVA: 0x0016BE9F File Offset: 0x0016A09F
		[XmlAttribute]
		public int PasswordNotificationWindowDays
		{
			get
			{
				return this.passwordNotificationWindowDaysField;
			}
			set
			{
				this.passwordNotificationWindowDaysField = value;
			}
		}

		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x060066EE RID: 26350 RVA: 0x0016BEA8 File Offset: 0x0016A0A8
		// (set) Token: 0x060066EF RID: 26351 RVA: 0x0016BEB0 File Offset: 0x0016A0B0
		[XmlIgnore]
		public bool PasswordNotificationWindowDaysSpecified
		{
			get
			{
				return this.passwordNotificationWindowDaysFieldSpecified;
			}
			set
			{
				this.passwordNotificationWindowDaysFieldSpecified = value;
			}
		}

		// Token: 0x17002457 RID: 9303
		// (get) Token: 0x060066F0 RID: 26352 RVA: 0x0016BEB9 File Offset: 0x0016A0B9
		// (set) Token: 0x060066F1 RID: 26353 RVA: 0x0016BEC1 File Offset: 0x0016A0C1
		[XmlAttribute]
		public string VerificationCode
		{
			get
			{
				return this.verificationCodeField;
			}
			set
			{
				this.verificationCodeField = value;
			}
		}

		// Token: 0x040043DA RID: 17370
		private const int BposInternalRelayFlagValue = 4;

		// Token: 0x040043DB RID: 17371
		private string nameField;

		// Token: 0x040043DC RID: 17372
		private LiveNamespaceType liveTypeField;

		// Token: 0x040043DD RID: 17373
		private byte[] liveNetIdField;

		// Token: 0x040043DE RID: 17374
		private int capabilitiesField;

		// Token: 0x040043DF RID: 17375
		private string mailTargetKeyField;

		// Token: 0x040043E0 RID: 17376
		private int passwordValidityPeriodDaysField;

		// Token: 0x040043E1 RID: 17377
		private bool passwordValidityPeriodDaysFieldSpecified;

		// Token: 0x040043E2 RID: 17378
		private int passwordNotificationWindowDaysField;

		// Token: 0x040043E3 RID: 17379
		private bool passwordNotificationWindowDaysFieldSpecified;

		// Token: 0x040043E4 RID: 17380
		private string verificationCodeField;
	}
}
