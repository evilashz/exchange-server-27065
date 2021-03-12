using System;

namespace Microsoft.Exchange.Transport.Agent.ContentFilter
{
	// Token: 0x0200000C RID: 12
	internal static class Constants
	{
		// Token: 0x0400004A RID: 74
		public static readonly Guid ContentFilterWrapperGuid = new Guid("5850470B-ADFD-4EF8-B68A-AFF968721A46");

		// Token: 0x0400004B RID: 75
		public static readonly uint[] ExpectedMessageFailureHResults = new uint[]
		{
			839232U,
			2147942487U,
			2147942511U,
			2147943513U,
			2147944414U,
			2147946736U,
			2148077569U,
			2148077570U,
			2148077571U,
			2148077572U,
			2148077573U,
			2148077574U,
			2148077575U,
			2148077576U,
			2148077577U,
			2148077578U,
			2148077579U,
			2148077580U,
			2148077581U,
			2148077582U,
			2148077583U,
			2148077584U,
			2148077585U,
			2148081665U,
			2148081666U,
			2148081667U,
			2148081668U,
			2148081669U,
			2148081670U,
			2148081671U,
			2148081672U,
			2148081673U,
			2148081674U,
			2148081675U,
			2148081676U,
			2148081677U,
			2148081678U,
			2148081679U,
			2148081680U,
			2148081681U,
			2148081682U,
			2148081683U,
			2148081684U,
			2148081696U,
			2148081697U,
			2148081698U,
			2148081699U,
			2148081700U,
			2148081701U,
			2148081702U,
			2148081703U,
			2148081704U,
			2148081705U,
			2148081706U,
			2148081707U,
			2148081708U,
			2148322819U,
			2148322820U,
			2148322828U,
			2148322993U,
			2148322998U,
			2148322999U,
			2148323000U,
			2148323056U,
			2148323059U,
			2148323060U,
			2148323061U,
			2148323062U,
			2148323063U
		};

		// Token: 0x0200000D RID: 13
		internal static class ComErrorCodes
		{
			// Token: 0x0400004C RID: 76
			public const int InsufficientBuffer = -2147024774;

			// Token: 0x0400004D RID: 77
			public const int AlreadyInitialized = -2147023649;

			// Token: 0x0400004E RID: 78
			public const int ExSMimeInitialization = -1067253755;

			// Token: 0x0400004F RID: 79
			public const int Win32ErrorDiskFull = -2147024784;
		}

		// Token: 0x0200000E RID: 14
		public static class ComInteropPropertyIds
		{
			// Token: 0x04000050 RID: 80
			public const int Error = 0;

			// Token: 0x04000051 RID: 81
			public const int RequestType = 1;

			// Token: 0x04000052 RID: 82
			public const int RequestResult = 2;

			// Token: 0x04000053 RID: 83
			public const int ScanCompleteResult = 3;

			// Token: 0x04000054 RID: 84
			public const int SCL = 4;

			// Token: 0x04000055 RID: 85
			public const int UnmodifiedSCL = 5;

			// Token: 0x04000056 RID: 86
			public const int CustomWordBufferLength = 6;

			// Token: 0x04000057 RID: 87
			public const int CustomWords = 7;

			// Token: 0x04000058 RID: 88
			public const int PRD = 8;

			// Token: 0x04000059 RID: 89
			public const int SenderIdResult = 9;

			// Token: 0x0400005A RID: 90
			public const int FilterDirectory = 10;

			// Token: 0x0400005B RID: 91
			public const int OutlookEmailPostmarkValidationEnabled = 11;

			// Token: 0x0400005C RID: 92
			public const int RecipientsBufferLength = 12;

			// Token: 0x0400005D RID: 93
			public const int Recipients = 13;

			// Token: 0x0400005E RID: 94
			public const int Diagnostics = 14;

			// Token: 0x0400005F RID: 95
			public const int PCL = 15;

			// Token: 0x04000060 RID: 96
			public const int Postmark = 16;

			// Token: 0x04000061 RID: 97
			public const int PremiumSKUEnabled = 17;

			// Token: 0x04000062 RID: 98
			public const int FailureHResult = 18;

			// Token: 0x04000063 RID: 99
			public const int FailureFunctionID = 19;
		}

		// Token: 0x0200000F RID: 15
		public static class RequestTypes
		{
			// Token: 0x04000064 RID: 100
			public static readonly byte[] Initialize = BitConverter.GetBytes(1);

			// Token: 0x04000065 RID: 101
			public static readonly byte[] ScanMessage = BitConverter.GetBytes(2);

			// Token: 0x04000066 RID: 102
			public static readonly byte[] Shutdown = BitConverter.GetBytes(3);
		}

		// Token: 0x02000010 RID: 16
		internal enum FailureFunctionIDs
		{
			// Token: 0x04000068 RID: 104
			NoError,
			// Token: 0x04000069 RID: 105
			MessageStreamQueryInterface,
			// Token: 0x0400006A RID: 106
			RecipientBufferAllocation,
			// Token: 0x0400006B RID: 107
			SmartScreenEvaluate,
			// Token: 0x0400006C RID: 108
			SmartScreenResultGetSCL,
			// Token: 0x0400006D RID: 109
			SmartScreenResultGetUnmodifiedSCL,
			// Token: 0x0400006E RID: 110
			SmartScreenResultGetPCL,
			// Token: 0x0400006F RID: 111
			SmartScreenResultGetDiagnostics,
			// Token: 0x04000070 RID: 112
			SmartScreenResultGetPresolveStatus,
			// Token: 0x04000071 RID: 113
			PropertyBagPutSCL,
			// Token: 0x04000072 RID: 114
			PropertyBagPutUnmodifiedSCL,
			// Token: 0x04000073 RID: 115
			PropertyBagPutDiagnostics,
			// Token: 0x04000074 RID: 116
			PropertyBagPutPCL,
			// Token: 0x04000075 RID: 117
			PropertyBagPutPostmark
		}
	}
}
