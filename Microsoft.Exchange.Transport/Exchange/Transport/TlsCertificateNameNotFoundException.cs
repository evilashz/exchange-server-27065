using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000581 RID: 1409
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TlsCertificateNameNotFoundException : ExchangeConfigurationException
	{
		// Token: 0x0600413C RID: 16700 RVA: 0x0011A795 File Offset: 0x00118995
		public TlsCertificateNameNotFoundException(string tlsCertificateName, string connectorName) : base(Strings.TlsCertificateNameNotFound(tlsCertificateName, connectorName))
		{
			this.tlsCertificateName = tlsCertificateName;
			this.connectorName = connectorName;
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x0011A7B2 File Offset: 0x001189B2
		public TlsCertificateNameNotFoundException(string tlsCertificateName, string connectorName, Exception innerException) : base(Strings.TlsCertificateNameNotFound(tlsCertificateName, connectorName), innerException)
		{
			this.tlsCertificateName = tlsCertificateName;
			this.connectorName = connectorName;
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x0011A7D0 File Offset: 0x001189D0
		protected TlsCertificateNameNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.tlsCertificateName = (string)info.GetValue("tlsCertificateName", typeof(string));
			this.connectorName = (string)info.GetValue("connectorName", typeof(string));
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0011A825 File Offset: 0x00118A25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("tlsCertificateName", this.tlsCertificateName);
			info.AddValue("connectorName", this.connectorName);
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x0011A851 File Offset: 0x00118A51
		public string TlsCertificateName
		{
			get
			{
				return this.tlsCertificateName;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x0011A859 File Offset: 0x00118A59
		public string ConnectorName
		{
			get
			{
				return this.connectorName;
			}
		}

		// Token: 0x0400253E RID: 9534
		private readonly string tlsCertificateName;

		// Token: 0x0400253F RID: 9535
		private readonly string connectorName;
	}
}
