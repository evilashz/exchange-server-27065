using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001066 RID: 4198
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetDagHealthInfoRequestException : LocalizedException
	{
		// Token: 0x0600B0D4 RID: 45268 RVA: 0x00296E11 File Offset: 0x00295011
		public GetDagHealthInfoRequestException(string serverFqdn, string errorMsg) : base(Strings.GetDagHealthInfoRequestException(serverFqdn, errorMsg))
		{
			this.serverFqdn = serverFqdn;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600B0D5 RID: 45269 RVA: 0x00296E2E File Offset: 0x0029502E
		public GetDagHealthInfoRequestException(string serverFqdn, string errorMsg, Exception innerException) : base(Strings.GetDagHealthInfoRequestException(serverFqdn, errorMsg), innerException)
		{
			this.serverFqdn = serverFqdn;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600B0D6 RID: 45270 RVA: 0x00296E4C File Offset: 0x0029504C
		protected GetDagHealthInfoRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverFqdn = (string)info.GetValue("serverFqdn", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600B0D7 RID: 45271 RVA: 0x00296EA1 File Offset: 0x002950A1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverFqdn", this.serverFqdn);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17003859 RID: 14425
		// (get) Token: 0x0600B0D8 RID: 45272 RVA: 0x00296ECD File Offset: 0x002950CD
		public string ServerFqdn
		{
			get
			{
				return this.serverFqdn;
			}
		}

		// Token: 0x1700385A RID: 14426
		// (get) Token: 0x0600B0D9 RID: 45273 RVA: 0x00296ED5 File Offset: 0x002950D5
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x040061BF RID: 25023
		private readonly string serverFqdn;

		// Token: 0x040061C0 RID: 25024
		private readonly string errorMsg;
	}
}
