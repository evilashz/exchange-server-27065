using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class EasConnectionParameters : ConnectionParameters
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00005C44 File Offset: 0x00003E44
		public EasConnectionParameters(INamedObject id, ILog log, EasProtocolVersion easProtocolVersion = EasProtocolVersion.Version140, bool acceptMultipart = false, bool requestCompression = false, string clientLanguage = null) : base(id, log, long.MaxValue, 300000)
		{
			this.EasProtocolVersion = easProtocolVersion;
			this.AcceptMultipart = acceptMultipart;
			this.RequestCompression = requestCompression;
			this.ClientLanguage = clientLanguage;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005C7B File Offset: 0x00003E7B
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00005C83 File Offset: 0x00003E83
		public EasProtocolVersion EasProtocolVersion { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005C8C File Offset: 0x00003E8C
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00005C94 File Offset: 0x00003E94
		internal bool AcceptMultipart { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00005C9D File Offset: 0x00003E9D
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00005CA5 File Offset: 0x00003EA5
		internal bool RequestCompression { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005CAE File Offset: 0x00003EAE
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00005CB6 File Offset: 0x00003EB6
		internal string ClientLanguage { get; set; }
	}
}
