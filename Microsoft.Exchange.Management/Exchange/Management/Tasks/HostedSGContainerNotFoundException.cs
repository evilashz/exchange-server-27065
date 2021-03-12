using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E01 RID: 3585
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HostedSGContainerNotFoundException : LocalizedException
	{
		// Token: 0x0600A4FE RID: 42238 RVA: 0x0028537D File Offset: 0x0028357D
		public HostedSGContainerNotFoundException(string orgName) : base(Strings.HostedSGContainerNotFoundException(orgName))
		{
			this.orgName = orgName;
		}

		// Token: 0x0600A4FF RID: 42239 RVA: 0x00285392 File Offset: 0x00283592
		public HostedSGContainerNotFoundException(string orgName, Exception innerException) : base(Strings.HostedSGContainerNotFoundException(orgName), innerException)
		{
			this.orgName = orgName;
		}

		// Token: 0x0600A500 RID: 42240 RVA: 0x002853A8 File Offset: 0x002835A8
		protected HostedSGContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.orgName = (string)info.GetValue("orgName", typeof(string));
		}

		// Token: 0x0600A501 RID: 42241 RVA: 0x002853D2 File Offset: 0x002835D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("orgName", this.orgName);
		}

		// Token: 0x17003617 RID: 13847
		// (get) Token: 0x0600A502 RID: 42242 RVA: 0x002853ED File Offset: 0x002835ED
		public string OrgName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x04005F7D RID: 24445
		private readonly string orgName;
	}
}
