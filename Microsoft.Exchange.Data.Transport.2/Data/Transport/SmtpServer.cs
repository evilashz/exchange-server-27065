using System;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000029 RID: 41
	public abstract class SmtpServer : ICloneableInternal
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005C9A File Offset: 0x00003E9A
		internal SmtpServer()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000DB RID: 219
		public abstract string Name { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000DC RID: 220
		public abstract Version Version { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000DD RID: 221
		public abstract IPPermission IPPermission { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000DE RID: 222
		public abstract AddressBook AddressBook { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DF RID: 223
		public abstract AcceptedDomainCollection AcceptedDomains { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E0 RID: 224
		public abstract RemoteDomainCollection RemoteDomains { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005CA2 File Offset: 0x00003EA2
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005CAA File Offset: 0x00003EAA
		internal Agent AssociatedAgent
		{
			get
			{
				return this.associatedAgent;
			}
			set
			{
				this.associatedAgent = value;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005CB3 File Offset: 0x00003EB3
		object ICloneableInternal.Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060000E4 RID: 228
		public abstract void SubmitMessage(EmailMessage message);

		// Token: 0x0400013F RID: 319
		private Agent associatedAgent;
	}
}
