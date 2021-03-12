using System;
using System.Net;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200000B RID: 11
	internal sealed class SenderIdValidationBaseContext
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000259F File Offset: 0x0000079F
		public SenderIdValidationBaseContext(SenderIdValidator senderIdValidator, IPAddress ipAddress, RoutingAddress purportedResponsibleAddress, string helloDomain, SmtpServer server)
		{
			this.senderIdValidator = senderIdValidator;
			this.ipAddress = ipAddress;
			this.purportedResponsibleAddress = purportedResponsibleAddress;
			this.helloDomain = helloDomain;
			this.server = server;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025CC File Offset: 0x000007CC
		public SenderIdValidationContext CreateContext(string purportedResponsibleDomain, bool processExpModifier, AsyncCallback asyncCallback, object asyncState)
		{
			SenderIdValidationContext senderIdValidationContext = new SenderIdValidationContext(this, purportedResponsibleDomain, processExpModifier, asyncCallback, asyncState);
			this.numValidations++;
			if (this.numValidations > 10)
			{
				senderIdValidationContext.SetInvalid();
			}
			return senderIdValidationContext;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002604 File Offset: 0x00000804
		public SenderIdValidator SenderIdValidator
		{
			get
			{
				return this.senderIdValidator;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000260C File Offset: 0x0000080C
		public IPAddress IPAddress
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002614 File Offset: 0x00000814
		public RoutingAddress PurportedResponsibleAddress
		{
			get
			{
				return this.purportedResponsibleAddress;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000261C File Offset: 0x0000081C
		public string HelloDomain
		{
			get
			{
				return this.helloDomain;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002624 File Offset: 0x00000824
		public SmtpServer Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000262C File Offset: 0x0000082C
		public bool UsesUncacheableMacro
		{
			get
			{
				return this.usesUncacheableMacro;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002634 File Offset: 0x00000834
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000263C File Offset: 0x0000083C
		public string ExpandedPMacro
		{
			get
			{
				return this.expandedPMacro;
			}
			set
			{
				this.expandedPMacro = value;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002645 File Offset: 0x00000845
		public void SetUncacheable()
		{
			this.usesUncacheableMacro = true;
		}

		// Token: 0x0400001F RID: 31
		public const int MaxRecursiveValidations = 10;

		// Token: 0x04000020 RID: 32
		private readonly SenderIdValidator senderIdValidator;

		// Token: 0x04000021 RID: 33
		private readonly IPAddress ipAddress;

		// Token: 0x04000022 RID: 34
		private readonly RoutingAddress purportedResponsibleAddress;

		// Token: 0x04000023 RID: 35
		private readonly string helloDomain;

		// Token: 0x04000024 RID: 36
		private readonly SmtpServer server;

		// Token: 0x04000025 RID: 37
		private int numValidations;

		// Token: 0x04000026 RID: 38
		private bool usesUncacheableMacro;

		// Token: 0x04000027 RID: 39
		private string expandedPMacro;
	}
}
