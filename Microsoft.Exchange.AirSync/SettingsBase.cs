using System;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200006B RID: 107
	internal class SettingsBase
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x0002229C File Offset: 0x0002049C
		public SettingsBase(XmlNode request, XmlNode response, ProtocolLogger protocolLogger)
		{
			this.request = request;
			this.response = response;
			this.protocolLogger = protocolLogger;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000222B9 File Offset: 0x000204B9
		public XmlNode Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000222C1 File Offset: 0x000204C1
		public XmlNode Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000222C9 File Offset: 0x000204C9
		public ProtocolLogger ProtocolLogger
		{
			get
			{
				return this.protocolLogger;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000222D1 File Offset: 0x000204D1
		public virtual void Execute()
		{
			this.response = null;
		}

		// Token: 0x040003F3 RID: 1011
		private XmlNode request;

		// Token: 0x040003F4 RID: 1012
		private XmlNode response;

		// Token: 0x040003F5 RID: 1013
		private ProtocolLogger protocolLogger;

		// Token: 0x0200006C RID: 108
		protected enum ErrorCode
		{
			// Token: 0x040003F7 RID: 1015
			Success = 1,
			// Token: 0x040003F8 RID: 1016
			ProtocolError,
			// Token: 0x040003F9 RID: 1017
			AccessDenied,
			// Token: 0x040003FA RID: 1018
			ServerUnavailable,
			// Token: 0x040003FB RID: 1019
			InvalidArguments,
			// Token: 0x040003FC RID: 1020
			ConflictingArguments,
			// Token: 0x040003FD RID: 1021
			DeniedByPolicy
		}
	}
}
