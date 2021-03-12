using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x0200036A RID: 874
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class VerifyCert : ServiceCommand<int>
	{
		// Token: 0x06001C0C RID: 7180 RVA: 0x0006DEAB File Offset: 0x0006C0AB
		public VerifyCert(CallContext callContext, string certRawData) : base(callContext)
		{
			this.certRawData = certRawData;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0006DEBC File Offset: 0x0006C0BC
		protected override int InternalExecute()
		{
			if (string.IsNullOrEmpty(this.certRawData))
			{
				return -1;
			}
			int result;
			try
			{
				byte[] rawData = Convert.FromBase64String(this.certRawData);
				X509Certificate2 certificate = new X509Certificate2(rawData);
				X509Chain x509Chain = new X509Chain(true);
				x509Chain.Build(certificate);
				X509ChainStatusFlags x509ChainStatusFlags = X509ChainStatusFlags.NoError;
				foreach (X509ChainStatus x509ChainStatus in x509Chain.ChainStatus)
				{
					x509ChainStatusFlags |= x509ChainStatus.Status;
				}
				result = (int)x509ChainStatusFlags;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x04000FE3 RID: 4067
		private readonly string certRawData;
	}
}
