using System;
using System.Runtime.Serialization;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200008B RID: 139
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class BposNavBarInfo
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public BposNavBarInfo(string version, NavBarData navBarData)
		{
			this.version = version;
			this.navBarData = navBarData;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0000EBCA File Offset: 0x0000CDCA
		[DataMember]
		public NavBarData NavBarData
		{
			get
			{
				return this.navBarData;
			}
			set
			{
				this.navBarData = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000EBD3 File Offset: 0x0000CDD3
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0000EBDB File Offset: 0x0000CDDB
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x040002B5 RID: 693
		private NavBarData navBarData;

		// Token: 0x040002B6 RID: 694
		private string version;
	}
}
