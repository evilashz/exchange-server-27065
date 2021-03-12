using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004CB RID: 1227
	[DataContract]
	public class SetUMMailboxConfiguration : UMBasePinSetConfiguration
	{
		// Token: 0x06003C26 RID: 15398 RVA: 0x000B4F04 File Offset: 0x000B3104
		public SetUMMailboxConfiguration(UMMailbox umMailbox) : base(umMailbox)
		{
			this.dialPlan = base.UMMailbox.GetDialPlan();
			this.primaryExtension = UMMailbox.GetPrimaryExtension(base.UMMailbox.EmailAddresses, ProxyAddressPrefix.UM);
			this.secondaryExtensions = new List<UMExtension>();
			foreach (ProxyAddress proxyAddress in base.UMMailbox.EmailAddresses)
			{
				string extension;
				string phoneContext;
				string dpName;
				if (!proxyAddress.IsPrimaryAddress && proxyAddress.Prefix == ProxyAddressPrefix.UM && UMMailbox.ExtractExtensionInformation(proxyAddress, out extension, out phoneContext, out dpName))
				{
					this.secondaryExtensions.Add(new UMExtension(extension, phoneContext, dpName));
				}
			}
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000B4FE0 File Offset: 0x000B31E0
		// (set) Token: 0x06003C28 RID: 15400 RVA: 0x000B4FF5 File Offset: 0x000B31F5
		[DataMember]
		public bool IsE164DialPlan
		{
			get
			{
				return this.GetValue<bool>(() => this.dialPlan.URIType == UMUriType.E164, false);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000B500E File Offset: 0x000B320E
		// (set) Token: 0x06003C2A RID: 15402 RVA: 0x000B5027 File Offset: 0x000B3227
		[DataMember]
		public string DialPlanName
		{
			get
			{
				return this.GetValue<string>(() => this.dialPlan.ToIdentity().DisplayName, string.Empty);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x000B5045 File Offset: 0x000B3245
		// (set) Token: 0x06003C2C RID: 15404 RVA: 0x000B505E File Offset: 0x000B325E
		[DataMember]
		public string DialPlanId
		{
			get
			{
				return this.GetValue<string>(() => this.dialPlan.ToIdentity().RawIdentity.ToString(), string.Empty);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000B5075 File Offset: 0x000B3275
		// (set) Token: 0x06003C2E RID: 15406 RVA: 0x000B508A File Offset: 0x000B328A
		[DataMember]
		public bool IsSipDialPlan
		{
			get
			{
				return this.GetValue<bool>(() => this.dialPlan.URIType == UMUriType.SipName, false);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000B509E File Offset: 0x000B329E
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000B50B4 File Offset: 0x000B32B4
		[DataMember]
		public int ExtensionLength
		{
			get
			{
				return this.GetValue<int>(() => this.dialPlan.NumberOfDigitsInExtension, 20);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000B50BB File Offset: 0x000B32BB
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000B50CD File Offset: 0x000B32CD
		[DataMember]
		public Identity UMMailboxPolicy
		{
			get
			{
				return base.UMMailbox.UMMailboxPolicy.ToIdentity();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000B50D4 File Offset: 0x000B32D4
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000B50E1 File Offset: 0x000B32E1
		[DataMember]
		public string OperatorNumber
		{
			get
			{
				return base.UMMailbox.OperatorNumber;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000B50E8 File Offset: 0x000B32E8
		// (set) Token: 0x06003C36 RID: 15414 RVA: 0x000B50F0 File Offset: 0x000B32F0
		[DataMember]
		public string PrimaryExtension
		{
			get
			{
				return this.primaryExtension;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000B50F7 File Offset: 0x000B32F7
		// (set) Token: 0x06003C38 RID: 15416 RVA: 0x000B50FF File Offset: 0x000B32FF
		[DataMember]
		public IEnumerable<UMExtension> SecondaryExtensions
		{
			get
			{
				return this.secondaryExtensions;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000B5108 File Offset: 0x000B3308
		// (set) Token: 0x06003C3A RID: 15418 RVA: 0x000B512E File Offset: 0x000B332E
		[DataMember]
		public string WhenChanged
		{
			get
			{
				return base.UMMailbox.WhenChanged.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000B5135 File Offset: 0x000B3335
		private TResult GetValue<TResult>(Func<TResult> d1, TResult defaultValue)
		{
			if (base.UMMailboxPin == null || this.dialPlan == null)
			{
				return defaultValue;
			}
			return d1();
		}

		// Token: 0x04002790 RID: 10128
		private List<UMExtension> secondaryExtensions;

		// Token: 0x04002791 RID: 10129
		private string primaryExtension;

		// Token: 0x04002792 RID: 10130
		private UMDialPlan dialPlan;
	}
}
