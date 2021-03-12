using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002DF RID: 735
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotDeterimineServiceInstanceException : LocalizedException
	{
		// Token: 0x060019BC RID: 6588 RVA: 0x0005D79D File Offset: 0x0005B99D
		public CouldNotDeterimineServiceInstanceException(string domainName) : base(Strings.CouldNotDeterimineServiceInstanceException(domainName))
		{
			this.domainName = domainName;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0005D7B2 File Offset: 0x0005B9B2
		public CouldNotDeterimineServiceInstanceException(string domainName, Exception innerException) : base(Strings.CouldNotDeterimineServiceInstanceException(domainName), innerException)
		{
			this.domainName = domainName;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0005D7C8 File Offset: 0x0005B9C8
		protected CouldNotDeterimineServiceInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.domainName = (string)info.GetValue("domainName", typeof(string));
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0005D7F2 File Offset: 0x0005B9F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("domainName", this.domainName);
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0005D80D File Offset: 0x0005BA0D
		public string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x040009A5 RID: 2469
		private readonly string domainName;
	}
}
