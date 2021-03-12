using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C2 RID: 4290
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReleaseUriException : FederationException
	{
		// Token: 0x0600B2BD RID: 45757 RVA: 0x0029A346 File Offset: 0x00298546
		public UnableToReleaseUriException(string uri, string domain, string appId, string errdetails) : base(Strings.ErrorUnableToReleaseUri(uri, domain, appId, errdetails))
		{
			this.uri = uri;
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2BE RID: 45758 RVA: 0x0029A375 File Offset: 0x00298575
		public UnableToReleaseUriException(string uri, string domain, string appId, string errdetails, Exception innerException) : base(Strings.ErrorUnableToReleaseUri(uri, domain, appId, errdetails), innerException)
		{
			this.uri = uri;
			this.domain = domain;
			this.appId = appId;
			this.errdetails = errdetails;
		}

		// Token: 0x0600B2BF RID: 45759 RVA: 0x0029A3A8 File Offset: 0x002985A8
		protected UnableToReleaseUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uri = (string)info.GetValue("uri", typeof(string));
			this.domain = (string)info.GetValue("domain", typeof(string));
			this.appId = (string)info.GetValue("appId", typeof(string));
			this.errdetails = (string)info.GetValue("errdetails", typeof(string));
		}

		// Token: 0x0600B2C0 RID: 45760 RVA: 0x0029A440 File Offset: 0x00298640
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uri", this.uri);
			info.AddValue("domain", this.domain);
			info.AddValue("appId", this.appId);
			info.AddValue("errdetails", this.errdetails);
		}

		// Token: 0x170038D2 RID: 14546
		// (get) Token: 0x0600B2C1 RID: 45761 RVA: 0x0029A499 File Offset: 0x00298699
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x170038D3 RID: 14547
		// (get) Token: 0x0600B2C2 RID: 45762 RVA: 0x0029A4A1 File Offset: 0x002986A1
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170038D4 RID: 14548
		// (get) Token: 0x0600B2C3 RID: 45763 RVA: 0x0029A4A9 File Offset: 0x002986A9
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170038D5 RID: 14549
		// (get) Token: 0x0600B2C4 RID: 45764 RVA: 0x0029A4B1 File Offset: 0x002986B1
		public string Errdetails
		{
			get
			{
				return this.errdetails;
			}
		}

		// Token: 0x04006238 RID: 25144
		private readonly string uri;

		// Token: 0x04006239 RID: 25145
		private readonly string domain;

		// Token: 0x0400623A RID: 25146
		private readonly string appId;

		// Token: 0x0400623B RID: 25147
		private readonly string errdetails;
	}
}
