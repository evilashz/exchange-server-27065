using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C0 RID: 4288
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReserveUriException : FederationException
	{
		// Token: 0x0600B2AE RID: 45742 RVA: 0x0029A0BA File Offset: 0x002982BA
		public UnableToReserveUriException(string uri, string domain, string appId, string errdetails) : base(Strings.ErrorUnableToReserveUri(uri, domain, appId, errdetails))
		{
			this.uri = uri;
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2AF RID: 45743 RVA: 0x0029A0E9 File Offset: 0x002982E9
		public UnableToReserveUriException(string uri, string domain, string appId, string errdetails, Exception innerException) : base(Strings.ErrorUnableToReserveUri(uri, domain, appId, errdetails), innerException)
		{
			this.uri = uri;
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2B0 RID: 45744 RVA: 0x0029A11C File Offset: 0x0029831C
		protected UnableToReserveUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (string)info.GetValue("uri", typeof(string));
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.appId = (string)info.GetValue("appId", typeof(string));
			this.errdetails = (string)info.GetValue("errdetails", typeof(string));
		}

		// Token: 0x0600B2B1 RID: 45745 RVA: 0x0029A1B4 File Offset: 0x002983B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
			info.AddValue("domain", this.domain);
			info.AddValue("appId", this.appId);
			info.AddValue("errdetails", this.errdetails);
		}

		// Token: 0x170038CB RID: 14539
		// (get) Token: 0x0600B2B2 RID: 45746 RVA: 0x0029A20D File Offset: 0x0029840D
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170038CC RID: 14540
		// (get) Token: 0x0600B2B3 RID: 45747 RVA: 0x0029A215 File Offset: 0x00298415
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170038CD RID: 14541
		// (get) Token: 0x0600B2B4 RID: 45748 RVA: 0x0029A21D File Offset: 0x0029841D
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170038CE RID: 14542
		// (get) Token: 0x0600B2B5 RID: 45749 RVA: 0x0029A225 File Offset: 0x00298425
		public string Errdetails
		{
			get
			{
				return this.errdetails;
			}
		}

		// Token: 0x04006231 RID: 25137
		private readonly string uri;

		// Token: 0x04006232 RID: 25138
		private readonly string domain;

		// Token: 0x04006233 RID: 25139
		private readonly string appId;

		// Token: 0x04006234 RID: 25140
		private readonly string errdetails;
	}
}
