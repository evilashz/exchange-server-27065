using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200051F RID: 1311
	[DataContract]
	[KnownType(typeof(ManagementRoleRow))]
	public class ManagementRoleRow : BaseRow
	{
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000BD2B0 File Offset: 0x000BB4B0
		public ManagementRoleRow(ExchangeRole exchangeRole) : base(exchangeRole)
		{
			this.ExchangeRole = exchangeRole;
		}

		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000BD2C0 File Offset: 0x000BB4C0
		// (set) Token: 0x06003EB3 RID: 16051 RVA: 0x000BD2C8 File Offset: 0x000BB4C8
		protected ExchangeRole ExchangeRole { get; set; }

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06003EB4 RID: 16052 RVA: 0x000BD2D1 File Offset: 0x000BB4D1
		// (set) Token: 0x06003EB5 RID: 16053 RVA: 0x000BD2DE File Offset: 0x000BB4DE
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.ExchangeRole.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000BD2E5 File Offset: 0x000BB4E5
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x000BD2F2 File Offset: 0x000BB4F2
		[DataMember]
		public string Name
		{
			get
			{
				return this.ExchangeRole.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000BD2F9 File Offset: 0x000BB4F9
		// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x000BD306 File Offset: 0x000BB506
		[DataMember]
		public bool IsEndUserRole
		{
			get
			{
				return this.ExchangeRole.IsEndUserRole;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
