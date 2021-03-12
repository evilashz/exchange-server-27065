using System;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;

namespace Microsoft.Exchange.Servicelets.ExchangeCertificate
{
	// Token: 0x02000003 RID: 3
	internal class ExchangeCertificateServer : ExchangeCertificateRpcServer
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021AC File Offset: 0x000003AC
		public static bool Start(out Exception e)
		{
			e = null;
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.Read, AccessControlType.Allow);
			FileSecurity fileSecurity = new FileSecurity();
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			bool result;
			try
			{
				ExchangeCertificateServer.server = (ExchangeCertificateServer)RpcServerBase.RegisterServer(typeof(ExchangeCertificateServer), fileSecurity, 1, false);
				result = true;
			}
			catch (RpcException ex)
			{
				e = ex;
				result = false;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002224 File Offset: 0x00000424
		public static void Stop()
		{
			if (ExchangeCertificateServer.server != null)
			{
				RpcServerBase.StopServer(ExchangeCertificateRpcServer.RpcIntfHandle);
				ExchangeCertificateServer.server = null;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000223D File Offset: 0x0000043D
		public override byte[] CreateCertificate(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.CreateCertificate(ExchangeCertificateRpcVersion.Version1, inputBlob);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002246 File Offset: 0x00000446
		public override byte[] GetCertificate(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.GetCertificate(ExchangeCertificateRpcVersion.Version1, inputBlob);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224F File Offset: 0x0000044F
		public override byte[] RemoveCertificate(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.RemoveCertificate(ExchangeCertificateRpcVersion.Version1, inputBlob);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002258 File Offset: 0x00000458
		public override byte[] ExportCertificate(int version, byte[] inputBlob, SecureString password)
		{
			return ExchangeCertificateServerHelper.ExportCertificate(ExchangeCertificateRpcVersion.Version1, inputBlob, password);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002262 File Offset: 0x00000462
		public override byte[] ImportCertificate(int version, byte[] inputBlob, SecureString password)
		{
			return ExchangeCertificateServerHelper.ImportCertificate(ExchangeCertificateRpcVersion.Version1, inputBlob, password);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000226C File Offset: 0x0000046C
		public override byte[] EnableCertificate(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.EnableCertificate(ExchangeCertificateRpcVersion.Version1, inputBlob);
		}

		// Token: 0x04000003 RID: 3
		internal const string RequestStoreName = "REQUEST";

		// Token: 0x04000004 RID: 4
		private static ExchangeCertificateServer server;
	}
}
