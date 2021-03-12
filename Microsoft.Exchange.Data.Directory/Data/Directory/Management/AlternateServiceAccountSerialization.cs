using System;
using System.Linq;
using System.Management.Automation;
using System.Security;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006DF RID: 1759
	public static class AlternateServiceAccountSerialization
	{
		// Token: 0x06005145 RID: 20805 RVA: 0x0012CB5C File Offset: 0x0012AD5C
		public static SecureString[] GetSerializationData_AlternateServiceAccountConfiguration_AllCredentials_Password(PSObject clientAccessServerObject)
		{
			ClientAccessServer clientAccessServer = clientAccessServerObject.BaseObject as ClientAccessServer;
			if (clientAccessServer == null || clientAccessServer.AlternateServiceAccountConfiguration == null)
			{
				return null;
			}
			return (from credential in clientAccessServer.AlternateServiceAccountConfiguration.AllCredentials
			select credential.Password).ToArray<SecureString>();
		}
	}
}
