using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200008F RID: 143
	internal class AddressListGrammar : DirectoryGrammar
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00013D90 File Offset: 0x00011F90
		public AddressListGrammar(Guid addressListGuid)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "AddressListGrammar constructor - addressListGuid='{0}'", new object[]
			{
				addressListGuid
			});
			this.addressListGuid = addressListGuid;
			this.fileName = addressListGuid.ToString();
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00013DDE File Offset: 0x00011FDE
		public override string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00013DE6 File Offset: 0x00011FE6
		protected override bool ShouldAcceptGrammarEntry(ADEntry entry)
		{
			return entry.AddressListGuids.Contains(this.addressListGuid);
		}

		// Token: 0x04000322 RID: 802
		private readonly Guid addressListGuid;

		// Token: 0x04000323 RID: 803
		private readonly string fileName;
	}
}
