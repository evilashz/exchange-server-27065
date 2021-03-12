using System;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;

namespace Microsoft.Exchange.Servicelets.ExchangeCertificate
{
	// Token: 0x02000004 RID: 4
	internal class ExchangeCertificateServer2 : ExchangeCertificateRpcServer2
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002280 File Offset: 0x00000480
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
				ExchangeCertificateServer2.server = (ExchangeCertificateServer2)RpcServerBase.RegisterServer(typeof(ExchangeCertificateServer2), fileSecurity, 1, false);
				result = true;
			}
			catch (RpcException ex)
			{
				e = ex;
				result = false;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F8 File Offset: 0x000004F8
		public static void Stop()
		{
			if (ExchangeCertificateServer2.server != null)
			{
				RpcServerBase.StopServer(ExchangeCertificateRpcServer2.RpcIntfHandle);
				ExchangeCertificateServer2.server = null;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002311 File Offset: 0x00000511
		public override byte[] CreateCertificate2(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.CreateCertificate(ExchangeCertificateRpcVersion.Version2, inputBlob);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000231A File Offset: 0x0000051A
		public override byte[] GetCertificate2(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.GetCertificate(ExchangeCertificateRpcVersion.Version2, inputBlob);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002323 File Offset: 0x00000523
		public override byte[] RemoveCertificate2(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.RemoveCertificate(ExchangeCertificateRpcVersion.Version2, inputBlob);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000232C File Offset: 0x0000052C
		public override byte[] ExportCertificate2(int version, byte[] inputBlob, SecureString password)
		{
			return ExchangeCertificateServerHelper.ExportCertificate(ExchangeCertificateRpcVersion.Version2, inputBlob, password);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002336 File Offset: 0x00000536
		public override byte[] ImportCertificate2(int version, byte[] inputBlob, SecureString password)
		{
			return ExchangeCertificateServerHelper.ImportCertificate(ExchangeCertificateRpcVersion.Version2, inputBlob, password);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		public override byte[] EnableCertificate2(int version, byte[] inputBlob)
		{
			return ExchangeCertificateServerHelper.EnableCertificate(ExchangeCertificateRpcVersion.Version2, inputBlob);
		}

		// Token: 0x04000005 RID: 5
		private static ExchangeCertificateServer2 server;
	}
}
