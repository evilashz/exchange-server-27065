using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000014 RID: 20
	internal class ServerInfoAnchorMailbox : AnchorMailbox
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004810 File Offset: 0x00002A10
		public ServerInfoAnchorMailbox(string fqdn, IRequestContext requestContext) : base(AnchorSource.ServerInfo, fqdn, requestContext)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			base.NotFoundExceptionCreator = delegate()
			{
				string message = string.Format("Cannot find server {0}.", fqdn);
				return new ServerNotFoundException(message, fqdn);
			};
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000486A File Offset: 0x00002A6A
		public ServerInfoAnchorMailbox(BackEndServer backendServer, IRequestContext requestContext) : base(AnchorSource.ServerInfo, backendServer.Fqdn, requestContext)
		{
			this.BackEndServer = backendServer;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004882 File Offset: 0x00002A82
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000488A File Offset: 0x00002A8A
		public BackEndServer BackEndServer { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004893 File Offset: 0x00002A93
		public string Fqdn
		{
			get
			{
				return (string)base.SourceObject;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000048A0 File Offset: 0x00002AA0
		public override BackEndServer TryDirectBackEndCalculation()
		{
			if (this.BackEndServer != null)
			{
				return this.BackEndServer;
			}
			int? num = ServerLookup.LookupVersion(this.Fqdn);
			if (num == null)
			{
				return base.CheckForNullAndThrowIfApplicable<BackEndServer>(null);
			}
			this.BackEndServer = new BackEndServer(this.Fqdn, num.Value);
			return this.BackEndServer;
		}
	}
}
