using System;
using System.Net;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public class DatabaseAvailabilityGroupNetworkInterface
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0003743D File Offset: 0x0003563D
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x00037445 File Offset: 0x00035645
		public string NodeName
		{
			get
			{
				return this.m_nodeName;
			}
			set
			{
				this.m_nodeName = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0003744E File Offset: 0x0003564E
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x00037456 File Offset: 0x00035656
		public DatabaseAvailabilityGroupNetworkInterface.InterfaceState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0003745F File Offset: 0x0003565F
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x00037467 File Offset: 0x00035667
		public IPAddress IPAddress
		{
			get
			{
				return this.m_ipAddr;
			}
			set
			{
				this.m_ipAddr = value;
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00037470 File Offset: 0x00035670
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{",
				this.NodeName,
				",",
				this.State.ToString(),
				",",
				this.IPAddress.ToString(),
				"}"
			});
		}

		// Token: 0x04000AEF RID: 2799
		private string m_nodeName;

		// Token: 0x04000AF0 RID: 2800
		private DatabaseAvailabilityGroupNetworkInterface.InterfaceState m_state;

		// Token: 0x04000AF1 RID: 2801
		private IPAddress m_ipAddr;

		// Token: 0x0200020C RID: 524
		public enum InterfaceState
		{
			// Token: 0x04000AF3 RID: 2803
			[LocDescription(DataStrings.IDs.Unknown)]
			Unknown,
			// Token: 0x04000AF4 RID: 2804
			[LocDescription(DataStrings.IDs.Up)]
			Up,
			// Token: 0x04000AF5 RID: 2805
			[LocDescription(DataStrings.IDs.Failed)]
			Failed,
			// Token: 0x04000AF6 RID: 2806
			[LocDescription(DataStrings.IDs.Unreachable)]
			Unreachable,
			// Token: 0x04000AF7 RID: 2807
			[LocDescription(DataStrings.IDs.Unavailable)]
			Unavailable
		}
	}
}
