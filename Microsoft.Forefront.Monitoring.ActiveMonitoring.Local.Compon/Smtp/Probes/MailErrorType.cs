using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000257 RID: 599
	public enum MailErrorType
	{
		// Token: 0x0400098D RID: 2445
		Success,
		// Token: 0x0400098E RID: 2446
		SlaExceeded,
		// Token: 0x0400098F RID: 2447
		NoDelivery,
		// Token: 0x04000990 RID: 2448
		VerificationFailure,
		// Token: 0x04000991 RID: 2449
		UnexpectedDelivery,
		// Token: 0x04000992 RID: 2450
		CheckMailException,
		// Token: 0x04000993 RID: 2451
		SendMailException,
		// Token: 0x04000994 RID: 2452
		ProbeTimeOut,
		// Token: 0x04000995 RID: 2453
		ConfigurationError,
		// Token: 0x04000996 RID: 2454
		DnsFailure,
		// Token: 0x04000997 RID: 2455
		FfoConnectionFailure,
		// Token: 0x04000998 RID: 2456
		FfoProxyFailure,
		// Token: 0x04000999 RID: 2457
		FfoAttributionFailure,
		// Token: 0x0400099A RID: 2458
		FfoGlsFailure,
		// Token: 0x0400099B RID: 2459
		FfoAntispamFailure,
		// Token: 0x0400099C RID: 2460
		ShadowFailure,
		// Token: 0x0400099D RID: 2461
		ServerInBackpressure,
		// Token: 0x0400099E RID: 2462
		MiscAckFailure,
		// Token: 0x0400099F RID: 2463
		SmtpAuthFailure,
		// Token: 0x040009A0 RID: 2464
		MailboxLoginFailure,
		// Token: 0x040009A1 RID: 2465
		CertificateExpiredFailure,
		// Token: 0x040009A2 RID: 2466
		StartTlsNotAdvertisedFailure,
		// Token: 0x040009A3 RID: 2467
		UnableToConnect,
		// Token: 0x040009A4 RID: 2468
		AzureDnsFailure,
		// Token: 0x040009A5 RID: 2469
		ServiceLocatorFailure,
		// Token: 0x040009A6 RID: 2470
		SqlQueryFailure,
		// Token: 0x040009A7 RID: 2471
		ConnectorConfigurationError,
		// Token: 0x040009A8 RID: 2472
		ProxySocketFailure,
		// Token: 0x040009A9 RID: 2473
		MaxConcurrentConnectionFailure,
		// Token: 0x040009AA RID: 2474
		ServiceNotAvailableFailure,
		// Token: 0x040009AB RID: 2475
		NoDestinationsFailure,
		// Token: 0x040009AC RID: 2476
		PopProxyFailure,
		// Token: 0x040009AD RID: 2477
		ContentConversionFailure,
		// Token: 0x040009AE RID: 2478
		FindProbeResultsTimeOut,
		// Token: 0x040009AF RID: 2479
		SmtpSendAuthTimeOut,
		// Token: 0x040009B0 RID: 2480
		ActiveDirectoryFailure,
		// Token: 0x040009B1 RID: 2481
		ConnectionDroppedFailure,
		// Token: 0x040009B2 RID: 2482
		CheckMailTimeOut,
		// Token: 0x040009B3 RID: 2483
		SendMailTimeOut,
		// Token: 0x040009B4 RID: 2484
		SendMailConnectTimeOut,
		// Token: 0x040009B5 RID: 2485
		UpdateUndeliveredTimeOut,
		// Token: 0x040009B6 RID: 2486
		NetworkingConfigurationFailure,
		// Token: 0x040009B7 RID: 2487
		SaveStatusTimeout
	}
}
