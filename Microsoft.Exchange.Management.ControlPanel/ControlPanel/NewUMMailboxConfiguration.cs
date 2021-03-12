using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C9 RID: 1225
	[DataContract]
	public class NewUMMailboxConfiguration : UMMailboxRow
	{
		// Token: 0x06003BFA RID: 15354 RVA: 0x000B4C63 File Offset: 0x000B2E63
		public NewUMMailboxConfiguration(UMMailbox umMailbox) : base(umMailbox)
		{
		}

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000B4C6C File Offset: 0x000B2E6C
		// (set) Token: 0x06003BFC RID: 15356 RVA: 0x000B4C74 File Offset: 0x000B2E74
		internal UMMailboxPolicy Policy
		{
			get
			{
				return this.policy;
			}
			set
			{
				if (value != null)
				{
					this.policy = value;
					this.dialPlan = this.policy.GetDialPlan();
				}
			}
		}

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000B4C91 File Offset: 0x000B2E91
		// (set) Token: 0x06003BFE RID: 15358 RVA: 0x000B4CBD File Offset: 0x000B2EBD
		[DataMember]
		public string Extension
		{
			get
			{
				if (base.UMMailbox.Extensions.Count <= 0)
				{
					return string.Empty;
				}
				return base.UMMailbox.Extensions[0];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000B4CD1 File Offset: 0x000B2ED1
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x000B4CE6 File Offset: 0x000B2EE6
		[DataMember]
		public int MinPinLength
		{
			get
			{
				return this.GetValue<int>(() => this.Policy.MinPINLength, 6);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x000B4CFA File Offset: 0x000B2EFA
		// (set) Token: 0x06003C02 RID: 15362 RVA: 0x000B4D10 File Offset: 0x000B2F10
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

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x06003C03 RID: 15363 RVA: 0x000B4D32 File Offset: 0x000B2F32
		// (set) Token: 0x06003C04 RID: 15364 RVA: 0x000B4D4B File Offset: 0x000B2F4B
		[DataMember]
		public string SipResourceIdentifier
		{
			get
			{
				return this.GetValue<string>(delegate
				{
					if (!this.IsSipDialPlan)
					{
						return string.Empty;
					}
					return base.UMMailbox.SIPResourceIdentifier;
				}, string.Empty);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x000B4D6D File Offset: 0x000B2F6D
		// (set) Token: 0x06003C06 RID: 15366 RVA: 0x000B4D86 File Offset: 0x000B2F86
		[DataMember]
		public string E164ResourceIdentifier
		{
			get
			{
				return this.GetValue<string>(delegate
				{
					if (!this.IsE164DialPlan)
					{
						return string.Empty;
					}
					return base.UMMailbox.SIPResourceIdentifier;
				}, string.Empty);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x06003C07 RID: 15367 RVA: 0x000B4D9D File Offset: 0x000B2F9D
		// (set) Token: 0x06003C08 RID: 15368 RVA: 0x000B4DB2 File Offset: 0x000B2FB2
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

		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x06003C09 RID: 15369 RVA: 0x000B4DC9 File Offset: 0x000B2FC9
		// (set) Token: 0x06003C0A RID: 15370 RVA: 0x000B4DDE File Offset: 0x000B2FDE
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

		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000B4DE5 File Offset: 0x000B2FE5
		// (set) Token: 0x06003C0C RID: 15372 RVA: 0x000B4DE8 File Offset: 0x000B2FE8
		[DataMember]
		public bool PinExpired
		{
			get
			{
				return true;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x000B4DEF File Offset: 0x000B2FEF
		// (set) Token: 0x06003C0E RID: 15374 RVA: 0x000B4DF6 File Offset: 0x000B2FF6
		[DataMember]
		public string ManualPin
		{
			get
			{
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000B4DFD File Offset: 0x000B2FFD
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000B4E04 File Offset: 0x000B3004
		[DataMember]
		public string AutoPin
		{
			get
			{
				return "true";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x000B4E0B File Offset: 0x000B300B
		private TResult GetValue<TResult>(Func<TResult> d1, TResult defaultValue)
		{
			if (this.policy == null | this.dialPlan == null)
			{
				return defaultValue;
			}
			return d1();
		}

		// Token: 0x0400278C RID: 10124
		private UMDialPlan dialPlan;

		// Token: 0x0400278D RID: 10125
		private UMMailboxPolicy policy;
	}
}
