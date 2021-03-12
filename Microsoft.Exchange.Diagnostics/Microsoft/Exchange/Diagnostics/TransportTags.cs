using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000217 RID: 535
	public struct TransportTags
	{
		// Token: 0x04000B97 RID: 2967
		public const int General = 0;

		// Token: 0x04000B98 RID: 2968
		public const int SmtpReceive = 1;

		// Token: 0x04000B99 RID: 2969
		public const int SmtpSend = 2;

		// Token: 0x04000B9A RID: 2970
		public const int Pickup = 3;

		// Token: 0x04000B9B RID: 2971
		public const int Service = 4;

		// Token: 0x04000B9C RID: 2972
		public const int Queuing = 5;

		// Token: 0x04000B9D RID: 2973
		public const int DSN = 6;

		// Token: 0x04000B9E RID: 2974
		public const int Routing = 7;

		// Token: 0x04000B9F RID: 2975
		public const int Resolver = 8;

		// Token: 0x04000BA0 RID: 2976
		public const int ContentConversion = 9;

		// Token: 0x04000BA1 RID: 2977
		public const int Extensibility = 10;

		// Token: 0x04000BA2 RID: 2978
		public const int Scheduler = 11;

		// Token: 0x04000BA3 RID: 2979
		public const int SecureMail = 12;

		// Token: 0x04000BA4 RID: 2980
		public const int MessageTracking = 13;

		// Token: 0x04000BA5 RID: 2981
		public const int ResourceManager = 14;

		// Token: 0x04000BA6 RID: 2982
		public const int Configuration = 15;

		// Token: 0x04000BA7 RID: 2983
		public const int Dumpster = 16;

		// Token: 0x04000BA8 RID: 2984
		public const int Expo = 17;

		// Token: 0x04000BA9 RID: 2985
		public const int Certificate = 18;

		// Token: 0x04000BAA RID: 2986
		public const int Orar = 19;

		// Token: 0x04000BAB RID: 2987
		public const int ShadowRedundancy = 20;

		// Token: 0x04000BAC RID: 2988
		public const int Approval = 22;

		// Token: 0x04000BAD RID: 2989
		public const int TransportDumpster = 23;

		// Token: 0x04000BAE RID: 2990
		public const int TransportSettingsCache = 24;

		// Token: 0x04000BAF RID: 2991
		public const int TransportRulesCache = 25;

		// Token: 0x04000BB0 RID: 2992
		public const int MicrosoftExchangeRecipientCache = 26;

		// Token: 0x04000BB1 RID: 2993
		public const int RemoteDomainsCache = 27;

		// Token: 0x04000BB2 RID: 2994
		public const int JournalingRulesCache = 28;

		// Token: 0x04000BB3 RID: 2995
		public const int ResourcePool = 29;

		// Token: 0x04000BB4 RID: 2996
		public const int DeliveryAgents = 30;

		// Token: 0x04000BB5 RID: 2997
		public const int Supervision = 31;

		// Token: 0x04000BB6 RID: 2998
		public const int RightsManagement = 32;

		// Token: 0x04000BB7 RID: 2999
		public const int PerimeterSettingsCache = 33;

		// Token: 0x04000BB8 RID: 3000
		public const int PreviousHopLatency = 34;

		// Token: 0x04000BB9 RID: 3001
		public const int FaultInjection = 35;

		// Token: 0x04000BBA RID: 3002
		public const int OrganizationSettingsCache = 36;

		// Token: 0x04000BBB RID: 3003
		public const int AnonymousCertificateValidationResultCache = 37;

		// Token: 0x04000BBC RID: 3004
		public const int AcceptedDomainsCache = 38;

		// Token: 0x04000BBD RID: 3005
		public const int ProxyHubSelector = 39;

		// Token: 0x04000BBE RID: 3006
		public const int MessageResubmission = 40;

		// Token: 0x04000BBF RID: 3007
		public const int Storage = 41;

		// Token: 0x04000BC0 RID: 3008
		public const int Poison = 42;

		// Token: 0x04000BC1 RID: 3009
		public const int HostedEncryption = 43;

		// Token: 0x04000BC2 RID: 3010
		public const int OutboundConnectorsCache = 44;

		// Token: 0x04000BC3 RID: 3011
		public static Guid guid = new Guid("c3ea5adf-c135-45e7-9dff-e1dc3bd67999");
	}
}
