using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x0200012A RID: 298
	[XmlType(TypeName = "PINInfoType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PINInfo : UMRpcResponse
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00025AAB File Offset: 0x00023CAB
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00025AB3 File Offset: 0x00023CB3
		[XmlElement]
		public string PIN
		{
			get
			{
				return this.pin;
			}
			set
			{
				this.pin = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00025ABC File Offset: 0x00023CBC
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00025AC4 File Offset: 0x00023CC4
		[XmlElement]
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00025ACD File Offset: 0x00023CCD
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00025AD5 File Offset: 0x00023CD5
		[XmlElement]
		public bool PinExpired
		{
			get
			{
				return this.pinExpired;
			}
			set
			{
				this.pinExpired = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00025ADE File Offset: 0x00023CDE
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00025AE6 File Offset: 0x00023CE6
		[XmlElement]
		public bool LockedOut
		{
			get
			{
				return this.lockedOut;
			}
			set
			{
				this.lockedOut = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00025AEF File Offset: 0x00023CEF
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00025AF7 File Offset: 0x00023CF7
		[XmlElement]
		public bool FirstTimeUser
		{
			get
			{
				return this.firstTimeUser;
			}
			set
			{
				this.firstTimeUser = value;
			}
		}

		// Token: 0x04000559 RID: 1369
		private string pin;

		// Token: 0x0400055A RID: 1370
		private bool lockedOut;

		// Token: 0x0400055B RID: 1371
		private bool isValid;

		// Token: 0x0400055C RID: 1372
		private bool pinExpired;

		// Token: 0x0400055D RID: 1373
		private bool firstTimeUser;
	}
}
