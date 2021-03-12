using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200011D RID: 285
	internal class GlsLoggerContext
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003663B File Offset: 0x0003483B
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00036643 File Offset: 0x00034843
		internal int TickStart { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0003664C File Offset: 0x0003484C
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x00036654 File Offset: 0x00034854
		internal string MethodName { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0003665D File Offset: 0x0003485D
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x00036665 File Offset: 0x00034865
		internal string ParameterValue { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0003666E File Offset: 0x0003486E
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00036676 File Offset: 0x00034876
		internal string EndpointHostName { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0003667F File Offset: 0x0003487F
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00036687 File Offset: 0x00034887
		internal bool IsRead { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00036690 File Offset: 0x00034890
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00036698 File Offset: 0x00034898
		internal Guid RequestTrackingGuid { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x000366A1 File Offset: 0x000348A1
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x000366A9 File Offset: 0x000348A9
		internal string ConnectionId { get; set; }

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000366B2 File Offset: 0x000348B2
		internal GlsLoggerContext(string methodName, object parameterValue, string endpointHostName, bool isRead, Guid requestTrackingGuid)
		{
			this.TickStart = Environment.TickCount;
			this.MethodName = methodName;
			this.ParameterValue = parameterValue.ToString();
			this.EndpointHostName = endpointHostName;
			this.IsRead = isRead;
			this.RequestTrackingGuid = requestTrackingGuid;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000366EF File Offset: 0x000348EF
		internal string ResolveEndpointToIpAddress(bool flushCache)
		{
			return this.EndpointHostName;
		}
	}
}
