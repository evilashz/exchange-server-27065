using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200030C RID: 780
	[DataContract]
	public class QuarantinedDevice : BaseRow
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x0008CC1A File Offset: 0x0008AE1A
		public QuarantinedDevice(MobileDevice device) : base(device)
		{
			this.device = device;
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x0008CC2A File Offset: 0x0008AE2A
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x0008CC46 File Offset: 0x0008AE46
		[DataMember]
		public string UserName
		{
			get
			{
				return this.device.Id.Parent.Parent.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x0008CC4D File Offset: 0x0008AE4D
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x0008CC5A File Offset: 0x0008AE5A
		[DataMember]
		public string DeviceType
		{
			get
			{
				return this.device.DeviceType;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x0008CC61 File Offset: 0x0008AE61
		// (set) Token: 0x06002E68 RID: 11880 RVA: 0x0008CC6E File Offset: 0x0008AE6E
		[DataMember]
		public string DeviceModel
		{
			get
			{
				return this.device.DeviceModel;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x0008CC78 File Offset: 0x0008AE78
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x0008CCB5 File Offset: 0x0008AEB5
		[DataMember]
		public string FirstSyncTime
		{
			get
			{
				if (this.device.FirstSyncTime != null)
				{
					return this.device.FirstSyncTime.UtcToUserDateTimeString();
				}
				return Strings.NotAvailable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002297 RID: 8855
		private readonly MobileDevice device;
	}
}
