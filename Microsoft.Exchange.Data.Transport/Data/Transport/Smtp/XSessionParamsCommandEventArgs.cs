using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000047 RID: 71
	internal class XSessionParamsCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006367 File Offset: 0x00004567
		public XSessionParamsCommandEventArgs(SmtpSession smtpSession, Guid destMdbGuid, XSessionType type) : base(smtpSession)
		{
			this.DestinationMdbGuid = destMdbGuid;
			this.SessionType = type;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000637E File Offset: 0x0000457E
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00006386 File Offset: 0x00004586
		public Guid DestinationMdbGuid { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000638F File Offset: 0x0000458F
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00006397 File Offset: 0x00004597
		public XSessionType SessionType { get; private set; }
	}
}
