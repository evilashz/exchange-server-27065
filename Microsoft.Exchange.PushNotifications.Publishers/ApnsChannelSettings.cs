using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000020 RID: 32
	internal class ApnsChannelSettings : PushNotificationSettingsBase
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00005938 File Offset: 0x00003B38
		public ApnsChannelSettings(string appId, string certificateThumbprint, ApnsEndPoint host, bool ignoreCertificateErrors = false) : this(appId, certificateThumbprint, null, host, 500, 300000, 3, 1500, 2, 5000, 5000, 600, ignoreCertificateErrors)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005974 File Offset: 0x00003B74
		public ApnsChannelSettings(string appId, string certificateThumbprint, string certificateThumbprintFallback, ApnsEndPoint host, int connectStepTimeout, int connectTotalTimeout, int connectRetryMax, int connectRetryDelay, int authenticateRetryMax, int readTimeout, int writeTimeout, int backOffTime, bool ignoreCertificateErrors) : base(appId)
		{
			this.CertificateThumbprint = certificateThumbprint;
			this.CertificateThumbprintFallback = certificateThumbprintFallback;
			this.ApnsEndPoint = host;
			this.ConnectStepTimeout = connectStepTimeout;
			this.ConnectTotalTimeout = connectTotalTimeout;
			this.ConnectRetryMax = connectRetryMax;
			this.ConnectRetryDelay = connectRetryDelay;
			this.AuthenticateRetryMax = authenticateRetryMax;
			this.ReadTimeout = readTimeout;
			this.WriteTimeout = writeTimeout;
			this.BackOffTimeInSeconds = backOffTime;
			this.IgnoreCertificateErrors = ignoreCertificateErrors;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000059E6 File Offset: 0x00003BE6
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000059EE File Offset: 0x00003BEE
		public ApnsEndPoint ApnsEndPoint { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000059F7 File Offset: 0x00003BF7
		public string Host
		{
			get
			{
				return this.ApnsEndPoint.Host;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005A04 File Offset: 0x00003C04
		public string FeedbackHost
		{
			get
			{
				return this.ApnsEndPoint.FeedbackHost;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005A11 File Offset: 0x00003C11
		public int Port
		{
			get
			{
				return this.ApnsEndPoint.Port;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005A1E File Offset: 0x00003C1E
		public int FeedbackPort
		{
			get
			{
				return this.ApnsEndPoint.FeedbackPort;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005A2B File Offset: 0x00003C2B
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005A33 File Offset: 0x00003C33
		public string CertificateThumbprint { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005A3C File Offset: 0x00003C3C
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005A44 File Offset: 0x00003C44
		public string CertificateThumbprintFallback { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005A4D File Offset: 0x00003C4D
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00005A55 File Offset: 0x00003C55
		public int ConnectStepTimeout { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005A5E File Offset: 0x00003C5E
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005A66 File Offset: 0x00003C66
		public int ConnectTotalTimeout { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005A6F File Offset: 0x00003C6F
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005A77 File Offset: 0x00003C77
		public int ConnectRetryMax { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005A80 File Offset: 0x00003C80
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005A88 File Offset: 0x00003C88
		public int ConnectRetryDelay { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005A91 File Offset: 0x00003C91
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005A99 File Offset: 0x00003C99
		public int AuthenticateRetryMax { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005AA2 File Offset: 0x00003CA2
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005AAA File Offset: 0x00003CAA
		public int ReadTimeout { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005AB3 File Offset: 0x00003CB3
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005ABB File Offset: 0x00003CBB
		public int WriteTimeout { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005AC4 File Offset: 0x00003CC4
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005ACC File Offset: 0x00003CCC
		public int BackOffTimeInSeconds { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005AD5 File Offset: 0x00003CD5
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005ADD File Offset: 0x00003CDD
		public bool IgnoreCertificateErrors { get; private set; }

		// Token: 0x06000150 RID: 336 RVA: 0x00005AE8 File Offset: 0x00003CE8
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			if (string.IsNullOrWhiteSpace(this.CertificateThumbprint))
			{
				errors.Add(Strings.ValidationErrorEmptyString("CertificateThumbprint"));
			}
			if (string.IsNullOrWhiteSpace(this.Host) || Uri.CheckHostName(this.Host) == UriHostNameType.Unknown)
			{
				errors.Add(Strings.ValidationErrorInvalidUri("Host", this.Host ?? string.Empty, string.Empty));
			}
			if (string.IsNullOrWhiteSpace(this.FeedbackHost) || Uri.CheckHostName(this.FeedbackHost) == UriHostNameType.Unknown)
			{
				errors.Add(Strings.ValidationErrorInvalidUri("FeedbackHost", this.FeedbackHost ?? string.Empty, string.Empty));
			}
			if (this.Port < 0 || this.Port > 65535)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("Port", 0, 65535, this.Port));
			}
			if (this.FeedbackPort < 0 || this.FeedbackPort > 65535)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("FeedbackPort", 0, 65535, this.FeedbackPort));
			}
			if (this.ConnectTotalTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("ConnectTotalTimeout", this.ConnectTotalTimeout));
			}
			if (this.ConnectStepTimeout < 0 || this.ConnectStepTimeout > this.ConnectTotalTimeout)
			{
				errors.Add(Strings.ValidationErrorRangeInteger("ConnectStepTimeout", 0, this.ConnectTotalTimeout, this.ConnectStepTimeout));
			}
			if (this.ConnectRetryMax < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("ConnectRetryMax", this.ConnectRetryMax));
			}
			if (this.ConnectRetryDelay < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("ConnectRetryDelay", this.ConnectRetryDelay));
			}
			if (this.AuthenticateRetryMax < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("AuthenticateRetryMax", this.AuthenticateRetryMax));
			}
			if (this.ReadTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("ReadTimeout", this.ReadTimeout));
			}
			if (this.WriteTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("WriteTimeout", this.WriteTimeout));
			}
		}

		// Token: 0x04000063 RID: 99
		public const string DefaultCertificateThumbprintFallback = null;

		// Token: 0x04000064 RID: 100
		public const int DefaultConnectStepTimeout = 500;

		// Token: 0x04000065 RID: 101
		public const int DefaultConnectTotalTimeout = 300000;

		// Token: 0x04000066 RID: 102
		public const int DefaultConnectRetryMax = 3;

		// Token: 0x04000067 RID: 103
		public const int DefaultConnectRetryDelay = 1500;

		// Token: 0x04000068 RID: 104
		public const int DefaultAuthenticateRetryMax = 2;

		// Token: 0x04000069 RID: 105
		public const int DefaultReadTimeout = 5000;

		// Token: 0x0400006A RID: 106
		public const int DefaultWriteTimeout = 5000;

		// Token: 0x0400006B RID: 107
		public const int DefaultBackOffTimeInSeconds = 600;

		// Token: 0x0400006C RID: 108
		public const bool DefaultIgnoreCertificateErrors = false;
	}
}
