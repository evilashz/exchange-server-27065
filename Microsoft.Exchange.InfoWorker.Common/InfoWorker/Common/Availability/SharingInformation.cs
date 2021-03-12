using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000D1 RID: 209
	internal sealed class SharingInformation
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000179E5 File Offset: 0x00015BE5
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000179ED File Offset: 0x00015BED
		public TokenTarget TokenTarget { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000179F6 File Offset: 0x00015BF6
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x000179FE File Offset: 0x00015BFE
		public WebServiceUri TargetSharingEpr { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00017A07 File Offset: 0x00015C07
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00017A0F File Offset: 0x00015C0F
		public Uri TargetAutodiscoverEpr { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00017A18 File Offset: 0x00015C18
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00017A20 File Offset: 0x00015C20
		public SmtpAddress RequestorSmtpAddress { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00017A29 File Offset: 0x00015C29
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00017A31 File Offset: 0x00015C31
		public SmtpAddress SharingKey { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00017A3A File Offset: 0x00015C3A
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00017A42 File Offset: 0x00015C42
		public bool IsFromIntraOrgConnector { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00017A4B File Offset: 0x00015C4B
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00017A53 File Offset: 0x00015C53
		public Exception Exception { get; set; }

		// Token: 0x06000565 RID: 1381 RVA: 0x00017A5C File Offset: 0x00015C5C
		public SharingInformation(SmtpAddress requestorSmtpAddress, SmtpAddress sharingKey, TokenTarget tokenTarget, WebServiceUri targetSharingEpr, Uri targetAutodiscoverEpr)
		{
			this.RequestorSmtpAddress = requestorSmtpAddress;
			this.SharingKey = sharingKey;
			this.TokenTarget = tokenTarget;
			this.TargetSharingEpr = targetSharingEpr;
			this.TargetAutodiscoverEpr = targetAutodiscoverEpr;
			this.IsFromIntraOrgConnector = false;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017A90 File Offset: 0x00015C90
		public SharingInformation(SmtpAddress requestorSmtpAddress, WebServiceUri targetSharingEpr, Uri targetAutodiscoverEpr)
		{
			this.RequestorSmtpAddress = requestorSmtpAddress;
			this.TargetSharingEpr = targetSharingEpr;
			this.TargetAutodiscoverEpr = targetAutodiscoverEpr;
			this.IsFromIntraOrgConnector = true;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017AB4 File Offset: 0x00015CB4
		public SharingInformation(Exception exception)
		{
			this.Exception = exception;
		}
	}
}
