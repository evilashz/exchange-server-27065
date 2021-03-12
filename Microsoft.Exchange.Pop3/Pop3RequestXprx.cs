using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000018 RID: 24
	internal sealed class Pop3RequestXprx : Pop3Request
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003FF8 File Offset: 0x000021F8
		public Pop3RequestXprx(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_XPRX_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_XPRX_Failures;
			base.AllowedStates = ((ResponseFactory.GlobalCriminalComplianceEnabled && ProtocolBaseServices.StoredSecretKeysValid) ? Pop3State.Authenticated : Pop3State.None);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004048 File Offset: 0x00002248
		public override void ParseArguments()
		{
			if (!string.IsNullOrEmpty(base.Arguments))
			{
				this.parameters = base.Arguments.Split(new char[]
				{
					' '
				});
				if (this.parameters.Length != 3)
				{
					this.ParseResult = ParseResult.invalidNumberOfArguments;
				}
				this.ParseResult = ParseResult.success;
				return;
			}
			this.ParseResult = ParseResult.invalidNumberOfArguments;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000040A4 File Offset: 0x000022A4
		public override ProtocolResponse Process()
		{
			try
			{
				if (!GccUtils.SetStoreSessionClientIPEndpointsFromXproxy(base.Factory.Store, this.parameters[0], this.parameters[1], this.parameters[2], base.Session.Connection))
				{
					return new Pop3Response(Pop3Response.Type.err);
				}
			}
			catch (InvalidDatacenterProxyKeyException ex)
			{
				ProtocolBaseServices.LogEvent(Pop3EventLogConstants.Tuple_InvalidDatacenterProxyKey, null, new string[]
				{
					ex.Message
				});
				return new Pop3Response(Pop3Response.Type.err);
			}
			if (base.Session.LrsSession != null)
			{
				base.Session.LrsSession.SetEndpoints(this.parameters[1], this.parameters[2]);
			}
			return new Pop3Response(Pop3Response.Type.ok);
		}

		// Token: 0x04000052 RID: 82
		private string[] parameters;
	}
}
