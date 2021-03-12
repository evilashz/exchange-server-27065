using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F5 RID: 757
	[DataContract]
	public class ActiveSyncMailboxPolicyRow : BaseRow
	{
		// Token: 0x06002D46 RID: 11590 RVA: 0x0008B090 File Offset: 0x00089290
		public ActiveSyncMailboxPolicyRow(ActiveSyncMailboxPolicy policy) : base(policy)
		{
			this.ActiveSyncMailboxPolicy = policy;
		}

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x0008B0A0 File Offset: 0x000892A0
		// (set) Token: 0x06002D48 RID: 11592 RVA: 0x0008B0A8 File Offset: 0x000892A8
		protected ActiveSyncMailboxPolicy ActiveSyncMailboxPolicy { get; set; }

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x0008B0B1 File Offset: 0x000892B1
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x0008B0E1 File Offset: 0x000892E1
		[DataMember]
		public string DisplayName
		{
			get
			{
				if (this.IsDefault)
				{
					return string.Format(Strings.DefaultEASPolicy, this.ActiveSyncMailboxPolicy.Name);
				}
				return this.ActiveSyncMailboxPolicy.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x0008B0E8 File Offset: 0x000892E8
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x0008B0F5 File Offset: 0x000892F5
		[DataMember]
		public bool IsDefault
		{
			get
			{
				return this.ActiveSyncMailboxPolicy.IsDefault;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x0008B0FC File Offset: 0x000892FC
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x0008B10E File Offset: 0x0008930E
		[DataMember]
		public string Modified
		{
			get
			{
				return this.ActiveSyncMailboxPolicy.WhenChangedUTC.UtcToUserDateTimeString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
