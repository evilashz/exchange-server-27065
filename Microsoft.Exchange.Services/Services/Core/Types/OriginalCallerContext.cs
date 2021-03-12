using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000835 RID: 2101
	internal class OriginalCallerContext
	{
		// Token: 0x06003C8A RID: 15498 RVA: 0x000D5E99 File Offset: 0x000D4099
		private OriginalCallerContext()
		{
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000D5EA4 File Offset: 0x000D40A4
		public static OriginalCallerContext FromAuthZClientInfo(AuthZClientInfo authZClientInfo)
		{
			return new OriginalCallerContext
			{
				OrganizationId = authZClientInfo.GetADRecipientSessionContext().OrganizationId,
				Sid = authZClientInfo.ObjectSid,
				IdentifierString = authZClientInfo.ToCallerString()
			};
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000D5EE3 File Offset: 0x000D40E3
		public static OriginalCallerContext Empty
		{
			get
			{
				return OriginalCallerContext.EmptyInstance;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000D5EEA File Offset: 0x000D40EA
		// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000D5EF2 File Offset: 0x000D40F2
		public SecurityIdentifier Sid { get; private set; }

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000D5EFB File Offset: 0x000D40FB
		// (set) Token: 0x06003C90 RID: 15504 RVA: 0x000D5F03 File Offset: 0x000D4103
		public string IdentifierString { get; private set; }

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000D5F0C File Offset: 0x000D410C
		// (set) Token: 0x06003C92 RID: 15506 RVA: 0x000D5F14 File Offset: 0x000D4114
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x04002171 RID: 8561
		private static readonly OriginalCallerContext EmptyInstance = new OriginalCallerContext();
	}
}
