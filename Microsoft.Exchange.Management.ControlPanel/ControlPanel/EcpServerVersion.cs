using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000476 RID: 1142
	[DataContract]
	public class EcpServerVersion
	{
		// Token: 0x06003976 RID: 14710 RVA: 0x000AE908 File Offset: 0x000ACB08
		public EcpServerVersion(ServerVersion serverVersion)
		{
			this.serverVersion = serverVersion;
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000AE917 File Offset: 0x000ACB17
		// (set) Token: 0x06003978 RID: 14712 RVA: 0x000AE924 File Offset: 0x000ACB24
		[DataMember]
		public int Major
		{
			get
			{
				return this.serverVersion.Major;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x000AE92B File Offset: 0x000ACB2B
		// (set) Token: 0x0600397A RID: 14714 RVA: 0x000AE938 File Offset: 0x000ACB38
		[DataMember]
		public int Minor
		{
			get
			{
				return this.serverVersion.Minor;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000AE93F File Offset: 0x000ACB3F
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x000AE94C File Offset: 0x000ACB4C
		[DataMember]
		public int Revision
		{
			get
			{
				return this.serverVersion.Revision;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040026A8 RID: 9896
		private ServerVersion serverVersion;
	}
}
