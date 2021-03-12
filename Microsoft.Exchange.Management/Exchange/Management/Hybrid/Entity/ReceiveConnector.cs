using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008FF RID: 2303
	internal class ReceiveConnector : IReceiveConnector, IEntity<IReceiveConnector>
	{
		// Token: 0x060051A4 RID: 20900 RVA: 0x0015289D File Offset: 0x00150A9D
		public ReceiveConnector()
		{
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x001528A8 File Offset: 0x00150AA8
		public ReceiveConnector(ReceiveConnector rc)
		{
			this.Identity = (ADObjectId)rc.Identity;
			this.Server = rc.Server;
			this.TlsCertificateName = rc.TlsCertificateName;
			this.TlsDomainCapabilities = ((rc.TlsDomainCapabilities != null && rc.TlsDomainCapabilities.Count > 0) ? rc.TlsDomainCapabilities[0] : null);
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x0015290F File Offset: 0x00150B0F
		// (set) Token: 0x060051A7 RID: 20903 RVA: 0x00152917 File Offset: 0x00150B17
		public ADObjectId Identity { get; set; }

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x00152920 File Offset: 0x00150B20
		// (set) Token: 0x060051A9 RID: 20905 RVA: 0x00152928 File Offset: 0x00150B28
		public ADObjectId Server { get; set; }

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x00152931 File Offset: 0x00150B31
		// (set) Token: 0x060051AB RID: 20907 RVA: 0x00152939 File Offset: 0x00150B39
		public SmtpX509Identifier TlsCertificateName { get; set; }

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x00152942 File Offset: 0x00150B42
		// (set) Token: 0x060051AD RID: 20909 RVA: 0x0015294A File Offset: 0x00150B4A
		public SmtpReceiveDomainCapabilities TlsDomainCapabilities { get; set; }

		// Token: 0x060051AE RID: 20910 RVA: 0x00152953 File Offset: 0x00150B53
		private static bool AreEqual(SmtpReceiveDomainCapabilities a, SmtpReceiveDomainCapabilities b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x0015296A File Offset: 0x00150B6A
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return "<New>";
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x00152988 File Offset: 0x00150B88
		public bool Equals(IReceiveConnector obj)
		{
			return obj != null && string.Equals(this.Server.Name, obj.Server.Name, StringComparison.InvariantCultureIgnoreCase) && TaskCommon.AreEqual(this.TlsCertificateName, obj.TlsCertificateName) && ReceiveConnector.AreEqual(this.TlsDomainCapabilities, obj.TlsDomainCapabilities);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x001529DC File Offset: 0x00150BDC
		public IReceiveConnector Clone(ADObjectId identity)
		{
			ReceiveConnector receiveConnector = new ReceiveConnector();
			receiveConnector.UpdateFrom(this);
			receiveConnector.Identity = identity;
			return receiveConnector;
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x001529FE File Offset: 0x00150BFE
		public void UpdateFrom(IReceiveConnector obj)
		{
			this.Server = obj.Server;
			this.TlsCertificateName = obj.TlsCertificateName;
			this.TlsDomainCapabilities = obj.TlsDomainCapabilities;
		}
	}
}
