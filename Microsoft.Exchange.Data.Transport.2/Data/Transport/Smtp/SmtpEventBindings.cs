using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000009 RID: 9
	internal class SmtpEventBindings
	{
		// Token: 0x04000026 RID: 38
		internal const string EventOnProcessAuthentication = "OnProcessAuthentication";

		// Token: 0x04000027 RID: 39
		internal const string EventOnProxyInboundMessage = "OnProxyInboundMessage";

		// Token: 0x04000028 RID: 40
		internal const string EventOnXSessionParamsCommand = "OnXSessionParamsCommand";

		// Token: 0x04000029 RID: 41
		public const string EventOnAuthCommand = "OnAuthCommand";

		// Token: 0x0400002A RID: 42
		public const string EventOnDataCommand = "OnDataCommand";

		// Token: 0x0400002B RID: 43
		public const string EventOnEhloCommand = "OnEhloCommand";

		// Token: 0x0400002C RID: 44
		public const string EventOnEndOfAuthentication = "OnEndOfAuthentication";

		// Token: 0x0400002D RID: 45
		public const string EventOnEndOfData = "OnEndOfData";

		// Token: 0x0400002E RID: 46
		public const string EventOnEndOfHeaders = "OnEndOfHeaders";

		// Token: 0x0400002F RID: 47
		public const string EventOnHeloCommand = "OnHeloCommand";

		// Token: 0x04000030 RID: 48
		public const string EventOnHelpCommand = "OnHelpCommand";

		// Token: 0x04000031 RID: 49
		public const string EventOnMailCommand = "OnMailCommand";

		// Token: 0x04000032 RID: 50
		public const string EventOnNoopCommand = "OnNoopCommand";

		// Token: 0x04000033 RID: 51
		public const string EventOnRcptCommand = "OnRcptCommand";

		// Token: 0x04000034 RID: 52
		public const string EventOnRcpt2Command = "OnRcpt2Command";

		// Token: 0x04000035 RID: 53
		public const string EventOnReject = "OnReject";

		// Token: 0x04000036 RID: 54
		public const string EventOnRsetCommand = "OnRsetCommand";

		// Token: 0x04000037 RID: 55
		public const string EventOnConnectEvent = "OnConnectEvent";

		// Token: 0x04000038 RID: 56
		public const string EventOnDisconnectEvent = "OnDisconnectEvent";

		// Token: 0x04000039 RID: 57
		public const string EventOnStartTlsCommand = "OnStartTlsCommand";

		// Token: 0x0400003A RID: 58
		internal static readonly string[] All = new string[]
		{
			"OnConnectEvent",
			"OnHeloCommand",
			"OnEhloCommand",
			"OnStartTlsCommand",
			"OnAuthCommand",
			"OnProcessAuthentication",
			"OnEndOfAuthentication",
			"OnXSessionParamsCommand",
			"OnMailCommand",
			"OnRcptCommand",
			"OnDataCommand",
			"OnEndOfHeaders",
			"OnProxyInboundMessage",
			"OnEndOfData",
			"OnHelpCommand",
			"OnNoopCommand",
			"OnReject",
			"OnRsetCommand",
			"OnDisconnectEvent"
		};
	}
}
