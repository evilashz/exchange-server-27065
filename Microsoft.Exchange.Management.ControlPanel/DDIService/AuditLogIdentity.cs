using System;
using System.ServiceModel;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020003B4 RID: 948
	internal class AuditLogIdentity
	{
		// Token: 0x060031AE RID: 12718 RVA: 0x0009990E File Offset: 0x00097B0E
		internal AuditLogIdentity(Identity identity)
		{
			this.Parse(identity.RawIdentity);
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x00099922 File Offset: 0x00097B22
		// (set) Token: 0x060031B0 RID: 12720 RVA: 0x0009992A File Offset: 0x00097B2A
		public string StartDate { get; set; }

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x00099933 File Offset: 0x00097B33
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x0009993B File Offset: 0x00097B3B
		public string EndDate { get; set; }

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x060031B3 RID: 12723 RVA: 0x00099944 File Offset: 0x00097B44
		// (set) Token: 0x060031B4 RID: 12724 RVA: 0x0009994C File Offset: 0x00097B4C
		public string ObjectId { get; set; }

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x00099955 File Offset: 0x00097B55
		// (set) Token: 0x060031B6 RID: 12726 RVA: 0x0009995D File Offset: 0x00097B5D
		public string Cmdlet { get; set; }

		// Token: 0x060031B7 RID: 12727 RVA: 0x00099968 File Offset: 0x00097B68
		protected virtual void Parse(string rawIdentity)
		{
			string[] array = rawIdentity.Split(new char[]
			{
				'\n'
			});
			if (array.Length >= 3)
			{
				this.ObjectId = array[0];
				this.StartDate = array[1];
				this.EndDate = array[2];
				this.Cmdlet = array[3];
				return;
			}
			throw new FaultException(new ArgumentException("rawIdentity").Message);
		}
	}
}
