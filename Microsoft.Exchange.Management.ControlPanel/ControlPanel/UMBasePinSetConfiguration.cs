using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Tasks.UM;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C2 RID: 1218
	[KnownType(typeof(SetUMMailboxConfiguration))]
	[KnownType(typeof(SetUMMailboxPinConfiguration))]
	[DataContract]
	public abstract class UMBasePinSetConfiguration : UMMailboxRow
	{
		// Token: 0x06003BDB RID: 15323 RVA: 0x000B4A11 File Offset: 0x000B2C11
		public UMBasePinSetConfiguration(UMMailbox umMailbox) : base(umMailbox)
		{
		}

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x000B4A1A File Offset: 0x000B2C1A
		// (set) Token: 0x06003BDD RID: 15325 RVA: 0x000B4A22 File Offset: 0x000B2C22
		public UMMailboxPin UMMailboxPin { get; set; }

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x000B4A2B File Offset: 0x000B2C2B
		// (set) Token: 0x06003BDF RID: 15327 RVA: 0x000B4A42 File Offset: 0x000B2C42
		[DataMember]
		public bool AccountLockedOut
		{
			get
			{
				return this.UMMailboxPin != null && this.UMMailboxPin.LockedOut;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000B4A49 File Offset: 0x000B2C49
		// (set) Token: 0x06003BE1 RID: 15329 RVA: 0x000B4A64 File Offset: 0x000B2C64
		[DataMember]
		public string AccountLockedOutStatus
		{
			get
			{
				return this.AccountLockedOut ? Strings.EditUMMailboxAccountLockedOutStatus : Strings.EditUMMailboxAccountActiveStatus;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000B4A6B File Offset: 0x000B2C6B
		// (set) Token: 0x06003BE3 RID: 15331 RVA: 0x000B4A85 File Offset: 0x000B2C85
		[DataMember]
		public string AccountLockedOutMessage
		{
			get
			{
				if (!this.AccountLockedOut)
				{
					return string.Empty;
				}
				return Strings.EditUMMailboxLockedoutAction;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
